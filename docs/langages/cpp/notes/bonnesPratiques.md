# C++ — Bonnes Pratiques et Nuances

[...retorn en rèire](../menu.md)

---

## 🧩 1. La “forme canonique” d’une classe

*(aussi appelée “Rule of Three / Five”)*

> En C++, certaines classes gèrent des **ressources** (mémoire, fichier, socket...).
> Il faut donc définir proprement **comment elles se construisent, se copient et se détruisent.**

---

### ⚙️ Les 4 membres fondamentaux (forme canonique classique)

| Élément                            | Rôle                                    | Appelé quand…               |
| ---------------------------------- | --------------------------------------- | --------------------------- |
| 🧱 **Constructeur par défaut**     | initialise un objet vide ou par défaut  | création simple (`T x;`)    |
| 📋 **Constructeur de copie**       | duplique un objet existant              | `T y = x;`                  |
| 🧹 **Destructeur**                 | libère les ressources                   | quand l’objet sort du scope |
| 🔁 **Opérateur d’affectation (=)** | remplace le contenu d’un objet existant | `a = b;`                    |

---

### 🧩 Exemple complet

```cpp
class Joueur {
    std::string nom;
    int score;
    int* historique; // ressource dynamique

public:
    // Constructeur par défaut
    Joueur() : nom("inconnu"), score(0), historique(nullptr) {}

    // Constructeur paramétré
    Joueur(std::string n, int s)
        : nom(std::move(n)), score(s), historique(new int[s]{}) {}

    // Constructeur de copie
    Joueur(const Joueur& other)
        : nom(other.nom), score(other.score)
    {
        if (other.historique) {
            historique = new int[score];
            std::copy(other.historique, other.historique + score, historique);
        } else historique = nullptr;
    }

    // Opérateur d’affectation
    Joueur& operator=(const Joueur& other) {
        if (this != &other) { // auto-affectation
            delete[] historique;
            nom = other.nom;
            score = other.score;
            if (other.historique) {
                historique = new int[score];
                std::copy(other.historique, other.historique + score, historique);
            } else historique = nullptr;
        }
        return *this;
    }

    // Destructeur
    ~Joueur() {
        delete[] historique;
    }
};
```

### 🧠 Pourquoi c’est important

* Évite les **fuites mémoire** (new sans delete).
* Garantit un comportement cohérent en cas de copie ou d’affectation.
* C’est la **base du RAII** (Resource Acquisition Is Initialization).

---

## 🧩 2. La “Rule of Five” (C++11 et +)

Avec C++ moderne, il faut aussi penser au **déplacement** (`move semantics`) :

| Élément                       | Rôle                        |
| ----------------------------- | --------------------------- |
| `Joueur(Joueur&&)`            | Constructeur de déplacement |
| `Joueur& operator=(Joueur&&)` | Affectation par déplacement |

Ces deux membres permettent de **transférer la ressource** plutôt que de la copier — gain de performance (pas de double allocation).

---

### 🧩 Exemple (simplifié)

```cpp
Joueur(Joueur&& other) noexcept
  : nom(std::move(other.nom)),
    score(other.score),
    historique(other.historique)
{
    other.historique = nullptr; // transfert de propriété
}
```

---

## ⚡ 3. Optimisations

---

### 🧩 `inline` — Éviter l’appel de fonction

> Demande au compilateur d’**insérer le corps de la fonction directement** à l’appel (pas de saut d’instruction).

### 📚 Syntaxe

```cpp
inline int carre(int x) { return x * x; }
```

### 💡 Détails

* Utile pour les **petites fonctions** très souvent appelées.
* Automatique pour les fonctions **définies dans un header** (depuis C++17).
* Le compilateur décide in fine si l’inlining est pertinent (ce n’est qu’une **suggestion**).

### ⚠️ Mauvaise pratique

* Ne jamais abuser sur des fonctions lourdes → code gonflé et cache moins efficace.

---

### 🧩 `constexpr` — Calcul à la compilation

> Indique que la valeur peut être **évaluée à la compilation**.

### 📚 Syntaxe

```cpp
constexpr int carre(int x) { return x * x; }

constexpr int n = carre(5); // calculé à la compilation
```

### 🧠 Cas d’usage

* Constantes calculées à la compilation.
* Paramètres de templates.
* Fonctions pures sans effet de bord.

### ⚠️ Attention

* Les expressions doivent être **évaluable à compile-time** (pas d’I/O, pas de malloc).
* C’est une **garantie de performance et de sécurité**.

---

## 🧩 4. Sécurité et robustesse

---

### 🧩 Vérification des bornes : `at()` vs `operator[]`

| Méthode   | Vérifie les bornes ? | Complexité | En cas d’erreur                           |
| --------- | -------------------- | ---------- | ----------------------------------------- |
| `v[i]`    | ❌ Non                | O(1)       | Comportement indéfini (plantage possible) |
| `v.at(i)` | ✅ Oui                | O(1)       | Lance `std::out_of_range`                 |

### 🧩 Exemple

```cpp
std::vector<int> v = {1,2,3};
std::cout << v[10];    // ⚠️ Comportement indéfini
std::cout << v.at(10); // 💥 Lance une exception
```

### 🧠 Règle :

* En **code critique** (sécurité, I/O, calcul sensible) → `at()`.
* En **performance pure** (algo interne) → `operator[]`.

---

### 🧩 Initialisation systématique

> Toujours initialiser tes variables (même à 0).

```cpp
int x{};          // plutôt que int x;
std::string s{};  // chaîne vide garantie
```

---

### 🧩 Éviter les fuites mémoire

> Utilise les **pointeurs intelligents** :

```cpp
#include <memory>
auto joueur = std::make_unique<Joueur>("Jean", 10);
```

* `std::unique_ptr` : propriétaire unique
* `std::shared_ptr` : référence partagée (compteur)
* `std::weak_ptr` : pointeur non propriétaire (évite cycles)

---

### 🧩 RAII (Resource Acquisition Is Initialization)

> Toute ressource doit être **acquise dans un constructeur** et **libérée dans le destructeur**.
> Aucune fonction “cleanup()” manuelle ne devrait être nécessaire.

```cpp
std::ofstream f("data.txt"); // ouvert ici
// ...
// fermé automatiquement à la fin du scope
```

---

### 🧩 Const-correctness

> Déclare en `const` tout ce qui ne doit pas changer.

```cpp
class Livre {
public:
    std::string titre;
    int nbPages;
    void afficher() const { std::cout << titre; } // ne modifie rien
};
```

✅ → Empêche les erreurs de modification involontaire.
✅ → Permet d’appeler la méthode sur des objets const.

---

## 🧠 5. Résumé global

| Concept             | Objectif                                | À retenir                              |
| ------------------- | --------------------------------------- | -------------------------------------- |
| **Forme canonique** | Gérer proprement la vie d’une ressource | Constructeurs, destructeur, opérateurs |
| **Rule of Five**    | Gérer copie **et** déplacement          | Ajoute move constructor / move assign  |
| **inline**          | Supprime le coût d’appel de fonction    | Petites fonctions fréquentes           |
| **constexpr**       | Calcul à la compilation                 | Constantes sûres et rapides            |
| **at()**            | Accès sûr (avec exception)              | Vérifie les bornes                     |
| **operator[]**      | Accès rapide                            | Pas de vérification                    |
| **RAII**            | Nettoyage automatique                   | Fermeture fichiers, mémoire            |
| **const**           | Sécurité des données                    | Garantit l’immuabilité                 |

---

[...retorn en rèire](../menu.md)