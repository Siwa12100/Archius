# Classes, Méthodes et Fonctions Amies (`friend`) en C++

[...retorn en rèire](../menu.md)

---

Les amis en C++ sont un **mécanisme d’exception au principe d’encapsulation** : on donne à certaines fonctions / classes le droit de voir l’**intérieur (private / protected)** d’une classe.

> 🧠 Idée clé : *c’est la classe qui “offre sa confiance” à quelqu’un en le déclarant `friend`.*

---

## 2.1 🧑‍🤝‍🧑 Principe de l’amitié : accès privilégié

### 🔒 Rappel : encapsulation sans `friend`

Sans `friend`, seule la classe elle-même (et ses classes dérivées pour `protected`) peut accéder à ses membres privés :

```cpp
class CompteBancaire {
private:
    double solde;

public:
    explicit CompteBancaire(double s) : solde{s} {}

    void deposer(double montant) {
        solde += montant;
    }

    double getSolde() const {
        return solde;
    }
};
```

Une fonction externe ne peut pas faire :

```cpp
void pirater(CompteBancaire& c) {
    // c.solde = 1'000'000;  // ❌ Erreur : solde est privé
}
```

---

### ✅ Avec `friend` : donner un accès “VIP”

On peut autoriser une **fonction externe** à accéder aux membres privés :

```cpp
class CompteBancaire {
private:
    double solde;

public:
    explicit CompteBancaire(double s) : solde{s} {}

    void deposer(double montant) { solde += montant; }

    double getSolde() const { return solde; }

    // Déclaration d'ami :
    friend void afficherDetails(const CompteBancaire& c);
};

// Définition de la fonction amie (en dehors de la classe)
void afficherDetails(const CompteBancaire& c) {
    // ✅ Accès direct à un membre privé
    std::cout << "Solde interne = " << c.solde << " euros\n";
}
```

🔎 Points importants :

* La **classe** `CompteBancaire` dit : *"cette fonction est mon amie"*
* `afficherDetails` n’est **pas** une méthode, c’est une **fonction libre**, mais elle voit `solde` comme si elle était “à l’intérieur” de la classe.

---

### 👭 Classes amies

On peut aussi déclarer **une classe entière** comme amie :

```cpp
class Moteur;

class Voiture {
private:
    double carburant = 50.0;

    friend class Moteur;  // 👈 Moteur est amie de Voiture

public:
    void afficherCarburant() const {
        std::cout << "Carburant : " << carburant << " L\n";
    }
};

class Moteur {
public:
    void consommer(Voiture& v, double litres) {
        // ✅ Accès direct au private de Voiture
        v.carburant -= litres;
    }
};
```

Ici :

* `Moteur` peut lire/modifier `v.carburant` directement.
* En revanche, **l’inverse n’est pas vrai** (on le verra dans les limites).

---

### 👇 Méthode amie d’une autre classe

On peut aussi rendre **une méthode précise** amie d’une classe :

```cpp
class CompteBancaire;

class Auditeur {
public:
    void auditer(const CompteBancaire& c);
};

class CompteBancaire {
private:
    double solde;

    // 👇 Seule cette méthode est amie
    friend void Auditeur::auditer(const CompteBancaire& c);

public:
    explicit CompteBancaire(double s) : solde{s} {}
};

void Auditeur::auditer(const CompteBancaire& c) {
    // ✅ a accès à solde grâce à l'amitié
    std::cout << "Audit : solde interne = " << c.solde << "\n";
}
```

---

## 2.2 🧱 `friend` et surcharge d’opérateurs (cas très courant)

Un usage **hyper classique** de `friend` : les opérateurs comme `operator<<` pour `std::ostream`.

```cpp
class Vector2D {
private:
    double x;
    double y;

public:
    Vector2D(double x, double y) : x{x}, y{y} {}

    // Fonction amie pour pouvoir afficher Vector2D
    friend std::ostream& operator<<(std::ostream& os, const Vector2D& v);
};

std::ostream& operator<<(std::ostream& os, const Vector2D& v) {
    // ✅ Accès aux membres privés
    os << "(" << v.x << ", " << v.y << ")";
    return os;
}
```

💡 Pourquoi `friend` est pratique ici ?

* On veut que `operator<<` soit une **fonction libre** (pour respecter la forme `os << v`).
* Mais cette fonction a besoin d’accéder à `x` et `y` → `friend` résout ça proprement.

---

## 2.3 ⚠️ Limites de l’amitié

Maintenant les points importants : l’amitié est **très limitée** et ne se propage pas magiquement.

### 2.3.1 ❌ Pas de symétrie

> Si A déclare B comme ami, **l’inverse n’est pas automatiquement vrai**.

Exemple :

```cpp
class B;  // déclaration anticipée

class A {
    friend class B;  // B est amie de A
private:
    int secretA = 42;
};

class B {
private:
    int secretB = 7;

public:
    void foo(A& a) {
        a.secretA = 0;   // ✅ OK, B est amie de A
    }
};

void f(B& b, A& a) {
    // b.secretB = 0;   // ❌ A n'est PAS amie de B, même si l'inverse est vrai
}
```

👉 L’amitié va **dans un seul sens**, celui de la classe qui déclare `friend`.

---

### 2.3.2 ❌ Pas de transitivité

> Si A est amie de B, et B est amie de C, **A n’est pas automatiquement amie de C**.

Schéma :

* `C` déclare `B` amie → `B` peut voir les privés de `C`.
* `B` déclare `A` amie → `A` peut voir les privés de `B`.
* Mais **A ne peut pas voir les privés de C**.

Exemple :

```cpp
class C;

class B {
    friend class A;  // A est amie de B
private:
    int secretB = 10;
};

class C {
    friend class B;  // B est amie de C
private:
    int secretC = 20;
};

class A {
public:
    void test(B& b, C& c) {
        b.secretB = 0;   // ✅ OK (A est amie de B)
        // c.secretC = 0;   // ❌ Interdit : A n'est PAS amie de C
    }
};
```

🧩 Moralité :
L’amitié ne se “propage” pas. On doit déclarer **explicitement** chaque relation d’amitié voulue.

---

### 2.3.3 ❌ Pas d’héritage automatique de l’amitié

Deux sens à bien distinguer :

#### 🔹 (1) Une classe dérivée n’hérite pas des amis de sa base

```cpp
class Base {
    friend class AmiDeBase;
private:
    int secretBase = 1;
};

class Derivee : public Base {
private:
    int secretDerivee = 2;
};

class AmiDeBase {
public:
    void f(Base& b, Derivee& d) {
        b.secretBase = 0;       // ✅ OK
        // d.secretDerivee = 0; // ❌ Non, l'amitié ne s'étend pas à Derivee
    }
};
```

* `AmiDeBase` a accès aux `private` de `Base`,
* mais pas à ceux de `Derivee`, sauf si `Derivee` déclare aussi `friend class AmiDeBase;`.

#### 🔹 (2) Une classe dérivée ne devient pas amie parce que la base est amie

```cpp
class A {
    friend class B;  // B est amie de A
private:
    int secretA = 1;
};

class B {
    // Rien de spécial ici
};

class C : public B {
public:
    void f(A& a) {
        // a.secretA = 0;   // ❌ C n'est PAS amie de A, même si B l'est
    }
};
```

L’amitié **ne suit pas l’héritage** : ni vers la base, ni vers les dérivés, ni via les amis.

---

### 2.3.4 ❌ Pas de “propagation” aux amis des amis

> Un ami d’une classe ne devient pas automatiquement ami des autres amis de cette classe.

Exemple :

```cpp
class A {
    friend class B;
    friend class C;
private:
    int secret = 42;
};

class B {
public:
    void f(A& a) { a.secret = 0; }   // ✅
};

class C {
public:
    void g(A& a) { a.secret = 1; }   // ✅
    void h(B& b) {
        // Ici C n'a aucun droit spécial sur les membres privés de B
        // sauf si B déclare explicitement C comme friend.
    }
};
```

👉 Chaque lien d’amitié est **individuel** et doit être **déclaré là où l’accès est accordé**.

---

### 2.3.5 📝 Nécessité d’une déclaration explicite dans tous les cas

Règle d’or :

> 🔑 *On ne devient jamais ami “par accident”. L’amitié doit être explicitement accordée par la classe qui ouvre son encapsulation.*

Concrètement :

* Une **fonction libre** doit être déclarée `friend` **dans la classe qui partage ses privés**
* Une **classe amie** doit être listée avec `friend class Nom;`
* Une **méthode amie** doit être déclarée exactement avec sa **signature complète** dans la classe qui lui donne accès.

Exemple complet :

```cpp
class B;   // forward declaration

class A {
    friend class B;  // ✅ classe amie
    friend void utilitaire(A&);  // ✅ fonction amie

private:
    int secret = 123;
};

class B {
public:
    void f(A& a) { a.secret = 0; }    // ✅ OK
};

void utilitaire(A& a) {
    a.secret = 999;                   // ✅ OK
}

void g(A& a) {
    // a.secret = 10;                 // ❌ pas amie, pas d'accès
}
```

Sans cette déclaration `friend` **dans A**, ni `B::f` ni `utilitaire` n’auraient le droit de toucher `secret`.

---

## 2.4 🎯 Quand (et comment) utiliser `friend` proprement

Parce que `friend` casse (un peu) l’encapsulation, il faut l’utiliser avec **parcimonie**.

✅ Cas où `friend` est souvent **pertinent** :

* ✅ Surcharge de `operator<<` ou `operator>>` pour les I/O.
* ✅ Fonctions utilitaires très proches de la classe, mais qu’on veut garder libres (ex. fonctions mathématiques sur des vecteurs / matrices).
* ✅ Classes fortement liées (pattern de type `Builder`, `Factory`, `Manager` qui doivent manipuler des détails internes).

⚠️ À éviter :

* ❌ Mettre `friend` partout “par facilité” → forte **couplage**, difficile à maintenir.
* ❌ Utiliser `friend` pour contourner paresseusement un mauvais design.

---

[...retorn en rèire](../menu.md)