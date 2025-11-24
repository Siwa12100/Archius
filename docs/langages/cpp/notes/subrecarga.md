# 🎛️ Surcharge (Overloading) en C++

[...retorn en rèire](../menu.md)

---

La surcharge permet d’utiliser **le même nom** pour **plusieurs fonctions** tant que leurs **signatures diffèrent**.
C’est un pilier de l’écriture de code propre, expressif et lisible en C++.

---

# 3.1 🎯 Surcharge de fonctions

## 🔧 Principe général

Une fonction est surchargée quand **elle a le même nom**, mais :

* un **nombre d’arguments différent**,
* ou des **types différents**.

👉 Le **retour** ne compte *jamais* dans la surcharge.
👉 Le compilateur choisit la bonne fonction grâce à la **résolution de surcharge**.

---

## 🟪 Par type d’arguments

```cpp
void afficher(int x)       { std::cout << "int : " << x; }
void afficher(double x)    { std::cout << "double : " << x; }
void afficher(std::string s) { std::cout << "string : " << s; }

afficher(10);         // appelle afficher(int)
afficher(3.14);       // appelle afficher(double)
afficher("Salut");    // appelle afficher(string)
```

---

## 🟪 Par nombre d’arguments

```cpp
int addition(int a, int b) {
    return a + b;
}

int addition(int a, int b, int c) {
    return a + b + c;
}
```

---

## ⚠️ Non distinction entre méthode const et non-const à l’appel

C’est un cas très important en POO !

```cpp
class Compte {
public:
    int get()       { std::cout << "normal\n"; return 1; }
    int get() const { std::cout << "const\n"; return 2; }
};

void test() {
    Compte c;
    const Compte cc;

    c.get();   // appelle get() normal
    cc.get();  // appelle get() const
}
```

💡 Ici la surcharge ne dépend pas du type `Compte`, mais du fait que **l’objet est const ou non**.

➡️ Le compilateur choisit selon la **qualification const du receiver**.

⚠️ Mais : **on ne peut pas surcharger uniquement par const du *retour*** ⛔
(Ça ne fait pas partie de la signature.)

---

# 3.2 🔀 Surcharge via pointeurs et références

Deux points cruciaux :

* différence entre **T*** et **const T***
* différence entre **T&** et **const T&**

Ces différences font partie de la **signature**, donc permettent de surcharger.

---

## 📌 T* vs const T*

```cpp
void process(int* p) {
    std::cout << "pointeur normal\n";
}

void process(const int* p) {
    std::cout << "pointeur vers const\n";
}

int x = 10;
const int y = 20;

process(&x); // → pointeur normal
process(&y); // → pointeur vers const
```

💡 règle :
Le compilateur choisit la meilleure correspondance **la plus const-correcte**.

---

## 📌 T& vs const T&

```cpp
void afficher(int& x) {
    std::cout << "ref non const\n";
}

void afficher(const int& x) {
    std::cout << "ref const\n";
}

int a = 10;
const int b = 20;

afficher(a); // ref non const
afficher(b); // ref const
```

🧠 Pourquoi ?
Parce qu’un `int&` **ne peut référencer qu’un objet modifiable**, mais un `const int&` peut référencer **tous les types** (modifiable ou non).

💡 Cette propriété est utilisée partout en C++ pour éviter les copies inutiles (ex : `const std::string&` dans les API).

---

# 3.3 ⚡ Surcharge des opérateurs

L’un des aspects les plus puissants (mais souvent mal maîtrisés) du C++.

## 🧩 Principes généraux

* Tous les opérateurs ne sont pas surchargeables (ex : `?:`, `.`).
* On surcharge en gardant le **sens naturel** (pas de `operator+` qui divise !!).
* Les opérateurs peuvent être :

  * des **méthodes membres**,
  * ou des **fonctions libres** (très fréquent).

---

## 3.3.1 🔢 Surcharge de `operator[]`

Utilisé pour écrire :

```cpp
tableau[i]
```

### Exemple :

```cpp
class Tableau {
private:
    int data[10];

public:
    int& operator[](size_t i) {
        return data[i];
    }

    const int& operator[](size_t i) const {
        return data[i];
    }
};
```

👉 Version const → indispensable pour les objets `const`.

---

## 3.3.2 🟦 `operator=` (assignation)

Cas où il faut faire **une copie profonde** (deep copy).
Indispensable si votre classe possède des ressources allouées dynamiquement.

```cpp
class Buffer {
private:
    int* data;
    size_t size;

public:
    Buffer(size_t size) : size(size), data(new int[size]) {}

    // opérateur d’assignation
    Buffer& operator=(const Buffer& other) {
        if (this != &other) {          // auto-assignation
            delete[] data;             // libération ancienne mémoire
            size = other.size;
            data = new int[size];
            std::copy(other.data, other.data + size, data);
        }
        return *this;
    }

    ~Buffer() { delete[] data; }
};
```

🎯 Objectif : éviter les **double delete** et les fuites mémoire (RAII).

---

## 3.3.3 ➕ Opérateurs arithmétiques (+, -, *, /)

Ils peuvent être définis comme :

* méthodes membres (rare)
* fonctions `friend` (souvent)
* fonctions libres (meilleur design dans beaucoup de cas)

Exemple simple :

```cpp
class Vector2D {
public:
    double x, y;

    Vector2D(double x, double y) : x{x}, y{y} {}
};

Vector2D operator+(const Vector2D& a, const Vector2D& b) {
    return Vector2D(a.x + b.x, a.y + b.y);
}

Vector2D c = a + b;
```

🔍 Pourquoi souvent en fonction libre ?

* permet la commutativité (ex : `a + b`)
* évite d’ajouter trop de méthodes à la classe
* garde les opérations mathématiques externes

---

## 3.3.4 🔼 Pré-incrémentation ++p vs post-incrémentation p++

Différence extrêmement importante.

### Pré-incrémentation (++p)

* Modifie l’objet
* Retourne une **référence vers l’objet modifié**

```cpp
class Entier {
private:
    int x;
public:
    Entier(int v) : x(v) {}

    Entier& operator++() {   // ++p
        ++x;
        return *this;
    }
};
```

---

### Post-incrémentation (p++)

* Modifie l’objet
* Mais retourne **l’ancienne valeur** (copie)

```cpp
class Entier {
public:
    Entier operator++(int) {  // p++  (le int est un paramètre factice)
        Entier temp = *this;  // sauvegarde de l’état
        ++x;                  // modification
        return temp;          // renvoie l’ancien état
    }
};
```

💡 Le paramètre `int` sert juste à différencier les deux surcharges !

---

## 3.3.5 📝 Opérateurs d’insertion/extraction `<<` et `>>`

Très fréquents pour rendre une classe “imprimable”.

### operator<<

```cpp
class Vector2D {
    double x, y;

public:
    Vector2D(double x, double y) : x{x}, y{y} {}

    friend std::ostream& operator<<(std::ostream& os, const Vector2D& v);
};

std::ostream& operator<<(std::ostream& os, const Vector2D& v) {
    return os << "(" << v.x << ", " << v.y << ")";
}
```

### operator>>

```cpp
friend std::istream& operator>>(std::istream& is, Vector2D& v) {
    return is >> v.x >> v.y;
}
```

---

## 3.3.6 🗂️ Opérateurs et copie profonde (deep copy)

Quand votre classe gère :

* un tableau dynamique
* une ressource système (fichier, socket…)
* de la mémoire allouée
* un buffer C…

Alors il faut impérativement surcharger **au moins** :

* `operator=`
* constructeur de copie
* destructeur

C’est la **règle des 3 (ou 5 en C++11)**.

Exemple classique avec deep copy (réécriture du `operator=` vue plus haut).

---

[...retorn en rèire](../menu.md)
