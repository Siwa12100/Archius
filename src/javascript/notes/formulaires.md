# Les formulaires

[Rappel sur les formulaires](../../htmlCss/fichiers/formulaires.md)

## Récupérer la valeur d'un champ de formulaire

### Cas des Inputs

Dans le html :

```html
<form ...>
    ...
    <input id="idInput" name="nomInput" type="text">
    ...
</form>
```

Récupération de la valeur en js :

```js
// On récupère l'élément par son nom :
let monInput = document.querySelector("input[name='nomInput']");
// On aurait aussi pu le récupérer par l'id comme ça :
let monInput = document.getElementById("idInput");

// Ensuite on récupère sa valeur :
let valeur = monInput.value;
```

### Cas de boutons radios

Dans le html :

```html
<label>Préférence de couleur :</label>

<input type="radio" id="red" name="couleur" value="red" checked>
<label for="red">Rouge</label>

<input type="radio" id="blue" name="couleur" value="blue">
<label for="blue">Bleu</label>

<input type="radio" id="green" name="couleur" value="green">
<label for="green">Vert</label>
```

Récupération de la valeur en js :

```js
// On récupère une liste d'éléments
let baliseCouleur = document.querySelectorAll('input[name="couleur"]')
let couleur = ""
// On parcours l'ensemble des éléments récupérés
for (let i = 0; i < baliseCouleur.length; i++) {
    // On regarde lequel a la valeur true
    if (baliseCouleur[i].checked) {
        // On récupère sa valeur et on quitte le for
        couleur = baliseCouleur[i].value
        break
    }
}
....
....
```

## Récupérer les données du formulaire

Lorsqu'un formulaire est levé, un évènement `submit` est levé. Il nous faut ainsi simplement ajouter un écouteur sur cet évènement pour ensuite traiter les données.

Par défaut, le comportement de cet évènement fait que la page est rafraichie à la soumission. Généralement on souhaite annuler ce comportement pour faire en sorte qu'il n'y ait pas de rafraichissement de la page.
Pour cela, au début du code actionné par l'écouteur à la soumission, on commence par un `event.preventDefault()` pour annuler le comportement par défaut.

### Mettre en place une vérification

Une fois qu'on a récupéré les valeurs entrées dans le formulaire, on va appeler une petite fonction qui va se charger de vérifier que les données rentrées sont bien valides. Si ce n'est pas le cas, elle lèvera une erreur à l'aide du mot clé `throw new Error("message d'erreur... ")`.

On ajoutera donc au code du listener un bloc `try/catch`, et en cas d'erreur, on affichera un message d'erreur dans la vue.

**Code général :**

```js
function verifValeurExistante(valeur) {

    if (valeur === "") {
        throw new Error("La valeur est nulle ! ");
    }
}


let monFormulaire = document.getElementById("idDeMonFormulaire");

monFormulaire.addEventListener("submit", (event) => {

    try {
        // on récupère les valeurs du formulaire
        let nom = document.querySelector("input[name='nomInput']").value;
        let prenom = document.querySelector("input[name='prenomInput']").value;

        // On vérifie que les valeurs entrées sont correctes
        verifValeurExistante(nom);
        verifValeurExistante(prenom);

    } catch (erreur) {
        console.log("Erreur : " + erreur.message);
    }
});
```

Imaginons que l'on ai en plus dans le css une petite classe erreur :

```css
.erreur {
    border: 1px solid red;
}
```

Et que l'on rajoute une fonction qui affiche un message dans la vue :

```js
function afficherErreur(message) {
    let erreurDiv = document.createElement("div");
    let erreurTitre = document.createElement("h3");
    erreurTitre.textContent = message;
    erreurDiv.appendChild(erreurTitre);
    document.body.appendChild(erreurDiv);
}
```

On pourrait ainsi rajouter ce code dans le catch pour réagir à l'erreur :

```js
...
// Afficher l'erreur dans l'interface utilisateur
afficherErreur("Erreur : " + erreur.message);

// Mettre en surbrillance en rouge les champs avec des erreurs
document.querySelector("input[name='nomInput']").classList.add("erreur");
document.querySelector("input[name='prenomInput']").classList.add("erreur");
```

---

[...retour au sommaire](../sommaire.md)