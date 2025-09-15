# **Types, Tailles, Bases et Conversions en C**

[...retorn en rèire](../menu.md)
---
## **Sommaire**
1. **[Introduction aux Types Primitifs en C](#1-introduction-aux-types-primitifs-en-c)**
   - Types entiers (`char`, `int`, `short`, `long`, `long long`)
   - Types réels (`float`, `double`, `long double`)
   - Types booléens (`_Bool`, `stdbool.h`)

2. **[Représentation Binaire, Octale et Hexadécimale](#2-représentation-binaire-octale-et-hexadécimale)**
   - Binaire (base 2)
   - Octale (base 8)
   - Hexadécimale (base 16)
   - Conversions entre bases

3. **[Tailles des Types et Mémoire](#3-tailles-des-types-et-mémoire)**
   - Taille en octets (`sizeof`)
   - Différences entre systèmes 32 bits et 64 bits
   - Tableau récapitulatif des tailles

4. **[Valeurs Minimales et Maximales](#4-valeurs-minimales-et-maximales)**
   - Entiers signés et non signés
   - Réels (`float`, `double`)
   - Fichier `limits.h` et `float.h`

5. **[Affichage des Valeurs avec `printf`](#5-affichage-des-valeurs-avec-printf)**
   - Formats d'affichage (`%d`, `%u`, `%x`, `%f`, `%e`, etc.)
   - Exemples concrets

6. **[Conversions de Types (`cast`)](#6-conversions-de-types-cast)**
   - Conversion implicite et explicite
   - Perte de précision
   - Exemple de programme

7. **[Exercices Corrigés](#7-exercices-corrigés)**
   - Conversion décimal ↔ binaire/octal/hexadécimal
   - Affichage des tailles et valeurs min/max
   - Programme de conversion de types

---

---

## **1. Introduction aux Types Primitifs en C** <a name="1-introduction-aux-types-primitifs-en-c"></a>

### **1.1. Types Entiers**
| Type | Signé ? | Taille (octets) | Plage de valeurs (32 bits) | Plage de valeurs (64 bits) |
|------|---------|----------------|---------------------------|---------------------------|
| `char` | Signé ou non signé (dépend du compilateur) | 1 | -128 à 127 ou 0 à 255 | Idem |
| `unsigned char` | Non signé | 1 | 0 à 255 | Idem |
| `short` | Signé | 2 | -32 768 à 32 767 | Idem |
| `unsigned short` | Non signé | 2 | 0 à 65 535 | Idem |
| `int` | Signé | 4 | -2 147 483 648 à 2 147 483 647 | Idem (sauf LP64 où `int` reste 4 octets) |
| `unsigned int` | Non signé | 4 | 0 à 4 294 967 295 | Idem |
| `long` | Signé | 4 (LP32) ou 8 (LP64) | -2 147 483 648 à 2 147 483 647 (LP32) ou -9 223 372 036 854 775 808 à 9 223 372 036 854 775 807 (LP64) | Idem |
| `unsigned long` | Non signé | 4 ou 8 | 0 à 4 294 967 295 (LP32) ou 0 à 18 446 744 073 709 551 615 (LP64) | Idem |
| `long long` | Signé | 8 | -9 223 372 036 854 775 808 à 9 223 372 036 854 775 807 | Idem |
| `unsigned long long` | Non signé | 8 | 0 à 18 446 744 073 709 551 615 | Idem |

### **1.2. Types Réels**
| Type | Taille (octets) | Précision (chiffres significatifs) | Plage de valeurs |
|------|----------------|----------------------------------|------------------|
| `float` | 4 | ~7 | ±1.2e-38 à ±3.4e+38 |
| `double` | 8 | ~15 | ±2.3e-308 à ±1.7e+308 |
| `long double` | 8, 12 ou 16 (dépend du compilateur) | ~19+ | ±3.4e-4932 à ±1.1e+4932 |

### **1.3. Type Booléen**
- Pas de type booléen natif en C (avant C99).
- On utilise `_Bool` (C99) ou `bool` via `<stdbool.h>`.
  ```c
  #include <stdbool.h>
  bool variable = true; // ou false
  ```

---

---

## **2. Représentation Binaire, Octale et Hexadécimale** <a name="2-représentation-binaire-octale-et-hexadécimale"></a>

### **2.1. Bases Numériques**
| Base | Nom | Chiffres utilisés | Préfixe en C |
|------|-----|-------------------|--------------|
| 2 | Binaire | 0, 1 | `0b` (non standard, mais souvent supporté) |
| 8 | Octale | 0-7 | `0` (ex: `077`) |
| 10 | Décimal | 0-9 | Aucun |
| 16 | Hexadécimale | 0-9, A-F | `0x` (ex: `0x1A3`) |

### **2.2. Conversion entre Bases**
#### **a) Décimal → Binaire**
Méthode : Division successive par 2.
**Exemple : 10 (décimal) → Binaire**
```
10 ÷ 2 = 5 reste 0
5 ÷ 2 = 2 reste 1
2 ÷ 2 = 1 reste 0
1 ÷ 2 = 0 reste 1
```
→ Lire les restes de bas en haut : `1010`

#### **b) Décimal → Hexadécimal**
Méthode : Division successive par 16.
**Exemple : 255 (décimal) → Hexadécimal**
```
255 ÷ 16 = 15 reste 15 (F)
15 ÷ 16 = 0 reste 15 (F)
```
→ Lire les restes : `0xFF`

#### **c) Binaire → Hexadécimal**
Regrouper les bits par 4 (de droite à gauche) et convertir chaque groupe.
**Exemple : `11011110` → Hexadécimal**
```
1101 1110
D    E
```
→ `0xDE`

#### **d) Hexadécimal → Décimal**
Multiplier chaque chiffre par 16^position (en partant de 0 à droite).
**Exemple : `0x1A3` → Décimal**
```
1 × 16² + A(10) × 16¹ + 3 × 16⁰ = 256 + 160 + 3 = 419
```

### **2.3. Exercices de Conversion (QUESTION 2 et 3)**
#### **QUESTION 2 : Décimal → Décimal**
| Nombre | Méthode | Résultat |
|--------|---------|----------|
| `0777` (octal) | `0777 = 7×8² + 7×8¹ + 7×8⁰ = 7×64 + 7×8 + 7 = 448 + 56 + 7 = 511` | **511** |

#### **QUESTION 3 : Hexadécimal → Décimal**
| Nombre | Méthode | Résultat |
|--------|---------|----------|
| `100` (hex) | `1×16² + 0×16¹ + 0×16⁰ = 256 + 0 + 0 = 256` | **256** |
| `06401` (octal) | `6×8⁴ + 4×8³ + 0×8² + 1×8⁰ = 6×4096 + 4×512 + 0 + 1 = 24576 + 2048 + 1 = 26625` | **26625** |

---

---

## **3. Tailles des Types et Mémoire** <a name="3-tailles-des-types-et-mémoire"></a>

### **3.1. Opérateur `sizeof`**
- Donne la taille en octets d'un type ou d'une variable.
- **Exemple :**
  ```c
  #include <stdio.h>
  int main() {
      printf("Taille de int : %zu octets\n", sizeof(int));
      printf("Taille de double : %zu octets\n", sizeof(double));
      return 0;
  }
  ```

### **3.2. Modèles de Données (32 bits vs 64 bits)**
| Modèle | Système | `int` | `long` | `pointer` |
|--------|---------|-------|--------|-----------|
| **LP32** | Win32, Unix 32 bits | 4 | 4 | 4 |
| **ILP32** | Unix 32 bits (ancien) | 4 | 4 | 4 |
| **LP64** | Unix 64 bits (Linux, Mac OS X) | 4 | 8 | 8 |
| **LLP64** | Win64 | 4 | 4 | 8 |

### **3.3. Tableau Récapitulatif (QUESTION 5)**
| Type | LP32 (32 bits) | LP64 (64 bits) |
|------|---------------|---------------|
| `char` | 1 | 1 |
| `short` | 2 | 2 |
| `int` | 4 | 4 |
| `long` | 4 | 8 |
| `long long` | 8 | 8 |
| `float` | 4 | 4 |
| `double` | 8 | 8 |
| `long double` | 8 ou 12 | 16 |
| `pointer` | 4 | 8 |

---

---

## **4. Valeurs Minimales et Maximales** <a name="4-valeurs-minimales-et-maximales"></a>

### **4.1. Fichiers `<limits.h>` et `<float.h>`**
- `<limits.h>` : Constantes pour les entiers (`INT_MAX`, `CHAR_BIT`, etc.).
- `<float.h>` : Constantes pour les réels (`FLT_MAX`, `DBL_MIN`, etc.).

| Type | Valeur Minimale | Valeur Maximale | Constante (`limits.h`) |
|------|-----------------|-----------------|------------------------|
| `char` | `-128` ou `0` | `127` ou `255` | `CHAR_MIN`, `CHAR_MAX` |
| `unsigned char` | `0` | `255` | `UCHAR_MAX` |
| `int` | `-2147483648` | `2147483647` | `INT_MIN`, `INT_MAX` |
| `unsigned int` | `0` | `4294967295` | `UINT_MAX` |
| `long` | `-2147483648` (LP32) ou `-9223372036854775808` (LP64) | `2147483647` (LP32) ou `9223372036854775807` (LP64) | `LONG_MIN`, `LONG_MAX` |

### **4.2. Programme pour Afficher les Tailles et Plages (QUESTION 5 et 6)**
```c
#include <stdio.h>
#include <limits.h>
#include <float.h>

int main() {
    // Types entiers
    printf("Type\t\tTaille\tMin\t\t\tMax\n");
    printf("char\t\t%zu\t%d\t\t\t%d\n", sizeof(char), CHAR_MIN, CHAR_MAX);
    printf("unsigned char\t%zu\t%d\t\t\t%d\n", sizeof(unsigned char), 0, UCHAR_MAX);
    printf("int\t\t%zu\t%d\t\t\t%d\n", sizeof(int), INT_MIN, INT_MAX);
    printf("unsigned int\t%zu\t%d\t\t\t%u\n", sizeof(unsigned int), 0, UINT_MAX);
    printf("long\t\t%zu\t%ld\t\t\t%ld\n", sizeof(long), LONG_MIN, LONG_MAX);

    // Types réels
    printf("\nType\t\tTaille\tPrécision\tMin\t\t\tMax\n");
    printf("float\t\t%zu\t%d\t\t%e\t\t%e\n", sizeof(float), FLT_DIG, FLT_MIN, FLT_MAX);
    printf("double\t\t%zu\t%d\t\t%e\t\t%e\n", sizeof(double), DBL_DIG, DBL_MIN, DBL_MAX);

    return 0;
}
```

---

---

## **5. Affichage des Valeurs avec `printf`** <a name="5-affichage-des-valeurs-avec-printf"></a>

### **5.1. Formats de `printf`**
| Format | Type | Exemple |
|--------|------|---------|
| `%d` | `int` (décimal signé) | `printf("%d", -42);` → `-42` |
| `%u` | `unsigned int` (décimal non signé) | `printf("%u", 255);` → `255` |
| `%o` | `unsigned int` (octal) | `printf("%o", 65);` → `101` |
| `%x` | `unsigned int` (hexadécimal, minuscules) | `printf("%x", 255);` → `ff` |
| `%X` | `unsigned int` (hexadécimal, majuscules) | `printf("%X", 255);` → `FF` |
| `%f` | `float` ou `double` (décimal) | `printf("%f", 3.14);` → `3.140000` |
| `%e` | `float` ou `double` (notation scientifique) | `printf("%e", 392.65);` → `3.926500e+02` |
| `%g` | `float` ou `double` (le plus court entre `%f` et `%e`) | `printf("%g", 392.65);` → `392.65` |
| `%c` | `char` (caractère) | `printf("%c", 'A');` → `A` |
| `%s` | Chaîne de caractères | `printf("%s", "Hello");` → `Hello` |
| `%p` | Pointeur (adresse mémoire) | `printf("%p", &variable);` → `0x7ffd42...` |

### **5.2. Exemple Complet (QUESTION 4)**
```c
#include <stdio.h>

int main() {
    printf("1. %%c : \'%c\'\n", 'a');
    printf("2. %%d : %d\n", 65);
    printf("3. %%o : %o\n", 100);
    printf("4. %%x : %x\n", 100);
    printf("5. %%X : %X\n", 100);
    printf("6. %%#o : %#o\n", 100);
    printf("7. %%#x : %#x\n", 100);
    printf("8. %%#X : %#X\n", 100);
    printf("9. %%e : %e\n", 3.1416);
    printf("10. %%E : %E\n", 3.1416);
    return 0;
}
```
**Sortie :**
```
1. %c : 'a'
2. %d : 65
3. %o : 144
4. %x : 64
5. %X : 64
6. %#o : 0144
7. %#x : 0x64
8. %#X : 0X64
9. %e : 3.141600e+00
10. %E : 3.141600E+00
```

---

---

## **6. Conversions de Types (`cast`)** <a name="6-conversions-de-types-cast"></a>

### **6.1. Conversion Implicite vs Explicite**
- **Implicite** : Automatique (peut entraîner une perte de précision).
  ```c
  double x = 3.14;
  int y = x; // y = 3 (perte de la partie décimale)
  ```
- **Explicite** : Utilisation du `cast`.
  ```c
  double x = 3.14;
  int y = (int)x; // y = 3
  ```

### **6.2. Exemple de Programme (QUESTION 7)**
```c
#include <stdio.h>
#include <stdlib.h>

int main() {
    int i = 4;
    float f = (float)i;
    char c = 100;
    int dc = (int)c;

    printf("i = %d\n", i);
    printf("f = %f\n", f);
    printf("c = %c\n", c);
    printf("dc = %d\n", dc);

    return 0;
}
```
**Sortie :**
```
i = 4
f = 4.000000
c = d
dc = 100
```

---

---

## **7. Exercices Corrigés** <a name="7-exercices-corrigés"></a>

### **7.1. Conversion Décimal ↔ Binaire/Octal/Hexadécimal**
**Exercice :** Convertir `42` en binaire, octal et hexadécimal.
- **Binaire** : `42 ÷ 2 = 21 R0 → 21 ÷ 2 = 10 R1 → 10 ÷ 2 = 5 R0 → 5 ÷ 2 = 2 R1 → 2 ÷ 2 = 1 R0 → 1 ÷ 2 = 0 R1` → `101010`
- **Octal** : `42 ÷ 8 = 5 R2` → `52`
- **Hexadécimal** : `42 ÷ 16 = 2 R10 (A)` → `0x2A`

### **7.2. Programme pour Afficher Tailles et Plages (QUESTION 5 et 6)**
```c
#include <stdio.h>
#include <limits.h>
#include <float.h>

int main() {
    // Entiers signés
    printf("Type\t\tTaille\tMin\t\t\tMax\n");
    printf("char\t\t%zu\t%d\t\t\t%d\n", sizeof(char), CHAR_MIN, CHAR_MAX);
    printf("short\t\t%zu\t%d\t\t\t%d\n", sizeof(short), SHRT_MIN, SHRT_MAX);
    printf("int\t\t%zu\t%d\t\t\t%d\n", sizeof(int), INT_MIN, INT_MAX);
    printf("long\t\t%zu\t%ld\t\t\t%ld\n", sizeof(long), LONG_MIN, LONG_MAX);
    printf("long long\t%zu\t%lld\t\t%lld\n", sizeof(long long), LLONG_MIN, LLONG_MAX);

    // Entiers non signés
    printf("\nType\t\t\tTaille\tMin\tMax\n");
    printf("unsigned char\t\t%zu\t%d\t\t%u\n", sizeof(unsigned char), 0, UCHAR_MAX);
    printf("unsigned short\t\t%zu\t%d\t\t%u\n", sizeof(unsigned short), 0, USHRT_MAX);
    printf("unsigned int\t\t%zu\t%d\t\t%u\n", sizeof(unsigned int), 0, UINT_MAX);
    printf("unsigned long\t\t%zu\t%d\t\t%lu\n", sizeof(unsigned long), 0, ULONG_MAX);

    // Réels
    printf("\nType\t\tTaille\tPrécision\tMin\t\t\tMax\n");
    printf("float\t\t%zu\t%d\t\t%e\t\t%e\n", sizeof(float), FLT_DIG, FLT_MIN, FLT_MAX);
    printf("double\t\t%zu\t%d\t\t%e\t\t%e\n", sizeof(double), DBL_DIG, DBL_MIN, DBL_MAX);
    printf("long double\t%zu\t%d\t\t%Le\t\t%Le\n", sizeof(long double), LDBL_DIG, LDBL_MIN, LDBL_MAX);

    return 0;
}
```

### **7.3. Programme de Conversion de Types (QUESTION 7)**
```c
#include <stdio.h>

int main() {
    int i = 4;
    float f = (float)i;
    char c = 100;
    int dc = (int)c;

    printf("i = %d (taille: %zu octets)\n", i, sizeof(i));
    printf("f = %f (taille: %zu octets)\n", f, sizeof(f));
    printf("c = %c (code ASCII: %d, taille: %zu octets)\n", c, c, sizeof(c));
    printf("dc = %d (taille: %zu octets)\n", dc, sizeof(dc));

    return 0;
}
```

---

---
## **8. Annexes**
### **8.1. Table ASCII**
| Décimal | Caractère | Décimal | Caractère |
|---------|-----------|---------|-----------|
| 0 | NUL | 32 | (espace) |
| 48 | '0' | 65 | 'A' |
| 97 | 'a' | 90 | 'Z' |

**Lien utile :** [Table ASCII complète](http://www.asciitable.com)

### **8.2. Complément à Deux (Entiers Signés)**
- **Exemple :** Représentation de `-5` sur 4 bits.
  - `5` en binaire : `0101`
  - Inverser les bits : `1010`
  - Ajouter 1 : `1011` → `-5` en complément à deux.

---

[...retorn en rèire](../menu.md)