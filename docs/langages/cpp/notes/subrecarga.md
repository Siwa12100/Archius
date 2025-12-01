# 🎛️ Surcharge (Overloading) en C++

[...retorn en rèire](../menu.md)

---

La **surcharge** permet d’utiliser **le même nom** pour **plusieurs fonctions ou opérateurs**, tant que leurs **signatures diffèrent**.

🎯 Objectif :
Écrire du code **expressif**, **lisible** et **proche des maths** ou du langage naturel, sans dupliquer des noms (`afficherInt`, `afficherDouble`, etc.).

---

# 3.1 🎯 Surcharge de fonctions

## 🔧 Principe général

On parle de *surcharge* (overload) lorsqu’on définit **plusieurs fonctions** :

* avec le **même nom**,
* mais **des paramètres différents** (en nombre, en type, ou en const-ness).

> ⚠️ **Le type de retour ne participe PAS à la surcharge.**
> On ne peut pas distinguer deux fonctions uniquement par leur type de retour.

---

## 🟪 Surcharge par type d’arguments

```cpp
void afficher(int x) {
    std::cout << "int : " << x << '\n';
}

void afficher(double x) {
    std::cout << "double : " << x << '\n';
}

void afficher(const std::string& s) {
    std::cout << "string : " << s << '\n';
}

void demo() {
    afficher(10);           // appelle afficher(int)
    afficher(3.14);         // appelle afficher(double)
    afficher("Salut");      // const char[] → conversion en std::string → afficher(string)
}
```

🧠 Le compilateur choisit la **meilleure correspondance** (best match) selon le type réel, et applique si besoin des conversions **minimales**.

---

## 🟪 Surcharge par nombre d’arguments

```cpp
int addition(int a, int b) {
    return a + b;
}

int addition(int a, int b, int c) {
    return a + b + c;
}

void demo() {
    std::cout << addition(1, 2);      // 3 → version à 2 paramètres
    std::cout << addition(1, 2, 3);   // 6 → version à 3 paramètres
}
```

---

## ⚠️ Surcharge & `const` : méthodes const et non-const

Très important pour les classes.

```cpp
class Compte {
public:
    int get()       { 
        std::cout << "normal\n"; 
        return 1; 
    }

    int get() const { 
        std::cout << "const\n"; 
        return 2; 
    }
};

void demo() {
    Compte c;
    const Compte cc;

    c.get();   // appelle get() non const  → "normal"
    cc.get();  // appelle get() const      → "const"
}
```

💡 Ici, la surcharge repose sur la **qualification `const` de l’objet** (`this`).

* Pour un objet non `const`, la version non-const est préférée.
* Pour un objet `const`, seule la version `const` est autorisée.

📌 **On ne peut pas surcharger deux fonctions qui ne diffèrent que par le type de retour** :

```cpp
int f();
double f();  // ❌ interdit
```

Le compilateur ne pourrait pas savoir **laquelle choisir** quand tu écris juste `f();`.

---

# 3.2 🔀 Surcharge via pointeurs et références

Les pointeurs et références permettent également de définir des surcharges **plus fines**, notamment pour la gestion de const-ness.

---

## 📌 `T*` vs `const T*`

```cpp
void process(int* p) {
    std::cout << "pointeur normal\n";
}

void process(const int* p) {
    std::cout << "pointeur vers const\n";
}

void demo() {
    int x = 10;
    const int y = 20;

    process(&x); // → "pointeur normal"
    process(&y); // → "pointeur vers const"
}
```

🧠 Règle :

* `int*` → pointeur vers données modifiables
* `const int*` → pointeur vers données non-modifiables

Le compilateur choisit la version la **plus compatible** avec ce que tu lui passes.

---

## 📌 `T&` vs `const T&`

```cpp
void afficher(int& x) {
    std::cout << "ref non const\n";
}

void afficher(const int& x) {
    std::cout << "ref const\n";
}

void demo() {
    int a = 10;
    const int b = 20;

    afficher(a);  // → ref non const
    afficher(b);  // → ref const
}
```

🧠 Détails :

* Un `int&` ne peut référencer que des objets **modifiables**.
* Un `const int&` peut référencer :

  * un `int` modifiable,
  * un `const int`,
  * des temporaires (`afficher(3)` par ex),
  * etc.

C’est pourquoi on voit partout dans les API :

```cpp
void f(const std::string& s);
```

→ pas de copie, pas de modification, accepte tout (littéraux, temporaires, etc.).

---

# 3.3 ⚡ Surcharge des opérateurs

La surcharge d’opérateurs permet d’écrire du code naturel :

```cpp
Vector2D u, v;
Vector2D w = u + v;       // comme en maths
std::cout << w << "\n";   // affichage lisible
```

## 🧩 Principes généraux

* tous les opérateurs ne sont pas surchargeables (ex : `?:`, `.`),
* on garde autant que possible le **sens naturel** de l’opérateur,
* un opérateur peut être :

  * une **méthode membre**,
  * ou une **fonction libre** (souvent friend).

---

## 3.3.1 🔢 `operator[]` (indexation)

Permet d’écrire `obj[i]`.

```cpp
class Tableau {
private:
    int data[10];

public:
    int& operator[](std::size_t i) {
        return data[i];           // accès modifiable
    }

    const int& operator[](std::size_t i) const {
        return data[i];           // accès en lecture seule
    }
};
```

📌 Pourquoi deux versions ?

* pour un `Tableau t;` → `t[i]` retourne un `int&` modifiable,
* pour un `const Tableau t;` → `t[i]` retourne un `const int&` (lecture seule).

---

## 3.3.2 ➕ Opérateurs arithmétiques (+, -, *, /)

Souvent comme **fonctions libres** (parfois `friend`) :

```cpp
class Vector2D {
public:
    double x, y;

    Vector2D(double x, double y) : x{x}, y{y} {}
};

Vector2D operator+(const Vector2D& a, const Vector2D& b) {
    return Vector2D(a.x + b.x, a.y + b.y);
}

void demo() {
    Vector2D u{1.0, 2.0};
    Vector2D v{3.0, 4.0};
    Vector2D w = u + v;   // utilise operator+
}
```

💡 En fonction libre :

* tu peux aussi écrire des choses comme `u + v` où `u` est à gauche,
* tu peux plus facilement définir des opérateurs **commutatifs**, etc.

---

## 3.3.3 🔼 Pré-incrément (++p) vs post-incrément (p++)

### ✅ Pré-incrément : `++p`

* Modifie l’objet,
* Renvoie une **référence vers l’objet modifié**.

```cpp
class Entier {
private:
    int x;
public:
    Entier(int v) : x(v) {}

    Entier& operator++() {   // pré-incrément (++p)
        ++x;
        return *this;
    }
};
```

---

### ✅ Post-incrément : `p++`

* Modifie l’objet,
* Renvoie **l’ancienne valeur** (copie).

```cpp
class Entier {
private:
    int x;
public:
    Entier(int v) : x(v) {}

    Entier operator++(int) {  // post-incrément (p++)
        Entier temp = *this;  // copie de l’état actuel
        ++x;                  // on modifie l’objet
        return temp;          // on renvoie l’ancienne valeur
    }
};
```

💡 Le `int` dans la signature est un **paramètre factice** pour distinguer `++p` de `p++`.

---

## 3.3.4 📝 `operator<<` et `operator>>`

Très utilisés pour le **debug** et les **logs**.

```cpp
class Vector2D {
private:
    double x, y;

public:
    Vector2D(double x, double y) : x{x}, y{y} {}

    friend std::ostream& operator<<(std::ostream& os, const Vector2D& v);
};

std::ostream& operator<<(std::ostream& os, const Vector2D& v) {
    os << "(" << v.x << ", " << v.y << ")";
    return os;
}
```

Pour `>>` :

```cpp
friend std::istream& operator>>(std::istream& is, Vector2D& v) {
    return is >> v.x >> v.y;
}
```

---

# 3.4 🧠 Gestion de la Mémoire et Opérateurs spéciaux

Ici on se concentre sur **deux opérateurs cruciaux** lorsqu’une classe gère de la mémoire dynamique :

* `operator=` (affectation / assignation),
* `operator[]` (indexation).

## 3.4.1 🧱 Règle des 3 : destructeur, constructeur de copie, operator=

Si ta classe possède un **pointeur** vers de la mémoire dynamique (ex : `new[]`), ou une ressource à gérer (fichier, socket…), tu dois te soucier de :

1. **Destructeur**
2. **Constructeur de copie**
3. **Opérateur d’affectation `operator=`**

C’est la **règle des 3** (avant C++11).

### Exemple : classe `Vecteur` avec tableau dynamique

```cpp
class Vecteur {
private:
    std::size_t n;
    double* data;

public:
    // Constructeur
    explicit Vecteur(std::size_t n)
        : n{n}, data{new double[n]} {}

    // Destructeur
    ~Vecteur() {
        delete[] data;
    }

    // Constructeur de copie
    Vecteur(const Vecteur& other)
        : n{other.n}, data{new double[other.n]} {
        std::copy(other.data, other.data + n, data);
    }

    // Opérateur d'affectation (operator=)
    Vecteur& operator=(const Vecteur& other) {
        if (this != &other) {                // protection auto-affectation
            delete[] data;                   // 1. libérer anciennes données
            n = other.n;                     // 2. copier la taille
            data = new double[n];            // 3. allouer nouveau tableau
            std::copy(other.data, other.data + n, data);  // 4. copier le contenu
        }
        return *this;
    }
};
```

🧠 Pourquoi c’est indispensable ?

Sans ces trois fonctions :

* copie par défaut = **copie superficielle** (shallow copy) du pointeur,
* deux objets pointent vers la **même** zone mémoire,
* → **double `delete[]`** dans les destructeurs,
* → comportement indéfini, crash, corruption mémoire.

---

## 3.4.2 🪪 `operator[]` et gestion de la mémoire

On peut maintenant ajouter un **`operator[]`** à notre `Vecteur` :

```cpp
class Vecteur {
private:
    std::size_t n;
    double* data;

public:
    // ... constructeurs, destructeur, operator= comme ci-dessus ...

    std::size_t size() const { return n; }

    double& operator[](std::size_t i) {
        // (en vraie prod, on vérifierait les bornes)
        return data[i]; // accès modifiable
    }

    const double& operator[](std::size_t i) const {
        return data[i]; // accès lecture seule
    }
};
```

Utilisation :

```cpp
Vecteur v(3);
v[0] = 1.0;
v[1] = 2.0;
v[2] = 3.0;

const Vecteur cv = v;
std::cout << cv[1]; // OK, lecture seule
```

📌 Ce qu’il faut bien voir :

* `operator[]` **ne gère pas** la mémoire lui-même,
* il donne juste un **accès pratique** aux éléments du tableau interne,
* la gestion mémoire (allocation, copie, destruction) reste dans :

  * constructeur,
  * destructeur,
  * constructeur de copie,
  * `operator=`.

---

## 3.4.3 🔄 Récap Mémoire + Opérateurs

Pour une classe qui gère des **pointeurs** (ex : `Vecteur`) :

* ✅ **Destructeur** → libérer la mémoire
* ✅ **Constructeur de copie** → créer une vraie nouvelle copie (deep copy)
* ✅ **`operator=`** → remplacer proprement le contenu d’un objet par un autre

Ensuite, tu peux ajouter :

* ✅ **`operator[]`** pour accéder facilement aux éléments
* ✅ éventuellement d’autres opérateurs (`+`, `-`, `==`, etc.)

👉 Sans ça, tu te heurteras tôt ou tard à des **crashs aléatoires**, des **fuites mémoire** ou du **comportement indéfini**.

---

# 🧾 Récap global sur la surcharge

* La **surcharge** = même nom, signatures différentes (params / const / ref / pointer…).
* Le **type de retour ne compte pas** pour distinguer les surcharges.
* On surcharge :

  * des **fonctions** (par type / nombre d’arguments, const-ness),
  * des **méthodes** (const / non-const, ref-qualifiers…),
  * des **opérateurs** (`[]`, `=`, `+`, `++`, `<<`, `>>`, etc.).
* `operator=` et `operator[]` sont **critiques** pour les classes qui gèrent de la **mémoire dynamique**.

---

[...retorn en rèire](../menu.md)

