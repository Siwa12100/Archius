# C++ — Organisation du code, Namespaces et Includes

[...retorn en rèire](../menu.md)

---

## 🔹 1. Namespaces — “espaces de noms”

---

### 🧩 Pourquoi ?

Les **namespaces** (espaces de noms) servent à **éviter les collisions** de noms entre plusieurs fichiers, bibliothèques ou modules.

👉 En C++, **tout le code global est visible partout**, donc deux fonctions `init()` dans des fichiers différents posent problème.

---

### 🧱 Exemple sans namespace

```cpp
// player.cpp
void init() { std::cout << "Init joueur\n"; }

// server.cpp
void init() { std::cout << "Init serveur\n"; }

// main.cpp
int main() {
    init(); // ❌ Ambigu : quelle fonction ?
}
```

---

### ✅ Avec namespace

```cpp
// player.cpp
namespace game {
    void init() { std::cout << "Init joueur\n"; }
}

// server.cpp
namespace net {
    void init() { std::cout << "Init serveur\n"; }
}

// main.cpp
int main() {
    game::init();
    net::init();
}
```

💡 Tu peux imaginer les namespaces comme des **“dossiers logiques”** pour ton code.

---

## 🧩 2. Déclaration et utilisation

### 📚 Syntaxe

```cpp
namespace app {
    int version = 1;
    void hello() { std::cout << "Hello\n"; }
}
```

Appel :

```cpp
app::hello();
```

---

### 🧩 Namespace imbriqué

```cpp
namespace app {
    namespace util {
        void log(std::string msg);
    }
}
```

Depuis C++17, on peut écrire :

```cpp
namespace app::util {
    void log(std::string msg);
}
```

Appel :

```cpp
app::util::log("Salut !");
```

---

### 🧩 `using` pour simplifier temporairement

```cpp
using namespace app;
hello();  // plus besoin d’écrire app::
```

⚠️ **À utiliser avec prudence** :

* ✅ OK dans un petit `.cpp`
* ❌ À éviter dans les `.h` (risque de collisions globales)

---

### 🧩 Alias de namespace

```cpp
namespace srv = app::network::server;
srv::connect();
```

---

## 🧩 3. Organisation typique d’un projet avec namespaces

Structure :

```
includes/
  app/
    domain/
      Player.hpp
      Server.hpp
    service/
      GameService.hpp
src/
  domain/
    Player.cpp
    Server.cpp
  service/
    GameService.cpp
main.cpp
```

Exemple dans `Player.hpp` :

```cpp
#pragma once
#include <string>

namespace app::domain {

class Player {
    std::string name;
public:
    Player(std::string n);
    void afficher() const;
};

} // namespace app::domain
```

Et dans `Player.cpp` :

```cpp
#include "app/domain/Player.hpp"
#include <iostream>

namespace app::domain {

Player::Player(std::string n) : name(std::move(n)) {}
void Player::afficher() const {
    std::cout << "Joueur : " << name << "\n";
}

} // namespace app::domain
```

💡 **Chaque entité logique → son propre namespace** :

* `app::domain` → classes métiers
* `app::service` → logique applicative
* `app::io` → gestion des fichiers et flux
* `app::util` → outils divers

---

## ⚙️ 4. Includes — comprendre leur fonctionnement

---

### 🧩 `#include`

> Insère le **contenu d’un fichier** dans un autre **avant la compilation** (au moment de la *précompilation*).

```cpp
#include <iostream>        // bibliothèque standard
#include "app/domain/Player.hpp"  // fichier du projet
```

### 📦 Deux syntaxes :

| Syntaxe    | Cherche où ?                                         | Usage                              |
| ---------- | ---------------------------------------------------- | ---------------------------------- |
| `<header>` | dans les **dossiers système** (`/usr/include`)       | bibliothèques standard ou externes |
| `"header"` | d’abord dans le **répertoire courant**, puis système | fichiers du projet                 |

---

### 🧠 Logique :

Quand tu compiles un projet, le préprocesseur :

1. Copie le contenu des `#include` dans le fichier source.
2. Compile le tout comme un seul gros fichier.

---

### ⚠️ Problème : **inclusions multiples**

Exemple :

```cpp
// A.hpp
struct A {};

// B.hpp
#include "A.hpp"
struct B {};

// main.cpp
#include "A.hpp"
#include "B.hpp"
```

> `A.hpp` est inclus **deux fois** → redefinition error ❌

---

## 🧩 5. Gardes d’inclusion

### 🧱 Solution classique : `#ifndef` / `#define` / `#endif`

*(aussi appelée **“include guard”**)*

```cpp
#ifndef PLAYER_HPP
#define PLAYER_HPP

class Player { /* ... */ };

#endif // PLAYER_HPP
```

🧠 Fonctionnement :

* Si `PLAYER_HPP` n’est **pas encore défini**, le code est lu et `PLAYER_HPP` est défini.
* Si le fichier est inclus à nouveau, il sera **ignoré**.

---

### ✅ Solution moderne : `#pragma once`

> Alternative non standard mais **universellement supportée** (GCC, Clang, MSVC)

```cpp
#pragma once
class Player { /* ... */ };
```

💡 Avantages :

* Plus court, plus lisible.
* Évite les fautes de frappe sur les macros.
* Même efficacité.

✅ Recommandé pour **tous les projets modernes**.
❌ À éviter seulement si tu vises un compilateur exotique non compatible (cas rares).

---

## 🧩 6. Bonnes pratiques avec includes

### 📘 Règles générales

1. **Toujours protéger les headers** (`#pragma once` ou `#ifndef`).
2. **Jamais d’`using namespace` dans un `.hpp`**.
3. **Inclure uniquement ce qui est nécessaire.**
4. **Préférer les déclarations anticipées (forward declarations)** si possible.
5. **Toujours inclure ton propre header en premier** dans un `.cpp`.

---

### 🧩 Exemple d’ordre d’includes dans un `.cpp`

```cpp
// 1️⃣ ton propre header
#include "app/service/GameService.hpp"

// 2️⃣ headers de la STL
#include <iostream>
#include <vector>

// 3️⃣ headers externes (libs)
#include <nlohmann/json.hpp>

// 4️⃣ autres headers du projet
#include "app/domain/Player.hpp"
#include "app/domain/Server.hpp"
```

---

### 🧩 Déclaration anticipée (“forward declaration”)

> Permet de déclarer l’existence d’une classe sans inclure tout le header.

```cpp
// GameService.hpp
#pragma once
#include <string>

namespace app::domain {
class Player; // déclaration anticipée
}

namespace app::service {
class GameService {
    app::domain::Player* player; // OK : pointeur
public:
    void setPlayer(app::domain::Player* p);
};
}
```

👉 Évite d’inclure inutilement `Player.hpp` (gain de temps de compilation).
👉 Tu l’incluras **dans le .cpp** pour les détails d’implémentation.

---

## 🧩 7. Résumé global

| Élément                  | Rôle                                    | À retenir                    |
| ------------------------ | --------------------------------------- | ---------------------------- |
| `namespace`              | Regrouper logiquement le code           | Évite les collisions         |
| `using namespace`        | Simplifie la syntaxe                    | ⚠️ jamais dans un `.hpp`     |
| `#include`               | Insère un fichier avant compilation     | Respecter l’ordre et limiter |
| `#ifndef / #pragma once` | Empêche inclusion multiple              | `#pragma once` = moderne     |
| Forward declaration      | Déclare une classe sans include complet | Réduit la dépendance         |
| Organisation projet      | Séparer headers / sources / namespaces  | Respecter logique métier     |
| Nom des macros de garde  | En MAJUSCULES + unique                  | ex. `PLAYER_HPP`             |

---

## 🧠 8. Exemple complet de projet bien organisé

```
includes/
  app/
    domain/
      Player.hpp
    service/
      GameService.hpp
src/
  domain/
    Player.cpp
  service/
    GameService.cpp
main.cpp
```

**Player.hpp**

```cpp
#pragma once
#include <string>
namespace app::domain {
class Player {
    std::string name;
public:
    Player(std::string n);
    void afficher() const;
};
}
```

**Player.cpp**

```cpp
#include "app/domain/Player.hpp"
#include <iostream>
namespace app::domain {
Player::Player(std::string n) : name(std::move(n)) {}
void Player::afficher() const { std::cout << name; }
}
```

**GameService.hpp**

```cpp
#pragma once
#include <memory>
namespace app::domain { class Player; }

namespace app::service {
class GameService {
    std::shared_ptr<app::domain::Player> joueur;
public:
    void start();
};
}
```

**GameService.cpp**

```cpp
#include "app/service/GameService.hpp"
#include "app/domain/Player.hpp"
#include <iostream>
namespace app::service {
void GameService::start() {
    std::cout << "Démarrage du jeu...\n";
}
}
```

---

[...retorn en rèire](../menu.md)