# 📜 Documentation C : Syntaxe Essentielle

[...retorn en rèire](../menu.md)

---

## **📄 1. Organisation des Fichiers `.c` et `.h`**
### **🔹 Pourquoi séparer `.c` et `.h` ?**
- **`.h` (Header)** : Déclarations (prototypes de fonctions, structures, macros).
- **`.c` (Source)** : Définitions (implémentations des fonctions).

**Avantages** :
✅ **Modularité** : Réutiliser le code dans plusieurs fichiers.
✅ **Clarté** : Séparation entre interface (`.h`) et implémentation (`.c`).
✅ **Compilation séparée** : Accélère la recompilation (`make`).

---

### **📌 Exemple : Communication entre `mon_module.h` et `mon_module.c`**
#### **📄 `mon_module.h` (Déclarations)**
```c
#ifndef MON_MODULE_H  // Garde contre les inclusions multiples
#define MON_MODULE_H

// Déclaration d'une structure
typedef struct {
    int x;
    int y;
} Point;

// Prototypes de fonctions
void afficher_point(const Point *p);
Point creer_point(int x, int y);
int distance(const Point *p1, const Point *p2);

#endif // MON_MODULE_H
```

#### **📄 `mon_module.c` (Définitions)**
```c
#include "mon_module.h"
#include <stdio.h>
#include <math.h>

// Implémentation des fonctions
void afficher_point(const Point *p) {
    printf("Point(%d, %d)\n", p->x, p->y);
}

Point creer_point(int x, int y) {
    Point p = {x, y};
    return p;
}

int distance(const Point *p1, const Point *p2) {
    int dx = p1->x - p2->x;
    int dy = p1->y - p2->y;
    return (int)sqrt(dx*dx + dy*dy);
}
```

#### **📄 `main.c` (Utilisation)**
```c
#include "mon_module.h"
#include <stdio.h>

int main() {
    Point p1 = creer_point(3, 4);
    Point p2 = creer_point(6, 8);

    afficher_point(&p1);
    afficher_point(&p2);

    printf("Distance: %d\n", distance(&p1, &p2));
    return 0;
}
```

---
### **🔧 Compilation et Lien**
```bash
# Compilation séparée
gcc -c mon_module.c -o mon_module.o  # Génère un fichier objet
gcc -c main.c -o main.o

# Édition de liens
gcc mon_module.o main.o -o programme -lm  # -lm pour math.h

# Exécution
./programme
```
**Sortie** :
```
Point(3, 4)
Point(6, 8)
Distance: 5
```

---
### **⚠️ Bonnes Pratiques**
1. **Garde contre les inclusions multiples** :
   ```c
   #ifndef MON_MODULE_H
   #define MON_MODULE_H
   // ...
   #endif
   ```
2. **Ne pas définir de variables globales dans les `.h`** (utilisez `extern` si nécessaire).
3. **Limitez les dépendances** : Un `.h` ne doit inclure que ce qui est strictement nécessaire.

---

---

## **🏗 2. Structures (`struct`) et `typedef`**
### **🔹 Déclaration d'une Structure**
```c
// Sans typedef (nécessite d'écrire "struct Point" partout)
struct Point {
    int x;
    int y;
};

// Avec typedef (plus court : "Point" suffit)
typedef struct {
    int x;
    int y;
} Point;
```

---
### **📌 Exemple : Structure Imbriquée**
```c
typedef struct {
    int jour;
    int mois;
    int annee;
} Date;

typedef struct {
    char nom[50];
    Date date_naissance;
} Personne;
```

---
### **🔧 Accès aux Champs**
```c
Personne p = {"Alice", {15, 8, 1990}};
printf("Nom: %s, Né(e) le %d/%d/%d\n",
       p.nom, p.date_naissance.jour,
       p.date_naissance.mois, p.date_naissance.annee);
```

---
### **⚠️ Pièges à Éviter**
- **Oublier le `;`** après une déclaration de `struct` :
  ```c
  typedef struct {  // ❌ Erreur : il manque un ";"
      int x;
      int y;
  } Point
  ```
- **Confondre `=` et `==`** dans les comparaisons de structures (il faut comparer champ par champ).

---

---

## **🏷 3. `typedef` et `define`**
### **🔹 `typedef` : Créer des Alias de Types**
```c
typedef unsigned long ulong;  // ulong = alias pour unsigned long
typedef int* ptr_int;         // ptr_int = pointeur vers int

ulong population = 7800000000;
ptr_int p = malloc(sizeof(int));
```

---
### **🔹 `define` : Macros et Constantes**
```c
#define PI 3.14159          // Constante
#define CARRE(x) ((x) * (x)) // Macro avec paramètre
#define MAX(a, b) ((a) > (b) ? (a) : (b))

// Utilisation
printf("PI = %f\n", PI);
printf("Carré de 5: %d\n", CARRE(5));  // Affiche 25
printf("Max entre 3 et 7: %d\n", MAX(3, 7));  // Affiche 7
```

---
### **⚠️ Pièges avec `define`**
1. **Oublier les parenthèses** :
   ```c
   #define CARRE(x) x * x  // ❌ Mauvaise pratique
   CARRE(1 + 2)  // Devient 1 + 2 * 1 + 2 = 5 (au lieu de 9)
   ```
   **Solution** :
   ```c
   #define CARRE(x) ((x) * (x))  // ✅ Correct
   ```

2. **Effets de bord** :
   ```c
   #define MAX(a, b) ((a) > (b) ? (a) : (b))
   int x = 5;
   int y = MAX(x++, 6);  // x est incrémenté deux fois !
   ```

3. **Pas de typage** : `define` est remplacé par le préprocesseur **avant** la compilation.

---
### **🔹 Quand utiliser `typedef` vs `define` ?**
| `typedef` | `define` |
|-----------|----------|
| Créer des **alias de types** (ex: `typedef int* ptr_int`). | Définir des **constantes** ou **macros**. |
| Vérifié par le compilateur (typage fort). | Remplacé textuellement (pas de typage). |
| Exemple : `typedef struct { ... } Point;` | Exemple : `#define TAILLE_MAX 100` |

---

---

## **👉 4. Pointeurs : Syntaxe et Utilisation**
### **🔹 Déclaration et Initialisation**
```c
int x = 10;
int *ptr = &x;  // ptr pointe vers x

printf("Valeur de x: %d\n", x);       // 10
printf("Adresse de x: %p\n", &x);     // Adresse mémoire (ex: 0x7ffd42a1b2ac)
printf("Valeur de ptr: %p\n", ptr);   // Même adresse que &x
printf("Valeur pointée par ptr: %d\n", *ptr);  // 10 (déréférencement)
```

---
### **📌 Arithmétique des Pointeurs**
```c
int tab[5] = {10, 20, 30, 40, 50};
int *ptr = tab;  // ptr pointe vers tab[0]

printf("%d\n", *ptr);    // 10 (tab[0])
printf("%d\n", *(ptr + 1));  // 20 (tab[1])
printf("%d\n", *(ptr + 3));  // 40 (tab[3])
```

---
### **🔹 Pointeurs et Fonctions**
#### **Passage par référence (modification possible)**
```c
void incrementer(int *x) {
    (*x)++;  // Déréférencement pour modifier la valeur
}

int main() {
    int a = 5;
    incrementer(&a);
    printf("a = %d\n", a);  // a = 6
    return 0;
}
```

#### **Passage par valeur (copie, pas de modification)**
```c
void faux_incrementer(int x) {
    x++;  // Modifie seulement la copie locale
}

int main() {
    int a = 5;
    faux_incrementer(a);
    printf("a = %d\n", a);  // a = 5 (inchangé)
    return 0;
}
```

---
### **⚠️ Erreurs Courantes avec les Pointeurs**
| Erreur | Exemple | Solution |
|--------|---------|----------|
| **Déréférencement d'un pointeur non initialisé** | `int *p; printf("%d", *p);` | Toujours initialiser : `int *p = NULL;` |
| **Oublier `&` pour passer l'adresse** | `incrementer(x);` au lieu de `incrementer(&x);` | Passer `&variable`. |
| **Arithmétique sur des pointeurs non compatibles** | `double *p; p++; // puis cast en int*` | Éviter les casts inutiles. |
| **Débordement de tableau** | `int tab[5]; tab[5] = 10;` | Vérifier les limites (`size_t i < 5`). |

---

---

## **🗃 5. Allocation Dynamique (`malloc`, `realloc`, `free`)**
### **🔹 `malloc` : Allouer de la Mémoire**
```c
#include <stdlib.h>

// Allouer un entier
int *ptr = (int*)malloc(sizeof(int));
if (ptr == NULL) {
    fprintf(stderr, "Erreur d'allocation\n");
    exit(EXIT_FAILURE);
}
*ptr = 42;
printf("%d\n", *ptr);  // 42
free(ptr);  // Libérer la mémoire
```

---
### **🔹 `calloc` : Allouer et Initialiser à 0**
```c
int *tab = (int*)calloc(5, sizeof(int));  // Tableau de 5 entiers initialisés à 0
if (tab == NULL) {
    perror("calloc");
    exit(EXIT_FAILURE);
}
for (int i = 0; i < 5; i++) {
    printf("%d ", tab[i]);  // 0 0 0 0 0
}
free(tab);
```

---
### **🔹 `realloc` : Redimensionner un Bloc Alloué**
```c
int *tab = malloc(3 * sizeof(int));
for (int i = 0; i < 3; i++) tab[i] = i + 1;  // tab = [1, 2, 3]

// Agrandir à 5 éléments
int *new_tab = realloc(tab, 5 * sizeof(int));
if (new_tab == NULL) {
    perror("realloc");
    free(tab);  // Libérer l'ancienne mémoire en cas d'échec
    exit(EXIT_FAILURE);
}
tab = new_tab;
tab[3] = 4;
tab[4] = 5;  // tab = [1, 2, 3, 4, 5]

free(tab);
```

---
### **🔧 Bonnes Pratiques pour l'Allocation Dynamique**
1. **Toujours vérifier le retour de `malloc`/`calloc`/`realloc`** :
   ```c
   int *ptr = malloc(sizeof(int));
   if (ptr == NULL) { /* Gérer l'erreur */ }
   ```
2. **Libérer la mémoire avec `free`** quand elle n'est plus utile :
   ```c
   free(ptr);
   ptr = NULL;  // Éviter les pointeurs pendants (dangling pointers)
   ```
3. **Ne pas utiliser de mémoire libérée** :
   ```c
   int *p = malloc(sizeof(int));
   free(p);
   *p = 5;  // ❌ Comportement indéfini !
   ```
4. **Éviter les fuites mémoire** :
   ```c
   void fuite() {
       int *p = malloc(sizeof(int));
       // Oublier free(p) → fuite !
   }
   ```
5. **Utiliser `sizeof(*ptr)` pour plus de clarté** :
   ```c
   int *tab = malloc(n * sizeof(*tab));  // Plus lisible que sizeof(int)
   ```

---
### **📌 Exemple Complet : Gestion d'un Tableau Dynamique**
```c
#include <stdio.h>
#include <stdlib.h>

int main() {
    size_t taille = 5;
    int *tab = malloc(taille * sizeof(int));
    if (tab == NULL) {
        perror("malloc");
        return EXIT_FAILURE;
    }

    // Remplir le tableau
    for (size_t i = 0; i < taille; i++) {
        tab[i] = (int)(i + 1);
    }

    // Agrandir le tableau
    taille = 10;
    int *new_tab = realloc(tab, taille * sizeof(int));
    if (new_tab == NULL) {
        perror("realloc");
        free(tab);
        return EXIT_FAILURE;
    }
    tab = new_tab;

    // Remplir les nouvelles cases
    for (size_t i = 5; i < taille; i++) {
        tab[i] = (int)(i + 1);
    }

    // Afficher
    for (size_t i = 0; i < taille; i++) {
        printf("%d ", tab[i]);  // 1 2 3 4 5 6 7 8 9 10
    }
    printf("\n");

    free(tab);
    return EXIT_SUCCESS;
}
```

---
### **⚠️ Erreurs Courantes avec l'Allocation Dynamique**
| Erreur | Conséquence | Solution |
|--------|-------------|----------|
| **Oublier `free`** | Fuite mémoire. | Toujours libérer la mémoire après utilisation. |
| **Utiliser un pointeur après `free`** | Comportement indéfini (crash possible). | Mettre le pointeur à `NULL` après `free`. |
| **`realloc` échoue et on perd l'ancien pointeur** | Perte de mémoire. | Toujours stocker le retour de `realloc` dans une variable temporaire. |
| **Allouer moins que nécessaire** | Débordement de mémoire. | Vérifier les tailles avec `sizeof`. |
| **Caster inutilement le retour de `malloc`** | Moins portable. | En C, le cast est implicite (à éviter). |

---

---

## **📚 Résumé des Bonnes Pratiques**
### **📄 Fichiers `.h`/`.c`**
- Séparer **déclarations** (`.h`) et **définitions** (`.c`).
- Utiliser des **gardes d'inclusion** (`#ifndef`).
- Éviter les variables globales dans les `.h`.

### **🏗 Structures et `typedef`**
- Utiliser `typedef` pour simplifier la syntaxe.
- Comparer les structures **champ par champ** (pas avec `==`).

### **🏷 `define` vs `typedef`**
- `define` : Pour des **constantes** ou **macros simples**.
- `typedef` : Pour créer des **alias de types**.

### **👉 Pointeurs**
- Toujours **initialiser** (`NULL` si inutilisé).
- Vérifier les **déréférencements** (`if (ptr != NULL)`).
- Passer les **adresses** (`&variable`) aux fonctions qui modifient des variables.

### **🗃 Allocation Dynamique**
- **Vérifier les retours** de `malloc`/`realloc`.
- **Libérer la mémoire** avec `free` quand elle n'est plus utile.
- **Mettre à `NULL`** après `free` pour éviter les pointeurs pendants.
- Utiliser `sizeof(*ptr)` pour plus de lisibilité.

---

---
## **🎯 Exemple Final : Programme Complet**
### **📄 `point.h`**
```c
#ifndef POINT_H
#define POINT_H

typedef struct {
    int x;
    int y;
} Point;

Point* creer_point_dynamique(int x, int y);
void afficher_point(const Point *p);
void liberer_point(Point *p);

#endif
```

### **📄 `point.c`**
```c
#include "point.h"
#include <stdio.h>
#include <stdlib.h>

Point* creer_point_dynamique(int x, int y) {
    Point *p = malloc(sizeof(Point));
    if (p == NULL) {
        return NULL;
    }
    p->x = x;
    p->y = y;
    return p;
}

void afficher_point(const Point *p) {
    if (p == NULL) {
        printf("Pointeur nul!\n");
        return;
    }
    printf("Point(%d, %d)\n", p->x, p->y);
}

void liberer_point(Point *p) {
    free(p);
}
```

### **📄 `main.c`**
```c
#include "point.h"
#include <stdio.h>

int main() {
    Point *p = creer_point_dynamique(3, 4);
    if (p == NULL) {
        fprintf(stderr, "Erreur d'allocation\n");
        return EXIT_FAILURE;
    }

    afficher_point(p);  // Point(3, 4)
    liberer_point(p);
    p = NULL;  // Bonne pratique

    return EXIT_SUCCESS;
}
```

### **🔧 Compilation et Exécution**
```bash
gcc -c point.c -o point.o
gcc -c main.c -o main.o
gcc point.o main.o -o programme
./programme
```
**Sortie** :
```
Point(3, 4)
```

---

[...retorn en rèire](../menu.md)