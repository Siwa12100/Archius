# Classes, Méthodes et Fonctions Amies (`friend`) en C++

[...retorn en rèire](../menu.md)

---

Les **amis** (`friend`) en C++ sont un **mécanisme d'exception à l'encapsulation** :
une classe peut **offrir volontairement un accès privilégié** à ses membres privés ou protégés.

> 🧠 *C’est toujours la classe qui déclare quelqu’un comme ami, jamais l’inverse.*

L’amitié sert à **autoriser** des fonctions ou classes externes à manipuler directement des données internes, **sans modifier la visibilité générale** (private/protected).

---

# 🔵 1. Principe Fondamental : Accès Privé Autorisé

## 🔒 1.1. Sans `friend` : encapsulation stricte

Par défaut, les membres `private` restent invisibles de l’extérieur :

```cpp
class CompteBancaire {
private:
    double solde;

public:
    explicit CompteBancaire(double s) : solde{s} {}

    void deposer(double montant) { solde += montant; }
    double getSolde() const { return solde; }
};
```

Impossible pour une fonction externe de faire :

```cpp
void pirater(CompteBancaire& c) {
    // c.solde = 1000000; // ❌ Interdit
}
```

---

## 🔑 1.2. Avec `friend` : accès “VIP”

Une **fonction libre** peut devenir amie :

```cpp
class CompteBancaire {
private:
    double solde;

public:
    explicit CompteBancaire(double s) : solde{s} {}

    friend void afficherDetails(const CompteBancaire& c); // 💡 Accès autorisé
};

void afficherDetails(const CompteBancaire& c) {
    std::cout << "Solde interne = " << c.solde << " euros\n";  // ✅ OK
}
```

### ❤️ Points clés

* La **classe** accorde sa confiance.
* La fonction amie **n’est pas une méthode**.
* Elle accède malgré tout aux membres privés.

---

# 👫 2. Déclarations d’Amitié (`friend`)

Les amitiés en C++ se déclarent **dans la classe qui ouvre l’accès**, jamais ailleurs.

## 👭 2.1. Classe amie

```cpp
class Moteur;

class Voiture {
private:
    double carburant = 50.0;

    friend class Moteur;  // 👈 Moteur a accès au private de Voiture
};

class Moteur {
public:
    void consommer(Voiture& v, double litres) {
        v.carburant -= litres; // ✅ autorisé
    }
};
```

---

## 👇 2.2. Méthode amie (plus précis)

```cpp
class CompteBancaire;

class Auditeur {
public:
    void auditer(const CompteBancaire& c);
};

class CompteBancaire {
private:
    double solde;

    friend void Auditeur::auditer(const CompteBancaire&);
};
```

La méthode **précise** `Auditeur::auditer` est amie, pas toute la classe.

---

## 🧾 2.3. Fonction amie (cas le plus courant)

Très utilisé pour les opérateurs :

```cpp
class Vector2D {
private:
    double x, y;

    friend std::ostream& operator<<(std::ostream& os, const Vector2D& v);
};

std::ostream& operator<<(std::ostream& os, const Vector2D& v) {
    os << "(" << v.x << ", " << v.y << ")";
    return os;
}
```

> 💡 `operator<<` DOIT être une **fonction libre**, pas une méthode :
> forme `os << v` uniquement possible ainsi.

---

# ⚠️ 3. Limites Importantes de l’Amitié

L’amitié en C++ **ne se transmet pas**, **n’est pas symétrique**, **n’est pas héritée**, **n’est pas rétroactive**.

## 🚫 3.1. Pas de symétrie

> Si A déclare B amie, B peut accéder aux privés de A…
> mais A ne peut PAS accéder à B.

```cpp
class A {
    friend class B;
private:
    int secretA = 42;
};

class B {
private:
    int secretB = 7;
public:
    void f(A& a) { a.secretA = 0; }   // ✅
};

void test(B& b) {
    // b.secretB = 0; // ❌ A ne devient pas amie de B automatiquement
}
```

---

## 🔗 3.2. Pas de transitivité

> Si A est amie de B, et B est amie de C, A n’est **pas** amie de C.

```cpp
class B;
class C;

class A { friend class B; };
class B { friend class C; };
class C {};
```

A ➜ B
B ➜ C
❌ A ➜ C (NON)

---

## 🧬 3.3. Pas d’héritage

### ① Les amis de la base **ne deviennent pas** amis de la dérivée

```cpp
class Base {
    friend class Ami;
private:
    int secretBase = 1;
};

class Derivee : public Base {
private:
    int secretDerive = 2;
};

class Ami {
public:
    void f(Base& b, Derivee& d) {
        b.secretBase = 0;      // ✔️
        // d.secretDerive = 0; // ❌ Non hérité
    }
};
```

### ② Une classe dérivée **n’est pas amie** juste parce que la base l’est

```cpp
class A {
    friend class B;
};

class B {};

class C : public B {
public:
    void f(A& a) {
        // ❌ Pas amie, même si B l'était
    }
};
```

---

## ❗ 3.4. Pas d’amitié entre amis

Même si deux fonctions/classes sont amies d’une même classe, elles ne sont **pas amies entre elles**.

```cpp
class A {
    friend class B;
    friend class C;
private:
    int secret;
};
```

* `B` peut toucher `secret`
* `C` peut toucher `secret`
* mais **B n’a aucun droit sur C**, ni C sur B.

---

## 📝 3.5. L’amitié doit toujours être explicitement déclarée

Règle d’or :

> 🧷 *Aucune amitié n’est implicite.
> Si une fonction ou une classe doit accéder à un private, elle doit être listée comme `friend` dans la classe concernée.*

Exemple complet :

```cpp
class A {
    friend class B;
    friend void util(A&);
private:
    int secret = 123;
};

class B {
public:
    void f(A& a) { a.secret = 0; }   // OK
};

void util(A& a) {
    a.secret = 99; // OK
}

void g(A& a) {
    // a.secret = 1; // ❌ pas ami
}
```

---

# 🎯 4. Guideline : Quand utiliser `friend` ?

## 👍 Cas où c’est une bonne idée

* ✔️ Surcharge des opérateurs (`<<`, `>>`, `==`, etc.)
* ✔️ Algèbre (ex. géométrie : `norme(u+v)`)
* ✔️ Patterns *Builder*, *Factory*, *Manager*
* ✔️ Fonctions utilitaires très liées à une classe

---

## 👎 Cas où c’est une mauvaise idée

* ❌ Pour “casser l’encapsulation” vite fait
* ❌ Pour corriger un mauvais design
* ❌ Pour donner trop de pouvoir à trop de classes

---

# 🧭 5. Récap express

| Règle                                                 | Vrai ? |
| ----------------------------------------------------- | ------ |
| Une classe peut ouvrir son encapsulation via `friend` | ✔️     |
| L’amitié n’est pas symétrique                         | ❌      |
| L’amitié n’est pas transitive                         | ❌      |
| L’amitié n’est pas héritée                            | ❌      |
| Toute fonction amie doit être déclarée explicitement  | ✔️     |
| `friend` est utile pour les opérateurs                | ✔️     |

---

[...retorn en rèire](../menu.md)

---

