# JavaScript - Les bases

## Conventions de nommage

* camelCase `maVariable` pour variables et fonctions.
  
* PascalCase pour les classes `MaClasse`.
  
* Snake_case pour les constantes `MA_CONSTANTE`.

## Les variables

### Déclaration

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


## Afficher infos en console

* `console.log("coucou : ", maVariable);` pour afficher des infos en console.

**Autres méthodes d'affichage :**

* `console.info("Ceci est un message informatif");`
* `console.warn("Ceci est un avertissement");`
* `console.error("Ceci est une erreur");`

On peut aussi utiliser `prompt` pour demander de saisir une valeur :

```js
let maValeur = prompt("message a envoyer a l'utilisateur", "valeur par defaut");
```

## Types de données

### Les types primitifs

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

**L'interpolation :**

Il est possible d'interpréter le contenu d'une chaine de caratère dynamiquement en utilisant \` et `${}` :

```js
const monNom = "Siwa";
console.log(`Bonjour, je suis ${monNom}`);
```

### Les objets

Attention, en js, les notions d'objets et classes sont bien distinctes, dans le sens où l'on peut avoir des objets sans pour autant avoir de classes associées.
Ils fonctionnent avec un système de clé/valeur et sont créés à l'aide de `{}`.

Les objets sont ainsi simplement des conteneurs qui possèdent des couples clé/valeur appelés propriétés.

**Déclaration :**

```js
let maPersonne = {
  nom: "Louis",
  prenom: "Dupont",
  age: 34,
  enVie: true
};
```

Il est ensuite possible d'appeler les différentes propriétés de manière assez classique : `maPersonne.age = 56;`.
Si la propriété n'existe pas, elle est créée `maPersonne.surnom = "Loulou";`.

Il est possible de supprimer une propriété en utilisant `delete` :

```js
delete maPersonne.enVie;
```

**Créer un objet vide**

Il est possible de créer des objets vide en faisant :

```js
let monObjet = {}
// Ou bien
let monAutreObjet = new Object();
```

**Propriétés calculées**

Il est possible de définir une propriété d'un objet à partir d'une valeur variable. Par exemple :

```js
// On récupère la valeur de la clé 
let maFutureCle = prompt("Entrez une clé", "maCle");

let monObjet = {

  nom : "Nom de l'objet",
  [maFutureCle] : 56, 
  ["voici_" + maFutureCle] : 67
};

// On imagine que l'utilisateur a entré la valeur "voiture" dans le prompt...
console.log(monObjet.voiture); // -> 56
console.log(monObjet.voici_voiture); // -> 67
```

Cela permet de définir de manière dynamique la clé. On voit qu'il est aussi possible de compléter le nom de la clé en ajoutant du contenu à l'intérieur des `[]`.

## Les tableaux

**Declaration :**

```js
let mesFruits= ["pomme", "poire", "figue"];
```

**Accès :**

```js
const monFruit = mesFruits[2];
```

**Nombre d'éléments :**

```js
const maTaille = mesFruits.length;
```

**Ajouter & supprimer :**

```js
// Rajouter en fin de tableau :
mesFruits.push("framboise");
// Supprimer en fin de tableau :
mesFruits.pop();
// A supprimé framboise...
```

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