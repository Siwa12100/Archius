# 📘 Pointeurs et Tableaux en C 

[...retorn en rèire](./../menu.md)

---

## 1️⃣ Pointeurs : notions fondamentales

### 📍 Variable et adresse mémoire

* Une **variable** est stockée dans un emplacement mémoire unique, identifié par une **adresse**.
* Deux façons d’accéder à la valeur :

  * 👉 **Adressage symbolique** : par son nom (`a`).
  * 👉 **Adressage indirect** : en passant par son adresse via un pointeur.

```c
int a = 42;    // variable "classique"
printf("%d", a); // accès symbolique
```

### 🧭 Déclaration d’un pointeur

Un pointeur est une variable qui contient une **adresse mémoire** :

```c
int *p;     // p est un pointeur vers un int
double *q;  // q est un pointeur vers un double
```

⚠️ Le type est crucial : `int*` ne se manipule pas comme `double*`.

---

### ✨ Opérateurs essentiels

* `&` : donne l’adresse d’une variable
* `*` : déréférence (accède au contenu pointé)

```c
int a = 10;
int *p = &a;   // p reçoit l'adresse de a

printf("%p\n", p);   // affiche l'adresse de a
printf("%d\n", *p);  // affiche la valeur contenue (10)

*p = 20;             // modifie a via le pointeur
printf("%d\n", a);   // a vaut maintenant 20
```

---

### 🔍 Affichage d’adresses

* `%p` dans `printf()` permet d’afficher une adresse mémoire.

---

### ⚠️ Pointeur non initialisé

* Un pointeur non affecté contient une **valeur indéfinie** → risque de segmentation fault.
* ✅ Bonne pratique : initialiser avec `NULL`.

```c
int *p = NULL;
if(p == NULL) {
    printf("Pointeur vide !\n");
}
```

---

### 🌀 Pointeur générique `void*`

* Peut stocker l’adresse de **n’importe quel type**.
* Nécessite un **cast** pour être utilisé.

```c
void *ptr;
int x = 5;
ptr = &x;             // ok
printf("%d", *(int*)ptr); // cast obligatoire
```

---

### 🎭 Cast de type

* `(type*)` change l’interprétation du pointeur.

```c
double d = 3.14;
void *ptr = &d;
printf("%f", *(double*)ptr); // cast en double*
```

---

### 📏 Taille mémoire

* `sizeof` indique la taille (en octets).

```c
printf("%lu\n", sizeof(int));   // ex: 4
printf("%lu\n", sizeof(int*));  // ex: 8 sur une machine 64 bits
```

---

## 2️⃣ Tableaux et pointeurs

### 📦 Déclaration d’un tableau

```c
int tab[5] = {10, 20, 30, 40, 50};
```

---

### 🔗 Tableau ↔ Pointeur

* Le nom du tableau (`tab`) agit comme **pointeur vers la première case**.
* Équivalence : `tab[i]` ↔ `*(tab + i)`.

```c
printf("%d\n", tab[2]);     // 30
printf("%d\n", *(tab+2));   // 30
```

---

### 🏃 Parcours d’un tableau avec pointeur

```c
int tab[3] = {1, 2, 3};
int *p = tab;

for(int i=0; i<3; i++) {
    printf("%d\n", *(p+i));
}
```

---

### ➕ Arithmétique des pointeurs

* `p++` : avance d’une case (taille = type pointé).
* `p+n` : avance de `n` cases.

⚠️ Dépasser les bornes (`tab[10]` quand tab\[5]) → **comportement indéfini**.

---

### 🔢 Tableaux multidimensionnels

```c
int mat[2][3] = {{1,2,3}, {4,5,6}};
printf("%d\n", mat[1][2]);   // 6
```

➡️ Stockage ligne par ligne en mémoire (row-major order).

---

## 3️⃣ Arithmétique et sécurité

### 🚨 Dépassement mémoire

* Lire/écrire hors du tableau (`tab[11]` dans `tab[10]`) → danger.

### 🎯 Conséquences possibles

* ❌ Crash (`segmentation fault`)
* 🔓 Accès illégal à d’autres données → faille de sécurité.

---

## 4️⃣ Pointeurs et fonctions

### 📤 Passage de paramètres en C

* Par défaut : passage **par valeur** (copie).
* Donc une fonction ne peut pas modifier la variable d’appelante.

```c
void setZero(int a) {
    a = 0; // ne change pas la variable originale
}
```

---

### 🔧 Utilisation des pointeurs pour modifier

Exemple : fonction `swap` pour échanger deux entiers :

```c
void swap(int *a, int *b) {
    int tmp = *a;
    *a = *b;
    *b = tmp;
}

int x=3, y=7;
swap(&x, &y);
printf("%d %d", x, y); // affiche 7 3
```

---

[...retorn en rèire](./../menu.md)