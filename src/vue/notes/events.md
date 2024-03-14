# Events

[...retour au sommaire](../sommaire.md)

---

En Vue.js, l'émission et la réception d'événements nous permettent une communication efficace entre composants, particulièrement du composant enfant vers le parent.

## Émission d'Événements par le Composant Enfant

Nos composants enfants peuvent émettre des événements vers leurs parents en utilisant la méthode `this.$emit`. Cette méthode permet de signaler une action ou de transmettre des données au composant parent.

**Syntaxe de base :**

```javascript
this.$emit('nom-de-levenement', donnee);
```

- `nom-de-levenement` : une chaîne de caractères qui identifie l'événement.
- `donnee` (optionnelle) : la donnée à envoyer au parent. On peut passer plusieurs données en les séparant par des virgules.

**Exemple :**

Dans un composant enfant, supposons qu'un bouton, lorsqu'il est cliqué, doive informer le composant parent :

```vue
<template>
  <button @click="envoyerEvenement">Clique moi</button>
</template>

<script>
export default {
  methods: {
    envoyerEvenement() {
      this.$emit('evenementClic', 'Bonjour du composant enfant!');
    }
  }
}
</script>
```

## Réception des Événements dans le Composant Parent

On écoute les événements émis par nos enfants en utilisant la directive `v-on` sur l'instance du composant enfant, dans notre template. On peut aussi utiliser le raccourci `@` pour `v-on`.

**Syntaxe de base :**

```html
<ComposantEnfant @nom-de-levenement="gestionnaireEvenement"></ComposantEnfant>
```

- `ComposantEnfant` : le nom du composant enfant.
- `nom-de-levenement` : le nom de l'événement émis par l'enfant, en kebab-case.
- `gestionnaireEvenement` : la méthode dans le composant parent qui sera appelée lorsque l'événement est reçu.

**Exemple :**

Voici comment nous pouvons écouter l'événement émis par l'enfant :

```vue
<template>
  <ComposantEnfant @evenement-clic="traiterClic"></ComposantEnfant>
</template>

<script>
import ComposantEnfant from './ComposantEnfant.vue';

export default {
  components: {
    ComposantEnfant
  },
  methods: {
    traiterClic(message) {
      alert(message);
    }
  }
}
</script>
```

Dans cet exemple, lorsque le bouton dans `ComposantEnfant` est cliqué, `ComposantEnfant` émet `evenementClic` avec un message. Le composant parent écoute cet événement via `@evenement-clic="traiterClic"` et déclenche la méthode `traiterClic`, affichant le message dans une alerte.


## Gestionnaires d'Événements en Ligne (Inline Handlers)

En plus des gestionnaires de méthodes, on peut utiliser des gestionnaires d'événements en ligne pour exécuter du JavaScript directement lorsqu'un événement est déclenché.

**Exemple d'utilisation :**

```html
<button @click="compte++">Ajouter 1</button>
<p>Le compte est : {{ compte }}</p>
```

Cette approche est pratique pour des cas simples mais est généralement évitée pour des logiques plus complexes.

### Modificateurs d'Événements

Vue.js fournit des modificateurs d'événements pour faciliter la gestion des événements sans avoir à gérer les détails des événements du DOM dans les méthodes.

- **.stop** - `@click.stop` arrête la propagation de l'événement.
- **.prevent** - `@submit.prevent` empêche l'action par défaut (utile pour les formulaires).
- **.capture** - `@click.capture` utilise le mode de capture pour l'écouteur d'événements.
- **.self** - `@click.self` s'assure que l'événement a été déclenché par l'élément lui-même et non par un enfant.
- **.once** - `@click.once` écoute l'événement une seule fois.
- **.passive** - `@scroll.passive` indique au navigateur que vous ne prévoyez pas d'annuler l'événement scroll.

**Exemples :**

```html
<!-- Stoppe la propagation -->
<a @click.stop="faireCeci"></a>

<!-- Empêche l'action par défaut -->
<form @submit.prevent="soumettreFormulaire"></form>

<!-- Modificateurs chaînés -->
<a @click.stop.prevent="faireCela"></a>

<!-- Écoute uniquement les événements déclenchés sur cet élément -->
<div @click.self="faireCela">...</div>
```

### Modificateurs de Touches

Pour les événements clavier, Vue.js permet l'utilisation de modificateurs spécifiques aux touches pour écouter des combinaisons de touches spécifiques.

**Exemples :**

```html
<!-- Appeler `soumettre` quand `Enter` est pressé -->
<input @keyup.enter="soumettre" />
```

### Modificateurs de Boutons de Souris

Vue.js permet également de restreindre certains gestionnaires d'événements aux clics de boutons de souris spécifiques.

```html
<!-- Écouteur pour le clic droit -->
<div @click.right="handleRightClick">Clic droit ici</div>
```

### Accès à l'Argument de l'Événement dans les Gestionnaires en Ligne

On peut accéder à l'argument de l'événement dans un gestionnaire en ligne en utilisant la variable spéciale `$event`.

```html
<button @click="warn('Formulaire non soumis', $event)">
  Soumettre
</button>
```

---

[...retour au sommaire](../sommaire.md)
