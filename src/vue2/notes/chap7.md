# Chapitre 7 : TypeScript avec Vue

[Menu vue](../menu.md)

## Sommaire

1. [Pourquoi utiliser TypeScript avec Vue ?](#pourquoi-utiliser-typescript-avec-vue)  
   1.1 [Avantages : typage statique, meilleure maintenance, réduction des bugs](#avantages-typage-statique-meilleure-maintenance-réduction-des-bugs)  
2. [Configuration TypeScript avec Vue et Vite](#configuration-typescript-avec-vue-et-vite)  
   2.1 [Ajout de TypeScript lors de l’installation avec Vite](#ajout-de-typescript-lors-de-linstallation-avec-vite)  
   2.2 [Configuration dans `tsconfig.json`](#configuration-dans-tsconfigjson)  
   2.3 [Résolution des problèmes fréquents](#résolution-des-problèmes-fréquents)  
3. [Typage des composants](#typage-des-composants)  
   3.1 [Typage des props, événements, méthodes et computed properties](#typage-des-props-événements-méthodes-et-computed-properties)  
   3.2 [Utilisation de `defineComponent`](#utilisation-de-definecomponent)  
4. [Séparation stricte du code](#séparation-stricte-du-code)  
   4.1 [Fichiers distincts pour template, logique et styles](#fichiers-distincts-pour-template-logique-et-styles)  
   4.2 [Comparaison avec la structure de Blazor](#comparaison-avec-la-structure-de-blazor)  
5. [Utilisation d’interfaces et types globaux](#utilisation-dinterfaces-et-types-globaux)  
   5.1 [Définition et réutilisation d’interfaces pour structurer les props et les données](#définition-et-réutilisation-dinterfaces-pour-structurer-les-props-et-les-données)  

---

## 1. Pourquoi utiliser TypeScript avec Vue ?

### 1.1 Avantages : typage statique, meilleure maintenance, réduction des bugs

**TypeScript** est un sur-ensemble de JavaScript qui introduit le **typage statique**. Cela permet de **détecter les erreurs** lors de l’écriture du code, avant même son exécution. Lorsqu’il est utilisé avec Vue, il offre des avantages majeurs :

1. **Typage statique** :  
   Permet de vérifier que les types des variables, des props ou des retours de méthodes sont conformes.

   Exemple :
   ```typescript
   let compteur: number = 10;
   compteur = 'texte'; // Erreur détectée au moment de l'écriture
   ```

2. **Maintenance facilitée** :  
   Un typage explicite rend le code plus **facile à comprendre** pour les développeurs travaillant en équipe ou reprenant un projet existant.

3. **Réduction des bugs** :  
   Évite les erreurs courantes, comme l’appel de méthodes sur des objets `undefined`.

4. **Documentation implicite** :  
   Le code devient **auto-documenté**, car les types décrivent les attentes et les retours des fonctions.

---

## 2. Configuration TypeScript avec Vue et Vite

### 2.1 Ajout de TypeScript lors de l’installation avec Vite

Vite permet de démarrer un projet Vue avec TypeScript dès la création :

1. **Créer un projet Vite avec TypeScript** :

   ```bash
   npm create vite@latest my-vue-ts-app
   ```

2. **Sélectionner l’option TypeScript** lors de la configuration :
   
   ```plaintext
   Select a framework:
   » Vue
     Vue + TypeScript
   ```

3. **Installer les dépendances** :
   
   ```bash
   cd my-vue-ts-app
   npm install
   ```

---

### 2.2 Configuration dans `tsconfig.json`

Une fois TypeScript installé, un fichier `tsconfig.json` est généré. Ce fichier est crucial pour configurer TypeScript dans votre projet.

#### Exemple de `tsconfig.json` :

```json
{
  "compilerOptions": {
    "target": "ESNext",                  // Utiliser les dernières fonctionnalités JS
    "module": "ESNext",                  // Modules ES pour un support moderne
    "strict": true,                      // Activer le mode strict pour un typage sûr
    "jsx": "preserve",                   // Support JSX si nécessaire
    "esModuleInterop": true,             // Compatibilité avec les modules CommonJS
    "skipLibCheck": true,                // Ignore la vérification des types de bibliothèques
    "forceConsistentCasingInFileNames": true,
    "moduleResolution": "node",          // Résolution des modules basée sur Node.js
    "types": ["vite/client"]             // Types spécifiques à Vite
  },
  "include": ["src/**/*.ts", "src/**/*.d.ts", "src/**/*.tsx", "src/**/*.vue"]
}
```

#### Options importantes :
- **`strict`** : Active un ensemble de règles strictes pour garantir un typage sûr.
- **`include`** : Définit les fichiers inclus dans la compilation TypeScript.
- **`types`** : Ajoute des définitions spécifiques à Vite pour une meilleure compatibilité.

---

### 2.3 Résolution des problèmes fréquents

1. **Erreur `Cannot find module` pour les fichiers `.vue`** :  
   Assurez-vous que votre fichier `tsconfig.json` inclut bien les fichiers `.vue` :

   ```json
   "include": ["src/**/*.vue"]
   ```

2. **Support des modules Node.js** :  
   Si des erreurs apparaissent lors de l’utilisation de modules Node, vérifiez l’option `moduleResolution` :

   ```json
   "moduleResolution": "node"
   ```

---

## 3. Typage des composants

### 3.1 Typage des props, événements, méthodes et computed properties

Avec TypeScript, chaque aspect d’un composant Vue peut être typé précisément.

#### Typage des props :

```typescript
import { defineComponent, PropType } from 'vue';

export default defineComponent({
  props: {
    titre: {
      type: String as PropType<string>, // Typage explicite
      required: true
    },
    compteur: {
      type: Number as PropType<number>,
      default: 0
    }
  }
});
```

#### Typage des méthodes :

```typescript
setup() {
  const afficherMessage = (message: string): void => {
    console.log(message);
  };

  return { afficherMessage };
}
```

---

### 3.2 Utilisation de `defineComponent`

`defineComponent` est essentiel pour gérer les types des props et des méthodes dans un composant Vue.

#### Exemple :

```typescript
import { defineComponent } from 'vue';

export default defineComponent({
  props: {
    titre: String
  },
  setup(props) {
    console.log(props.titre); // Type vérifié
    return {};
  }
});
```

---

## 4. Séparation stricte du code

### 4.1 Fichiers distincts pour template, logique et styles

Dans les **grands projets**, il est conseillé de séparer le code en plusieurs fichiers pour une meilleure organisation.

#### Exemple de séparation stricte :

##### **1. Template : `MonComposant.vue`**

```vue
<template>
  <div>
    <h1>{{ titre }}</h1>
    <button @click="incrementer">Incrémenter</button>
  </div>
</template>

<script src="./MonComposant.logic.ts" setup lang="ts"></script>
<style src="./MonComposant.style.css" scoped></style>
```

##### **2. Logique : `MonComposant.logic.ts`**

```typescript
import { ref } from 'vue';

const titre = ref('Bienvenue !');
const incrementer = () => console.log('Incrémenté !');

export { titre, incrementer };
```

##### **3. Styles : `MonComposant.style.css`**

```css
h1 {
  color: blue;
}
button {
  background-color: lightgray;
}
```

---

### 4.2 Comparaison avec la structure de Blazor

En **Blazor** :
- **`.razor`** : Contient le **template**.
- **`.cs`** : Gère la **logique**.

En **Vue avec TypeScript** :
- **`.vue`** : Gère le **template**.
- **`.ts`** : Contient la **logique**.
- **`.css`** ou **`.scss`** : Stocke les **styles**.

---

## 5. Utilisation d’interfaces et types globaux

### 5.1 Définition et réutilisation d’interfaces pour structurer les props et les données

Les **interfaces TypeScript** permettent de structurer et réutiliser les types.

#### Exemple d’interface :

```typescript
interface Utilisateur {
  id: number;
  nom: string;
  email: string;
}

setup() {
  const utilisateur: Utilisateur = {
    id: 1,
    nom: 'Jean Dupont',
    email: 'jean.dupont@example.com'
  };

  return { utilisateur };
}
```

#### Types globaux :

Pour éviter de répéter les interfaces, elles peuvent être définies globalement dans un fichier `.d.ts` :

```typescript
// global.d

.ts
declare global {
  interface Produit {
    id: number;
    nom: string;
    prix: number;
  }
}

export {};
```

---

[Menu vue](../menu.md)