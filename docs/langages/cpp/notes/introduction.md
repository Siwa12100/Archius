
# **📚 Introduction au C++ : Guide Technique Approfondi**
*(Basé sur C++14)*

[...retorn en rèire](../menu.md)

---

## **🔹 1. Philosophie et Approches**
### **1.1. Approche Procédurale vs. Orientée Objet**
| **Approche Procédurale**                          | **Approche Objet (C++)**                          |
|--------------------------------------------------|--------------------------------------------------|
| ✅ Accent sur les **traitements** (fonctions).   | ✅ Accent sur les **données** (quelles données pour résoudre le problème ?). |
| ✅ Découpage en **fonctions**.                    | ✅ Regroupement des **méthodes** (traitements) et **attributs** (données) dans des **classes**. |
| ✅ Raffinements successifs (**top-down**).       | ✅ Développement **bottom-up** (construction de classes réutilisables). |
| ✅ Exemple : Algorithmes en C avec `stdio.h`.    | ✅ Exemple : Classes C++ avec `iostream`, encapsulation, héritage. |

> **Note** : En C++, on utilise **`c++14`** (standard moderne avec améliorations par rapport au C++98/03).

---

## **🔹 2. Extensions Clés du C++ par Rapport au C**
### **2.1. Généralités**
| **Fonctionnalité**               | **Description**                                                                 | **Exemple**                                                                 |
|----------------------------------|-------------------------------------------------------------------------------|-----------------------------------------------------------------------------|
| **Surcharge**                    | Plusieurs fonctions/opérateurs avec le même nom mais des paramètres différents. | `int add(int a, int b); double add(double a, double b);`                  |
| **Paramètres par défaut**        | Valeurs par défaut pour les paramètres de fonctions/méthodes.                | `void foo(int x = 0, int y = 1);`                                          |
| **Références (`&`)**             | Alias pour une variable (évite les pointeurs dans certains cas).             | `int a = 5; int &ref = a; ref = 10; // a vaut maintenant 10.`               |
| **`const` généralisé**            | Constantes typées et immuables.                                                | `const int MAX_SIZE = 100;`                                                  |
| **Allocation typée**             | `new`/`delete` remplacent `malloc`/`free` (typage fort).                        | `int* ptr = new int[10]; delete[] ptr;`                                      |
| **Bibliothèques I/O**            | `iostream` > `stdio.h` (plus sûr et orienté objet).                            | `cout << "Hello"; cin >> x;`                                                |
| **Exceptions**                   | Gestion structurée des erreurs (non couvert ici, mais basé sur des classes).  | `try { ... } catch (const exception& e) { ... }`                            |

### **2.2. Ajouts POO**
| **Concept**          | **Description**                                                                 | **Exemple**                                                                 |
|----------------------|-------------------------------------------------------------------------------|-----------------------------------------------------------------------------|
| **Encapsulation**    | Protection des données via `private`/`protected`.                            | `class Foo { private: int x; public: int getX() { return x; } };`           |
| **Polymorphisme**    | Méthodes virtuelles et surcharge.                                             | `virtual void draw() = 0;` (méthode abstraite).                            |
| **Généricité**       | Templates pour classes/fonctions.                                             | `template<typename T> T max(T a, T b) { return (a > b) ? a : b; }`          |
| **Héritage multiple**| Une classe peut hériter de plusieurs classes (risque de diamant).            | `class Child : public Parent1, public Parent2 { ... };`                     |

---

## **🔹 3. Structure d’un Programme C++**
### **3.1. Fichiers Source et En-têtes**
| **Fichier**       | **Rôle**                                                                                     | **Contenu Typique**                                                                 |
|--------------------|---------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------|
| **`.h` (Header)** | **Déclarations** (interface publique).                                                      | Définitions de classes, constantes, prototypes de fonctions, `include` nécessaires. |
| **`.cpp`**        | **Implémentation** (code réel).                                                              | Corps des méthodes, algorithmes, `include` du `.h` correspondant.                 |

> **Règle d’or** :
> - **1 classe = 1 `.h` + 1 `.cpp`**.
> - **Ne pas mettre d’`include` inutiles dans le `.h`** (seulement ceux nécessaires à la compréhension de l’interface).
> - Utiliser des **gardes d’inclusion** pour éviter les doublons :
>   ```cpp
>   #if !defined(MA_CLASSE_H)
>   #define MA_CLASSE_H
>   // ...
>   #endif
>   ```

### **3.2. Point d’Entrée**
```cpp
int main() { /* ... */ }                     // Version simple
int main(int argc, char* argv[]) { /* ... */ } // Avec arguments
```
> **Note** :
> - `argc` = nombre d’arguments.
> - `argv` = tableau de chaînes (nom du programme en `argv[0]`).
> - **`Ctrl+D`** (Linux) pour signaler la fin de l’entrée standard (`cin`).

---

## **🔹 4. Données et Types**
### **4.1. Déclaration de Variables**
Syntaxe :
`[descripteur] type déclarateur [initialisateur];`

| **Composant**       | **Exemples**                                                                 | **Remarques**                                                                 |
|----------------------|-----------------------------------------------------------------------------|-----------------------------------------------------------------------------|
| **Descripteur**      | `extern`, `static`, `const`, `virtual`                                      | Optionnel.                                                                   |
| **Type**            | `int`, `double`, `char`, `bool`, `MyClass*`                                  | Obligatoire.                                                                |
| **Déclarateur**      | `x`, `*ptr`, `&ref`, `tab[10]`, `func(int)`                                 | Peut inclure des opérateurs (`*`, `&`, `[]`, `()`).                        |
| **Initialisateur**  | `= 5`, `{1, 2, 3}`, `(42)`                                                  | Optionnel. `int i = 9;` ≡ `int i(9);`.                                      |

### **4.2. Types Primitifs et Dérivés**
| **Catégorie**       | **Types**                                                                     | **Exemple**                                                                 |
|----------------------|-----------------------------------------------------------------------------|-----------------------------------------------------------------------------|
| **Entiers**         | `int`, `short`, `long`, `long long`, `unsigned`                            | `unsigned int age = 25;`                                                    |
| **Virgule flottante** | `float`, `double`, `long double`                                           | `double pi = 3.14159;`                                                      |
| **Caractères**      | `char`, `wchar_t`, `char16_t`, `char32_t`                                  | `char c = 'A';`                                                             |
| **Booléen**         | `bool` (`true`/`false`)                                                      | `bool isValid = true;`                                                      |
| **Enumérés**        | `enum`                                                                       | `enum Color { RED, GREEN, BLUE };`                                          |
| **Tableaux**        | `T[n]` (taille constante)                                                   | `int arr[5] = {1, 2, 3, 4, 5};`                                             |
| **Pointeurs**       | `T*`                                                                         | `int* ptr = &x;`                                                            |
| **Références**      | `T&`                                                                         | `int& ref = x;`                                                             |
| **`void`**          | Absence de type (pour fonctions sans retour).                              | `void foo();`                                                              |

> **⚠️ Piège** :
> - **Tableaux** : Toujours utiliser des constantes pour la taille :
>   ```cpp
>   #define SIZE 10
>   int tab[SIZE]; // ✅ Correct
>   int tab[10];   // ❌ À éviter (magique number)
>   ```
> - **Initialisation** : `int i(9);` est préférable à `int i = 9;` (uniformité avec les classes).

---

## **🔹 5. Pointeurs et Références : Concepts Avancés**
### **5.1. Pointeurs**
| **Opération**               | **Syntaxe**               | **Exemple**                                                                 |
|------------------------------|---------------------------|-----------------------------------------------------------------------------|
| **Déclaration**              | `T* ptr;`                 | `int* ptr;`                                                                 |
| **Adresse de**               | `&var`                    | `ptr = &x;`                                                                 |
| **Déréférencement**          | `*ptr`                    | `*ptr = 42;` (modifie la valeur pointée).                                  |
| **Arithmétique**             | `ptr + n`                 | Avance de `n * sizeof(T)` octets.                                           |
| **Comparaison**              | `ptr == nullptr`          | Vérifie si le pointeur est nul.                                            |
| **Allocation dynamique**     | `new T`, `new T[n]`       | `int* arr = new int[10];`                                                   |
| **Libération**               | `delete ptr`, `delete[] ptr` | **Obligatoire** pour éviter les fuites mémoire.                          |

> **⚠️ Pièges Courants** :
> 1. **Déréférencement de `nullptr`** → **Plantage**.
> 2. **Oublier `delete`/`delete[]`** → **Fuite mémoire**.
> 3. **Utiliser `delete` au lieu de `delete[]` (ou inversement)** → **Comportement indéfini**.
> 4. **Retourner un pointeur vers une variable locale** → **Dangling pointer**.

### **5.2. Références**
| **Caractéristique**          | **Exemple**                                                                 |
|------------------------------|-----------------------------------------------------------------------------|
| **Alias pour une variable**  | `int x = 5; int& ref = x; ref = 10;` → `x` vaut maintenant `10`.           |
| **Doit être initialisée**    | `int& ref;` → **Erreur de compilation**.                                    |
| **Pas de réaffectation**     | `int& ref = x; ref = &y;` → **Impossible** (référence toujours liée à `x`). |
| **Utilisation typique**      | Passage de paramètres sans copie (efficace pour les gros objets).         |

> **⚠️ Différence Clé avec les Pointeurs** :
> - Une référence **ne peut pas être nulle** (contrairement à un pointeur).
> - Pas d’arithmétique possible sur les références.

### **5.3. Pointeurs vs. Références**
| **Critère**          | **Pointeurs (`T*`)**                     | **Références (`T&`)**                     |
|----------------------|------------------------------------------|------------------------------------------|
| **Nullité**          | Peut être `nullptr`.                     | Toujours liée à un objet.               |
| **Réaffectation**    | Possible (`ptr = &other;`).              | Impossible.                              |
| **Syntaxe**          | `*ptr` pour accéder à la valeur.        | Utilisation directe (`ref.member`).      |
| **Initialisation**   | Peut être non initialisé.               | **Doit** être initialisée.               |
| **Cas d’usage**      | Allocation dynamique, structures de données. | Passage de paramètres, retour de fonctions. |

---

## **🔹 6. Gestion de la Mémoire Dynamique**
### **6.1. `new` et `delete`**
| **Opération**       | **Syntaxe**               | **Comportement**                                                                 |
|----------------------|---------------------------|---------------------------------------------------------------------------------|
| **Allocation**       | `new T`                   | Alloue un objet de type `T` et appelle son constructeur.                        |
| **Allocation tableau** | `new T[n]`               | Alloue un tableau de `n` objets `T` (constructeurs appelés pour chaque élément). |
| **Libération**       | `delete ptr`              | Appelle le destructeur et libère la mémoire.                                  |
| **Libération tableau** | `delete[] ptr`          | Libère un tableau alloué avec `new[]`. **⚠️ Ne pas mélanger `new`/`delete[]` !** |

> **⚠️ Bonnes Pratiques** :
> - Toujours **appeler `delete` pour chaque `new`** (même dans les exceptions).
> - Préférer les **pointeurs intelligents** (`unique_ptr`, `shared_ptr`) en C++ moderne.
> - **Ne jamais faire** :
>   ```cpp
>   int* ptr1 = new int(42);
>   int* ptr2 = ptr1;
>   delete ptr1;
>   delete ptr2; // ❌ Double suppression → Comportement indéfini !
>   ```

### **6.2. Exemple Complet**
```cpp
// Allocation dynamique d'un tableau d'entiers
int* dynamicArray = new int[10];
for (int i = 0; i < 10; ++i) {
    dynamicArray[i] = i * 2;
}
// Libération obligatoire
delete[] dynamicArray; // ✅ Correct
// delete dynamicArray; // ❌ Erreur (doit être delete[])
```

---

## **🔹 7. Opérateurs en Profondeur**
### **7.1. Priorité et Associativité**
| **Priorité** | **Opérateurs** (du plus prioritaire au moins) | **Associativité** |
|--------------|-----------------------------------------------|-------------------|
| 1            | `::` (résolution de portée)                  | Gauche à droite   |
| 2            | `++` (postfix), `--` (postfix), `()` (appel), `[]` | Gauche à droite   |
| 3            | `++` (prefix), `--` (prefix), `!`, `~`, `+`, `-` (unaire), `*` (déréf), `&` (adresse), `sizeof`, `new`, `delete` | Droite à gauche |
| 4            | `.*`, `->*` (accès membre via pointeur)      | Gauche à droite   |
| 5            | `*`, `/`, `%`                               | Gauche à droite   |
| 6            | `+`, `-` (binaire)                           | Gauche à droite   |
| 7            | `<<`, `>>` (décalage)                       | Gauche à droite   |
| 8            | `<`, `<=`, `>`, `>=`                        | Gauche à droite   |
| 9            | `==`, `!=`                                  | Gauche à droite   |
| 10           | `&` (ET bit à bit)                           | Gauche à droite   |
| 11           | `^` (XOR)                                   | Gauche à droite   |
| 12           | `|` (OU bit à bit)                          | Gauche à droite   |
| 13           | `&&` (ET logique)                           | Gauche à droite   |
| 14           | `||` (OU logique)                           | Gauche à droite   |
| 15           | `?:` (ternaire)                              | Droite à gauche   |
| 16           | `=`, `+=`, `-=`, `*=`, etc.                 | Droite à gauche   |

> **⚠️ Piège** :
> - **`=` vs `==`** : `if (x = 5)` est **légal** (affectation) mais probablement une erreur (vouliez-vous `x == 5` ?).
> - **Opérateurs surchargés** : Leur priorité reste celle de l’opérateur de base (ex: `<<` pour `cout` a une priorité basse).

### **7.2. Surcharge d’Opérateurs**
```cpp
class Complex {
private:
    double real, imag;
public:
    Complex(double r = 0, double i = 0) : real(r), imag(i) {}
    // Surcharge de l'opérateur +
    Complex operator+(const Complex& other) const {
        return Complex(real + other.real, imag + other.imag);
    }
    // Surcharge de l'opérateur << (doit être une fonction globale)
    friend ostream& operator<<(ostream& os, const Complex& c) {
        os << c.real << " + " << c.imag << "i";
        return os;
    }
};
```
> **Règles** :
> - Les opérateurs **doivent** respecter leur sémantique naturelle (ex: `+` doit être commutatif).
> - Certains opérateurs **doivent** être membres (ex: `=`, `[]`, `->`).
> - D’autres **doivent** être globaux (ex: `<<`, `>>`).

---

## **🔹 8. Programmation Orientée Objet (POO)**
### **8.1. Classes et Objets**
#### **Squelette d’une Classe**
```cpp
// Xxx.h
#if !defined(XXX_H)
#define XXX_H

//---- Interfaces utilisées
#include <iostream>
//---- Constantes
const int DEFAULT_SIZE = 10;
//---- Types
typedef double Prix;

// Rôle de la classe <Xxx>
class Xxx : public Ancetre {
public:
    //---- Méthodes publiques
    void methodePublique();
    //---- Surcharge d'opérateurs
    Xxx& operator=(const Xxx& autre);
    //---- Constructeurs / Destructeur
    Xxx();
    ~Xxx();
protected:
    //---- Méthodes protégées
    void methodeProtegee();
    //---- Attributs protégés
    int attribut;
};

#endif
```

#### **Squelette du `.cpp`**
```cpp
// Xxx.cpp
#include "Xxx.h"
#include <iostream> // Includes nécessaires à l'implémentation (pas dans le .h !)

using namespace std;

//---- Définition des méthodes
void Xxx::methodePublique() {
    // Implémentation
}
```

> **⚠️ Contrat de Cohérence** :
> - Si une méthode reçoit un **tableau en paramètre**, elle **doit** aussi recevoir sa **taille** (sinon, impossible de connaître sa taille en C++).

### **8.2. Constructeurs et Destructeurs**
| **Type**               | **Syntaxe**                          | **Rôle**                                                                 |
|------------------------|--------------------------------------|--------------------------------------------------------------------------|
| **Par défaut**         | `Xxx() { ... }`                     | Initialise les attributs avec des valeurs par défaut.                   |
| **Paramétré**          | `Xxx(int x) { ... }`                | Initialise avec des paramètres.                                          |
| **De copie**           | `Xxx(const Xxx& autre) { ... }`      | Crée une copie profonde d’un objet.                                      |
| **Destructeur**        | `~Xxx() { ... }`                    | Libère les ressources (mémoire, fichiers, etc.).                        |
| **Liste d’initialisation** | `Xxx() : attr1(val1), attr2(val2) {}` | **Meilleure pratique** pour initialiser les attributs.                   |

> **⚠️ Pièges** :
> - **Oublier le constructeur de copie** → Copie superficielle (problème si l’objet contient des pointeurs).
> - **Ne pas respecter la forme canonique de Coplien** (4 méthodes essentielles : constructeur par défaut, constructeur de copie, opérateur `=`, destructeur).

### **8.3. Héritage**
| **Concept**           | **Syntaxe**                          | **Exemple**                                                                 |
|-----------------------|--------------------------------------|-----------------------------------------------------------------------------|
| **Simple**            | `class Enfant : public Parent { ... };` | Héritage public (le plus courant).                                         |
| **Multiple**          | `class Enfant : public P1, public P2 {}`; | **Risque de diamant** (ambiguïté si `P1` et `P2` héritent d’une même classe). |
| **Visibilité**        | `private`, `protected`, `public`     | `public` : conserve les niveaux d’accès du parent.                         |
| **Appel constructeur parent** | `Enfant() : Parent(args) { ... }` | **Obligatoire** si le parent n’a pas de constructeur par défaut.          |

> **⚠️ Problème du Diamant** :
> ```cpp
> class A {};
> class B : public A {};
> class C : public A {};
> class D : public B, public C {}; // ❌ Ambiguïté : D hérite deux fois de A !
> ```
> **Solution** : Héritage virtuel (`class B : virtual public A {};`).

### **8.4. Polymorphisme**
| **Concept**           | **Syntaxe**                          | **Exemple**                                                                 |
|-----------------------|--------------------------------------|-----------------------------------------------------------------------------|
| **Méthodes virtuelles** | `virtual void methode() { ... }`    | Permet la redéfinition dans les classes dérivées.                         |
| **Méthodes pures**    | `virtual void methode() = 0;`       | Rend la classe **abstraite** (ne peut pas être instanciée).                |
| **Redéfinition**      | `void methode() override { ... }`    | **C++11** : `override` pour clarifier.                                      |
| **Dynamic Cast**      | `dynamic_cast<Derived*>(basePtr)`   | Conversion sécurisée en temps d’exécution (vérifie le type).               |

> **Exemple Complet** :
> ```cpp
> class Animal {
> public:
>     virtual void cri() const { cout << "..." << endl; }
> };
>
> class Chien : public Animal {
> public:
>     void cri() const override { cout << "Woof!" << endl; }
> };
>
> int main() {
>     Animal* a = new Chien();
>     a->cri(); // Affiche "Woof!" (polymorphisme)
>     delete a;
>     return 0;
> }
> ```

### **8.5. Classes Abstraites et Interfaces**
```cpp
class Forme {
public:
    virtual double aire() const = 0; // Méthode pure virtuelle
    virtual ~Forme() = default;      // Destructeur virtuel (important pour le polymorphisme)
};

class Carre : public Forme {
private:
    double cote;
public:
    Carre(double c) : cote(c) {}
    double aire() const override { return cote * cote; }
};
```
> **Règles** :
> - Une classe avec **au moins une méthode pure virtuelle** est **abstraite**.
> - **Destructeur virtuel** : **Obligatoire** si la classe est destinée à être héritée (pour éviter les fuites mémoire lors du `delete` via un pointeur de base).

---

## **🔹 9. Gestion des Exceptions (Aperçu)**
*(Non couvert en détail, mais basé sur des classes.)*
```cpp
try {
    // Code susceptible de lancer une exception
    throw runtime_error("Erreur critique!");
} catch (const exception& e) {
    cerr << "Erreur: " << e.what() << endl;
} catch (...) {
    cerr << "Erreur inconnue!" << endl;
}
```
> **Bonnes Pratiques** :
> - **Ne pas attraper `...`** (trop générique) sauf pour nettoyer avant de relancer.
> - **Utiliser les exceptions standard** (`runtime_error`, `logic_error`, etc.).
> - **Libérer les ressources** dans les blocs `try` (ou utiliser RAII).

---

## **🔹 10. Préprocesseur et Compilation**
### **10.1. `#include`**
| **Syntaxe**       | **Comportement**                                                                 |
|--------------------|---------------------------------------------------------------------------------|
| `#include <fichier>` | Cherche dans les répertoires standard (`/usr/include` sous Linux).             |
| `#include "fichier"` | Cherche d’abord dans le répertoire courant, puis dans les répertoires standard. |

> **Bonnes Pratiques** :
> - **Ordre des includes** :
>   1. Fichiers `.h` du projet.
>   2. Bibliothèques tierces.
>   3. Bibliothèques standard (`<iostream>`, etc.).
> - **Éviter les includes inutiles** dans les `.h` (ralentit la compilation).

### **10.2. `#define` (Peu Recommandé en C++)**
| **Usage**               | **Exemple**                                                                 |
|-------------------------|-----------------------------------------------------------------------------|
| **Constantes**          | `#define MAX_SIZE 100` (préférer `const int MAX_SIZE = 100;`).              |
| **Macros**              | `#define CARRE(x) ((x) * (x))` (risque d’effets de bord).                   |
| **Compilation conditionnelle** | `#ifdef DEBUG ... #endif` (utile pour le débogage).               |

> **⚠️ Pourquoi éviter `#define` ?** :
> - **Pas de typage** (remplacement textuel brut).
> - **Risque d’effets de bord** :
>   ```cpp
>   #define CARRE(x) x*x
>   CARRE(a + b) // Développe en a + b * a + b → Erreur !
>   ```
> - **Préférer** :
>   - `const`/`constexpr` pour les constantes.
>   - `inline` pour les petites fonctions.
>   - `template` pour la généricité.

---

## **🔹 11. Structures et Unions**
### **11.1. Structures (`struct`)**
- **Équivalent à une classe où tout est `public` par défaut**.
- **Utilisation** : Regrouper des données **sans comportement** (sinon, utiliser une classe).
```cpp
struct Etudiant {
    char nom[30];
    int numEtudiant;
    enum Section { S3IF1, S3IF2, S3IF3 } section;
};

Etudiant e1 = {"Alice", 12345, Etudiant::S3IF1};
Etudiant* ptr = &e1;
cout << ptr->nom; // Accès via ->
```

### **11.2. Unions (`union`)**
- **Tous les membres partagent la même mémoire** (taille = taille du plus grand membre).
- **Utilisation** : Économiser de la mémoire quand un seul membre est utilisé à la fois.
```cpp
union Data {
    int i;
    float f;
    char str[20];
};

Data d;
d.i = 42;
cout << d.i; // OK
// cout << d.f; // ❌ Comportement indéfini (f n'est pas initialisé)
```

> **⚠️ Piège** :
> - **Accéder à un membre non actif** → Comportement indéfini.
> - **Pas de constructeur/destructeur** dans les unions simples (utiliser `std::variant` en C++17+).

---

## **🔹 12. Fonctions Avancées**
### **12.1. Passage de Paramètres**
| **Méthode**          | **Syntaxe**               | **Utilisation**                                                                 |
|----------------------|---------------------------|-----------------------------------------------------------------------------|
| **Par valeur**       | `void foo(int x)`         | Copie de la valeur (sûr, mais coûteux pour les gros objets).               |
| **Par pointeur**     | `void foo(int* ptr)`      | Modifie l’original. Risque de `nullptr`.                                    |
| **Par référence**    | `void foo(int& ref)`      | **Préféré** : pas de copie, pas de risque de `nullptr`.                     |
| **Reference-to-const** | `void foo(const int& ref)` | Lecture seule (efficace pour les gros objets).                            |

> **Exemple** :
> ```cpp
> void increment(int& x) { x++; } // Modifie l'original
> void print(const string& s) { cout << s; } // Lecture seule, pas de copie
> ```

### **12.2. Retour de Fonction**
| **Type de Retour**   | **Syntaxe**               | **Risques**                                                                 |
|----------------------|---------------------------|-----------------------------------------------------------------------------|
| **Par valeur**       | `int foo() { return 42; }` | Copie (coûteux pour les gros objets).                                      |
| **Par référence**    | `int& foo() { return x; }` | **Danger** : Ne jamais retourner une référence vers une variable locale ! |
| **Par pointeur**     | `int* foo() { return &x; }` | **Danger** : Idem que ci-dessus.                                           |

> **⚠️ Erreur Courante** :
> ```cpp
> int& badFunction() {
>     int local = 42;
>     return local; // ❌ local est détruit à la fin de la fonction !
> }
> ```

### **12.3. Paramètres par Défaut**
```cpp
void foo(int x, int y = 0, int z = 10); // y et z ont des valeurs par défaut
```
> **Règles** :
> - Les paramètres avec valeurs par défaut **doivent être à droite**.
> - **Appel** :
>   ```cpp
>   foo(1);      // x=1, y=0, z=10
>   foo(1, 2);   // x=1, y=2, z=10
>   foo(1, 2, 3); // x=1, y=2, z=3
>   ```

### **12.4. Fonctions Inline**
```cpp
inline int square(int x) { return x * x; }
```
> **À savoir** :
> - **`inline`** est une **suggestion** au compilateur (pas une garantie).
> - Utile pour les **petites fonctions** appelées fréquemment.
> - **Éviter** pour les grosses fonctions (code dupliqué → binaire plus gros).

---

## **🔹 13. Surcharge de Fonctions**
- Plusieurs fonctions avec le **même nom** mais des **paramètres différents**.
- **Le retour ne fait pas partie de la signature** (ne peut pas surcharger uniquement sur le type de retour).

```cpp
int add(int a, int b) { return a + b; }
double add(double a, double b) { return a + b; }
```
> **⚠️ Piège** :
> - **Conversions implicites** peuvent mener à des appels ambigus :
>   ```cpp
>   void foo(int);
>   void foo(double);
>   foo(42); // OK, appelle foo(int)
>   foo(3.14); // OK, appelle foo(double)
>   foo(42.0); // ❌ Ambiguïté si pas de foo(double) !
>   ```

---

## **🔹 14. Templates (Généricité)**
### **14.1. Fonctions Templates**
```cpp
template<typename T>
T max(T a, T b) {
    return (a > b) ? a : b;
}

int main() {
    cout << max(3, 5) << endl;      // int
    cout << max(3.14, 2.71) << endl; // double
}
```
> **Spécialisation** :
> ```cpp
> template<>
> const char* max<const char*>(const char* a, const char* b) {
>     return (strcmp(a, b) > 0) ? a : b;
> }
> ```

### **14.2. Classes Templates**
```cpp
template<typename T>
class Boite {
private:
    T contenu;
public:
    Boite(T c) : contenu(c) {}
    T getContenu() const { return contenu; }
};

int main() {
    Boite<int> boiteInt(42);
    Boite<string> boiteString("Hello");
}
```
> **⚠️ Pièges** :
> - **Erreurs de compilation** : Les templates sont instanciés à l’appel → erreurs parfois cryptiques.
> - **Fichiers `.h` obligatoires** : Le code des templates doit être visible à l’instantiation (donc généralement tout dans le `.h`).

---

## **🔹 15. Entrées/Sorties (I/O)**
### **15.1. Flots Standard**
| **Flot** | **Description**               | **Exemple**                     |
|----------|--------------------------------|---------------------------------|
| `cin`    | Entrée standard (clavier).     | `int x; cin >> x;`              |
| `cout`   | Sortie standard (écran).       | `cout << "Hello";`              |
| `cerr`   | Sortie d’erreur (non bufferisée). | `cerr << "Erreur !";`          |
| `clog`   | Sortie d’erreur (bufferisée). | `clog << "Log...";`            |

> **Manipulateurs** :
> - `endl` : Saut de ligne + **vidage du buffer** (coûteux, préférer `\n` si pas besoin de vider le buffer).
> - `setw(n)` : Définit la largeur du champ.
> - `hex`, `oct`, `dec` : Change la base d’affichage.

### **15.2. Fichiers**
```cpp
#include <fstream>

// Écriture
ofstream out("fichier.txt");
if (out) {
    out << "Bonjour les fichiers !" << endl;
}
out.close();

// Lecture
ifstream in("fichier.txt");
if (in) {
    string ligne;
    while (getline(in, ligne)) {
        cout << ligne << endl;
    }
}
in.close();
```
> **Bonnes Pratiques** :
> - **Toujours vérifier l’ouverture** (`if (fichier)`).
> - **Fermer les fichiers** explicitement (ou utiliser RAII avec les constructeurs/destructeurs).

---

## **🔹 16. Bonnes Pratiques et Pièges**
### **16.1. Gestion de la Mémoire**
| **Mauvaise Pratique**            | **Bonne Pratique**                          | **Pourquoi**                                                                 |
|-----------------------------------|---------------------------------------------|-----------------------------------------------------------------------------|
| `int* ptr = new int[10];` sans `delete[]` | `std::vector<int> vec(10);`                 | Évite les fuites mémoire.                                                   |
| `delete ptr;` sur un tableau      | `delete[] ptr;`                             | Comportement indéfini.                                                     |
| Retourner un pointeur local      | Retourner une copie ou utiliser des références valides. | Le pointeur devient invalide après la fin de la fonction.                 |

### **16.2. Pointeurs et Références**
| **À Éviter**                      | **À Faire**                                  | **Pourquoi**                                                                 |
|-----------------------------------|---------------------------------------------|-----------------------------------------------------------------------------|
| `int& ref;` (non initialisé)      | `int x = 0; int& ref = x;`                  | Les références doivent toujours être initialisées.                        |
| `if (ptr)` sans vérifier `nullptr` | `if (ptr != nullptr)`                       | Évite les crashes.                                                          |
| Passer des gros objets par valeur | Passer par `const&`                         | Évite les copies coûteuses.                                                 |

### **16.3. Classes et POO**
| **Mauvaise Pratique**            | **Bonne Pratique**                          | **Pourquoi**                                                                 |
|-----------------------------------|---------------------------------------------|-----------------------------------------------------------------------------|
| Oublier le destructeur virtuel   | `virtual ~Base() = default;`                | Permet un `delete` correct via un pointeur de base.                        |
| Attributs publics                 | Utiliser des getters/setters                | Meilleure encapsulation.                                                   |
| Héritage multiple sans besoin     | Préférer la composition                     | Évite les problèmes de diamant.                                            |

### **16.4. Performances**
| **À Éviter**                      | **À Faire**                                  | **Pourquoi**                                                                 |
|-----------------------------------|---------------------------------------------|-----------------------------------------------------------------------------|
| `endl` systématique               | `\n`                                        | `endl` vide le buffer (coûteux).                                            |
| Copies inutiles                   | Passer par référence (`const&`)             | Évite les copies de gros objets.                                           |
| Boucles `for` inefficaces          | Utiliser des algorithmes STL (`std::sort`)  | La STL est optimisée.                                                      |

---

## **🔹 17. Exemple Complet : Classe `Vecteur`**
### **17.1. Interface (`Vecteur.h`)**
```cpp
#if !defined(VECTEUR_H)
#define VECTEUR_H

#include <iostream>

class Vecteur {
private:
    int* data;
    size_t size;
public:
    // Constructeurs
    Vecteur(size_t size = 10);
    Vecteur(const Vecteur& autre); // Constructeur de copie
    ~Vecteur(); // Destructeur

    // Opérateur d'affectation
    Vecteur& operator=(const Vecteur& autre);

    // Accès aux éléments
    int& operator[](size_t index);
    const int& operator[](size_t index) const;

    // Affichage
    void afficher() const;
};

#endif
```

### **17.2. Implémentation (`Vecteur.cpp`)**
```cpp
#include "Vecteur.h"
#include <algorithm> // pour std::copy

Vecteur::Vecteur(size_t size) : size(size), data(new int[size]) {}

Vecteur::Vecteur(const Vecteur& autre) : size(autre.size), data(new int[size]) {
    std::copy(autre.data, autre.data + size, data);
}

Vecteur::~Vecteur() {
    delete[] data;
}

Vecteur& Vecteur::operator=(const Vecteur& autre) {
    if (this != &autre) { // Évite l'auto-affectation
        delete[] data;
        size = autre.size;
        data = new int[size];
        std::copy(autre.data, autre.data + size, data);
    }
    return *this;
}

int& Vecteur::operator[](size_t index) {
    return data[index];
}

const int& Vecteur::operator[](size_t index) const {
    return data[index];
}

void Vecteur::afficher() const {
    for (size_t i = 0; i < size; ++i) {
        std::cout << data[i] << " ";
    }
    std::cout << std::endl;
}
```

### **17.3. Utilisation (`main.cpp`)**
```cpp
#include "Vecteur.h"

int main() {
    Vecteur v1(5);
    for (size_t i = 0; i < 5; ++i) {
        v1[i] = i * 2;
    }
    v1.afficher(); // 0 2 4 6 8

    Vecteur v2 = v1; // Appel du constructeur de copie
    v2[0] = 100;
    v2.afficher(); // 100 2 4 6 8

    Vecteur v3(3);
    v3 = v1; // Appel de l'opérateur =
    v3.afficher(); // 0 2 4 6 8 (troncature si v3.size < v1.size)

    return 0;
}
```

> **Points Clés** :
> - **Constructeur de copie** : Copie profonde pour éviter les pointeurs partagés.
> - **Opérateur `=`** : Vérifie l’auto-affectation et gère la mémoire correctement.
> - **Destructeur** : Libère la mémoire pour éviter les fuites.
> - **Forme canonique de Coplien** : Respectée (constructeur de copie, opérateur `=`, destructeur).

---

## **🔹 18. Ressources et Outils**
### **18.1. Débogage**
| **Outil**       | **Utilisation**                                                                 |
|------------------|-------------------------------------------------------------------------------|
| **GDB**         | Débogueur en ligne de commande (`gdb ./mon_programme`).                       |
| **Valgrind**    | Détection de fuites mémoire (`valgrind --leak-check=full ./mon_programme`).   |
| **AddressSanitizer** | Compilation avec `-fsanitize=address` pour détecter les erreurs mémoire.   |

### **18.2. Analyse Statique**
| **Outil**       | **Utilisation**                                                                 |
|------------------|-------------------------------------------------------------------------------|
| **clang-tidy**   | Analyse de code (`clang-tidy mon_fichier.cpp --checks=-*,modernize-*`).     |
| **cppcheck**     | Détection d’erreurs (`cppcheck --enable=all mon_fichier.cpp`).               |

### **18.3. Tests Unitaires**
| **Framework**   | **Utilisation**                                                                 |
|------------------|-------------------------------------------------------------------------------|
| **Google Test**  | Tests unitaires en C++ (`EXPECT_EQ`, `ASSERT_TRUE`).                         |
| **Catch2**      | Framework moderne et simple.                                                   |

---

## **🔹 19. Résumé des Concepts Clés**
| **Concept**               | **Description**                                                                 | **Exemple**                                                                 |
|---------------------------|-------------------------------------------------------------------------------|-----------------------------------------------------------------------------|
| **Encapsulation**         | Protection des données via `private`/`protected`.                            | `class Foo { private: int x; };`                                           |
| **Héritage**              | Réutilisation de code via `class Enfant : public Parent`.                     | `class Chien : public Animal { ... };`                                      |
| **Polymorphisme**          | Comportement différent selon le type réel (méthodes virtuelles).             | `virtual void cri() const;`                                                |
| **Surcharge**              | Plusieurs méthodes avec le même nom mais des paramètres différents.         | `void foo(int); void foo(double);`                                         |
| **Templates**             | Généricité pour classes/fonctions.                                            | `template<typename T> T max(T a, T b);`                                    |
| **Gestion mémoire**        | `new`/`delete` pour l’allocation dynamique.                                  | `int* ptr = new int; delete ptr;`                                          |
| **Références**            | Alias pour une variable (pas de `nullptr`).                                  | `int& ref = x;`                                                            |
| **Exceptions**            | Gestion structurée des erreurs.                                               | `try { ... } catch (const exception& e) { ... }`                           |

---

## **🔹 20. Pour Aller Plus Loin**
### **20.1. Livres Recommandés**
- **"Le Langage C++"** – Bjarne Stroustrup (créateur du C++).
- **"Effective C++"** – Scott Meyers (55 bonnes pratiques).
- **"C++ Primer"** – Lippman, Lajoie, Moo (pour débutants).
- **"Modern C++ Design"** – Andrei Alexandrescu (techniques avancées).

### **20.2. Standards C++
| **Standard** | **Année** | **Nouveautés Clés**                                                                 |
|--------------|-----------|-------------------------------------------------------------------------------------|
| C++98/03     | 1998/2003 | STL, templates, exceptions.                                                        |
| C++11        | 2011      | `auto`, lambdas, smart pointers, `nullptr`, `constexpr`.                          |
| C++14        | 2014      | Améliorations mineures (génériques lambdas, `constexpr` étendu).                  |
| C++17        | 2017      | `if constexpr`, `std::optional`, `std::variant`, filesystems.                     |
| C++20        | 2020      | Coroutines, concepts, ranges, `std::span`.                                          |
| C++23        | 2023      | Améliorations des ranges, `std::print`, `std::expected`.                          |

### **20.3. Sites Utiles**
- [cppreference.com](https://en.cppreference.com/) (documentation complète).
- [isocpp.org](https://isocpp.org/) (site officiel du standard C++).
- [Godbolt Compiler Explorer](https://godbolt.org/) (voir le code assembleur généré).

---

[...retorn en rèire](../menu.md)