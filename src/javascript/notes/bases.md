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

### Les objets

A ce qu'il semblerait, les notions d'objets et de classes sont distinctes en js. Il est ainsi possible d'avoir des objets qui ne sont pas forcément associés à une classe.

Les objets sont ainsi simplement des conteneurs qui possèdent plusieurs propriétés.

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

### Les tableaux

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

### Copie et référence

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