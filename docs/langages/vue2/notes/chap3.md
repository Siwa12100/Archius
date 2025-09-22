# Chapitre 3 : Gestion des données

[Menu vue](../menu.md)

## Sommaire

1. [Cycle de vie des composants](#cycle-de-vie-des-composants)  
   1.1 [Présentation du cycle de vie d’un composant Vue](#présentation-du-cycle-de-vie-dun-composant-vue)  
   1.2 [Utilisation de `created()` pour charger des données](#utilisation-de-created-pour-charger-des-données)  
   1.3 [Autres méthodes de cycle de vie importantes](#autres-méthodes-de-cycle-de-vie-importantes)  
2. [Props et données réactives](#props-et-données-réactives)  
   2.1 [Introduction aux props](#introduction-aux-props)  
   2.2 [Gestion des props non définies et valeurs par défaut](#gestion-des-props-non-définies-et-valeurs-par-défaut)  
   2.3 [Réactivité des données](#réactivité-des-données)  
3. [Formulaires et validation](#formulaires-et-validation)  
   3.1 [Création de formulaires dynamiques](#création-de-formulaires-dynamiques)  
   3.2 [Validation des champs avec des règles personnalisées](#validation-des-champs-avec-des-règles-personnalisées)  
4. [Mise à jour et filtrage des données](#mise-à-jour-et-filtrage-des-données)  
   4.1 [Utilisation des watchers pour réagir aux changements](#utilisation-des-watchers-pour-réagir-aux-changements)  

---

## 1. Cycle de vie des composants

### 1.1 Présentation du cycle de vie d’un composant Vue

Un composant Vue passe par plusieurs **étapes de cycle de vie** depuis sa création jusqu'à sa destruction. Ces étapes permettent de manipuler un composant à des moments précis pour effectuer des tâches spécifiques, comme le chargement de données ou le nettoyage des ressources.

#### Étapes principales du cycle de vie :
1. **Création** : Initialisation du composant.
   - `beforeCreate` : Les données et méthodes ne sont pas encore disponibles.
   - `created` : Les données et méthodes sont initialisées, mais le DOM n’est pas encore rendu.
2. **Montage** : Insertion dans le DOM.
   - `beforeMount` : Avant l'insertion dans le DOM.
   - `mounted` : Une fois le DOM rendu et le composant inséré.
3. **Mise à jour** : Lorsqu'une donnée réactive change.
   - `beforeUpdate` : Avant que le DOM soit mis à jour.
   - `updated` : Après que le DOM soit mis à jour.
4. **Destruction** : Suppression du composant.
   - `beforeUnmount` : Avant que le composant soit retiré du DOM.
   - `unmounted` : Après avoir été retiré.

---

### 1.2 Utilisation de `created()` pour charger des données

La méthode `created()` est souvent utilisée pour **charger des données** ou effectuer des **configurations initiales** avant que le composant ne soit monté dans le DOM.

#### Exemple :

```javascript
export default {
  data() {
    return {
      utilisateurs: []
    };
  },
  created() {
    fetch('https://api.example.com/utilisateurs')
      .then(response => response.json())
      .then(data => {
        this.utilisateurs = data;
      })
      .catch(error => {
        console.error('Erreur de chargement des utilisateurs :', error);
      });
  }
};
```

Dans cet exemple, l'application récupère une liste d'utilisateurs via une API avant que le composant ne soit affiché.

#### Pourquoi utiliser `created()` ?
- **Initialisation des données** : S'assurer que les données sont prêtes avant l'affichage.
- **Optimisation** : Prévenir des rendus inutiles du DOM.

---

### 1.3 Autres méthodes de cycle de vie importantes

1. **`mounted()`** :  
   Utilisé pour accéder au **DOM** après le rendu initial.
   
   Exemple : initialisation d'une bibliothèque tierce (comme un graphique).

   ```javascript
   mounted() {
     const chart = new Chart(this.$refs.chartCanvas, {...});
   }
   ```

2. **`beforeUpdate()` et `updated()`** :  
   Permettent de suivre les changements de données réactives et de vérifier l’impact sur le DOM.
   
3. **`beforeUnmount()` et `unmounted()`** :  
   Utiles pour nettoyer les **écouteurs d'événements** ou **annuler des abonnements**.

---

## 2. Props et données réactives

### 2.1 Introduction aux props

Les **props** sont des données que le **composant parent** transmet à un **composant enfant**. Cela permet de passer des informations spécifiques pour personnaliser le comportement ou l’affichage d’un composant.

#### Exemple d’utilisation des props :

Dans le composant parent :

```html
<enfant :message="parentMessage"></enfant>
```

Dans le composant enfant :

```javascript
export default {
  props: ['message']
};
```

Le composant enfant peut maintenant utiliser la prop `message`.

---

### 2.2 Gestion des props non définies et valeurs par défaut

Pour s'assurer qu'une prop a toujours une valeur, Vue permet de définir des **valeurs par défaut**.

#### Exemple de définition des props avec types et valeurs par défaut :

```javascript
export default {
  props: {
    titre: {
      type: String,
      required: true
    },
    compteur: {
      type: Number,
      default: 0
    }
  }
};
```

- **`titre`** est une prop obligatoire.
- **`compteur`** a une valeur par défaut de `0`.

---

### 2.3 Réactivité des données

Dans Vue, toutes les données déclarées dans `data()` sont **réactives**. Cela signifie que lorsque leur valeur change, le DOM est mis à jour automatiquement.

#### Exemple de réactivité :

```javascript
data() {
  return {
    compteur: 0
  };
},
methods: {
  incrementer() {
    this.compteur++;
  }
}
```

Chaque fois que `incrementer` est appelé, l'interface se met à jour sans intervention manuelle.

---

## 3. Formulaires et validation

### 3.1 Création de formulaires dynamiques

Vue facilite la création de formulaires dynamiques avec la directive `v-model`, qui permet de lier un champ de formulaire à une donnée réactive.

#### Exemple de formulaire :

```html
<form @submit.prevent="soumettreFormulaire">
  <label>Nom :</label>
  <input v-model="nom" type="text" required>

  <label>Email :</label>
  <input v-model="email" type="email" required>

  <button type="submit">Envoyer</button>
</form>
```

Dans l'instance Vue :

```javascript
data() {
  return {
    nom: '',
    email: ''
  };
},
methods: {
  soumettreFormulaire() {
    console.log(`Nom : ${this.nom}, Email : ${this.email}`);
  }
}
```

---

### 3.2 Validation des champs avec des règles personnalisées

Pour valider les champs d’un formulaire, des **conditions personnalisées** peuvent être appliquées. On peut également afficher des messages d’erreur en fonction des règles.

#### Exemple de validation :

```html
<div v-if="erreurs.length">
  <ul>
    <li v-for="erreur in erreurs" :key="erreur">{{ erreur }}</li>
  </ul>
</div>
```

Dans le script :

```javascript
data() {
  return {
    erreurs: [],
    nom: '',
    email: ''
  };
},
methods: {
  soumettreFormulaire() {
    this.erreurs = [];
    
    if (!this.nom) {
      this.erreurs.push('Le nom est requis.');
    }

    if (!this.email.includes('@')) {
      this.erreurs.push('Un email valide est requis.');
    }

    if (!this.erreurs.length) {
      console.log('Formulaire soumis avec succès !');
    }
  }
}
```

---

## 4. Mise à jour et filtrage des données

### 4.1 Utilisation des watchers pour réagir aux changements

Les **watchers** permettent de surveiller des variables spécifiques et d’exécuter du code en réponse à leur changement.

#### Exemple avec `watch` :

```javascript
data() {
  return {
    recherche: '',
    resultats: []
  };
},
watch: {
  recherche(nouveauTerme) {
    this.filtrerResultats(nouveauTerme);
  }
},
methods: {
  filtrerResultats(terme) {
    this.resultats = this.toutesDonnees.filter(item =>
      item.includes(terme)
    );
  }
}
```

---
[Menu vue](../menu.md)