# Notes - Fonctions, boucles & conditions

[...retour au sommaire](../sommaire.md)

---

## Boucles & conditions

### Les conditions

Le grand classique **if/else** :

```js
if (la condition...) {
    
} else {

}
```

Le **switch/case** fonctionne aussi de manière classique :

```js
switch(maVariable) {

    case 1 :
        ...
        break;
    
    case 2 : 
        ...
        break;

    default :
    ...
}
```

### Les boucles

**for & while :**

Il faut distinguer le `for .. in` (plus réservé pour des collections clé / valeur) et le `for .. of` pour des collections comme des tableaux.

```js
for (let cpt = 0; cpt < 10; cpt++) {
    console.log(cpt);
}

let tableau = ["pomme", "poire", "figue"];

for (let fruit in tableau) {
    console.log(fruit); // Va afficher l'index du fruit : 1 , 2, ...
}

for (let fruit of tableau) {
    console.log(fruit); // Va afficher le fruit : pomme, ...
}

let monObjet = {
    nom : "Louis", 
    age : 45
}

for (let propriete in monObjet) {

    console.log(propriete + " -> " + monObjet[propriete]); // Va afficher nom -> Louis, puis age -> 45
}

let i = 0;

while (i < 10) {
    console.log(i);
    i++;
}

do {
    ...
} while (... != ...);
```

## Les fonctions

### Declaration

Il n'est pas utile de préciser le type de retour de la fonction et le type des paramètres en js.

**Exemple :**

```js
function maFonction(mot, nombre) {

    let monTexte = "bonjour mon mot est " + mot + " et mon nombre est " + nombre + ".";
    console.log(monTexte); 
}

function monAutreFonction() {

    // ...
    let val = 56;
    return val;
}
```

Il est aussi possible de stocker des fonctions dans des variables et de passer des fonctions en paramètre d'autres fonctions : 

```js

// Déclaration d'une fonction anonyme
const maFonction1 = function (msg) {
    console.log(msg);
}

let tableau = new Array("Pomme", "Poire", "Prune", "Figue");

// On passe l'autre fonction en paramètre...
function monAutreFonction(f, tableau) {

    for (let fruit of tableau) {
        f(fruit);
    }
}

monAutreFonction(maFonction1, tableau);
```

Il est aussi possible de déclarer des fonctions au sein d'autres fonctions (elles ont ainsi accès à toutes les données des fonctions parentes. Cela s'appelle des fonctions imbriquées. 

```js

function maFonction(msg) {

    let tab = ["Pomme", "Poire", "Prune"];
    function maFonctionImbriquee(fruit) {
        console.log(" --> " + msg + " : " + fruit);
    }

    for (let f of tab) {
        maFonctionImbriquee(f);
    }
}
```

Il est aussi possible de donner des valeurs par défaut aux paramètres d'une fonction : 

```js

function maFonction(val, msg = "coucou") {
    console.log(msg + " : " + val);
}

maFonction(4); // -> Affiche 4 : coucou
```

### Parcours d'arguments

Il est possible de passer un nombre variable d'arguments à des fonctions. On récupère ensuite ces arguments grâce à un tableau appelé `arguments`.

```js

maFonction() {
    
    for (let i = 0; i < arguments.length, i++) {
        console.log(arguments[i])
    }
}

maFonction("Pomme", "Poire", "Prune");

// On peut aussi stocker des paramètres variables dans un tableau à l'aide du mot clé ... : 
function monAutreFonction(valeur, mot, ...autresArguments) {
    console.log("Ma valeur : " + valeur);
    console.log("Mon mot : " + mot);

    autresArguments.forEach((argument) => {
        console.log(" - " + argument);
    });
}

monAutreFonction(42, "Bonjour", "Argument1", "Argument2", "Argument3");
```

## Fonctions fléchées

Il s'agit d'un moyen plus concis de déclarer des fonctions. 

**Syntaxe générale :**

```js

// Avec deux arguments
const maFonction = (val1, val2) => {
    ....
    ....
}

// Avec un seul argument 
const maFonction2 = (val) => {
    ...
    ...
}

// Sans argument
const maFonction3 = () => {
    ...
    ...
}
```

Si la fonction peut être écrite sur une seule ligne, alors les accolades ne sont plus obligatoires et la ligne unique se comporte comme un `return`.

```js

const addition = (val1, val2) => val1 + val2;

// Revient à la même chose que (ps : ceci est une fonction anonyme): 
const autreAddition = function (val1, val2) {
    return val1 + val2;
}
```

---



[retour au sommaire](../sommaire.md)
