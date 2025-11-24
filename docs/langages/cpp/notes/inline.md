# Fonctions et Méthodes `inline` en C++

[...retorn en rèire](../menu.md)

---

## 1. Ce que signifie vraiment `inline` en C++

En C++ moderne, le mot-clé `inline` a **deux aspects** à distinguer :

1. **Aspect langage (ODR / multiples définitions)**
   `inline` autorise une **même fonction** (ou méthode) à être **définie dans plusieurs unités de compilation** (typiquement parce qu’elle est dans un `.hpp` inclus partout), tout en respectant la *One Definition Rule* (ODR).
   Le linker fusionne ces définitions en **une seule entité**.

2. **Aspect optimisation (inlining au sens machine)**
   Historiquement, `inline` servait à suggérer au compilateur de **remplacer l’appel par le corps de la fonction** pour éviter l’overhead d’un appel de fonction.
   👉 En pratique aujourd’hui : **le compilateur est libre d’ignorer cette suggestion**. Il peut inline une fonction non marquée `inline`, et refuser d’inliner une fonction marquée `inline`.

Donc :

> `inline` en C++ sert surtout à **gérer les définitions dans les headers**, pas à garantir l’inlining au sens performance.

---

## 2. Déclaration et définition de fonctions / méthodes `inline`

### 2.1. Fonction libre `inline`

```cpp
// max_utils.hpp
#pragma once

inline int max_int(int a, int b) {
    return (a > b) ? a : b;
}
```

* La fonction est **définie dans un header**.
* Le mot-clé `inline` permet d’avoir cette **définition recopiée** dans plusieurs `.cpp` sans erreur de linker (ODR).

### 2.2. Méthodes `inline` dans une classe

Toute **méthode définie directement dans le corps de la classe** est **implicitement `inline`** :

```cpp
class Vector2D {
public:
    double x;
    double y;

    // Méthode implicitement inline
    double norm2() const {
        return x * x + y * y;
    }
};
```

On peut aussi l’écrire explicitement :

```cpp
class Vector2D {
public:
    double x;
    double y;

    inline double norm2() const {
        return x * x + y * y;
    }
};
```

Mais le `inline` est redondant : définir la méthode **dans la classe** suffit pour qu’elle soit considérée inline.

---

## 3. `inline` vs `#define` : différences fondamentales

Les macros `#define` sont gérées par le **préprocesseur**, avant la compilation.
Les fonctions `inline` sont gérées par le **compilateur**, avec types, portée, etc.

### 3.1. Exemple de macro vs fonction `inline`

Macro :

```cpp
#define SQR(x) ((x) * (x))

int a = SQR(1 + 2);  // se transforme en ((1 + 2) * (1 + 2)) => OK, 9
int b = SQR(i++);    // se transforme en ((i++) * (i++)) => i est incrémenté 2 fois !
```

Fonction `inline` :

```cpp
inline int sqr(int x) {
    return x * x;
}

int a = sqr(1 + 2);  // OK, 9
int b = sqr(i++);    // i est incrémenté une seule fois
```

### 3.2. Principales différences

| Aspect               | `#define` macro                           | Fonction `inline`                          |
| -------------------- | ----------------------------------------- | ------------------------------------------ |
| Lieu de traitement   | Préprocesseur (simple substitution texte) | Compilateur (fonction normale)             |
| Vérification de type | Aucune                                    | Complète (types, conversions, overload…)   |
| Portée               | Globale après la définition               | Respecte les règles de portée C++          |
| Débogage             | Galère (code expansé, pas de symbole)     | Facile (point d’arrêt dans la fonction)    |
| Effets de bord       | Très dangereux (paramètres réévalués)     | Contrôlés (arguments évalués 1 seule fois) |
| Surcharge            | Impossible                                | Possible (overload, templates)             |
| Namespaces           | Impossible                                | Fonction dans un namespace                 |

Pour du “code de fonction”, une `inline` est **quasi toujours préférable** à une macro.

---

## 4. Mise en œuvre pratique des fonctions `inline`

### 4.1. Où mettre les fonctions `inline` ?

En pratique :

* On met la **déclaration + définition** de la fonction `inline` dans un **header** (`.hpp`, `.hxx`, `.inl`, …).
* Ce header est inclus partout où on en a besoin.

```cpp
// math_utils.hpp
#pragma once

inline double clamp(double value, double minVal, double maxVal) {
    if (value < minVal) return minVal;
    if (value > maxVal) return maxVal;
    return value;
}
```

Chaque `.cpp` incluant ce header verra la définition, mais grâce à `inline`, le linker acceptera les **multiples définitions identiques**.

### 4.2. Attention à l’ODR (One Definition Rule)

Pour une fonction `inline`, il faut :

* Que **toutes les définitions soient identiques** (même signature, même corps).
* Qu’il n’y ait pas une autre définition non-inline contradictoire ailleurs.

Sinon → **UB** (comportement indéfini) ou erreurs de link.

### 4.3. `inline` et templates

Pour les **fonctions templates**, on met aussi généralement la **définition dans le header**, mais ce n’est pas le `inline` qui gère le problème, c’est le mécanisme de **génération de code template**.

Exemple :

```cpp
template<typename T>
T add(const T& a, const T& b) {
    return a + b;
}
```

On ne met pas forcément `inline`, mais on la met **dans le header** car le compilateur doit voir la définition au moment d’instancier le template.

---

## 5. Avantages / inconvénients des fonctions `inline`

### 5.1. Avantages

1. **Gestion propre dans les headers**
   On peut définir des petites fonctions utilitaires directement dans les headers sans violer l’ODR.

2. **Potentiel gain de performance**
   Si le compilateur décide de les inliner au sens machine :

   * suppression du coût d’appel de fonction,
   * potentielle meilleure optimisation (propagation de constantes, élimination de code mort, etc.).

3. **Lisibilité & maintenabilité**

   * Code typé, lisible, debuggable (stack trace, breakpoints).
   * Possible de surcharger / templatiser proprement.

4. **Pas les problèmes des macros**

   * Pas de réévaluation multiple des paramètres,
   * Pas de substitutions surprises,
   * Pas d’étrangetés avec les priorités d’opérateurs.

### 5.2. Inconvénients

1. **Risque de bloat du code (code size)**
   Nombreux appels inlinés → répétition du corps → binaire plus gros.
   Peut dégrader les performances (cache d’instruction).

2. **Pas de garantie de performance**

   * `inline` ne garantit pas que le compilateur fera un inlining effectif.
   * Parfois, un appel normal est plus optimal (meilleure localité, moins de pollution du cache).

3. **Couplage via headers**

   * Changer une fonction `inline` dans un header implique de **recompiler toutes les unités** qui l’utilisent (effet classique des headers, mais amplifié si on en abuse).

---

## 6. Avantages / inconvénients des macros `#define` par rapport à `inline`

Pour les *fonctions*, les macros ont très peu d’avantages aujourd’hui, mais pour être complet :

### 6.1. Quand une macro peut encore se justifier

* **Constantes simples** (même là, on préfère `constexpr` ou `const` en C++ moderne) :

  ```cpp
  #define PI 3.141592653589793
  ```

  👉 On préférera :

  ```cpp
  inline constexpr double PI = 3.141592653589793;
  ```

* **Code conditionnel de compilation** :

  ```cpp
  #ifdef DEBUG
    // code de debug
  #endif
  ```

* **Cas très spécifiques de métaprogrammation préprocesseur** (génération automatique de code, etc.) → rare, très avancé, et souvent remplacé par d’autres techniques plus modernes.

### 6.2. Inconvénients majeurs des macros-fonctions

* Pas de type → erreurs silencieuses.
* Pas de scope → pollution globale.
* Effets de bord dangereux.
* Difficiles à déboguer (pas de symbole de fonction, code expansé).
* Ne respectent pas les namespaces, ni la surcharge, ni les templates.

---

[...retorn en rèire](../menu.md)