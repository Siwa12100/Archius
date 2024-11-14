# Chapitre 5 : Navigation avec Vue Router

[Menu Vue](../menu.md)

## Sommaire

1. [Configuration et navigation](#configuration-et-navigation)  
   1.1 [Introduction à Vue Router](#introduction-à-vue-router)  
   1.2 [Définition des routes](#définition-des-routes)  
2. [Paramètres et données de route](#paramètres-et-données-de-route)  
   2.1 [Utilisation des `params` pour charger des vues dynamiques](#utilisation-des-params-pour-charger-des-vues-dynamiques)  
3. [Composants imbriqués et classes actives](#composants-imbriqués-et-classes-actives)  
   3.1 [Routes imbriquées](#routes-imbriquées)  
   3.2 [Classe active dynamique (`router-link-active`)](#classe-active-dynamique-router-link-active)  

---

## 1. Configuration et navigation

### 1.1 Introduction à Vue Router

Vue Router est une **bibliothèque officielle** de Vue.js qui permet de gérer la **navigation** dans une application web monopage (SPA - Single Page Application). Contrairement aux sites web traditionnels qui rechargent entièrement la page lors de la navigation, Vue Router permet de **changer dynamiquement le contenu** sans rechargement complet.

#### Principaux concepts de Vue Router :
- **Routes** : Définissent les chemins d'accès et le composant à afficher.
- **Liens dynamiques** : Permettent de naviguer entre les pages sans rechargement.
- **Paramètres de route** : Passent des informations spécifiques pour afficher du contenu dynamique.
- **Garde de navigation** : Permet de contrôler l'accès à certaines routes.

---

### 1.2 Définition des routes

La configuration de Vue Router commence par la création d'un **fichier de routes** qui associe des chemins URL à des composants spécifiques.

#### Étapes pour configurer Vue Router :

1. **Installer Vue Router** :

   ```bash
   npm install vue-router@4
   ```

2. **Créer un fichier de routes** :

   Dans le dossier `src`, créez un fichier `router/index.ts` :

   ```typescript
   import { createRouter, createWebHistory } from 'vue-router';
   import Home from '../views/Home.vue';
   import About from '../views/About.vue';

   const routes = [
     { path: '/', component: Home, name: 'Home' },
     { path: '/about', component: About, name: 'About' }
   ];

   const router = createRouter({
     history: createWebHistory(),
     routes
   });

   export default router;
   ```

3. **Inclure le routeur dans l'application** :

   Modifiez le fichier `main.ts` pour utiliser Vue Router :

   ```typescript
   import { createApp } from 'vue';
   import App from './App.vue';
   import router from './router';

   createApp(App).use(router).mount('#app');
   ```

4. **Créer des composants de vue** :  
   Dans `src/views`, créez les fichiers suivants :
   
   - **Home.vue** :
     ```vue
     <template>
       <h1>Page d'accueil</h1>
     </template>
     ```

   - **About.vue** :
     ```vue
     <template>
       <h1>À propos</h1>
     </template>
     ```

5. **Navigation entre les routes** :  
   Utilisez la balise `router-link` pour créer des liens navigables.

   ```vue
   <template>
     <nav>
       <router-link to="/">Accueil</router-link>
       <router-link to="/about">À propos</router-link>
     </nav>
     <router-view />
   </template>
   ```

---

## 2. Paramètres et données de route

### 2.1 Utilisation des `params` pour charger des vues dynamiques

Les **params** sont utilisés pour transmettre des **données dynamiques** à travers l'URL. Cela permet de créer des pages personnalisées ou basées sur des ressources spécifiques.

#### Définir une route avec paramètres :

```typescript
const routes = [
  { path: '/utilisateur/:id', component: Utilisateur, name: 'Utilisateur' }
];
```

- **`:id`** est un paramètre dynamique. Il sera remplacé par une valeur spécifique lors de la navigation.

#### Exemple de composant avec `params` :

```vue
<template>
  <div>
    <h1>Profil de l'utilisateur</h1>
    <p>ID utilisateur : {{ $route.params.id }}</p>
  </div>
</template>
```

- Le paramètre `id` est accessible via `$route.params.id`.

#### Navigation programmatique :

Vous pouvez naviguer vers une route avec des paramètres en utilisant `router.push` :

```typescript
this.$router.push({ name: 'Utilisateur', params: { id: 42 } });
```

Cela redirigera vers `/utilisateur/42`.

---

## 3. Composants imbriqués et classes actives

### 3.1 Routes imbriquées

Vue Router permet de définir des **routes imbriquées** pour afficher des sous-composants dans une structure hiérarchique.

#### Définir des routes imbriquées :

```typescript
const routes = [
  {
    path: '/dashboard',
    component: Dashboard,
    children: [
      { path: 'profile', component: Profile },
      { path: 'settings', component: Settings }
    ]
  }
];
```

#### Exemple de composant parent avec `router-view` :

```vue
<template>
  <div>
    <h1>Tableau de bord</h1>
    <nav>
      <router-link to="/dashboard/profile">Profil</router-link>
      <router-link to="/dashboard/settings">Paramètres</router-link>
    </nav>
    <router-view />
  </div>
</template>
```

- **`router-view`** rend dynamiquement les sous-composants en fonction de la route actuelle.

---

### 3.2 Classe active dynamique (`router-link-active`)

Par défaut, Vue Router applique une classe CSS appelée **`router-link-active`** aux liens de navigation correspondant à la route actuelle.

#### Exemple de style actif :

```vue
<template>
  <nav>
    <router-link to="/" active-class="actif">Accueil</router-link>
    <router-link to="/about">À propos</router-link>
  </nav>
</template>

<style>
.actif {
  font-weight: bold;
  color: blue;
}
</style>
```

Ici, le lien vers la route active aura un style personnalisé grâce à la classe `actif`.

---

[Menu vue](../menu.md)