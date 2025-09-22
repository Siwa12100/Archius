# Formulaires

[...retour au sommaire](../sommaire.md)

---

## Création d'un Formulaire

Pour créer un formulaire en Vue.js, vous commencez par définir le template HTML du formulaire, puis vous utilisez `v-model` pour lier les champs de formulaire aux données de votre instance Vue.

### Exemple de Base

```vue
<template>
  <form @submit.prevent="soumettreFormulaire">
    <label for="nom">Nom:</label>
    <input id="nom" v-model="formulaire.nom">

    <label for="email">Email:</label>
    <input id="email" type="email" v-model="formulaire.email">

    <button type="submit">Soumettre</button>
  </form>
</template>

<script>
export default {
  data() {
    return {
      formulaire: {
        nom: '',
        email: ''
      }
    };
  },
  methods: {
    soumettreFormulaire() {
      console.log(this.formulaire);
      // Traitement du formulaire, par exemple envoi à un serveur
    }
  }
}
</script>
```

Dans cet exemple, `v-model` est utilisé pour créer une liaison bidirectionnelle entre les éléments `<input>` et les propriétés `nom` et `email` dans l'objet `formulaire`. 

Cela signifie que lorsque l'utilisateur saisit des données dans les champs, les propriétés correspondantes dans les données de Vue sont automatiquement mises à jour, et vice-versa.

L'événement `@submit.prevent` sur le `<form>` empêche le comportement de soumission par défaut du formulaire (qui rafraîchirait la page) et appelle la méthode `soumettreFormulaire` à la place.

### Validation de Formulaire

La validation de formulaire est cruciale pour vérifier que les données entrées par l'utilisateur sont valides avant de les traiter ou de les envoyer à un serveur. Vue.js ne fournit pas de système de validation intégré, mais vous pouvez facilement implémenter votre propre logique de validation ou utiliser une bibliothèque tierce.

### Exemple avec Validation Simple

```vue
<template>
  <form @submit.prevent="soumettreFormulaire">
    <label for="age">Age:</label>
    <input id="age" type="number" v-model.number="formulaire.age">
    <p v-if="validation.age">L'âge doit être supérieur à 18</p>

    <button type="submit" :disabled="!estValide">Soumettre</button>
  </form>
</template>

<script>
export default {
  data() {
    return {
      formulaire: {
        age: 0
      },
      validation: {
        age: false
      }
    };
  },
  computed: {
    estValide() {
      this.validation.age = this.formulaire.age < 18;
      return !this.validation.age;
    }
  },
  methods: {
    soumettreFormulaire() {
      if(this.estValide) {
        console.log("Formulaire soumis", this.formulaire);
        // Traitement du formulaire
      }
    }
  }
}
</script>
```

Dans cet exemple, une validation simple est effectuée pour s'assurer que l'âge entré est supérieur à 18. La propriété `estValide` dans `computed` est utilisée pour calculer dynamiquement si le formulaire est valide ou non, basée sur les critères de validation définis. La validation est affichée via un paragraphe conditionnel (`v-if="validation.age"`), et le bouton de soumission est désactivé (`:disabled="!estValide"`) tant que le formulaire n'est pas valide.

### Conclusion

Vue.js simplifie considérablement le processus de création, de liaison et de validation de formulaires, rendant la gestion des données de formulaire fluide et réactive. 

Avec la liaison bidirectionnelle `v-model` et les méthodes d'instance Vue pour le traitement des soumissions, vous avez un contrôle total sur le comportement de vos formulaires et sur la validation des données utilisateurs. 

Pour des cas de validation plus complexes, envisagez d'utiliser des bibliothèques de validation spécifiques à Vue comme Vuelidate ou VeeValidate.

---

[...retour au sommaire](../sommaire.md)