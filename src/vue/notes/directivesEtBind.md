# Directives et liaisons de données

[...retour au sommaire](../sommaire.md)

---

Les directives en Vue.js sont des indicateurs spéciaux dans le code HTML qui indiquent au framework de faire quelque chose à l'élément DOM. Parmi les directives les plus utilisées pour manipuler les données et les éléments d'interface, on trouve `v-bind`, `v-model`, `v-for`, et `v-if`. Explorons chacune d'elles en détail.

## v-bind

La directive `v-bind` est utilisée pour lier dynamiquement un ou plusieurs attributs, ou une propriété de composant, aux données de l'instance Vue. Cela signifie que lorsque les données changent, l'attribut lié sera automatiquement mis à jour pour refléter le changement.

**Usage :**

```html
<!-- Lier dynamiquement l'attribut `href` -->
<a v-bind:href="url">Visitez notre site</a>
```

- `url` est une donnée de l'instance Vue. Si `url` change, l'attribut `href` de l'élément `<a>` sera mis à jour en conséquence.

**Raccourci :**

Vue.js offre un raccourci pour `v-bind` en utilisant simplement `:`.

```html
<a :href="url">Visitez notre site</a>
```

## v-model

`v-model` crée une liaison bidirectionnelle sur les éléments de formulaire (`<input>`, `<textarea>`, et `<select>`), ce qui permet de lier les champs de formulaire aux données de l'instance Vue. Tout changement dans les données sera immédiatement reflété dans le formulaire, et tout changement dans le formulaire mettra à jour les données.

**Usage :**

```html
<input v-model="message">
```

- Chaque fois que l'utilisateur modifie la valeur de l'`<input>`, la propriété `message` de l'instance Vue est mise à jour. De même, si la valeur de `message` change, l'élément `<input>` reflète cette modification.

## v-for

`v-for` est une directive utilisée pour rendre une liste d'éléments basée sur un tableau de données. Avec `v-for`, on peut construire des données dynamiquement répétitives comme des listes ou des tableaux dans le DOM.

**Usage :**

```html
<ul>
  <li v-for="item in items" :key="item.id">
    {{ item.text }}
  </li>
</ul>
```

- `items` est un tableau de données de l'instance Vue. Chaque élément du tableau est rendu comme un élément `<li>` dans la liste. La propriété `key` est importante pour le tracking des éléments de la liste et doit être unique.

## v-if, v-else-if, v-else

`v-if` est une directive qui conditionnellement rend un bloc si la condition est vraie. `v-else-if` et `v-else` sont des directives complémentaires qui permettent de créer des structures conditionnelles plus complexes.

**Usage :**

```html
<div v-if="type === 'A'">Type A</div>
<div v-else-if="type === 'B'">Type B</div>
<div v-else>Type inconnu</div>
```

- Selon la valeur de la donnée `type` de l'instance Vue, un des trois `<div>` sera rendu dans le DOM.

Chacune de ces directives offre une méthode puissante et flexible pour lier les données de l'instance Vue aux éléments du DOM, manipuler le contenu dynamiquement, et gérer les conditions et les listes dans vos applications Vue.js.

## v-for en détail

La directive `v-for` de Vue.js est utilisée pour afficher une liste d'éléments en se basant sur un tableau de données. Avec `v-for`, vous pouvez construire des structures de données répétitives comme des listes, des tableaux, ou même des éléments `<template>` pour grouper des fragments de template.

### Syntaxe de Base

```html
<li v-for="item in items">{{ item.text }}</li>
```

Dans cet exemple, `items` est un tableau où chaque élément du tableau est assigné à la variable `item` pendant l'itération. Vue.js répétera l'élément `<li>` pour chaque élément dans le tableau `items`.

### Exemple Complet avec `v-for`

```vue
<template>
  <ul>
    <li v-for="(item, index) in items" :key="item.id">
      {{ index }} - {{ item.text }}
    </li>
  </ul>
</template>

<script>
export default {
  data() {
    return {
      items: [
        { id: 1, text: 'Apprendre Vue.js' },
        { id: 2, text: 'Explorer v-for' },
        { id: 3, text: 'Maîtriser Vue.js' }
      ]
    };
  }
}
</script>
```

Dans cet exemple, en plus de l'élément `item`, on utilise aussi l'`index` de l'itération actuelle. L'attribut `:key` est crucial pour l'optimisation, car il aide Vue à identifier chaque noeud de manière unique et à réutiliser/retirer les éléments existants de manière efficace lors de la mise à jour de la liste.

## v-if en détail

La directive `v-if` est utilisée pour conditionnellement afficher un élément en fonction de la vérité d'une expression JavaScript. L'élément et ses enfants seront rendus uniquement si l'expression évaluée est vraie.

### Syntaxe de Base

```html
<h1 v-if="awesome">Vue est impressionnant!</h1>
```

Dans cet exemple, le `<h1>` ne sera affiché que si la donnée `awesome` est évaluée à `true`.

### Exemple Complet avec `v-if`, `v-else-if`, `v-else`

```vue
<template>
  <div>
    <button @click="type = 'A'">Type A</button>
    <button @click="type = 'B'">Type B</button>
    <button @click="type = 'C'">Type C</button>
    
    <p v-if="type === 'A'">Option A sélectionnée</p>
    <p v-else-if="type === 'B'">Option B sélectionnée</p>
    <p v-else>Autre Option sélectionnée</p>
  </div>
</template>

<script>
export default {
  data() {
    return {
      type: ''
    };
  }
}
</script>
```

---

[...retour au sommaire](../sommaire.md)
