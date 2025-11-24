# 🔄 Conversion de Types en C++

[...retorn en rèire](../menu.md)

---

La conversion de type (ou *type conversion*) correspond à la manière dont le C++ change un type en un autre, soit automatiquement (implicite), soit sous ton contrôle (explicite).

---

# 5.1 🤖 Conversions implicites

Ce sont les conversions que le compilateur effectue **sans que tu le demandes**, lorsque le contexte l’exige.

---

## 5.1.1 🔢 Promotions entières et flottantes

### 🔸 Promotions entières (*integer promotions*)

Exemples classiques :

* `char`, `unsigned char`, `signed char` → **promus en int**
* `bool` → int
* `short` → int

```cpp
char c = 'A';
int x = c;  // conversion implicite char → int
```

---

### 🔸 Promotions flottantes

* `float` → **double** (en contexte arithmétique)

```cpp
float f = 3.14f;
double d = f;  // conversion implicite
```

---

## 5.1.2 🧮 Conversions arithmétiques usuelles (usual arithmetic conversions)

Elles se produisent dans les **opérations binaires** : `+ - * / == < …`

Exemple :

```cpp
int    a = 2;
double b = 3.5;
auto c = a * b;  // a est converti en double
```

Ordre général :

1. Les types entiers sont promus.
2. Le plus “grand” type commande (double > float > int > char).
3. L’opération se fait dans ce type.

---

## 5.1.3 🏗️ Conversions via constructeurs **non-explicites**

⚠️ Très important !

Un constructeur **sans le mot-clé `explicit`** autorise des conversions implicites.

```cpp
class Entier {
    int x;
public:
    Entier(int v) : x(v) {}   // non-explicit → conversion implicite autorisée
};

void afficher(const Entier& e);

afficher(5);  // 🟢 OK ! 5 est converti automatiquement en Entier(5)
```

Cela peut créer des surprises :

```cpp
Entier e = 10; // conversion implicite : Entier(10)
```

### 🚨 Problème : conversions inattendues

Si tu fais :

```cpp
class Ratio {
public:
    Ratio(int num, int den);  // pas de constructor unitaire → OK
    Ratio(double d);          // OUPS : conversion implicite depuis double
};
```

Alors :

```cpp
Ratio r = 3.14; // conversion automatique via Ratio(double)
```

➡️ Pour éviter les conversions non voulues :
➜ **toujours mettre `explicit` aux constructeurs monovalents**.

---

# 5.2 🎯 Conversions explicites

Ce sont les conversions où tu demandes *explicitement* un changement de type.

---

## 5.2.1 🟦 `static_cast`

Le cast le plus utilisé en C++ moderne.

### 🔹 Caractéristiques :

* Vérifié **à la compilation** (pas à l’exécution)
* Autorise :

  * conversions arithmétiques
  * conversions entre classes liées (base <-> dérivée mais **avec prudence**)
  * conversions explicites entre types compatibles

### Exemple simple :

```cpp
double d = 3.14;
int i = static_cast<int>(d);  // d → i (3)
```

---

## 5.2.2 🧱 Appel explicite du constructeur

Tu peux convertir en appelant simplement un constructeur :

```cpp
Entier e = Entier(42);
Entier f(3.14);         // si constructeur double → Entier(double)
```

Forme fonctionnelle :

```cpp
Entier x = Entier{10};  // C++11
```

---

## 🤜 `static_cast` vs constructeur ?

```cpp
Entier e = Entier(42);          // construct conversion
Entier f = static_cast<Entier>(42); // identique, mais plus explicite
```

* Le constructeur est plus “naturel”
* `static_cast<>()` est préféré en contexte **technique ou ambigu** (héritage, conversions multiples, template).

---

# 5.3 🏷️ Opérateur de conversion (`operator type()`)

Une classe peut se convertir **elle-même** vers un autre type.

```cpp
class Entier {
    int x;
public:
    Entier(int v) : x(v) {}
    operator int() const { return x; }  // opérateur de conversion
};
```

Usage :

```cpp
Entier e(10);
int x = e;  // conversion automatique via operator int()
```

---

## ⚠️ Pourquoi utiliser cet opérateur avec prudence ?

### ❌ Problème 1 : ambiguïtés

```cpp
Entier a = 5;
Entier b = 6;

if (a < b) { ... }  // compare int ? compare Entier ? conversion implicite ?
```

👉 Trop de conversions implicites cassent la lisibilité et introduisent des bugs subtils.

---

### ❌ Problème 2 : conversions involontaires

```cpp
void f(double);

f(Entier(10));  // → int → double
```

Tu appelles parfois la mauvaise surcharge sans t’en rendre compte.

---

### 💡 Recommandation moderne :

* **Toujours écrire `explicit operator type()`**, sauf cas très particulier.

```cpp
explicit operator bool() const;
```

Ainsi, seules les conversions **dans les contextes booléens** fonctionnent (if, while…).

---

# 6. 🌀 Transtypage (Casting) en C++

Le C++ possède plusieurs types de cast.
Ici, on aborde le plus important pour la POO polymorphe : **dynamic_cast**.

---

# 6.1 🧭 `dynamic_cast` : transtypage dynamique

Il sert uniquement dans un contexte **polymorphe** (classes avec au moins une méthode virtuelle).

```cpp
class Base { public: virtual ~Base(){} };
class Derived : public Base { /* ... */ };
```

---

## 🎯 Objectif : vérifier si un pointeur/référence vers Base pointe réellement un objet Derived

Deux directions :

---

## 6.1.1 📉 Downcast (Base* → Derived*) avec contrôle

```cpp
Base* b = obtenirUnObjet();

Derived* d = dynamic_cast<Derived*>(b);

if (d != nullptr) {
    // 🟢 b était bien un Derived*
} else {
    // 🔴 b ne pointe pas vers un Derived
}
```

💡 Le cast **est vérifié à l’exécution**, donc **sécurisé**.

---

## 6.1.2 📈 Upcast (Derived* → Base*) : implicite

```cpp
Derived* d = new Derived();
Base* b = d;        // upcast implicite
```

* Toujours sûr
* Pas besoin de `dynamic_cast`

---

## 6.1.3 ❌ Échec d’un `dynamic_cast`

### Pour les pointeurs :

```cpp
Derived* d = dynamic_cast<Derived*>(uneBase);
if (d == nullptr) {
    // 🔴 transformation impossible
}
```

### Pour les références :

```cpp
try {
    Derived& d = dynamic_cast<Derived&>(*uneBase);
}
catch(const std::bad_cast& e) {
    // 🔴 cast impossible → exception
}
```

🎯 **Pointeur échoue → nullptr**
🎯 **Référence échoue → exception (`std::bad_cast`)**

---

## 💡 Conditions nécessaires

* La classe de base doit être **polymorphe** :

  ```cpp
  class Base { public: virtual ~Base() {} };
  ```
* Sinon :
  `dynamic_cast` entre types liés **échoue à la compilation**.

---

# 6.2 🚨 Quand utiliser dynamic_cast ?

### ✔️ À utiliser quand :

* tu manipules des pointeurs/références vers base
* et tu veux savoir *le type réel derrière*

Ex :

```cpp
std::vector<Base*> objets;

for (Base* b : objets) {
    if (auto* d = dynamic_cast<Derived*>(b)) {
        d->fonctionSpecifique();
    }
}
```

---

### ❌ À ne pas utiliser quand :

* tu peux utiliser des fonctions virtuelles à la place
* tu forces la logique “au cas par cas” dans un switch du type dynamique
* tu peux utiliser un design pattern (visiteur, polymorphisme, stratégie…)

---

[...retorn en rèire](../menu.md)