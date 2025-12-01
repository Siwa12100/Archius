# C++ — Gestion des Erreurs et Exceptions

[...retorn en rèire](../menu.md)

---

## 🔹 1. Pourquoi des exceptions ?

Avant les exceptions, les fonctions en C utilisaient des **codes d’erreurs** (`-1`, `NULL`, etc.).
En C++, on préfère **remonter les erreurs automatiquement** avec des `throw`, pour séparer **la logique normale** de **la logique d’erreur**.

---

# ⚙️ 2. Les bases : `try`, `throw`, `catch`

---

## 🧩 `throw`

> Sert à **signaler** qu’une erreur est survenue.

### 📚 Syntaxe

```cpp
if (x == 0)
    throw std::runtime_error("Division par zéro !");
```

💡 On peut lancer n’importe quel type :

* un entier : `throw 42;`
* une chaîne : `throw "Erreur";`
* un objet d’exception standard : `throw std::invalid_argument("x<0");`

---

## 🧩 `try` / `catch`

> Sert à **intercepter** les exceptions pour éviter l’arrêt brutal du programme.

### 📚 Syntaxe

```cpp
try {
    riskyFunction();
}
catch (const std::exception& e) {
    std::cerr << "Erreur : " << e.what() << "\n";
}
catch (...) {
    std::cerr << "Erreur inconnue !\n";
}
```

* Le bloc `try` contient le code qui **peut échouer**.
* Le bloc `catch` capture les erreurs selon leur **type**.
* Le `catch(...)` attrape **toutes** les exceptions (utile en dernier recours).

---

## 🧩 Exemple complet

```cpp
#include <iostream>
#include <stdexcept>

double divide(double a, double b) {
    if (b == 0)
        throw std::invalid_argument("Division par zéro");
    return a / b;
}

int main() {
    try {
        std::cout << divide(10, 0) << "\n";
    }
    catch (const std::invalid_argument& e) {
        std::cerr << "Erreur mathématique : " << e.what() << "\n";
    }
    catch (...) {
        std::cerr << "Erreur inconnue\n";
    }
}
```

🧭 Sortie :

```
Erreur mathématique : Division par zéro
```

---

# 🧩 3. Types d’exceptions standards

C++ propose plusieurs classes d’erreurs dans `<stdexcept>` :

| Classe                  | Signification                               | Exemple                                |
| ----------------------- | ------------------------------------------- | -------------------------------------- |
| `std::runtime_error`    | erreur à l’exécution                        | disque plein, division par 0           |
| `std::logic_error`      | erreur logique du programme                 | argument invalide                      |
| `std::out_of_range`     | index hors bornes                           | `v.at(10)` sur un vector de 5 éléments |
| `std::invalid_argument` | argument incorrect                          | racine carrée de négatif               |
| `std::length_error`     | conteneur trop grand                        | dépassement de taille maximale         |
| `std::bad_alloc`        | échec d’allocation mémoire                  | `new` échoue                           |
| `std::exception`        | classe de base de toutes les exceptions STL | polymorphisme possible                 |

---

## 🧠 Exemple d’usage concret

```cpp
#include <vector>
#include <iostream>
#include <stdexcept>

int main() {
    try {
        std::vector<int> v = {1,2,3};
        std::cout << v.at(5); // ⚠️ hors bornes
    }
    catch (const std::out_of_range& e) {
        std::cerr << "Erreur : " << e.what() << "\n";
    }
}
```

🧭 Sortie :

```
Erreur : vector::_M_range_check: __n (which is 5) >= this->size() (which is 3)
```

---

# 🧱 4. Création d’exceptions personnalisées

Tu peux créer tes propres classes d’erreurs pour ton domaine :

```cpp
class FichierIntrouvableException : public std::runtime_error {
public:
    explicit FichierIntrouvableException(const std::string& nom)
        : std::runtime_error("Fichier introuvable : " + nom) {}
};
```

Et les utiliser :

```cpp
if (!std::filesystem::exists("data.txt"))
    throw FichierIntrouvableException("data.txt");
```

---

# 🔧 5. Gestion des flux (`iostream`)

---

## 🧩 Les flux et leurs états

Chaque flux (`std::cin`, `std::cout`, `std::ifstream`, `std::ofstream`, etc.)
possède **des drapeaux d’état internes** (bits) :

| Bit       | Signification                     | Méthode  | Exemple                              |
| --------- | --------------------------------- | -------- | ------------------------------------ |
| `goodbit` | Tout va bien                      | `good()` | Aucun problème                       |
| `eofbit`  | Fin du fichier atteinte           | `eof()`  | lecture terminée                     |
| `failbit` | Erreur de lecture ou mauvais type | `fail()` | lire un int mais recevoir une lettre |
| `badbit`  | Erreur grave (I/O système)        | `bad()`  | disque débranché, corruption flux    |

---

## 🧩 Exemple de lecture robuste

```cpp
#include <iostream>
#include <fstream>

int main() {
    std::ifstream f("data.txt");
    if (!f) {
        std::cerr << "Impossible d’ouvrir le fichier\n";
        return 1;
    }

    int x;
    while (f >> x) {
        std::cout << "Lu : " << x << "\n";
    }

    if (f.eof())
        std::cout << "Fin du fichier atteinte\n";
    else if (f.fail())
        std::cerr << "Format invalide (mauvais type)\n";
    else if (f.bad())
        std::cerr << "Erreur d’entrée/sortie grave\n";
}
```

---

## 🧩 Vérification combinée

```cpp
if (!f.good()) {
    if (f.eof()) std::cout << "EOF\n";
    if (f.fail()) std::cout << "Fail\n";
    if (f.bad()) std::cout << "Bad\n";
}
```

🧠 Les flux **ne lèvent pas d’exceptions** par défaut : tu dois tester leur état manuellement.
Mais tu peux demander à un flux de **lancer des exceptions** :

```cpp
f.exceptions(std::ifstream::failbit | std::ifstream::badbit);
```

---

# 🧩 6. Cas pratiques de DS

| Cas                                                        | Solution                               |
| ---------------------------------------------------------- | -------------------------------------- |
| Lire un entier depuis un fichier et gérer erreur de format | `fail()`                               |
| Vérifier la fin d’un fichier                               | `eof()`                                |
| Intercepter une erreur de logique (indice hors borne)      | `std::out_of_range`                    |
| Gérer un fichier manquant                                  | exception personnalisée                |
| Diviser deux valeurs saisies par l’utilisateur             | `throw std::invalid_argument` si div/0 |

---

# ⚡ 7. Bonnes pratiques

✅ **Toujours lancer des objets**, pas des primitives (`throw std::runtime_error("msg")`).
✅ **Toujours attraper par référence const** (`catch(const std::exception& e)`).
✅ **Nettoyer automatiquement** grâce au RAII (`std::ifstream` ferme le fichier à la fin du scope).
✅ **Ne pas tout encapsuler dans un seul `try`** — capture les exceptions au niveau logique.
✅ **Ne pas abuser des exceptions** pour la logique normale (réservées aux erreurs).

---

# 🧭 8. Résumé visuel

| Élément  | Rôle              | Exemple                              | Niveau         |
| -------- | ----------------- | ------------------------------------ | -------------- |
| `throw`  | Lancer une erreur | `throw std::runtime_error("erreur")` | signaler       |
| `try`    | Protéger du code  | `try { ... }`                        | surveiller     |
| `catch`  | Intercepter       | `catch(const std::exception& e)`     | traiter        |
| `good()` | Flux OK           | `if(f.good())`                       | état normal    |
| `fail()` | Mauvais format    | `if(f.fail())`                       | erreur lecture |
| `eof()`  | Fin fichier       | `if(f.eof())`                        | fin de flux    |
| `bad()`  | Erreur système    | `if(f.bad())`                        | grave          |

---

[...retorn en rèire](../menu.md)