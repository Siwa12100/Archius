# Notes JavaScript

[...retour au sommaire](../../../README.md)

---

## JavaScript - Introduction

C'est un des 3 languages au coeur du Web, avec HTML & CSS. Il permet de rajouter de l'interactivité dans les pages.
En plus de son utilisation dans le cadre des navigateurs web, il est aussi utilisé comme un language côté serveur, avec des environnements comme Node.js. Il peut aussi être utilisé dans le cadre d'applications de bureau (avec Electron) et mobiles (avec React Native) entre autre.

## Sommaire

### Roadmap.sh - JavaScript

* [Variables & objets](./notes/bases.md)
* [Les erreurs](./notes/erreurs.md)

* [Arrays](./notes/array.md)
* [JSON](./notes/json.md)

* [Fonctions, boucles & conditions](./notes/fonctions.md)
* [Fetch & asynchrone](./notes/asynchrone.md)
* [Les classes](./notes/classes.md)

### Interagir avec le DOM

* [Interagir avec page web](./notes/elementWeb.md)
* [Les formulaires](./notes/formulaires.md)

### Roadmamp.sh - Node.js

* [introduction à node](./notes_nodeJS/bases.md)

## Autres notes utiles

### Récupérer une valeur

La fonction `prompt` permet de demander de saisir une valeur en ouvrant une fenêtre popup. C'est vraiment inssuportable et à proscrire, mais utile pour le débuggage...

### Inclusion de code js dans le html

Pour inclure un script js dans une vue html, on utilise la balise `script`.

**Exemple :**

```js
let monMot = prompt("Entrez un mot  : ");
console.log(monMot);
```

**Exemple :**

```html
<!DOCTYPE HTML>
<html lang="fr">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <script src="script.js"></script>
</head>
<body>

</body>
</html>
```

Dans cet exemple, on admet que le fichier `script.js` est dans le même dossier que le fichier html. s'il avait été dans un dossier `src`, on aurait mis : `<script src="src/script.js></script>`.

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
[retour à l'accueil](../../../README.md)
