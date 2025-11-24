# 🏗️ Constructeurs & Forme Canonique en C++

[...retorn en rèire](../menu.md)

---

Les constructeurs, le constructeur de copie et l’opérateur d’affectation sont au cœur de la gestion de ressources en C++.
Quand ta classe gère *une ressource* (mémoire dynamique, fichier, socket, mutex, etc.), tu dois respecter un schéma précis : **RAII** + **forme canonique**.

---

# 4.1 🧬 Constructeur de copie

Le **constructeur de copie** sert à *initialiser* un objet **à partir d’un autre objet existant**.

```cpp
class A {
public:
    A(const A& other) {
        std::cout << "Constructeur de copie\n";
    }
};
```

---

## 📌 Quand est-il utilisé automatiquement ?

### 1️⃣ Lorsqu’une fonction **retourne un objet par valeur**

```cpp
A creer() {
    A x;
    return x;  // utilisation du constructeur de copie (ou move)
}
```

Selon l’optimisation du compilateur :

* C++ moderne → RVO / NRVO (Return Value Optimization) : pas toujours de copie effective
* Mais conceptuellement, **la copie est nécessaire**

---

### 2️⃣ Lors de l’appel de **fonctions binaires** (opérateurs +, -, etc.)

Exemple classique :

```cpp
A operator+(const A& a, const A& b) {
    A result = a;        // copie nécessaire
    // ...
    return result;       // possible RVO mais conceptuellement copie
}
```

💡 Même si en pratique le compilateur optimise, **le constructeur de copie doit exister**.

---

## 🎯 Grand usage : gérer la **copie profonde** (deep copy)

Si ta classe contient :

* un `new`
* un buffer dynamique
* une ressource système

Alors **le constructeur de copie doit dupliquer la ressource** :

```cpp
class Buffer {
    int* data;
    size_t size;
public:
    Buffer(size_t s) : size{s}, data(new int[s]) {}

    Buffer(const Buffer& other)
        : size(other.size), data(new int[other.size]) {
        std::copy(other.data, other.data + size, data);
    }
};
```

🎉 Ici, chaque Buffer possède sa **vraie copie personnelle** des données.

---

# 4.2 🔄 Opérateur d’affectation (`operator=`)

Le constructeur de copie sert pour **initialiser**.
`operator=` sert pour **assigner un objet existant**.

```cpp
A a;
A b;
b = a;  // opérateur d’affectation
```

---

## 📌 Obligatoire en présence de **ressources dynamiques**

Si la classe contient un `new` dans son constructeur, elle doit fournir un opérateur d’affectation **deep copy**, sinon :

* double `delete`
* fuite mémoire
* comportement indéfini

👉 C’est l’un des points où les débutants se trompent le plus.

---

## 📜 Schéma classique : “Destructeur + Constructeur de copie”

On l’appelle **règle des 3** (C++98) ou **des 5** (C++11 avec move).

### Modèle recommandé

```cpp
class Buffer {
    int* data;
    size_t size;

public:
    Buffer(size_t s) : size{s}, data(new int[s]) {}

    // Constructeur de copie
    Buffer(const Buffer& other)
        : size(other.size), data(new int[other.size]) {
        std::copy(other.data, other.data + size, data);
    }

    // Operateur =
    Buffer& operator=(const Buffer& other) {
        if (this != &other) {         // éviter auto-affectation
            delete[] data;            // nettoyer ancienne ressource

            size = other.size;
            data = new int[size];     // nouvelle ressource copiée
            std::copy(other.data, other.data + size, data);
        }
        return *this;
    }

    // Destructeur
    ~Buffer() {
        delete[] data;
    }
};
```

---

## 🧠 Pourquoi “destructeur + constructeur de copie” ?

Parce que l’opérateur = doit gérer la copie profonde **comme si on détruisait l’ancien objet pour en recréer un nouveau**, mais *sans casser la référence existante*.

---

# 4.3 📦 Forme canonique d’une classe (Rule of 3)

Une classe qui gère une ressource doit fournir **au moins** :

### 🧱 1. Constructeur par défaut

```cpp
A() { /* init */ }
```

### 🧱 2. Constructeur de copie

```cpp
A(const A& other);
```

### 🧱 3. Destructeur

```cpp
~A();
```

### 🧱 4. Opérateur d’affectation

```cpp
A& operator=(const A& other);
```

C’est la **forme canonique** en C++98 (avant les move semantics).

---

# 🚀 Bonus moderne : la “Rule of 5” (C++11+)

Avec les move semantics, on ajoute :

### 5️⃣ Constructeur de move

```cpp
A(A&& other);
```

### 6️⃣ Opérateur d’affectation de move

```cpp
A& operator=(A&& other);
```

💡 Pour les classes qui gèrent des ressources, c’est **fortement recommandé**.

---

# 4.4 🎯 Quand faut-il implémenter ces éléments ?

| Classe                                                   | A un destructeur ?    | A un constructeur de copie ? | A un operator= ?  |
| -------------------------------------------------------- | --------------------- | ---------------------------- | ----------------- |
| Classe simple sans pointeurs                             | Pas nécessaire        | Pas nécessaire               | Pas nécessaire    |
| Classe avec `std::string`, `std::vector`, smart pointers | Pas nécessaire (RAII) | Pas nécessaire               | Pas nécessaire    |
| Classe avec `new` / `malloc` / FILE* / socket            | **Indispensable**     | **Indispensable**            | **Indispensable** |

👉 Si tu utilises les containers STL (vector, string, unique_ptr…), tu n’as quasiment jamais besoin d’écrire ces trois-là.

👉 Si tu utilises un tableau dynamique (`new[]`), alors **obligatoire**.

---

# 4.5 🧩 Résumé global

### Constructeur de copie

* utilisé lors du retour par valeur
* utilisé par les opérateurs binaires (result = a + b)
* nécessaire pour le deep copy

### Opérateur d’affectation

* utilisé lors des affectations (`a = b`)
* doit éviter auto-affectation
* doit libérer l’ancienne ressource
* doit copier profondément

### Forme canonique

* constructeur par défaut
* constructeur de copie
* destructeur
* operator=
  ➡️ indispensable pour les classes avec mémoire dynamique

---

# 4.6 📝 Exemple final clair et complet

Voici une classe complète conforme **à la forme canonique**, avec **comportement profond** et **messages pour visualiser les appels** :

```cpp
class Ressource {
    int* data;
    size_t size;

public:
    // Constructeur par défaut
    Ressource(size_t s = 0)
        : size{s}, data(s ? new int[s] : nullptr) {
        std::cout << "Default ctor\n";
    }

    // Constructeur de copie
    Ressource(const Ressource& other)
        : size{other.size}, data(other.size ? new int[other.size] : nullptr) {
        std::cout << "Copy ctor\n";
        std::copy(other.data, other.data + size, data);
    }

    // Opérateur d’affectation
    Ressource& operator=(const Ressource& other) {
        std::cout << "Operator=\n";
        if (this != &other) {
            delete[] data;

            size = other.size;
            data = size ? new int[size] : nullptr;
            std::copy(other.data, other.data + size, data);
        }
        return *this;
    }

    // Destructeur
    ~Ressource() {
        std::cout << "Destructor\n";
        delete[] data;
    }
};
```

---

[...retorn en rèire](../menu.md)