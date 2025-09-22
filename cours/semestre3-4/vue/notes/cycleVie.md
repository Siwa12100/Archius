# Cycle de vie

[...retour au sommaire](../sommaire.md)

---

Vue.js offre un système de gestion de cycle de vie pour ses composants, permettant d'exécuter du code à différents moments de la vie d'un composant, depuis sa création jusqu'à sa destruction. 

## Hooks de Création

### beforeCreate

- **Quand est-il appelé ?** Immédiatement après l'initialisation de l'instance.
- **Utilisation :** Ce hook est appelé avant que l'instance n'ait initialisé ses réactivités, événements ou le moindre cycle de vie. C'est rare de l'utiliser, car très peu de choses sont définies à ce moment-là.

### created

- **Quand est-il appelé ?** Après que l'instance ait fini de traiter les propriétés réactives, les méthodes, et les watchers, mais avant que le montage dans le DOM soit effectué.
- **Utilisation :** Parfait pour initialiser des données, envoyer des requêtes pour récupérer des données, ou pour des actions ne nécessitant pas l'accès au DOM ou à des enfants montés, puisque ces derniers ne sont pas encore disponibles.

## Hooks de Montage

### beforeMount

- **Quand est-il appelé ?** Juste avant que le composant soit monté dans le DOM.
- **Utilisation :** Utile pour des actions juste avant la création du DOM, mais dans la plupart des cas, vous utiliserez plutôt `created` ou `mounted`.

### mounted

- **Quand est-il appelé ?** Après que le composant a été monté dans le DOM.
- **Utilisation :** Idéal pour des opérations qui nécessitent l'accès au DOM ou pour exécuter du code qui doit s'exécuter une fois le composant monté, comme le lancement d'une requête API ou l'initialisation d'une bibliothèque externe.

## Hooks de Mise à Jour

### beforeUpdate

- **Quand est-il appelé ?** Avant la mise à jour du DOM en réaction à la modification des données réactives.
- **Utilisation :** Permet d'accéder à l'état du DOM avant sa mise à jour, utile pour obtenir l'état avant la modification.

### updated

- **Quand est-il appelé ?** Après que le DOM a été mis à jour.
- **Utilisation :** S'utilise pour effectuer des actions après que le DOM a été mis à jour en réponse à la modification des données. À utiliser avec précaution pour éviter les boucles infinies de mise à jour.

## Hooks de Démontage

### beforeUnmount

- **Quand est-il appelé ?** Juste avant que le composant soit démonté et détruit.
- **Utilisation :** Utile pour effectuer des tâches de nettoyage, comme arrêter des timers ou détruire des bibliothèques externes qui sont liées au composant.

### unmounted

- **Quand est-il appelé ?** Après que le composant a été démonté du DOM.
- **Utilisation :** Idéal pour confirmer que le composant a été complètement démonté, nettoyer ou terminer des tâches démarrées dans `mounted`.

### Autres Hooks Importants

### watch

- **Utilisation :** Bien que pas un hook de cycle de vie à proprement parler, `watch` permet d'observer des changements sur les données réactives et d'exécuter du code en réponse.

### computed

- **Utilisation :** Similaire à `watch`, les propriétés calculées (`computed`) réagissent aux changements des données qu'elles observent et recalculent leur valeur de manière paresseuse.

### Exemple d'Utilisation des Hooks

```vue
<template>
  <div>{{ message }}</div>
</template>

<script>
export default {
  data() {
    return {
      message: 'Bonjour Vue!'
    };
  },
  beforeCreate() {
    console.log("L'instance est en cours d'initialisation.");
  },
  created() {
    console.log("L'instance a fini son initialisation.");
  },
  beforeMount() {
    console.log("Le composant est sur le point d'être monté.");
  },
  mounted() {
    console.log("Le composant a été monté.");
  },
  beforeUpdate() {
    console.log("Le composant est sur le point d'être mis à jour.");
```

---

[...retour au sommaire](../sommaire.md)