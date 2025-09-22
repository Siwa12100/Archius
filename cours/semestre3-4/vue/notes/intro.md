# Introduction

[...retour au sommaire](../sommaire.md)

---

## Fondamentaux de Vue.js

### Compréhension de Vue.js

Vue.js est un framework JavaScript progressif utilisé pour construire des interfaces utilisateur interactives et dynamiques. Il se concentre sur la vue (d'où son nom), ce qui signifie qu'il gère principalement la partie de notre application concernant l'interface utilisateur. Vue.js est souvent utilisé pour créer des applications à page unique (SPA) ainsi que des interfaces utilisateur complexes.

### Principaux avantages de Vue.js

- **Facilité d'apprentissage :** Vue.js est facile à apprendre, en partie grâce à sa syntaxe simple et concise.
- **Réactivité :** Vue offre une réactivité efficace grâce à son système de liaison de données bidirectionnelles.
- **Composition :** Vue encourage la composition des composants réutilisables, ce qui favorise la modularité du code.
- **Flexibilité :** Vue peut être utilisé de manière incrémentale, ce qui signifie que nous pouvons commencer par l'intégrer dans une petite partie de notre projet existant.

## Instance de Vue

Pour utiliser Vue.js dans une application, nous créons une instance de Vue. Cela se fait en instanciant un objet Vue et en lui passant un objet de configuration qui définit le comportement de l'application. Voici un exemple simple :

```html
<!DOCTYPE html>
<html>
<head>
  <title>Instance Vue</title>
  <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
</head>
<body>

<div id="app">
  {{ message }}
</div>

<script>
// Création d'une instance Vue
var app = new Vue({
  el: '#app', // Élément HTML où Vue sera attaché
  data: {
    message: 'Hello Vue!'
  }
})
</script>

</body>
</html>
```

Dans cet exemple, nous créons une instance Vue avec `new Vue({})`. L'objet de configuration contient `el` qui indique à Vue où monter l'instance (dans ce cas, l'élément avec l'ID "app") et `data` qui contient les données utilisées par l'instance. `message` est une propriété de données que nous utilisons dans notre modèle.

## Réactivité dans Vue

Vue utilise un système de réactivité pour détecter automatiquement les changements de données et mettre à jour l'interface utilisateur en conséquence. Lorsque les données utilisées par une instance Vue changent, Vue détecte ces changements et met à jour le DOM de manière efficace et réactive.

Revenons à l'exemple précédent. Si nous changeons la valeur de `message`, le DOM sera automatiquement mis à jour pour refléter ce changement, sans que nous ayons à manipuler directement le DOM :

```javascript
app.message = 'Bonjour Vue!';
```

Après l'exécution de cette ligne de code, le texte affiché dans l'élément avec l'ID "app" sera automatiquement mis à jour pour afficher "Bonjour Vue!" au lieu de "Hello Vue!". C'est le système de réactivité de Vue en action.

## Créer rapidement un projet avec Vue

On peut utiliser la commande `npm init vue@latest`.

---

[...retour au sommaire](../sommaire.md)