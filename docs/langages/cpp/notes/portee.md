# 🧠 Portée, Visibilité et Durée de Vie en C / C++

[...retorn en rèire](../menu.md)

---

## 🧭 0. Carte mentale globale (à garder en tête)

Quand tu vois **un nom en C/C++** (variable, fonction, constante, etc.), tu dois **toujours** te poser **4 questions** :

> 🔎 **Qui peut voir ce nom ?**
> 📍 **Où peut-on l’utiliser ?**
> ⏳ **Combien de temps existe-t-il en mémoire ?**
> 🧱 **Est-ce une déclaration ou une définition ?**

Ces questions correspondent à :

| Notion                       | Question                                         |
| ---------------------------- | ------------------------------------------------ |
| **Portée (scope)**           | Où dans le code je peux l’utiliser ?             |
| **Visibilité (linkage)**     | Est-ce que les autres fichiers peuvent le voir ? |
| **Durée de vie (lifetime)**  | Combien de temps la mémoire existe ?             |
| **Déclaration / Définition** | Y a-t-il de la mémoire associée ?                |

---

## 🧱 1. Déclaration vs Définition

### 📌 Déclaration

👉 **Annonce l’existence d’un nom**, sans mémoire associée.

* Sert au **compilateur**
* Ne réserve **aucune mémoire**
* Permet de vérifier l’utilisation

```cpp
extern int compteur;
void f();
```

🧠 *« Le compilateur sait que ça existe, mais pas où c’est stocké »*

---

### 📌 Définition

👉 **Crée réellement l’objet** et **réserve la mémoire**.

* Sert au **linker**
* Il ne doit y avoir **qu’une seule définition**
* Toute définition est aussi une déclaration

```cpp
int compteur = 0;
void f() { }
```

🧠 *« La mémoire est réellement allouée ici »*

---

### ⚠️ Règle fondamentale (TD Q1)

> 🔴 **Une déclaration ≠ une définition**
> 🔴 **Une définition alloue de la mémoire, une déclaration non**

---

## 📍 2. La Portée (Scope)

👉 **La portée répond à la question :**

> *Dans quelles zones du code puis-je utiliser ce nom ?*

### 🔹 2.1 Portée bloc `{ }`

```cpp
if (true) {
    int x = 3;
}
// x n'existe plus ici
```

* Limitée aux `{ }`
* Très courte
* Typique des variables locales

🧠 **Mot-clé mental** : *bloc = accolades*

---

### 🔹 2.2 Portée fonction

```cpp
void f(int param) {
    int local;
}
```

* Paramètres + variables locales
* Existent uniquement dans la fonction

---

### 🔹 2.3 Portée fichier (globale)

```cpp
int compteur;

void f() {
    compteur++;
}
```

* Visible dans **tout le fichier**
* Peut être utilisée par toutes les fonctions du fichier

---

## 👁️ 3. Visibilité (Linkage / Édition de liens)

⚠️ **Notion cruciale du TD**
👉 La visibilité concerne **plusieurs fichiers**

### ❗ À ne pas confondre

| Notion         | Concerne  |
| -------------- | --------- |
| **Portée**     | Le code   |
| **Visibilité** | Le linker |

---

### 🔹 3.1 Visibilité interne (internal linkage)

👉 Visible **uniquement dans le fichier courant**

```cpp
static int compteur;
static void calcul();
```

* Le linker **ne voit pas ce symbole**
* Protège contre les conflits de noms

🧠 *« Ce fichier garde son secret »*

---

### 🔹 3.2 Visibilité externe (external linkage)

👉 Visible **dans toute l’application**

```cpp
int compteur;          // définition
extern int compteur;   // déclaration
```

* Le linker relie les fichiers entre eux
* Un seul exemplaire mémoire

🧠 *« Tout le programme partage la même variable »*

---

## ⏳ 4. Durée de Vie (Lifetime)

👉 **Combien de temps la mémoire existe**

### 🔹 4.1 Durée de vie automatique

```cpp
void f() {
    int x;
}
```

* Allouée à l’entrée du bloc
* Détruite à la sortie

🧠 *pile (stack)*

---

### 🔹 4.2 Durée de vie statique

```cpp
static int x;
int y; // global
```

* Existe pendant **toute l’exécution**
* Initialisée une seule fois

🧠 *segment statique*

---

### 🔹 4.3 Durée de vie dynamique

```cpp
int* p = new int;
delete p;
```

* Contrôlée par le programmeur
* Erreurs possibles (fuite mémoire)

🧠 *tas (heap)*

---

## 🧪 5. Lecture guidée des lignes du TD

### Exemple :

```cpp
static int calcul(int x);
```

👉 Tu dois répondre instantanément :

| Notion                   | Réponse     |
| ------------------------ | ----------- |
| Portée                   | fichier     |
| Visibilité               | interne     |
| Durée de vie             | statique    |
| Déclaration / définition | déclaration |

---

### Exemple :

```cpp
static int indice = 1;
```

| Notion       | Réponse         |
| ------------ | --------------- |
| Portée       | bloc (fonction) |
| Visibilité   | interne         |
| Durée de vie | statique        |
| Mémoire      | allouée         |

---

### Exemple :

```cpp
char *zone;
zone = new char[10];
delete[] zone;
```

* `zone` → durée de vie automatique
* la mémoire pointée → dynamique

🧠 **Important** : la durée de vie du pointeur ≠ durée de vie de la mémoire pointée

---

## 🔗 6. Compilation vs Édition de liens (TD pièges)

### Erreur classique

```cpp
int i; // dans F1.cpp
int i; // dans F2.cpp
```

❌ **Erreur de multiple définition**

---

### Correction

```cpp
// F1.cpp
int i;

// F2.cpp
extern int i;
```

✔️ Une définition
✔️ Plusieurs déclarations

---

## 🧨 7. Cas C vs C++ (TD 9 / 10)

### En C :

```c
const int taille = 10;
```

👉 **visibilité interne par défaut**

### En C++ :

```cpp
const int taille = 10;
```

👉 **visibilité interne aussi**, MAIS :

```cpp
extern const int taille;
```

➡️ Nécessite une définition explicite ailleurs

🧠 **Piège classique de TD**

---

## 🧩 8. Méthode infaillible pour le TD

Devant **chaque ligne**, fais ce raisonnement automatique :

1️⃣ Est-ce une **déclaration ou définition** ?
2️⃣ Où est-elle écrite ? (bloc / fonction / fichier)
3️⃣ Y a-t-il `static` ou `extern` ?
4️⃣ Quelle mémoire est allouée ?
5️⃣ Le linker peut-il la voir ?

👉 **Toujours dans cet ordre**

---

Parfait — on enchaîne en mode **“prof-piégeur-proof”** 😄
Je te fais la **suite de la doc**, en C++11, avec **analyse ligne par ligne** + **pièges probables**.

---

# 🧪 Suite — Exemples complets (F1.cpp) + analyse ligne par ligne (C++11)

> 🧠 Rappel ultra-court :
>
> * **Portée (scope)** = où on peut écrire le nom dans le code
> * **Visibilité (linkage)** = est-ce que le symbole est “exporté” aux autres fichiers (linker)
> * **Durée de vie (lifetime)** = combien de temps la mémoire existe
> * **Déclaration / Définition** = mémoire allouée ou pas

On suppose que `F1.cpp` fait partie d’une appli multi-fichiers (avec potentiellement un `F2.cpp`).

```cpp
...
int Compteur;
static char carLu;
...
int Ouvrir ( const string & nomFichier );
...
static int calcul ( int x ) // Fonction ordinaire
{
 ...
} //----- Fin de calcul
...
char *Traitement ( ) // Fonction ordinaire
{
 static int indice = 1;
 char *zone;
 ...
 zone = new char [ ... ];
 ...
 delete [ ] zone;
 ...
```
---

## 1) `int Compteur;`

### ✅ Ce que c’est

➡️ **Définition** d’une variable globale (au sens “namespace global”).

### 🔍 Analyse complète

| Aspect                       | Réponse                                                        |
| ---------------------------- | -------------------------------------------------------------- |
| **Nom**                      | `Compteur`                                                     |
| **Nature**                   | variable globale                                               |
| **Déclaration / définition** | ✅ **définition** (alloue de la mémoire)                        |
| **Portée (scope)**           | **fichier / global** (visible dans tout F1.cpp après la ligne) |
| **Visibilité (linkage)**     | 🌍 **externe** (external linkage)                              |
| **Durée de vie**             | ⏳ **statique** (toute l’exécution du programme)                |
| **Stockage**                 | segment statique (data/bss)                                    |

### 🎯 Pièges classiques

* Si un autre fichier fait aussi `int Compteur;` → ❌ **multiple definition** à l’édition de liens.
* Si tu veux juste l’utiliser ailleurs : dans l’autre fichier tu écris `extern int Compteur;` (déclaration, pas définition).

---

## 2) `static char carLu;`

### ✅ Ce que c’est

➡️ **Définition** d’une variable globale **avec visibilité interne**.

### 🔍 Analyse complète

| Aspect                       | Réponse                                              |
| ---------------------------- | ---------------------------------------------------- |
| **Nom**                      | `carLu`                                              |
| **Nature**                   | variable globale                                     |
| **Déclaration / définition** | ✅ définition                                         |
| **Portée (scope)**           | fichier/global                                       |
| **Visibilité (linkage)**     | 🔒 **interne** (*internal linkage* grâce à `static`) |
| **Durée de vie**             | ⏳ statique (toute l’exécution)                       |

### 🎯 Pièges

* `static` ici **ne change PAS la portée** (toujours globale), il change la **visibilité (linkage)**.
* Dans un autre fichier, même si tu écris `extern char carLu;` → ❌ **impossible** : le symbole n’est pas exporté.

---

## 3) `int Ouvrir ( const string & nomFichier );`

### ✅ Ce que c’est

➡️ **Déclaration** d’une fonction (prototype).

### 🔍 Analyse complète

| Aspect                       | Réponse                                                                   |
| ---------------------------- | ------------------------------------------------------------------------- |
| **Nom**                      | `Ouvrir`                                                                  |
| **Nature**                   | fonction                                                                  |
| **Déclaration / définition** | 📣 **déclaration** (pas de corps donc pas de définition ici)              |
| **Portée (scope)**           | fichier/global                                                            |
| **Visibilité (linkage)**     | 🌍 externe (par défaut pour une fonction non-`static`)                    |
| **Durée de vie**             | ⏳ “statique” (le code de la fonction, s’il existe, vit toute l’exécution) |

### 🎯 Pièges prof

* Si `Ouvrir` n’est **définie nulle part** dans l’application → ❌ **undefined reference** au link.
* Si tu avais écrit `static int Ouvrir(...);` → visibilité interne, donc utilisable seulement dans F1.cpp.

---

## 4) `static int calcul ( int x ) { ... }`

### ✅ Ce que c’est

➡️ **Définition** d’une fonction avec **visibilité interne**.

### 🔍 Analyse complète

| Aspect                       | Réponse                                             |
| ---------------------------- | --------------------------------------------------- |
| **Nom**                      | `calcul`                                            |
| **Nature**                   | fonction                                            |
| **Déclaration / définition** | ✅ **définition** (corps présent)                    |
| **Portée (scope)**           | fichier/global                                      |
| **Visibilité (linkage)**     | 🔒 **interne** (à cause de `static`)                |
| **Durée de vie**             | ⏳ statique (code présent pendant toute l’exécution) |

### 🎯 Pièges

* On peut avoir une autre fonction `static int calcul(int)` dans **F2.cpp** sans conflit ✅ (car interne à chaque fichier).
* Mais si tu enlèves `static` et que F2.cpp définit aussi `int calcul(int)` → ❌ conflit (multiple definition).

---

## 5) `char *Traitement ( ) { ... }`

### ✅ Ce que c’est

➡️ **Définition** d’une fonction “publique” (visibilité externe par défaut).

### 🔍 Analyse globale

| Aspect                       | Réponse                   |
| ---------------------------- | ------------------------- |
| **Nom**                      | `Traitement`              |
| **Déclaration / définition** | ✅ définition              |
| **Portée**                   | fichier/global            |
| **Visibilité (linkage)**     | 🌍 externe (non-`static`) |
| **Durée de vie**             | ⏳ statique (code)         |

### 🎯 Piège prof (très important)

Cette fonction **retourne un pointeur** `char*`.
➡️ Le prof peut te demander : **“ça pointe vers quoi ? et est-ce valide après return ?”**
Réponse : ça dépend **d’où vient la mémoire** (stack vs heap vs static).
On va le voir avec `zone`.

---

# 🧩 À l’intérieur de `Traitement()`

## 6) `static int indice = 1;`

### ✅ Ce que c’est

➡️ Variable locale **statique** : portée locale, durée de vie globale.

| Aspect                       | Réponse                                                             |
| ---------------------------- | ------------------------------------------------------------------- |
| **Nom**                      | `indice`                                                            |
| **Déclaration / définition** | ✅ définition (initialisation)                                       |
| **Portée (scope)**           | 📍 **bloc de la fonction** (accessible seulement dans `Traitement`) |
| **Visibilité (linkage)**     | 🚫 aucune (pas d’édition de liens : variable locale)                |
| **Durée de vie**             | ⏳ **statique** (elle existe du début à la fin du programme)         |
| **Initialisation**           | une seule fois (au 1er appel, ou avant main selon implémentation)   |

### 🎯 Pièges prof

* “Est-ce qu’elle est réinitialisée à 1 à chaque appel ?” → ❌ non.
* “Est-ce que deux appels partagent la même variable ?” → ✅ oui.

---

## 7) `char *zone;`

### ✅ Ce que c’est

➡️ Pointeur local automatique (sur la pile).

| Aspect                       | Réponse                                                               |
| ---------------------------- | --------------------------------------------------------------------- |
| **Nom**                      | `zone`                                                                |
| **Déclaration / définition** | ✅ définition (déclare un pointeur, mémoire pour le pointeur lui-même) |
| **Portée**                   | bloc de la fonction                                                   |
| **Visibilité**               | 🚫 aucune (local)                                                     |
| **Durée de vie**             | ⏳ **automatique** (jusqu’à la fin de l’appel de fonction)             |

### 🎯 Piège majeur

* Le pointeur `zone` disparaît à la fin, **mais ça ne dit rien** sur la durée de vie de la mémoire pointée.

---

## 8) `zone = new char [ ... ];`

### ✅ Ce que c’est

➡️ Allocation dynamique (heap).

| Aspect                                 | Réponse                                                        |
| -------------------------------------- | -------------------------------------------------------------- |
| **Nom concerné**                       | `zone` (le pointeur) + le bloc mémoire alloué                  |
| **Portée du pointeur**                 | fonction                                                       |
| **Durée de vie du pointeur**           | automatique (fin de fonction)                                  |
| **Durée de vie de la mémoire allouée** | 💥 **dynamique : jusqu’à delete[]**                            |
| **Visibilité**                         | local, mais l’adresse peut être transmise (return, paramètre…) |

### 🎯 Piège prof

* “Si on retourne `zone`, c’est valide ?”
  ✅ Oui **tant que** on ne `delete[]` pas avant return, et qu’on `delete[]` plus tard ailleurs.

---

## 9) `delete [ ] zone;`

### ✅ Ce que c’est

➡️ Libération mémoire heap.

| Aspect    | Réponse                                                            |
| --------- | ------------------------------------------------------------------ |
| **Effet** | détruit la mémoire allouée par `new[]`                             |
| **Après** | `zone` devient un pointeur pendu (dangling) si pas mis à `nullptr` |

### 🎯 Pièges prof

* Si tu fais `return zone;` **après** ce `delete[]` → ❌ tu retournes un pointeur vers mémoire libérée (dangling).
* Si tu oublies ce `delete[]` → ❌ fuite mémoire (memory leak).
* Si tu écris `delete zone;` au lieu de `delete[] zone;` → ❌ comportement indéfini (UB).

---

# 🧨 Les “cas triky” que le prof adore

## ✅ Cas 1 — Confusion portée vs visibilité

> `static char carLu;`

* portée : globale
* visibilité : interne

📌 Beaucoup d’étudiants répondent “portée interne” → c’est faux.
✅ Il faut dire : **linkage interne**.

---

## ✅ Cas 2 — “La variable locale static est visible dans d’autres fichiers ?”

> `static int indice = 1;` (dans une fonction)

Non. Elle n’a **pas** de linkage du tout.
C’est une variable **locale**.

---

## ✅ Cas 3 — “Retourné pointeur : valide ?”

Un retour `char*` peut être :

* ✅ ok si ça pointe vers heap (`new`)
* ✅ ok si ça pointe vers `static` (ex : `static char buf[100];`)
* ❌ pas ok si ça pointe vers une variable locale `char buf[100];` (stack)

---

## ✅ Cas 4 — Mélange `static` de C vs `static` de méthode C++ en classe

Ici on est en **C++ “C-like”** (fichiers .cpp sans classes), donc `static` = **internal linkage** ou **durée de vie statique** selon contexte.

---

Parfait, tu mets le doigt **exactement** sur la difficulté centrale de ce chapitre 👍
👉 **Tu n’es pas en retard**, c’est *le* point qui fait trébucher 80 % des étudiants, parce que :

* portée et visibilité **se ressemblent**
* elles agissent **à des moments différents**
* elles ne répondent **pas à la même question**
* et le prof **joue volontairement sur la confusion**

Je vais donc **rajouter une GROSSE PARTIE à ton cours**, conçue **uniquement pour lever cette confusion**, avec :

* des analogies mentales,
* une méthode mécanique pour répondre,
* des tableaux décisionnels,
* et des “tests rapides” que tu peux faire devant une ligne de code.

---

#  🔥 COMPRENDRE VRAIMENT la différence entre **PORTÉE – VISIBILITÉ – DURÉE DE VIE**

## 🧩 1. Les 3 notions n’agissent PAS au même moment

👉 C’est LA clé.

| Notion                      | Agit quand ?                       | Qui s’en occupe ?                |
| --------------------------- | ---------------------------------- | -------------------------------- |
| **Portée (scope)**          | Quand on **écrit le code**         | le **langage / compilateur**     |
| **Visibilité (linkage)**    | Quand on **assemble les fichiers** | le **linker (éditeur de liens)** |
| **Durée de vie (lifetime)** | Quand le **programme s’exécute**   | la **mémoire / runtime**         |

👉 **Elles ne jouent pas dans la même dimension du temps.**

---

## 🧠 2. Analogie ultra simple (à retenir absolument)

### 🏠 Imagine un immeuble

### 🔹 Portée = **dans quelles pièces je peux aller**

* cuisine
* salon
* chambre
* étage

➡️ **C’est une question de STRUCTURE DU CODE**

---

### 🔹 Visibilité = **est-ce que les autres immeubles savent que la pièce existe**

* porte privée
* porte publique
* adresse connue ou non

➡️ **C’est une question de MULTI-FICHIERS**

---

### 🔹 Durée de vie = **combien de temps la pièce existe**

* construite pour toujours
* construite temporairement
* détruite à un moment précis

➡️ **C’est une question de MÉMOIRE**

---

## 🎯 3. DÉFINITION CLAIRE ET NON AMBIGÜE

### ✅ Portée (scope)

> **La portée est l’ensemble des endroits du code où un nom peut être écrit sans erreur.**

📌 Elle dépend uniquement de :

* `{ }`
* fonctions
* fichiers
* namespaces

🚫 Elle **ne dépend jamais** de `extern`, `static` (au sens linkage), du linker.

---

### ✅ Visibilité (linkage)

> **La visibilité indique si un nom est connu en dehors du fichier courant.**

📌 Elle dépend uniquement de :

* `static`
* `extern`
* règles du langage (par défaut)

🚫 Elle **ne dépend jamais** des `{ }`.

---

### ✅ Durée de vie (lifetime)

> **La durée de vie est la période pendant laquelle la mémoire associée existe réellement.**

📌 Elle dépend de :

* automatique (pile)
* statique
* dynamique (`new/delete`)

---

## 🧠 4. LA RÈGLE D’OR (à apprendre par cœur)

> 🔴 **Portée = où dans le code ?**
> 🔵 **Visibilité = dans quels fichiers ?**
> 🟢 **Durée de vie = pendant combien de temps ?**

Si tu confonds encore → reviens à cette phrase.

---

## 🧪 5. Méthode MÉCANIQUE pour analyser UNE ligne

👉 Devant **n’importe quelle ligne**, fais **toujours ce raisonnement en 3 passes** :

---

### 🔍 PASSE 1 — PORTÉE (TOUJOURS EN PREMIER)

❓ **Où suis-je dans le code ?**

* dans un bloc `{ }` → portée bloc
* dans une fonction → portée fonction
* hors de toute fonction → portée fichier

👉 **Ignore totalement `static` et `extern` à cette étape**

---

### 🔍 PASSE 2 — VISIBILITÉ (SEULEMENT SI GLOBAL)

❓ **Ce nom peut-il être vu depuis un autre fichier ?**

* `static` → visibilité interne
* `extern` → visibilité externe
* rien → visibilité externe (par défaut)

👉 **Si la variable est locale : visibilité = aucune (le linker n’intervient pas)**

---

### 🔍 PASSE 3 — DURÉE DE VIE

❓ **Quand la mémoire est-elle créée et détruite ?**

* variable locale non `static` → automatique
* variable `static` ou globale → statique
* `new` / `delete` → dynamique

---

## 🧠 6. EXEMPLES ULTRA GUIDÉS (le cœur du déclic)

---

### Exemple 1

```cpp
int x;
```

**Passe 1 – Portée**
➡️ écrit hors fonction → **portée fichier**

**Passe 2 – Visibilité**
➡️ pas `static` → **visibilité externe**

**Passe 3 – Durée de vie**
➡️ globale → **statique**

✅ Conclusion :

> portée fichier – visibilité externe – durée de vie statique

---

### Exemple 2

```cpp
static int x;
```

**Passe 1 – Portée**
➡️ toujours hors fonction → **portée fichier**

**Passe 2 – Visibilité**
➡️ `static` → **visibilité interne**

**Passe 3 – Durée de vie**
➡️ statique

🚨 ERREUR CLASSIQUE À ÉVITER
❌ “portée interne” → FAUX
✅ **portée fichier, visibilité interne**

---

### Exemple 3

```cpp
void f() {
    int x;
}
```

**Passe 1 – Portée**
➡️ bloc de fonction → **portée bloc**

**Passe 2 – Visibilité**
➡️ variable locale → **aucune visibilité (linker non concerné)**

**Passe 3 – Durée de vie**
➡️ automatique

---

### Exemple 4

```cpp
void f() {
    static int x;
}
```

💥 Celui que le prof adore.

* portée → bloc (fonction)
* visibilité → aucune
* durée de vie → **statique**

👉 **Portée courte, durée de vie longue**
👉 C’est NORMAL, et c’est voulu.

---

## 🧨 7. TABLEAU ANTI-CONFUSION (À MÉMORISER)

| Situation        | Portée  | Visibilité | Durée de vie |
| ---------------- | ------- | ---------- | ------------ |
| globale          | fichier | externe    | statique     |
| globale `static` | fichier | interne    | statique     |
| locale           | bloc    | aucune     | automatique  |
| locale `static`  | bloc    | aucune     | statique     |
| `new`            | —       | —          | dynamique    |

---

## 🧠 8. Test mental rapide (anti-piège prof)

Quand le prof te demande :

> “Quelle est la portée et la visibilité de X ?”

👉 Tu dois **répondre en 2 phrases distinctes** :

> 🔹 *La portée de X est … car …*
> 🔹 *La visibilité de X est … car …*

S’il te fait répondre en une seule → il t’a piégé.

---

[...retorn en rèire](../menu.md)