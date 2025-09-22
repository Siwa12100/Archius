# Bases d'Angular

[...retour au menu sur Angular](../menu.md)

---

## Table des matières
- [Bases d'Angular](#bases-dangular)
  - [Table des matières](#table-des-matières)
  - [Introduction à Angular](#introduction-à-angular)
    - [Qu'est-ce qu'Angular ?](#quest-ce-quangular-)
    - [Différentes versions d'Angular](#différentes-versions-dangular)
  - [Architecture d'une application Angular](#architecture-dune-application-angular)
    - [Structure d'un projet Angular](#structure-dun-projet-angular)
    - [Modules](#modules)
  - [Environnement de travail Angular](#environnement-de-travail-angular)
    - [Installation et configuration](#installation-et-configuration)
  - [TypeScript](#typescript)
    - [Présentation](#présentation)
    - [Interfaces](#interfaces)
    - [Classes et Héritage](#classes-et-héritage)
  - [Composants](#composants)
    - [Création d’un composant](#création-dun-composant)
  - [Bindings](#bindings)
    - [Types de bindings](#types-de-bindings)
  - [Directives](#directives)
    - [Directives Structurelles](#directives-structurelles)
    - [Directives d'attribut](#directives-dattribut)
  - [Services et injection de dépendances](#services-et-injection-de-dépendances)
    - [Création d'un service](#création-dun-service)
  - [Formulaires](#formulaires)
    - [Formulaires réactifs](#formulaires-réactifs)
  - [Routage](#routage)
    - [Définir les routes](#définir-les-routes)
    - [Router Outlet](#router-outlet)
  - [Appels HTTP et API](#appels-http-et-api)
    - [GET Request](#get-request)
  - [Observables et RxJS](#observables-et-rxjs)
  - [Cycle de vie d'un composant](#cycle-de-vie-dun-composant)

---

## Introduction à Angular

### Qu'est-ce qu'Angular ?

Angular est un **framework JavaScript** open-source, maintenu par **Google**, conçu pour créer des applications web modernes appelées **SPA** (Single Page Applications).

- **Framework tout-en-un** : Comprend des outils pour le routage, les formulaires, les animations, la gestion des requêtes HTTP, etc.
- **Langage principal** : Angular utilise **TypeScript**, un surensemble de JavaScript qui ajoute un typage statique et d'autres fonctionnalités utiles.
  
### Différentes versions d'Angular

Angular a beaucoup évolué :
- **AngularJS** (2010) : Première version utilisant JavaScript pur.
- **Angular 2** (2016) : Réécriture complète en TypeScript.
- **Versions suivantes** : Angular 4, 5, 6... et Angular 18 (2024), avec des améliorations progressives en termes de performance et de fonctionnalités.

---

## Architecture d'une application Angular

### Structure d'un projet Angular
Un projet Angular suit une architecture bien définie avec des composants, des services, et des modules.

```bash
src/
 ├── app/
 │    ├── app.component.ts     # Composant racine
 │    ├── app.module.ts        # Module principal
 │    ├── components/          # Répertoire pour composants supplémentaires
 │    └── services/            # Répertoire pour les services
 ├── assets/                   # Fichiers statiques (images, polices, etc.)
 └── environments/             # Configuration des environnements (prod/dev)
```

### Modules

Les **modules** organisent une application Angular en groupes fonctionnels. Le module racine est généralement `AppModule`, mais des modules spécifiques (comme `FormsModule`, `HttpClientModule`) peuvent être importés selon les besoins.

Exemple d'import de module :

```typescript
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule],
  bootstrap: [AppComponent]
})
export class AppModule {}
```

---

## Environnement de travail Angular

### Installation et configuration

1. **Installer Node.js** : Angular nécessite Node.js pour gérer les dépendances.
2. **Installer Angular CLI** : Utilise la commande suivante pour installer l'outil de ligne de commande Angular.

```bash
npm install -g @angular/cli
```

3. **Créer un nouveau projet** :

```bash
ng new mon-projet
```

4. **Lancer le serveur de développement** :

```bash
ng serve
```

---

## TypeScript

### Présentation
TypeScript est un surensemble de JavaScript avec des fonctionnalités supplémentaires comme le typage statique, les interfaces, et les classes.

```typescript
let message: string = 'Hello Angular!';
let age: number = 25;
```

### Interfaces

Les interfaces définissent des contrats pour les objets et classes.

```typescript
interface Person {
  name: string;
  age: number;
}

let student: Person = {
  name: 'John',
  age: 22
};
```

### Classes et Héritage

```typescript
class Animal {
  constructor(public name: string) {}
  speak(): void {
    console.log(`${this.name} makes a sound.`);
  }
}

class Dog extends Animal {
  speak(): void {
    console.log(`${this.name} barks.`);
  }
}

let dog = new Dog('Rex');
dog.speak(); // Rex barks.
```

---

## Composants

Les **composants** sont les blocs de construction principaux d'une application Angular. Chaque composant se compose de trois parties :
- Un fichier TypeScript contenant la logique du composant.
- Un fichier HTML pour le template.
- Un fichier CSS pour le style.

### Création d’un composant

```bash
ng generate component my-component
```

Exemple d'un composant :

```typescript
@Component({
  selector: 'app-my-component',
  templateUrl: './my-component.component.html',
  styleUrls: ['./my-component.component.css']
})
export class MyComponent {
  message: string = "Bienvenue dans Angular!";
}
```

---

## Bindings

### Types de bindings

- **Interpolation** : Liens entre la vue et le modèle avec `{{ }}`.
  
```html
<p>{{ message }}</p>
```

- **Property Binding** : Lie une propriété de DOM à une valeur dans le composant.

```html
<img [src]="imageUrl" />
```

- **Event Binding** : Gère les événements (ex. clic).

```html
<button (click)="handleClick()">Click me</button>
```

- **Two-way Binding** : Synchronise les données dans les deux sens (utilisé avec les formulaires).

```html
<input [(ngModel)]="name" />
```

---

## Directives

Les **directives** sont des instructions pour manipuler les éléments du DOM.

### Directives Structurelles

- **`*ngIf`** : Affiche ou masque un élément en fonction d'une condition.

```html
<p *ngIf="isLoggedIn">Bienvenue!</p>
```

- **`*ngFor`** : Boucle sur un tableau.

```html
<li *ngFor="let item of items">{{ item }}</li>
```

### Directives d'attribut

- **`[ngStyle]`** : Applique dynamiquement des styles.

```html
<div [ngStyle]="{'color': color}">Colored Text</div>
```

---

## Services et injection de dépendances

Les **services** sont utilisés pour partager des données ou des fonctionnalités entre composants.

### Création d'un service

```bash
ng generate service data
```

Exemple de service :

```typescript
@Injectable({
  providedIn: 'root',
})
export class DataService {
  getData() {
    return ['Data1', 'Data2', 'Data3'];
  }
}
```

Injection dans un composant :

```typescript
constructor(private dataService: DataService) {}
```

---

## Formulaires

Angular propose deux approches pour les formulaires :
1. **Template-driven forms** : Simplifiés, basés sur des directives dans le HTML.
2. **Reactive forms** : Basés sur des classes TypeScript pour un meilleur contrôle.

### Formulaires réactifs

```typescript
this.form = this.fb.group({
  name: ['', Validators.required],
  email: ['', [Validators.required, Validators.email]],
});
```

---

## Routage

Le **routage** permet de naviguer entre différents composants.

### Définir les routes

```typescript
const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'about', component: AboutComponent },
];
```

### Router Outlet

```html
<router-outlet></router-outlet>
```

---

## Appels HTTP et API

Angular utilise `HttpClient` pour gérer les requêtes HTTP.

### GET Request

```typescript
this.http.get('https://api.example.com/data')
  .subscribe(data => console.log(data));
```

---

## Observables et RxJS

Les **observables** sont des flux de données asynchrones, utilisés avec les appels HTTP et la gestion des événements.

```typescript
this.dataService.getData().subscribe(data => console.log(data));
```

---

## Cycle de vie d'un composant

Angular offre plusieurs **hooks** pour gérer les différentes phases du cycle de vie d'un composant.

- **ngOnInit** : Exécuté après la création du composant.
- **ngOnChanges** : Exécuté lors de la modification des propriétés d'entrée.
- **ngOnDestroy** : Exécuté juste avant la destruction du composant.

---

[...retour au menu sur Angular](../menu.md)
