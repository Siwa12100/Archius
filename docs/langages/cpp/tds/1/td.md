```markdown
# TD C++ – Surcharge, valeur, pointeur et référence  

### [...retorn en rèire](../../menu.md)

### [Sujet](./td1-cpp.pdf)

---

## 0. Rappel du contexte et du code

On travaille avec une classe `Point` qui surcharge :

- l’opérateur d’incrémentation préfixe `++p` et postfixe `p++`  
- l’opérateur d’affectation `=`  
- l’opérateur d’insertion dans un flux `<<` (pour `cout << p`)  

On a aussi trois méthodes :

- `Point M1()` : retour **par valeur**  
- `Point * M2()` : retour **par pointeur**  
- `Point & M3()` : retour **par référence**

Et on utilise parfois la constante de compilation `MAP` pour afficher les constructeurs / destructeurs / etc.

---

### 0.1. Rappel de la classe `Point`

```cpp
class Point
{
public :
    friend ostream & operator << ( ostream & out, const Point & p );

    Point M1 ( );
    Point * M2 ( );
    Point & M3 ( );

    Point & operator ++ ( void );     // ++p (préfixe)
    Point operator ++ ( int inutile );// p++ (postfixe)

    Point & operator = ( const Point & p );

    Point ( const Point & p );        // constructeur de copie
    Point ( int abs = 0, int ord = 0 );
    ~Point ( );

private :
    int x;
    int y;
};
```

---

### 0.2. Rappel des implémentations importantes

```cpp
Point Point::M1 ()
{
#ifdef MAP
    cout << ">> M1 : retour par valeur d'un Point" << endl;
#endif
    (*this)++;      // POSTFIXE : p++ sur l'objet courant
    return *this;   // retour par valeur (copie)
}

Point * Point::M2 ()
{
#ifdef MAP
    cout << ">> M2 : retour par valeur d'un pointeur sur Point" << endl;
#endif
    ++(*this);      // PREFIXE : ++p sur l'objet courant
    return this;    // pointeur sur l'objet courant
}

Point & Point::M3 ()
{
#ifdef MAP
    cout << ">> M3 : retour par référence d'un Point" << endl;
#endif
    (*this)++;      // POSTFIXE
    return *this;   // retour par référence
}
```

Les surcharges de `++` :

```cpp
// ++p (préfixe) : incrémente et renvoie une RÉFÉRENCE sur l'objet modifié
Point & Point::operator ++ ( void )
{
    ++x;
    ++y;
    return *this;
}

// p++ (postfixe) : renvoie une COPIE de l'ancienne valeur, puis incrémente l'objet
Point Point::operator ++ ( int inutile )
{
    Point tmp ( x, y ); // copie de l'état AVANT incrément
    x++;
    y++;
    return tmp;         // renvoie l'ancienne valeur (par copie)
}
```

L’opérateur `<<` :

```cpp
ostream & operator << ( ostream & out, const Point & p )
{
    out << "( " << p.x << "," << p.y << " )";
    return out;
}
```

---

## 1. Notations fonctionnelles (Exercice 1, question 1)

On te demande de réécrire des expressions C++ avec opérateurs et appels implicites en **notation fonctionnelle explicite**.

Rappel :  
Pour un opérateur surchargé :
- `a = b;` peut devenir `a.operator=(b);` ou `operator=(a,b);`
- `p++;` devient `p.operator++(0);`
- `++p;` devient `p.operator++();`
- `cout << p;` devient `operator<<(cout, p);` (car `operator<<` est ici une fonction amie non-membre).

On va maintenant traiter chaque ligne demandée.

---

### 1.1. `p2 = p1.M1();` (ligne 12 de `main.cpp`)

Code :

```cpp
p2 = p1.M1();
```

Décomposition conceptuelle :

1. Appel de méthode :  
   ```cpp
   Point tmp = p1.M1();
   ```
   équivalent fonctionnel :
   ```cpp
   Point tmp = Point::M1( p1 );
   ```
   (on voit `p1` comme l’objet passé en paramètre caché `this`).

2. Affectation à `p2` avec l’opérateur surchargé `operator=` :  
   ```cpp
   p2.operator=( tmp );
   ```
   ou, en notation non-membre :
   ```cpp
   operator=( p2, tmp );
   ```

**Notation fonctionnelle globale** :

Directement sur l’expression :

```cpp
p2.operator=( p1.M1() );
```

ou encore :

```cpp
operator=( p2, Point::M1( p1 ) );
```

---

#### 1.1.1. Effet sur les valeurs (très important pour le DS)

Analysons ce qui se passe dans `M1()` :

```cpp
Point Point::M1()
{
    (*this)++;   // appel au POSTFIXE
    return *this;
}
```

- Avant `M1()` : disons que `p1 = (1,2)`.
- `(*this)++;` appelle le **postfixe** :
  - À l’intérieur de `operator++(int)` :
    - `tmp` est créé avec `(1,2)`
    - `x++` et `y++` : l’objet `p1` devient `(2,3)`
    - on renvoie `tmp` (= `(1,2)`) mais la valeur renvoyée est IGNORÉE dans `M1`, car `(*this)++;` est une instruction seule.
- Ensuite `return *this;` : on renvoie **par valeur** une copie de `*this` (donc une copie de `(2,3)`).

Donc :
- `p1` est modifié de `(1,2)` à `(2,3)`.
- `M1()` renvoie la valeur `(2,3)`.
- L’affectation `p2 = ...` copie `(2,3)` dans `p2`.

Après la ligne 12 :
- `p1 = (2,3)`
- `p2 = (2,3)`

---

### 1.2. `p3 = p2.M2();` (ligne 16 de `main.cpp`)

Code :

```cpp
p3 = p2.M2();
```

Décomposition :

1. Appel de méthode :

```cpp
Point * tmpP = p2.M2();
```

notation fonctionnelle :

```cpp
Point * tmpP = Point::M2( p2 );
```

2. Affectation à un pointeur `p3` : ici, c’est une **affectation de pointeur standard**, non surchargée (type `Point*`). On peut éventuellement la noter `p3 = tmpP;` (on évite de parler d’un `operator=` surchargé, ce n’est pas le cas ici).

**Notation fonctionnelle compacte** :

```cpp
p3 = Point::M2( p2 );
```

ou (en insistant sur l’objet récepteur) :

```cpp
p3 = p2.M2(); // déjà en notation "méthode"
```

Mais si on veut absolument tout mettre en forme « fonction » :

```cpp
p3 = Point::M2( p2 );
```

---

#### 1.2.1. Effet sur les valeurs

Rappel de `M2()` :

```cpp
Point * Point::M2 ()
{
    ++(*this);   // PREFIXE
    return this;
}
```

- Avant appel : supposons `p2 = (2,3)` (comme vu après `M1`).
- `++(*this);` appelle le préfixe :
  - `operator++()` :
    - `++x; ++y;` : `p2` devient `(3,4)`
    - retourne une référence sur `*this` (ignorée ici).
- `return this;` : renvoie un **pointeur** vers l’objet courant, donc l’adresse de `p2`.

Après la ligne 16 :
- `p2 = (3,4)`
- `p3` pointe sur `p2` : `p3 == &p2`
- donc `*p3` est un alias de `p2` et vaut `(3,4)`.

---

### 1.3. `(*this)++;` (ligne 11 de `Point.cpp` dans `M1()`)

Code :

```cpp
(*this)++;
```

Ici, c’est l’opérateur **postfixe** `++` sur l’objet `*this`.

**Notation fonctionnelle membre** :

```cpp
(*this).operator++( 0 );
```

(Le paramètre `int inutile` vaut typiquement `0` et sert juste à distinguer postfixe de préfixe.)

**Notation fonctionnelle non-membre** (plus théorique) :

```cpp
operator++( *this, 0 );
```

---

### 1.4. `++(*this);` (ligne 20 de `Point.cpp` dans `M2()`)

Code :

```cpp
++(*this);
```

Ici, c’est l’opérateur **préfixe** `++`.

**Notation fonctionnelle membre** :

```cpp
(*this).operator++();
```

**Notation fonctionnelle non-membre** :

```cpp
operator++( *this );
```

Différence importante :

- `(*this)++;` appelle `Point Point::operator++(int);` (postfixe)
- `++(*this);` appelle `Point & Point::operator++();` (préfixe)

---

### 1.5. `++p++;` (expression piège !)

Code :

```cpp
++p++;
```

C’est très piégeux. Il faut savoir :

- la priorité : `p++` (postfixe) a une priorité plus élevée que `++` (préfixe)
- donc c’est équivalent à :

```cpp
++(p++);
```

Décomposition :

1. `p++` est le postfixe :  
   → `p.operator++(0)`  
   → renvoie une **copie** de l’ancienne valeur de `p` (un temporaire).

2. `++` appliqué au résultat renvoyé (un temporaire)  
   → préfixe `operator++()` appliqué au temporaire.

Donc, en notation fonctionnelle :

```cpp
operator++( p.operator++(0) );
```

ou en version « objet » :

```cpp
p.operator++(0).operator++();
```

Remarque pédagogique (très importante pour un DS) :

- `p.operator++(0)` modifie **p** (via le postfixe) et crée un **temporaire** (copie de l’ancienne valeur).
- `operator++( … )` (préfixe) modifie ce **temporaire**, pas `p`.
- Résultat :
  - `p` est incrémenté **une seule fois** (par le postfixe).
  - le préfixe modifie seulement l’objet temporaire, qui sera détruit.

Donc `++p++;` est légal mais complètement tordu, et ne fait pas « deux incréments sur p » comme on pourrait naïvement le croire.

---

### 1.6. `cout << "p1 = " << p1 << endl;` (ligne 13 de `main.cpp`)

Code :

```cpp
cout << "p1 = " << p1 << endl;
```

L’opérateur `<<` est **associatif à gauche**.  
Donc ceci est équivalent à :

```cpp
((cout << "p1 = ") << p1) << endl;
```

Maintenant, on passe tout en notation fonctionnelle.

1. `cout << "p1 = "` :
   ```cpp
   operator<<( cout, "p1 = " );
   ```
   (surcharge standard de la STL pour `ostream` et `const char*`)

2. On prend le résultat (un `ostream&`), et on fait `<< p1` :

   ```cpp
   operator<<( operator<<( cout, "p1 = " ), p1 );
   ```

   Ici, on utilise notre surcharge amie :

   ```cpp
   ostream & operator<<( ostream & out, const Point & p );
   ```

3. Enfin, `<< endl` :

   ```cpp
   operator<<( operator<<( operator<<( cout, "p1 = " ), p1 ), endl );
   ```

   où `endl` est un **manipulateur de flux**  
   (en fait un pointeur de fonction de type `ostream& (*)(ostream&)`).

**Notation fonctionnelle finale :**

```cpp
operator<<(
    operator<<(
        operator<<( cout, "p1 = " ),
        p1
    ),
    endl
);
```

---

## 2. Notation fonctionnelle pour `cout << p++ << endl;` (question 2)

On considère le deuxième `main` :

```cpp
int main ( )
{
    Point p ( 1, 2 );

    cout << p++ << endl;
    cout << p << endl;

    return 0;
}
```

On se focalise sur la ligne 10 :

```cpp
cout << p++ << endl;
```

---

### 2.1. Décomposition syntaxique

1. `p++` : postfixe → `p.operator++(0)`.

2. `cout << p++` :  
   devient  
   ```cpp
   operator<<( cout, p.operator++(0) );
   ```
   et cette surcharge est celle-ci :

   ```cpp
   ostream & operator<<( ostream & out, const Point & p );
   ```

3. Ensuite, `(cout << p++) << endl` en considérant le retour de `operator<<` :

   ```cpp
   operator<<( operator<<( cout, p.operator++(0) ), endl );
   ```

**Notation fonctionnelle demandée :**

```cpp
operator<<(
    operator<<( cout, p.operator++(0) ),
    endl
);
```

---

### 2.2. Effet sur les valeurs

Au moment de la première ligne utilisant `p++` :

- Avant : `p = (1,2)`.
- `p++` (postfixe) :
  - crée une copie temporaire `(1,2)`
  - modifie `p` → `(2,3)`
  - renvoie la copie `(1,2)` (c’est cette valeur qui est affichée).
- `cout << p++ << endl;`
  - affiche `( 1,2 )`
  - après l’instruction, `p` vaut `(2,3)`.

La ligne suivante :

```cpp
cout << p << endl;
```

affiche alors `( 2,3 )`.

On détaillera plus loin la trace exacte avec `MAP`.

---

## 3. Dessin mémoire et simulation pas-à-pas (question 3)

On considère le **main simplifié** :

```cpp
int main ( )
{
    Point p ( 1, 2 );

    cout << p++ << endl;
    cout << p << endl;

    return 0;
}
```

On te demande :  
> Faire le dessin mémoire pour la simulation de l’exécution de cette fonction simplifiée du main.  
> On arrêtera la simulation à la ligne 11, en excluant l’exécution de cette ligne.

Donc on simule :

- la ligne 8 : `Point p(1,2);`
- la ligne 10 : `cout << p++ << endl;`
- **on ne simule pas la ligne 11** (mais j’en parlerai pour la compréhension globale).

On va raisonner en termes de **pile (stack)** et **objets temporaires**.

---

### 3.1. État avant l’exécution de main

- La pile est vide (en ce qui nous concerne).
- Des objets globaux comme `cout` existent déjà en mémoire (section globale/statique), mais on peut les considérer comme « déjà là ».

---

### 3.2. Entrée dans `main`

Activation de la fonction `main` :

Pile (vue simplifiée) :

| Pile (haut vers bas) |
|-----------------------|
| variables locales de `main` (bientôt `p`) |
| ... registre de retour, etc. |

À ce stade, `p` **n’existe pas encore**, on n’a pas exécuté la ligne 8.

---

### 3.3. Ligne 8 : `Point p(1,2);`

Cette ligne :

- alloue l’objet `p` sur la pile (objet de type `Point`).
- appelle le constructeur `Point::Point(int abs, int ord)` avec `abs=1`, `ord=2`.

Pile après la ligne 8 :

| Nom | Type  | Localisation | Contenu          |
|-----|-------|--------------|------------------|
| p   | Point | pile (`main`) | `x = 1`, `y = 2` |

Si `MAP` est défini, le constructeur affiche :

```text
>> Constructeur de la classe <Point>
```

---

### 3.4. Ligne 10 : `cout << p++ << endl;`

On s’arrête juste après l’exécution complète de cette ligne.

Décomposons :

#### 3.4.1. Évaluation de `p++`

On appelle le **postfixe** :

```cpp
Point Point::operator++ (int inutile)
{
    Point tmp ( x, y );
    x++;
    y++;
    return tmp;
}
```

À l’entrée de `operator++(int)` :

Pile (simplifiée) :

| Contexte     | Nom      | Type   | Valeur          |
|--------------|----------|--------|-----------------|
| `operator++` | this     | Point* | &p              |
|              | inutile  | int    | indéterminé (argument) |
|              | tmp      | Point  | (pas encore construit) |
| `main`       | p        | Point  | `x = 1`, `y = 2` |

Étapes internes :

1. Construction de `tmp` :

   ```cpp
   Point tmp(x, y);
   ```

   - Appel du constructeur `Point::Point(int abs, int ord)` avec `abs = 1`, `ord = 2`.
   - `tmp` devient `(1,2)`.

   Pile dans `operator++` :

   | Nom      | Type  | Valeur          |
   |----------|-------|-----------------|
   | this     | Point* | &p             |
   | inutile  | int   | ...             |
   | tmp      | Point | `x = 1, y = 2`  |
   | p (dans main) | Point | `x = 1, y = 2` |

2. Incrémentation de l’objet pointé par `this` (donc `p`) :

   ```cpp
   x++;
   y++;
   ```

   Ici, `x` et `y` sont les attributs de `*this`, donc de `p`.  
   Après ces deux lignes :

   - `p` (dans `main`) devient `(2,3)`.
   - `tmp` reste `(1,2)`.

3. Retour de `tmp` par valeur :

   ```cpp
   return tmp;
   ```

   Conceptuellement (sans optimisation) :
   - on construit un **objet temporaire de retour**, disons `T_ret`, sur la pile de `main`, via le constructeur de copie :

     ```cpp
     Point::Point(const Point & p); // constructeur de copie
     ```

   - `T_ret` reçoit les valeurs de `tmp` : `(1,2)`.
   - en fin de fonction, `tmp` est détruit.

   Juste après le retour dans `main` :

   Pile (hors implémentation détaillée) :

   | Nom    | Type   | Localisation | Valeur          |
   |--------|--------|--------------|-----------------|
   | T_ret  | Point  | temporaire   | `x = 1, y = 2`  |
   | p      | Point  | pile (main)  | `x = 2, y = 3`  |

   Temporaire `T_ret` correspond à la valeur renvoyée par `p++` (l’ancienne valeur de `p`).

#### 3.4.2. Appel de `operator<<(cout, T_ret)`

On exécute maintenant :

```cpp
cout << p++; // c’est-à-dire cout << T_ret;
```

Ce qui appelle :

```cpp
operator<<( cout, T_ret );
```

À l’intérieur de cette fonction (notre surcharge amie) :

```cpp
ostream & operator << ( ostream & out, const Point & p )
{
    out << "( " << p.x << "," << p.y << " )";
    return out;
}
```

- Le paramètre `p` de cette fonction est un alias (référence const) vers le temporaire `T_ret`.

- On exécute :
  - `out << "( ";`
  - `out << p.x;`  → `1`
  - `out << ",";`
  - `out << p.y;`  → `2`
  - `out << " )";`

Ce qui affiche :

```text
( 1,2 )
```

(sans fin de ligne, car `endl` n’a pas encore été envoyé).

La fonction retourne ensuite une référence vers `cout`.

Le temporaire `T_ret` est encore vivant jusqu’à la fin de toute l’expression `cout << p++ << endl;`.

#### 3.4.3. Appel de `operator<<( ..., endl )`

On termine avec :

```cpp
(... résultat de cout << p++ ...) << endl;
```

Ce qui appelle :

```cpp
operator<<( cout, endl );
```

Cet `operator<<` de la STL :

- appelle la fonction `endl(cout)`, ce qui :
  - écrit un caractère de fin de ligne (`'\n'`)
  - vide le buffer (`flush`).

À l’écran, on a donc terminé la **première ligne complète** :

```text
( 1,2 )
```

#### 3.4.4. Destruction du temporaire de retour de `p++`

Une fois l’expression complète `cout << p++ << endl;` terminée :

- Le temporaire `T_ret` (la valeur renvoyée par `p++`) est détruit.
- Si `MAP` est défini, cela déclenche :

```text
>> Destructeur de la classe <Point>
```

État mémoire juste après la ligne 10, avant la ligne 11 (comme demandé) :

| Nom | Type  | Localisation | Valeur          |
|-----|-------|--------------|-----------------|
| p   | Point | pile (main)  | `x = 2, y = 3`  |

Les temporaires (`tmp` dans `operator++` et `T_ret`) ont été détruits.

---

### 3.5. Remarque sur la ligne 11 (non demandée mais utile)

La ligne 11 :

```cpp
cout << p << endl;
```

- ne crée aucun nouvel objet `Point`,
- appelle simplement `operator<<(cout, p);`,
- affiche `( 2,3 )` puis un retour à la ligne via `endl`.

À la fin de `main`, l’objet `p` est détruit, et si `MAP` est défini, on voit encore un message de destructeur.

---

## 4. Trace exacte à l’écran avec `MAP` défini (question 4)

On reprend le `main` simplifié :

```cpp
int main ( )
{
    Point p ( 1, 2 );

    cout << p++ << endl;
    cout << p << endl;

    return 0;
}
```

On suppose que :

```cpp
#define MAP
```

est défini à la compilation, donc toutes les zones `#ifdef MAP ... #endif` sont actives.

Je vais ignorer les détails internes des `operator<<` de la STL et ne considérer que :

- les cout présents dans la classe `Point` (constructeur, constructeur de copie, destructeur, etc.),
- l’affichage de `( 1,2 )` et `( 2,3 )`.

---

### 4.1. Hypothèse pédagogique : pas d’optimisation de copie (pas d’elision)

Dans un TD/examen, on suppose généralement :

- Que **le constructeur de copie est appelé** quand on renvoie un objet par valeur (`return tmp;`), même si en pratique le compilateur peut optimiser (RVO / copy elision).

C’est ce qu’on va supposer ici, pour bien voir la vie des objets.

---

### 4.2. Chronologie détaillée

#### Étape 1 : construction de `p` (ligne 8)

```cpp
Point p(1,2);
```

Appel du constructeur `Point(int,int)` :

```text
>> Constructeur de la classe <Point>
```

---

#### Étape 2 : ligne 10 : `cout << p++ << endl;`

1. Appel de `p++` ⇒ `operator++(int)` :

   - Construction de `tmp` dans `operator++(int)` :

     ```text
     >> Constructeur de la classe <Point>
     ```

   - Retour par valeur : `return tmp;`  
     ⇒ création de l’objet de retour via le **constructeur de copie** :

     ```text
     >> Constructeur de copie de la classe <Point>
     ```

   - Fin de `operator++(int)` ⇒ destruction de `tmp` :

     ```text
     >> Destructeur de la classe <Point>
     ```

   À ce moment-là :
   - `p` a été modifié et vaut `(2,3)`,
   - la valeur renvoyée par `p++` est un temporaire contenant `(1,2)`.

2. `cout << p++` ⇒ `operator<<(cout, temporaire)` affiche :

   ```text
   ( 1,2 )
   ```

   (puisqu’on enchaîne avec `<< endl`, il y a un saut de ligne juste après.)

3. Fin de l’expression complète `cout << p++ << endl;` ⇒ destruction du temporaire renvoyé par `p++` :

   ```text
   >> Destructeur de la classe <Point>
   ```

---

#### Étape 3 : ligne 11 : `cout << p << endl;`

À ce moment :

- `p` vaut `(2,3)`.

L’expression :

```cpp
cout << p << endl;
```

appelle :

- `operator<<(cout, p);` → affiche :

  ```text
  ( 2,3 )
  ```

- puis `operator<<(cout, endl);` → saut de ligne.

Pas de nouveau `Point` créé ni détruit dans cette étape (en dehors de `p` lui-même qui existe déjà).

---

#### Étape 4 : fin de `main` ⇒ destruction de `p`

À la sortie de `main`, l’objet local `p` est détruit :

```text
>> Destructeur de la classe <Point>
```

---

### 4.3. Trace complète (dans l’ordre)

En réunissant tout, la **trace exacte attendue** (dans ce modèle pédagogique sans optimisation de copie) est :

```text
>> Constructeur de la classe <Point>               // p dans main
>> Constructeur de la classe <Point>               // tmp dans operator++(int)
>> Constructeur de copie de la classe <Point>      // objet de retour de p++
>> Destructeur de la classe <Point>                // destruction de tmp (operator++(int))
( 1,2 )                                            // affichage du temporaire p++ 
>> Destructeur de la classe <Point>                // destruction du temporaire de retour
( 2,3 )                                            // affichage de p dans cout << p << endl;
>> Destructeur de la classe <Point>                // destruction de p en fin de main
```

Remarques :

- Chaque message issu de `MAP` se termine par `endl`, donc occupe sa propre ligne.
- Les lignes `( 1,2 )` et `( 2,3 )` sont chacune suivies d’un retour à la ligne (à cause de `endl` dans `main`).

---

## 5. Récapitulatif pédagogique des notions clés (pour bien tenir au DS)

Voici les points essentiels que ton prof peut essayer d’exploiter pour te piéger.

---

### 5.1. Différences valeur / pointeur / référence

- **Retour par valeur** (`Point M1()`):
  - On renvoie une **copie** de l’objet.
  - Modifier l’objet receveur n’affecte pas l’original.
  - Problème possible : performance (copie) mais sémantique simple.

- **Retour par pointeur** (`Point * M2()`):
  - On renvoie une **adresse**.
  - On peut accéder à l’objet via `(*ptr)` ou `ptr->`.
  - Risque : **pointeur pendu** si l’objet n’existe plus (objet local détruit, etc.).
  - Ici, M2 renvoie `this`, donc pointeur vers un objet vivant (membre de `main`).

- **Retour par référence** (`Point & M3()`):
  - On renvoie un **alias** vers l’objet.
  - Modification via la référence == modification de l’original.
  - Aucun coût de copie, mais mêmes risques de durée de vie qu’avec un pointeur (référence vers un objet détruit = UB).

---

### 5.2. Différences `++p` (préfixe) / `p++` (postfixe)

Dans cette classe :

- **Préfixe** : 

  ```cpp
  Point & Point::operator++()
  {
      ++x;
      ++y;
      return *this;
  }
  ```

  - Modifie l’objet
  - Renvoie une **référence** sur l’objet modifié
  - Utilisation : `++p;` ou `++(*this);`

- **Postfixe** :

  ```cpp
  Point Point::operator++(int)
  {
      Point tmp(x, y);
      x++;
      y++;
      return tmp;
  }
  ```

  - Crée une **copie** de l’état AVANT l’incrément
  - Modifie l’objet
  - Renvoie la **copie** (l’ancienne valeur)
  - Utilisation : `p++;` ou `(*this)++;`

Conséquences :

- `p++` renvoie une valeur temporaire (ancienne valeur) ; `++p` renvoie une référence sur `p`.
- Dans une expression comme `cout << p++`, on affiche **l’ancienne valeur**, mais `p` est déjà incrémenté.

---

### 5.3. Mécanique interne de `M1`, `M2`, `M3` dans le main original

Pour être complet, voici ce qui se passe dans le `main` original avec `p1`, `p2`, `p3` :

```cpp
int main()
{
    Point p1(1, 2);
    Point p2;
    Point *p3;

    p2 = p1.M1();
    cout << "p1 = " << p1 << endl;
    cout << "p2 = " << p2 << endl;

    p3 = p2.M2();
    cout << "p2 = " << p2 << endl;
    cout << "*p3= " << *p3 << endl;

    p1 = p2.M3();
    cout << "p2 = " << p2 << endl;
    cout << "p1 = " << p1 << endl;

    return 0;
}
```

Étapes (sans entrer dans les constructions/déstructions) :

1. Initialisation :
   - `p1 = (1,2)`
   - `p2 = (0,0)`
   - `p3` = indéterminé

2. `p2 = p1.M1();`
   - `M1` :
     - `(*this)++;` → `p1` : `(1,2)` → `(2,3)`
     - `return *this;` → retourne `(2,3)` (par valeur)
   - Affectation : `p2` devient `(2,3)`

   Après ligne 12 :
   - `p1 = (2,3)`
   - `p2 = (2,3)`

3. `p3 = p2.M2();`
   - `M2` :
     - `++(*this);` → `p2` : `(2,3)` → `(3,4)` (préfixe)
     - `return this;` → adresse de `p2`
   - `p3` pointe sur `p2`.

   Après ligne 16 :
   - `p2 = (3,4)`
   - `p3 == &p2` → `*p3 = (3,4)`

4. `p1 = p2.M3();`
   - `M3` :
     - `(*this)++;` → `p2` : `(3,4)` → `(4,5)` (postfixe)
     - renvoie une **référence** sur `p2` (`(4,5)`)
   - Affectation : `p1 = p2;` → `p1` devient `(4,5)`

   Après ligne 20 :
   - `p2 = (4,5)`
   - `p1 = (4,5)`
   - `p3` pointe toujours sur `p2` → `*p3 = (4,5)`

Cette analyse illustre parfaitement :
- L’effet de la modification de l’objet courant via `this`,
- La différence entre retour par valeur, pointeur, référence,
- Les conséquences sur les variables `p1`, `p2`, `p3`.

---

[...retorn en rèire](../../menu.md)