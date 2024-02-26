# Les objets

[...retour aux bases](./bases.md)

---

Attention, en js, les notions d'objets et classes sont bien distinctes, dans le sens où l'on peut avoir des objets sans pour autant avoir de classes associées.
Ils fonctionnent avec un système de clé/valeur et sont créés à l'aide de `{}`.

Les objets sont ainsi simplement des conteneurs qui possèdent des couples clé/valeur appelés propriétés.

## Déclaration & manipulation

```js
let maPersonne = {
  nom: "Louis",
  prenom: "Dupont",
  age: 34,
  enVie: true

  direBonjour : function() {
    console.log("Bonjour je m'appelle" + this.nom + " !");
  }
};
```

Il est ensuite possible d'appeler les différentes propriétés de manière assez classique : `maPersonne.age = 56;`.
Si la propriété n'existe pas, elle est créée `maPersonne.surnom = "Loulou";`.

On remarque l'utilisation de `this` pour faire référence à l'objet en lui même.

Il est possible de supprimer une propriété en utilisant `delete` :

```js
delete maPersonne.enVie;
```

### Créer un objet vide

Il est possible de créer des objets vide en faisant :

```js
let monObjet = {}
// Ou bien
let monAutreObjet = new Object();
```

### Propriétés calculées

Il est possible de définir une propriété d'un objet à partir d'une valeur variable. Par exemple :

```js
// On récupère la valeur de la clé 
let maFutureCle = prompt("Entrez une clé", "maCle");

let monObjet = {

  nom : "Nom de l'objet",
  [maFutureCle] : 56, 
  ["voici_" + maFutureCle] : 67
};

// On imagine que l'utilisateur a entré la valeur "voiture" dans le prompt...
console.log(monObjet.voiture); // -> 56
console.log(monObjet.voici_voiture); // -> 67
```

Cela permet de définir de manière dynamique la clé. On voit qu'il est aussi possible de compléter le nom de la clé en ajoutant du contenu à l'intérieur des `[]`.

### Fonctions renvoyant un objet

Une fonction peut renvoyer un objet. On peut transformer très simplement un paramètre de la fonction en propriété, où le nom du paramètre sera la clé de la propriété, et sa valeur la valeur de la propriété.

```js
function maFonction(nom, description) {

  return {
    nom,
    description
  };
}

let monLivre = maFonction("Titre du livre", "description du livre");
console.log(monLivre.nom); // -> Titre du livre
console.log(monLivre.description); // description du livre
```

### Voir si une propriété existante : In

Pour voir si une propriété existe bien, la meilleure pratique est d'utiliser l'opérateur `in` qui renvoie un booléen :

```js

const monObjet {

  nom = "coucou"
};

if ("nom" in monObjet) {
  console.log(monObjet.nom);
} else {
  ...
}
```

### Parcourir les propriétés d'un objet : for .. in

On peut parcourir l'ensemble de propriétés d'un objet avec les mots clés `for` et `in` :

```js

const monObjet {
  nom : "coucou",
  titre : "coucou 2", 
  description : "coucou 3"
};

for (let p in monObjet) {
  console.log(p);
}
```

## Prototypes




--- 
[...retour aux bases](./bases.md)