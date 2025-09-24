
# 📖 Accès aux fichiers en C

[...retorn en rèire](../menu.md)

---

## 🚀 1. Principe général

En C, on manipule les fichiers à travers des **flots de données** (*streams*), représentés par le type `FILE *`.

* Avant de lire ou écrire → ouvrir avec `fopen()`.
* Pendant l’utilisation → lire ou écrire avec les fonctions adaptées.
* À la fin → fermer avec `fclose()`.

👉 En C (et en UNIX), **tout est fichier** : un vrai fichier sur disque, mais aussi l’entrée standard, la sortie standard, un socket réseau…

---

## 🗂️ 2. Ouvrir et fermer un fichier

### 🔹 `fopen`

```c
FILE *fopen(const char *pathname, const char *mode);
```

* `pathname` → chemin du fichier (`"monfichier.txt"`).
* `mode` → mode d’accès :

  * `"r"` : lecture seule
  * `"w"` : écriture seule (écrase si existe)
  * `"a"` : ajout à la fin (*append*)
  * `"r+"` : lecture/écriture (ne crée pas)
  * `"w+"` : lecture/écriture (écrase si existe)
  * `"a+"` : lecture/écriture en ajout

Exemple :

```c
FILE *f = fopen("donnees.txt", "r");
if (!f) {
    perror("Erreur ouverture fichier");
    exit(1);
}
```

### 🔹 `fclose`

```c
int fclose(FILE *stream);
```

* Ferme un fichier.
* Libère les ressources associées (tampon mémoire, descripteur système).

⚠️ **Bonne pratique** : toujours fermer un fichier ouvert avec `fclose()`.

---

## 🖋️ 3. Lire et écrire caractère par caractère

### 🔹 `fgetc`

```c
int fgetc(FILE *stream);
```

* Lit **un caractère**.
* Retourne ce caractère ou `EOF` si fin du fichier.

### 🔹 `fputc`

```c
int fputc(int c, FILE *stream);
```

* Écrit **un caractère** dans un fichier.

### Exemple

```c
FILE *in = fopen("input.txt", "r");
FILE *out = fopen("output.txt", "w");

int c;
while ((c = fgetc(in)) != EOF) {
    fputc(c, out);
}

fclose(in);
fclose(out);
```

👉 Copie un fichier caractère par caractère.

---

## 📄 4. Lire et écrire ligne par ligne

### 🔹 `fgets`

```c
char *fgets(char *s, int size, FILE *stream);
```

* Lit au maximum `size - 1` caractères (jusqu’à un `\n` ou `EOF`).
* Termine toujours par `\0`.
* Retourne `NULL` en cas d’erreur ou fin de fichier.

📌 **Trois cas d’arrêt :**

1. On rencontre un **retour à la ligne** → la ligne est terminée.
2. On atteint la **fin du fichier (EOF)**.
3. On atteint la **limite `size - 1`** caractères.

### 🔹 `fputs`

```c
int fputs(const char *s, FILE *stream);
```

* Écrit une chaîne de caractères dans un fichier.

### Exemple

```c
char buffer[128];
while (fgets(buffer, sizeof(buffer), in)) {
    fputs(buffer, stdout); // écrit sur la sortie standard
}
```

---

## 📍 5. Fin de fichier et erreurs

### 🔹 `feof`

```c
int feof(FILE *stream);
```

* Retourne ≠ 0 si on a atteint la fin du fichier.

### 🔹 `EOF`

* Constante spéciale pour signaler la **fin du fichier**.
* Valeur négative (souvent `-1`).

---

## 📌 6. Positionnement dans un fichier

Par défaut, la lecture/écriture est **séquentielle**. Mais on peut aussi se déplacer directement.

### 🔹 `fseek`

```c
int fseek(FILE *stream, long offset, int whence);
```

* Change la position de lecture/écriture.
* `whence` :

  * `SEEK_SET` → depuis le début.
  * `SEEK_CUR` → depuis la position courante.
  * `SEEK_END` → depuis la fin du fichier.

### 🔹 `rewind`

```c
void rewind(FILE *stream);
```

* Repositionne au **début du fichier**.
* Équivaut à `fseek(stream, 0, SEEK_SET);`.

### 🔹 `ftell`

```c
long ftell(FILE *stream);
```

* Retourne la **position courante** dans le fichier (en octets depuis le début).

---

## 🔢 7. Conversion pratique pour lecture d’offsets

### 🔹 `atol`

```c
long atol(const char *nptr);
```

* Convertit une chaîne de caractères en entier (`long`).
* Exemple :

  ```c
  long n = atol("42"); // n = 42
  ```

👉 Utile quand l’utilisateur tape un offset ou un nombre à la main.

---

## 🖥️ 8. Entrée et sortie standard

* `stdin` → entrée standard (clavier ou redirection).
* `stdout` → sortie standard (écran ou redirection).
* Ce sont aussi des **`FILE *`**.

👉 Exemple :

```c
fputs("Bonjour !\n", stdout);   // équivalent à printf
int c = fgetc(stdin);           // lit un caractère du clavier
```

---

## 🧹 9. Bonnes pratiques

✔️ Toujours vérifier le retour de `fopen` (si `NULL` → erreur).
✔️ Fermer chaque fichier avec `fclose`.
✔️ Ne pas oublier de gérer les **cas limites** (`EOF`, erreurs).
✔️ Ne jamais mélanger `fgets` et `scanf` sans vider le buffer (`\n`).
✔️ Utiliser `fflush(stdout)` si besoin pour forcer l’écriture immédiate.
✔️ Préférer des **buffers de taille fixe** (sécurisé).
✔️ Pour manipuler des gros fichiers → penser au positionnement (`fseek`).

---

## 🧭 10. Résumé visuel

📂 Fichier → ouvert avec `fopen()` → manipulé par un `FILE *`.

* **Lecture :**

  * `fgetc` (char)
  * `fgets` (ligne)

* **Écriture :**

  * `fputc` (char)
  * `fputs` (ligne)

* **Contrôle :**

  * `feof` (fin de fichier)
  * `EOF` (constante spéciale)

* **Positionnement :**

  * `fseek`, `rewind`, `ftell`

* **Fermeture :**

  * `fclose`

---

[...retorn en rèire](../menu.md)