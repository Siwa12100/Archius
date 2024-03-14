# Création de composants

[...retour au sommaire](../sommaire.md)

---

## Structure d'un Composant

Un composant Vue.js est défini par un objet JavaScript. Voici un exemple basique :

```javascript
// fichier MonComposant.js
const MonComposant = {
    template: `<div>{{ message }}</div>`,
    data() {
        return {
            message: 'Bonjour Vue.js!'
        }
    }
}
```

## Enregistrement de Composants

Les composants peuvent être enregistrés globalement ou localement.

### Enregistrement Global

```javascript
Vue.component('mon-composant-global', MonComposant);
```

### Enregistrement Local

```javascript
// Dans le fichier de votre instance Vue principale, par exemple main.js
new Vue({
    el: '#app',
    components: {
        'mon-composant-local': MonComposant
    }
});
```

## Utilisation de Composants

Après l'enregistrement, vous pouvez utiliser votre composant dans le template HTML de la même manière pour les composants globaux et locaux :

```html
<!-- Dans votre fichier HTML -->
<div id="app">
    <mon-composant-local></mon-composant-local>
    <mon-composant-global></mon-composant-global>
</div>
```

---

[...retour au sommaire](../sommaire.md)