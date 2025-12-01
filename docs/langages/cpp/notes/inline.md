# Fonctions et Méthodes `inline` en C++

[...retorn en rèire](../menu.md)

---

# 🌟 1. Ce que signifie vraiment `inline` en C++

Le mot-clé `inline` a **deux rôles bien distincts** en C++ :
👉 **un rôle lié au *langage*** (One Definition Rule)
👉 **un rôle lié à l’optimisation** (suggestion d’inlining machine)

---

## 🧩 1.1. Rôle langage : autoriser plusieurs définitions

En C++, une fonction *ne doit être définie qu'une seule fois* dans tout le programme → c'est la **One Definition Rule (ODR)**.

Mais si une fonction est définie dans un header `.hpp` inclus dans plusieurs `.cpp`, on obtient plusieurs définitions → 🚫 erreur de link.

Le mot-clé **`inline` autorise explicitement cette situation** :

```cpp
inline int max_int(int a, int b) {
    return (a > b) ? a : b;
}
```

Ainsi, la fonction peut apparaître **dans tous les fichiers qui incluent ce header**, et le linker les fusionne en **une seule entité**.

---

## ⚙️ 1.2. Rôle optimisation : inlining machine (optionnel)

Historiquement, `inline` voulait dire :

> “Cher compilateur, remplace l'appel de la fonction par son code.”

Mais aujourd’hui :

* le compilateur peut **ignorer** `inline`,
* il peut **inliner une fonction non-inline** si ça l'arrange,
* il choisit selon l’optimisation globale.

👉 **`inline` n’est PAS une garantie de performance.**
👉 Son rôle principal est **structurel**, pas **optimisation**.

---

# 🧪 2. Fonctions `inline` : déclaration & utilisation

## 2.1. 🔧 Fonction libre déclarée `inline`

Dans un fichier header :

```cpp
// max_utils.hpp
#pragma once

inline int max_int(int a, int b) {
    return (a > b) ? a : b;
}
```

📌 **Pourquoi inline ?**
Parce que cette fonction sera **incluse dans plusieurs `.cpp`**, et on évite les erreurs ODR.

---

## 2.2. 🏷️ Exemple important : `minSur` vs `minARisque`

C’est un cas classique pour comprendre les dangers des macros et les avantages des `inline`.

### ❌ Version macro dangereuse

```cpp
#define minARisque(a, b) ((a) < (b) ? (a) : (b))
```

Regarde :

```cpp
int i = 3;
int r = minARisque(i++, 10);  
```

Macro expansée :

```
((i++) < (10) ? (i++) : (10))
```

⚠️ `i++` évalué **deux fois** → **comportement dangereux**.

---

### ✅ Version `inline` sûre

```cpp
inline int minSur(int a, int b) {
    return (a < b) ? a : b;
}

int r = minSur(i++, 10);  // i++ évalué UNE SEULE FOIS
```

📌 Les fonctions `inline` :

* respectent les priorités d’opérateurs,
* n’évaluent leurs arguments qu’une seule fois,
* sont typées et sûres.

---

# 🏗️ 3. Classes & Méthodes Inline

## 3.1. 🧱 Méthode définie *dans* la classe → automatiquement inline

Exemple :

```cpp
class Vector2D {
public:
    double x, y;

    // Implicitement inline
    double norm2() const {
        return x * x + y * y;
    }
};
```

✔️ Le C++ considère **toute méthode définie dans la classe** comme `inline`.
✔️ Pas besoin de le préciser (mais c’est possible).

---

## 3.2. 📝 Méthode définie après la classe → on écrit `inline`

```cpp
class Vector2D {
public:
    double x, y;
    double norm2() const;  // déclaration
};

// définition plus loin :
inline double Vector2D::norm2() const {
    return x * x + y * y;
}
```

Pourquoi écrire `inline` ici ?

🎯 Parce que cette définition peut être dans un header `.hpp`.
Sans `inline` → ❌ erreur de multiple definition.

---

# 🧮 4. `inline` vs `#define` : comprendre la différence essentielle

| Aspect ⚖️            | Macro `#define`       | Fonction `inline` |
| -------------------- | --------------------- | ----------------- |
| Traité par           | Préprocesseur         | Compilateur       |
| Vérification de type | ❌ Aucune              | ✅ Complète        |
| Évaluation arguments | ⚠️ Peut être multiple | ✔️ Une seule fois |
| Débogage             | Très difficile        | Normal            |
| Surcharge            | ❌ Impossible          | ✔️ Possible       |
| Templates            | ❌ Non                 | ✔️ Oui            |
| Namespaces           | ❌ Non                 | ✔️ Oui            |
| Sécurité             | ⚠️ Dangereux          | ✔️ Sûr            |

### Conclusion :

> Pour remplacer une “macro-fonction”, **utilise presque toujours une fonction inline**.

---

# 🔨 5. Où mettre les fonctions `inline` ?

Toujours dans un **header** :

```cpp
// math.hpp
#pragma once

inline double clamp(double v, double minV, double maxV) {
    if (v < minV) return minV;
    if (v > maxV) return maxV;
    return v;
}
```

✔️ Incluse partout
✔️ Pas de viol ODR
✔️ Code propre

---

# ⚠️ 6. Contraintes ODR pour les inline

Pour être valide, toutes les définitions :

* doivent être **identiques**,
* doivent apparaître dans **toutes les unités** qui l’utilisent,
* ne doivent jamais être définies “ailleurs” de manière différente.

📌 Sinon → UB ou erreur de link.

---

# 🧬 7. Inline et Templates

Les templates doivent être définis **dans les headers**, car le compilateur doit voir leur code pour les instancier.

```cpp
template<typename T>
T add(const T& a, const T& b) {
    return a + b;
}
```

Ici, pas besoin de `inline`, mais ce n'est **pas incorrect** de le mettre.
C’est le mécanisme d’instanciation template qui gère les multiples définitions.

---

# 📈 8. Avantages et Inconvénients des fonctions inline

## 👍 Avantages

* 🌐 Permettent de mettre des petites fonctions dans les headers proprement
* 🚀 Potentiel inlining machine (si le compilateur juge utile)
* 🧹 Code lisible, typé, déboguable
* 🛡️ Évitent les dangers des macros

---

## 👎 Inconvénients

* 📦 Risque de **bloat** (binaire trop gros)
* 🤷 Pas de garantie d’inlining machine réel
* 🔄 Recompile tous les fichiers quand le header change

---

# 🎯 9. Quand préférer `inline` ou une macro ?

### Utilisation recommandée

| Besoin                           | Solution            |
| -------------------------------- | ------------------- |
| Fonction simple, utilitaire      | **inline**          |
| Fonction dépendant du type       | **template inline** |
| Constante                        | `constexpr`         |
| Code conditionnel de compilation | `#define`, `#ifdef` |
| Métaprogrammation préprocesseur  | Macro (rare)        |

---

# 🧲 10. Récap rapide (ultra synthétique)

* `inline` = ✔️ multiple définitions en header + ❓ suggestion d’optimisation.
* Les méthodes définies **dans la classe** sont **automatiquement inline**.
* Toujours préférer fonction `inline` à une macro-fonction.
* Les macros peuvent *réévaluer* leurs arguments → ⚠️ danger.
* Les fonctions `inline` sont typées, sûres, namespace-compatibles.

---

[...retorn en rèire](../menu.md)

