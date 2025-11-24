# 📚 Entrées / Sorties : `iostream`

[...retorn en rèire](./flus.md)

---

Les flux C++ sont construits en **couches** :

* des **buffers** (tampons) bas niveau
* des **flux** (input / output)
* des **adaptateurs** (`std::cout`, `std::cin`, `std::stringstream`, etc.)

---

## 8.1 🏛️ Hiérarchie des flux

### 8.1.1 `ios_base`

Classe **de base la plus basse** dans la hiérarchie.

* Gère :

  * les **flags** (formatage),
  * les **modes d’ouverture** (`std::ios::in`, `std::ios::out`, `std::ios::binary`, …),
  * les **fmtflags** (`std::ios::hex`, `std::ios::showbase`, …).
* Ne dépend pas du type de caractère.

---

### 8.1.2 `basic_ios`

```cpp
template<class CharT, class Traits = std::char_traits<CharT>>
class basic_ios : public ios_base { ... };
```

* Hérite de `ios_base`.
* Ajoute :

  * le **pointeur vers le buffer** (`rdbuf()`),
  * la gestion de l’**état du flux** (good/fail/eof/bad),
  * le caractère de remplissage (`fill()`), etc.

`std::istream` et `std::ostream` héritent de `std::basic_ios<char>`.

---

### 8.1.3 `basic_ostream`, `basic_istream`, `basic_iostream`

* `basic_ostream<CharT>` : flux de sortie (écriture).

  * Typedef : `using ostream = basic_ostream<char>;`
* `basic_istream<CharT>` : flux d’entrée (lecture).

  * Typedef : `using istream = basic_istream<char>;`
* `basic_iostream<CharT>` : combine entrée + sortie.

Par exemple :

```cpp
std::ostream& os = std::cout;   // basic_ostream<char>
std::istream& is = std::cin;    // basic_istream<char>
```

---

### 8.1.4 Buffers : `streambuf`, `filebuf`, `stringbuf`

Les flux ne stockent pas eux-mêmes les données :
ils utilisent un **tampon** (`std::basic_streambuf`) :

* `std::streambuf` : tampon générique
* `std::filebuf`   : tampon pour fichiers
* `std::stringbuf` : tampon pour chaînes en mémoire

Exemple :

```cpp
std::ofstream f("data.txt");     // ofstream possède un filebuf
std::streambuf* buf = f.rdbuf(); // récupérer le buffer brut
```

---

## 8.2 🚦 État d’un flux

Chaque flux garde un **état interne** sous forme de bits.

### 8.2.1 Bits principaux

* `eofbit` : fin de fichier atteinte
* `failbit` : échec d’une opération de format (ex : lire un int dans du texte non numérique)
* `badbit` : erreur grave (I/O, corruption du flux)
* `goodbit` : aucun des trois bits précédents

---

### 8.2.2 Fonctions de test

* `good()` : vrai si **aucune erreur** → tous les bits à zéro
* `fail()` : vrai si `failbit` ou `badbit` est positionné
* `eof()` : vrai si `eofbit` est positionné
* `bad()` : vrai si `badbit` est positionné

Exemple classique :

```cpp
std::ifstream f("data.txt");
int x;

while (f >> x) {   // tant que l'extraction réussit
    // ...
}

if (f.eof()) {
    std::cout << "Fin de fichier.\n";
} else if (f.fail()) {
    std::cout << "Erreur de format.\n";
}
```

💡 `while (f >> x)` est équivalent à `while (! (f >> x).fail())`.

---

## 8.3 🎨 Formatage (`fmtflags`)

Les flux C++ permettent de contrôler finement la **mise en forme**.

### 8.3.1 Indicateurs courants

* `std::hex` : base 16
* `std::dec` : base 10 (par défaut)
* `std::oct` : base 8
* `std::showbase` : affiche le préfixe (ex : `0x` pour hex)
* `std::showpos` : signe + pour les positifs
* `std::fixed` / `std::scientific` : format flottant
* `std::setprecision(n)` : nombre de chiffres significatifs / décimales (selon mode)

Exemple :

```cpp
int n = 255;
std::cout << std::dec << n << "\n";   // 255
std::cout << std::hex << n << "\n";   // ff
std::cout << std::showbase << std::hex << n << "\n"; // 0xff
```

---

### 8.3.2 Manipulation via `setf()`, `unsetf()`, `flags()`

* `setf(flags)` : ajoute/modifie certains flags.
* `unsetf(flags)` : retire certains flags.
* `flags()` :

  * sans argument : retourne les flags courants,
  * avec argument : remplace complètement les flags.

Exemple :

```cpp
std::cout.setf(std::ios::hex, std::ios::basefield); // base = hex
std::cout.setf(std::ios::showbase);                 // garde les autres flags
std::cout << 255 << "\n"; // → 0xff
```

💡 L’argument optionnel de `setf()` (ex. `std::ios::basefield`) indique **le groupe** de flags à remplacer (base numérique, alignement, etc.).

---

### 8.3.3 Manipulateurs standard : `std::hex`, `std::setw`, `std::setprecision`, …

Les **manipulateurs** sont des petits objets/fonctions qu’on injecte dans les flux :

```cpp
#include <iomanip>

double x = 3.1415926535;

std::cout << std::fixed << std::setprecision(2) << x << "\n"; // 3.14
std::cout << std::setw(10) << 42 << "\n";                     // "        42"
```

* `std::setw(n)` : largeur minimale du champ
* `std::setfill('0')` : caractère de remplissage
* `std::left`, `std::right`, `std::internal` : alignement

---

## 8.4 🧩 Manipulateurs personnalisés

Tu peux créer tes **propres manipulateurs**, pour enrichir les flux (couleurs, reset de format, etc.).

### 8.4.1 Manipulateur simple (sans paramètre)

Signature :

```cpp
std::ostream& manip(std::ostream& os);
```

Exemple : remettre `std::dec` + enlever `showbase` :

```cpp
std::ostream& reset_format(std::ostream& os) {
    os << std::dec;
    os.unsetf(std::ios::showbase);
    return os;
}

std::cout << std::hex << std::showbase << 255 << reset_format << " " << 42;
// → "0xff 42"
```

---

### 8.4.2 Manipulateurs paramétrés

Astuce : on renvoie un petit **objet helper** avec un `operator<<` surchargé.

#### 🎨 Exemple : couleur ANSI

```cpp
enum class CouleurTTY { Rouge, Vert, Bleu, Reset };

struct CouleurManip {
    CouleurTTY c;
};

std::ostream& operator<<(std::ostream& os, CouleurManip m) {
    switch (m.c) {
        case CouleurTTY::Rouge: os << "\033[31m"; break;
        case CouleurTTY::Vert:  os << "\033[32m"; break;
        case CouleurTTY::Bleu:  os << "\033[34m"; break;
        case CouleurTTY::Reset: os << "\033[0m";  break;
    }
    return os;
}

inline CouleurManip couleur(CouleurTTY c) { return {c}; }

// Utilisation :
std::cout << couleur(CouleurTTY::Rouge) << "Texte rouge"
          << couleur(CouleurTTY::Reset) << "\n";
```

---

#### 🧹 Exemple : ignorer une ligne (`ignoreLigne`)

```cpp
struct IgnoreLigneManip {};

std::istream& operator>>(std::istream& is, IgnoreLigneManip) {
    is.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
    return is;
}

inline IgnoreLigneManip ignoreLigne() { return {}; }

// Utilisation :
int x;
std::cin >> x >> ignoreLigne();
```

---

## 8.5 📤 Méthodes de `ostream`

### 8.5.1 `write()`

Écriture **binaire / brute** de données (pas formatée) :

```cpp
const char buffer[] = "Hello";
std::cout.write(buffer, 5);
```

⚠️ Ne rajoute pas de `\0` ni de `\n`.

---

### 8.5.2 `tellp()` et `seekp()`

* `tellp()` : donne la **position actuelle** du pointeur d’écriture (put pointer).
* `seekp(pos)` : déplace le pointeur d’écriture.

Exemple :

```cpp
std::ofstream f("data.bin", std::ios::binary);
f.write("AAAA", 4);
auto pos = f.tellp();      // pos = 4
f.seekp(0);                // retour au début
f.write("BBBB", 4);        // remplace les 4 premiers octets
```

---

## 8.6 📥 Méthodes de `istream`

### 8.6.1 `read()` : lecture brute

```cpp
char buffer[100];
file.read(buffer, 100);   // lit 100 octets (ou moins si EOF)
auto lus = file.gcount(); // nombre réel de caractères lus
```

---

### 8.6.2 `getline()`

Lecture d’une **ligne entière** dans une `std::string` :

```cpp
std::string ligne;
std::getline(std::cin, ligne);
```

⚠️ À ne pas confondre avec `istream::getline(char*, size_t)` qui lit dans un buffer C.

---

### 8.6.3 `tellg()` et `seekg()`

* `tellg()` : position du pointeur de lecture (get pointer)
* `seekg(pos)` : déplacer le pointeur de lecture

```cpp
std::ifstream f("data.txt");
auto pos = f.tellg();
f.seekg(0, std::ios::end);
auto fin = f.tellg();
f.seekg(pos);
```

---

### 8.6.4 Surcharges de `operator>>`

Les opérateurs `>>` sont surchargés pour :

* Types arithmétiques (`int`, `double`, …)
* `char`, `char*`
* `std::string`
* `std::streambuf*`
* Les **manipulateurs** (`std::ws`, par ex.)

Exemple :

```cpp
int x;
double d;
std::string s;

std::cin >> x >> d >> s;   // formaté, ignore spaces/tabs/newlines
```

---

# 9. 📁 Fichiers : `fstream`

Pour les fichiers, on utilise :

* `std::ifstream` : input file stream (lecture)
* `std::ofstream` : output file stream (écriture)
* `std::fstream`  : lecture + écriture

---

## 9.1 Ouverture texte vs binaire

```cpp
std::ifstream fin("data.txt");                           // texte
std::ifstream finb("data.bin", std::ios::binary);        // binaire

std::ofstream fout("out.txt");
std::ofstream foutb("out.bin", std::ios::binary);
```

Différences principales :

* **Mode texte** : normalisation éventuelle des fins de ligne (`\r\n` ↔ `\n`)
* **Mode binaire** : aucun traitement, les octets sont lus/écrits tels quels

---

## 9.2 📎 Copier un fichier en une seule lecture/écriture

### Version “buffer” brut via `rdbuf()` :

```cpp
std::ifstream src("in.bin", std::ios::binary);
std::ofstream dst("out.bin", std::ios::binary);

dst << src.rdbuf();
```

👉 Très efficace, simple, ne traite pas les données.

---

## 9.3 Positionnement dans un fichier (`seekg`, `seekp`)

On peut naviguer dans le fichier comme dans un tableau de bytes :

```cpp
std::ifstream f("data.bin", std::ios::binary);
f.seekg(0, std::ios::end);
auto taille = f.tellg();           // taille du fichier
f.seekg(0, std::ios::beg);         // retour au début
```

Avec `std::ofstream` / `std::fstream` :

* `seekg()` : pointeur de lecture
* `seekp()` : pointeur d’écriture

---

# 10. 🔤 String Streams : `sstream`

Les **string streams** permettent d’utiliser l’API des flux (`<<`, `>>`, etc.)
mais avec un **tampon en mémoire**, représenté par une `std::string`.

* `std::ostringstream` : sortie → construit une string
* `std::istringstream` : entrée → lit à partir d’une string
* `std::stringstream`  : les deux

---

## 10.1 ✏️ `ostringstream`

### Construction de chaînes formatées

```cpp
#include <sstream>

int id = 42;
std::string nom = "Jean";

std::ostringstream oss;
oss << "ID=" << id << ", Nom=" << nom;

std::string result = oss.str();  // récupérer la chaîne
```

✅ Avantages :

* même syntaxe que `std::cout`
* pas de risque de dépassement de buffer
* pratique pour logs, messages d’erreur, etc.

---

## 10.2 🔍 `istringstream`

### Analyse de chaînes (tokenisation simple)

```cpp
std::string ligne = "12 3.5 hello";
std::istringstream iss(ligne);

int i;
double d;
std::string s;

iss >> i >> d >> s;  // i=12, d=3.5, s="hello"
```

Utilisation typique :

* parser des lignes lues par `std::getline`
* découper des champs séparés par espaces/tabulations

---

## 10.3 🔁 `stringstream`

Combine lecture + écriture.

```cpp
std::stringstream ss;
ss << 10 << " " << 3.14 << " " << "ok";

int i;
double d;
std::string word;

ss >> i >> d >> word;  // i=10, d=3.14, word="ok"
```

Le buffer interne est manipulable via :

* `str()` : obtenir ou remplacer tout le contenu
* `rdbuf()` : accéder au buffer bas niveau

---

### ⚠️ Attention à l’état du flux

Après avoir lu jusqu’au bout, `eofbit` sera positionné :

```cpp
std::stringstream ss("1 2 3");
int x;
while (ss >> x) {
    // ...
}
// ici eofbit est vrai
ss.clear();       // reset des flags
ss.str("4 5 6");  // nouveau contenu
```

---

# 🎯 Récap global (8–10)

* Les flux C++ sont construits sur :

  * des **buffers** (`streambuf`, `filebuf`, `stringbuf`)
  * des **flux génériques** (`basic_istream`, `basic_ostream`)
  * des **typdefs** prêts à l’emploi (`std::cin`, `std::cout`, `fstream`, `stringstream`, …)
* L’**état du flux** (good/fail/eof/bad) est **central** pour écrire du code robuste.
* Le **formatage** se fait via :

  * flags (`setf`, `flags`, `unsetf`)
  * manipulateurs (`std::hex`, `std::setw`, `std::setprecision`, …)
  * **manipulateurs personnalisés** (couleurs, ignoreLigne, etc.)
* Les fichiers (`fstream`) et `stringstream` utilisent **la même API**, ce qui rend le code réutilisable :

  * change juste le type de flux → même code, mêmes opérateurs.

---

[...retorn en rèire](./flus.md)