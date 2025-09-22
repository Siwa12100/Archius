# Chapitre 6 : API Composition

[Menu vue](../menu.md)

## Sommaire

1. [Introduction à l’API Composition](#introduction-à-lapi-composition)  
   1.1 [Qu’est-ce que l’API Composition ?](#quest-ce-que-lapi-composition)  
   1.2 [Pourquoi utiliser l’API Composition ?](#pourquoi-utiliser-lapi-composition)  
   1.3 [Utilisation de `setup`](#utilisation-de-setup)  
2. [Injection de dépendances](#injection-de-dépendances)  
   2.1 [Concepts de `provide` et `inject`](#concepts-de-provide-et-inject)  
   2.2 [Exemple pratique de partage de données](#exemple-pratique-de-partage-de-données)  
   2.3 [Gestion avancée avec des valeurs réactives](#gestion-avancée-avec-des-valeurs-réactives)  
3. [Gestion des données et formulaires](#gestion-des-données-et-formulaires)  
   3.1 [Liaison de données et méthodes de formulaire](#liaison-de-données-et-méthodes-de-formulaire)  
4. [Propriétés calculées et surveillées](#propriétés-calculées-et-surveillées)  
   4.1 [Utilisation de `computed`](#utilisation-de-computed)  
   4.2 [Surveillance des données avec `watch`](#surveillance-des-données-avec-watch)  
5. [Méthodes avancées](#méthodes-avancées)  
   5.1 [Création de fonctions utilitaires réutilisables](#création-de-fonctions-utilitaires-réutilisables)  

---

## 1. Introduction à l’API Composition

### 1.1 Qu’est-ce que l’API Composition ?

L’**API Composition** est un modèle de programmation introduit avec Vue 3. Contrairement aux approches classiques, elle offre une manière **modulaire et flexible** de structurer le code des composants Vue. Elle est conçue pour répondre aux besoins des applications modernes qui peuvent devenir complexes.

#### Cadre et principes :
- **Centralisation** : Toutes les fonctionnalités d'un composant (données, méthodes, propriétés calculées, watchers) sont regroupées dans une seule fonction appelée `setup`.
- **Modularité** : Favorise la création de **fonctions composables** pour partager des fonctionnalités entre composants.
- **Clarté** : Facilite la lecture et la maintenance de composants complexes.

L’API Composition n’est pas obligatoire mais est particulièrement adaptée aux projets :
- Avec **beaucoup de logique complexe**.
- Nécessitant une **réutilisabilité accrue** des fonctions et des structures de données.

---

### 1.2 Pourquoi utiliser l’API Composition ?

L’API Composition est utile pour plusieurs raisons :

1. **Réutilisabilité accrue** : Les fonctions définies dans `setup` peuvent être facilement extraites et réutilisées dans d'autres composants.
2. **Meilleure organisation** : Idéal pour des composants ayant plusieurs responsabilités.
3. **Réduction du couplage** : Favorise un découplage fort entre les différentes parties de l'application.
4. **Intégration avec d’autres bibliothèques** : Facilite l'intégration avec des solutions comme Pinia pour la gestion d'état.

---

### 1.3 Utilisation de `setup`

La fonction `setup` est le **cœur** de l'API Composition. Elle est exécutée avant le cycle de vie du composant et permet de définir des **données réactives**, des **méthodes**, des **propriétés calculées** et des **watchers**.

#### Exemple simple :

```javascript
import { ref } from 'vue';

export default {
  setup() {
    const message = ref('Bonjour Vue 3 !');

    const changerMessage = () => {
      message.value = 'Message modifié !';
    };

    return { message, changerMessage };
  }
};
```

- **`ref`** permet de créer des données réactives.
- **`return`** expose les données et méthodes au template.

Dans le template :

```vue
<template>
  <div>
    <p>{{ message }}</p>
    <button @click="changerMessage">Changer le message</button>
  </div>
</template>
```

---

## 2. Injection de dépendances

### 2.1 Concepts de `provide` et `inject`

Dans Vue, les **données globales** peuvent être partagées entre un composant parent et ses enfants en utilisant les méthodes `provide` et `inject`. 

- **`provide`** : Défini dans le parent, il permet d'exposer une donnée ou une fonction.
- **`inject`** : Utilisé dans les composants enfants pour consommer les données ou fonctions fournies.

#### Utilisation typique :
- **Partage de configurations globales**.
- **Communication entre composants sans passer par des props**.

---

### 2.2 Exemple pratique de partage de données

#### Composant parent avec `provide` :

```javascript
import { provide } from 'vue';

export default {
  setup() {
    provide('message', 'Bonjour depuis le parent !');
  }
};
```

Le parent rend accessible la chaîne de caractères `'Bonjour depuis le parent !'` à ses enfants.

#### Composant enfant avec `inject` :

```javascript
import { inject } from 'vue';

export default {
  setup() {
    const message = inject('message');
    return { message };
  }
};
```

Dans le template de l’enfant :

```vue
<template>
  <p>{{ message }}</p>
</template>
```

L'enfant affiche désormais le message fourni par le parent.

---

### 2.3 Gestion avancée avec des valeurs réactives

Les valeurs passées via `provide` peuvent aussi être **réactives**.

#### Exemple avec une valeur réactive :

```javascript
import { provide, ref } from 'vue';

export default {
  setup() {
    const compteur = ref(0);
    provide('compteur', compteur);

    setInterval(() => {
      compteur.value++;
    }, 1000);
  }
};
```

Dans un composant enfant :

```javascript
import { inject } from 'vue';

export default {
  setup() {
    const compteur = inject('compteur');
    return { compteur };
  }
};
```

L'enfant pourra afficher un compteur qui s'incrémente automatiquement.

---

## 3. Gestion des données et formulaires

### 3.1 Liaison de données et méthodes de formulaire

Avec l'API Composition, les **formulaires dynamiques** sont facilement gérés à l'aide de `ref` et `v-model`.

#### Exemple :

```vue
<template>
  <form @submit.prevent="soumettreFormulaire">
    <label>Nom :</label>
    <input v-model="nom" type="text">

    <label>Email :</label>
    <input v-model="email" type="email">

    <button type="submit">Envoyer</button>
  </form>
</template>

<script>
import { ref } from 'vue';

export default {
  setup() {
    const nom = ref('');
    const email = ref('');

    const soumettreFormulaire = () => {
      console.log(`Nom : ${nom.value}, Email : ${email.value}`);
    };

    return { nom, email, soumettreFormulaire };
  }
};
</script>
```

---

## 4. Propriétés calculées et surveillées

### 4.1 Utilisation de `computed`

Les **propriétés calculées** dérivent des valeurs à partir des données réactives.

#### Exemple :

```javascript
import { ref, computed } from 'vue';

export default {
  setup() {
    const compteur = ref(0);
    const estElevé = computed(() => compteur.value > 10);

    return { compteur, estElevé };
  }
};
```

---

### 4.2 Surveillance des données avec `watch`

La méthode `watch` surveille les changements de données et déclenche des actions en conséquence.

#### Exemple :

```javascript
import { ref, watch } from 'vue';

export default {
  setup() {
    const recherche = ref('');
    const resultats = ref([]);

    watch(recherche, (nouveauTerme) => {
      resultats.value = rechercherDansBaseDeDonnées(nouveauTerme);
    });

    return { recherche, resultats };
  }
};
```

---

## 5. Méthodes avancées

### 5.1 Création de fonctions utilitaires réutilisables

Les **fonctions composables** permettent de réutiliser des blocs de logique dans différents composants.

#### Exemple :

```javascript
// useCompteur.js
import { ref } from 'vue';

export function useCompteur() {
  const compteur = ref(0);
  const incrementer = () => compteur.value++;
  return { compteur, incrementer };
}
```

---

[Menu vue](../menu.md)