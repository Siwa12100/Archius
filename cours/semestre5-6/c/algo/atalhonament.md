# 📘 Table de Hachage

[...retorn en rèire](../menu.md)

---

## 🚀 Introduction

En algorithmique, un **dictionnaire** est une structure de données qui associe des **clés** à des **valeurs**.

* Exemple : associer un **nom** (clé) à un **numéro de téléphone** (valeur).

Jusqu’à présent, les meilleures structures comme les **arbres équilibrés (AVL, Red-Black)** offrent une complexité en **O(log n)**.

👉 Mais peut-on faire mieux ?

➡️ Oui : avec les **tables de hachage**, qui permettent des recherches, insertions et suppressions en **temps constant (O(1))**, dans le cas idéal.

---

## 🗂️ Tableau à accès direct

### Principe :

* La clé **est directement** l’indice dans un tableau.

```c
int tableau[10000]; // exemple d’un tableau direct
tableau[1987] = 42; // clé = 1987 (année de naissance), valeur = 42
```

### ✅ Avantage :

* Accès en **temps constant** (O(1))

### ❌ Inconvénient :

* Le tableau doit être aussi grand que l’**amplitude des données possibles**, ce qui est souvent énorme et inutile.

👉 Exemple : pour stocker des **années de naissance**, on aurait besoin d’un tableau avec des millions de cases, alors qu’on n’utilise qu’une petite fraction.

---

## 🔑 Table de hachage

Pour éviter le gaspillage mémoire, on intercale une **fonction de hachage** entre la clé et l’indice du tableau :

```
clé  ──> fonction de hachage h(clé) ──> indice dans le tableau
```

* La taille du tableau (**m**) est bien plus petite que le nombre total de clés possibles.
* Contrainte : `h(clé)` doit être un entier entre `0` et `m-1`.

---

## 🌀 Fonction de hachage

### Caractéristiques :

* ⚠️ **Non injective** → deux clés différentes peuvent donner la même valeur de hachage.
* Cela entraîne des **collisions**.

### Exemple simple en C :

```c
unsigned int hachage_mod(int cle, int m) {
    return cle % m;  // reste de la division
}
```

👉 Si deux clés donnent le même indice → **collision**.

---

## ⚔️ Gestion des collisions

Deux stratégies principales existent :

### 1. 🔗 Adressage fermé (chaînage)

Chaque case du tableau contient une **liste chaînée** (ou autre structure) des éléments qui ont le même indice.

```c
// Exemple simplifié : liste chaînée
typedef struct Element {
    int cle;
    int valeur;
    struct Element* suivant;
} Element;

Element* table[100]; // table de 100 cases
```

* **Insertion** : ajouter au début de la liste.
* **Recherche** : parcourir la liste.

👉 Complexité dépend du rapport `u/m` :

* `u` = nombre de clés utilisées
* `m` = nombre de cases dans le tableau
* Si `u = O(m)` → coût ≈ O(1).

---

### 2. 🎯 Adressage ouvert

Au lieu d’une liste, on **cherche une autre case libre** dans le tableau.

#### Exemple en C – sondage linéaire :

```c
int hachage_ouvert(int cle, int m, int i) {
    return (cle + i) % m; // i = rang d'essai
}
```

* Si `T[h(cle,0)]` est occupée → essayer `T[h(cle,1)]`, etc.
* **Suppression** : utiliser une valeur spéciale (`-1` par ex.) pour indiquer une case supprimée.

👉 Inconvénient : la suppression **dégrade les performances** car elle laisse des "trous".

---

## ⚙️ Exemples de fonctions de hachage

### 1. 🔢 Méthode du reste

```c
unsigned int hachage(int cle, int m) {
    return cle % m;
}
```

* Simple mais piège si `m` est une puissance de 2 → seules les **valeurs de poids faible** comptent.

👉 Solution : choisir `m` premier.

---

### 2. 📐 Méthode des parties entières

```c
unsigned int hachage_fraction(int cle, int m) {
    double A = 0.6180339887; // (√5 - 1) / 2
    return (unsigned int)(m * (fmod(cle * A, 1.0)));
}
```

* Utilise une constante irrationnelle → meilleure distribution.

---

### 3. 🔄 Rotation de bits (pour chaînes de caractères)

```c
unsigned int shift_rotate(unsigned int val, unsigned int n) {
    n = n % (sizeof(unsigned int) * 8);
    return (val << n) | (val >> (sizeof(unsigned int) * 8 - n));
}
```

👉 Utile pour combiner les caractères d’une chaîne sans perdre d’information.

---

## ⚖️ Comparaison des méthodes

### 🔗 Adressage fermé

✅ Avantages :

* Supporte un nombre quelconque d’éléments.
* Performances stables même après suppression.

❌ Inconvénient :

* Surcoût mémoire (listes).

---

### 🎯 Adressage ouvert

✅ Avantages :

* Pas de surcoût mémoire.
* Très rapide si peu de collisions.

❌ Inconvénients :

* Taille de la table fixe.
* Choix de la fonction de hachage critique.
* Suppression dégrade les performances.

---

## 📌 Conclusion

* Les **tables de hachage** offrent une complexité proche de **O(1)** pour les opérations de dictionnaire.
* ⚠️ Pas d’ordre : impossible d’obtenir les éléments triés sans autre structure.
* 👉 À privilégier si on cherche **rapidité** et non **ordre**.

---

# 📝 Exemple complet en C

Voici un petit exemple d’implémentation d’une **table de hachage avec chaînage** en C :

```c
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define M 10  // taille de la table

typedef struct Element {
    char* cle;
    int valeur;
    struct Element* suivant;
} Element;

Element* table[M];

// Fonction de hachage simple (somme des caractères mod M)
unsigned int hachage(const char* cle) {
    unsigned int sum = 0;
    for (int i = 0; cle[i] != '\0'; i++) {
        sum += cle[i];
    }
    return sum % M;
}

void inserer(const char* cle, int valeur) {
    unsigned int idx = hachage(cle);
    Element* nouvel = malloc(sizeof(Element));
    nouvel->cle = strdup(cle);
    nouvel->valeur = valeur;
    nouvel->suivant = table[idx];
    table[idx] = nouvel;
}

int rechercher(const char* cle) {
    unsigned int idx = hachage(cle);
    Element* courant = table[idx];
    while (courant != NULL) {
        if (strcmp(courant->cle, cle) == 0) {
            return courant->valeur;
        }
        courant = courant->suivant;
    }
    return -1; // non trouvé
}

int main() {
    inserer("Alice", 42);
    inserer("Bob", 99);
    printf("Valeur de Alice : %d\n", rechercher("Alice"));
    printf("Valeur de Bob : %d\n", rechercher("Bob"));
    return 0;
}
```

[...retorn en rèire](../menu.md)