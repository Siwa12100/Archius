# 🐞 Déboguer un programme C avec GDB

[...retorn en rèire](../menu.md)

---

## 🚀 1. Introduction à GDB

**GDB (GNU Debugger)** est un outil qui permet de :

* Exécuter un programme **pas à pas**.
* Placer des **points d’arrêt** (*breakpoints*).
* **Inspecter** le contenu des variables.
* **Suivre** l’exécution d’une fonction.
* Détecter les **erreurs logiques ou mémoire**.

👉 C’est un incontournable pour tout développeur C.

---

## ⚙️ 2. Préparer son programme pour GDB

Pour utiliser GDB, il faut compiler le programme avec l’option **`-g`** (ajoute les symboles de debug).

```bash
gcc -g -o factorielle factorielle.c
```

* `-g` → inclut les infos nécessaires au débogage (noms de variables, lignes de code).
* Sans `-g`, GDB peut quand même déboguer, mais seulement au niveau **assembleur** → inutilisable en TP.

---

## 🏁 3. Démarrer une session GDB

On lance GDB sur l’exécutable généré :

```bash
gdb factorielle
```

On arrive alors dans l’invite de GDB :

```
(gdb)
```

Toutes les commandes s’écrivent à partir de là.

---

## 🎯 4. Placer des breakpoints

Un **breakpoint** arrête l’exécution du programme à une ligne donnée.

### Exemple :

```bash
(gdb) break 10
```

👉 Arrête le programme **avant d’exécuter la ligne 10**.

Si le programme est réparti dans plusieurs fichiers :

```bash
(gdb) break liste.c:25
```

---

## ▶️ 5. Lancer l’exécution

* Sans arguments :

  ```bash
  (gdb) run
  ```
* Avec arguments (si ton `main` attend des paramètres) :

  ```bash
  (gdb) run arg1 arg2
  ```

👉 Le programme démarre et s’arrête au premier breakpoint.

---

## 🔍 6. Inspecter les variables

La commande `print` permet d’afficher la valeur d’une variable.

```bash
(gdb) print i
(gdb) print j
(gdb) print num
```

👉 Exemple :

```
$1 = 42
```

On peut aussi inspecter des expressions :

```bash
(gdb) print i + 10
(gdb) print *ptr
(gdb) print tab[3]
```

---

## ⏯️ 7. Avancer dans l’exécution

* **continue** (ou `c`) → reprend jusqu’au prochain breakpoint.
* **next** (ou `n`) → exécute la **prochaine ligne** sans entrer dans les fonctions.
* **step** (ou `s`) → exécute la prochaine ligne **en entrant dans les fonctions appelées**.

Exemple :

```c
x = f(z);
```

* `next` → affecte directement `x` avec le retour de `f`.
* `step` → entre dans `f()` et exécute son code pas à pas.

---

## 🗂️ 8. Parcourir une liste chaînée avec GDB

Supposons une structure :

```c
typedef struct elem {
    int value;
    struct elem *next;
} elem;
```

Et une liste de 5 éléments.
Dans GDB, on peut suivre **chaîne par chaîne** en affichant les pointeurs :

```bash
(gdb) print list          # adresse du premier élément
$1 = (elem *) 0x555555559260

(gdb) print *list         # contenu du premier maillon
$2 = { value = 10, next = 0x555555559280 }

(gdb) print list->next
$3 = (elem *) 0x555555559280

(gdb) print *(list->next) # contenu du deuxième maillon
$4 = { value = 20, next = 0x5555555592a0 }
```

👉 En répétant `->next`, on peut parcourir **toute la liste manuellement**.

---

## 🛠️ 9. Corriger un code avec GDB

Prenons un code de tri bogué :

```c
void tri(int *tab, int n) {
    for (int i = 0; i <= n; i++) {   // ⚠️ erreur : i <= n
        for (int j = i+1; j < n; j++) {
            if (tab[j] < tab[i]) {
                int tmp = tab[i];
                tab[i] = tab[j];
                tab[j] = tmp;
            }
        }
    }
}
```

Avec GDB, on peut :

1. Mettre un **breakpoint** au début.
2. Exécuter ligne par ligne (`n`).
3. Afficher les valeurs :

   ```bash
   (gdb) print i
   (gdb) print j
   (gdb) print tab[i]
   ```
4. On constate un débordement (`i == n` → `tab[n]` hors limite).

✅ Correction :

```c
for (int i = 0; i < n; i++) {   // strictement <
```

---

## 📚 10. Commandes utiles

* `list` ou `l` → affiche le code source autour de la ligne courante.
* `info breakpoints` → liste tous les breakpoints.
* `delete N` → supprime le breakpoint numéro N.
* `backtrace` → affiche la pile des appels (très utile pour les bugs de segmentation).
* `quit` → quitter GDB.

---

## 📌 11. Ressources complémentaires

* `man gdb` → documentation locale.
* [Docs officielles](https://sourceware.org/gdb/documentation/)
* [Cheat sheet pratique](https://gist.github.com/rkubik/b96c23bd8ed58333de37f2b8cd052c30)

---

[...retorn en rèire](../menu.md)