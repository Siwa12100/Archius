# JSON.stringify()

[...retour au sommaire](../sommaire.md)

---

La méthode statique `JSON.stringify()` convertit une valeur JavaScript en une chaîne JSON.

## Utilisation basique

```javascript
const obj = { name: "John", age: 30, city: "New York" };
const jsonString = JSON.stringify(obj);
console.log(jsonString);
// Résultat : '{"name":"John","age":30,"city":"New York"}'
```

Dans cet exemple, un objet JavaScript est converti en une chaîne JSON à l'aide de `JSON.stringify()`.

## Personnaliser avec la fonction de remplacement

```javascript
const user = {
  username: "john_doe",
  password: "secret",
  isAdmin: false,
};

const jsonString = JSON.stringify(user, (key, value) => {
  if (key === "password") {
    return undefined; // Ne pas inclure le champ "password"
  }
  return value; // Sinon, utilise la valeur par défaut
});

console.log(jsonString);
// Résultat : '{"username":"john_doe","isAdmin":false}'
```

La fonction de remplacement permet de personnaliser le processus de sérialisation. Dans cet exemple, le champ "password" est exclu de la sortie JSON.

## Gérer l'espacement pour une meilleure lisibilité

```javascript
const person = { name: "Alice", age: 25, city: "Wonderland" };
const jsonString = JSON.stringify(person, null, 2);
console.log(jsonString);
// Résultat avec indentation : 
// {
//   "name": "Alice",
//   "age": 25,
//   "city": "Wonderland"
// }
```

En spécifiant un nombre pour le paramètre `space`, vous pouvez ajouter une indentation à la chaîne JSON résultante pour une meilleure lisibilité.

## Utiliser la méthode `toJSON()` de l'objet

```javascript
const book = {
  title: "The Great Gatsby",
  author: "F. Scott Fitzgerald",
  toJSON: function () {
    return { bookTitle: this.title, bookAuthor: this.author };
  },
};

const jsonString = JSON.stringify(book);
console.log(jsonString);
// Résultat : '{"bookTitle":"The Great Gatsby","bookAuthor":"F. Scott Fitzgerald"}'
```

En définissant la méthode `toJSON()` dans l'objet, vous pouvez contrôler le processus de sérialisation.

## Gérer les références circulaires

```javascript
const circularRef = { prop: "I am circular" };
circularRef.circular = circularRef;

try {
  JSON.stringify(circularRef);
} catch (error) {
  console.error(error.message);
  // Résultat : "TypeError: Converting circular structure to JSON"
}
```

`JSON.stringify()` génère une erreur si elle rencontre une référence circulaire. Gérer les références circulaires nécessite des solutions plus avancées.

Ces exemples montrent différentes utilisations de `JSON.stringify()` avec des options pour personnaliser la sérialisation selon les besoins.

---

[...retour au sommaire](../sommaire.md)
