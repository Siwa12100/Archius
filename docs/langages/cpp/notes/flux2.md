# 🌊 Flux C++11, `ifstream` / `ofstream`, `<<`, `>>`, `endl` 

[...retorn en rèire](../menu.md)

---

> Objectif :  
> Être **totalement à l’aise** avec :
> - les **flux** (`istream`, `ostream`, `ifstream`, `ofstream`, `fstream`, `cin`, `cout`, …),
> - la **lecture / écriture** dans la console et les fichiers,
> - la surcharge de `<<` / `>>` pour tes **types perso**,
> - le “bordel” avec `std::endl` et les **manipulateurs**.

Tout est valable en **C++11** (et plus).  
Format : **progressif**, exemples **copiables en DS**, avec quelques emojis pour respirer 🙂.

---

## 1. Panorama des flux en C++11

### 1.1. Les grandes familles de flux

En C++, l’IO se fait via des **objets flux** (streams) :

- `std::istream` : flux **d’entrée** (lecture)  
  → base de `std::cin`, `std::ifstream`, `std::istringstream`, …
- `std::ostream` : flux **de sortie** (écriture)  
  → base de `std::cout`, `std::cerr`, `std::ofstream`, `std::ostringstream`, …
- `std::iostream` : **entrée + sortie**  
  → base de `std::fstream`, `std::stringstream`, …

Spécialisés fichiers (dans `<fstream>`) :

- `std::ifstream` : lecture depuis un fichier (`istream` spécialisé)
- `std::ofstream` : écriture dans un fichier (`ostream` spécialisé)
- `std::fstream` : lecture + écriture fichier (`iostream` spécialisé)

Idée clé 🤓 :

> `std::ofstream` **est un** `std::ostream`  
> `std::ifstream` **est un** `std::istream`  
> donc les **mêmes opérateurs** `<<` et `>>` marchent pour `cout` ET pour les fichiers.

---

### 1.2. Flux standard (console)

Dans `<iostream>` :

```cpp
std::istream  cin;   // entrée standard (clavier)
std::ostream  cout;  // sortie standard
std::ostream  cerr;  // erreurs
std::ostream  clog;  // logs
```

Usage classique :

```cpp
int x;
std::cout << "Entrez un entier : ";
std::cin  >> x;
std::cout << "Vous avez tapé : " << x << std::endl;
```

---

## 2. Utilisation basique de `ofstream` / `ifstream` 📄

### 2.1. Inclure le bon header

```cpp
#include <fstream>   // ofstream, ifstream, fstream
#include <iostream>  // cout, cin, cerr
#include <string>
```

### 2.2. Écriture dans un fichier avec `ofstream`

```cpp
#include <fstream>
#include <iostream>

int main() {
    std::ofstream out("data.txt");  // ouverture du fichier

    if (!out) { // équivalent à !out.is_open() ou out.fail()
        std::cerr << "Erreur d'ouverture du fichier en écriture\n";
        return 1;
    }

    out << "Hello file!" << std::endl;
    out << 42 << " " << 3.14 << std::endl;

    // out.close();  // facultatif : appelé automatiquement au destructeur
    return 0;
}
```

Points clés :

- Constructeur `std::ofstream out("fichier.txt");` → ouvre en **écriture** (mode texte, `ios::out | ios::trunc`).
- On teste l’ouverture par `if (!out)` ou `if (!out.is_open())`.
- On écrit **exactement comme** sur `cout` : `out << ...`.

---

### 2.3. Lecture depuis un fichier avec `ifstream`

```cpp
#include <fstream>
#include <iostream>
#include <string>

int main() {
    std::ifstream in("data.txt");

    if (!in) {
        std::cerr << "Erreur d'ouverture du fichier en lecture\n";
        return 1;
    }

    std::string word;
    int number;

    in >> word >> number;  // lit formaté : mot puis entier

    std::cout << "Mot : " << word << ", nombre : " << number << std::endl;

    return 0;
}
```

Même pattern que pour `cin` :

- `in >> variable;` pour lire au format texte (séparé par espaces / newlines).
- On teste l’état du flux : `if (!in)` → erreur / fin de fichier.

---

### 2.4. Lire ligne par ligne avec `std::getline`

```cpp
std::ifstream in("data.txt");

if (!in) { /* ... */ }

std::string line;
while (std::getline(in, line)) {        // jusqu'à EOF
    std::cout << "Ligne : " << line << std::endl;
}
```

À retenir :

- `std::getline` lit **une ligne entière** (jusqu’à `\n`).
- À utiliser si le format n’est pas juste “mots séparés par des espaces”.

---

## 3. Modes d’ouverture (`std::ios::...`) ⚙️

On peut spécifier des **flags** :

```cpp
std::ofstream out("data.txt", std::ios::out | std::ios::app);
```

Flags principaux :

- `std::ios::in`   : ouverture en **lecture**
- `std::ios::out`  : ouverture en **écriture**
- `std::ios::app`  : ajout à la fin (append)
- `std::ios::trunc`: tronquer (effacer le fichier avant d’écrire)  
  *(par défaut avec `ios::out`)*
- `std::ios::binary` : mode **binaire**
- `std::ios::ate` : se positionner à la fin dès l’ouverture

Exemples :

```cpp
// ajout à la fin sans effacer
std::ofstream out("log.txt", std::ios::out | std::ios::app);

// lecture + écriture
std::fstream file("data.bin", std::ios::in | std::ios::out | std::ios::binary);
```

En DS, une phrase du type :

> “On peut préciser le **mode d’ouverture** en combinant des flags `std::ios::...` avec l’opérateur `|`.”

fait très bonne impression 😉

---

## 4. Surcharge de `<<` / `>>` pour tes types perso 🎭

C’est souvent là que tu te perds, on remet tout à plat.

### 4.1. Idée centrale

On veut écrire :

```cpp
Point p(1, 2);
std::cout  << p << std::endl;
std::ofstream out("points.txt");
out << p << std::endl;
```

Donc on a besoin d’un **opérateur global** :

```cpp
std::ostream & operator<<(std::ostream &os, const Point &p);
```

Pourquoi **global** (fonction libre) ?

- Parce que le **premier argument** doit être un `std::ostream&`,  
  or tu ne peux pas ajouter une méthode à `std::ostream` lui-même.
- Parce qu’il doit marcher avec **tous** les flux de sortie (`cout`, `ofstream`, `ostringstream`, …).

---

### 4.2. Modèle à recopier : `operator<<` pour un `Point`

```cpp
#include <iostream>

class Point {
    int x, y;

public:
    Point(int x = 0, int y = 0) : x(x), y(y) {}

    // Déclaration de l'ami
    friend std::ostream & operator<<(std::ostream &os, const Point &p);
};

// Définition
std::ostream & operator<<(std::ostream &os, const Point &p) {
    os << "(" << p.x << ", " << p.y << ")";
    return os;
}
```

Utilisation :

```cpp
Point p(3, 4);
std::cout << "p = " << p << std::endl;

std::ofstream out("points.txt");
out << p << std::endl;   // même opérateur
```

Points clés 🧠 :

- `os` est passé par **référence**, pour pouvoir écrire dedans *et* le renvoyer.
- On renvoie `os` pour permettre le **chainage** :
  ```cpp
  std::cout << "p = " << p << std::endl;
  // => operator<<( operator<<( operator<<(cout, "p = "), p ), endl );
  ```

---

### 4.3. `operator>>` pour la lecture

Même principe :

```cpp
class Point {
    int x, y;
public:
    Point(int x = 0, int y = 0) : x(x), y(y) {}

    friend std::istream & operator>>(std::istream &is, Point &p);
};

std::istream & operator>>(std::istream &is, Point &p) {
    // Exemple simple : lit "x y"
    is >> p.x >> p.y;
    return is;
}
```

Utilisation :

```cpp
Point p;
std::cin >> p;               // depuis la console

std::ifstream in("points.txt");
in >> p;                     // depuis un fichier
```

Remarques :

- `p` est en **référence non const** (on le modifie).
- On retourne `is` pour écrire :
  ```cpp
  std::cin >> p1 >> p2;
  // => operator>>( operator>>(cin, p1), p2 );
  ```

---

### 4.4. Pattern propre avec `print` / `read`

Pour éviter de dupliquer le code, tu peux :

```cpp
class Point {
    int x, y;

public:
    void print(std::ostream &os) const {
        os << "(" << x << ", " << y << ")";
    }

    void read(std::istream &is) {
        is >> x >> y;
    }

    friend std::ostream & operator<<(std::ostream &os, const Point &p) {
        p.print(os);
        return os;
    }

    friend std::istream & operator>>(std::istream &is, Point &p) {
        p.read(is);
        return is;
    }
};
```

Très bon **style de bibliothèque** (et très bien vu en DS).

---

## 5. `std::endl` & les manipulateurs 🧩

### 5.1. Ce qu’est vraiment `std::endl`

`std::endl` n’est **pas** un caractère, c’est un **manipulateur** :  
une fonction de type :

```cpp
std::ostream & endl(std::ostream & os);
```

Elle :

1. écrit un `'\n'` dans le flux,
2. **flushe** (vide) le buffer du flux.

Dans `<ostream>`, la STL fournit une surcharge de :

```cpp
std::ostream & operator<<(std::ostream &os,
                          std::ostream & (*manip)(std::ostream &));
```

Donc quand tu écris :

```cpp
std::cout << std::endl;
```

C’est en gros :

```cpp
std::endl(std::cout);
```

Et :

```cpp
std::cout << p << std::endl;
```

=  

```cpp
operator<<( operator<<(std::cout, p), std::endl );
```

---

### 5.2. Différence `'\n'` vs `std::endl`

- `'\n'` : juste **saut de ligne** (pas forcément flush).
- `std::endl` : saut de ligne **+ flush immédiat**.

Donc :

```cpp
out << "Hello\n";       // plus rapide dans des gros fichiers
out << "Hello" << std::endl;  // flush → utile pour un log temps réel
```

En DS, si on te pose la question :

> `std::endl` est un *manipulateur* qui écrit un `\n` et flush le flux.  
> Il existe une surcharge de `operator<<` pour les manipulateurs dans la STL.

✅ Réponse classe.

---

### 5.3. Autres manipulateurs utiles

Dans `<iomanip>` :

```cpp
#include <iomanip>

double x = 3.1415926535;

std::cout << std::fixed << std::setprecision(2) << x << std::endl;
// affiche : 3.14
```

Tous ces trucs (`std::hex`, `std::setw`, `std::setfill`, `std::boolalpha`, etc.)  
sont aussi des **manipulateurs** gérés par des surcharges d’`operator<<`.

---

## 6. Résumé des liens “surcharge / flux” 🔗

### 6.1. Pourquoi un seul `operator<<` pour tout (`cout`, `ofstream`, …) ?

Parce que :

```cpp
std::ostream & operator<<(std::ostream &os, const T &obj);
```

Accepte **n’importe quel** objet dérivé de `std::ostream` :

- `std::cout` (`std::ostream`)
- `std::ofstream` (`std::basic_ofstream<char>` → dérive de `std::basic_ostream<char>`)
- `std::ostringstream`, etc.

Donc ton code :

```cpp
Point p;
std::cout << p;
std::ofstream out("f.txt");
out << p;
```

utilise **le même** `operator<<`.

Idem pour `operator>>(std::istream&, T&)`  
→ marche pour `cin`, `ifstream`, `istringstream`, …

---

### 6.2. Chaining : pourquoi `return os;` et `return is;` ?

Pour que ça marche :

```cpp
std::cout << a << b << std::endl;
std::cin  >> x >> y;
```

Chaque appel doit **renvoyer le flux** pour que le suivant travaille dessus.

---

## 7. Mini-fiche DS 📝

À relire juste avant de rentrer en salle.

### 7.1. Patterns de base

```cpp
// Fichier
#include <fstream>
#include <iostream>

// écriture
std::ofstream out("data.txt");
if (!out) { /* erreur */ }
out << "Hello" << std::endl;

// lecture
std::ifstream in("data.txt");
if (!in) { /* erreur */ }
int x; in >> x;

// boucle de lecture
int v;
while (in >> v) {
    // ...
}

// getline
std::string line;
while (std::getline(in, line)) {
    // ...
}
```

---

### 7.2. Surcharge `<<` / `>>` pour un type `T`

```cpp
class T {
    // ...
public:
    friend std::ostream & operator<<(std::ostream &os, const T &t);
    friend std::istream & operator>>(std::istream &is, T &t);
};

std::ostream & operator<<(std::ostream &os, const T &t) {
    // afficher t dans os
    return os;
}

std::istream & operator>>(std::istream &is, T &t) {
    // lire t depuis is
    return is;
}
```

Idées à pouvoir **expliquer en phrase** :

- `operator<<` / `operator>>` sont des **fonctions libres** (souvent `friend`)  
  parce que le premier paramètre doit être un `std::ostream&` / `std::istream&`.
- Elles retournent une **référence au flux** pour permettre le **chainage**.
- Le même `operator<<` pour `T` marche avec `cout`, `ofstream`, etc.,  
  car `ofstream` **hérite** de `ostream`.

---

### 7.3. `std::endl` & cie

- `std::endl` est un **manipulateur de flux** :
  - type : `std::ostream& (*)(std::ostream&)`,
  - écrit un `'\n'` et fait un `flush`.
- Il existe une surcharge de `operator<<` pour ce type de fonction.
- Différence avec `'\n'` :
  - `'\n'` : juste nouvelle ligne,
  - `std::endl` : nouvelle ligne + flush.

---

[...retorn en rèire](../menu.md)