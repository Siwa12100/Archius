# Standard Template Library (STL) — Partie 1 : Les Conteneurs

[...retorn en rèire](../menu.md)

---

## 🔹 Introduction

La STL est un ensemble de **composants génériques** en C++ :

* **Conteneurs** : structures de données (listes, vecteurs, ensembles, maps…)
* **Itérateurs** : pointeurs abstraits pour parcourir les conteneurs
* **Algorithmes** : tris, recherches, transformations (prochaine partie)
* **Foncteurs / lambdas** : comportements paramétrables

👉 Ici, on se concentre sur les **conteneurs** — les boîtes qui stockent nos objets.

---

# 🧱 1. Catégories de conteneurs STL

| Catégorie       | Description                                                                | Exemples                                                 |
| --------------- | -------------------------------------------------------------------------- | -------------------------------------------------------- |
| **Séquentiels** | Stockent les éléments dans un **ordre précis**, souvent linéaire.          | `std::vector`, `std::list`, `std::deque`                 |
| **Associatifs** | Stockent les éléments selon une **clé d’ordre ou de hachage**.             | `std::set`, `std::map`, `std::multiset`, `std::multimap` |
| **Adaptateurs** | Enveloppent un conteneur sous forme de **pile / file / file de priorité**. | `std::stack`, `std::queue`, `std::priority_queue`        |

---

# ⚙️ 2. Conteneurs séquentiels

## 🧩 `std::vector<T>`

> Tableau dynamique contigu en mémoire

### 📚 Logique interne

* Les éléments sont **stockés à la suite** (comme un `T[]` classique).
* En cas de dépassement de capacité, le vector **réalloue** (copie les éléments vers un nouveau bloc plus grand).
* Temps amorti constant pour `push_back()`.

### ⚡ Complexités

| Opération                           | Complexité | Détails               |
| ----------------------------------- | ---------- | --------------------- |
| Accès aléatoire `v[i]`, `v.at(i)`   | O(1)       | index direct          |
| Insertion/suppression **à la fin**  | O(1)*      | *amortie*             |
| Insertion/suppression **au milieu** | O(n)       | décalage des éléments |
| Recherche séquentielle              | O(n)       |                       |

### 🧠 Cas d’usage

* Quand tu veux un **tableau dynamique rapide**, avec accès par indice.
* Idéal pour les structures à **taille variable** (ex : inventaire, historique, logs).

### 🧩 Exemple

```cpp
std::vector<int> scores = {10, 20, 30};
scores.push_back(40);
scores[1] = 25;              // accès direct
for (int s : scores) std::cout << s << " ";
```

### ⚠️ Attention

* Invalide les pointeurs/itérateurs quand il réalloue.
* `reserve(n)` permet d’anticiper une taille et éviter les réallocations.

---

## 🧩 `std::list<T>`

> Liste doublement chaînée

### 📚 Logique interne

* Chaque élément est un **nœud séparé** contenant un pointeur vers le précédent et le suivant.
* Pas de déplacement des éléments lors d’insertion/suppression → juste ajustement de pointeurs.

### ⚡ Complexités

| Opération                              | Complexité | Détails                      |
| -------------------------------------- | ---------- | ---------------------------- |
| Insertion/suppression **n’importe où** | O(1)       | une fois l’itérateur obtenu  |
| Parcours                               | O(n)       |                              |
| Accès aléatoire `list[i]`              | ❌          | **impossible**, pas d’indice |
| Recherche                              | O(n)       | séquentielle                 |

### 🧠 Cas d’usage

* Quand tu fais **beaucoup d’insertion/suppression au milieu**.
* Quand tu veux **préserver la validité** des itérateurs lors de modifications.

### 🧩 Exemple

```cpp
std::list<std::string> noms = {"Jean", "Marie", "Luc"};
noms.push_front("Anna");
auto it = std::next(noms.begin());
noms.insert(it, "Paul");
```

### ⚠️ Attention

* Accès aléatoire impossible.
* Moins performant que `vector` pour les données petites ou continues.

---

## 🧩 `std::deque<T>`

*(Double Ended QUEue)*

> Tableau dynamique **segmenté**, optimisé pour insérer/supprimer aux deux extrémités.

### 📚 Logique interne

* Structure composée de **blocs de mémoire contigus**, reliés entre eux.
* Accès aléatoire direct possible (calcul d’indice sur blocs).
* Pas besoin de recopier tout le tableau à chaque extension comme `vector`.

### ⚡ Complexités

| Opération                                 | Complexité |
| ----------------------------------------- | ---------- |
| Accès aléatoire                           | O(1)       |
| Insertion/suppression **en début ou fin** | O(1)       |
| Insertion/suppression **au milieu**       | O(n)       |

### 🧠 Cas d’usage

* Quand tu veux **ajouter et retirer des éléments aux deux extrémités**.
  → Ex : simulateur, historique, buffers circulaires, algorithmes BFS.
* Alternative à `vector` quand tu modifies beaucoup le début.

### 🧩 Exemple

```cpp
std::deque<int> d = {1,2,3};
d.push_front(0);   // [0,1,2,3]
d.push_back(4);    // [0,1,2,3,4]
std::cout << d.front() << " " << d.back(); // 0 4
```

### ⚠️ Attention

* Les éléments ne sont **pas garantis contigus** en mémoire.
  → Si tu veux un bloc contigu (pour du C interop, ex.), utilise `vector`.

---

# 🌳 3. Conteneurs associatifs

> Ces conteneurs organisent leurs éléments selon une **clé d’ordre**.
> Basés sur un **arbre équilibré (généralement un Red-Black Tree)** → O(log n) pour la plupart des opérations.

---

## 🧩 `std::set<T>`

> Ensemble de **valeurs uniques triées**

### 📚 Logique interne

* Stocke des **valeurs uniques** ordonnées par `operator<` par défaut.
* Chaque élément est à la fois **clé et valeur**.
* Arbre équilibré (RB-tree) → accès, insertion, suppression en **O(log n)**.

### ⚡ Complexités

| Opération          | Complexité | Détails        |
| ------------------ | ---------- | -------------- |
| Insertion          | O(log n)   |                |
| Suppression        | O(log n)   |                |
| Recherche          | O(log n)   | via `find()`   |
| Accès par position | O(n)       | via itérateurs |

### 🧠 Cas d’usage

* Quand tu veux **garantir l’unicité** et **l’ordre automatique** des éléments.
* Idéal pour : ensembles, tags triés, dictionnaires de mots uniques, etc.

### 🧩 Exemple

```cpp
std::set<int> s = {5, 3, 1, 5, 2};
for (int v : s) std::cout << v << " "; // 1 2 3 5
if (s.count(3)) std::cout << "3 existe";
```

---

## 🧩 `std::multiset<T>`

> Comme `set`, mais **autorise les doublons**

### 📚 Logique interne

* Toujours trié, mais **plusieurs clés identiques** possibles.
* Idéal pour stocker des valeurs **classées** mais non uniques (scores, notes, logs triés…).

### ⚡ Complexités

Même que `set` (O(log n)), mais `count()` peut retourner > 1.

### 🧩 Exemple

```cpp
std::multiset<int> notes = {10, 15, 10, 20};
std::cout << notes.count(10); // 2
```

---

## 🧩 `std::map<Key,Value>`

> Table associative triée par clé

### 📚 Logique interne

* Chaque élément = `std::pair<const Key, Value>`.
* Les **clés sont uniques** et triées.
* Basé sur un arbre équilibré → **O(log n)** pour accès, insertion, suppression.

### ⚡ Complexités

| Opération            | Complexité |
| -------------------- | ---------- |
| Insertion            | O(log n)   |
| Suppression          | O(log n)   |
| Recherche (`find`)   | O(log n)   |
| Accès (`operator[]`) | O(log n)   |

### 🧠 Cas d’usage

* Dictionnaire clé → valeur (login → joueur, id → objet, etc.)
* Quand tu veux à la fois **ordre** et **associativité**.

### 🧩 Exemple

```cpp
std::map<std::string, int> scores;
scores["Jean"] = 15;
scores["Marie"] = 18;
for (auto& [nom, val] : scores)
    std::cout << nom << " : " << val << "\n";
```

### ⚠️ Attention

* Si la clé n’existe pas, `operator[]` **crée une entrée par défaut** (0 pour les entiers, etc.).
  → utilise `find()` si tu veux juste vérifier.

---

## 🧩 `std::multimap<Key,Value>`

> Comme `map`, mais **permet plusieurs valeurs pour une même clé**

### 📚 Logique interne

* Toujours trié selon les clés.
* Chaque clé peut être associée à **plusieurs valeurs distinctes**.
* Les itérateurs renvoyés par `equal_range(key)` permettent de parcourir toutes les valeurs d’une clé donnée.

### ⚡ Complexités

Identiques à `map` (O(log n)).

### 🧠 Cas d’usage

* Index inversés, bases de données : un joueur → plusieurs serveurs, un mot → plusieurs traductions, etc.

### 🧩 Exemple

```cpp
std::multimap<std::string, int> points;
points.insert({"Jean", 10});
points.insert({"Jean", 20});
auto [beg, end] = points.equal_range("Jean");
for (auto it = beg; it != end; ++it)
    std::cout << it->second << " "; // 10 20
```

---

# 🧩 4. Comparatif résumé

| Conteneur  | Ordonné | Doublons | Accès aléatoire | Insertion rapide | Complexité principale      | Cas typiques                  |
| ---------- | ------- | -------- | --------------- | ---------------- | -------------------------- | ----------------------------- |
| `vector`   | ❌       | ✅        | ✅               | ❌ (sauf fin)     | O(1) accès, O(n) insertion | tableaux dynamiques           |
| `list`     | ❌       | ✅        | ❌               | ✅ (n’importe où) | O(1) insertion, O(n) accès | files chaînées, logs          |
| `deque`    | ❌       | ✅        | ✅               | ✅ (début/fin)    | O(1) extrémités            | files doubles                 |
| `set`      | ✅       | ❌        | ❌               | ✅ (O(log n))     | O(log n)                   | ensembles uniques             |
| `multiset` | ✅       | ✅        | ❌               | ✅ (O(log n))     | O(log n)                   | ensembles avec doublons       |
| `map`      | ✅       | ❌        | ❌               | ✅ (O(log n))     | O(log n)                   | dico clé→valeur               |
| `multimap` | ✅       | ✅        | ❌               | ✅ (O(log n))     | O(log n)                   | index inversé, relations n..n |

---

# 🧭 5. Choisir le bon conteneur

| Besoin                                    | Choix idéal |
| ----------------------------------------- | ----------- |
| Accès rapide par index, tableau dynamique | `vector`    |
| Insertion/suppression fréquente au milieu | `list`      |
| Accès rapide début/fin                    | `deque`     |
| Ensemble de valeurs uniques triées        | `set`       |
| Ensemble trié avec doublons autorisés     | `multiset`  |
| Dictionnaire (clé → valeur)               | `map`       |
| Dictionnaire à clés multiples             | `multimap`  |

---

# 🧠 6. Petits rappels de syntaxe utiles

```cpp
// Accès
v.at(i);     // vérifie les bornes
v[i];        // plus rapide, pas de vérification
s.find(x);   // retourne iterator ou end()

// Insertion / Suppression
m.insert({key, value});
m.erase(key);
s.emplace(x);  // construit directement l’objet

// Itérateurs
for (auto it = s.begin(); it != s.end(); ++it)
    std::cout << *it;

// Initialisation
std::set<int> s = {1,2,3};
std::map<std::string,int> m = {{"A",1},{"B",2}};
```

---

[...retorn en rèire](../menu.md)