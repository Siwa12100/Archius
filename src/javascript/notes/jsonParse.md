# Méthode JSON.parse()

[...retour au sommaire](../sommaire.md)

---

La méthode `JSON.parse()` en JavaScript analyse une chaîne JSON pour créer la valeur ou l'objet JavaScript correspondant. Optionnellement, une fonction `reviver` peut être fournie pour transformer l'objet résultant avant de le retourner.

## Syntaxe

```javascript
JSON.parse(texte)
JSON.parse(texte, reviver)
```

### Paramètres

- `texte`: La chaîne à analyser en JSON.
- `reviver` (Optionnel): Une fonction spécifiant comment chaque valeur originellement produite par l'analyse est transformée avant d'être renvoyée.

### Valeur de Retour

La méthode renvoie l'objet JavaScript, le tableau, la chaîne, le nombre, le booléen, ou null correspondant au texte JSON donné.

### Exceptions

- `SyntaxError`: Déclenché si la chaîne à analyser n'est pas du JSON valide.

## Exemples

### Utilisation Simple

```javascript
const objetParse = JSON.parse('{"cle": "valeur"}');
console.log(objetParse); // { cle: 'valeur' }
```

### Paramètre Reviver

```javascript
const objetAvecReviver = JSON.parse('{"p": 5}', (cle, valeur) => (typeof valeur === "number" ? valeur * 2 : valeur));
console.log(objetAvecReviver); // { p: 10 }
```

### Utilisation avec Tableau

```javascript
const tableauParse = JSON.parse('[1, 2, 3]');
console.log(tableauParse); // [1, 2, 3]
```

### Gestion d'Erreurs

```javascript
try {
  const objetInvalide = JSON.parse('{"cle": "valeur",}');
} catch (erreur) {
  console.error("Erreur de syntaxe JSON :", erreur.message);
}
```

### Utilisation avec Types Personnalisés

```javascript
class Personne {
  constructor(nom, age) {
    this.nom = nom;
    this.age = age;
  }
}

const personneJSON = '{"nom": "Alice", "age": 30}';
const personne = JSON.parse(personneJSON, (cle, valeur) => (cle === '' ? new Personne(valeur.nom, valeur.age) : valeur));

console.log(personne); // Personne { nom: 'Alice', age: 30 }
```

### Paramètre Reviver Profond

```javascript
const objetProfond = JSON.parse('{"a": {"b": {"c": 42}}}', (cle, valeur) => (typeof valeur === "number" ? valeur * 2 : valeur));
console.log(objetProfond); // { a: { b: { c: 84 } } }
```

---

[...retour au sommaire](../sommaire.md)