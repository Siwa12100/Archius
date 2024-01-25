# JavaScript - Les bases

### Conventions de nommage

* camelCase `maVariable` pour variables et fonctions.
  
* PascalCase pour les classes `MaClasse`.
  
* Snake_case pour les constantes `MA_CONSTANTE`.

### Les variables

On déclare des variables avec `let` et des constantes avec `const`. Elles ont une portée de blocs `{}.`

**Exemple :**

```js
let maVariable = 6;
const maConstante = "coucou"
```

### Afficher infos en console

* `console.log("coucou : ", maVariable);` pour afficher des infos en console.

**Autres méthodes d'affichage :**

* `console.info("Ceci est un message informatif");`
* `console.warn("Ceci est un avertissement");`
* `console.error("Ceci est une erreur");`

**Les types :**

Il existe 3 types principaux :

* **Number** qui correspond aux nombres entiers et à virgule.
* **Boolean** les booléens.
* **String** les chaînes de caractère.

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

