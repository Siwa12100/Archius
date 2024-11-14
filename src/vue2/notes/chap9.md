# Chapitre 9 : Directives fondamentales et optimisation des conditions en Vue

[Menu vue](../menu.md)

## Sommaire

1. [Directives conditionnelles](#directives-conditionnelles)  
   1.1 [Utilisation de `v-if`, `v-else`, `v-else-if`](#utilisation-de-v-if-v-else-v-else-if)  
   1.2 [Différences entre `v-if` et `v-show`](#différences-entre-v-if-et-v-show)  
2. [Boucles avancées](#boucles-avancées)  
   2.1 [Approfondissement de `v-for` et clés uniques](#approfondissement-de-v-for-et-clés-uniques)  
3. [Autres directives](#autres-directives)  
   3.1 [Utilisation de `v-once`](#utilisation-de-v-once)  
   3.2 [Utilisation de `v-html` et sécurité](#utilisation-de-v-html-et-sécurité)  
4. [Combinaisons de directives](#combinaisons-de-directives)  
   4.1 [Combinaison de `v-if` et `v-for`](#combinaison-de-v-if-et-v-for)  
5. [Expressions complexes dans les templates](#expressions-complexes-dans-les-templates)  
   5.1 [Conditions ternaires et expressions logiques](#conditions-ternaires-et-expressions-logiques)  
6. [Gestion des erreurs de rendu conditionnel](#gestion-des-erreurs-de-rendu-conditionnel)  
   6.1 [Erreurs courantes avec `v-if` et `v-for`](#erreurs-courantes-avec-v-if-et-v-for)  
   6.2 [Forcer un re-render avec `key` et `v-if`](#forcer-un-re-render-avec-key-et-v-if)  

---

## 1. Directives conditionnelles

### 1.1 Utilisation de `v-if`, `v-else`, `v-else-if`

Les directives conditionnelles permettent de rendre ou masquer des éléments dans le DOM en fonction d'une condition.

#### Exemple de base avec `v-if` et `v-else` :

```vue
<template>
  <div>
    <p v-if="estConnecté">Bienvenue, utilisateur connecté !</p>
    <p v-else>Veuillez vous connecter.</p>
  </div>
</template>

<script setup lang="ts">
const estConnecté = ref(false);
</script>
```

#### `v-else-if` pour ajouter plusieurs conditions :

```vue
<template>
  <div>
    <p v-if="role === 'admin'">Bienvenue, administrateur !</p>
    <p v-else-if="role === 'user'">Bienvenue, utilisateur.</p>
    <p v-else>Accès invité.</p>
  </div>
</template>

<script setup lang="ts">
const role = ref('guest');
</script>
```

### 1.2 Différences entre `v-if` et `v-show`

- **`v-if`** :  
  L'élément est **ajouté ou retiré** du DOM en fonction de la condition.  
  Utile lorsque l'élément doit être complètement retiré.

- **`v-show`** :  
  L'élément est toujours présent dans le DOM, mais sa **visibilité** est contrôlée via `display: none`.  
  Utile pour des basculements fréquents.

#### Comparaison d'utilisation :

```vue
<template>
  <button v-if="chargé">Rafraîchir</button>
  <button v-show="chargé">Rafraîchir</button>
</template>
```

---

## 2. Boucles avancées

### 2.1 Approfondissement de `v-for` et clés uniques

`v-for` est utilisé pour **itérer sur des listes** et **rendre dynamiquement** des éléments.

#### Exemple simple avec une liste :

```vue
<template>
  <ul>
    <li v-for="(utilisateur, index) in utilisateurs" :key="utilisateur.id">
      {{ index + 1 }}. {{ utilisateur.nom }}
    </li>
  </ul>
</template>

<script setup lang="ts">
const utilisateurs = ref([
  { id: 1, nom: 'Alice' },
  { id: 2, nom: 'Bob' },
  { id: 3, nom: 'Charlie' }
]);
</script>
```

#### Pourquoi utiliser `key` ?  
- **`key`** aide Vue à **identifier de manière unique** chaque élément.
- Cela permet d'**optimiser les performances** en mettant à jour uniquement les éléments modifiés.

---

## 3. Autres directives

### 3.1 Utilisation de `v-once`

`v-once` permet de **rendre un élément une seule fois** et d’ignorer les mises à jour réactives.

#### Exemple :

```vue
<template>
  <h1 v-once>{{ titre }}</h1>
</template>

<script setup lang="ts">
const titre = ref('Bienvenue');
titre.value = 'Titre mis à jour'; // Le DOM ne sera pas mis à jour
</script>
```

---

### 3.2 Utilisation de `v-html` et sécurité

`v-html` permet d’**insérer du HTML brut** dans un composant.

#### Exemple :

```vue
<template>
  <div v-html="contenuHtml"></div>
</template>

<script setup lang="ts">
const contenuHtml = ref('<p style="color: red;">Texte rouge</p>');
</script>
```

#### Attention :  
- **XSS (Cross-Site Scripting)** : Évitez d'insérer du HTML non sécurisé ou provenant de sources non fiables.

---

## 4. Combinaisons de directives

### 4.1 Combinaison de `v-if` et `v-for`

La **combinaison directe** de `v-if` et `v-for` peut poser des problèmes de performances ou de lisibilité.

#### Exemple incorrect :

```vue
<template>
  <li v-for="item in liste" v-if="item.visible" :key="item.id">{{ item.nom }}</li>
</template>
```

Ici, la condition `v-if` est évaluée **à chaque itération**, ce qui peut entraîner des ralentissements.

#### Solution recommandée :  
**Filtrer les données** avant l’itération.

```vue
<template>
  <li v-for="item in élémentsVisibles" :key="item.id">{{ item.nom }}</li>
</template>

<script setup lang="ts">
const liste = ref([
  { id: 1, nom: 'Élément 1', visible: true },
  { id: 2, nom: 'Élément 2', visible: false }
]);

const élémentsVisibles = computed(() =>
  liste.value.filter(item => item.visible)
);
</script>
```

---

## 5. Expressions complexes dans les templates

### 5.1 Conditions ternaires et expressions logiques

Vue permet d’utiliser des **expressions ternaires** pour simplifier les conditions directement dans les templates.

#### Exemple simple :

```vue
<template>
  <p>{{ estAdmin ? 'Accès administrateur' : 'Accès utilisateur' }}</p>
</template>

<script setup lang="ts">
const estAdmin = ref(true);
</script>
```

#### Exemple d’expression logique complexe :

```vue
<template>
  <p>
    {{ role === 'admin' ? 'Bienvenue Admin' : role === 'user' ? 'Bienvenue Utilisateur' : 'Accès Invité' }}
  </p>
</template>

<script setup lang="ts">
const role = ref('user');
</script>
```

---

## 6. Gestion des erreurs de rendu conditionnel

### 6.1 Erreurs courantes avec `v-if` et `v-for`

1. **Absence de `key` unique** :
   - Peut provoquer des comportements imprévisibles lors de la mise à jour du DOM.
   
   ```vue
   <template>
     <li v-for="item in liste" v-if="item.actif">{{ item.nom }}</li> <!-- Mauvais -->
   </template>
   ```

2. **État initial incorrect** :
   - Si les données initiales sont mal définies, cela peut entraîner des erreurs silencieuses.

---

### 6.2 Forcer un re-render avec `key` et `v-if`

Pour **forcer Vue à recréer un composant**, utilisez une combinaison de `key` et `v-if`.

#### Exemple :

```vue
<template>
  <Composant :key="id" v-if="visible" />
</template>

<script setup lang="ts">
const id = ref(1);
const visible = ref(true);

function changerComposant() {
  id.value++; // Change la clé pour forcer le re-render
}
</script>
```

---

[Menu vue](../menu.md)