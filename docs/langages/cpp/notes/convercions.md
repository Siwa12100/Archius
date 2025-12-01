# 🔄 Conversion de Types en C++

[...retorn en rèire](../menu.md)

---

La **conversion de type** (ou *type conversion*) désigne la manière dont le C++ transforme une valeur d’un type vers un autre :

* soit **automatiquement** → conversions **implicites**,
* soit **sous ton contrôle** → conversions **explicites** / *casts*.

Comprendre ça est crucial pour :

* éviter les surprises,
* bien utiliser `explicit`,
* maîtriser `static_cast`, `dynamic_cast`, etc.

---

# 5.1 🤖 Conversions implicites

Ce sont les conversions que le compilateur fait **tout seul**, sans que tu écrives de cast, parce que le **contexte l’exige**.

Exemples typiques :

* affectation : `double d = 3;`
* appel de fonction : `f(3.5)` vers `void f(int);`
* opérations arithmétiques : `2 * 3.5`

---

## 5.1.1 🔢 Promotions entières et flottantes

Ce sont des conversions de “petits” types vers des types plus larges, pour travailler dans un type **plus confortable**.

### 🔸 Promotions entières (*integer promotions*)

Typiquement :

* `char`, `signed char`, `unsigned char` → `int`
* `bool` → `int`
* `short` → `int`
* `enum` souvent → `int`

```cpp
char c = 'A';
int x = c;  // conversion implicite char → int (promotion)
```

Même chose dans les expressions :

```cpp
char a = 10;
char b = 20;
auto s = a + b; // a et b sont promus en int, s est un int
```

---

### 🔸 Promotions flottantes

* `float` → `double` (dans de nombreuses expressions arithmétiques)

```cpp
float f = 3.14f;
double d = f;  // conversion implicite float → double
```

---

## 5.1.2 🧮 Conversions arithmétiques usuelles (*usual arithmetic conversions*)

Elles se produisent dans les **opérations binaires** : `+`, `-`, `*`, `/`, `==`, `<`, etc.

Exemple :

```cpp
int    a = 2;
double b = 3.5;

auto c = a * b;  // a est converti en double
```

Règle générale (simplifiée) :

1. On applique d’abord les **promotions entières**.
2. Si les types sont différents, on passe au “plus grand domaine” :

   * `long double` > `double` > `float` > `long long` > `long` > `int` > …
3. L’expression est évaluée dans ce type commun.

---

## 5.1.3 🏗️ Conversions implicites via constructeurs **non-explicites**

⚠️ Point **hyper important** pour la POO.

Un constructeur **monoparamètre non marqué `explicit`** peut être utilisé pour des **conversions implicites**.

```cpp
class Entier {
    int x;
public:
    Entier(int v) : x{v} {}   // non-explicit → conversion implicite autorisée
};

void afficher(const Entier& e);

void demo() {
    afficher(5);   // ✅ 5 est converti implicitement en Entier(5)
    Entier e = 10; // ✅ conversion implicite 10 → Entier(10)
}
```

C’est pratique… mais parfois **dangereux**, car le compilateur peut choisir ces conversions dans des contexts inattendus.

---

### 🚨 Exemple de conversions non désirées

```cpp
class Ratio {
public:
    Ratio(int num, int den); // OK
    Ratio(double d);         // OUPS : conversion implicite depuis double !
};

void f(Ratio r);

void demo() {
    f(3.14);       // conversion implicite 3.14 → Ratio(3.14)
    Ratio r = 2;   // conversion implicite 2 → Ratio(2)
}
```

Ça peut :

* appeler la mauvaise surcharge,
* rendre le code ambigu,
* introduire des conversions silencieuses.

---

### 🛡️ Solution moderne : `explicit`

On marque les constructeurs monoparamètres avec `explicit` :

```cpp
class Entier {
    int x;
public:
    explicit Entier(int v) : x{v} {}  // plus de conversion implicite
};

void demo() {
    Entier e1(10);           // ✅ OK (initialisation directe)
    Entier e2 = Entier(10);  // ✅ OK
    // Entier e3 = 10;       // ❌ interdit (conversion implicite bloquée)
}
```

📌 `explicit` :

* empêche les conversions **implicites**,
* mais autorise toujours :

  * l’appel direct du constructeur (`Entier(10)`),
  * `static_cast<Entier>(10)`,
  * la liste d’initialisation `{}`.

👉 **Bonne pratique moderne** :

> Mettre `explicit` sur tous les constructeurs mono-argument,
> sauf cas volontaire de “type wrapper” très simple.

---

# 5.2 🎯 Conversions explicites

Ici, **tu décides consciemment** de convertir une valeur : c’est toi qui écris le cast.

---

## 5.2.1 🟦 `static_cast`

Le cast **standard** en C++ moderne.

### 🔹 Caractéristiques

* vérifié **à la compilation**,
* ne fait que des conversions **“raisonnables”** :

  * entre types numériques (int, double, etc.),
  * entre types de classes liées par héritage (avec prudence),
  * vers/en `void*` dans certains cas,
* ne supprime pas `const` → pour ça, il faut `const_cast`.

### Exemples arithmétiques

```cpp
double d = 3.14;
int i = static_cast<int>(d);  // tronque → 3
```

---

### Exemples avec héritage

```cpp
class Base { /* ... */ };
class Derived : public Base { /* ... */ };

Derived d;
Base* b = &d; // upcast implicite, pas besoin de cast

// downcast (sans vérification runtime) :
Derived* dd = static_cast<Derived*>(b);
```

⚠️ Si `b` ne pointe pas vraiment vers un `Derived`, on aura un comportement indéfini.
Pour un downcast **sécurisé**, on utilise `dynamic_cast` (section 6.1).

---

## 5.2.2 🧱 Conversion via constructeur

Simplement :

```cpp
class Entier {
    int x;
public:
    explicit Entier(int v) : x{v} {}
};

Entier e1(42);             // appel direct
Entier e2 = Entier(42);    // forme fonctionnelle
auto  e3 = Entier{42};     // C++11, uniform init
```

Et avec `static_cast` :

```cpp
Entier e4 = static_cast<Entier>(42);
```

C’est **équivalent** à un appel de constructeur, mais `static_cast` met bien en évidence que tu fais une conversion volontaire.

---

# 5.3 🏷️ Opérateurs de conversion (`operator type()`)

Une classe peut se définir comme **convertible** vers un autre type en fournissant un **opérateur de conversion**.

```cpp
class Entier {
    int x;
public:
    Entier(int v) : x{v} {}

    operator int() const {   // opérateur de conversion implicite
        return x;
    }
};

void demo() {
    Entier e{10};
    int x = e;          // conversion implicite Entier → int
    double d = e;       // Entier → int → double
}
```

---

## ⚠️ Pourquoi c’est dangereux ?

### 1️⃣ Ambiguïtés et conversions involontaires

```cpp
class Entier {
    int x;
public:
    Entier(int v) : x{v} {}
    operator int() const { return x; }
};

void f(double);
void g(int);

void demo() {
    Entier e{10};
    f(e);   // Entier → int → double, OK mais implicite
    g(e);   // Entier → int
}
```

Trop de conversions implicites peuvent :

* déclencher la **mauvaise surcharge**,
* faire des conversions en chaîne que tu ne vois pas.

---

### 2️⃣ Recommandation moderne : `explicit operator type()`

On préfère :

```cpp
class Entier {
    int x;
public:
    Entier(int v) : x{v} {}
    explicit operator int() const { return x; }
};

void demo() {
    Entier e{10};

    // int x = e;                   // ❌ interdit (implicite)
    int y = static_cast<int>(e);    // ✅ OK, conversion explicite voulue
}
```

Très courant avec `operator bool` :

```cpp
class Handle {
    int fd;
public:
    explicit operator bool() const {
        return fd >= 0;
    }
};

Handle h;
if (h) { /* OK, conversion bool dans un contexte booléen */ }
```

---

# 6. 🌀 Transtypage (Casting) en C++

Le C++ offre plusieurs opérateurs de cast, chacun avec une **intention précise** :

* `static_cast`
* `dynamic_cast`
* `reinterpret_cast`
* `const_cast`

(Et le vieux cast en C `(type)expr`, à éviter.)

---

# 6.1 🧭 `dynamic_cast` : cast **dynamique** (polymorphe)

Utilisé pour les **downcasts sûrs** dans une hiérarchie de classes **polymorphes** (avec au moins une méthode virtuelle).

```cpp
class Base {
public:
    virtual ~Base() = default; // polymorphe
};

class Derived : public Base {
    // ...
};
```

---

## 6.1.1 📉 Downcast Base* → Derived* avec vérification

```cpp
Base* b = obtenirUnObjetBase();

if (auto* d = dynamic_cast<Derived*>(b)) {
    // 🟢 b pointe réellement vers un Derived
    d->fonctionSpecifique();
} else {
    // 🔴 b ne pointe PAS vers un Derived
}
```

💡 Si le cast échoue :

* pour les **pointeurs** → `nullptr`
* pour les **références** → exception `std::bad_cast`

---

## 6.1.2 📈 Upcast Derived* → Base* : implicite

```cpp
Derived* d = new Derived();
Base* b = d;     // upcast implicite, toujours sûr
```

Pas besoin de cast, ni `static_cast`, ni `dynamic_cast`.

---

## 6.1.3 🧩 Conditions pour utiliser `dynamic_cast`

* La base doit être **polymorphe** (au moins une méthode virtuelle) :

  ```cpp
  class Base { public: virtual ~Base() {} };
  ```

* Sinon, un `dynamic_cast` entre types de même hiérarchie → **erreur de compilation**.

---

# 6.2 🟦 `static_cast` (récap rapide côté POO)

On l’a déjà vu pour les conversions “normales”.

Pour l’héritage :

```cpp
Base* b = /* ... */;
Derived* d = static_cast<Derived*>(b); // ⚠️ aucun check runtime
```

❗ À utiliser uniquement si tu **es certain** que `b` pointe bien vers un `Derived`.
Sinon → comportement indéfini → bug potentiellement très méchant.

---

# 6.3 🧨 `reinterpret_cast` : conversion “brute”

C’est le cast **le plus dangereux** :

* il réinterprète les **bits** d’un type comme s’ils appartenaient à un autre type,
* typiquement pour des opérations très bas niveau.

Exemples :

```cpp
std::uintptr_t addr = reinterpret_cast<std::uintptr_t>(pointeur);
void* p = reinterpret_cast<void*>(addr);
```

Ou pire (à éviter) :

```cpp
int x = 0x12345678;
double* d = reinterpret_cast<double*>(&x); // ⚠️ UB si on déréférence
```

💣 En résumé :

* à **éviter** en code “normal”,
* réservé aux cas très bas niveau (drivers, sérialisation brute, interfaçage C / hardware…),
* peut violer facilement les règles d’aliasing, d’alignement, etc.

---

# 6.4 🧼 `const_cast` : modifier la const-ness

Permet de **retirer** ou **ajouter** (dans certains cas) un `const` / `volatile`.

Exemple classique :

```cpp
void f(const int* p) {
    int* q = const_cast<int*>(p);  // enlève le const
    // ⚠️ si *p était réellement const, modifier *q → UB
}
```

👉 Autorisé, mais dangereux si :

* la valeur d’origine était vraiment `const`,
* on modifie ensuite via le pointeur non-const.

Cas d’usage typiques (et prudents) :

* API C incorrectement typée (qui a oublié le `const`),
* réutiliser une même fonction interne prenant un pointeur non-const pour implémenter à la fois la version const et non-const (`begin()` / `begin() const` par ex).

---

# 6.5 🚫 Le cast en C `(type)expression`

Le vieux style C :

```cpp
double d = 3.14;
int i = (int)d;
```

En C++, ce cast :

* essaye plusieurs choses (`const_cast`, `static_cast`, `reinterpret_cast`…) dans un ordre complexe,
* est **moins lisible** que les casts C++ (`static_cast<>`, `reinterpret_cast<>`…),
* est donc **déconseillé**.

✅ Préférer toujours les casts C++ explicites, qui documentent **l’intention**.

---

# 6.6 🔁 Promotions de types (récap)

Pour boucler avec les conversions implicites :

* `short`, `char`, `bool` → **promus en `int`** dans les expressions,
* `float` → **promu en `double`** dans certains contextes,
* dans une expression mixte (`int` et `double`), on passe au type le plus large (`double`).

Exemple :

```cpp
short s = 5;
auto r = s + 10;  // s → int, r est un int

float f = 2.5f;
double x = 3;
auto z = f * x;   // f → double, calcul en double
```

---

# 🧾 Récap global : Conversions & Transtypages

* **Conversions implicites** :

  * promotions (`short → int`, `float → double`, …),
  * conversions arithmétiques usuelles,
  * constructeurs **non-`explicit`**,
  * opérateurs de conversion non `explicit`.

* **Conversions explicites** :

  * `static_cast` pour les conversions “logiques”,
  * appel explicite de constructeurs,
  * opérateurs de conversion `explicit`.

* **Transtypages C++** :

  * `static_cast` → conversion “normale”, vérifiée à la compilation,
  * `dynamic_cast` → downcast sécurisé en polymorphisme,
  * `reinterpret_cast` → réinterprétation brute des bits (à éviter),
  * `const_cast` → enlever/ajouter `const` (avec prudence).

* **Bonnes pratiques** :

  * `explicit` sur les constructeurs monovalents,
  * `explicit operator type()` autant que possible,
  * éviter le cast C `(type)expr`,
  * limiter `reinterpret_cast` et `const_cast` à des cas très ciblés.

---

[...retorn en rèire](../menu.md)
