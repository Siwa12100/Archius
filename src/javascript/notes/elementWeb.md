# Interagir avec une page Web

Une page html possède une structure qui ressemble à une arborescence avec ses balises imbriquées. Cette structure est appelée `DOM` pour document object model.
C'est avec cette structure que l'on va interagir en js pour rendre dynamique la page.

**Attendre que le rendu de la page soit terminé :**

Etant donné que nos scripts sont chargés dans le body, ils font parfois référence à des éléments de la page avant même que ceux-ci ne soient chargés. On utile donc le mot clé `defer` qui fait en sorte d'attendre que la page soit entièrement chargée avec de lancer les scripts :

```html
<script src="..." defer></script>
```

## Récupérer un élément du DOM

A chaque fois, on utilise le mot clé `document` pour indiquer que l'on intéragie avec le DOM. querySelector et querySelectorAll permettent de sélectionner des éléments de la même manière qu'on le ferait dans un fichier css pour appliquer des propriétés à des éléments.

### getElementById

Tout est dans le nom. Si on a une balise avec un id comme par exemple : `<div id="monId" >...</div>`, il est possible de récupérer l'élément en appelant la méthode getELementById : `let maDiv = document.getElementById("monId")`.
Le type récupéré dans la variable est `HTMLElement`.

### querySelector

Cette méthode permet de récupérer un seul élément du DOM.

```html
<div id="monId">
    <h1> ... </h1>
    <h1> ... </h1>
</div>

<div class="maClasse">
    ...
</div>
```

```js
// Dans le js
let maDiv = document.querySelector("#monId"); // permet de récupérer la div
let monTitre = document.querySelector("#monId h1"); // permet de récupérer le premier titre

// on récupère la div depuis sa classe comme en css 
let monAutreDiv = document.querySelector(".maClasse");
```

### querySelectorAll

Permet de récupérer plusieurs éléments.

```html
<div id="monId">
    <h1> ... </h1>
    <h1> ... </h1>
</div>
```

```js
// Dans le js
let mesTitres = document.querySelector("#monId h1"); // permet de récupérer les deux titres

/*
    On récupère ainsi une NodeList, qui est une sorte de tableau d'éléments que l'on peut parcourir
*/

for (let i = 0; i < mesTitres.length; i++) {
    console.log(mesTitres[i]);
}
```

### Modifier un élément du DOM

Une fois qu'un élément est sélectionné, il est possible de le modifier.

**Modifier ses attributs**

Dans le html :

```html
...
<img id="monImage" alt="voici une image..." src="..." class="premiereClasse secondeClasse">
```

Dans le js :

```js
// on récupère l'image :
let monImage = document.getElementById("monImage");
// On modifie l'attribut alt de l'image :
monImage.setAttribute("alt", "le nouveau alt de l'image ! ");
// Autre manière de le faire :
monImage.alt = "Encore un nouveau alt pour l'image ! ";
```

**Modifier les classes d'un élément**

De la même manière, il est possible de modifier les classes d'un élément.

voilà le code :

```js
// On imagine qu'on reprend l'image html juste au dessus
// On ajoute une classe 
monImage.classList.add("maNouvelleClasse");

// On supprime une classe de l'image
monImage.classList.remove("secondeClasse");
```

## Créer une nouvelle balise

### createElement

Il s'agit d'une méthode qui créé tout simplement un nouvel élément : `let maDiv = document.createElement("div")`.

Il est ensuite possible d'insérer un élément en tant qu'enfant d'un autre élément avec `appendChild(...)`.

**Exemple :**

```js
// on récupère une div
let maDiv = document.getElementById("maDiv");
// on créé un titre et une description
let monNouveauTitre = document.createElement("h1");
let maNouvelleDescription = document.createElement("p");
// On leur donne une valeur
monNouveauTitre.textContent = "Titre de ma partie !";
maNouvelleDescription.textContent = "ma description..... ";
// on les ajoute à la div 
maDiv.appendChild(monNouveauTitre);
maDiv.appendChild(maNouvelleDescription);
...
```

### innerHTML et interpolation

Une autre manière de faire, est de rajouter directement du code html dans la page avec la méthode `innerHtml`.
Pour cela, on va tout d'abord utiliser l'interpolation, qui consiste en gros à déclarer une chaîne de caractères ayant des parties interprétées dynamiquement.

**Exemple :**

```js
// on défini les valeurs...
const titre = "Titre de la partie ";
const description = "ma super description";

// On créée une div
let maDiv = `
    <div>
        <h1>${titre}</h1>
        <p>${description}</p>
    </div>
`;

// On récupère le body de la page
let monBody = document.querySelector("body");
// On lui ajoute le code html
monBody.innerHTML += maDiv;
```

---

[...retour au sommaire](../sommaire.md)