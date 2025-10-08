# **Héritage en C++ : Guide Ultime sur les `virtual` et les Constructeurs** 🚀

[...retorn e rèire](../../menu.md)
---

# **1. Les Fonctions `virtual` : Polymorphisme et Instanciation** 🎭

## **1.1. Qu'est-ce qu'une fonction `virtual` ?**
Une fonction `virtual` permet le **polymorphisme dynamique** : le choix de la fonction appelée dépend du **type dynamique** (réel) de l'objet, pas de son type statique (déclaré).

### **Exemple de base**
```cpp
class Animal {
public:
    virtual void crier() { cout << "Bruit d'animal" << endl; }
};

class Chien : public Animal {
public:
    void crier() override { cout << "Woof!" << endl; }  // Redéfinit la méthode
};

int main() {
    Animal* a = new Chien();  // Type statique: Animal*, type dynamique: Chien
    a->crier();  // Affiche "Woof!" (polymorphisme dynamique)
    delete a;
    return 0;
}
```
> **💡 Pourquoi `virtual` ?**
> Sans `virtual`, `a->crier()` aurait appelé `Animal::crier()` (liage statique).

---

## **1.2. Comportement selon qui déclare `virtual`**

### **Cas 1 : La mère déclare `virtual`, la fille aussi (explicite)**
```cpp
class Mere {
public:
    virtual void f() { cout << "Mere::f()" << endl; }
};

class Fille : public Mere {
public:
    void f() override { cout << "Fille::f()" << endl; }  // override est optionnel mais recommandé
};
```
> **✅ Comportement** :
> - Polymorphisme actif.
> - `Fille::f()` est appelée si l'objet est de type `Fille`.

---

### **Cas 2 : La mère déclare `virtual`, la fille **ne le déclare pas****
```cpp
class Mere {
public:
    virtual void f() { cout << "Mere::f()" << endl; }
};

class Fille : public Mere {
public:
    void f() { cout << "Fille::f()" << endl; }  // Pas de `virtual` ni `override`
};
```
> **✅ Comportement** :
> - **Le polymorphisme fonctionne quand même** ! La redéfinition est implicite.
> - **⚠️ Danger** : Si la signature diffère (ex: `void f(int)`), ce n'est **plus une redéfinition** mais une **surcharge** (et le polymorphisme est cassé).
> - **🔹 Bonne pratique** : Toujours utiliser `override` pour éviter les erreurs.

---

### **Cas 3 : La mère **ne déclare pas** `virtual`, la fille le déclare**
```cpp
class Mere {
public:
    void f() { cout << "Mere::f()" << endl; }  // PAS virtual
};

class Fille : public Mere {
public:
    virtual void f() { cout << "Fille::f()" << endl; }  // virtual ici ne change RIEN
};
```
> **❌ Comportement** :
> - **Aucun polymorphisme** : `Mere::f()` est toujours appelée, même si l'objet est de type `Fille`.
> - Le `virtual` dans `Fille` est **ignoré** car la méthode de base n'est pas `virtual`.

---

### **Cas 4 : Aucune classe ne déclare `virtual`**
```cpp
class Mere {
public:
    void f() { cout << "Mere::f()" << endl; }
};

class Fille : public Mere {
public:
    void f() { cout << "Fille::f()" << endl; }
};
```
> **❌ Comportement** :
> - **Liage statique** : Toujours `Mere::f()` appelée, même avec un objet `Fille`.
> - **Pas de polymorphisme**.

---

## **1.3. Instanciation Statique vs. Dynamique** 🔄

### **Instanciation Statique (sans `virtual`)**
- Le compilateur détermine **à la compilation** quelle fonction appeler, en fonction du **type du pointeur/référence**.
- **Exemple** :
  ```cpp
  Mere* m = new Fille();
  m->f();  // Appelle Mere::f() (même si l'objet est Fille)
  ```

### **Instanciation Dynamique (avec `virtual`)**
- La décision est prise **à l'exécution** via la **table des méthodes virtuelles (vtable)**.
- **Exemple** :
  ```cpp
  Mere* m = new Fille();
  m->f();  // Appelle Fille::f() (polymorphisme)
  ```
> **🔹 Comment ça marche ?**
> - Chaque classe avec des méthodes `virtual` a une **vtable** (table de pointeurs vers les fonctions).
> - À l'instanciation, l'objet pointe vers la **vtable de sa classe réelle**.
> - L'appel `m->f()` suit le pointeur de la vtable.

---

### **Exemple avec vtable (simplifié)**
```cpp
// Pseudo-code illustrant la vtable
class Mere {
    void* __vptr;  // Pointeur vers la vtable (ajouté automatiquement)
public:
    virtual void f() { ... }
};

// vtable de Mere :
// [ &Mere::f ]

class Fille : public Mere {
public:
    void f() override { ... }
};

// vtable de Fille :
// [ &Fille::f ]  // Remplace Mere::f
```
> **💡 Pourquoi `override` est utile ?**
> - Le compilateur vérifie que la méthode redéfinit bien une méthode `virtual` de la classe parente.
> - Évite les erreurs de signature.

---

## **1.4. Destructeurs `virtual` : Un Cas Particulier** ☠️

### **Pourquoi un destructeur `virtual` ?**
Si une classe est destinée à être héritée **et** que des objets fils sont supprimés via un pointeur sur la mère, le destructeur **doit** être `virtual` pour éviter des fuites mémoire.

#### **❌ Mauvaise pratique (destructeur non-virtual)**
```cpp
class Mere {
public:
    ~Mere() { cout << "Détruit Mere" << endl; }  // PAS virtual
};

class Fille : public Mere {
public:
    ~Fille() { cout << "Détruit Fille" << endl; }
};

int main() {
    Mere* m = new Fille();
    delete m;  // ❌ Seule ~Mere() est appelée ! Fuite mémoire si Fille alloue des ressources.
    return 0;
}
```
> **💥 Problème** :
> - Le destructeur de `Fille` n'est **jamais appelé** → fuites mémoire si `Fille` alloue des ressources.

#### **✅ Bonne pratique (destructeur `virtual`)**
```cpp
class Mere {
public:
    virtual ~Mere() { cout << "Détruit Mere" << endl; }  // virtual !
};

class Fille : public Mere {
public:
    ~Fille() override { cout << "Détruit Fille" << endl; }
};

int main() {
    Mere* m = new Fille();
    delete m;  // ✅ ~Fille() puis ~Mere() sont appelés.
    return 0;
}
```
> **🔹 Règle d'or** :
> - **Toute classe avec des méthodes `virtual` doit avoir un destructeur `virtual`** (même s'il est vide).

---

## **1.5. Fonctions `virtual` pures et Classes Abstraites** 👻

Une fonction `virtual pure` (`= 0`) rend la classe **abstraite** (ne peut pas être instanciée directement).

### **Exemple**
```cpp
class Forme {
public:
    virtual double aire() const = 0;  // Méthode pure → classe abstraite
};

class Carre : public Forme {
    double cote;
public:
    double aire() const override { return cote * cote; }  // Implémentation obligatoire
};
```
> **✅ Comportement** :
> - `Forme f;` → **Erreur** (classe abstraite).
> - `Forme* f = new Carre();` → **OK** (polymorphisme).

---

## **1.6. `final` : Bloquer l'héritage ou la redéfinition** 🚫

### **Empêcher une classe d'être héritée**
```cpp
class Mere final {  // Personne ne peut hériter de Mere
    virtual void f() {}
};
```

### **Empêcher une méthode d'être redéfinie**
```cpp
class Mere {
public:
    virtual void f() final {}  // f() ne peut plus être override
};
```

---

## **1.7. Appel Explicite à la Version Parente (`Mere::f()`)** 🔄

Même avec du polymorphisme, on peut forcer l'appel à la version parente :
```cpp
class Fille : public Mere {
public:
    void f() override {
        Mere::f();  // Appel explicite à la version de Mere
        cout << "Fille::f()" << endl;
    }
};
```

---

## **1.8. Coût des Fonctions `virtual`** ⚖️
- **➕ Avantages** :
  - Polymorphisme puissant.
- **➖ Inconvénients** :
  - **Overhead mémoire** : Chaque objet a un pointeur vers sa vtable.
  - **Overhead temps** : Indirection via la vtable (mais négligeable sur les machines modernes).
  - **Moins d'optimisations** : Le compilateur ne peut pas inliner les appels `virtual`.

> **💡 Quand éviter `virtual` ?**
> - Dans du code critique en performance (ex: boucles serrées).
> - Utiliser des alternatives comme **CRTP** (Curiously Recurring Template Pattern) ou des `std::variant`.

---

---
---

# **2. Constructeurs, Destructeurs et Héritage** 🏗️

## **2.1. Ordre d'Appel des Constructeurs/Destructeurs** 🔄

### **Règle générale**
1. **Construction** :
   - **Classe mère** → **Membres** → **Classe fille**.
2. **Destruction** :
   - **Classe fille** → **Membres** → **Classe mère**.

### **Exemple**
```cpp
class Mere {
public:
    Mere() { cout << "Constructeur Mere" << endl; }
    ~Mere() { cout << "Destructeur Mere" << endl; }
};

class Fille : public Mere {
public:
    Fille() { cout << "Constructeur Fille" << endl; }
    ~Fille() { cout << "Destructeur Fille" << endl; }
};

int main() {
    Fille f;
    return 0;
}
```
> **Sortie** :
> ```
> Constructeur Mere
> Constructeur Fille
> Destructeur Fille
> Destructeur Mere
> ```

---

## **2.2. Initialisation des Membres de la Classe Mère** 🛠️

### **Cas 1 : Constructeur par défaut de la mère**
Si la mère a un constructeur **sans arguments**, il est appelé automatiquement :
```cpp
class Mere {
public:
    Mere() { cout << "Mere()" << endl; }
};

class Fille : public Mere {
public:
    Fille() { cout << "Fille()" << endl; }  // Mere() est appelé avant
};
```

### **Cas 2 : Constructeur de la mère avec arguments**
Si la mère **n'a pas de constructeur par défaut**, la fille **doit** l'initialiser explicitement via la **liste d'initialisation** :
```cpp
class Mere {
    int val;
public:
    Mere(int v) : val(v) { cout << "Mere(" << val << ")" << endl; }
};

class Fille : public Mere {
public:
    Fille(int v) : Mere(v) {  // Initialisation obligatoire
        cout << "Fille()" << endl;
    }
};
```
> **❌ Erreur si oublié** :
> ```cpp
> Fille::Fille(int v) { /* Oublie : Mere(v) */ }  // ❌ Erreur de compilation
> ```

---

### **Cas 3 : Appel d'un constructeur spécifique de la mère**
On peut choisir quel constructeur de la mère appeler :
```cpp
class Mere {
public:
    Mere(int) { cout << "Mere(int)" << endl; }
    Mere(double) { cout << "Mere(double)" << endl; }
};

class Fille : public Mere {
public:
    Fille() : Mere(42) {}  // Appelle Mere(int)
};
```

---

## **2.3. Héritage et Constructeurs de Copie** 📋

Le constructeur de copie **n'est pas hérité**. Si la fille n'en définit pas, le compilateur en génère un **par défaut** (copie membre à membre).

### **Exemple**
```cpp
class Mere {
public:
    Mere(const Mere&) { cout << "Copie Mere" << endl; }
};

class Fille : public Mere {
    // Pas de constructeur de copie défini → généré par le compilateur
};

int main() {
    Fille f1;
    Fille f2 = f1;  // Appelle le constructeur de copie généré
    return 0;
}
```
> **⚠️ Attention** :
> - Le constructeur de copie généré appelle **celui de la mère**.
> - Si la fille a des membres dynamiques (pointeurs), il faut **définir explicitement** le constructeur de copie.

---

## **2.4. Constructeurs et Héritage Multiple** 🤯

En cas d'héritage multiple, l'ordre d'appel des constructeurs suit l'ordre des classes dans la déclaration :
```cpp
class A {
public:
    A() { cout << "A()" << endl; }
};
class B {
public:
    B() { cout << "B()" << endl; }
};

class C : public A, public B {
public:
    C() { cout << "C()" << endl; }
};

int main() {
    C c;  // Affiche : A() → B() → C()
    return 0;
}
```

> **⚠️ Problème du diamant** :
> Si `A` et `B` héritent tous deux de `D`, `C` hérite **deux fois de `D`** (sauf si on utilise `virtual` dans l'héritage).

---

## **2.5. Délegation de Constructeurs** 🔀

Une classe fille peut déléguer un constructeur à un autre :
```cpp
class Fille : public Mere {
public:
    Fille(int x) : Mere(x) {}
    Fille() : Fille(42) {}  // Délègue à Fille(int)
};
```

---

## **2.6. Destructeurs et Héritage** 🗑️

- **Toujours déclarer le destructeur `virtual` si la classe est polymorphe** (cf. [1.4](#14-destructeurs-virtual-un-cas-particulier-)).
- **Ordre de destruction** : Fille → Mère (inverse de la construction).

### **Exemple avec héritage multiple**
```cpp
class A { public: ~A() { cout << "~A()" << endl; } };
class B { public: ~B() { cout << "~B()" << endl; } };
class C : public A, public B { public: ~C() { cout << "~C()" << endl; } };

int main() {
    C c;  // Destruction : ~C() → ~B() → ~A()
    return 0;
}
```

---

## **2.7. Pièges Courants** ⚠️

### **Piège 1 : Oublier d'initialiser la mère**
```cpp
class Mere { public: Mere(int) {} };
class Fille : public Mere {
public:
    Fille() {}  // ❌ Erreur : Mere n'a pas de constructeur par défaut
};
```
> **Solution** : Toujours initialiser la mère dans la liste d'initialisation.

---

### **Piège 2 : Appeler une méthode `virtual` dans un constructeur**
```cpp
class Mere {
public:
    Mere() { f(); }  // ❌ Danger !
    virtual void f() { cout << "Mere::f()" << endl; }
};

class Fille : public Mere {
public:
    void f() override { cout << "Fille::f()" << endl; }
};

int main() {
    Fille f;  // Affiche "Mere::f()" (pas "Fille::f()") !
    return 0;
}
```
> **💥 Pourquoi ?**
> - Pendant la construction de `Mere`, l'objet n'est pas encore de type `Fille`.
> - **Règle** : Éviter d'appeler des méthodes `virtual` dans les constructeurs/destructeurs.

---

### **Piège 3 : Fuites mémoire avec des membres dynamiques**
```cpp
class Mere {
    int* data;
public:
    Mere() : data(new int[100]) {}
    ~Mere() { delete[] data; }  // PAS virtual !
};

class Fille : public Mere {
    int* moreData;
public:
    Fille() : moreData(new int[200]) {}
    ~Fille() { delete[] moreData; }
};

int main() {
    Mere* m = new Fille();
    delete m;  // ❌ ~Fille() n'est pas appelé → fuite de moreData
    return 0;
}
```
> **Solution** : Rendre le destructeur de `Mere` `virtual`.

---

---
---

# **3. Résumé des Bonnes Pratiques** ✅

| **Sujet**               | **Bonne Pratique**                                                                 |
|-------------------------|------------------------------------------------------------------------------------|
| **Fonctions `virtual`** | Toujours utiliser `override` pour éviter les erreurs de signature.               |
| **Destructeurs**        | Toujours `virtual` si la classe est polymorphe.                                   |
| **Constructeurs**       | Toujours initialiser la classe mère dans la liste d'initialisation.             |
| **Appels `virtual`**    | Ne pas appeler de méthodes `virtual` dans les constructeurs/destructeurs.       |
| **Héritage multiple**    | Utiliser `virtual` pour éviter le problème du diamant (`class C : virtual public A`). |

---

# **4. Exercice Pratique** 📝

**Énoncé** :
Créez une hiérarchie de classes `Vehicule` → `Voiture` → `VoitureElectrique` avec :
1. Un destructeur `virtual` dans `Vehicule`.
2. Une méthode `virtual` `afficher()` redéfinie à chaque niveau.
3. Un constructeur dans `Voiture` prenant un paramètre (transmis à `Vehicule`).
4. Dans `main()`, créez une `VoitureElectrique` via un pointeur `Vehicule*` et appelez `afficher()`.

> **Solution** :
> ```cpp
> #include <iostream>
> using namespace std;
>
> class Vehicule {
> public:
>     Vehicule(const string& type) : type(type) {}
>     virtual ~Vehicule() { cout << "Détruit Vehicule" << endl; }
>     virtual void afficher() const { cout << "Type: " << type << endl; }
> private:
>     string type;
> };
>
> class Voiture : public Vehicule {
> public:
>     Voiture(const string& marque) : Vehicule("Voiture"), marque(marque) {}
>     void afficher() const override {
>         Vehicule::afficher();
>         cout << "Marque: " << marque << endl;
>     }
> private:
>     string marque;
> };
>
> class VoitureElectrique : public Voiture {
> public:
>     VoitureElectrique(const string& marque, int autonomie)
>         : Voiture(marque), autonomie(autonomie) {}
>     void afficher() const override {
>         Voiture::afficher();
>         cout << "Autonomie: " << autonomie << " km" << endl;
>     }
> private:
>     int autonomie;
> };
>
> int main() {
>     Vehicule* v = new VoitureElectrique("Tesla", 500);
>     v->afficher();  // Affiche tout (polymorphisme)
>     delete v;       // Destructeurs appelés dans le bon ordre
>     return 0;
> }
> ```

---

[...retorn e rèire](../../menu.md)