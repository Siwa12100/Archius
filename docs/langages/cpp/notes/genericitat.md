# 🧬 Généricité (Templates)

[...retorn en rèire](../menu.md)

---

Les **templates** permettent d’écrire du code **paramétré par des types**, réutilisable et optimisé **à la compilation** (pas de coût d’abstraction).
C’est l’un des mécanismes les plus puissants du C++.

---

# 7.1 🧰 Fonctions génériques (function templates)

Une fonction générique fonctionne avec n’importe quel type **tant que les opérations utilisées existent**.

---

## 7.1.1 🎯 Déclaration classique

```cpp
template<typename T>
T maximum(T a, T b) {
    return (a < b ? b : a);
}
```

Appel :

```cpp
int x = maximum(3, 7);           // T = int
double y = maximum(2.5, 1.7);    // T = double
std::string s = maximum("ab"s, "ac"s); // T = string
```

➡️ Le compilateur **déduit automatiquement** le type `T` lors de l’appel.

---

## 7.1.2 🤖 Déduction automatique du type

C’est l’un des gros atouts :

```cpp
auto r = maximum(10, 20); // T = int
auto s = maximum(2.5, 3.7); // T = double
```

### ⚠️ Mais la déduction peut échouer

Exemple :

```cpp
maximum(3, 4.5);  // ❌ erreur : impossible de déduire UN type T commun
```

Pour corriger :

```cpp
maximum<double>(3, 4.5);  // ✔️ T = double imposé
```

➡️ **Les templates ne réalisent pas de promotion implicite** comme les fonctions normales.

---

## 7.1.3 ⚠️ Ambiguïtés nécessitant explicitations (ex : Minimum<T>)

Exemple classique :

```cpp
template<typename T>
T minimum(T a, T b);

minimum(3, 4.5);        // ❌ T ? int ? double ?
minimum<double>(3, 4.5); // ✔️
```

Ou encore :

```cpp
template<typename T>
void f(T a, T b);

f(1, 'c');   // ❌ T incompatible
```

🧠 Règle : *Tous les paramètres doivent avoir le même type T, sauf si tu précises explicitement T.*

---

## 7.1.4 🚧 Limites : nécessitent l’existence d’opérateurs

Le template fonctionne **uniquement si le type T possède les opérations utilisées**.

Exemple :

```cpp
template<typename T>
T addition(T a, T b) { return a + b; }
```

Fonctionne avec :

* int, double → ✔️
* std::string → ✔️ (`operator+`)
* ta propre classe → seulement si **tu fournis `operator+`**

Sinon :

```cpp
addition(objetSansPlus, objetSansPlus);  
// ❌ erreur : operator+ n’existe pas pour ce type
```

---

# 7.2 🧩 Spécialisations : résoudre les cas particuliers

Les templates permettent de faire des **exceptions** pour certains types précis.

---

## 7.2.1 🎯 Spécialisation totale

```cpp
template<typename T>
T abs_generique(T x) { return (x < 0 ? -x : x); }

template<>
const char* abs_generique(const char* x) {
    return x;  // définition spéciale pour const char*
}
```

Appels :

```cpp
abs_generique(-3);          // version générique
abs_generique("Coucou");    // version spécialisée
```

💡 Idéal pour adapter le comportement à certains types particuliers.

---

## 7.2.2 🔧 Spécialisation partielle (pour les classes)

Impossible avec **les fonctions**, mais possible pour **les classes** :

```cpp
template<typename T>
class Boite { /* ... */ };

template<typename U>
class Boite<U*> { /* version spécialisée pour pointeurs */ };
```

Cible :

* tous les `T*`
* mais pas les autres types

---

## 7.2.3 📌 Pourquoi spécialiser ?

* Optimiser pour un type spécifique (ex : `vector<bool>`)
* Changer le comportement (ex : représentation spéciale des chaînes)
* Fournir des opérations seulement disponibles pour un sous-type

---

# 7.3 🧱 Classes génériques (class templates)

Les classes peuvent aussi être paramétrées par des types.

---

## 7.3.1 🧱 Syntaxe classique

```cpp
template<typename T>
class Boite {
private:
    T valeur;

public:
    Boite(T v) : valeur(v) {}

    T get() const { return valeur; }
};
```

Usage :

```cpp
Boite<int> b1(10);
Boite<std::string> b2("Salut");
```

---

## 7.3.2 🧩 Membres utilisant des structures internes (typedef / using)

Dans une classe template, tu peux définir :

```cpp
template<typename T>
class Tableau {
public:
    using value_type = T;

private:
    T data[10];
};
```

Ou encore des structures internes :

```cpp
template<typename T>
class Boite {
public:
    struct Info {
        T element;
        int id;
    };
};
```

---

## 7.3.3 🔢 Templates à plusieurs paramètres

```cpp
template<typename T, typename U>
class Paire {
public:
    T first;
    U second;

    Paire(T f, U s) : first(f), second(s) {}
};
```

Appel :

```cpp
Paire<int, double> p(3, 2.5);
```

Tu peux mélanger autant de types que tu veux :

```cpp
template<typename T, typename U, typename V>
class Triple { /* ... */ };
```

---

## 7.3.4 🧠 Définition en dehors de la classe (important !)

```cpp
template<typename T>
class Boite {
public:
    T get() const;
};

template<typename T>
T Boite<T>::get() const {
    return T{42};    // exemple
}
```

💡 Remarque cruciale :
Les templates doivent être **entièrement définis dans le header**, car le compilateur doit les voir lors de l’instanciation.

---

# 7.4 📌 Résumé général

| Concept               | Idée clé                                           |
| --------------------- | -------------------------------------------------- |
| Déduction automatique | Le compilateur choisit T selon les arguments       |
| Ambiguïtés            | Parfois T doit être précisé manuellement           |
| Limites               | Le type T doit supporter les opérations utilisées  |
| Spécialisation        | Permet de gérer des cas particuliers               |
| Classes génériques    | Utilisées pour containers, utilitaires, structures |
| Multi-templates       | Plus d’un paramètre : classique pour maps, pairs…  |

---

# 7.5 🎁 Exemple complet regroupant tout

```cpp
template<typename T>
class Vec2 {
public:
    T x, y;

    Vec2(T x, T y) : x(x), y(y) {}

    T norm2() const { return x*x + y*y; }
};

// Spécialisation totale pour bool
template<>
class Vec2<bool> {
public:
    bool x, y;
    Vec2(bool x, bool y) : x(x), y(y) {}

    int norm2() const { return x + y; } // adaptation spécifique
};
```

---

[...retorn en rèire](../menu.md)