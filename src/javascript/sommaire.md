# Notes JavaScript

[...retour au sommaire](../../README.md)

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

## Ancien sommaire

**les bases :**


* 
* [Interagir avec page web](./notes/elementWeb.md)
* [Les formulaires](./notes/formulaires.md)
* [Les classes](./notes/classes.md)

## Ressources intéressantes

* 

---
[retour à l'accueil](../../README.md)
