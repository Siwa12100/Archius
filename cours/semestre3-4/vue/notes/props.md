# Props

[...retour au sommaire](../sommaire.md)

---

## Définition des Props

Les props (abréviation de propriétés) sont des attributs personnalisés que l'on définit sur des composants. Lorsqu'on utilise un composant enfant dans un composant parent, on peut lui passer des données sous forme de props. Ces données deviennent ensuite disponibles dans le composant enfant, ce qui lui permet de les utiliser pour son rendu ou sa logique interne.

### Comment Définir et Utiliser des Props

Pour utiliser des props dans un composant Vue.js, nous suivons ces étapes :

1. **Définir les Props dans le Composant Enfant** : On commence par définir les props dans l'option `props` du composant enfant. Cela déclare les props que le composant attend de recevoir de ses parents.

2. **Passer les Props depuis le Composant Parent** : Ensuite, lorsqu'on utilise le composant enfant dans le template du composant parent, on spécifie les props comme attributs du composant enfant, en passant les valeurs souhaitées.

3. **Utiliser les Props dans le Composant Enfant** : Dans le composant enfant, on peut accéder aux valeurs des props reçues pour les utiliser dans le template, les méthodes, les calculs, etc.

### Exemple

Imaginons que nous avons un composant enfant `EnfantComponent` qui a besoin d'afficher un message. Le message doit lui être passé par le composant parent.

#### Composant Enfant (EnfantComponent)

```javascript
const EnfantComponent = {
  props: ['message'], // Définition de la prop 'message'
  template: `<div>{{ message }}</div>` // Utilisation de la prop dans le template
};
```

#### Composant Parent

```html
<div id="app">
  <!-- Utilisation du composant enfant avec une prop -->
  <enfant-component message="Bonjour depuis le parent!"></enfant-component>
</div>
```

#### Initialisation de Vue.js

```javascript
// Définition du composant enfant
Vue.component('enfant-component', EnfantComponent);

// Création de l'instance Vue pour le composant parent
new Vue({
  el: '#app'
});
```

Dans cet exemple, `EnfantComponent` définit une prop nommée `message`. Le composant parent utilise `<enfant-component>` et lui passe une valeur pour `message` via un attribut HTML. Dans `EnfantComponent`, nous accédons à cette valeur en utilisant `{{ message }}` dans le template pour l'afficher.

### Validation des Props

Vue.js nous permet également de valider les types des props passées aux composants, en fournissant un objet avec des types spécifiques plutôt qu'un tableau simple. Cela améliore la robustesse et la prévisibilité de nos composants.

```javascript
const EnfantComponent = {
  props: {
    // Définition plus détaillée de la prop 'message'
    message: {
      type: String,
      required: true
    }
  },
  template: `<div>{{ message }}</div>`
};
```

Dans cet exemple, nous avons ajouté une validation pour s'assurer que `message` est toujours une chaîne de caractères (`String`) et qu'elle est requise (`required: true`).

---

[...retour au sommaire](../sommaire.md)