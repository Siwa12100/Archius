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

### Parcours d'arguments

Il est possible de passer un nombre variable d'arguments à des fonctions. On récupère ensuite ces arguments grâce à un tableau appelé `arguments`.

```js

maFonction() {
    
    for (let i = 0; i < arguments.lengh, i++) {
        console.log(arguments[i])
    }
}

maFonction("Pomme", "Poire", "Prune");
---



[retour au sommaire](../sommaire.md)
