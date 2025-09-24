# ⚙️ Compilation & Édition de liens en C

[...retorn en rèire](../menu.md)

---

## 🚀 1. Les étapes de compilation

Quand tu tapes une ligne comme :

```bash
gcc main.c -o prog
```

il se passe en réalité **plusieurs étapes** :

1. **Préprocesseur (`cpp`)**

   * Remplace les `#include`, `#define`, etc.
   * Produit un fichier `.i` (*code source prétraité*).

   ```bash
   gcc -E main.c -o main.i
   ```

2. **Compilation (`cc1`)**

   * Traduit le C en **assembleur**.
   * Produit un fichier `.s`.

   ```bash
   gcc -S main.c -o main.s
   ```

3. **Assemblage (`as`)**

   * Traduit l’assembleur en **code machine objet**.
   * Produit un fichier `.o`.

   ```bash
   gcc -c main.c -o main.o
   ```

4. **Édition de liens (`ld`)**

   * Regroupe tous les `.o` + les bibliothèques nécessaires.
   * Produit l’exécutable final.

   ```bash
   gcc main.o biblio.o -o prog
   ```

👉 En une seule commande `gcc main.c -o prog`, GCC enchaîne tout ça automatiquement.

---

## 🧩 2. Les options essentielles

### 🔹 `-c` → Compilation seulement

* Compile le code source `.c` en fichier **objet** `.o`.
* Pas d’édition de liens.
* Exemple :

  ```bash
  gcc -c main.c    # produit main.o
  ```

### 🔹 `-o` → Choisir le nom de sortie

* Permet de donner un nom au fichier de sortie.
* Par défaut : `a.out`.
* Exemple :

  ```bash
  gcc main.c -o monprog
  ```

### 🔹 `-g` → Debug

* Ajoute les **symboles de débogage** (noms de variables, lignes de code).
* Nécessaire pour GDB.
* Exemple :

  ```bash
  gcc -g -c main.c
  ```

### 🔹 `-Wall` → Activer les warnings

* Montre plus d’avertissements (toujours recommandé).

### 🔹 `-I` → Ajouter un chemin d’includes

* Pour indiquer où chercher les fichiers `.h`.
* Exemple :

  ```bash
  gcc -I./include -c main.c
  ```

### 🔹 `-L` et `-l` → Bibliothèques

* `-L` → ajoute un **chemin de recherche** pour les bibliothèques.
* `-l` → indique quelle **librairie** utiliser.
* Exemple :

  ```bash
  gcc main.o -L/usr/lib -lm   # -lm = libm.so (bibliothèque math)
  ```

👉 GCC cherche d’abord dans les chemins relatifs (`./`), puis dans les chemins systèmes (`/usr/lib`, `/usr/local/lib`, etc.).

---

## 🔗 3. Édition de liens (Linking)

L’éditeur de liens (`ld`) doit :

* **Assembler** tous les fichiers objets `.o`.
* **Résoudre les symboles** :

  * Chaque fonction/variable a un **nom de symbole**.
  * Le linker doit associer les appels (`printf`) aux définitions (dans libc).
* **Relier les bibliothèques** nécessaires (`-lm`, `-lpthread`, etc.).

Exemple classique :

```bash
gcc main.o biblio.o -o prog
```

* `main.o` contient `main()` qui appelle `somme()`.
* `biblio.o` contient l’implémentation de `somme()`.
* Le linker relie les deux.

👉 Si `biblio.o` manque, tu as une erreur du type :

```
undefined reference to `somme'
```

---

## 📂 4. Chemins relatifs vs absolus

* **Relatif** : basé sur ton répertoire courant (`./include/mon.h`).
* **Absolu** : chemin complet depuis `/` (`/usr/include/mon.h`).

Exemple :

```bash
gcc -I./include -c main.c
gcc main.o ./lib/mabiblio.o -o prog
```

---

## 🛠️ 5. Exemple complet

### Fichiers :

* `main.c`
* `biblio.c`
* `biblio.h`

### Étapes manuelles :

```bash
gcc -c -g main.c    # → main.o
gcc -c -g biblio.c  # → biblio.o
gcc main.o biblio.o -o prog
```

### Avec Makefile :

```makefile
prog: main.o biblio.o
    gcc main.o biblio.o -o prog

main.o: main.c biblio.h
    gcc -c -g main.c

biblio.o: biblio.c biblio.h
    gcc -c -g biblio.c
```

---

## 🧹 6. Résumé visuel

1. `main.c` + `biblio.c`
   ↓ **gcc -c**
   `main.o` + `biblio.o`
   ↓ **gcc (link)**
   `prog` (exécutable)

2. Options utiles :

* `-c` → compile seulement.
* `-o` → nom de sortie.
* `-g` → debug.
* `-I` → includes.
* `-L` + `-l` → bibliothèques.

---

[...retorn en rèire](../menu.md)