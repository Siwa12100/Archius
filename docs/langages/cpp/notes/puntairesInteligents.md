# 📘 Pointeurs intelligents + auto (C++ moderne)

[...retorn en rèire](../menu.md)

---

## 🔰 1. Pourquoi les pointeurs intelligents ?  
Avant C++11, on écrivait :

```cpp
Serveur* s = new Serveur(1, "Itaya");
delete s;  // il ne faut pas oublier !!!
```

Si tu oublies `delete` → **fuite mémoire**.  
Si tu delete deux fois → **crash**.

C++ moderne dit : **NE JAMAIS utiliser directement `new` et `delete`**  
→ tu utilises des *pointeurs intelligents* à la place.

---

## 📦 2. Le but : gérer automatiquement la mémoire (RAII)

RAII = *Resource Acquisition Is Initialization*  
→ quand un objet sort du scope, on libère automatiquement les ressources.

Avec un pointeur intelligent, **le delete est automatique** → jamais d’oubli.

---

## 🟢 3. `auto` — Le mot-clé qui devine les types

### 🔹 Principe  
`auto` = “le compilateur devine le type pour toi”.

Exemple :

```cpp
auto x = 10;       // x est int
auto s = "test";   // s est const char*
```

Très utile pour les pointeurs intelligents :

```cpp
auto ptr = std::make_unique<Serveur>(1, "Itaya");
```

→ tu n’écris pas le type long et lourd :

```cpp
std::unique_ptr<Serveur> ptr = std::make_unique<Serveur>(1, "Itaya");
```

### Pourquoi c’est bien ?
- moins d’écriture  
- plus clair  
- moins d’erreurs de type  
- recommandé par les normes C++ modernes

---

## 🟩 4. `std::unique_ptr` — *Possession exclusive*

### 🎯 Idée simple  
Un `unique_ptr` = **UNE SEULE personne possède l’objet**.

→ impossible de copier  
→ transfert de propriété possible

### Exemple :

```cpp
auto p = std::make_unique<Serveur>(1, "Itaya");
```

À la fin du scope → l’objet est automatiquement `delete`.

### 🚫 Copie interdite :

```cpp
auto p2 = p;  // ❌ erreur : unique_ptr ne peut PAS être copié
```

### ✔️ On peut *déplacer* (transfert de possession)

```cpp
auto p2 = std::move(p); // OK
```

👉 Après ça, `p` devient `nullptr`.

### Quand utiliser `unique_ptr` ?
- quand **une seule personne** doit posséder l’objet  
- services internes  
- structures d’arbre (parent → enfants)  
- ressources système uniques  

---

## 🟦 5. `std::shared_ptr` — *Possession partagée*

### 🎯 Idée simple  
`shared_ptr` = **plusieurs personnes peuvent posséder l’objet**.

→ compteur de références  
→ quand le compteur tombe à 0 → delete automatique

Analogie :  
📘 Un livre dans une bibliothèque : tant qu’il y a un lecteur → le livre reste.

### Exemple :

```cpp
auto s = std::make_shared<Serveur>(1, "Itaya");
```

Copie autorisée :

```cpp
auto s2 = s;  // OK : compteur++, pas de copie physique
```

Quand `s` ET `s2` sortent du scope → delete auto.

### 👍 Quand utiliser `shared_ptr` ?
- plusieurs couches doivent garder une copie (service, controller, etc.)  
- cycles de vie complexes  
- interactions multiples  

### ⚠️ Important : si tu n’en as pas besoin → préfère `unique_ptr`.

---

## 🟪 6. `std::weak_ptr` — *Version non-propriétaire*

### 🎯 Idée simple  
`weak_ptr` = observer un objet possédé par un `shared_ptr`, sans augmenter le compteur.

Pourquoi utile ?  
→ éviter les **références circulaires** :

A pointe vers B, B pointe vers A → jamais delete → fuite mémoire.

`weak_ptr` casse le cycle.

---

## 🟧 7. Pourquoi utiliser `make_unique` et `make_shared` ?

### 🔥 Toujours utiliser :

```cpp
auto p = std::make_unique<T>(...);
auto s = std::make_shared<T>(...);
```

Et jamais :

```cpp
std::unique_ptr<T> p(new T(...));        // ❌
std::shared_ptr<T> s(new T(...));        // ❌
```

### Avantages :
- **plus sûr** (exception‑safe)
- **plus performant** (`make_shared` alloue tout en 1 bloc mémoire)
- **plus lisible**

---

## 🧠 8. Comparatif ultra simple

| Type                | Propriété | Copiable ? | Delete auto ? | Usage conseillé |
|---------------------|-----------|------------|----------------|-----------------|
| `unique_ptr`        | exclusive | ❌ non      | ✔️ oui         | un seul owner |
| `shared_ptr`        | partagée  | ✔️ oui      | ✔️ oui         | plusieurs owners |
| `weak_ptr`          | aucune    | ✔️ oui      | ❌ non         | observer un shared_ptr |
| `auto`              | aucun     | n/a        | n/a            | simplifier les types |

---

## 🧪 9. Exemples pédagogiques simples

---

### 🎒 unique_ptr : 1 propriétaire

```cpp
auto serveur = std::make_unique<Serveur>(1, "Itaya");
std::cout << serveur->getNom();
```

À la fin → delete auto.

---

### 👥 shared_ptr : plusieurs propriétaires

```cpp
auto s1 = std::make_shared<Serveur>(1, "Itaya");
{
    auto s2 = s1;  // compteur : 2
}   // s2 meurt → compteur : 1

// À la fin du main → compteur : 0 → delete
```

---

### 🎭 weak_ptr : éviter les cycles

```cpp
std::shared_ptr<A> a;
std::shared_ptr<B> b;

a->ami = b;
b->ami = a;        // ❌ fuite mémoire

// Solution :
b->ami = std::weak_ptr<A>(a);
```

---

## 🧩 10. Dans ton architecture (services / repo)

### Recommandation 100% PRO

- Repository stocké dans un **shared_ptr**  
  → car le service et d’autres composants doivent le partager.

- Service aussi en **shared_ptr**  
  → plusieurs controllers peuvent l’utiliser.

- Modèles (`Serveur`) → objets *normaux*, pas en pointeurs.

- Relations / propriétés → `unique_ptr` si tu veux posséder quelque chose.

---

## 🎯 11. Résumé simple pour ne jamais te tromper

### 🟢 Tu veux un *objet possédé par une seule entité*  
→ **unique_ptr**

### 🔵 Tu veux *plusieurs propriétaires*  
→ **shared_ptr**

### 🟣 Tu veux *observer sans posséder*  
→ **weak_ptr**

### 🟡 Tu veux *éviter d’écrire un type long*  
→ **auto**

### 🟠 Tu veux créer un pointeur intelligent  
→ `std::make_unique<T>(...)`  
→ `std::make_shared<T>(...)`

---

[...retorn en rèire](../menu.md)