# Notes - Fonctions, boucles & conditions

[...retour au sommaire](../sommaire.md)

---

## Boucles & conditions

### Les conditions

Le grand classique if/else :

```js
if (la condition...) {
    
} else {

}
```

**Exemple :**

```js
let monMot = prompt("Entrez un mot  : ");
console.log(monMot);
```

Le **switch/case** fonctionne aussi de manière classique :

```
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

### Declaration de fonctions

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

maFonction("coucou", 56);
const test = monAutreFonction();
```

### Inclusion de fichiers et portées de variables

L'inclusion de fichiers.js entre eux a l'air de ressembler à ce qui se fait en php avec un `require`.
Dans ce sens, avec ce code :

```html
<script src="src/premierScript.js"></script>
<script src="src/secondScript.js"></script>
```

C'est comme si on fusionnait les fichiers ensemble, dans l'ordre de leur inclusion. Donc secondScript connait l'ensemble de premierScript, mais premierScript ne connait pas le contenu de secondScript.

Grâce à cette inclusion, il est possible de découper le code en différents fichiers.

Les variables ont comme portée l'ensemble de leur bloc `{}`. Dans ce sens, les variables dans un bloc sont appelées locales, et les variables déclarées directement dans le script en dehors des blocs sont dites globales.

**Exemple :**

```js
let maVariableGlobale = 67;

function maFonction() {
    ...
    const maVariableLocale = "coucou";
    console.log(maVariableLocale); // marche car c'est le même bloc
    console.log(maVariableGlobale); // marche car variable locale
}

console.log(maVariableLocale); // n'existe plus ici
console.log(maVariableGlobale); //marche toujours
```

---

[retour au sommaire](../sommaire.md)
