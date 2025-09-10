# **Compilation C/C++ : Des Sources à l'Exécutable**

[...retorn en rèire](../menu.md)

---

## **📌 Table des Matières**
1. **[Le Problème Fondamental : LP → LM](#probleme)**
2. **[Chaîne de Production : Du Source à l'Exécutable](#chaine)**
   - 2.1. [Préprocesseur (`cpp`)](#preprocesseur)
   - 2.2. [Compilation (`cc1plus`/`cc1`)](#compilation)
     - 2.2.1. [Analyse Lexicale](#lexical)
     - 2.2.2. [Analyse Syntaxique](#syntaxique)
     - 2.2.3. [Analyse Sémantique](#semantique)
     - 2.2.4. [Optimisation & Génération de Code](#optimisation)
   - 2.3. [Assemblage (`as`)](#assemblage)
   - 2.4. [Édition de Liens (`ld`)](#linking)
3. **[Interprétation vs Compilation](#interpretation)**
4. **[Options GCC/Clang et Suffixes de Fichiers](#options)**
5. **[Erreurs Sémantiques Approfondies](#erreurs-semantiques)**
6. **[Erreurs de Linkage Détaillées](#erreurs-linking)**
7. **[Exemple Complet avec Analyse](#exemple-complet)**
8. **[Outils d'Inspection Avancés](#outils)**

---

## **1️⃣ Le Problème Fondamental : LP → LM** <a name="probleme"></a>

### **🔹 Contexte**
- **Langage de Programmation (LP)** :
  - Compréhensible par les humains (C, C++, Python, etc.).
  - Abstraction élevée (boucles, fonctions, objets).
  - **Exemple** : `int x = 5 + 3;`

- **Langage Machine (LM)** :
  - Compréhensible par le processeur (binaire, instructions assembleur).
  - Bas niveau (registres, adresses mémoire, opérations arithmétiques).
  - **Exemple** (x86) :
    ```asm
    mov eax, 5
    add eax, 3
    mov [x], eax
    ```

### **🔹 Le Défi**
**Traduire un programme LP → LM** tout en :
✅ **Préservant la sémantique** (le programme doit faire la même chose).
✅ **Optimisant les performances** (code machine efficace).
✅ **Gérant les dépendances** (bibliothèques, fichiers multiples).

### **🔹 Deux Approches**
| **Compilation**                          | **Interprétation**                          |
|------------------------------------------|---------------------------------------------|
| Traduction **avant exécution** (→ binaire). | Traduction **pendant l'exécution** (→ pas de binaire). |
| **Avantages** : Rapide à l'exécution.     | **Avantages** : Portabilité, débogage facile. |
| **Inconvénients** : Compilation lente.     | **Inconvénients** : Lent à l'exécution.      |
| Ex: C, C++, Rust.                         | Ex: Python, JavaScript, PHP.                |

---

## **2️⃣ Chaîne de Production : Du Source à l'Exécutable** <a name="chaine"></a>

### **📌 Schéma Global**
```
Texte Source (C1.cpp, TC1.cpp)
       │
       ▼
[1] Préprocesseur (cpp) ────► Texte Source Modifié (.i/.ii)
       │
       ▼
[2] Compilation (cc1plus) ────► Code Assembleur (.s)
       │
       ▼
[3] Assemblage (as) ────────► Code Objet (.o, binaire translatable BT)
       │
       ▼
[4] Édition de Liens (ld) ───► Exécutable (.out, LM)
```
**Bibliothèques** (`.a`, `.so`) sont injectées lors du **linking**.

---

### **2.1 Préprocesseur (`cpp`)** <a name="preprocesseur"></a>

#### **🔹 Rôle**
- **Expansion des macros** (`#define`).
- **Inclusion de fichiers** (`#include`).
- **Compilation conditionnelle** (`#ifdef`, `#if`).
- **Suppression des commentaires** (`//`, `/* */`).

#### **🔹 Processus**
1. **Remplacement des `#include`** :
   - Copie le contenu du fichier inclus **à la place** de la directive.
   - **Exemple** :
     ```cpp
     #include <iostream>
     int main() { std::cout << "Hello"; }
     ```
     → Devient (après préprocessing) :
     ```cpp
     // Contenu de <iostream> (des centaines de lignes)
     // ...
     int main() { std::cout << "Hello"; }
     ```

2. **Expansion des macros** :
   - Remplace les macros par leur valeur.
   - **Exemple** :
     ```cpp
     #define MAX 100
     int x = MAX;
     ```
     → Devient :
     ```cpp
     int x = 100;
     ```

3. **Compilation conditionnelle** :
   - Garde ou exclut du code selon des conditions.
   - **Exemple** :
     ```cpp
     #ifdef DEBUG
     std::cout << "Debug mode\n";
     #endif
     ```
     → Si `DEBUG` n'est pas défini, la ligne est supprimée.

#### **🔹 Commande**
```bash
g++ -E C1.cpp -o C1.i  # Affiche le résultat après préprocessing
```

#### **🔹 Erreurs Courantes**
- **Fichier inclus introuvable** :
  ```bash
  fatal error: mon_fichier.h: No such file or directory
  ```
  **Solution** : Vérifier le chemin (`-I` pour ajouter un dossier d'inclusion).

- **Macro non définie** :
  ```cpp
  #ifdef UNDEFINED_MACRO
  // Code mort
  #endif
  ```
  **Solution** : Définir la macro (`-DUNDEFINED_MACRO`).

- **Commentaire non fermé** :
  ```cpp
  /* Commentaire non fermé
  int x = 5;  // Erreur : tout est commenté !
  ```

#### **🔹 Bonnes Pratiques**
- **Évitez les includes inutiles** (ralentit la compilation).
- **Utilisez des guards** (`#pragma once` ou `#ifndef HEADER_H`).
- **Préférez `const`/`constexpr` aux macros** (meilleure sécurité de type).

---

### **2.2 Compilation (`cc1plus` pour C++, `cc1` pour C)** <a name="compilation"></a>

#### **🔹 Rôle**
Transformer le **code source modifié** (`.i`/`.ii`) en **code assembleur** (`.s`).
**4 sous-étapes** :

---

##### **2.2.1 Analyse Lexicale** <a name="lexical"></a>

**🔹 Rôle** :
- Découper le code en **tokens** (unités lexicales).
- **Tokens** = mots-clés (`int`, `if`), identifiants (`x`, `maFonction`), opérateurs (`+`, `=`), littéraux (`42`, `"hello"`).

**🔹 Exemple** :
```cpp
int x = 42 + foo();
```
→ Tokens :
```
[int, x, =, 42, +, foo, (, ), ;]
```

**🔹 Erreurs** :
- **Caractère invalide** :
  ```cpp
  int x = 42£;  // '£' n'est pas un token valide
  ```
- **Littéral mal formé** :
  ```cpp
  int x = 0xZZZ;  // 'Z' n'est pas un chiffre hexadécimal
  ```

**🔹 Outils** :
- `g++ -fpreprocessed -dD -E C1.cpp` pour voir les tokens.

---

##### **2.2.2 Analyse Syntaxique** <a name="syntaxique"></a>

**🔹 Rôle** :
- Vérifier que les tokens **respectent la grammaire** du langage.
- Construire un **arbre syntaxique abstrait (AST)**.

**🔹 Exemple** :
```cpp
x = 42 + foo();
```
→ AST :
```
   =
  / \
 x   +
    / \
   42 foo()
```

**🔹 Erreurs** :
- **Parenthèse manquante** :
  ```cpp
  if (x == 5 { ... }  // Oublie de ')
  ```
- **Point-virgule manquant** :
  ```cpp
  int x = 5  // Oublie de ;
  ```
- **Mauvaise déclaration** :
  ```cpp
  int long = 5;  // 'long' est un mot-clé
  ```

**🔹 Outils** :
- `clang -Xclang -ast-dump -fsyntax-only C1.cpp` pour voir l'AST.

---

##### **2.2.3 Analyse Sémantique** <a name="semantique"></a>

**🔹 Rôle** :
- Vérifier la **cohérence des types**.
- Résoudre les **portées** (variables, fonctions).
- Appliquer les **règles du langage** (ex: `const`, surcharges).

**🔹 Exemples** :

| **Type d'Erreur**          | **Exemple**                          | **Solution**                          |
|----------------------------|--------------------------------------|---------------------------------------|
| Type incompatible           | `double x = "hello";`               | `const char* x = "hello";`            |
| Symbole non déclaré         | `cout << y;` (si `y` n'existe pas)   | Déclarer `y`.                         |
| Violation de `const`        | `const int x = 5; x = 10;`           | Enlever `const` ou ne pas modifier.   |
| Surcharge ambiguë (C++)    | `f(1)` avec `f(int)` et `f(double)` | Caster : `f(static_cast<int>(1))`.    |
| ODR Violation (C++)        | Variable globale dans un `.h`       | Utiliser `extern` ou `inline`.        |

**🔹 Cas Avancés (C++)** :
- **Templates** :
  ```cpp
  template<typename T>
  T max(T a, T b) { return a > b ? a : b; }

  max(1, 2.0);  // ERREUR : types incompatibles (int vs double)
  ```
  **Solution** : Forcer le type (`max<double>(1, 2.0)`).

- **Héritage et `virtual`** :
  ```cpp
  class Base { public: virtual void f() = 0; };
  class Derived : public Base { };
  // ERREUR : 'f' n'est pas implémentée
  ```

**🔹 Outils** :
- `g++ -fsyntax-only C1.cpp` pour vérifier la sémantique sans générer de code.

---

##### **2.2.4 Optimisation & Génération de Code** <a name="optimisation"></a>

**🔹 Rôle** :
- **Optimiser** l'AST (suppression de code mort, inlining, etc.).
- **Générer du code assembleur** (`.s`).

**🔹 Optimisations Courantes** :

| **Optimisation**          | **Exemple**                          | **Résultat**                          |
|---------------------------|--------------------------------------|---------------------------------------|
| **Inlining**              | `int square(int x) { return x*x; }` | Le code de `square` est inséré directement. |
| **Élimination de code mort** | `int x = 5; return 0;`          | `x` est supprimé.                     |
| **Vectorisation**         | Boucle sur un tableau               | Utilise des instructions SIMD (AVX).  |
| **Propagations de constantes** | `int x = 5 + 3;`          | Remplacé par `int x = 8;`.            |

**🔹 Niveaux d'Optimisation GCC** :
| **Option** | **Description**                          |
|------------|------------------------------------------|
| `-O0`      | Aucune optimisation (débogage).          |
| `-O1`      | Optimisations basiques.                 |
| `-O2`      | Optimisations agressives (recommandé).   |
| `-O3`      | Optimisations très agressives (risque de bug). |
| `-Os`      | Optimiser pour la taille.                |

**🔹 Commande pour voir l'assembleur** :
```bash
g++ -S -O2 C1.cpp -o C1.s  # Génère l'assembleur optimisé
```

**🔹 Exemple d'Optimisation** :
```cpp
int square(int x) { return x * x; }
int main() { return square(5); }
```
→ Après `-O2` :
```asm
mov eax, 25  ; Le compilateur a calculé 5*5 à la compilation !
ret
```

---

### **2.3 Assemblage (`as`)** <a name="assemblage"></a>

#### **🔹 Rôle**
- Transformer le **code assembleur** (`.s`) en **code objet** (`.o`, binaire translatable **BT**).
- **BT** = Binaire Translatable (format ELF sous Linux, PE sous Windows).

#### **🔹 Processus**
1. **Traduction des instructions assembleur** en **opcodes** (binaire).
2. **Résolution des adresses relatives** (ex: sauts, appels de fonctions).
3. **Création des sections** :
   - `.text` : Code exécutable.
   - `.data` : Variables initialisées.
   - `.bss` : Variables non initialisées (mises à 0 au démarrage).
   - `.rodata` : Constantes (ex: chaînes de caractères).

#### **🔹 Commande**
```bash
as C1.s -o C1.o  # ou directement : g++ -c C1.cpp -o C1.o
```

#### **🔹 Inspection du `.o`**
```bash
objdump -d C1.o  # Désassemble le code
nm C1.o          # Liste les symboles (fonctions/variables)
```

**Exemple de sortie `nm`** :
```
0000000000000000 T main    # 'T' = fonction définie dans le section text
0000000000000000 b x       # 'b' = variable non initialisée (bss)
0000000000000000 d y       # 'd' = variable initialisée (data)
```

#### **🔹 Erreurs Courantes**
- **Syntax error in assembly** :
  ```asm
  mov eax, ebx  ; OK en x86, mais pas en ARM
  ```
  **Solution** : Vérifier l'architecture cible (`-m32`, `-m64`).

---

### **2.4 Édition de Liens (`ld`)** <a name="linking"></a>

#### **🔹 Rôle**
- **Combiner plusieurs fichiers objets** (`.o`) en un **exécutable** (`.out`, `.exe`).
- **Résoudre les références externes** (appels de fonctions entre fichiers).
- **Ajouter les bibliothèques** (`libc`, `libstdc++`, etc.).

#### **🔹 Processus**
1. **Résolution des symboles** :
   - Pour chaque symbole non résolu dans un `.o`, le linker cherche une **définition** dans :
     - Les autres `.o`.
     - Les **bibliothèques statiques** (`.a`).
     - Les **bibliothèques dynamiques** (`.so`).
2. **Placement en mémoire** :
   - Détermine l'**adresse de chaque fonction/variable** dans l'exécutable final.
3. **Génération du binaire exécutable** (format ELF/PE).

#### **🔹 Commande**
```bash
ld C1.o TC1.o -lc -lstdc++ -o TC1  # -l pour les bibliothèques
# Ou plus simplement :
g++ C1.o TC1.o -o TC1
```

#### **🔹 Bibliothèques Courantes**
| **Bibliothèque** | **Description**               | **Option GCC** |
|------------------|-------------------------------|----------------|
| `libc`           | Fonctions C (`printf`, etc.)  | `-lc` (implicite) |
| `libstdc++`      | Standard C++ (`cout`, etc.)   | `-lstdc++` (implicite avec `g++`) |
| `libm`           | Fonctions math (`sin`, `cos`) | `-lm`          |

#### **🔹 Inspection de l'Exécutable**
```bash
ldd TC1          # Liste les dépendances dynamiques
objdump -x TC1   # Affiche les headers ELF
readelf -a TC1   # Détails avancés (sections, symboles)
```

#### **🔹 Erreurs Courantes**
| **Erreur**                     | **Cause**                          | **Solution**                          |
|--------------------------------|------------------------------------|---------------------------------------|
| `undefined reference to 'foo'` | `foo` déclarée mais pas définie.  | Définir `foo` dans un `.cpp`.        |
| `multiple definition of 'x'`   | `x` définie plusieurs fois.       | Utiliser `extern` ou `inline`.        |
| `cannot find -l<lib>`          | Bibliothèque manquante.            | Installer la lib (`-L` pour le chemin). |
| `relocation truncated to fit`  | Dépassement de mémoire.           | Compiler en 64 bits (`-m64`).        |

---

## **3️⃣ Interprétation vs Compilation** <a name="interpretation"></a>

| **Critère**               | **Compilation**                          | **Interprétation**                      |
|---------------------------|------------------------------------------|------------------------------------------|
| **Quand la traduction a lieu** | Avant l'exécution.               | Pendant l'exécution.                     |
| **Sortie**                | Binaire exécutable (`.out`, `.exe`).      | Pas de binaire (exécution directe).      |
| **Performance**           | ⚡ Très rapide (code natif).              | 🐢 Lent (traduction à la volée).           |
| **Portabilité**           | ❌ Dépend de l'architecture.             | ✅ Indépendant (si l'interpréteur existe).|
| **Débogage**              | Difficile (binaire).                     | Facile (code source disponible).         |
| **Exemples**              | C, C++, Rust.                            | Python, JavaScript, PHP.                |

**🔹 Schéma Comparatif** :
```
Compilation:
[Source] → (Compilateur) → [Binaire] → (Exécution)

Interprétation:
[Source] → (Interpréteur) → [Exécution directe]
```

---

## **4️⃣ Options GCC/Clang et Suffixes de Fichiers** <a name="options"></a>

### **🔹 Options de Compilation**
| **Option**       | **Description**                                  | **Exemple**                          |
|------------------|--------------------------------------------------|--------------------------------------|
| `-E`             | Arrêt après préprocessing.                       | `g++ -E C1.cpp > C1.i`              |
| `-S`             | Arrêt après compilation (génère `.s`).          | `g++ -S C1.cpp`                      |
| `-c`             | Arrêt après assemblage (génère `.o`).            | `g++ -c C1.cpp`                      |
| `-o <fichier>`   | Spécifie le fichier de sortie.                   | `g++ C1.cpp -o mon_programme`        |
| `-O<n>`          | Niveau d'optimisation (`n=0,1,2,3,s`).           | `g++ -O2 C1.cpp`                     |
| `-g`             | Ajoute des infos de débogage.                    | `g++ -g C1.cpp`                      |
| `-Wall`          | Active tous les warnings.                       | `g++ -Wall C1.cpp`                   |
| `-I<chemin>`     | Ajoute un dossier d'inclusion.                   | `g++ -I./includes C1.cpp`            |
| `-L<chemin>`     | Ajoute un dossier de bibliothèques.              | `g++ -L./libs C1.cpp -lmylib`        |
| `-l<lib>`        | Lie avec une bibliothèque.                       | `g++ C1.cpp -lm` (pour libmath)      |
| `-static`        | Lie statiquement (pas de `.so`).                 | `g++ -static C1.cpp`                 |
| `-shared`        | Génère une bibliothèque dynamique (`.so`).       | `g++ -shared -o libfoo.so foo.cpp`   |

### **🔹 Suffixes de Fichiers**
| **Suffixe** | **Type**                          | **Description**                          |
|-------------|-----------------------------------|------------------------------------------|
| `.c`        | Source C                          | Compilé comme du C.                      |
| `.cpp`      | Source C++                        | Compilé comme du C++.                    |
| `.h`        | Header C/C++                      | Inclus via `#include`, jamais compilé directement. |
| `.i`        | Source C après préprocessing     | Généré avec `gcc -E`.                   |
| `.ii`       | Source C++ après préprocessing   | Généré avec `g++ -E`.                   |
| `.s`        | Assembleur                        | Généré avec `gcc -S`.                   |
| `.o`        | Objet (binaire translatable)      | Généré avec `gcc -c`.                   |
| `.a`        | Bibliothèque statique             | Archive de `.o` (ex: `libfoo.a`).       |
| `.so`       | Bibliothèque dynamique            | Partagée (ex: `libfoo.so`).              |
| `.out`      | Exécutable (par défaut)           | Généré par `ld` ou `gcc` sans `-o`.     |

---

## **5️⃣ Erreurs Sémantiques Approfondies** <a name="erreurs-semantiques"></a>

### **🔹 1. Incompatibilité de Types**
**Exemple** :
```cpp
double x = 5.5;
int y = x;  // Warning : perte de précision
```
**Solution** : Caster explicitement (`static_cast<int>(x)`).

**Cas extrême** :
```cpp
int* ptr = reinterpret_cast<int*>(0x1000);  // Danger !
*ptr = 5;  // Segmentation fault (adresse invalide)
```

---

### **🔹 2. Portée et Masquage de Variables**
**Exemple** :
```cpp
int x = 5;
{
    int x = 10;  // Masque la variable externe
    std::cout << x;  // Affiche 10
}
std::cout << x;  // Affiche 5
```
**Solution** : Utiliser `::x` pour accéder à la variable globale.

---

### **🔹 3. Surcharges et Résolution de Fonctions (C++)**
**Exemple ambigu** :
```cpp
void f(int);
void f(double);
f(true);  // ERREUR : 'true' est un bool (convertible en int ET double)
```
**Solution** : Caster explicitement (`f(static_cast<int>(true))`).

---

### **🔹 4. Références et Pointeurs Pendants**
**Exemple** :
```cpp
const int& getRef() {
    int x = 5;
    return x;  // ERREUR : x est détruit à la sortie
}
```
**Solution** : Retourner par valeur ou utiliser `static`.

---

### **🔹 5. Violation de l'ODR (One Definition Rule)**
**Exemple** :
```cpp
// Dans un header :
int counter = 0;  // ERREUR si inclus dans plusieurs fichiers
```
**Solution** :
```cpp
// Dans le header :
extern int counter;
// Dans UN .cpp :
int counter = 0;
```

---

### **🔹 6. Templates et Instanciation**
**Exemple** :
```cpp
template<typename T>
T max(T a, T b) { return a > b ? a : b; }

max(1, 2.0);  // ERREUR : types incompatibles
```
**Solution** : Forcer le type (`max<double>(1, 2.0)`).

---

### **🔹 7. Héritage et Fonctions Virtuelles**
**Exemple** :
```cpp
class Base { public: virtual void f() = 0; };
class Derived : public Base { };  // ERREUR : 'f' non implémentée
```
**Solution** : Implémenter `f` dans `Derived`.

---

### **🔹 8. Conversions Implicites Dangereuses**
**Exemple** :
```cpp
std::string s = "hello";
char* ptr = s.c_str();
ptr[0] = 'H';  // UB : s.c_str() retourne un const char*
```
**Solution** : Utiliser `std::string` correctement (pas de modification via `c_str()`).

---

## **6️⃣ Erreurs de Linkage Détaillées** <a name="erreurs-linking"></a>

### **🔴 1. Symbole Non Défini (`undefined reference`)**
**Cause** : Une fonction/variable est **déclarée mais pas définie**.
**Exemple** :
```cpp
// C1.cpp
void foo();  // Déclaration
int main() { foo(); }

// TC1.cpp
// Oublie de définir foo()
```
**Solution** : Définir `foo` dans un `.cpp` et compiler les deux fichiers :
```bash
g++ C1.cpp TC1.cpp -o TC1
```

---

### **🔴 2. Définition Multiple (`multiple definition`)**
**Cause** : Un symbole est **définis plusieurs fois** (ex: variable globale dans un header).
**Exemple** :
```cpp
// mon_header.h
int x = 0;  // ERREUR si inclus dans 2 fichiers
```
**Solution** :
```cpp
// mon_header.h
extern int x;
// mon_file.cpp
int x = 0;
```

---

### **🔴 3. Bibliothèque Manquante**
**Cause** : Oublie de lier une bibliothèque (ex: `-lm` pour `sin`).
**Exemple** :
```cpp
#include <cmath>
int main() { return sin(1.0); }
```
**Erreur** :
```
undefined reference to `sin'
```
**Solution** :
```bash
g++ C1.cpp -lm -o TC1
```

---

### **🔴 4. Incompatibilité ABI (C vs C++)**
**Cause** : Mélanger du code C et C++ sans `extern "C"`.
**Exemple** :
```c
// fichier.c
void foo() {}
```
```cpp
// fichier.cpp
void foo();  // Le linker cherche _Z3foov (name mangling)
```
**Solution** :
```cpp
// Dans le header :
#ifdef __cplusplus
extern "C" {
#endif
void foo();
#ifdef __cplusplus
}
#endif
```

---

### **🔴 5. Problèmes de Name Mangling (C++)**
**Cause** : Le compilateur décore les noms pour les surcharges.
**Exemple** :
```cpp
void f(int);
void f(double);
// Le linker voit _Z1fi et _Z1fd
```
**Solution** : Vérifier que les déclarations/définitions correspondent.

---

### **🔴 6. Erreurs de Liens Dynamiques**
**Cause** : Bibliothèque dynamique (`.so`) introuvable à l'exécution.
**Exemple** :
```bash
./mon_programme: error while loading shared libraries: libfoo.so: cannot open shared object file
```
**Solution** :
- Vérifier `LD_LIBRARY_PATH` :
  ```bash
  export LD_LIBRARY_PATH=/chemin/vers/la/lib:$LD_LIBRARY_PATH
  ```
- Utiliser `ldd` pour diagnostiquer :
  ```bash
  ldd mon_programme
  ```

---

## **7️⃣ Exemple Complet avec Analyse** <a name="exemple-complet"></a>

### **📄 Fichiers Sources**
```cpp
// C1.cpp
#include <iostream>
extern void foo();  // Déclarée dans TC1.cpp
int main() {
    foo();
    std::cout << "Hello\n";
    return 0;
}
```
```cpp
// TC1.cpp
#include <cmath>  // Nécessite -lm
void foo() {
    double x = sin(1.0);
    std::cout << "sin(1) = " << x << "\n";
}
```

### **🛠️ Compilation Paso à Paso**
```bash
# 1. Précompilation (optionnel)
g++ -E C1.cpp -o C1.i
g++ -E TC1.cpp -o TC1.i

# 2. Compilation → Objets
g++ -c C1.cpp -o C1.o
g++ -c TC1.cpp -o TC1.o

# 3. Linking (avec lib math)
g++ C1.o TC1.o -lm -o TC1

# 4. Exécution
./TC1
```
**Sortie attendue** :
```
sin(1) = 0.841471
Hello
```

### **🔍 Analyse des Fichiers Générés**
1. **Fichier `.i` (après préprocessing)** :
   - Contient le code avec tous les `#include` développés.
   - Exemple : `C1.i` inclut tout `<iostream>`.

2. **Fichier `.o` (objet)** :
   ```bash
   nm C1.o
   ```
   Sortie :
   ```
   U foo          # 'U' = symbole non résolu (undefined)
   00000000 T main # 'T' = fonction définie dans la section text
   ```

3. **Exécutable final** :
   ```bash
   ldd TC1
   ```
   Sortie :
   ```
   linux-vdso.so.1
   libstdc++.so.6
   libm.so.6      # Bibliothèque math
   libc.so.6
   libgcc_s.so.1
   ```

---

## **8️⃣ Outils d'Inspection Avancés** <a name="outils"></a>

| **Outil**       | **Description**                                  | **Exemple**                          |
|-----------------|--------------------------------------------------|--------------------------------------|
| `gcc -###`     | Affiche les commandes internes de GCC.          | `gcc -### C1.cpp`                   |
| `objdump`      | Désassemble un binaire.                          | `objdump -d TC1`                    |
| `readelf`      | Affiche les headers ELF.                         | `readelf -a TC1`                    |
| `nm`           | Liste les symboles d'un `.o` ou exécutable.     | `nm C1.o`                           |
| `ldd`          | Liste les dépendances dynamiques.                | `ldd TC1`                           |
| `strace`       | Trace les appels système.                        | `strace ./TC1`                      |
| `gdb`          | Débogueur.                                       | `gdb ./TC1`                         |
| `valgrind`     | Détecte les fuites mémoire.                      | `valgrind ./TC1`                    |
| `addr2line`    | Traduit une adresse en ligne de code.           | `addr2line -e TC1 0x4005a0`         |

---

[...retorn en rèire](../menu.md)