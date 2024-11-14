# Chapitre 10 : Utilisation d’Axios avec Vue.js

[Menu vue](../menu.md)

## Sommaire

1. [Qu’est-ce qu’Axios ?](#quest-ce-quaxios)  
   1.1 [Pourquoi utiliser Axios ?](#pourquoi-utiliser-axios)  
2. [Installation et configuration d’Axios](#installation-et-configuration-daxios)  
   2.1 [Installation d’Axios dans un projet Vue](#installation-daxios-dans-un-projet-vue)  
   2.2 [Configuration de base](#configuration-de-base)  
3. [Appels API avec Axios](#appels-api-avec-axios)  
   3.1 [Requêtes GET](#requêtes-get)  
   3.2 [Requêtes POST](#requêtes-post)  
   3.3 [Requêtes PUT et DELETE](#requêtes-put-et-delete)  
4. [Gestion des erreurs avec Axios](#gestion-des-erreurs-avec-axios)  
5. [Intercepteurs (Interceptors)](#intercepteurs-interceptors)  
   5.1 [Ajouter des en-têtes automatiquement](#ajouter-des-en-têtes-automatiquement)  
   5.2 [Gestion globale des erreurs](#gestion-globale-des-erreurs)  
6. [Intégration avancée avec Vue](#intégration-avancée-avec-vue)  
   6.1 [Créer un service Axios global](#créer-un-service-axios-global)  
   6.2 [Injection via `provide/inject`](#injection-via-provideinject)  

---

## 1. Qu’est-ce qu’Axios ?

### 1.1 Pourquoi utiliser Axios ?

**Axios** est une bibliothèque JavaScript qui permet de faire des **requêtes HTTP** (GET, POST, etc.) vers des APIs.

#### Avantages d’Axios :
- **Support des promesses** : Syntaxe claire avec `async/await`.
- **Intercepteurs** : Personnalisation des requêtes et réponses.
- **Support des en-têtes** : Facilité pour ajouter des en-têtes (comme les tokens d’authentification).
- **Gestion des erreurs simplifiée**.

---

## 2. Installation et configuration d’Axios

### 2.1 Installation d’Axios dans un projet Vue

Pour utiliser Axios dans un projet Vue :

```bash
npm install axios
```

---

### 2.2 Configuration de base

Il est possible de configurer une **instance Axios** avec une URL de base et des en-têtes communs.

#### Exemple de configuration dans un fichier dédié :

##### `axiosInstance.ts` :
```typescript
import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'https://api.example.com',
  timeout: 10000, // Temps d'attente max pour une requête
  headers: {
    'Content-Type': 'application/json'
  }
});

export default axiosInstance;
```

---

## 3. Appels API avec Axios

### 3.1 Requêtes GET

Les requêtes GET sont utilisées pour **récupérer des données**.

#### Exemple de requête GET dans un composant :

```vue
<template>
  <div>
    <ul>
      <li v-for="utilisateur in utilisateurs" :key="utilisateur.id">{{ utilisateur.nom }}</li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import axiosInstance from './axiosInstance';

const utilisateurs = ref([]);

const chargerUtilisateurs = async () => {
  try {
    const response = await axiosInstance.get('/utilisateurs');
    utilisateurs.value = response.data;
  } catch (error) {
    console.error('Erreur lors du chargement des utilisateurs', error);
  }
};

onMounted(() => {
  chargerUtilisateurs();
});
</script>
```

---

### 3.2 Requêtes POST

Les requêtes POST sont utilisées pour **envoyer des données** au serveur.

#### Exemple de création d’un utilisateur :

```typescript
const nouvelUtilisateur = ref({ nom: 'Jean', email: 'jean@example.com' });

const créerUtilisateur = async () => {
  try {
    const response = await axiosInstance.post('/utilisateurs', nouvelUtilisateur.value);
    console.log('Utilisateur créé', response.data);
  } catch (error) {
    console.error('Erreur lors de la création de l’utilisateur', error);
  }
};
```

---

### 3.3 Requêtes PUT et DELETE

- **PUT** : Met à jour une ressource existante.
- **DELETE** : Supprime une ressource.

#### Exemple de mise à jour :

```typescript
const mettreAJourUtilisateur = async (id: number, data: object) => {
  try {
    const response = await axiosInstance.put(`/utilisateurs/${id}`, data);
    console.log('Utilisateur mis à jour', response.data);
  } catch (error) {
    console.error('Erreur lors de la mise à jour', error);
  }
};
```

#### Exemple de suppression :

```typescript
const supprimerUtilisateur = async (id: number) => {
  try {
    await axiosInstance.delete(`/utilisateurs/${id}`);
    console.log('Utilisateur supprimé');
  } catch (error) {
    console.error('Erreur lors de la suppression', error);
  }
};
```

---

## 4. Gestion des erreurs avec Axios

Axios permet de gérer facilement les **erreurs** via un bloc `try/catch` ou grâce à des **intercepteurs**.

#### Exemple de gestion d’erreurs :

```typescript
try {
  const response = await axiosInstance.get('/utilisateurs');
} catch (error) {
  if (axios.isAxiosError(error)) {
    console.error('Erreur Axios :', error.message);
  } else {
    console.error('Erreur inconnue :', error);
  }
}
```

---

## 5. Intercepteurs (Interceptors)

Les intercepteurs permettent de **modifier les requêtes ou les réponses** avant qu'elles ne soient traitées.

### 5.1 Ajouter des en-têtes automatiquement

Vous pouvez utiliser un intercepteur pour **ajouter un token d’authentification** à chaque requête.

```typescript
axiosInstance.interceptors.request.use(config => {
  const token = 'votre-token-ici';
  if (token) {
    config.headers['Authorization'] = `Bearer ${token}`;
  }
  return config;
}, error => {
  return Promise.reject(error);
});
```

---

### 5.2 Gestion globale des erreurs

Les intercepteurs de réponse peuvent également **gérer globalement les erreurs**.

```typescript
axiosInstance.interceptors.response.use(response => {
  return response;
}, error => {
  if (error.response?.status === 401) {
    console.error('Utilisateur non authentifié.');
  }
  return Promise.reject(error);
});
```

---

## 6. Intégration avancée avec Vue

### 6.1 Créer un service Axios global

Il est recommandé de **centraliser les appels API** dans un fichier de service.

#### Exemple de service utilisateur :

##### `userService.ts` :
```typescript
import axiosInstance from './axiosInstance';

export const getUtilisateurs = () => {
  return axiosInstance.get('/utilisateurs');
};

export const créerUtilisateur = (data: object) => {
  return axiosInstance.post('/utilisateurs', data);
};
```

#### Utilisation dans un composant :

```vue
<script setup lang="ts">
import { getUtilisateurs } from './userService';

const utilisateurs = ref([]);

onMounted(async () => {
  const response = await getUtilisateurs();
  utilisateurs.value = response.data;
});
</script>
```

---

### 6.2 Injection via `provide/inject`

Pour **injecter Axios** dans plusieurs composants, vous pouvez utiliser `provide` et `inject`.

#### Fournir Axios dans l’application principale :

```typescript
import { createApp } from 'vue';
import App from './App.vue';
import axiosInstance from './axiosInstance';

const app = createApp(App);
app.provide('axios', axiosInstance);
app.mount('#app');
```

#### Utilisation dans un composant enfant :

```vue
<script setup lang="ts">
import { inject } from 'vue';

const axios = inject('axios');
</script>
```

---

[Menu vue](../menu.md)