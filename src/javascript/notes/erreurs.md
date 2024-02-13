# Objets Error en JavaScript

[...retour au sommaire](../sommaire.md)

---

Les objets **Error** sont utilisés pour gérer les erreurs d'exécution en JavaScript. Ils fournissent des informations utiles pour identifier et traiter les problèmes dans le code.

## Types d'erreur

- **EvalError:** Erreur liée à la fonction globale `eval()`.
- **RangeError:** Erreur survenant quand une variable numérique ou un paramètre est en dehors de sa plage de validité.
- **ReferenceError:** Erreur survenant lors du déréférencement d'une référence invalide.
- **SyntaxError:** Erreur de syntaxe.
- **TypeError:** Erreur survenant quand une variable ou un paramètre n'est pas d'un type valide.
- **URIError:** Erreur survenant quand des paramètres invalides sont passés à `encodeURI()` ou `decodeURI()`.
- **AggregateError:** Erreur regroupant différentes erreurs en une seule, par exemple avec `Promise.any()`.
- **InternalError (Non-standard):** Erreur se produisant en cas d'erreur interne dans le moteur JavaScript.

## Constructeur

Le constructeur **Error()** crée un nouvel objet Error.

## Propriétés des instances

- **`message`:** Message décrivant l'erreur.
- **`name`:** Nom de l'erreur.

## Méthodes statiques

- **`Error.captureStackTrace()`:** Fonction non-standard qui crée la propriété `stack` d'une instance de Error.

La propriété `stack` d'une instance de l'objet `Error` en JavaScript contient une trace de la pile d'appels (stack trace) au moment où l'erreur a été créée. La trace de la pile d'appels est une liste des appels de fonctions qui ont été effectués jusqu'au point où l'erreur a été lancée.

La propriété `stack` est très utile pour le débogage, car elle fournit des informations sur la séquence d'appels de fonctions qui ont conduit à l'erreur. Cela aide les développeurs à identifier l'endroit précis dans le code où l'erreur s'est produite.

Voici un exemple d'utilisation de la propriété `stack` :

```javascript
try {
  // Simuler une erreur
  throw new Error("Une erreur s'est produite !");
} catch (e) {
  // Afficher la trace de la pile d'appels
  console.log(e.stack);
}
```

Le résultat pourrait ressembler à ceci (le format peut varier selon le moteur JavaScript) :

```
Error: Une erreur s'est produite !
    at <nom de la fonction ou du script> (<emplacement dans le code>)
    at <nom de la fonction ou du script> (<emplacement dans le code>)
    at <nom de la fonction ou du script> (<emplacement dans le code>)
    // ... autres appels de fonctions dans la trace de la pile d'appels
```

Chaque ligne de la trace de la pile d'appels indique où la fonction a été appelée et à quelle position dans le code source. Cela aide les développeurs à comprendre le contexte entourant l'erreur.

## Exemples d'utilisation

### Déclenchement d'une erreur générique

```javascript
try {
  throw new Error("Oups !");
} catch (e) {
  console.log(`${e.name} : ${e.message}`);
}
```

### Gestion d'une erreur spécifique

```javascript
try {
  toto.truc();
} catch (e) {
  if (e instanceof EvalError || e instanceof RangeError) {
    console.error(`${e.name} : ${e.message}`);
  } else {
    throw e;
  }
}
```

### Distinguer des erreurs semblables

```javascript
function faireTruc() {
  try {
    echecUneFacon();
  } catch (err) {
    throw new Error("Echoue d'une certaine façon", { cause: err });
  }
  try {
    echecAutreFacon();
  } catch (err) {
    throw new Error("Echoue d'une autre façon", { cause: err });
  }
}

try {
  faireTruc();
} catch (err) {
  switch (err.message) {
    case "Echoue d'une certaine façon":
      gererUneFacon(err.cause);
      break;
    case "Echoue d'une autre façon":
      gererUneAutreFacon(err.cause);
      break;
  }
}
```

### Types d'erreur personnalisés

```javascript
class ErreurSpecifique extends Error {
  constructor(toto = "truc", ...params) {
    super(...params);
    if (Error.captureStackTrace) {
      Error.captureStackTrace(this, ErreurSpecifique);
    }
    this.name = "ErreurSpecifique";
    this.toto = toto;
    this.date = new Date();
  }
}

try {
  throw new ErreurSpecifique("truc", "trucMessage");
} catch (e) {
  console.error(e.name); // ErreurSpecifique
  console.error(e.toto); // truc
  console.error(e.message); // trucMessage
  console.error(e.stack); // stacktrace
}
```

### Types d'erreur personnalisés

```javascript
function ErreurSpecifique(toto, message, fileName, lineNumber) {
  var instance = new Error(message, fileName, lineNumber);
  instance.name = "ErreurSpecifique";
  instance.toto = toto;
  Object.setPrototypeOf(instance, Object.getPrototypeOf(this));
  if (Error.captureStackTrace) {
    Error.captureStackTrace(instance, ErreurSpecifique);
  }
  return instance;
}

ErreurSpecifique.prototype = Object.create(Error.prototype, {
  constructor: {
    value: Error,
    enumerable: false,
    writable: true,
    configurable: true,
  },
});

if (Object.setPrototypeOf) {
  Object.setPrototypeOf(ErreurSpecifique, Error);
} else {
  ErreurSpecifique.__proto__ = Error;
}

try {
  throw new ErreurSpecifique("truc", "trucMessage");
} catch (e) {
  console.error(e.name); // ErreurSpecifique
  console.error(e.toto); // truc
  console.error(e.message); // trucMessage
}
```

---

[...retour au sommaire](../sommaire.md)