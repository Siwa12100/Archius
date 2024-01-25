# Interagir avec une page Web

Une page html possède une structure qui ressemble à une arborescence avec ses balises imbriquées. Cette structure est appelée `DOM` pour document object model.
C'est avec cette structure que l'on va interagir en js pour rendre dynamique la page.

**Attendre que le rendu de la page soit terminé :**

Etant donné que nos scripts sont chargés dans le body, ils font parfois référence à des éléments de la page avant même que ceux-ci ne soient chargés. On utile donc le mot clé `defer` qui fait en sorte d'attendre que la page soit entièrement chargée avec de lancer les scripts :

```html
<script src="..." defer></script>
```

### Récupérer un élément du DOM

A chaque fois, on utilise le mot clé `document` pour indiquer que l'on intéragie avec le DOM. querySelector et querySelectorAll permettent de sélectionner des éléments de la même manière qu'on le ferait dans un fichier css pour appliquer des propriétés à des éléments.

**getElementById :**

Tout est dans le nom. Si on a une balise avec un id comme par exemple : `<div id="monId" >...</div>`, il est possible de récupérer l'élément en appelant la méthode getELementById : `let maDiv = document.getElementById("monId")`.
Le type récupéré dans la variable est `HTMLElement`.

**querySelector :**

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

**querySelectorAll :**

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

---

[...retour au sommaire](../sommaire.md)