# **📘 Guide : `make`, `gdb` et `valgrind`**

[...retorn en rèire](../menu.md)

---

## **📌 Table des Matières**
1. **[`make` : Automatiser la Compilation](#make)**
   - 1.1 [Généralités et Principe](#make-generalites)
   - 1.2 [Structure d'un `Makefile`](#make-structure)
   - 1.3 [Variables et Directives](#make-variables)
   - 1.4 [Règles Implicites vs Explicites](#make-regles)
   - 1.5 [Exemple Complet avec Patterns](#make-exemple)
   - 1.6 [Cible de Nettoyage (`clean`)](#make-clean)
   - 1.7 [Makefiles Conditionnels](#make-conditionnel)
   - 1.8 [Inclusion de Fichiers (`include`)](#make-include)

2. **[`gdb` : Déboguer comme un Pro](#gdb)**
   - 2.1 [Présentation et Installation](#gdb-presentation)
   - 2.2 [Lancer `gdb` et Commandes de Base](#gdb-base)
   - 2.3 [Points d'Arrêt (`break`)](#gdb-break)
   - 2.4 [Exécuter Pas à Pas (`step`, `next`)](#gdb-step)
   - 2.5 [Inspecter les Variables (`print`, `list`)](#gdb-print)
   - 2.6 [Pile d'Appels (`backtrace`)](#gdb-backtrace)
   - 2.7 [Watchpoints (Surveillance de Variables)](#gdb-watch)
   - 2.8 [Débogage Post-Mortem (Fichiers `core`)](#gdb-core)
   - 2.9 [Débogage à Distance](#gdb-distant)

3. **[`valgrind` : Chasser les Fuites Mémoire](#valgrind)**
   - 3.1 [Présentation et Installation](#valgrind-presentation)
   - 3.2 [Utilisation de Base (`memcheck`)](#valgrind-base)
   - 3.3 [Analyse des Résultats](#valgrind-analyse)
   - 3.4 [Exemple Complet avec Erreurs](#valgrind-exemple)
   - 3.5 [Autres Outils (`cachegrind`, `callgrind`)](#valgrind-outils)

4. **[Exemple Global : Projet C++ avec `make`, `gdb` et `valgrind`](#exemple-global)**

---

# **1️⃣ `make` : Automatiser la Compilation** <a name="make"></a>

## **1.1 Généralités et Principe** <a name="make-generalites"></a>

### **🔹 Qu'est-ce que `make` ?**
- **Outil standardisé** (IEEE Standard 1003.2-1992) pour **automatiser la compilation**.
- **Fonctionnement** :
  - Compare les **dates de modification** des fichiers.
  - **Recompile uniquement ce qui a changé** (économie de temps).
  - Utilise un fichier de description : **`Makefile`**.

### **🔹 Pourquoi utiliser `make` ?**
✅ **Gain de temps** : Pas besoin de retaper les commandes `g++` à chaque modification.
✅ **Moins d'erreurs** : Les commandes sont écrites une fois dans le `Makefile`.
✅ **Portabilité** : Un `Makefile` bien écrit fonctionne sur tous les systèmes Unix.
✅ **Gestion des dépendances** : `make` sait quels fichiers recompiler si un `.h` change.

### **🔹 Principe de Fonctionnement**
1. **Lire le `Makefile`** pour connaître les **règles de dépendances**.
2. **Vérifier les dates** :
   - Si un **fichier source** est plus récent que sa **cible** (ex: `.o` ou exécutable), `make` le recompile.
3. **Exécuter les commandes** nécessaires pour mettre à jour la cible.

**Exemple** :
```
demo (exécutable) dépend de demo1.o et demo2.o
demo1.o dépend de demo1.cpp et demo1.h
demo2.o dépend de demo2.cpp et demo2.h
```
→ Si `demo1.cpp` est modifié, `make` recompile **uniquement** `demo1.o`, puis relink `demo`.

---

## **1.2 Structure d'un `Makefile`** <a name="make-structure"></a>

### **🔹 Syntaxe de Base**
- **Commentaires** : Commencent par `#`.
- **Variables** : Pour éviter la répétition.
  ```makefile
  CC = g++          # Définition
  $(CC) -o demo ... # Utilisation
  ```
- **Règles** : Définissent comment construire une cible.
  ```makefile
  cible: dependances
      commande1
      commande2
  ```
  > **⚠️ Important** : Les commandes **doivent commencer par une tabulation** (pas d'espaces !).

---

### **🔹 Exemple Minimal**
```makefile
# Compilation de demo à partir de demo1.cpp et demo2.cpp
demo: demo1.o demo2.o
	g++ -o demo demo1.o demo2.o -lm

demo1.o: demo1.cpp demo1.h
	g++ -c demo1.cpp

demo2.o: demo2.cpp demo2.h config.h
	g++ -c demo2.cpp
```

---

## **1.3 Variables et Directives** <a name="make-variables"></a>

### **🔹 Variables**
| **Syntaxe**       | **Exemple**                     | **Description**                          |
|-------------------|---------------------------------|------------------------------------------|
| `VAR = valeur`    | `CC = g++`                      | Définition simple.                       |
| `VAR := valeur`   | `SRCS := $(wildcard *.cpp)`     | Définition immédiate (pas d'expansion différée). |
| `VAR += valeur`   | `CFLAGS += -O2`                 | Ajoute à une variable existante.         |
| `$(VAR)`          | `$(CC) -o demo $(OBJS)`          | Utilisation d'une variable.              |

**Variables Spéciales** :
| **Variable** | **Description**                                  |
|--------------|--------------------------------------------------|
| `$@`         | Nom de la **cible** (ex: `demo` dans `demo: ...`). |
| `$<`         | Première **dépendance** (ex: `demo1.o`).        |
| `$^`         | Toutes les **dépendances**.                     |
| `$(wildcard *.cpp)` | Liste tous les `.cpp` du dossier.           |

**Exemple avec Variables** :
```makefile
CC = g++
CFLAGS = -c -Wall -O2
OBJS = demo1.o demo2.o
EXE = demo
LIBRARIES = -lm

$(EXE): $(OBJS)
	$(CC) -o $@ $^ $(LIBRARIES)

%.o: %.cpp
	$(CC) $(CFLAGS) $< -o $@
```

---

### **🔹 Directives**
| **Directive**       | **Exemple**                     | **Description**                          |
|---------------------|---------------------------------|------------------------------------------|
| `include`           | `include config.mk`            | Inclut un autre `Makefile`.              |
| `ifeq`/`ifneq`      | `ifeq ($(CC),g++)`              | Conditionnelle (si égal/différent).       |
| `ifdef`/`ifndef`    | `ifdef DEBUG`                   | Vérifie si une variable est définie.     |
| `define`/`endef`   | `define RUN_TESTS` ... `endef`  | Définit un bloc de commandes réutilisable. |

**Exemple Conditionnel** :
```makefile
CFLAGS = -O2
ifeq ($(DEBUG),1)
CFLAGS += -g
endif
```

---

## **1.4 Règles Implicites vs Explicites** <a name="make-regles"></a>

### **🔹 Règles Explicites**
- **Définies par l'utilisateur**.
- **Syntaxe** :
  ```makefile
  cible: dependances
      commandes
  ```

**Exemple** :
```makefile
demo: demo1.o demo2.o
	g++ -o demo demo1.o demo2.o -lm
```

---

### **🔹 Règles Implicites**
- **Prédéfini par `make`** pour des cas courants (ex: `.cpp` → `.o`).
- **Exemple** :
  ```makefile
  %.o: %.cpp
      $(CXX) $(CFLAGS) -c $< -o $@
  ```
  → `make` sait déjà comment compiler un `.cpp` en `.o`.

**Règles Implicites Courantes** :
| **Cible**       | **Dépendance** | **Commande par Défaut**          |
|-----------------|----------------|-----------------------------------|
| `%.o`           | `%.c`          | `cc -c $< -o $@`                  |
| `%.o`           | `%.cpp`        | `g++ -c $< -o $@`                 |
| `executable`    | `%.o`          | `$(CC) $^ -o $@`                  |

---

## **1.5 Exemple Complet avec Patterns** <a name="make-exemple"></a>

### **📄 Structure du Projet**
```
mon_projet/
├── src/
│   ├── demo1.cpp
│   ├── demo1.h
│   ├── demo2.cpp
│   ├── demo2.h
│   └── config.h
├── Makefile
└── demo (exécutable final)
```

### **📄 `Makefile` Optimisé**
```makefile
# Variables
CC      = g++
CFLAGS  = -Wall -O2
LDFLAGS = -lm
SRCS    = $(wildcard src/*.cpp)
OBJS    = $(SRCS:.cpp=.o)
EXE     = demo

# Règles
all: $(EXE)

$(EXE): $(OBJS)
	$(CC) $^ -o $@ $(LDFLAGS)

%.o: %.cpp
	$(CC) $(CFLAGS) -c $< -o $@

clean:
	rm -f $(OBJS) $(EXE)

.PHONY: all clean
```

### **🔹 Explications**
1. **`SRCS = $(wildcard src/*.cpp)`** :
   - Liste automatiquement tous les `.cpp` dans `src/`.
2. **`OBJS = $(SRCS:.cpp=.o)`** :
   - Remplace `.cpp` par `.o` (ex: `demo1.cpp` → `demo1.o`).
3. **`%.o: %.cpp`** :
   - Règle **générique** pour compiler n'importe quel `.cpp` en `.o`.
4. **`clean`** :
   - Supprime les fichiers générés (`.o` et exécutable).
5. **`.PHONY: all clean`** :
   - Indique que `all` et `clean` ne sont pas des fichiers (évite les conflits).

---

### **🔹 Utilisation**
```bash
make        # Compile tout (cible 'all' par défaut)
make clean  # Nettoie les fichiers générés
```

---

## **1.6 Cible de Nettoyage (`clean`)** <a name="make-clean"></a>

### **🔹 Pourquoi une Cible `clean` ?**
- Supprime les **fichiers générés** (`.o`, exécutables) pour :
  - **Libérer de l'espace**.
  - **Forcer une recompilation complète** (`make clean && make`).

### **🔹 Exemple**
```makefile
clean:
	rm -f $(OBJS) $(EXE)
```

> **⚠️ Attention** :
> - Toujours utiliser `rm -f` pour éviter les erreurs si le fichier n'existe pas.
> - **Ne jamais mettre `clean` comme dépendance d'une autre cible** (risque de suppression accidentelle).

---

## **1.7 Makefiles Conditionnels** <a name="make-conditionnel"></a>

### **🔹 Utilisation des Conditionnelles**
Permet d'adapter le `Makefile` à différents environnements (debug/release, OS, etc.).

**Exemple : Mode Debug/Release**
```makefile
# Choix du mode (passé via la ligne de commande : make DEBUG=1)
ifeq ($(DEBUG),1)
    CFLAGS += -g -O0
    EXE := demo_debug
else
    CFLAGS += -O2
    EXE := demo_release
endif

all: $(EXE)
```

**Utilisation** :
```bash
make DEBUG=1  # Compile en mode debug
make          # Compile en mode release
```

---

### **🔹 Autre Exemple : Détection de l'OS**
```makefile
UNAME := $(shell uname)
ifeq ($(UNAME),Linux)
    CFLAGS += -DLINUX
else ifeq ($(UNAME),Darwin)
    CFLAGS += -DMACOS
endif
```

---

## **1.8 Inclusion de Fichiers (`include`)** <a name="make-include"></a>

### **🔹 Pourquoi Inclure des Fichiers ?**
- **Modularité** : Séparer les configurations (ex: `config.mk`).
- **Réutilisabilité** : Partager des règles entre plusieurs `Makefile`.

### **🔹 Exemple**
```makefile
# Dans config.mk
CC = g++
CFLAGS = -Wall -O2

# Dans Makefile
include config.mk

all: demo
```

---

# **2️⃣ `gdb` : Déboguer comme un Pro** <a name="gdb"></a>

## **2.1 Présentation et Installation** <a name="gdb-presentation"></a>

### **🔹 Qu'est-ce que `gdb` ?**
- **Débogueur** standard du projet GNU (depuis 1986).
- **Fonctionnalités** :
  - Exécution pas à pas.
  - Inspection des variables.
  - Analyse de la pile d'appels.
  - Débogage à distance (embarqué).
  - Support multi-langages (C, C++, Rust, Go, etc.).

### **🔹 Installation**
```bash
# Ubuntu/Debian
sudo apt install gdb

# Fedora/RHEL
sudo dnf install gdb
```

### **🔹 Prérequis**
- **Compiler avec `-g`** pour inclure les **symboles de debug** :
  ```bash
  g++ -g -o demo demo.cpp
  ```

---

## **2.2 Lancer `gdb` et Commandes de Base** <a name="gdb-base"></a>

### **🔹 Démarrer `gdb`**
```bash
gdb ./demo
```

### **🔹 Commandes Essentielles**
| **Commande**       | **Abréviation** | **Description**                          |
|--------------------|-----------------|------------------------------------------|
| `quit`             | `q`             | Quitter `gdb`.                           |
| `run`              | `r`             | Lancer le programme.                     |
| `run arg1 arg2`    | `r arg1 arg2`   | Lancer avec des arguments.               |
| `help`             | `h`             | Affiche l'aide.                          |

**Exemple** :
```bash
(gdb) run
Starting program: /chemin/vers/demo
Hello World!
[Inferior 1 (process 1234) exited normally]
(gdb) quit
```

---

## **2.3 Points d'Arrêt (`break`)** <a name="gdb-break"></a>

### **🔹 Poser un Point d'Arrêt**
| **Commande**               | **Exemple**               | **Description**                          |
|----------------------------|---------------------------|------------------------------------------|
| `break <ligne>`            | `b 10`                   | Pose un breakpoint à la ligne 10.        |
| `break <fonction>`         | `b main`                  | Pose un breakpoint au début de `main`.   |
| `break <fichier>:<ligne>`  | `b demo.cpp:20`           | Pose un breakpoint dans un fichier spécifique. |
| `info break`               | `i b`                     | Liste tous les breakpoints.              |

**Exemple** :
```bash
(gdb) break main
Breakpoint 1 at 0x4005a6: file demo.cpp, line 5.
(gdb) run
Breakpoint 1, main () at demo.cpp:5
5       int x = 5;
```

---

### **🔹 Supprimer un Point d'Arrêt**
| **Commande**       | **Exemple**       | **Description**                  |
|---------------------|-------------------|----------------------------------|
| `delete <num>`     | `d 1`             | Supprime le breakpoint n°1.      |
| `clear <ligne>`     | `cl 10`           | Supprime le breakpoint à la ligne 10. |
| `disable <num>`    | `dis 1`           | Désactive temporairement un breakpoint. |

---

## **2.4 Exécuter Pas à Pas (`step`, `next`)** <a name="gdb-step"></a>

### **🔹 Commandes d'Exécution Pas à Pas**
| **Commande** | **Abréviation** | **Description**                          |
|--------------|-----------------|------------------------------------------|
| `step`       | `s`             | Exécute **une ligne** (entre dans les fonctions). |
| `next`       | `n`             | Exécute **une ligne** (sans entrer dans les fonctions). |
| `continue`   | `c`             | Reprend l'exécution jusqu'au prochain breakpoint. |
| `finish`     | `fin`           | Termine l'exécution de la fonction courante. |

**Exemple** :
```bash
(gdb) break main
(gdb) run
Breakpoint 1, main () at demo.cpp:5
5       int x = 5;
(gdb) next   # Passe à la ligne suivante
6       int y = foo(x);
(gdb) step   # Entre dans la fonction foo
foo (a=5) at demo.cpp:10
10      return a * 2;
```

---

## **2.5 Inspecter les Variables (`print`, `list`)** <a name="gdb-print"></a>

### **🔹 Afficher des Variables**
| **Commande**       | **Exemple**               | **Description**                          |
|--------------------|---------------------------|------------------------------------------|
| `print <var>`      | `p x`                     | Affiche la valeur de `x`.               |
| `print <expr>`     | `p x + y`                 | Évalue une expression.                  |
| `list`             | `l`                       | Affiche le code source autour du point courant. |
| `list <ligne>`     | `l 10`                    | Affiche le code autour de la ligne 10.  |

**Exemple** :
```bash
(gdb) print x
$1 = 5
(gdb) print y
$2 = 10
(gdb) list
5       int x = 5;
6       int y = foo(x);
7       std::cout << y << std::endl;
8       return 0;
9   }
```

---

## **2.6 Pile d'Appels (`backtrace`)** <a name="gdb-backtrace"></a>

### **🔹 Analyser la Pile d'Appels**
| **Commande**       | **Abréviation** | **Description**                          |
|--------------------|-----------------|------------------------------------------|
| `backtrace`         | `bt`            | Affiche la pile d'appels (qui a appelé quoi). |
| `frame <num>`      | `f 2`           | Sélectionne un cadre de la pile.         |
| `info locals`      | `i loc`         | Affiche les variables locales.          |

**Exemple** :
```cpp
int foo(int a) { return bar(a + 1); }
int bar(int b) { return b * 2; }
int main() { return foo(5); }
```
```bash
(gdb) break bar
(gdb) run
Breakpoint 1, bar (b=6) at demo.cpp:2
2       return b * 2;
(gdb) backtrace
#0  bar (b=6) at demo.cpp:2
#1  0x00000000004005b6 in foo (a=5) at demo.cpp:1
#2  0x00000000004005c6 in main () at demo.cpp:3
```

---

## **2.7 Watchpoints (Surveillance de Variables)** <a name="gdb-watch"></a>

### **🔹 Surveiller une Variable**
- **Arrête l'exécution quand une variable change**.
- **Commandes** :
  | **Commande**       | **Exemple**       | **Description**                  |
  |--------------------|-------------------|----------------------------------|
  | `watch <var>`      | `watch x`         | Surveille les modifications de `x`. |
  | `info watchpoints` | `i watch`         | Liste les watchpoints.          |

**Exemple** :
```bash
(gdb) watch x
Watchpoint 2: x
(gdb) continue
Watchpoint 2: x
Old value = 5
New value = 10
main () at demo.cpp:6
6       x = 10;
```

---

## **2.8 Débogage Post-Mortem (Fichiers `core`)** <a name="gdb-core"></a>

### **🔹 Qu'est-ce qu'un Fichier `core` ?**
- **Image mémoire** du programme au moment du **plantage** (segmentation fault, etc.).
- Généré si :
  - Le programme **crash**.
  - La limite `core` n'est pas à 0 (`ulimit -c unlimited`).

### **🔹 Utilisation**
```bash
ulimit -c unlimited  # Active la génération des core dumps
./demo               # Le programme crash et génère core
gdb ./demo core       # Débogue avec le core dump
```

**Exemple d'Analyse** :
```bash
(gdb) backtrace
#0  0x00000000004005a0 in foo () at demo.cpp:10
#1  0x00000000004005b6 in main () at demo.cpp:5
```
→ Montre **où** le programme a planté.

---

## **2.9 Débogage à Distance** <a name="gdb-distant"></a>

### **🔹 Pourquoi le Débogage à Distance ?**
- **Déboguer un programme sur une machine embarquée** (Raspberry Pi, microcontrôleur).
- **Scénarios** :
  - Le programme tourne sur un **serveur distant**.
  - Le code est exécuté sur un **appareil sans écran** (IoT).

### **🔹 Étapes**
1. **Sur la machine cible** (embarquée) :
   ```bash
   gdbserver :1234 ./demo
   ```
2. **Sur la machine hôte** (votre PC) :
   ```bash
   gdb ./demo
   (gdb) target remote <IP_cible>:1234
   (gdb) continue
   ```

---

# **3️⃣ `valgrind` : Chasser les Fuites Mémoire** <a name="valgrind"></a>

## **3.1 Présentation et Installation** <a name="valgrind-presentation"></a>

### **🔹 Qu'est-ce que `valgrind` ?**
- **Outil d'analyse dynamique** pour détecter :
  - **Fuites mémoire** (`memcheck`).
  - **Accès mémoire invalides** (déréférencement de `NULL`, buffer overflow).
  - **Problèmes de threads** (`helgrind`).
  - **Performances** (`cachegrind`, `callgrind`).

### **🔹 Installation**
```bash
# Ubuntu/Debian
sudo apt install valgrind

# Fedora/RHEL
sudo dnf install valgrind
```

### **🔹 Prérequis**
- **Compiler avec `-g`** pour avoir les **noms de fichiers/ligne**.
- **Désactiver les optimisations** (`-O0`) pour une analyse précise.

---

## **3.2 Utilisation de Base (`memcheck`)** <a name="valgrind-base"></a>

### **🔹 Commande de Base**
```bash
valgrind --leak-check=full ./demo
```

| **Option**               | **Description**                          |
|--------------------------|------------------------------------------|
| `--leak-check=full`      | Détaille **toutes** les fuites.          |
| `--track-origins=yes`    | Affiche l'origine des valeurs non initialisées. |
| `--verbose`              | Mode verbeux.                            |
| `--log-file=valgrind.log`| Redirige la sortie vers un fichier.     |

---

## **3.3 Analyse des Résultats** <a name="valgrind-analyse"></a>

### **🔹 Exemple de Sortie**
```bash
==12345== Memcheck, a memory error detector
==12345== Copyright (C) 2002-2022, and GNU GPL'd, by Julian Seward et al.
==12345== Using Valgrind-3.20.0 and LibVEX
==12345== Command: ./demo
==12345==

==12345== Invalid write of size 4
==12345==    at 0x4005A0: erreur (demo.c:10)
==12345==    by 0x4005C0: main (demo.c:17)
==12345==  Address 0x4a4d044 is 0 bytes after a block of size 20 alloc'd
==12345==    at 0x483B7F3: malloc (vg_replace_malloc.c:307)
==12345==    by 0x400596: erreur (demo.c:9)

==12345== LEAK SUMMARY:
==12345==    definitely lost: 20 bytes in 1 blocks
==12345==    indirectly lost: 0 bytes in 0 blocks
==12345==      possibly lost: 0 bytes in 0 blocks
==12345==    still reachable: 0 bytes in 0 blocks
==12345==         suppressed: 0 bytes in 0 blocks
```

### **🔹 Interprétation**
1. **`Invalid write of size 4`** :
   - **Problème** : Écriture en dehors d'un bloc alloué (`malloc`).
   - **Ligne** : `demo.c:10` dans la fonction `erreur`.
   - **Cause** : Dépassement de tableau (`pt[TAILLE]` alors que `malloc(TAILLE * sizeof(int))`).

2. **`definitely lost: 20 bytes`** :
   - **Problème** : Fuite mémoire (20 octets alloués mais jamais libérés).
   - **Solution** : Ajouter `free(pt)` avant de quitter la fonction.

---

## **3.4 Exemple Complet avec Erreurs** <a name="valgrind-exemple"></a>

### **📄 Code Source (`demo.c`)**
```c
#include <stdlib.h>
#define TAILLE 5

void erreur(void) {
    int *pt = (int *)malloc(TAILLE * sizeof(int));
    pt[TAILLE] = -1;  // ❌ Dépassement de tableau
    // ❌ Pas de free(pt) → fuite mémoire
}

int main(void) {
    erreur();
    return 0;
}
```

### **🔹 Compilation et Exécution**
```bash
gcc -g -O0 -o demo demo.c
valgrind --leak-check=full ./demo
```

### **🔹 Corrections**
```c
void erreur(void) {
    int *pt = (int *)malloc(TAILLE * sizeof(int));
    if (pt == NULL) exit(1);  // Vérification de malloc
    pt[TAILLE - 1] = -1;      // ✅ Accès valide
    free(pt);                 // ✅ Libération mémoire
}
```

---

## **3.5 Autres Outils (`cachegrind`, `callgrind`)** <a name="valgrind-outils"></a>

### **🔹 `cachegrind` : Analyser les Performances Cache**
```bash
valgrind --tool=cachegrind ./demo
```
- Génère un fichier `cachegrind.out.<pid>` analysable avec `cg_annotate`.

### **🔹 `callgrind` : Profiler les Appels de Fonctions**
```bash
valgrind --tool=callgrind ./demo
```
- Génère un fichier pour `kcachegrind` (outil graphique) :
  ```bash
  kcachegrind callgrind.out.<pid>
  ```

---

# **4️⃣ Exemple Global : Projet C++ avec `make`, `gdb` et `valgrind`** <a name="exemple-global"></a>

## **📄 Structure du Projet**
```
mon_projet/
├── src/
│   ├── main.cpp
│   ├── utils.cpp
│   ├── utils.h
│   └── config.h
├── Makefile
└── README.md
```

## **📄 `Makefile`**
```makefile
# Variables
CC      = g++
CFLAGS  = -g -Wall -O0  # -O0 pour valgrind
LDFLAGS =
SRCS    = $(wildcard src/*.cpp)
OBJS    = $(SRCS:src/%.cpp=%.o)
EXE     = demo

# Règles
all: $(EXE)

$(EXE): $(OBJS)
	$(CC) $^ -o $@ $(LDFLAGS)

%.o: src/%.cpp
	$(CC) $(CFLAGS) -c $< -o $@

clean:
	rm -f $(OBJS) $(EXE)

.PHONY: all clean
```

## **📄 Code Source (`main.cpp`)**
```cpp
#include "utils.h"
#include <iostream>

int main() {
    int *arr = createArray(5);
    printArray(arr, 5);
    delete[] arr;  // ✅ Pas de fuite mémoire
    return 0;
}
```

## **📄 `utils.cpp` (avec une erreur volontaire)**
```cpp
#include "utils.h"
#include <iostream>

int* createArray(int size) {
    int *arr = new int[size];
    for (int i = 0; i <= size; i++) {  // ❌ Dépassement (i <= size)
        arr[i] = i * 2;
    }
    return arr;
}

void printArray(int *arr, int size) {
    for (int i = 0; i < size; i++) {
        std::cout << arr[i] << " ";
    }
    std::cout << std::endl;
}
```

## **🔹 Étapes de Débogage**
1. **Compilation** :
   ```bash
   make
   ```
2. **Exécution avec `valgrind`** :
   ```bash
   valgrind --leak-check=full ./demo
   ```
   → Détecte le **dépassement de tableau** dans `createArray`.

3. **Correction** :
   ```cpp
   for (int i = 0; i < size; i++) {  // ✅ i < size
       arr[i] = i * 2;
   }
   ```

4. **Débogage avec `gdb`** :
   ```bash
   gdb ./demo
   (gdb) break createArray
   (gdb) run
   ```

5. **Vérification Finale** :
   ```bash
   valgrind --leak-check=full ./demo
   ```
   → Plus d'erreurs !

---

## **🎯 Résumé des Commandes Clés**
| **Outil**  | **Commande**                          | **Description**                          |
|------------|---------------------------------------|------------------------------------------|
| `make`     | `make`                                | Compile le projet.                       |
|            | `make clean`                          | Nettoie les fichiers générés.            |
| `gdb`      | `gdb ./demo`                          | Lance le débogueur.                      |
|            | `break main`                         | Pose un breakpoint.                      |
|            | `run`                                 | Lance le programme.                      |
|            | `next` / `step`                       | Exécution pas à pas.                     |
| `valgrind` | `valgrind --leak-check=full ./demo`   | Détecte les fuites mémoire.              |

---

## **💡 Bonnes Pratiques**
1. **Pour `make`** :
   - Utilisez des **variables** pour éviter la répétition.
   - Préférez les **règles génériques** (`%.o: %.cpp`).
   - Ajoutez toujours une cible `clean`.

2. **Pour `gdb`** :
   - Compilez **toujours avec `-g`**.
   - Utilisez `backtrace` pour comprendre les crashes.
   - Posez des **breakpoints stratégiques** (début de fonctions, boucles).

3. **Pour `valgrind`** :
   - Compilez avec `-g -O0` pour une analyse précise.
   - Corrigez **d'abord les erreurs "Invalid"** (accès mémoire), puis les fuites.
   - Utilisez `--track-origins=yes` pour les variables non initialisées.

---

[...retorn en rèire](../menu.md)