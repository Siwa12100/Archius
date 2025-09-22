# Classes en JavaScript

[...retour au sommaire](../sommaire.md)

---

## Déclaration de Classes

- Déclarées avec `class`.
- Corps entre accolades pour définir méthodes et constructeur.

```javascript
class Rectangle {
  constructor(hauteur, largeur) {
    this.hauteur = hauteur;
    this.largeur = largeur;
  }
}
```

## Héritage avec `extends`

- Utilisation de `extends` pour créer une sous-classe.
- `super()` pour appeler le constructeur de la classe parente.

```javascript
class Animal {
  parler() {
    console.log(`${this.nom} fait du bruit.`);
  }
}

class Chien extends Animal {
  parler() {
    super.parler();
    console.log(`${this.nom} aboie.`);
  }
}
```

## Méthodes Statiques

- Définies avec `static`.
- Appelées par rapport à la classe, pas à une instance.

```javascript
class Point {

  constructor(x, y) {
    this.x = x;
    this.y = y;
  }

  static distance(a, b) {
    const dx = a.x - b.x;
    const dy = a.y - b.y;
    return Math.hypot(dx, dy);
  }
}
```

## Utilisation de `super`

- `super` utilisé pour appeler des fonctions de la classe parente.

```javascript
class Lion extends Chat {
  parler() {
    super.parler();
    console.log(`${this.nom} rugit.`);
  }
}
```

## Mix-ins (Modèles de Classes)

- Émulés avec des fonctions qui prennent une classe parente en entrée.
- Permettent de fournir des fonctionnalités à une classe.

```javascript
let calculetteMixin = (Base) =>
  class extends Base {
    calc() {}
  };

let aleatoireMixin = (Base) =>
  class extends Base {
    randomiseur() {}
  };

class Truc extends calculetteMixin(aleatoireMixin(Toto)) {}
```

## Déclarations de Champs (Expérimental)

- Fonctionnalité expérimentale pour déclarer des champs publics et privés.
- Champs privés ne peuvent être lus ou modifiés que depuis le corps de la classe.

```javascript
class Rectangle {
  #hauteur = 0;
  #largeur;
  constructor(hauteur, largeur) {
    this.#hauteur = hauteur;
    this.#largeur = largeur;
  }
}
```

---

[...retour au sommaire](../sommaire.md)