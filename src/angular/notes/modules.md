### Modules, Injection de Dépendances et Structuration projet en Angular

[...retour menu sur Angular](../menu.md)

---

## 1. **Les modules en JavaScript et TypeScript (vanilla)**

### 1.1. **Qu'est-ce qu'un module en JavaScript/TypeScript ?**
Un **module** en JavaScript et TypeScript est un fichier autonome contenant des variables, des fonctions, des classes, ou des objets. Par défaut, le contenu d'un module n'est pas accessible en dehors de celui-ci, sauf si tu l'**exportes** explicitement. Un autre fichier peut **importer** ce module et utiliser ce qui est exporté.

- **Encapsulation** : Le code non exporté reste privé au module.
- **Réutilisation** : Les modules permettent de partager du code entre différentes parties d'un projet.

### 1.2. **Utiliser `export` et `import`**

#### **Export nommé** :
Un module peut exporter plusieurs éléments nommés avec `export`. Ceux-ci sont importés via des accolades `{}`.

```javascript
// mathUtils.js
export function addition(a, b) { return a + b; }
export const PI = 3.1415;
```

```javascript
// main.js
import { addition, PI } from './mathUtils.js';
console.log(PI); // 3.1415
```

#### **Export par défaut** :
Un module peut avoir un seul **export par défaut**. Celui-ci est importé sans accolades.

```javascript
// defaultExport.js
export default function () { console.log('Export par défaut'); }
```

```javascript
// main.js
import defaultFunction from './defaultExport.js';
defaultFunction(); // Affiche 'Export par défaut'
```

### 1.3. **Modularité et Encapsulation**
En répartissant le code en différents fichiers/modules, tu peux **encapsuler** certaines fonctionnalités. Pour créer une **interface publique** avec un module final, tu peux regrouper des modules internes et n'en exposer qu'une seule partie.

Exemple :
```javascript
// userModel.js (module interne privé)
export class User {
  constructor(name, age) {
    this.name = name;
    this.age = age;
  }
}

// userModule.js (module final public)
import { User } from './userModel.js';
export function createUser(name, age) {
  return new User(name, age);
}
```

---

## 2. **Modules en Angular**

### 2.1. **Qu'est-ce qu'un module en Angular ?**
Un **module Angular** est une classe décorée par le décorateur `@NgModule`. Il sert à regrouper des composants, services, directives, pipes, et autres éléments, afin de structurer l'application de manière modulaire. Chaque application Angular a au moins un module racine, souvent appelé `AppModule`.

#### Exemple de module Angular :
```typescript
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [AppComponent], // Les composants de ce module
  imports: [BrowserModule], // Les autres modules nécessaires
  providers: [], // Les services disponibles pour le module
  bootstrap: [AppComponent] // Le composant racine à lancer
})
export class AppModule { }
```

### 2.2. **Composition d'un `NgModule`**
- **`declarations`** : Les composants, directives et pipes déclarés dans ce module.
- **`imports`** : Les modules dont ce module dépend.
- **`providers`** : Les services disponibles via l'injection de dépendances.
- **`bootstrap`** : Le composant à démarrer au lancement de l'application.
- **`exports`** : Les composants/directives/pipes exposés à d'autres modules.

### 2.3. **Modularité en Angular**
Angular recommande d'organiser ton application en **modules de fonctionnalités**. Par exemple :
- Un **UserModule** pour gérer les fonctionnalités utilisateur.
- Un **SharedModule** pour les composants et directives réutilisables.

Cela améliore la maintenabilité et permet de charger certaines parties de l'application uniquement quand elles sont nécessaires (lazy loading).

---

## 3. **Scopes et Injection de Dépendances en Angular**

### 3.1. **Introduction à l'injection de dépendances (DI)**
L'**injection de dépendances** permet à Angular de fournir des services ou objets à des composants ou à d'autres services sans qu'ils aient besoin de créer ces instances eux-mêmes. Cela favorise la modularité et la réutilisation du code.

### 3.2. **Les différents scopes de services**

#### **Scope global : `providedIn: 'root'`**
Le service est disponible dans toute l'application, et une seule instance (singleton) est partagée entre tous les composants et services.
```typescript
@Injectable({
  providedIn: 'root'
})
export class MonService { }
```

#### **Scope de module**
Le service est limité à un module particulier. Les composants ou services de ce module partagent la même instance du service.
```typescript
@NgModule({
  providers: [MonService]
})
export class MonModule { }
```

#### **Scope de composant**
Chaque instance du composant a sa propre instance du service. Cela permet d'isoler l'état du service au niveau du composant.
```typescript
@Component({
  selector: 'app-mon-composant',
  providers: [MonService]
})
export class MonComposant { }
```

### 3.3. **Standalone Components dans Angular 18**
Depuis Angular 18, les composants peuvent être **standalone** par défaut, ce qui signifie qu'ils ne dépendent plus forcément d'un `NgModule`. Ils peuvent importer des modules et services directement dans leur propre fichier.

#### Exemple de composant standalone :
```typescript
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-mon-standalone-composant',
  standalone: true,
  imports: [CommonModule],
  template: `<h1>Standalone Composant</h1>`
})
export class MonStandaloneComposant { }
```

Les composants standalone simplifient la gestion des dépendances et permettent une plus grande modularité.

---

## 4. **Structuration Professionnelle d'un Projet Angular**

### 4.1. **Structure des dossiers et fichiers**

Une structure claire et cohérente est essentielle dans un projet Angular professionnel. Voici une structure typique :

```
src/
  app/
    core/
      services/
        auth.service.ts
        user.service.ts
      guards/
        auth.guard.ts
    shared/
      components/
        navbar/
          navbar.component.ts
          navbar.component.html
          navbar.component.css
      directives/
        highlight.directive.ts
    features/
      user/
        user.module.ts
        components/
          user-profile.component.ts
          user-list.component.ts
      admin/
        admin.module.ts
        components/
          admin-dashboard.component.ts
    app.component.ts
    app.module.ts
  assets/
  environments/
  index.html
```

### 4.2. **Organisation des services**
Les **services** partagés sont généralement placés dans un répertoire `core/services`. Ces services sont fournis à l'échelle de l'application (`providedIn: 'root'`), à moins qu'ils ne soient spécifiques à une fonctionnalité ou un module.

Exemple :
```typescript
// core/services/auth.service.ts
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  login() { /* logique d'authentification */ }
}
```

### 4.3. **Organisation des composants**
Les **composants** peuvent être regroupés en fonctionnalités dans des répertoires `features`. Chaque **fonctionnalité** a son propre module (`UserModule`, `AdminModule`) qui organise les composants associés. Depuis Angular 18, les composants standalone peuvent aussi être utilisés pour éviter de créer des modules superflus.

### 4.4. **Utilisation des modules partagés**
Un **SharedModule** regroupe les composants, directives, et pipes qui sont réutilisés dans plusieurs modules. Cela évite la duplication de code.

Exemple :
```typescript
@NgModule({
  declarations: [MonComposant],
  imports: [CommonModule],
  exports: [MonComposant, CommonModule]
})
export class SharedModule { }
```

### 4.5. **Optimisation avec le Lazy Loading**
Le **lazy loading** est essentiel pour les applications Angular de grande envergure. Il permet de charger certains modules uniquement lorsque l'utilisateur accède à une fonctionnalité particulière, améliorant ainsi les performances initiales.

```typescript
// app-routing.module.ts
const routes: Routes = [
  {
    path: 'admin',
    loadChildren: () => import('./features/admin/admin.module').then(m => m.AdminModule)
  }
];
```

---

## 5. **Bonnes pratiques pour structurer un projet Angular**

### 5.1. **Modularité**
Chaque **module** doit avoir une responsabilité claire (UserModule, AdminModule). Cela améliore la lisibilité et la maintenabilité du code.

### 5.2. **Single Responsibility Principle (SRP)**
Que ce soit pour les services, les composants ou les modules, applique toujours le principe de **responsabilité unique**. Chaque composant ou service ne doit accomplir qu'une tâche.

### 5.3. **Utiliser des standalone components**
Avec Angular 18 et plus, les **composants standalone** sont une bonne option pour réduire la complexité de gestion des

 modules. Utilise-les lorsque tu n'as pas besoin d'organiser plusieurs composants sous un même module.

### 5.4. **Centraliser les services partagés dans `core/`**
Les services partagés comme l'authentification, la gestion des utilisateurs, ou l'accès à une API doivent être centralisés dans un répertoire `core/services` et fournis à l'échelle de l'application.

### 5.5. **Utiliser des modules partagés**
Regroupe les composants et directives réutilisables dans un **SharedModule**. Cela évite de dupliquer les imports à travers plusieurs modules.

---

[...retour menu sur Angular](../menu.md)