# Notes - Tableau

[...retour au sommaire](../sommaire.md)

---

Un tableau (ou array) en JavaScript est un type de données qui permet de stocker plusieurs valeurs dans une seule variable. Les tableaux sont utiles pour organiser et manipuler des collections d'éléments. Voici les points clés à connaître sur les tableaux en JavaScript :

### Création d'un tableau :

On peut créer un tableau de différentes manières :

1. **Avec la syntaxe littérale :**
   ```javascript
   var fruits = ["Pomme", "Banane", "Orange"];
   ```

2. **En utilisant le constructeur `Array()` :**
   ```javascript
   var colors = new Array("Rouge", "Vert", "Bleu");
   ```

### Accès aux éléments :

Les éléments d'un tableau sont indexés, et l'index commence à zéro.

```javascript
var fruits = ["Pomme", "Banane", "Orange"];
console.log(fruits[0]);  // Affiche "Pomme"
```

### Modification d'un tableau :

On peut ajouter, supprimer et modifier des éléments d'un tableau.

#### Ajout d'éléments :

```javascript
fruits.push("Fraise");  // Ajoute "Fraise" à la fin du tableau
fruits.unshift("Cerise");  // Ajoute "Cerise" au début du tableau
```

#### Suppression d'éléments :

```javascript
fruits.pop();  // Supprime le dernier élément du tableau (retourne "Fraise")
fruits.shift();  // Supprime le premier élément du tableau (retourne "Cerise")
```

#### Modification d'éléments :

```javascript
fruits[1] = "Ananas";  // Modifie la deuxième position du tableau
```

### Propriétés et méthodes utiles :

- **`length` :** Propriété qui indique la longueur du tableau.
  ```javascript
  console.log(fruits.length);  // Affiche la longueur du tableau
  ```

- **`indexOf()` :** Méthode qui renvoie l'index d'un élément dans le tableau (ou -1 s'il n'est pas présent).
  ```javascript
  var index = fruits.indexOf("Banane");  // Renvoie l'index de "Banane"
  ```

- **`slice()` :** Méthode qui crée une copie partielle du tableau.
  ```javascript
  var subset = fruits.slice(1, 3);  // Copie les éléments de l'index 1 à 2 (exclu)
  ```

### Itération à travers un tableau :

On peut utiliser des boucles pour parcourir les éléments d'un tableau.

#### Boucle `for` :
```javascript
for (var i = 0; i < fruits.length; i++) {
  console.log(fruits[i]);
}
```

#### Méthode `forEach()` :
```javascript
fruits.forEach(function(fruit) {
  console.log(fruit);
});
```

### Tableaux multidimensionnels :

Un tableau peut contenir d'autres tableaux, créant ainsi une structure multidimensionnelle.

```javascript
var matrix = [
  [1, 2, 3],
  [4, 5, 6],
  [7, 8, 9]
];
console.log(matrix[1][2]);  // Accède à l'élément à la deuxième ligne, troisième colonne (6)
```

### Spread Operator (Opérateur de propagation) :

L'opérateur de propagation (`...`) permet de copier un tableau ou de concaténer plusieurs tableaux.

```javascript
var moreFruits = ["Kiwi", "Pêche"];
var allFruits = [...fruits, ...moreFruits];  // Concatène les deux tableaux
```

### Remarques :

- Les tableaux en JavaScript peuvent contenir des éléments de types différents.
- Leur taille peut changer dynamiquement.

---

[...retour au sommaire](../sommaire.md)