# **Tableaux de `char`, Chaînes de Caractères (`strings`) et Fonctions de Manipulation en C**

[...retorn en rèire](../menu.md)

---

## **Sommaire**
1. **[Introduction aux Tableaux de `char`](#1-introduction-aux-tableaux-de-char)**
   - Déclaration et initialisation
   - Différence entre `char` et `char*`

2. **[Chaînes de Caractères (`strings`) en C](#2-chaînes-de-caractères-strings-en-c)**
   - Définition et terminaison par `\0`
   - Exemples de déclaration

3. **[Fonctions de Base pour les Chaînes](#3-fonctions-de-base-pour-les-chaînes)**
   - `strlen` (longueur)
   - `strcpy` et `strncpy` (copie)
   - `strcat` et `strncat` (concaténation)
   - `strcmp` et `strncmp` (comparaison)

4. **[Fonctions Avancées](#4-fonctions-avancées)**
   - `strchr` et `strrchr` (recherche de caractère)
   - `strstr` (recherche de sous-chaîne)
   - `sprintf` et `snprintf` (formatage)
   - `sscanf` (analyse)

5. **[Manipulation Sécurisée](#5-manipulation-sécurisée)**
   - Risques de débordement (`buffer overflow`)
   - Bonnes pratiques

6. **[Exemples Complets](#6-exemples-complets)**
   - Copie, concaténation, comparaison
   - Parsing de chaînes

7. **[Exercices Corrigés](#7-exercices-corrigés)**

---

---

## **1. Introduction aux Tableaux de `char`** <a name="1-introduction-aux-tableaux-de-char"></a>

### **1.1. Déclaration et Initialisation**
Un tableau de `char` est une séquence de caractères stockés en mémoire contiguë.

#### **Syntaxe :**
```c
char tableau[N];  // Tableau non initialisé de taille N
char tableau[] = "hello";  // Tableau initialisé (taille automatique = 6 : 'h','e','l','l','o','\0')
```

#### **Exemples :**
```c
// Déclaration et initialisation explicite
char mot1[6] = {'h', 'e', 'l', 'l', 'o', '\0'};  // Avec \0 terminal
char mot2[] = "hello";  // Le compilateur ajoute \0 automatiquement

// Tableau non initialisé (contenant des valeurs aléatoires)
char buffer[100];
```

#### **Attention :**
- **Toujours réserver un octet pour `\0`** (caractère de fin de chaîne).
- **Pas de vérification des limites** : Déborder d'un tableau de `char` provoque un **comportement indéfini** (plantage, corruption mémoire).

---

### **1.2. Différence entre `char[]` et `char*`**
| `char[]` | `char*` |
|----------|---------|
| Tableau de caractères **modifiable**. | Pointeur vers une chaîne **littérale** (constante en mémoire morte, non modifiable). |
| Alloué sur la pile (`stack`). | Peut pointer vers la pile ou le tas (`heap`). |
| `sizeof` donne la taille totale (y compris `\0`). | `sizeof` donne la taille du pointeur (4 ou 8 octets). |

#### **Exemple :**
```c
char tab[] = "hello";  // Modifiable
tab[0] = 'H';  // OK

char* ptr = "world";  // Littérale (non modifiable)
ptr[0] = 'W';  // ❌ ERREUR : Segmentation fault (mémoire en lecture seule)
```

#### **Pour modifier une chaîne littérale :**
```c
char* ptr = strdup("world");  // Alloue une copie modifiable dans le tas
ptr[0] = 'W';  // OK
free(ptr);  // Libérer la mémoire !
```

---

---

## **2. Chaînes de Caractères (`strings`) en C** <a name="2-chaînes-de-caractères-strings-en-c"></a>

### **2.1. Définition**
En C, une **chaîne de caractères** (`string`) est un **tableau de `char` terminé par `\0`** (caractère nul, code ASCII `0`).

#### **Exemple :**
```c
char str[] = "bonjour";
// En mémoire : 'b', 'o', 'n', 'j', 'o', 'u', 'r', '\0'
```

#### **Attention :**
- **Oublier `\0`** → Fonctions comme `strlen` ou `strcpy` ne s'arrêteront pas et liront la mémoire jusqu'à trouver un `\0` (comportement indéfini).
- **Taille du tableau** = nombre de caractères **+ 1** (pour `\0`).

---

### **2.2. Déclaration et Initialisation**
```c
// Méthode 1 : Tableau avec taille explicite
char str1[7] = "bonjour";  // 6 lettres + \0

// Méthode 2 : Taille automatique (le compilateur compte les caractères + \0)
char str2[] = "bonjour";

// Méthode 3 : Pointeur vers une littérale (non modifiable)
const char* str3 = "bonjour";
```

---

### **2.3. Parcourir une Chaîne**
```c
#include <stdio.h>

int main() {
    char str[] = "hello";

    // Parcours avec un index
    for (int i = 0; str[i] != '\0'; i++) {
        printf("%c ", str[i]);
    }
    printf("\n");

    // Parcours avec un pointeur
    for (char* p = str; *p != '\0'; p++) {
        printf("%c ", *p);
    }
    printf("\n");

    return 0;
}
```
**Sortie :**
```
h e l l o
h e l l o
```

---

---

## **3. Fonctions de Base pour les Chaînes** <a name="3-fonctions-de-base-pour-les-chaînes"></a>

Toutes ces fonctions sont déclarées dans **`<string.h>`**.

---

### **3.1. `strlen` : Longueur d'une Chaîne**
```c
size_t strlen(const char* str);
```
- Retourne le nombre de caractères **avant `\0`** (exclu).

#### **Exemple :**
```c
#include <stdio.h>
#include <string.h>

int main() {
    char str[] = "hello";
    printf("Longueur : %zu\n", strlen(str));  // 5
    return 0;
}
```

---

### **3.2. `strcpy` et `strncpy` : Copie de Chaînes**
#### **a) `strcpy` (copie non sécurisée)**
```c
char* strcpy(char* dest, const char* src);
```
- Copie `src` dans `dest` **jusqu'à `\0`**.
- **Danger** : Si `dest` est trop petit → **débordement de mémoire** (`buffer overflow`).

#### **Exemple :**
```c
char src[] = "hello";
char dest[6];  // 5 caractères + \0
strcpy(dest, src);
printf("%s\n", dest);  // "hello"
```

#### **b) `strncpy` (copie sécurisée)**
```c
char* strncpy(char* dest, const char* src, size_t n);
```
- Copie **au maximum `n` caractères** de `src` vers `dest`.
- **Si `src` est plus long que `n`** → `dest` **n'est pas terminé par `\0`** (à ajouter manuellement).

#### **Exemple :**
```c
char src[] = "hello world";
char dest[6];
strncpy(dest, src, 5);
dest[5] = '\0';  // Ajout manuel du \0
printf("%s\n", dest);  // "hello"
```

---

### **3.3. `strcat` et `strncat` : Concaténation**
#### **a) `strcat` (concaténation non sécurisée)**
```c
char* strcat(char* dest, const char* src);
```
- Ajoute `src` à la fin de `dest`.
- **Danger** : Si `dest` n'a pas assez de place → **débordement**.

#### **Exemple :**
```c
char dest[11] = "hello";
strcat(dest, " world");
printf("%s\n", dest);  // "hello world"
```

#### **b) `strncat` (concaténation sécurisée)**
```c
char* strncat(char* dest, const char* src, size_t n);
```
- Ajoute **au maximum `n` caractères** de `src` à `dest`.
- **Ajoute toujours `\0`** à la fin.

#### **Exemple :**
```c
char dest[11] = "hello";
strncat(dest, " world!", 5);  // Ajoute seulement " worl"
printf("%s\n", dest);  // "hello worl"
```

---

### **3.4. `strcmp` et `strncmp` : Comparaison**
#### **a) `strcmp`**
```c
int strcmp(const char* s1, const char* s2);
```
- Retourne :
  - `0` si `s1 == s2`
  - `< 0` si `s1 < s2` (ordre lexicographique)
  - `> 0` si `s1 > s2`

#### **Exemple :**
```c
printf("%d\n", strcmp("apple", "banana"));  // < 0 ("apple" vient avant "banana")
printf("%d\n", strcmp("hello", "hello"));   // 0
```

#### **b) `strncmp`**
```c
int strncmp(const char* s1, const char* s2, size_t n);
```
- Compare **au maximum `n` caractères**.

#### **Exemple :**
```c
printf("%d\n", strncmp("hello", "helloworld", 5));  // 0 (5 premiers caractères identiques)
```

---

---

## **4. Fonctions Avancées** <a name="4-fonctions-avancées"></a>

---

### **4.1. `strchr` et `strrchr` : Recherche de Caractère**
#### **a) `strchr` (première occurrence)**
```c
char* strchr(const char* s, int c);
```
- Retourne un **pointeur vers la première occurrence** de `c` dans `s`.
- Retourne `NULL` si non trouvé.

#### **Exemple :**
```c
char str[] = "hello";
char* ptr = strchr(str, 'l');
if (ptr) {
    printf("Trouvé à l'index : %ld\n", ptr - str);  // 2
}
```

#### **b) `strrchr` (dernière occurrence)**
```c
char* strrchr(const char* s, int c);
```
- Retourne un **pointeur vers la dernière occurrence** de `c`.

#### **Exemple :**
```c
char* ptr = strrchr(str, 'l');
printf("Dernière occurrence à l'index : %ld\n", ptr - str);  // 3
```

---

### **4.2. `strstr` : Recherche de Sous-Chaîne**
```c
char* strstr(const char* haystack, const char* needle);
```
- Cherche la **première occurrence** de `needle` dans `haystack`.
- Retourne un **pointeur vers le début de la sous-chaîne** ou `NULL`.

#### **Exemple :**
```c
char text[] = "hello world";
char* ptr = strstr(text, "world");
if (ptr) {
    printf("Trouvé : %s\n", ptr);  // "world"
}
```

---

### **4.3. `sprintf` et `snprintf` : Formatage dans une Chaîne**
#### **a) `sprintf` (non sécurisé)**
```c
int sprintf(char* str, const char* format, ...);
```
- Écrit des données formatées dans `str`.
- **Danger** : Pas de vérification de la taille de `str` → risque de débordement.

#### **Exemple :**
```c
char buffer[50];
int age = 25;
sprintf(buffer, "J'ai %d ans", age);
printf("%s\n", buffer);  // "J'ai 25 ans"
```

#### **b) `snprintf` (sécurisé)**
```c
int snprintf(char* str, size_t size, const char* format, ...);
```
- Écrit **au maximum `size-1` caractères** (ajoute toujours `\0`).

#### **Exemple :**
```c
char buffer[10];
snprintf(buffer, sizeof(buffer), "J'ai %d ans", 25);
printf("%s\n", buffer);  // "J'ai 25 a" (tronqué pour éviter le débordement)
```

---

### **4.4. `sscanf` : Analyse d'une Chaîne**
```c
int sscanf(const char* str, const char* format, ...);
```
- Lit des données formatées depuis une chaîne (comme `scanf` mais depuis une `string`).

#### **Exemple :**
```c
char data[] = "Jean 25";
char nom[50];
int age;
sscanf(data, "%s %d", nom, &age);
printf("Nom : %s, Âge : %d\n", nom, age);  // "Nom : Jean, Âge : 25"
```

---

---

## **5. Manipulation Sécurisée** <a name="5-manipulation-sécurisée"></a>

### **5.1. Risques de Débordement (`Buffer Overflow`)**
- **Problème** : Écrire au-delà de la taille allouée → corruption mémoire, plantage, failles de sécurité.
- **Exemple dangereux :**
  ```c
  char buffer[5];
  strcpy(buffer, "trop long!");  // ❌ Débordement !
  ```

### **5.2. Bonnes Pratiques**
1. **Toujours vérifier les tailles** avant d'utiliser `strcpy`, `strcat`, `sprintf`.
2. **Préférer les versions sécurisées** :
   - `strncpy` → `strcpy`
   - `strncat` → `strcat`
   - `snprintf` → `sprintf`
3. **Allouer dynamiquement** si la taille est variable :
   ```c
   char* str = malloc(strlen(src) + 1);
   strcpy(str, src);
   // ...
   free(str);
   ```
4. **Utiliser des bibliothèques modernes** (si possible) comme `<stdbool.h>` ou des wrappers sécurisés.

---

---

## **6. Exemples Complets** <a name="6-exemples-complets"></a>

---

### **6.1. Copie et Concaténation Sécurisées**
```c
#include <stdio.h>
#include <string.h>

int main() {
    char src[50] = "bonjour";
    char dest[50] = "hello ";

    // Copie sécurisée
    strncpy(dest, src, sizeof(dest) - 1);
    dest[sizeof(dest) - 1] = '\0';  // Garantit la terminaison
    printf("Après copie : %s\n", dest);

    // Concaténation sécurisée
    strncat(dest, " tout le monde!", sizeof(dest) - strlen(dest) - 1);
    printf("Après concaténation : %s\n", dest);

    return 0;
}
```

---

### **6.2. Comparaison de Chaînes**
```c
#include <stdio.h>
#include <string.h>

int main() {
    char s1[] = "apple";
    char s2[] = "banana";

    int result = strcmp(s1, s2);
    if (result < 0) {
        printf("%s vient avant %s\n", s1, s2);
    } else if (result > 0) {
        printf("%s vient après %s\n", s1, s2);
    } else {
        printf("Les chaînes sont identiques\n");
    }

    return 0;
}
```

---

### **6.3. Parsing d'une Chaîne avec `sscanf`**
```c
#include <stdio.h>

int main() {
    char data[] = "Jean Dupont 30 Paris";
    char prenom[50], nom[50], ville[50];
    int age;

    sscanf(data, "%s %s %d %s", prenom, nom, &age, ville);
    printf("Prénom : %s\n", prenom);
    printf("Nom : %s\n", nom);
    printf("Âge : %d\n", age);
    printf("Ville : %s\n", ville);

    return 0;
}
```
**Sortie :**
```
Prénom : Jean
Nom : Dupont
Âge : 30
Ville : Paris
```

---

### **6.4. Recherche de Sous-Chaîne**
```c
#include <stdio.h>
#include <string.h>

int main() {
    char text[] = "La programmation en C est puissante";
    char* mot = strstr(text, "C");

    if (mot) {
        printf("Trouvé à la position : %ld\n", mot - text);
        printf("Sous-chaîne : %s\n", mot);
    } else {
        printf("Mot non trouvé\n");
    }

    return 0;
}
```
**Sortie :**
```
Trouvé à la position : 20
Sous-chaîne : C est puissante
```

---

---

## **7. Exercices Corrigés** <a name="7-exercices-corrigés"></a>

---

### **Exercice 1 : Inverser une Chaîne**
**Énoncé :** Écrire une fonction qui inverse une chaîne de caractères.

#### **Solution :**
```c
#include <stdio.h>
#include <string.h>

void inverser(char* str) {
    int len = strlen(str);
    for (int i = 0; i < len / 2; i++) {
        char temp = str[i];
        str[i] = str[len - 1 - i];
        str[len - 1 - i] = temp;
    }
}

int main() {
    char str[] = "hello";
    inverser(str);
    printf("Inversé : %s\n", str);  // "olleh"
    return 0;
}
```

---

### **Exercice 2 : Compter les Mots**
**Énoncé :** Écrire une fonction qui compte le nombre de mots dans une chaîne (les mots sont séparés par des espaces).

#### **Solution :**
```c
#include <stdio.h>
#include <stdbool.h>

int compter_mots(const char* str) {
    int count = 0;
    bool dans_mot = false;

    for (int i = 0; str[i] != '\0'; i++) {
        if (str[i] != ' ') {
            if (!dans_mot) {
                count++;
                dans_mot = true;
            }
        } else {
            dans_mot = false;
        }
    }
    return count;
}

int main() {
    char texte[] = "Bonjour tout le monde";
    printf("Nombre de mots : %d\n", compter_mots(texte));  // 4
    return 0;
}
```

---

### **Exercice 3 : Remplacer un Caractère**
**Énoncé :** Écrire une fonction qui remplace toutes les occurrences d'un caractère dans une chaîne.

#### **Solution :**
```c
#include <stdio.h>

void remplacer(char* str, char ancien, char nouveau) {
    for (int i = 0; str[i] != '\0'; i++) {
        if (str[i] == ancien) {
            str[i] = nouveau;
        }
    }
}

int main() {
    char str[] = "pomme";
    remplacer(str, 'o', 'a');
    printf("Résultat : %s\n", str);  // "pamme"
    return 0;
}
```

---

### **Exercice 4 : Vérifier si une Chaîne est un Palindrome**
**Énoncé :** Écrire une fonction qui vérifie si une chaîne est un palindrome (se lit pareil dans les deux sens).

#### **Solution :**
```c
#include <stdio.h>
#include <string.h>
#include <stdbool.h>

bool est_palindrome(const char* str) {
    int len = strlen(str);
    for (int i = 0; i < len / 2; i++) {
        if (str[i] != str[len - 1 - i]) {
            return false;
        }
    }
    return true;
}

int main() {
    char str1[] = "radar";
    char str2[] = "hello";

    printf("%s est un palindrome : %s\n", str1, est_palindrome(str1) ? "oui" : "non");
    printf("%s est un palindrome : %s\n", str2, est_palindrome(str2) ? "oui" : "non");

    return 0;
}
```
**Sortie :**
```
radar est un palindrome : oui
hello est un palindrome : non
```

---

---

## **8. Annexes : Tableau Récapitulatif des Fonctions**

| Fonction | Description | Exemple |
|----------|-------------|---------|
| `strlen` | Longueur d'une chaîne | `strlen("hello")` → `5` |
| `strcpy` | Copie une chaîne | `strcpy(dest, "hello")` |
| `strncpy` | Copie sécurisée | `strncpy(dest, "hello", 5)` |
| `strcat` | Concatène deux chaînes | `strcat(dest, " world")` |
| `strncat` | Concatène sécurisée | `strncat(dest, " world", 5)` |
| `strcmp` | Compare deux chaînes | `strcmp("a", "b")` → `< 0` |
| `strncmp` | Compare `n` caractères | `strncmp("hello", "hell", 4)` → `0` |
| `strchr` | Cherche un caractère | `strchr("hello", 'e')` → pointeur vers 'e' |
| `strrchr` | Cherche la dernière occurrence | `strrchr("hello", 'l')` → pointeur vers le dernier 'l' |
| `strstr` | Cherche une sous-chaîne | `strstr("hello world", "world")` → pointeur vers "world" |
| `sprintf` | Formate dans une chaîne | `sprintf(buf, "Age: %d", 25)` |
| `snprintf` | Formate sécurisé | `snprintf(buf, 10, "Age: %d", 25)` |
| `sscanf` | Lit depuis une chaîne | `sscanf("Jean 25", "%s %d", nom, &age)` |

---

[...retorn en rèire](../menu.md)