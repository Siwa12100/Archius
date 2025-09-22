# JavaScript - Variables & Objets

[...retour au sommaire](../sommaire.md)

---

## Déclaration

On déclare des variables avec `let` et des constantes avec `const`. Elles ont une portée de blocs `{}.`

**Exemple :**

```js
let maVariable = 6;
const maConstante = "coucou"
```

On utilise la convention de nommage **camelCase** et les noms de variables doivent toujours commencer par une lettre. La casse compte donc `pomme` et `Pomme` sont des variables différentes.

Une bonne pratique est de nommer les constantes définies dès le chargement de la page (comme des couleurs, des noms, ...) en majuscule, en séparant les mots par des `_` : `MA_CONSTANTE`.
Mais si une constante est calculée pendant l'exécution du script, elle peut être nommée comme une variable `let` en utilisant le **camelCase**.

### Hoisting (remontée)

Il s'agit du fait d'utiliser des fonctions ou des variables avant même leur déclaration. cela correspond par exemple à faire cela :

```js
console.log(val);
var val = 8;
// ça marche, mais de toute manière il ne faut pas utiliser var..

maFonction()
...
...
function maFonction() {
  ...
  ...
}
// C'est une manière d'appeler la fonction avec la déclaration. Cela ressemble à ce que l'on peut 
// faire avec les prototypes en C par exemple...
```

### Scopes

Les scopes en js font référence à la visibilitéd'une variable et comment elle peut être utilisée après sa déclaration.
Il existe 3 types de scopes :

* **Global scope :** Il s'agit des variables déclarées en dehors de fonctions ou même de `{}`. Elles sont ainsi accessible dans l'ensemble du code.

* **Function scope :** Les variables déclarées dans le corps d'une fonction ne peuvent être utilisées qu'en son sein.

* **Block scope :** Il s'agit des variables définies dans un bloc délimité par des `{}`. Le fonctionnement est le même que le function scope.


### Afficher infos en console

* `console.log("coucou : ", maVariable);` pour afficher des infos en console.

**Autres méthodes d'affichage :**

* `console.info("Ceci est un message informatif");`
* `console.warn("Ceci est un avertissement");`
* `console.error("Ceci est une erreur");`

On peut aussi utiliser `prompt` pour demander de saisir une valeur :

```js
let maValeur = prompt("message a envoyer a l'utilisateur", "valeur par defaut");
```

## Les types primitifs

Les 3 types primitifs principaux sont :

* **Number** qui correspond aux nombres entiers et à virgule.
* **Boolean** les booléens.
* **String** les chaînes de caractère.
  
-> Les autres sont à retrouver [ici](https://developer.mozilla.org/en-US/docs/Glossary/Primitive).

On peut concaténer des strings avec le `+` :

```js
const nom = "Dupont";
const prenom = "Louis";
const assemblage = nom + penom;
```

Il est possible de convertir des types entre eux.

### L'interpolation

Il est possible d'interpréter le contenu d'une chaine de caratère dynamiquement en utilisant \` et `${}` :

```js
const monNom = "Siwa";
console.log(`Bonjour, je suis ${monNom}`);
```

## Les objets

Notes à retrouver [ici](./objets.md) sur les objets.

## Typeof

On peut utiliser le mot clé `typeof` pour récupérer sous la forme d'une chaîne de caractère le type d'un élément :

```js

console.log(typeof 45); // -> number
console.log(typeof (45 == 45)); // -> Boolean
...
```

Les différents strings pouvant être retournés sont :

* number
* string
* boolean
* object
* function
* symbol

## Copie et référence

Si on affecte à une nouvelle variable un type primitif(string, nombre ou bool), alors c'est une affectation par copie :

```js
const val = 43;
const nouvelleVal = val;
```

Mais si ce n'est pas un type primitif (donc un tableau, un objet ou une classe...), c'est un passage simplement par référence sur la même instance :

```js
const mesFruits = ["pomme", "poire"];
const monNouveauTableau = mesFruits; // même instance...
```

**Copier un tableau :**

Si on veut vraiment copier un tableau et pas simplement le duppliquer, on peut utiliser l'opérateur `...`.

```js
const mesFruits = ["pomme", "poire"];
// On fait un nouveau tableau et on copie les valeurs 
const monNouveauTab = [...mesFruits];
```

## Conversion de type

Voici un résumé concis des principales méthodes pour convertir des types de données entre eux en JavaScript.

### Conversion explicite de type

1. **`Number()` :** Convertit une valeur en un nombre.

   ```javascript
   let str = "42";
   let num = Number(str);  // num sera 42 (en tant que nombre)
   ```

2. **`String()` :** Convertit une valeur en une chaîne de caractères.

   ```javascript
   let num = 42;
   let str = String(num);  // str sera "42" (en tant que chaîne)
   ```

3. **`Boolean()` :** Convertit une valeur en un booléen.

   ```javascript
   let str = "true";
   let bool = Boolean(str);  // bool sera true (non vide est équivalent à true)
   ```

4. **`parseInt()` :** Convertit une chaîne en un entier.

   ```javascript
   let str = "42";
   let integer = parseInt(str);  // integer sera 42 (en tant qu'entier)
   ```

5. **`parseFloat()` :** Convertit une chaîne en un nombre à virgule flottante.

   ```javascript
   let str = "42.5";
   let floatNum = parseFloat(str);  // floatNum sera 42.5 (en tant que nombre à virgule flottante)
   ```

### Conversion implicite de type (coercition)

La conversion implicite se produit automatiquement lors d'opérations entre des types différents.

1. **De chaîne à nombre :**
   ```javascript
   let str = "42";
   let num = +str;  // num sera 42 (en tant que nombre)
   ```

2. **De nombre à chaîne :**
   ```javascript
   let num = 42;
   let str = "" + num;  // str sera "42" (en tant que chaîne)
   ```

3. **De valeur à booléen :**
   ```javascript
   let value = "hello";
   let bool = !!value;  // bool sera true (non vide est équivalent à true)
   ```

4. **De booléen à nombre :**
   ```javascript
   let bool = true;
   let num = +bool;  // num sera 1 (true est équivalent à 1)
   ```

## Comparaisons

En JavaScript, `==` (opérateur d'égalité) et `===` (opérateur d'égalité stricte) sont deux opérateurs utilisés pour comparer des valeurs. La principale différence entre les deux réside dans la façon dont ils gèrent les types de données et les conversions de type.

### `==` (Opérateur d'égalité) :

L'opérateur `==` effectue une comparaison égalité non stricte, ce qui signifie qu'il effectue des conversions de type si les types des opérandes sont différents avant de comparer les valeurs. Par exemple, lorsqu'on compare une chaîne et un nombre, JavaScript tentera de convertir la chaîne en un nombre avant de faire la comparaison.

**Exemple :**

```javascript
"5" == 5;  // true, car la chaîne "5" est convertie en nombre avant la comparaison
```

### `===` (Opérateur d'égalité stricte) :

L'opérateur `===` effectue une comparaison égalité stricte, ce qui signifie qu'il ne fait pas de conversion de type. Les opérandes doivent avoir le même type et la même valeur pour que la comparaison retourne `true`.
**
Exemple :**

```javascript
"5" === 5;  // false, car les types sont différents (chaîne vs nombre)
```

### En ce qui concerne les objets :

Lorsque l'on compare des objets avec `==` ou `===`, le comportement dépend de la référence des objets, pas de leur contenu. Les deux opérateurs comparent les références mémoire.

Exemple :

```javascript
var obj1 = { key: "value" };
var obj2 = { key: "value" };

obj1 == obj2;  // false, car ce sont des références différentes
obj1 === obj2;  // false, car ce sont des références différentes
```

Dans cet exemple, même si les objets ont des propriétés identiques, la comparaison renvoie `false` car ce sont deux objets distincts en mémoire.

---
[...retour au sommaire](../sommaire.md)