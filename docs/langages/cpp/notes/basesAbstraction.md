# C++ — Abstraction & Injection de Dépendances

[...retorn en rèire](../menu.md)

---

## 🔹 1) Pourquoi l’abstraction en C++ ?

* **Dissocier** le *quoi* (contrat métier) du *comment* (implémentation I/O, OS, réseau).
* **Tester** facilement (remplacer un repo disque par un fake mémoire).
* **Évoluer** sans casser (ajouter une impl REST, SQLite, etc.).

👉 Trois grands moyens d’abstraire en C++ :

1. **Interfaces “classiques”** via classes **abstraites** (virtual pur)
2. **Paramétrage statique** via **templates** (policies / concepts)
3. **Type erasure** (effacer le type derrière une façade stable)

On peut les **mixer intelligemment** suivant les besoins.

---

# 🧱 2) Interfaces via classes abstraites (virtual)

> “Style Java” adapté à C++ : contrat binaire stable, dispatch dynamique à l’exécution.

### ✅ Quand l’utiliser

* Tu veux **choisir l’implémentation à l’exécution** (fichier vs mémoire vs réseau).
* Besoin d’un **plugin** ou d’un chargement dynamique.
* Frontière modulaire nette (lib partagée).

### 🧩 Exemple minimal

```cpp
// includes/app/ports/IPlayerRepo.hpp
#pragma once
#include <vector>
#include <optional>
#include <string>
#include "app/domain/Player.hpp"

namespace app::ports {
struct IPlayerRepo {
    virtual ~IPlayerRepo() = default;
    virtual void add(const app::domain::Player&) = 0;
    virtual std::optional<app::domain::Player> getById(const std::string&) const = 0;
    virtual std::vector<app::domain::Player> all() const = 0;
    virtual void save() = 0;
};
}
```

**Injection par constructeur** (la plus propre) :

```cpp
namespace app::service {
class RosterService {
    std::unique_ptr<app::ports::IPlayerRepo> repo_;
public:
    explicit RosterService(std::unique_ptr<app::ports::IPlayerRepo> r)
      : repo_(std::move(r)) {}
    // ...
};
}
```

**Implémentations concrètes** (fichier / mémoire) et **wiring** :

```cpp
auto svc = app::service::RosterService(
    std::make_unique<PlayerFileRepo>("data/players.csv")
);
// en test :
auto svcTest = app::service::RosterService(
    std::make_unique<PlayerRepoFake>()
);
```

### 👍 Avantages

* API stable, test facile, remplacement à chaud.

### ⚠️ Nuances

* **Overhead vtable** (faible, mais réel).
* **Couplage ABI** : changer une signature dans l’interface casse les impls binaires.
* **Include bloat** : mitiger avec **forward declarations** et pImpl.

---

# 🧮 3) Abstraction par **templates** (policies / concepts)

> “Zéro overhead” : résolue **à la compilation** (static polymorphism).
> Parfait quand la stratégie concrète est connue **à la compilation**.

### ✅ Quand l’utiliser

* Chemin hot-path / perfs critiques.
* Plusieurs variantes connues au build (ex : `FileRepo` vs `MemoryRepo` via flags).
* Bibliothèques génériques.

### 🧩 Version *policy-based*

```cpp
template <class Repo>  // Repo doit fournir add(), getById(), all(), save()
class RosterServiceT {
    Repo repo_;
public:
    explicit RosterServiceT(Repo r) : repo_(std::move(r)) {}
    // appels directs : repo_.add(...), etc.
};
```

### 🧩 Avec **Concepts** (C++20) pour un contrat clair

```cpp
template <class R>
concept PlayerRepoLike = requires(R r, app::domain::Player p, std::string id) {
    { r.add(p) };
    { r.getById(id) } -> std::same_as<std::optional<app::domain::Player>>;
    { r.all() }       -> std::same_as<std::vector<app::domain::Player>>;
    { r.save() };
};

template <PlayerRepoLike Repo>
class RosterServiceT { /* ... */ };
```

**Utilisation** :

```cpp
RosterServiceT<PlayerFileRepo> svc{ PlayerFileRepo{"data/players.csv"} };
RosterServiceT<PlayerRepoFake> testSvc{ PlayerRepoFake{} };
```

### 👍 Avantages

* **Aucune vtable**, **inlining** possible, perfs au top.
* Contrat vérifié **au compile-time** (concepts).

### ⚠️ Nuances

* Explosion de **codes générés** si beaucoup de combinaisons.
* Le choix d’impl est **fixé** à la compilation (pas d’échange à chaud).

---

# 🎭 4) **Type erasure** (effacement de type)

> Combine la **souplesse runtime** des interfaces avec la **stabilité d’un type valeur** (sans exposer de vtable côté API).

### ✅ Quand l’utiliser

* Tu veux passer un “**objet qui se comporte comme un repo**” sans publier d’interface virtuelle publique.
* API fluide, **pimpl-like** sans macro.

### 🧩 Exemple (façade minimale)

```cpp
class PlayerRepo {
    struct Concept {
        virtual ~Concept() = default;
        virtual void add(const app::domain::Player&) = 0;
        virtual std::optional<app::domain::Player> getById(const std::string&) const = 0;
        virtual std::vector<app::domain::Player> all() const = 0;
        virtual void save() = 0;
    };
    template<class T>
    struct Model : Concept {
        T impl;
        explicit Model(T x) : impl(std::move(x)) {}
        void add(const app::domain::Player& p) override { impl.add(p); }
        auto getById(const std::string& id) const -> std::optional<app::domain::Player> override { return impl.getById(id); }
        auto all() const -> std::vector<app::domain::Player> override { return impl.all(); }
        void save() override { impl.save(); }
    };
    std::unique_ptr<Concept> self;
public:
    template<class T>
    PlayerRepo(T x) : self(std::make_unique<Model<T>>(std::move(x))) {}
    // forward
    void add(const app::domain::Player& p) { self->add(p); }
    auto getById(const std::string& id) const { return self->getById(id); }
    auto all() const { return self->all(); }
    void save() { self->save(); }
};
```

**Service** :

```cpp
class RosterService {
    PlayerRepo repo_;
public:
    explicit RosterService(PlayerRepo r) : repo_(std::move(r)) {}
};
```

**Wiring** :

```cpp
RosterService svc{ PlayerRepo{ PlayerFileRepo{"data/players.csv"} } };
RosterService testSvc{ PlayerRepo{ PlayerRepoFake{} } };
```

### 👍 Avantages

* API propre (type valeur), choix runtime, cache l’impl.

### ⚠️ Nuances

* Un (léger) coût d’indirection.
* Plus de code “plomberie” à écrire (générateurs peuvent aider).

---

# 📦 5) **pImpl** (Pointer to Implementation)

> Cacher les **dépendances lourdes** et **stabiliser l’ABI** d’une classe publique.

### 🧩 Schéma

```cpp
// .hpp
class GameService {
public:
    GameService();
    ~GameService();
    void start();
private:
    struct Impl;                 // forward
    std::unique_ptr<Impl> p_;    // opaque
};
```

```cpp
// .cpp
struct GameService::Impl {
    // dépendances lourdes ici (JSON, réseau, etc.)
    void startImpl() { /* ... */ }
};

GameService::GameService() : p_(std::make_unique<Impl>()) {}
GameService::~GameService() = default;
void GameService::start() { p_->startImpl(); }
```

### 👍 Avantages

* **Récompilation minimisée**, **entêtes propres**, ABI stable.
* Idéal pour libs publiques.

### ⚠️ Nuances

* Indirection supplémentaire, mais souvent négligeable.

---

# 🧰 6) Patterns d’injection : quelles formes choisir ?

| Pattern DI                | Comment                                   | Quand                   | Notes                         |
| ------------------------- | ----------------------------------------- | ----------------------- | ----------------------------- |
| **Constructor injection** | passer la dépendance au ctor              | 99% des cas             | clair, testable               |
| **Setter injection**      | méthode `setX(dep)`                       | objets réutilisables    | faisable mais moins sûr       |
| **Factory**               | fabrique l’impl au bon moment             | création conditionnelle | garde une interface pure      |
| **Abstract factory**      | fabrique de familles d’objets             | lots cohérents d’impls  | utile en tests                |
| **Service Locator**       | singleton global qui “donne” des services | à éviter                | anti-pattern (couplage caché) |

**Exemple factory légère** :

```cpp
struct RepoFactory {
    static std::unique_ptr<app::ports::IPlayerRepo> makePlayerRepo(const std::string& mode){
        if (mode=="file") return std::make_unique<PlayerFileRepo>("data/players.csv");
        if (mode=="mem")  return std::make_unique<PlayerRepoFake>();
        throw std::invalid_argument("mode inconnu");
    }
};
```

---

# 🛡️ 7) Principes de design (adaptés à C++)

* **SRP** : une classe = une responsabilité (sépare domaine / repo / service).
* **ISP** : interfaces **petites et ciblées** (pas “IRepoGéant”).
* **DIP** : service dépend **d’interfaces**, pas d’impl concrètes.
* **RAII** + **smart pointers** : `std::unique_ptr` par défaut ; `shared_ptr` si partage réel ; `weak_ptr` pour éviter les cycles.
* **Const-correctness** (& noexcept) : contrats clairs, optimisation.
* **Forward declarations** + `#pragma once` : compile plus vite, couplage réduit.

---

# 🧪 8) Tests et DI : combo gagnant

* Avec **virtual** : `std::make_unique<FakeRepo>()` → test unitaire simple.
* Avec **templates** : instancie `RosterServiceT<FakeRepo>` → zéro overhead.
* Avec **type erasure** : `RosterService{ PlayerRepo{FakeRepo{}} }` → API stable.

👉 Toujours privilégier les **fakes en mémoire** pour du *unit test* rapide, et garder 1–2 **tests d’intégration** sur l’impl fichier.

---

# 🧭 9) Quel choix retenir (grille de décision rapide)

* **Tu veux du runtime pluggable** (prod/config) → **virtual** *ou* **type erasure**
* **Tu veux perfs max et compile-time** → **templates + concepts**
* **Tu publies une lib stable** (cacher deps) → **pImpl** (+ éventuellement virtual dedans)
* **Projet app interne classique** → **interfaces virtuelles + constructor DI** (simple, pro, testable)

---

## 🧩 10) Exemples de “wiring” propres

**Main prod (virtual)**

```cpp
int main() {
    auto repo = std::make_unique<PlayerFileRepo>("data/players.csv");
    app::service::RosterService svc(std::move(repo));
    // ...
}
```

**Main prod (type erasure)**

```cpp
int main() {
    PlayerRepo repo = PlayerFileRepo{"data/players.csv"};
    RosterService svc{ std::move(repo) };
}
```

**Test (templates)**

```cpp
TEST_CASE("service with FakeRepo") {
    RosterServiceT<PlayerRepoFake> svc{ PlayerRepoFake{} };
    // ...
}
```

---

[...retorn en rèire](../menu.md)