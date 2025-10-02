# **📚 Documentation Complète sur l'Héritage en C++**

[...retorn en rèire](../../menu.md)

---

## **🌱 Introduction à l'Héritage**
L'héritage est un **mécanisme fondamental** de la **Programmation Orientée Objet (POO)** qui permet de :
- **Réutiliser du code** (éviter la duplication).
- **Créer des hiérarchies de classes** (ex: `Animal` → `Chien`, `Chat`).
- **Implémenter le polymorphisme** (comportements différents selon le type réel).

En C++, l'héritage se déclare avec `:` (deux-points).

---

## **📌 1. Héritage Simple (Bases)**
### **🔹 Syntaxe de Base**
```cpp
class Base {
    // Membres de la classe de base
};

class Derivee : public Base {  // 'public' = type d'héritage (visibilité)
    // Membres supplémentaires
};
```
**Exemple :**
```cpp
class Animal {
public:
    void manger() { std::cout << "Je mange.\n"; }
};

class Chien : public Animal {
public:
    void aboyer() { std::cout << "Woof!\n"; }
};

int main() {
    Chien monChien;
    monChien.manger();  // Hérité de Animal
    monChien.aboyer();  // Spécifique à Chien
}
```
**Sortie :**
```
Je mange.
Woof!
```

---

### **🔹 Types de Visibilité en Héritage**
Le mot-clé avant `:` détermine **comment les membres de la classe de base sont accessibles** dans la classe dérivée.

| **Type**       | **Effet sur `public`** | **Effet sur `protected`** | **Effet sur `private`** |
|----------------|-----------------------|--------------------------|------------------------|
| `public`       | Reste `public`        | Reste `protected`        | Inaccessible           |
| `protected`    | Devient `protected`   | Reste `protected`        | Inaccessible           |
| `private`      | Devient `private`     | Devient `private`        | Inaccessible           |

**Exemple :**
```cpp
class Base {
public:
    int publicVar;
protected:
    int protectedVar;
private:
    int privateVar;
};

class DeriveePublic : public Base {
    // publicVar    → public
    // protectedVar → protected
    // privateVar   → inaccessible
};

class DeriveeProtected : protected Base {
    // publicVar    → protected
    // protectedVar → protected
    // privateVar   → inaccessible
};

class DeriveePrivate : private Base {
    // publicVar    → private
    // protectedVar → private
    // privateVar   → inaccessible
};
```

**⚠️ Piège :**
- Un membre `private` de la classe de base **n'est jamais accessible** dans la classe dérivée, quel que soit le type d'héritage.
- En C++, **l'héritage `private` est le comportement par défaut** (contrairement à Java/C# où c'est `public`).

---

## **🛠️ 2. Constructeurs et Destructeurs en Héritage**
### **🔹 Appel des Constructeurs**
Lors de la création d'un objet dérivé :
1. **Le constructeur de la classe de base est appelé en premier**.
2. **Puis le constructeur de la classe dérivée**.

**Syntaxe pour appeler explicitement le constructeur de la base :**
```cpp
class Base {
public:
    Base(int x) { std::cout << "Base: " << x << "\n"; }
};

class Derivee : public Base {
public:
    Derivee(int x, int y) : Base(x) {  // Appel explicite du constructeur de Base
        std::cout << "Derivee: " << y << "\n";
    }
};

int main() {
    Derivee d(10, 20);
}
```
**Sortie :**
```
Base: 10
Derivee: 20
```

**⚠️ Piège :**
- Si la classe de base **n'a pas de constructeur par défaut** et que vous n'appeler pas explicitement un constructeur, **compilation échoue**.
- **Ordre de destruction** : D'abord le destructeur de la dérivée, puis celui de la base.

---

### **🔹 Constructeur par Copie et Héritage**
Le **constructeur par copie** d'une classe dérivée **doit explicitement appeler** celui de la base.

**Exemple :**
```cpp
class Base {
public:
    Base(const Base& other) { std::cout << "Copie de Base\n"; }
};

class Derivee : public Base {
public:
    Derivee(const Derivee& other) : Base(other) {  // Appel du constructeur par copie de Base
        std::cout << "Copie de Derivee\n";
    }
};

int main() {
    Derivee d1;
    Derivee d2 = d1;  // Appelle le constructeur par copie
}
```
**Sortie :**
```
Copie de Base
Copie de Derivee
```

**⚠️ Piège :**
- Si vous **ne définissez pas de constructeur par copie** dans la dérivée, le compilateur en génère un **qui appelle celui de la base par défaut** (ce qui peut causer des problèmes si la base a des membres dynamiques).

---

## **🔄 3. Redéfinition de Méthodes et `override`**
### **🔹 Redéfinir une Méthode**
Une classe dérivée peut **redéfinir** (override) une méthode de la classe de base.

**Exemple :**
```cpp
class Animal {
public:
    void crier() { std::cout << "Bruit d'animal\n"; }
};

class Chien : public Animal {
public:
    void crier() { std::cout << "Woof!\n"; }  // Redéfinition
};

int main() {
    Chien monChien;
    monChien.crier();  // Appelle la version de Chien
}
```
**Sortie :**
```
Woof!
```

---

### **🔹 Le Mot-Clé `override` (C++11)**
- **Garantit** que la méthode redéfinit bien une méthode virtuelle de la base.
- **Évite les erreurs** (ex: mauvaise signature).

**Exemple :**
```cpp
class Animal {
public:
    virtual void crier() { std::cout << "Bruit\n"; }
};

class Chien : public Animal {
public:
    void crier() override { std::cout << "Woof!\n"; }  // OK
    // void crier(int x) override { ... }  // ❌ Erreur : signature différente
};
```

**⚠️ Piège :**
- Sans `override`, une erreur de signature **passe à la compilation** mais cause un comportement inattendu.
- `override` **ne fonctionne qu'avec des méthodes `virtual`**.

---

### **🔹 Appel Explicite de la Méthode de la Base**
Utilisez `Base::methode()` pour appeler la version de la classe de base.

**Exemple :**
```cpp
class Chien : public Animal {
public:
    void crier() override {
        Animal::crier();  // Appel de la version de Animal
        std::cout << "Woof!\n";
    }
};
```
**Sortie :**
```
Bruit d'animal
Woof!
```

---

## **👻 4. Classes Statiques vs. Dynamiques (Polymorphisme)**
### **🔹 Liaison Statique (Early Binding)**
- **Résolution à la compilation**.
- **Pas de `virtual`** → la méthode appelée dépend du **type déclaré**.

**Exemple :**
```cpp
class Animal {
public:
    void crier() { std::cout << "Animal\n"; }
};

class Chien : public Animal {
public:
    void crier() { std::cout << "Chien\n"; }
};

int main() {
    Animal* a = new Chien();
    a->crier();  // Appelle Animal::crier() (liaison statique)
    delete a;
}
```
**Sortie :**
```
Animal
```

---

### **🔹 Liaison Dynamique (Late Binding) avec `virtual`**
- **Résolution à l'exécution** (polymorphisme).
- La méthode appelée dépend du **type réel** de l'objet.

**Exemple :**
```cpp
class Animal {
public:
    virtual void crier() { std::cout << "Animal\n"; }  // virtual
};

class Chien : public Animal {
public:
    void crier() override { std::cout << "Chien\n"; }
};

int main() {
    Animal* a = new Chien();
    a->crier();  // Appelle Chien::crier() (liaison dynamique)
    delete a;
}
```
**Sortie :**
```
Chien
```

**⚠️ Piège :**
- **Oublier `virtual`** → pas de polymorphisme.
- **Destruction polymorphe** : Si la classe de base a un destructeur **non-virtuel**, `delete` sur un pointeur de base **ne détruit pas correctement** l'objet dérivé.

**Solution :**
```cpp
class Animal {
public:
    virtual ~Animal() = default;  // Destructeur virtuel
};
```

---

### **🔹 Classes Abstraites et Méthodes Pures (`= 0`)**
Une **classe abstraite** ne peut pas être instanciée et **doit être héritée**.

**Exemple :**
```cpp
class Animal {
public:
    virtual void crier() = 0;  // Méthode pure = classe abstraite
};

class Chien : public Animal {
public:
    void crier() override { std::cout << "Woof!\n"; }
};

int main() {
    // Animal a;  // ❌ Erreur : Animal est abstraite
    Animal* a = new Chien();
    a->crier();  // OK
    delete a;
}
```

**⚠️ Piège :**
- Une classe avec **au moins une méthode pure** est abstraite.
- Si une classe dérivée **ne redéfinit pas toutes les méthodes pures**, elle reste abstraite.

---

## **🧩 5. Héritage Multiple (Avancé)**
Une classe peut hériter de **plusieurs classes de base**.

**Exemple :**
```cpp
class A {
public:
    void afficher() { std::cout << "A\n"; }
};

class B {
public:
    void afficher() { std::cout << "B\n"; }
};

class C : public A, public B {
    // Hérite de A et B
};

int main() {
    C c;
    // c.afficher();  // ❌ Ambiguïté : A::afficher ou B::afficher ?
    c.A::afficher();  // OK
    c.B::afficher();  // OK
}
```

**Problèmes :**
- **Ambiguïté** si deux classes de base ont une méthode de même nom.
- **Diamant de la mort** (héritage en losange) → résolu avec **héritage virtuel**.

**Solution (Héritage Virtuel) :**
```cpp
class Base {
public:
    int valeur;
};

class A : virtual public Base {};  // héritage virtuel
class B : virtual public Base {};  // héritage virtuel

class C : public A, public B {
    // Une seule copie de Base
};
```

---

## **💡 6. Bonnes Pratiques et Pièges à Éviter**
### **✅ Bonnes Pratiques**
1. **Utilisez `override`** pour éviter les erreurs de redéfinition.
2. **Rendez les destructeurs virtuels** dans les classes de base polymorphes.
3. **Préférez la composition à l'héritage** quand c'est possible (évite les hiérarchies complexes).
4. **Évitez l'héritage multiple** sauf si vraiment nécessaire (risque de complexité).
5. **Utilisez `final`** pour empêcher la redéfinition ou l'héritage :
   ```cpp
   class Base {
   public:
       virtual void methode() final {}  // Ne peut pas être redéfinie
   };

   class Derivee final : public Base {};  // Ne peut pas être héritée
   ```

### **❌ Pièges Courants**
1. **Oublier `virtual`** → pas de polymorphisme.
2. **Destructeur non-virtuel** → fuite mémoire si `delete` sur un pointeur de base.
3. **Héritage privé par défaut** → attention à la visibilité.
4. **Slicing** (troncature) quand on copie un objet dérivé dans un objet de base :
   ```cpp
   Derivee d;
   Base b = d;  // Perte des données de Derivee
   ```
5. **Appel incorrect des constructeurs** → toujours initialiser la base explicitement.

---

## **📝 7. Exercice Pratique**
**Énoncé :**
Créez une hiérarchie de classes pour représenter des formes géométriques (`Forme` → `Cercle`, `Rectangle`) avec :
- Une méthode virtuelle `aire()`.
- Un destructeur virtuel.
- Une méthode `afficher()` redéfinie dans chaque classe dérivée.

**Solution :**
```cpp
#include <iostream>
#include <cmath>

class Forme {
public:
    virtual double aire() const = 0;  // Méthode pure
    virtual void afficher() const { std::cout << "Forme générique\n"; }
    virtual ~Forme() = default;  // Destructeur virtuel
};

class Cercle : public Forme {
    double rayon;
public:
    Cercle(double r) : rayon(r) {}
    double aire() const override { return M_PI * rayon * rayon; }
    void afficher() const override { std::cout << "Cercle (rayon=" << rayon << ")\n"; }
};

class Rectangle : public Forme {
    double largeur, hauteur;
public:
    Rectangle(double l, double h) : largeur(l), hauteur(h) {}
    double aire() const override { return largeur * hauteur; }
    void afficher() const override { std::cout << "Rectangle (" << largeur << "x" << hauteur << ")\n"; }
};

int main() {
    Forme* formes[] = { new Cercle(5), new Rectangle(4, 6) };
    for (const auto& forme : formes) {
        forme->afficher();
        std::cout << "Aire: " << forme->aire() << "\n";
        delete forme;
    }
}
```
**Sortie :**
```
Cercle (rayon=5)
Aire: 78.5398
Rectangle (4x6)
Aire: 24
```

---

## **🎓 8. Résumé des Concepts Clés**
| **Concept**               | **Description**                                                                 | **Exemple**                          |
|---------------------------|---------------------------------------------------------------------------------|--------------------------------------|
| **Héritage simple**        | Une classe dérivée hérite d'une classe de base.                               | `class Chien : public Animal`        |
| **Visibilité**            | `public`, `protected`, `private` contrôlent l'accès aux membres hérités.       | `class Derivee : public Base`        |
| **Constructeurs**         | La base est construite avant la dérivée.                                       | `Derivee(int x) : Base(x) { ... }`   |
| **`virtual`**             | Active le polymorphisme (liaison dynamique).                                    | `virtual void methode()`             |
| **`override`**            | Garantit la redéfinition correcte d'une méthode virtuelle.                     | `void methode() override`            |
| **Destructeur virtuel**   | Nécessaire pour une destruction polymorphe correcte.                            | `virtual ~Base() = default`          |
| **Classe abstraite**      | Contient au moins une méthode pure (`= 0`).                                     | `virtual void methode() = 0`         |
| **Héritage multiple**      | Une classe hérite de plusieurs bases (risque d'ambiguïté).                     | `class C : public A, public B`       |
| **Héritage virtuel**      | Résout le "diamant de la mort" en partageant une seule instance de la base.     | `class A : virtual public Base`      |

---

[...retorn en rèire](../../menu.md)