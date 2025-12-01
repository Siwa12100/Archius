# 🧬 Généricité (Templates)

[...retorn en rèire](../menu.md)

---

Les **templates** permettent d’écrire du code **paramétré par des types** :

* Une seule définition → utilisable avec **int, double, std::string, Point, etc.**
* Le tout est **instancié à la compilation** → pas de coût d’abstraction à l’exécution.

C’est le cœur de la **programmation générique** en C++ (comme `std::vector`, `std::sort`, `std::pair`, etc.).

---

# 5.1 🧰 Fonctions génériques (function templates)

Une **fonction template** ressemble à une fonction normale, mais avec un **paramètre de type**.

---

## 5.1.1 🎯 Déclaration et utilisation simple

```cpp
template<typename T>
T maximum(T a, T b) {
    return (a < b ? b : a);
}
```

Utilisation :

```cpp
int    ix = maximum(3, 7);                 // T = int
double dx = maximum(2.5, 1.7);             // T = double
std::string sx = maximum(std::string("ab"),
                         std::string("ac")); // T = std::string
```

🧠 Le compilateur **déduit automatiquement** `T` à partir des arguments.

---

## 5.1.2 🤖 Déduction automatique de type (et ses limites)

La déduction, c’est le compilateur qui infère `T` tout seul :

```cpp
auto r1 = maximum(10, 20);      // T = int
auto r2 = maximum(2.5, 3.7);    // T = double
```

Mais la déduction impose que **tous les paramètres** soient du **même** type `T`.

```cpp
maximum(3, 4.5);   // ❌ quelle valeur pour T ? int ? double ?
```

Pour corriger :

```cpp
maximum<double>(3, 4.5); // ✔️ on impose T = double
```

👉 *Règle :* si les arguments ne sont pas du même type, soit :

* tu forces `T` avec `maximum<double>(…)`,
* soit tu fournis une surcharge/template plus adaptée.

---

## 5.1.3 🚧 Contraintes : le type T doit supporter les opérations utilisées

Le template est accepté **uniquement** si, pour le type `T` choisi, les opérations utilisées dans le corps **existent**.

```cpp
template<typename T>
T addition(T a, T b) {
    return a + b;   // nécessite operator+ pour T
}
```

Fonctionne pour :

* `int`, `double`          → ✔️
* `std::string`            → ✔️ (`operator+` concatène)
* une classe `Point`       → ❌ si tu n’as pas défini `operator+`

En cas d’absence d’`operator+` :

```cpp
Point p1, p2;
addition(p1, p2); // ❌ erreur de compilation : pas d’operator+ pour Point
```

---

## 5.1.4 🎯 Spécialisation totale : cas particulier (ex : `Minimum<Point>`)

Parfois le **comportement générique** ne convient pas pour un type précis.
On peut alors écrire une **spécialisation totale**.

### 🧪 Exemple : `Minimum<T>` générique

```cpp
template<typename T>
const T& Minimum(const T& a, const T& b) {
    return (b < a ? b : a);   // utilise operator<
}
```

Pour des types “normaux” (int, double, string…), ça marche.
Pour une classe `Point`, on peut vouloir : *“le point le plus proche de l’origine”*.

---

### 🧱 Classe `Point` avec membres privés

```cpp
class Point {
private:
    double x;
    double y;

public:
    Point(double x, double y) : x{x}, y{y} {}

    double norm2() const { return x * x + y * y; }

    // on pourrait aussi définir operator< ici
};
```

On veut une **spécialisation** de `Minimum<Point>` :

```cpp
template<>
const Point& Minimum<Point>(const Point& a, const Point& b) {
    return (a.norm2() < b.norm2() ? a : b);
}
```

Ici, pas de problème : `norm2()` est public.

---

### ⚠️ Problème d’accessibilité + `friend`

Si on préfère comparer directement les coordonnées privées (`x`, `y`) *sans getter*,
on se heurte à un problème d’**encapsulation**.

Exemple (mauvais) :

```cpp
template<>
const Point& Minimum<Point>(const Point& a, const Point& b) {
    // ❌ accéder à a.x et a.y serait interdit si x/y sont private
}
```

Pour autoriser précisément ce code, on peut déclarer la spécialisation comme **amie** :

```cpp
class Point {
private:
    double x;
    double y;

    // 👇 Déclaration d’amitié : cette spécialisation peut accéder à x,y
    friend const Point& Minimum<Point>(const Point&, const Point&);

public:
    Point(double x, double y) : x{x}, y{y} {}
};
```

Maintenant, on peut écrire :

```cpp
template<>
const Point& Minimum<Point>(const Point& a, const Point& b) {
    // accès privé autorisé grâce à friend
    double na = a.x * a.x + a.y * a.y;
    double nb = b.x * b.x + b.y * b.y;
    return (nb < na ? b : a);
}
```

🧠 *Idée clé :*

> Les **templates** respectent l’encapsulation.
> Si une spécialisation a besoin des membres privés → il faut déclarer la fonction (ou template) comme `friend` dans la classe.

---

# 5.2 🧱 Classes génériques (class templates)

Les **classes templates** permettent de créer des **conteneurs et structures génériques**, comme `std::vector<T>`, `std::stack<T>`, `std::map<K,V>`, etc.

---

## 5.2.1 📦 Exemple : `Pile<T>` (stack générique) & forme canonique

On va coder une petite **pile générique** `Pile<T>` avec :

* stockage dynamique via un tableau `T*`,
* **forme canonique** : constructeur, destructeur, constructeur de copie, `operator=`,
* opérations : `push`, `pop`, `top`, `estVide`, `taille`,
* surcharge d’`operator<<` pour l’affichage (avec `friend`).

---

### 🧱 Déclaration de base

```cpp
template<typename T>
class Pile {
private:
    std::size_t capacite;
    std::size_t tailleCourante;
    T*          donnees;

public:
    // Constructeur
    explicit Pile(std::size_t cap = 10)
        : capacite{cap}, tailleCourante{0}, donnees{new T[cap]} {}

    // Destructeur
    ~Pile() {
        delete[] donnees;
    }

    // Constructeur de copie
    Pile(const Pile& autre)
        : capacite{autre.capacite},
          tailleCourante{autre.tailleCourante},
          donnees{new T[autre.capacite]} 
    {
        for (std::size_t i = 0; i < tailleCourante; ++i) {
            donnees[i] = autre.donnees[i];
        }
    }

    // Opérateur d'affectation (operator=)
    Pile& operator=(const Pile& autre) {
        if (this != &autre) {                // auto-affectation
            delete[] donnees;                // libérer anciennes données
            capacite       = autre.capacite;
            tailleCourante = autre.tailleCourante;
            donnees        = new T[capacite];
            for (std::size_t i = 0; i < tailleCourante; ++i) {
                donnees[i] = autre.donnees[i];
            }
        }
        return *this;
    }

    // Méthodes de pile
    bool estVide() const { return tailleCourante == 0; }
    std::size_t taille() const { return tailleCourante; }

    void push(const T& valeur) {
        if (tailleCourante == capacite) {
            // en vrai, on agrandirait la capacité (realloc dynamique)
            throw std::runtime_error("Pile pleine");
        }
        donnees[tailleCourante++] = valeur;
    }

    void pop() {
        if (estVide()) {
            throw std::runtime_error("Pile vide");
        }
        --tailleCourante;
    }

    T& top() {
        if (estVide()) {
            throw std::runtime_error("Pile vide");
        }
        return donnees[tailleCourante - 1];
    }

    const T& top() const {
        if (estVide()) {
            throw std::runtime_error("Pile vide");
        }
        return donnees[tailleCourante - 1];
    }

    // Déclaration friend pour l'affichage
    template<typename U>
    friend std::ostream& operator<<(std::ostream& os, const Pile<U>& p);
};
```

🔍 **Forme canonique** ici :

* constructeur,
* destructeur,
* constructeur de copie,
* `operator=` (deep copy obligatoire car on gère un `T*`),
* (en C++11 on ajouterait aussi le move constructor et move assignment → “règle des 5”).

---

### 🖨️ Surcharge d’`operator<<` avec `friend`

On veut pouvoir écrire :

```cpp
Pile<int> p;
p.push(1);
p.push(2);
std::cout << p << "\n";
```

On définit :

```cpp
template<typename T>
std::ostream& operator<<(std::ostream& os, const Pile<T>& p) {
    os << "[";
    for (std::size_t i = 0; i < p.tailleCourante; ++i) {
        os << p.donnees[i];
        if (i + 1 < p.tailleCourante) os << ", ";
    }
    os << "]";
    return os;
}
```

💡 Pourquoi `friend` dans la classe ?

* Parce que `donnees` et `tailleCourante` sont `private`.
* Sans `friend`, `operator<<` n’aurait pas accès à ces membres.
* On a donc ajouté dans la classe :

```cpp
template<typename U>
friend std::ostream& operator<<(std::ostream& os, const Pile<U>& p);
```

🧠 *Encore une fois :*

> Les templates respectent l’encapsulation → on doit explicitement déclarer les fonctions d’affichage/outil comme `friend` si elles ont besoin de l’intérieur.

---

## 5.2.2 📦 Classes génériques & formes canoniques

À retenir sur les classes templates comme `Pile<T>` :

* Elles sont **définies dans les headers** (sinon le compilateur ne peut pas les instancier).
* Elles suivent les **mêmes règles** que les classes normales :

  * destructeur,
  * constructeur de copie,
  * `operator=`,
  * éventuellement move constructor / move assignment.
* On peut surcharger :

  * `operator[]`, `operator==`, `operator!=`, etc.
  * `operator<<`/`>>` (en général via `friend` template).

---

## 5.2.3 🔧 Spécialiser une classe template (idée rapide)

Pour certains types, on peut écrire une **version différente** de la classe.

```cpp
template<typename T>
class Boite {
    // version générique
};

template<>
class Boite<bool> {
    // version spécialisée pour bool
};
```

Ou une **spécialisation partielle** :

```cpp
template<typename T>
class Wrapper { /* ... */ };

template<typename U>
class Wrapper<U*> { /* version spéciale pour les pointeurs */ };
```

➡️ Utilisé pour optimiser ou adapter le comportement (ex : `std::vector<bool>`).

---

# 5.3 🧾 Récap : Programmation Générique (Templates)

### 🔹 Fonctions génériques

* `template<typename T> T f(T a, T b);`
* Déduction automatique de `T`.
* Attention aux **ambiguïtés** (`f(3, 4.5)`).
* Les types doivent supporter les **opérations utilisées**.
* **Spécialisation totale** possible (`Minimum<Point>`).
* Problèmes d’accessibilité si on touche aux privés → **`friend`**.

### 🔹 Classes génériques

* `template<typename T> class Pile { … };`
* Utilisation de la **forme canonique** si on gère des ressources :

  * constructeur, destructeur,
  * constructeur de copie,
  * `operator=`,
  * (+ éventuellement move constructor / move assignment).
* Surcharge d’opérateurs (`operator<<`, `operator[]`, `operator==`, …).
* `friend` utile pour les fonctions d’affichage ou utilitaires qui ont besoin d’accéder aux membres privés.

---

[...retorn en rèire](../menu.md)
