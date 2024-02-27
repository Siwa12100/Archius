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

Il est possible de convertir des types entre eux :

```js
let monChiffre = "150";
let monString = 780;

// Conversion en nombre
const test = Number(monChiffre);
// Conversion en string
const test2 = String(monString);
```

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

---
[...retour au sommaire](../sommaire.md)