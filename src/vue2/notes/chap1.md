# Chapitre 1 : Premiers pas sans chaîne d’outils

[Menu vue](../menu.md)

## Sommaire

1. [Introduction à Vue sans chaîne d’outils](#introduction-à-vue-sans-chaîne-doutils)  
   1.1 [Utilisation d’un CDN](#utilisation-dun-cdn)  
   1.2 [Création d’un fichier `index.html` simple](#création-dun-fichier-indexhtml-simple)  
2. [Les bases de Vue.js](#les-bases-de-vuejs)  
   2.1 [Création d’une instance Vue](#création-dune-instance-vue)  
   2.2 [Lier des données à l’interface](#lier-des-données-à-linterface)  
3. [Directives de boucle](#directives-de-boucle)  
   3.1 [Utilisation de `v-for` pour générer des listes dynamiques](#utilisation-de-v-for-pour-générer-des-listes-dynamiques)  
   3.2 [Gestion des clés uniques avec `:key`](#gestion-des-clés-uniques-avec-key)  
4. [Gestion des événements](#gestion-des-événements)  
   4.1 [Utilisation de `v-on` ou `@`](#utilisation-de-v-on-ou-)  
   4.2 [Introduction à `preventDefault`](#introduction-à-preventdefault)  
5. [Styles dynamiques](#styles-dynamiques)  
   5.1 [Liaison conditionnelle de classes avec `v-bind:class`](#liaison-conditionnelle-de-classes-avec-v-bindclass)  
   5.2 [Gestion des styles dynamiques](#gestion-des-styles-dynamiques)  
6. [Propriétés calculées](#propriétés-calculées)  
   6.1 [Comparaison avec les méthodes](#comparaison-avec-les-méthodes)  
   6.2 [Avantages des propriétés calculées](#avantages-des-propriétés-calculées)  
7. [Introduction aux composants](#introduction-aux-composants)  
   7.1 [Qu'est-ce qu'un composant ?](#quest-ce-quun-composant)  
   7.2 [Création et utilisation d’un composant de base](#création-et-utilisation-dun-composant-de-base)  
8. [Flux de données](#flux-de-données)  
   8.1 [Unidirectionalité des données](#unidirectionalité-des-données)  
   8.2 [Communication parent-enfant avec des props](#communication-parent-enfant-avec-des-props)  

---

## 1. Introduction à Vue sans chaîne d’outils

### 1.1 Utilisation d’un CDN

Vue.js permet de démarrer sans aucune installation complexe en utilisant un CDN. Cela signifie que vous pouvez inclure Vue directement via une balise `<script>` dans votre fichier HTML. Voici un exemple simple d'intégration :

#### Exemple de code

```html
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Exemple Vue.js CDN</title>
</head>
<body>
  <div id="app">
    {{ message }}
  </div>

  <!-- Inclure Vue.js via CDN -->
  <script src="https://unpkg.com/vue@3"></script>
  <script>
    const app = Vue.createApp({
      data() {
        return {
          message: 'Bonjour, Vue.js via CDN !'
        };
      }
    });
    app.mount('#app');
  </script>
</body>
</html>
```

Dans cet exemple, nous avons inclus Vue à l’aide d’un CDN et avons monté une simple application Vue sur un élément HTML avec l’ID `app`.

### 1.2 Création d’un fichier `index.html` simple

Pour mieux comprendre, créons un fichier `index.html` avec une structure plus complète. Ce fichier servira de point de départ pour toutes les expérimentations Vue.

#### Exemple détaillé

```html
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Vue.js Débutant</title>
  <style>
    .highlight {
      color: red;
      font-weight: bold;
    }
  </style>
</head>
<body>
  <div id="app">
    <h1>{{ title }}</h1>
    <p>{{ description }}</p>
  </div>

  <script src="https://unpkg.com/vue@3"></script>
  <script>
    const app = Vue.createApp({
      data() {
        return {
          title: 'Bienvenue dans Vue.js',
          description: 'Ceci est votre première application Vue.'
        };
      }
    });
    app.mount('#app');
  </script>
</body>
</html>
```

Ce fichier montre comment utiliser des styles CSS de base avec Vue et comment afficher des données dynamiques dans plusieurs éléments.

---

## 2. Les bases de Vue.js

### 2.1 Création d’une instance Vue

La première étape dans toute application Vue est de créer une instance de l'application avec `Vue.createApp`. Cette instance contient l’état (data), les méthodes, et d'autres options de configuration.

#### Exemple détaillé

```javascript
const app = Vue.createApp({
  data() {
    return {
      message: 'Bienvenue dans Vue.js !'
    };
  },
  methods: {
    changeMessage() {
      this.message = 'Message modifié !';
    }
  }
});
app.mount('#app');
```

### 2.2 Lier des données à l’interface

Vue utilise une syntaxe appelée **liaison de données** avec des accolades doubles `{{ }}` pour afficher dynamiquement des valeurs dans le DOM.

#### Exemple d'affichage de données dynamiques

```html
<div id="app">
  <p>Message initial : {{ message }}</p>
  <button @click="changeMessage">Modifier le message</button>
</div>
```

Dans cet exemple, le bouton modifie le message affiché via une méthode définie dans l’instance Vue.

---

## 3. Directives de boucle

### 3.1 Utilisation de `v-for` pour générer des listes dynamiques

La directive `v-for` permet d'itérer sur des tableaux pour générer des éléments HTML dynamiquement.

#### Exemple détaillé

```html
<ul>
  <li v-for="(fruit, index) in fruits" :key="index">
    {{ index + 1 }}. {{ fruit }}
  </li>
</ul>
```

Dans l'instance Vue :

```javascript
data() {
  return {
    fruits: ['Pomme', 'Banane', 'Orange']
  };
}
```

### 3.2 Gestion des clés uniques avec `:key`

Lors de l’utilisation de `v-for`, l’attribut `:key` est essentiel pour améliorer les performances et éviter des comportements inattendus.

#### Exemple amélioré avec `:key`

```html
<li v-for="(item, idx) in items" :key="item.id">
  {{ item.name }}
</li>
```

---

## 4. Gestion des événements

### 4.1 Utilisation de `v-on` ou `@`

Pour gérer des événements utilisateur comme les clics, Vue propose la directive `v-on` ou son alias `@`.

#### Exemple

```html
<button @click="handleClick">Cliquez-moi</button>
```

Dans l'instance Vue :

```javascript
methods: {
  handleClick() {
    alert('Bouton cliqué !');
  }
}
```

### 4.2 Introduction à `preventDefault`

Pour empêcher le comportement par défaut d’un formulaire par exemple :

```html
<form @submit.prevent="onSubmit">
  <input type="text" v-model="inputValue">
  <button type="submit">Envoyer</button>
</form>
```

---

## 5. Styles dynamiques

### 5.1 Liaison conditionnelle de classes avec `v-bind:class`

Appliquez des classes conditionnellement :

```html
<div :class="{ active: isActive }">Contenu conditionnel</div>
```

### 5.2 Gestion des styles dynamiques

Ajoutez des styles en fonction de variables :

```html
<div :style="{ color: textColor, fontSize: fontSize + 'px' }">
  Texte stylé dynamiquement
</div>
```

---

## 6. Propriétés calculées

### 6.1 Comparaison avec les méthodes

Les **propriétés calculées** (`computed`) sont une fonctionnalité clé de Vue.js permettant de dériver des données à partir de l'état existant. Contrairement aux méthodes, elles sont **mémorisées** (cachées) jusqu'à ce que leurs dépendances changent.

#### Exemple d’utilisation d’une méthode

```javascript
methods: {
  reversedMessage() {
    return this.message.split('').reverse().join('');
  }
}
```

Dans le template :

```html
<p>Message inversé (via méthode) : {{ reversedMessage() }}</p>
```

Chaque fois que `reversedMessage()` est appelée, Vue réévalue la fonction.

#### Exemple avec une propriété calculée

```javascript
computed: {
  reversedMessage() {
    return this.message.split('').reverse().join('');
  }
}
```

Dans le template :

```html
<p>Message inversé (via propriété calculée) : {{ reversedMessage }}</p>
```

### 6.2 Avantages des propriétés calculées

1. **Performance** : Les propriétés calculées ne sont recalculées que si leurs dépendances changent.
2. **Lisibilité** : Plus facile à comprendre et à maintenir que l’appel direct à des méthodes répétées.

---

## 7. Introduction aux composants

### 7.1 Qu'est-ce qu'un composant ?

Les composants sont des blocs de construction réutilisables dans Vue.js. Chaque composant encapsule son propre **template**, **logique** et **styles**, ce qui permet de créer des interfaces modulaires et maintenables.

#### Points clés :
- **Encapsulation** : Chaque composant a son propre état.
- **Réutilisabilité** : Utilisez un composant plusieurs fois avec des données différentes.

### 7.2 Création et utilisation d’un composant de base

Définissons un composant simple appelé `MessageComponent`.

#### Exemple : Déclaration d’un composant

```javascript
app.component('MessageComponent', {
  template: `
    <div>
      <h3>{{ title }}</h3>
      <p>{{ message }}</p>
    </div>
  `,
  data() {
    return {
      title: 'Composant Message',
      message: 'Ceci est un message dans un composant réutilisable.'
    };
  }
});
```

Dans le template principal :

```html
<message-component></message-component>
<message-component></message-component>
```

Chaque instance du composant aura son propre état, rendant la réutilisation très simple.

---

## 8. Flux de données

### 8.1 Unidirectionalité des données

Vue utilise un flux de données **unidirectionnel** :  
- Les **props** sont passées des **parents** aux **enfants**.  
- Les **événements** sont émis des **enfants** vers les **parents**.

Cela garantit une structure de données claire et prévisible.

#### Exemple de flux de données parent-enfant

```javascript
app.component('ChildComponent', {
  props: ['message'],
  template: `<p>Message du parent : {{ message }}</p>`
});
```

Dans le parent :

```html
<child-component :message="parentMessage"></child-component>
```

### 8.2 Communication parent-enfant avec des props

Les **props** permettent de transmettre des données des composants parents aux enfants.

#### Définir des props dans le composant enfant

```javascript
props: {
  message: {
    type: String,
    required: true
  }
}
```

Cela garantit que le parent doit fournir une chaîne de caractères comme prop.

#### Exemple de composant parent

```javascript
data() {
  return {
    parentMessage: 'Hello depuis le parent'
  };
}
```

Dans le template du parent :

```html
<child-component :message="parentMessage"></child-component>
```

---

[Menu vue](../menu.md)