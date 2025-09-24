# 📖 Allocation dynamique en C

[...retorn en rèire](../menu.md)

---

## 🚀 1. Pourquoi utiliser l’allocation dynamique ?

En C, on peut allouer la mémoire de trois façons :

1. **Allocation statique** → taille connue à la compilation  
   ```c
   int tableau[10]; // taille figée
   ```
2. **Allocation automatique (pile)** → variables locales créées/détruites automatiquement  
   ```c
   void f() {
       int x = 42; // sur la pile
   } // libéré automatiquement
   ```
3. **Allocation dynamique (tas / heap)** → mémoire réservée **à l’exécution**, taille variable  
   * On utilise les fonctions de la bibliothèque `<stdlib.h>` : `malloc`, `calloc`, `realloc`, `free`.

---

## 🧰 2. Fonctions principales

### 🔹 `malloc`

* Réserve un **bloc contigu** de mémoire (non initialisé).
* Signature :
  ```c
  void *malloc(size_t size);
  ```
* Exemple :
  ```c
  int *tab = malloc(10 * sizeof(int)); // tableau de 10 int
  if (!tab) {
      perror("Erreur malloc");
      exit(1);
  }
  ```

⚠️ **Important : contenu non initialisé !**  
- Les cases contiennent ce qui se trouvait déjà à ces adresses → des **valeurs indéfinies** (souvent appelées *garbage values*).  
- Exemple :  
  ```c
  int *t = malloc(3 * sizeof(int));
  printf("%d %d %d\n", t[0], t[1], t[2]); // ⚠️ valeurs imprévisibles
  ```

👉 Si tu veux être sûr que tout démarre à zéro, utilise `calloc`.

---

### 🔹 `calloc`

* Comme `malloc`, mais :
  - Initialise **tous les octets** de la zone allouée à `0`.
  - Signature :
    ```c
    void *calloc(size_t n, size_t size);
    ```
* Exemple :
  ```c
  int *tab = calloc(10, sizeof(int)); // 10 int, tous à 0
  ```

📌 **Que veut dire "mettre tout à 0" ?**  
- Pour un `int` : sa représentation binaire est mise à `0000...` → valeur 0.  
- Pour un `char` : c’est le caractère nul `'\0'`.  
- Pour un `float` ou `double` : la valeur devient `0.0` (car la représentation IEEE de zéro est aussi tous les bits à 0).  
- Pour une `struct` : **tous les champs sont mis à 0 binaire**.  
  ```c
  typedef struct {
      int age;
      char nom[20];
      float taille;
      void *ptr;
  } Personne;
  
  Personne *p = calloc(1, sizeof(Personne));
  // Résultat :
  // p->age = 0
  // p->nom = tableau rempli de '\0' (chaîne vide)
  // p->taille = 0.0
  // p->ptr = NULL
  ```
👉 Cela marche car tous ces types ont une représentation binaire cohérente avec le zéro.

---

### 🔹 `realloc`

* Sert à **redimensionner** un bloc existant.
* Signature :
  ```c
  void *realloc(void *ptr, size_t new_size);
  ```
* Exemple :
  ```c
  int *tab = malloc(5 * sizeof(int));
  int *tmp = realloc(tab, 10 * sizeof(int)); // agrandit le tableau
  if (!tmp) {
      free(tab);
      exit(1);
  }
  tab = tmp;
  ```

⚠️ Si `realloc` échoue → retourne `NULL`, l’ancien bloc reste valide.  
👉 Toujours utiliser un **pointeur temporaire** pour éviter de perdre la mémoire initiale.

---

### 🔹 `free`

* Libère la mémoire réservée avec `malloc` / `calloc` / `realloc`.
* Signature :
  ```c
  void free(void *ptr);
  ```
* Exemple :
  ```c
  free(tab);
  tab = NULL; // 🔒 Bonne pratique : éviter les "dangling pointers"
  ```

---

## 🔍 3. Bonnes pratiques avec l’allocation dynamique

✔️ Toujours vérifier le retour de `malloc`/`calloc`/`realloc`.  
✔️ Penser à `free()` chaque allocation → sinon **fuites mémoire**.  
✔️ Mettre les pointeurs libérés à `NULL`.  
✔️ Ne jamais accéder à un pointeur après `free`.  
✔️ Initialiser si besoin (`calloc` ou boucle `for`).  
✔️ Utiliser un pointeur temporaire avec `realloc`.  

---

## 🛠️ 4. Pointeurs et tableaux dynamiques

### 🔹 Accès par indice
```c
int *tab = malloc(5 * sizeof(int));
for (int i = 0; i < 5; i++) {
    tab[i] = i * 10;  // notation tableau
}
```

### 🔹 Accès par pointeur (arithmétique des pointeurs)
```c
for (int i = 0; i < 5; i++) {
    *(tab + i) = i * 10;  // équivalent à tab[i]
}
```

### 🔹 Itération par pointeur
```c
int *p = tab;
for (int i = 0; i < 5; i++) {
    printf("%d ", *(p++)); // on avance le pointeur
}
```

---

## 🏗️ 5. Allocation dans les structures

### Exemple : tableau dynamique dans une structure
```c
typedef struct {
    int taille;
    int *valeurs;
} Tableau;

Tableau creerTableau(int n) {
    Tableau t;
    t.taille = n;
    t.valeurs = malloc(n * sizeof(int));
    return t;
}
```

✔️ Un pointeur dans une `struct` permet d’avoir des champs **de taille variable**.  
✔️ Il faut penser à **libérer le champ dynamique** avant de libérer la structure.  

---

## 🎩 6. Astuces avancées

### 🔹 Changer le type d’un pointeur
```c
double pi = 3.14;
void *p = &pi;
int *q = (int *)p; // interprétation différente de la mémoire
printf("%d\n", *q); // résultat indéfini ⚠️
```
⚠️ Sert parfois en **bas niveau**, mais à éviter sauf nécessité.

---

### 🔹 Struct et pointeurs → listes chaînées
```c
typedef struct Noeud {
    int valeur;
    struct Noeud *suivant;
} Noeud;

Noeud *ajouter(Noeud *tete, int val) {
    Noeud *n = malloc(sizeof(Noeud));
    n->valeur = val;
    n->suivant = tete;
    return n;
}
```

---

### 🔹 Allocation de "tableaux de structures"
```c
typedef struct {
    int x, y;
} Point;

Point *points = malloc(100 * sizeof(Point));
points[0].x = 10;
points[0].y = 20;
```

---

### 🔹 "Pointer casting" pour lire les octets bruts
```c
int val = 0x12345678;
unsigned char *bytes = (unsigned char *)&val;
printf("%x %x %x %x\n", bytes[0], bytes[1], bytes[2], bytes[3]);
```
👉 Utile en **réseaux, fichiers binaires, protocoles**.

---

[...retorn en rèire](../menu.md)

