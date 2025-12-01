# Standard Template Library (STL) — Partie 2 : Les Algorithmes

[...retorn en rèire](../menu.md)

---

## 🔹 Introduction

Les **algorithmes STL** sont des **fonctions génériques** qui opèrent sur des **plages** définies par deux **itérateurs** :

```cpp
std::sort(v.begin(), v.end());
```

👉 Un algorithme **ne dépend pas du conteneur**, seulement de la **catégorie d’itérateur** fournie (`input`, `forward`, `bidirectional`, `random access`, etc.).

* Compatible avec `vector`, `list`, `deque`, `set`, etc.
* Fonctionne avec tout ce qui expose `begin()` / `end()`.

---

# ⚙️ 1. Tri et recherche

---

## 🧩 `std::sort`

> Trie une plage d’éléments par **ordre croissant** par défaut (via `operator<`).

### 📚 Syntaxe

```cpp
std::sort(first, last);                      // ordre croissant
std::sort(first, last, std::greater<int>()); // ordre décroissant
```

### ⚡ Complexité

* **O(n log n)** en moyenne
* Nécessite des **itérateurs aléatoires** (vector, deque)

### 🧠 Cas d’usage

* Trier un tableau, un score board, une liste d’objets comparables.

### 🧩 Exemple

```cpp
std::vector<int> v = {5, 2, 8, 1};
std::sort(v.begin(), v.end());
for (int x : v) std::cout << x << " "; // 1 2 5 8
```

### 💡 Astuce

Tu peux trier avec un **lambda** :

```cpp
std::sort(v.begin(), v.end(), [](int a, int b){ return a > b; });
```

---

## 🧩 `std::binary_search`

> Vérifie si une **valeur existe** dans une **plage triée**.

### 📚 Syntaxe

```cpp
bool found = std::binary_search(first, last, value);
```

### ⚡ Complexité

* O(log n)
* Nécessite un **conteneur trié** (sinon résultat indéfini !)

### 🧠 Cas d’usage

* Vérifier rapidement la présence d’un élément dans un `vector` trié.

### 🧩 Exemple

```cpp
std::vector<int> v = {1, 2, 3, 5, 8};
if (std::binary_search(v.begin(), v.end(), 5))
    std::cout << "5 trouvé !";
```

---

## 🧩 `std::lower_bound` / `std::upper_bound`

> Recherche la **position d’insertion** d’un élément dans un conteneur trié.

### 📚 Syntaxe

```cpp
auto it = std::lower_bound(first, last, value);
```

* `lower_bound` → premier élément **≥ value**
* `upper_bound` → premier élément **> value**

### ⚡ Complexité

* O(log n)

### 🧠 Cas d’usage

* Trouver rapidement une **plage d’éléments égaux** (utile avec `multiset`, `multimap`).
* Insérer sans casser l’ordre.

### 🧩 Exemple

```cpp
std::vector<int> v = {1, 3, 3, 5, 7};
auto it = std::lower_bound(v.begin(), v.end(), 3);
std::cout << (it - v.begin()); // 1
```

---

# 🔧 2. Modification de séquences

---

## 🧩 `std::generate`

> Remplit une plage avec des **valeurs produites par une fonction**.

### 📚 Syntaxe

```cpp
std::generate(first, last, generator);
```

### 🧠 Cas d’usage

* Initialiser un tableau avec des valeurs calculées aléatoirement ou séquentiellement.

### 🧩 Exemple

```cpp
#include <random>

std::vector<int> v(5);
std::generate(v.begin(), v.end(), [](){ return rand() % 100; });
```

### ⚡ Complexité

* O(n)

---

## 🧩 `std::replace`

> Remplace toutes les occurrences d’une valeur par une autre.

### 📚 Syntaxe

```cpp
std::replace(first, last, old_value, new_value);
```

### 🧩 Exemple

```cpp
std::vector<int> v = {1, 2, 2, 3};
std::replace(v.begin(), v.end(), 2, 99); // 1 99 99 3
```

### ⚡ Complexité

* O(n)

### 🧠 Cas d’usage

* Nettoyer une liste, remplacer des valeurs par défaut, etc.

---

## 🧩 `std::remove` / `std::remove_if`

> Déplace les éléments à **garder** au début, et retourne un itérateur sur la nouvelle fin logique.
> ⚠️ **Ne supprime pas réellement les éléments du conteneur !**

### 📚 Syntaxe

```cpp
auto new_end = std::remove(first, last, value);
container.erase(new_end, container.end());
```

### 🧠 Cas d’usage

* Filtrer un `vector` sans recopier tout le contenu.

### 🧩 Exemple

```cpp
std::vector<int> v = {1,2,3,2,4};
v.erase(std::remove(v.begin(), v.end(), 2), v.end());
// v = {1,3,4}
```

### ⚡ Complexité

* O(n)

---

# 🔢 3. Opérations numériques

---

## 🧩 `std::accumulate`

> Calcule la **somme** (ou toute autre réduction) d’une séquence.

### 📚 Syntaxe

```cpp
auto sum = std::accumulate(first, last, init);
auto custom = std::accumulate(first, last, init, [](int a, int b){ return a + b*b; });
```

### ⚡ Complexité

* O(n)

### 🧠 Cas d’usage

* Moyennes, totaux, concaténations, etc.

### 🧩 Exemple

```cpp
#include <numeric>
std::vector<int> v = {1, 2, 3, 4};
int sum = std::accumulate(v.begin(), v.end(), 0); // 10
```

---

## 🧩 `std::inner_product`

> Produit scalaire entre deux séquences.

### 📚 Syntaxe

```cpp
auto res = std::inner_product(a.begin(), a.end(), b.begin(), init);
```

### ⚡ Complexité

* O(n)

### 🧠 Cas d’usage

* Calculs statistiques, comparaisons de vecteurs, machine learning (produit scalaire classique).

### 🧩 Exemple

```cpp
std::vector<int> a = {1, 2, 3};
std::vector<int> b = {4, 5, 6};
int dot = std::inner_product(a.begin(), a.end(), b.begin(), 0); // 1*4 + 2*5 + 3*6 = 32
```

---

# 🧩 4. Algorithmes de tri avancés

---

## 🧩 `std::partial_sort`

> Trie **les n premiers éléments** d’une séquence, sans trier le reste.

### 📚 Syntaxe

```cpp
std::partial_sort(first, middle, last);
```

* Trie la plage `[first, last)` de façon à ce que les **éléments `[first, middle)`** soient les plus petits, **triés**.

### ⚡ Complexité

* O(n log m) où *m = distance(first, middle)*

### 🧠 Cas d’usage

* Obtenir les **k plus petits éléments** rapidement (ex. top 10 scores).

### 🧩 Exemple

```cpp
std::vector<int> v = {9, 4, 7, 1, 3, 6};
std::partial_sort(v.begin(), v.begin() + 3, v.end());
// Les 3 premiers plus petits triés : [1,3,4]
```

---

## 🧩 `std::nth_element`

> Place le **nᵉ élément** à sa position finale comme s’il était trié,
> les plus petits avant, les plus grands après (mais non triés).

### 📚 Syntaxe

```cpp
std::nth_element(first, nth, last);
```

### ⚡ Complexité

* O(n) moyenne (partition rapide)

### 🧠 Cas d’usage

* Trouver une **médiane**, un **top k** sans trier tout le tableau.

### 🧩 Exemple

```cpp
std::vector<int> v = {9, 4, 7, 1, 3, 6};
std::nth_element(v.begin(), v.begin() + 2, v.end());
// v[2] est le 3e plus petit élément (tri partiel)
std::cout << v[2]; // 4
```

---

# 🧠 5. Itérateurs et compatibilité

Les algorithmes utilisent des **itérateurs** pour parcourir les conteneurs.

| Type d’itérateur        | Exemple de conteneurs | Autorise…                    |
| ----------------------- | --------------------- | ---------------------------- |
| `InputIterator`         | `istream_iterator`    | lecture séquentielle         |
| `ForwardIterator`       | `forward_list`        | parcours unique vers l’avant |
| `BidirectionalIterator` | `list`, `set`, `map`  | aller et retour              |
| `RandomAccessIterator`  | `vector`, `deque`     | accès direct, `+` et `-`     |

> Les algorithmes comme `sort` nécessitent **random access**, tandis que `find`, `replace` marchent avec tout.

---

# 🧩 6. Comparatif rapide

| Algorithme      | Catégorie    | Complexité | Besoin d’ordre ? | Cas typique               |
| --------------- | ------------ | ---------- | ---------------- | ------------------------- |
| `sort`          | tri          | O(n log n) | ✅                | trier un tableau          |
| `binary_search` | recherche    | O(log n)   | ✅                | vérifier existence        |
| `lower_bound`   | recherche    | O(log n)   | ✅                | trouver plage d’insertion |
| `generate`      | modification | O(n)       | ❌                | remplir un tableau        |
| `replace`       | modification | O(n)       | ❌                | remplacer valeurs         |
| `remove`        | modification | O(n)       | ❌                | filtrer un vector         |
| `accumulate`    | numérique    | O(n)       | ❌                | somme, moyenne            |
| `inner_product` | numérique    | O(n)       | ❌                | produit scalaire          |
| `partial_sort`  | tri avancé   | O(n log k) | ✅                | top k                     |
| `nth_element`   | tri avancé   | O(n)       | ✅                | médiane, percentile       |

---

# 🧭 7. Combinaisons utiles

| Besoin                      | Solution STL                                                    |
| --------------------------- | --------------------------------------------------------------- |
| Trier et supprimer doublons | `sort + unique + erase`                                         |
| Top 5 plus grands           | `partial_sort(v.begin(), v.begin()+5, v.end(), std::greater{})` |
| Trouver la médiane          | `nth_element(v.begin(), v.begin()+v.size()/2, v.end())`         |
| Somme et moyenne            | `accumulate` puis `/ v.size()`                                  |
| Filtrer des valeurs         | `remove_if` + `erase`                                           |
| Remplir une série           | `iota(v.begin(), v.end(), start)` ou `generate`                 |

---

[...retorn en rèire](../menu.md)