# Chapitre 4 : Événements personnalisés

[Menu vue](../menu.md)

## Sommaire

1. [Création d’événements](#création-dévévénements)  
   1.1 [Définir et émettre des événements](#définir-et-émettre-des-événements)  
2. [Bus d’événements globaux](#bus-dévévénements-globaux)  
   2.1 [Communication entre composants indépendants](#communication-entre-composants-indépendants)  

---

## 1. Création d’événements

Les événements personnalisés permettent à un composant enfant de communiquer avec son composant parent. Contrairement aux **props** qui permettent de transmettre des données du parent vers l’enfant, les événements permettent le flux inverse : de l’enfant au parent.

### 1.1 Définir et émettre des événements

Pour qu’un composant enfant envoie une information ou signale une action au parent, il doit **émettre un événement**. Vue fournit pour cela la méthode `this.$emit()`.

#### Étapes :
1. **Définir l’événement dans l’enfant** : Utiliser `this.$emit` pour envoyer un événement.
2. **Écouter l’événement dans le parent** : Utiliser `v-on` ou son alias `@`.

#### Exemple simple

##### Composant enfant :

```javascript
export default {
  template: `
    <button @click="notifierParent">Cliquez-moi</button>
  `,
  methods: {
    notifierParent() {
      this.$emit('bouton-cliqué', 'Données envoyées au parent');
    }
  }
};
```

##### Composant parent :

```html
<template>
  <enfant @bouton-cliqué="recevoirNotification"></enfant>
</template>

<script>
import Enfant from './Enfant.vue';

export default {
  components: { Enfant },
  methods: {
    recevoirNotification(données) {
      console.log('Événement reçu depuis l’enfant :', données);
    }
  }
};
</script>
```

Dans cet exemple :
- Le composant **Enfant** émet l’événement `bouton-cliqué` lorsqu’un bouton est cliqué.
- Le composant **Parent** écoute cet événement et exécute la méthode `recevoirNotification`.

#### Transmettre des données avec l’événement

L’événement peut également transporter des **données** en paramètre, comme dans l'exemple précédent, où une chaîne de caractères est envoyée (`'Données envoyées au parent'`).

---

## 2. Bus d’événements globaux

### 2.1 Communication entre composants indépendants

Il arrive que des composants doivent communiquer sans avoir de relation directe de parenté. Dans ce cas, il n'est pas possible d'utiliser simplement les props ou les événements locaux. Une solution consiste à utiliser un **bus d’événements globaux**.

#### Qu'est-ce qu'un bus d’événements ?

Un **bus d’événements** est une instance Vue partagée entre plusieurs composants, qui permet :
- **D’émettre des événements** depuis n’importe quel composant.
- **D’écouter ces événements** depuis d’autres composants.

#### Mise en place d’un bus d’événements

1. **Créer une instance de bus** :
   On peut créer une nouvelle instance Vue qui servira de bus.

   ```javascript
   // bus.js
   import { createApp } from 'vue';
   export const EventBus = createApp({});
   ```

2. **Émettre un événement depuis un composant** :

   ```javascript
   import { EventBus } from './bus';

   export default {
     methods: {
       envoyerNotification() {
         EventBus.config.globalProperties.$emit('notification', {
           message: 'Un nouvel événement a été émis'
         });
       }
     }
   };
   ```

3. **Écouter l’événement dans un autre composant** :

   ```javascript
   import { EventBus } from './bus';

   export default {
     created() {
       EventBus.config.globalProperties.$on('notification', (data) => {
         console.log('Notification reçue :', data.message);
       });
     }
   };
   ```

#### Points importants :
- **Centralisation des événements** : Permet de simplifier la communication entre plusieurs composants indépendants.
- **Nettoyage des écouteurs** : Lorsque le composant est détruit, il est important de désinscrire les événements pour éviter les fuites de mémoire :

   ```javascript
   beforeUnmount() {
     EventBus.config.globalProperties.$off('notification');
   }
   ```

---

[Menu vue](../menu.md)