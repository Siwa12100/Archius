# **Les Pointeurs Génériques (`void*`) et les Casts en C**

[...retorn en rèire](../menu.md)

---

## **Table des Matières**
- [**Les Pointeurs Génériques (`void*`) et les Casts en C**](#les-pointeurs-génériques-void-et-les-casts-en-c)
  - [**Table des Matières**](#table-des-matières)
  - [**1. Introduction aux Pointeurs Génériques (`void*`)**](#1-introduction-aux-pointeurs-génériques-void)
    - [**Caractéristiques Clés :**](#caractéristiques-clés-)
    - [**Exemple de Déclaration :**](#exemple-de-déclaration-)
  - [**2. Pourquoi Utiliser `void*` ?**](#2-pourquoi-utiliser-void-)
    - [**Cas d'Usage :**](#cas-dusage-)
    - [**Avantages :**](#avantages-)
    - [**Inconvénients :**](#inconvénients-)
  - [**3. Arithmétique des Pointeurs et Casts**](#3-arithmétique-des-pointeurs-et-casts)
    - [**3.1. Règles de Base**](#31-règles-de-base)
    - [**3.2. Exemples Concrets**](#32-exemples-concrets)
      - [**Exemple 1 : Incrémentation d'un `void*`**](#exemple-1--incrémentation-dun-void)
      - [**Exemple 2 : Copie d'un Pointeur**](#exemple-2--copie-dun-pointeur)
      - [**Exemple 3 : Arithmétique avec différents types**](#exemple-3--arithmétique-avec-différents-types)
  - [**4. Comprendre les Casts de Pointeurs**](#4-comprendre-les-casts-de-pointeurs)
    - [**4.1. Syntaxe et Utilité**](#41-syntaxe-et-utilité)
    - [**4.2. Cas d'Usage Courants**](#42-cas-dusage-courants)
  - [**5. Pièges et Erreurs Courantes**](#5-pièges-et-erreurs-courantes)
    - [**5.1. Déréférencement Incorrect**](#51-déréférencement-incorrect)
    - [**5.2. Problèmes d'Alignement Mémoire**](#52-problèmes-dalignement-mémoire)
    - [**5.3. Tailles de Types Incompatibles**](#53-tailles-de-types-incompatibles)
  - [**6. Exemples Pratiques**](#6-exemples-pratiques)
    - [**6.1. Fonction Générique de Copie (`memcpy`)**](#61-fonction-générique-de-copie-memcpy)
    - [**6.2. Liste Chaînée Générique**](#62-liste-chaînée-générique)
    - [**6.3. Gestion Dynamique de Types**](#63-gestion-dynamique-de-types)
  - [**7. Alternatives et Bonnes Pratiques**](#7-alternatives-et-bonnes-pratiques)
    - [**7.1. Utiliser des Unions (C99/C11)**](#71-utiliser-des-unions-c99c11)
    - [**7.2. Macros Génériques (`_Generic` en C11)**](#72-macros-génériques-_generic-en-c11)
    - [**7.3. Bonnes Pratiques avec `void*`**](#73-bonnes-pratiques-avec-void)
  - [**8. Exercices pour Maîtriser les Concepts**](#8-exercices-pour-maîtriser-les-concepts)
    - [**Exercice 1 : Écrire une fonction `print_array` générique**](#exercice-1--écrire-une-fonction-print_array-générique)
    - [**Exercice 2 : Implémenter un `malloc` simplifié**](#exercice-2--implémenter-un-malloc-simplifié)
    - [**Exercice 3 : Tri Générique avec `qsort`**](#exercice-3--tri-générique-avec-qsort)

---

## **1. Introduction aux Pointeurs Génériques (`void*`)**
En C, un **pointeur générique** (`void*`) est un pointeur qui peut stocker l'adresse de **n'importe quel type de donnée**, mais **sans information sur le type pointé**.

### **Caractéristiques Clés :**
- **Ne peut pas être déréférencé directement** (on ne peut pas faire `*ptr` si `ptr` est un `void*`).
- **Ne supporte pas l'arithmétique de pointeurs** (on ne peut pas faire `ptr + 1` sans un cast).
- **Peut être converti vers/n'importe quel autre type de pointeur** (et vice versa) sans perte d'information.

### **Exemple de Déclaration :**
```c
int x = 10;
float y = 3.14f;
void* ptr;

ptr = &x; // OK : ptr pointe vers un int
ptr = &y; // OK : ptr pointe maintenant vers un float
```

---
## **2. Pourquoi Utiliser `void*` ?**
### **Cas d'Usage :**
1. **Fonctions génériques** :
   - Exemple : `memcpy`, `malloc`, `qsort` utilisent `void*` pour manipuler des données de n'importe quel type.
   - ```c
     void* memcpy(void* dest, const void* src, size_t n);
     ```
2. **Structures de données hétérogènes** :
   - Exemple : Une liste chaînée qui peut stocker des `int`, `float`, `struct`, etc.
3. **Interopérabilité avec du code bas niveau** :
   - Exemple : Drivers matériels, systèmes embarqués, où les données sont souvent manipulées comme des octets bruts.
4. **Passage de pointeurs à des fonctions callback** :
   - Exemple : Threads, gestionnaires d'événements.

### **Avantages :**
- **Flexibilité** : Permet d'écrire du code indépendant du type.
- **Réutilisabilité** : Une seule fonction peut gérer plusieurs types.

### **Inconvénients :**
- **Perte de sécurité de type** : Le compilateur ne peut pas vérifier les erreurs de type.
- **Risque d'erreurs** : Mauvaise arithmétique, déréférencement incorrect, etc.

---
## **3. Arithmétique des Pointeurs et Casts**
### **3.1. Règles de Base**
| Opération | Avec `int*` | Avec `void*` | Solution |
|-----------|------------|--------------|----------|
| `ptr + 1` | Ajoute `sizeof(int)` (4 octets) | **ERREUR** : taille inconnue | Cast vers un type connu : `(int*)ptr + 1` |
| `ptr++`   | Incrémente de `sizeof(int)` | **ERREUR** | Cast requis |
| `*ptr`    | Accède à la valeur `int` | **ERREUR** | Cast requis : `*(int*)ptr` |

### **3.2. Exemples Concrets**
#### **Exemple 1 : Incrémentation d'un `void*`**
```c
int tab[2] = {10, 20};
void* ptr = tab;

ptr = (int*)ptr + 1; // ptr pointe maintenant vers tab[1]
printf("%d\n", *(int*)ptr); // Affiche 20
```
**Explication :**
1. `(int*)ptr` : On interprète `ptr` comme un pointeur vers `int`.
2. `+ 1` : On ajoute `sizeof(int)` (4 octets) à l'adresse.
3. `*(int*)ptr` : On déréférence le résultat comme un `int`.

#### **Exemple 2 : Copie d'un Pointeur**
```c
int x = 10;
void* ptr = &x;

int* ptr_int = (int*)ptr; // Cast vers int*
printf("%d\n", *ptr_int); // Affiche 10
```
**Explication :**
- Le cast `(int*)ptr` permet de dire au compilateur : "Traite `ptr` comme un pointeur vers `int`".

#### **Exemple 3 : Arithmétique avec différents types**
```c
double tab_d[2] = {1.1, 2.2};
void* ptr = tab_d;

ptr = (double*)ptr + 1; // Ajoute sizeof(double) (8 octets)
printf("%lf\n", *(double*)ptr); // Affiche 2.2
```
**Explication :**
- Sans le cast `(double*)`, le compilateur ne saurait pas qu'il faut ajouter 8 octets.

---
## **4. Comprendre les Casts de Pointeurs**
### **4.1. Syntaxe et Utilité**
Un **cast** est une conversion explicite du type d'un pointeur.
**Syntaxe :**
```c
(nouveau_type*)pointeur
```
**Utilité :**
- **Informer le compilateur** du type réel du pointeur.
- **Autoriser l'arithmétique de pointeurs**.
- **Permettre le déréférencement**.

### **4.2. Cas d'Usage Courants**
| Cas | Exemple | Explication |
|-----|---------|-------------|
| **Arithmétique** | `ptr = (int*)ptr + 1;` | Incrémente `ptr` de `sizeof(int)`. |
| **Déréférencement** | `x = *(int*)ptr;` | Lit la valeur comme un `int`. |
| **Passage à une fonction** | `func((int*)ptr);` | Passe `ptr` comme un `int*` à `func`. |
| **Comparaison** | `if ((int*)ptr == autre_ptr) {...}` | Compare deux pointeurs du même type. |

---
## **5. Pièges et Erreurs Courantes**
### **5.1. Déréférencement Incorrect**
**Erreur :**
```c
void* ptr = ...;
int x = *ptr; // ERREUR : déréférencement direct impossible
```
**Correction :**
```c
int x = *(int*)ptr; // OK : cast avant déréférencement
```

### **5.2. Problèmes d'Alignement Mémoire**
Certains processeurs exigent que les adresses soient alignées sur des frontières spécifiques (ex: `int` sur 4 octets, `double` sur 8 octets).
**Exemple dangereux :**
```c
char buffer[10];
void* ptr = buffer;
double* d = (double*)(ptr + 1); // ptr + 1 n'est pas aligné sur 8 octets !
*d = 3.14; // Comportement indéfini (plantage possible)
```
**Solution :**
- Utiliser des types alignés ou des fonctions comme `memcpy`.
- Éviter les casts vers des types plus grands que `char` sur des adresses non alignées.

### **5.3. Tailles de Types Incompatibles**
**Erreur :**
```c
int tab[2] = {10, 20};
void* ptr = tab;
ptr = (char*)ptr + 1; // Avance de 1 octet seulement !
int x = *(int*)ptr;   // Lit une valeur corrompue (décalage)
```
**Explication :**
- `(char*)ptr + 1` avance de 1 octet, mais `*(int*)ptr` lit 4 octets à partir de cette adresse décalée.
- **Résultat** : Valeur incorrecte (ou plantage si l'adresse est invalide).

**Correction :**
```c
ptr = (int*)ptr + 1; // Avance de sizeof(int) octets
```

---
## **6. Exemples Pratiques**
### **6.1. Fonction Générique de Copie (`memcpy`)**
```c
void* my_memcpy(void* dest, const void* src, size_t n) {
    char* d = (char*)dest;
    const char* s = (const char*)src;
    for (size_t i = 0; i < n; i++) {
        d[i] = s[i];
    }
    return dest;
}
```
**Explication :**
- On cast `dest` et `src` en `char*` pour copier octet par octet.
- Fonctionne pour n'importe quel type car on traite les données comme des `char`.

### **6.2. Liste Chaînée Générique**
```c
typedef struct Node {
    void* data;
    struct Node* next;
} Node;

Node* create_node(void* data) {
    Node* node = malloc(sizeof(Node));
    node->data = data;
    node->next = NULL;
    return node;
}

int main() {
    int x = 10;
    float y = 3.14f;
    Node* list = create_node(&x);
    list->next = create_node(&y);

    // Accès aux données
    printf("%d\n", *(int*)list->data);         // Affiche 10
    printf("%f\n", *(float*)list->next->data); // Affiche 3.14
    return 0;
}
```
**Explication :**
- `void* data` peut stocker n'importe quel type.
- On cast `data` vers le type réel (`int*`, `float*`) pour accéder à la valeur.

### **6.3. Gestion Dynamique de Types**
```c
typedef enum { INT, FLOAT, STRING } Type;

typedef struct {
    Type type;
    void* data;
} Value;

void print_value(Value v) {
    switch (v.type) {
        case INT:
            printf("%d\n", *(int*)v.data);
            break;
        case FLOAT:
            printf("%f\n", *(float*)v.data);
            break;
        case STRING:
            printf("%s\n", (char*)v.data);
            break;
    }
}

int main() {
    int x = 42;
    float y = 3.14f;
    char z[] = "Hello";

    Value values[] = {
        {INT, &x},
        {FLOAT, &y},
        {STRING, z}
    };

    for (int i = 0; i < 3; i++) {
        print_value(values[i]);
    }
    return 0;
}
```
**Explication :**
- On stocke le type dans un `enum` pour savoir comment caster `data`.
- `print_value` utilise un `switch` pour appliquer le bon cast.

---
## **7. Alternatives et Bonnes Pratiques**
### **7.1. Utiliser des Unions (C99/C11)**
```c
typedef union {
    int i;
    float f;
    char* s;
} Data;

Data d;
d.i = 42; // Stocke un int
d.f = 3.14f; // Stocke un float (écrase l'int précédent)
```
**Avantages :**
- Pas besoin de casts pour accéder aux membres.
- Le compilateur gère l'alignement.

**Inconvénients :**
- Un seul membre peut être valide à la fois (taille = taille du plus grand membre).

### **7.2. Macros Génériques (`_Generic` en C11)**
```c
#define print(x) _Generic((x), \
    int*: print_int,          \
    float*: print_float,      \
    char**: print_string      \
)(x)

void print_int(int* x) { printf("%d\n", *x); }
void print_float(float* x) { printf("%f\n", *x); }
void print_string(char** x) { printf("%s\n", *x); }

int main() {
    int a = 10;
    float b = 3.14f;
    char c[] = "Hello";
    print(&a); // Appelle print_int
    print(&b); // Appelle print_float
    print(&c); // ERREUR : type incompatible
    return 0;
}
```
**Limites :**
- Ne fonctionne qu'avec des types connus à la compilation.
- Moins flexible qu'un `void*`.

### **7.3. Bonnes Pratiques avec `void*`**
1. **Toujours documenter le type attendu** :
   - Exemple : `// ptr doit être un int*`.
2. **Vérifier les alignements** :
   - Utiliser `memcpy` pour copier des données si l'alignement est incertain.
3. **Éviter les casts inutiles** :
   - Exemple : `(int*)ptr` est inutile si `ptr` est déjà un `int*`.
4. **Préférer les types concrets quand possible** :
   - Utiliser `void*` seulement quand la généricité est nécessaire.

---
## **8. Exercices pour Maîtriser les Concepts**
### **Exercice 1 : Écrire une fonction `print_array` générique**
```c
// Prototypes
void print_array(void* array, size_t count, size_t size, void (*print_element)(void*));

void print_int(void* ptr) { printf("%d ", *(int*)ptr); }
void print_float(void* ptr) { printf("%f ", *(float*)ptr); }

int main() {
    int ints[] = {1, 2, 3};
    float floats[] = {1.1f, 2.2f, 3.3f};

    print_array(ints, 3, sizeof(int), print_int);
    print_array(floats, 3, sizeof(float), print_float);
    return 0;
}
```
**Solution :**
```c
void print_array(void* array, size_t count, size_t size, void (*print_element)(void*)) {
    char* ptr = (char*)array;
    for (size_t i = 0; i < count; i++) {
        print_element(ptr + i * size);
    }
    printf("\n");
}
```

### **Exercice 2 : Implémenter un `malloc` simplifié**
```c
void* my_malloc(size_t size) {
    // Alloue de la mémoire et retourne un void*
    // (Utiliser sbrk ou un tableau statique pour simplifier)
}
```
**Solution (version simplifiée) :**
```c
#define MEMORY_POOL_SIZE 1024
static char memory_pool[MEMORY_POOL_SIZE];
static size_t used = 0;

void* my_malloc(size_t size) {
    if (used + size > MEMORY_POOL_SIZE) {
        return NULL; // Plus de mémoire
    }
    void* ptr = &memory_pool[used];
    used += size;
    return ptr;
}
```

### **Exercice 3 : Tri Générique avec `qsort`**
```c
// Écrire une fonction de comparaison pour trier un tableau d'entiers et de floats.

int compare_int(const void* a, const void* b) {
    return (*(int*)a - *(int*)b);
}

int compare_float(const void* a, const void* b) {
    return (*(float*)a > *(float*)b) ? 1 : -1;
}

int main() {
    int ints[] = {3, 1, 2};
    float floats[] = {3.3f, 1.1f, 2.2f};

    qsort(ints, 3, sizeof(int), compare_int);
    qsort(floats, 3, sizeof(float), compare_float);
    return 0;
}
```

---

[...retorn en rèire](../menu.md)