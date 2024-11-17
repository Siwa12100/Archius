# Vue.js : Options API vs Composition API

[Menu vue](../menu.md)

## Introduction

Vue.js propose deux approches pour structurer un composant : **Options API** et **Composition API**. Ces deux styles diffèrent principalement dans la manière de définir la logique, gérer le cycle de vie, et manipuler la réactivité.

---

## 1. **Options API**

L'**Options API** organise un composant autour d'un objet qui déclare explicitement différentes options comme `data`, `methods`, et les hooks de cycle de vie.

### Structure d'un Composant avec Options API

```vue
<template>
  <div>
    <h1>{{ message }}</h1>
    <button @click="increment">Incrémenter</button>
    <p>Compteur : {{ count }}</p>
  </div>
</template>

<script lang="ts">
export default {
  data() {
    return {
      message: 'Bonjour depuis Options API !',
      count: 0,
    };
  },
  methods: {
    increment() {
      this.count++;
    },
  },
  created() {
    console.log('Composant créé :', this.message);
  },
};
</script>

<style scoped>
h1 {
  color: green;
}
</style>
```

### Points Clés de l'Options API

1. **`data`** :
   - Définit les **données réactives** du composant. 
   - Les propriétés déclarées dans `data` sont automatiquement rendues réactives.

2. **Réactivité automatique** :
   - Pas besoin d’utiliser `ref` ou `reactive`.
   - Vue suit automatiquement les modifications des propriétés définies dans `data`.

3. **Hooks de Cycle de Vie** :
   - Utilise `created`, `mounted`, etc. pour intégrer la logique au bon moment dans le cycle de vie du composant.

---

## 2. **Composition API**

La **Composition API** repose sur des **fonctions** pour organiser la logique. Toute la logique du composant est définie dans une **fonction `setup`**, qui est exécutée au moment de la création du composant.

### Structure d'un Composant avec Composition API

```vue
<template>
  <div>
    <h1>{{ message }}</h1>
    <button @click="increment">Incrémenter</button>
    <p>Compteur : {{ count }}</p>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue';

// Déclare des données réactives
const message = ref('Bonjour depuis Composition API !');
const count = ref(0);

// Méthode pour incrémenter
function increment() {
  count.value++;
}

// Hook de cycle de vie
onMounted(() => {
  console.log('Composant monté avec le message :', message.value);
});
</script>

<style scoped>
h1 {
  color: blue;
}
</style>
```

---

## 3. **Réactivité : `ref` vs `reactive`**

### **`ref` : Déclare des variables réactives simples**

- **`ref`** est utilisé pour créer une **variable réactive** contenant une valeur primitive ou un objet unique.
- La valeur réactive est accessible via la propriété **`.value`**.

#### Exemple :
```ts
import { ref } from 'vue';

const count = ref(0);
console.log(count.value); // 0

count.value++; // Modifie la valeur
console.log(count.value); // 1
```

- **Pour les valeurs primitives**, Vue suit les modifications via `.value`.
- **Pour les objets**, `ref` encapsule l'objet, mais tu dois toujours manipuler la valeur avec `.value`.

---

### **`reactive` : Rendre un objet entier réactif**

- **`reactive`** est utilisé pour créer un **objet réactif**, qui rend **toutes les propriétés de l'objet réactives**.

#### Exemple :
```ts
import { reactive } from 'vue';

const state = reactive({
  count: 0,
  user: {
    name: 'John',
    age: 25,
  },
});

console.log(state.count); // 0
state.count++;
console.log(state.count); // 1

state.user.name = 'Jane'; // Modifie directement les propriétés réactives
console.log(state.user.name); // Jane
```

### **Différences entre `ref` et `reactive`**

| **Aspect**             | **`ref`**                                              | **`reactive`**                                          |
|------------------------|---------------------------------------------------------|---------------------------------------------------------|
| **Utilisation**         | Pour une valeur primitive ou un objet encapsulé         | Pour un objet contenant plusieurs propriétés             |
| **Accès à la valeur**   | Via **`.value`** pour les primitives ou objets          | Directement via les propriétés                          |
| **Réactivité**          | La valeur entière est réactive                          | Chaque propriété de l'objet est réactive individuellement |
| **Conversion implicite**| Les objets sont "déstructurés" automatiquement dans le template | Aucun besoin spécial dans le template                 |

---

## 4. **Cycle de Vie : Comparaison**

| **Options API**        | **Composition API**    | **Description**                                   |
|------------------------|------------------------|---------------------------------------------------|
| `created()`            | `setup()`              | Appelé après la création de l'instance            |
| `mounted()`            | `onMounted()`          | Appelé après le montage dans le DOM               |
| `beforeUpdate()`       | `onBeforeUpdate()`     | Appelé avant la mise à jour du DOM                |
| `updated()`            | `onUpdated()`          | Appelé après la mise à jour du DOM                |
| `beforeUnmount()`      | `onBeforeUnmount()`    | Appelé juste avant la destruction                 |
| `unmounted()`          | `onUnmounted()`        | Appelé après la destruction                       |

### Exemple : Hooks de cycle de vie en Composition API

```ts
import { onMounted, onUpdated, onUnmounted } from 'vue';

onMounted(() => {
  console.log('Composant monté');
});

onUpdated(() => {
  console.log('Composant mis à jour');
});

onUnmounted(() => {
  console.log('Composant détruit');
});
```

---

## 5. **Injection de Dépendances (DI)**

### Options API : Fournir et Injecter

#### Fournir une dépendance (`provide`) :
```ts
export default {
  provide() {
    return {
      myService: new MyService(),
    };
  },
};
```

#### Injecter une dépendance (`inject`) :
```ts
export default {
  inject: ['myService'],
  mounted() {
    this.myService.doSomething();
  },
};
```

### Composition API : Fournir et Injecter

#### Fournir une dépendance :
```ts
import { provide } from 'vue';

provide('myService', new MyService());
```

#### Injecter une dépendance :
```ts
import { inject } from 'vue';

const myService = inject('myService');
myService?.doSomething();
```

---

## 6. **Comparaison : Options API vs Composition API**

| **Aspect**        | **Options API**                                  | **Composition API**                          |
|-------------------|--------------------------------------------------|---------------------------------------------|
| **Définition**     | Basée sur des options (`data`, `methods`, etc.)  | Basée sur des fonctions via `setup`         |
| **Réactivité**     | Automatique via `data`                           | `ref` pour les primitives, `reactive` pour les objets |
| **Cycle de Vie**   | `created`, `mounted`, etc.                       | `onMounted`, `onUpdated`, etc.              |
| **Réutilisation**  | Moins flexible                                   | Très flexible avec des composables          |
| **DI**            | `provide` / `inject` via objets                  | `provide` / `inject` via fonctions          |

---

[Menu vue](../menu.md)