# **🚀 Valgrind dans le Terminal**

[...retorn en rèire](../menu.md)

---

## **📌 Introduction : Qu'est-ce que Valgrind ?**
Valgrind est un **outil d'analyse dynamique** pour détecter des **erreurs mémoire**, des **fuites de mémoire**, des **accès non initialisés**, et bien plus dans les programmes C/C++.

🔹 **Pourquoi l'utiliser ?**
- Détecter les **fuites mémoire** (memory leaks).
- Trouver les **accès à des zones mémoire non allouées** (segmentation faults).
- Optimiser les performances avec **Cachegrind** et **Callgrind**.
- Analyser les **threads** avec **Helgrind** et **DRD**.

🔹 **Fonctionnement** :
Valgrind **simule un CPU** et **surveille toutes les opérations mémoire** de votre programme.

---

## **🛠️ Installation de Valgrind**
### **📥 Sur Linux (Debian/Ubuntu)**
```bash
sudo apt update
sudo apt install valgrind
```

### **📥 Sur macOS (via Homebrew)**
```bash
brew install valgrind
```
⚠️ **Note** : Valgrind sur macOS a des limitations (pas de support complet pour les dernières versions).

### **📥 Sur Windows (via WSL ou Cygwin)**
- Utilisez **WSL (Windows Subsystem for Linux)** et installez Valgrind comme sur Linux.
- Ou utilisez **Cygwin** (moins recommandé).

### **🔍 Vérifier l'installation**
```bash
valgrind --version
```
→ Doit afficher la version installée (ex: `valgrind-3.18.1`).

---

## **🎯 Utilisation de Base : `memcheck` (Détection d'erreurs mémoire)**
### **📝 Syntaxe de base**
```bash
valgrind [options] ./votre_programme [args]
```

### **🔍 Exemple Simple**
1. **Créons un programme buggé (`leak.c`)** :
   ```c
   #include <stdlib.h>

   int main() {
       int *ptr = malloc(sizeof(int) * 10); // Allocation
       ptr[10] = 42; // ❌ Accès hors limites (buffer overflow)
       // free(ptr); // ❌ Fuite mémoire (commenté volontairement)
       return 0;
   }
   ```

2. **Compilons avec `gcc` (sans optimisations pour Valgrind)** :
   ```bash
   gcc -g -O0 leak.c -o leak
   ```
   - `-g` : Ajoute les symboles de débogage.
   - `-O0` : Désactive les optimisations (pour une analyse précise).

3. **Lançons Valgrind** :
   ```bash
   valgrind ./leak
   ```

### **📊 Résultat Typique**
```
==12345== Memcheck, a memory error detector
==12345== Copyright (C) 2002-2017, and GNU GPL'd, by Julian Seward et al.
==12345== Using Valgrind-3.18.1 and LibVEX; rerun with -h for copyright info
==12345== Command: ./leak
==12345==
==12345== Invalid write of size 4
==12345==    at 0x10916E: main (leak.c:6)
==12345==    by 0x10918B: (below main) (in /home/user/leak)
==12345==  Address 0x4a4b068 is 0 bytes after a block of size 40 alloc'd
==12345==    at 0x483B7F3: malloc (vg_replace_malloc.c:307)
==12345==    by 0x109164: main (leak.c:4)
==12345==
==12345==
==12345== HEAP SUMMARY:
==12345==     in use at exit: 40 bytes in 1 blocks
==12345==   total heap usage: 1 allocs, 0 frees, 40 bytes allocated
==12345==
==12345== 40 bytes in 1 blocks are definitely lost in loss record 1 of 1
==12345==    at 0x483B7F3: malloc (vg_replace_malloc.c:307)
==12345==    by 0x109164: main (leak.c:4)
==12345==
==12345== LEAK SUMMARY:
==12345==    definitely lost: 40 bytes in 1 blocks
==12345==    indirectly lost: 0 bytes in 0 blocks
==12345==      possibly lost: 0 bytes in 0 blocks
==12345==    still reachable: 0 bytes in 0 blocks
==12345==         suppressed: 0 bytes in 0 blocks
==12345==
==12345== For lists of detected and suppressed errors, rerun with: -s
==12345== ERROR SUMMARY: 2 errors from 2 contexts (suppressed: 0 from 0)
```
🔴 **Erreurs détectées** :
1. **`Invalid write`** : Accès hors limites (`ptr[10]`).
2. **`definitely lost`** : Fuite mémoire (40 octets non libérés).

---

## **🔧 Options Utiles de Valgrind**
| Option | Description |
|--------|-------------|
| `--leak-check=full` | Affiche les fuites mémoire en détail. |
| `--show-leak-kinds=all` | Montre tous les types de fuites. |
| `--track-origins=yes` | Affiche l'origine des valeurs non initialisées. |
| `--verbose` | Mode verbeux (plus de détails). |
| `--log-file=valgrind.log` | Enregistre la sortie dans un fichier. |
| `--suppressions=file.sup` | Ignore certaines erreurs (via un fichier de suppression). |

### **📝 Exemple avec options avancées**
```bash
valgrind --leak-check=full --show-leak-kinds=all --track-origins=yes ./leak
```

---

## **🧹 Nettoyer les Fausses Alertes (Suppressions)**
Parfois, Valgrind signale des erreurs dans des **bibliothèques externes** (ex: OpenSSL, Qt).
Pour les ignorer, créez un **fichier de suppression** (`suppressions.sup`) :

```bash
{
   <Nom_de_la_suppression>
   Memcheck:Leak
   fun:malloc
   obj:/usr/lib/x86_64-linux-gnu/libsome_lib.so
}
```
Puis utilisez-le :
```bash
valgrind --suppressions=suppressions.sup ./mon_programme
```

---

## **🛑 Erreurs Courantes et Solutions**
| Erreur Valgrind | Cause | Solution |
|----------------|-------|----------|
| **Invalid read/write** | Accès à une zone mémoire non allouée. | Vérifier les indices de tableaux (`ptr[10]` sur un `malloc(10)`). |
| **Use of uninitialised value** | Variable non initialisée utilisée. | Initialiser les variables (`int x = 0;`). |
| **Memory leak** | `malloc` sans `free`. | Libérer la mémoire (`free(ptr)`). |
| **Mismatched free()** | `free` sur un pointeur non alloué ou déjà libéré. | Vérifier les appels à `free`. |
| **Double free** | `free` appelé deux fois sur le même pointeur. | Réinitialiser le pointeur à `NULL` après `free`. |

---

## **🔍 Outils Avancés de Valgrind**
### **1. 📊 `Callgrind` (Profiling des performances)**
Analyse les **appels de fonctions** et le **cache**.
```bash
valgrind --tool=callgrind ./mon_programme
```
→ Génère un fichier `callgrind.out.<pid>`.
Pour visualiser :
```bash
kcachegrind callgrind.out.12345
```
*(Installez `kcachegrind` avec `sudo apt install kcachegrind`.)*

### **2. 🧵 `Helgrind` (Détection d'erreurs de threads)**
Détecte les **race conditions** et les **verrous mal utilisés**.
```bash
valgrind --tool=helgrind ./mon_programme_threaded
```

### **3. 🗄️ `Massif` (Analyse de l'utilisation mémoire)**
Mesure l'**usage de la pile et du tas**.
```bash
valgrind --tool=massif ./mon_programme
```
→ Génère `massif.out.<pid>`.
Pour visualiser :
```bash
ms_print massif.out.12345
```

### **4. 🔄 `DRD` (Détection de race conditions)**
Alternative à Helgrind pour les **problèmes de threads**.
```bash
valgrind --tool=drd ./mon_programme_threaded
```

---

## **💡 Bonnes Pratiques avec Valgrind**
1. **Compiler avec `-g`** pour avoir des **noms de fonctions et numéros de ligne**.
2. **Éviter `-O2` ou `-O3`** (les optimisations peuvent fausser l'analyse).
3. **Tester sur des petits programmes d'abord** pour comprendre les erreurs.
4. **Utiliser `--leak-check=full`** pour des rapports détaillés.
5. **Corriger les erreurs une par une** (de la plus grave à la moins grave).
6. **Vérifier les fuites avec `--show-reachable=yes`** pour voir les blocs "atteignables" mais non libérés.

---

## **🎓 Exercice Pratique**
### **📝 Programme à analyser (`buggy.c`)**
```c
#include <stdlib.h>
#include <stdio.h>

void leak_memory() {
    int *arr = malloc(100 * sizeof(int));
    arr[100] = 42; // ❌ Buffer overflow
    // free(arr); // ❌ Fuite mémoire
}

int main() {
    int x; // ❌ Variable non initialisée
    printf("x = %d\n", x);
    leak_memory();
    return 0;
}
```

### **🔍 Questions**
1. Quelles erreurs Valgrind va-t-il détecter ?
2. Comment corriger chaque erreur ?
3. Quel outil Valgrind utiliser pour analyser les performances de ce programme ?

---
### **📌 Réponses**
1. **Erreurs détectées** :
   - `Use of uninitialised value` (variable `x`).
   - `Invalid write` (accès à `arr[100]`).
   - `Memory leak` (400 octets non libérés).

2. **Corrections** :
   ```c
   #include <stdlib.h>
   #include <stdio.h>

   void leak_memory() {
       int *arr = malloc(100 * sizeof(int));
       arr[99] = 42; // ✅ Accès valide
       free(arr);    // ✅ Libération mémoire
   }

   int main() {
       int x = 0; // ✅ Initialisation
       printf("x = %d\n", x);
       leak_memory();
       return 0;
   }
   ```

3. **Outil pour les performances** :
   ```bash
   valgrind --tool=callgrind ./buggy_corrected
   ```

---

## **📚 Ressources Complémentaires**
- [Documentation Officielle Valgrind](https://valgrind.org/docs/manual/index.html)
- [Tutoriel Valgrind (GNU)](https://www.gnu.org/software/valgrind/)
- [Valgrind Quick Start Guide](https://wiki.wxwidgets.org/Valgrind_Suppression_File_Howto)

---

[...retorn en rèire](../menu.md)