# Chapitre 2 : Utilisation du CLI Vue avec Vite

[Menu vue](../menu.md)

## Sommaire

1. [Pourquoi Vite ?](#pourquoi-vite)  
   1.1 [Qu'est-ce que Vite et Vue ?](#quest-ce-que-vite-et-vue)  
   1.2 [Qu'est-ce qu'un bundler ?](#quest-ce-quun-bundler)  
   1.3 [Différences entre Vite et Webpack](#différences-entre-vite-et-webpack)  
   1.4 [Avantages de Vite : Rapidité et Hot Module Replacement (HMR)](#avantages-de-vite-rapidité-et-hot-module-replacement-hmr)  
2. [Installation de Vite](#installation-de-vite)  
   2.1 [Utilisation de la commande `npm create vite@latest`](#utilisation-de-la-commande-npm-create-vitelatest)  
   2.2 [Configuration pour un projet Vue](#configuration-pour-un-projet-vue)  
3. [Structure d’un projet Vite](#structure-dun-projet-vite)  
   3.1 [Dossiers publics et sources (`main.ts`, `App.vue`)](#dossiers-publics-et-sources-maints-appvue)  
4. [Configuration avancée](#configuration-avancée)  
   4.1 [Fichiers de configuration `vite.config.ts`](#fichiers-de-configuration-viteconfigts)  
   4.2 [Plugins Vite pour Vue (vue-router, Vuex/Pinia)](#plugins-vite-pour-vue-vue-router-vuexpinia)  

---

## 1. Pourquoi Vite ?

### 1.1 Qu'est-ce que Vite et Vue ?

#### Vue.js : Un framework JavaScript progressif

Vue.js est un **framework JavaScript** utilisé pour créer des interfaces utilisateur dynamiques et interactives. Il permet aux développeurs de construire des **applications web modernes** de manière modulaire en utilisant des **composants**.

#### Principaux avantages de Vue.js :
- **Facilité d'apprentissage** : Syntaxe intuitive basée sur HTML, CSS et JavaScript.
- **Modularité** : Les applications Vue sont composées de **composants réutilisables**, chaque composant contenant son propre HTML, CSS et JavaScript.
- **Réactivité** : Vue suit automatiquement les changements de données et met à jour le DOM sans intervention manuelle.

#### Exemple basique avec Vue :

```html
<div id="app">
  <p>{{ message }}</p>
</div>

<script>
  const app = Vue.createApp({
    data() {
      return {
        message: 'Bonjour, Vue.js !'
      };
    }
  });
  app.mount('#app');
</script>
```

---

#### Vite : Un outil de développement moderne

**Vite**, créé par le développeur de Vue.js, est un **outil de construction** (build tool). Il sert à simplifier et accélérer le développement des applications web modernes, en particulier celles basées sur Vue.

Vite propose :
1. **Un serveur de développement rapide** pour tester votre code localement.
2. **Un système de compilation optimisé** pour créer des applications prêtes à être déployées.

---

### 1.2 Qu'est-ce qu'un bundler ?

Un **bundler** est un outil qui regroupe et optimise les différents fichiers de votre projet web (JavaScript, CSS, images, etc.) en un ou plusieurs fichiers optimisés pour le navigateur.

#### Pourquoi utiliser un bundler ?
- **Optimisation** : Les fichiers sont compressés, réduisant le temps de chargement.
- **Compatibilité** : Les fonctionnalités modernes de JavaScript sont transformées pour fonctionner sur des navigateurs plus anciens.
- **Gestion des dépendances** : Si votre projet utilise plusieurs bibliothèques externes, un bundler les regroupe pour éviter des chargements inutiles.

**Exemple sans bundler** :  
Chaque fichier JavaScript est chargé séparément par le navigateur, ce qui peut être lent.

**Exemple avec bundler** :  
Tous les fichiers sont combinés en un seul fichier optimisé.

---

### 1.3 Différences entre Vite et Webpack

**Webpack** est l’un des bundlers les plus populaires, mais il fonctionne différemment de Vite.

| **Caractéristique**       | **Vite**                                       | **Webpack**                    |
|---------------------------|------------------------------------------------|--------------------------------|
| **Mode développement**     | Charge les fichiers dynamiquement grâce aux modules ES natifs | Crée un gros bundle à l'avance |
| **Hot Module Replacement** | Très rapide, cible uniquement les modules modifiés | Plus lent, nécessite parfois un rechargement complet |
| **Temps de démarrage**     | Presque instantané                             | Peut prendre plusieurs secondes |
| **Configuration**         | Minimaliste par défaut                         | Configuration plus complexe    |
| **Build production**      | Utilise Rollup pour une optimisation optimale  | Utilise Webpack pour la production |

---

### 1.4 Avantages de Vite : Rapidité et Hot Module Replacement (HMR)

1. **Rapidité en mode développement** :  
   Grâce aux modules ES natifs, Vite charge uniquement les fichiers nécessaires, sans pré-bundler tout le projet. Cela accélère considérablement le **temps de démarrage**.

2. **Hot Module Replacement (HMR)** :  
   Vite détecte les modifications dans votre code et met instantanément à jour uniquement les modules concernés dans le navigateur. Cela signifie :
   - **Temps de feedback immédiat** : Les changements sont visibles en temps réel.
   - **Workflow fluide** : Pas besoin de recharger la page ou de perdre l’état de l’application.

---

## 2. Installation de Vite

### 2.1 Utilisation de la commande `npm create vite@latest`

Pour démarrer un nouveau projet avec Vite, utilisez cette commande simple dans votre terminal :

```bash
npm create vite@latest
```

#### Étapes détaillées :

1. **Exécutez la commande** :
   ```bash
   npm create vite@latest my-vue-app
   ```

2. **Choisissez le framework** :  
   Vous serez invité à sélectionner un framework. Choisissez **Vue** ou **Vue + TypeScript** si vous souhaitez utiliser TypeScript pour un typage robuste.

3. **Accédez au répertoire du projet** :
   ```bash
   cd my-vue-app
   ```

4. **Installez les dépendances** :
   ```bash
   npm install
   ```

5. **Lancez le serveur de développement** :
   ```bash
   npm run dev
   ```

Cela démarre un serveur local et ouvre automatiquement l’application dans votre navigateur à l’URL `http://localhost:3000`.

---

## 3. Structure d’un projet Vite

### 3.1 Dossiers publics et sources (`main.ts`, `App.vue`)

Un projet Vite suit une structure simple mais puissante :

```plaintext
my-vue-app/
├── public/         # Fichiers statiques accessibles sans transformation
├── src/            # Fichiers sources de l'application
│   ├── main.ts     # Point d'entrée principal
│   ├── App.vue     # Composant racine de l'application
│   ├── components/ # Composants Vue réutilisables
├── vite.config.ts  # Configuration de Vite
├── package.json    # Dépendances et scripts
```

#### Détails des fichiers :
- **`main.ts`** :  
  C'est ici que l'application Vue est créée et montée sur l'élément HTML racine.

  ```typescript
  import { createApp } from 'vue';
  import App from './App.vue';

  createApp(App).mount('#app');
  ```

- **`App.vue`** :  
  Le composant racine qui inclut du HTML, de la logique TypeScript, et des styles CSS.

  ```vue
  <template>
    <h1>Bienvenue sur mon application Vue avec Vite !</h1>
  </template>

  <script setup lang="ts">
  // Code TypeScript ici
  </script>

  <style>
  h1 {
    color: blue;
  }
  </style>
  ```

---

## 4. Configuration avancée

### 4.1 Fichiers de configuration `vite.config.ts`

Le fichier `vite.config.ts` permet de personnaliser le comportement de Vite, comme le port du serveur ou l’ajout de plugins.

#### Exemple basique de configuration :

```typescript
import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';

export default defineConfig({
  plugins: [vue()],
  server: {
    port: 3000, // Définit le port à 3000
    open: true  // Ouvre automatiquement le navigateur au démarrage
  },
  resolve: {
    alias: {
      '@': '/src'  // Simplifie les imports avec des alias
    }
  }
});
```

### 4.2 Plugins Vite pour Vue (vue-router, Vuex/Pinia)

1. **Installation de vue-router** :
   ```bash
   npm install vue-router@4
   ```

   Configurez-le dans votre projet :

   ```typescript
   import { createRouter, createWebHistory } from 'vue-router';

   const routes = [
     { path: '/', component: Home },
     { path: '/about', component: About }
   ];

  

 const router = createRouter({
     history: createWebHistory(),
     routes
   });

   export default router;
   ```

2. **Installation de Pinia pour la gestion d’état** :
   ```bash
   npm install pinia
   ```

---

[Menu vue](../menu.md)