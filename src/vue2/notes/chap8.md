# Chapitre 8 : Gestion de l’asynchrone en Vue avec TypeScript

[Menu vue](../menu.md)

## Sommaire

1. [Introduction à l’asynchrone en Vue](#introduction-à-lasynchrone-en-vue)  
   1.1 [Pourquoi l’asynchrone est important ?](#pourquoi-lasynchrone-est-important)  
   1.2 [Principales utilisations de l’asynchrone](#principales-utilisations-de-lasynchrone)  
2. [Gestion des appels asynchrones dans `setup`](#gestion-des-appels-asynchrones-dans-setup)  
   2.1 [Appels API et utilisation de `await` dans `setup`](#appels-api-et-utilisation-de-await-dans-setup)  
   2.2 [Gestion des états avec les hooks de cycle de vie](#gestion-des-états-avec-les-hooks-de-cycle-de-vie)  
3. [Hooks de cycle de vie pour l’asynchrone](#hooks-de-cycle-de-vie-pour-lasynchrone)  
   3.1 [`onMounted` : Initialiser des appels API](#onmounted-initialiser-des-appels-api)  
   3.2 [`onBeforeUnmount` : Nettoyage des ressources](#onbeforeunmount-nettoyage-des-ressources)  
4. [Réactivité avec `watch` et `watchEffect`](#réactivité-avec-watch-et-watcheffect)  
   4.1 [Utilisation de `watch` pour suivre les changements asynchrones](#utilisation-de-watch-pour-suivre-les-changements-asynchrones)  
   4.2 [Utilisation de `watchEffect` pour des dépendances automatiques](#utilisation-de-watcheffect-pour-des-dépendances-automatiques)  
5. [Gestion des erreurs et des états de chargement](#gestion-des-erreurs-et-des-états-de-chargement)  
   5.1 [Gestion complète des états avec TypeScript](#gestion-complète-des-états-avec-typescript)  
   5.2 [Typage des méthodes asynchrones](#typage-des-méthodes-asynchrones)  
6. [Comparaison avec Blazor](#comparaison-avec-blazor)  

---

## 1. Introduction à l’asynchrone en Vue

### 1.1 Pourquoi l’asynchrone est important ?

Dans une application moderne, **l’asynchrone** est essentiel pour :
- **Récupérer des données distantes** (via des APIs).
- **Attendre des réponses longues** sans bloquer l’interface.
- **Mettre à jour l'interface en temps réel** après une opération longue.

En **Vue avec TypeScript**, l’asynchrone permet de gérer ces opérations tout en conservant une **sécurité de type** et une **réactivité optimale**.

---

### 1.2 Principales utilisations de l’asynchrone

1. **Appels API** : 
   Récupération de données depuis un serveur.
2. **Temporisations** : 
   Simuler des délais ou des chargements progressifs.
3. **Nettoyage des ressources** : 
   Par exemple, fermer des connexions WebSocket.

---

## 2. Gestion des appels asynchrones dans `setup`

### 2.1 Appels API et utilisation de `await` dans `setup`

La fonction `setup` ne supporte pas directement `async`. Cependant, il est possible d’appeler des méthodes asynchrones au sein de cette fonction.

#### Exemple d’appel API :

```typescript
import { ref, onMounted } from 'vue';

export default {
  setup() {
    const utilisateurs = ref<string[]>([]);
    const erreur = ref<string | null>(null);

    const chargerUtilisateurs = async (): Promise<void> => {
      try {
        const response = await fetch('https://api.example.com/utilisateurs');
        utilisateurs.value = await response.json();
      } catch (e) {
        erreur.value = 'Erreur lors du chargement des utilisateurs.';
      }
    };

    onMounted(() => {
      chargerUtilisateurs();
    });

    return { utilisateurs, erreur };
  }
};
```

#### Explications :
- **`ref`** rend les données réactives.
- **`onMounted`** garantit que l'appel API se fait après le montage du composant.
- **`await`** permet d’attendre la réponse de l’API avant de continuer.

---

### 2.2 Gestion des états avec les hooks de cycle de vie

Dans Vue, les hooks de cycle de vie comme **`onMounted`** ou **`onBeforeUnmount`** permettent de gérer efficacement les tâches asynchrones.

---

## 3. Hooks de cycle de vie pour l’asynchrone

### 3.1 `onMounted` : Initialiser des appels API

Ce hook est idéal pour **initialiser des données** ou **connecter des ressources** après que le composant a été monté dans le DOM.

#### Exemple d'usage :

```typescript
import { ref, onMounted } from 'vue';

export default {
  setup() {
    const produits = ref<string[]>([]);

    onMounted(async () => {
      const response = await fetch('https://api.example.com/produits');
      produits.value = await response.json();
    });

    return { produits };
  }
};
```

---

### 3.2 `onBeforeUnmount` : Nettoyage des ressources

Pour **nettoyer les ressources**, comme fermer une connexion ou arrêter un timer, on utilise `onBeforeUnmount`.

#### Exemple avec un timer :

```typescript
import { ref, onMounted, onBeforeUnmount } from 'vue';

export default {
  setup() {
    const compteur = ref(0);
    let intervalId: number;

    onMounted(() => {
      intervalId = setInterval(() => {
        compteur.value++;
      }, 1000);
    });

    onBeforeUnmount(() => {
      clearInterval(intervalId);
    });

    return { compteur };
  }
};
```

---

## 4. Réactivité avec `watch` et `watchEffect`

### 4.1 Utilisation de `watch` pour suivre les changements asynchrones

`watch` permet d’observer une ou plusieurs variables réactives et d’exécuter une fonction lorsqu’elles changent.

#### Exemple avec un champ de recherche :

```typescript
import { ref, watch } from 'vue';

export default {
  setup() {
    const recherche = ref<string>('');
    const resultats = ref<string[]>([]);

    watch(recherche, async (nouvelleValeur) => {
      if (nouvelleValeur) {
        const response = await fetch(`https://api.example.com/search?q=${nouvelleValeur}`);
        resultats.value = await response.json();
      } else {
        resultats.value = [];
      }
    });

    return { recherche, resultats };
  }
};
```

---

### 4.2 Utilisation de `watchEffect` pour des dépendances automatiques

`watchEffect` observe automatiquement toutes les **dépendances réactives** dans sa fonction.

#### Exemple simple :

```typescript
import { ref, watchEffect } from 'vue';

export default {
  setup() {
    const compteur = ref(0);

    watchEffect(() => {
      console.log(`Compteur mis à jour : ${compteur.value}`);
    });

    return { compteur };
  }
};
```

---

## 5. Gestion des erreurs et des états de chargement

### 5.1 Gestion complète des états avec TypeScript

Lors d'opérations asynchrones, il est important de gérer plusieurs **états** : chargement, succès et erreur.

#### Exemple :

```typescript
import { ref, onMounted } from 'vue';

export default {
  setup() {
    const utilisateurs = ref<string[]>([]);
    const chargement = ref<boolean>(false);
    const erreur = ref<string | null>(null);

    const chargerUtilisateurs = async (): Promise<void> => {
      chargement.value = true;
      try {
        const response = await fetch('https://api.example.com/utilisateurs');
        utilisateurs.value = await response.json();
      } catch (e) {
        erreur.value = 'Erreur lors du chargement.';
      } finally {
        chargement.value = false;
      }
    };

    onMounted(() => {
      chargerUtilisateurs();
    });

    return { utilisateurs, chargement, erreur };
  }
};
```

### 5.2 Typage des méthodes asynchrones

Les méthodes asynchrones peuvent être typées pour garantir qu'elles retournent des **promesses de types spécifiques**.

```typescript
const fetchUtilisateurs = async (): Promise<string[]> => {
  const response = await fetch('https://api.example.com/utilisateurs');
  return response.json();
};
```

---

## 6. Comparaison avec Blazor

En **Blazor** :
- **`OnInitializedAsync`** permet d'effectuer des tâches asynchrones après l'initialisation.
- **`await`** est utilisé pour attendre des opérations longues.

En **Vue** :
- **`setup` + `onMounted`** remplissent ce rôle.
- Les appels asynchrones se font avec **`await`** dans des hooks ou des watchers.

---

[Menu vue](../menu.md)
