# Cours 2 Angular 17 - Résumé

[...retour en arriere](../menu.md)

---

## Table des matières
1. TypeScript
   - Les fonctions
   - Les classes
   - Exercice TypeScript
2. Utilisation d'Angular
   - Les pipes
   - Cycle de vie d’un composant
   - Gestion des événements
   - Les formulaires
   - Le routage
   - Les appels d’API et services HTTP
   - Observables et subscribe

---

## 1. TypeScript

### 1.1. Les fonctions

En TypeScript, les fonctions doivent être typées au niveau des paramètres et du type de retour.

#### Exemple d'une fonction typée :
```typescript
function addition(a: number, b: number): number {
  return a + b;
}
```

#### Paramètres optionnels et par défaut
- Utiliser `?` pour indiquer un paramètre optionnel.
- Spécifier une valeur par défaut pour les paramètres.

```typescript
function afficherMessage(message: string = "Bonjour", nom?: string): string {
  return nom ? `${message}, ${nom}` : message;
}
```

#### Les fonctions fléchées (Arrow Functions)
Les **arrow functions** permettent une syntaxe plus concise et n’ont pas de leur propre `this`.

```typescript
let nombres = [1, 2, 3, 4];
let result = nombres.filter((n) => n % 2 === 0);  // [2, 4]
```

### 1.2. Les classes

TypeScript prend en charge les classes avec des **modificateurs d'accès** (public, private, protected), l'héritage, et les **getters** et **setters**.

#### Exemple d’une classe avec des modificateurs d'accès :
```typescript
class Personne {
  private nom: string;
  private prenom: string;

  constructor(nom: string, prenom: string) {
    this.nom = nom;
    this.prenom = prenom;
  }

  public sePresenter(): string {
    return `Bonjour, je suis ${this.prenom} ${this.nom}`;
  }
}

let personne = new Personne("Doe", "John");
console.log(personne.sePresenter());
```

#### Héritage et méthode `super`
TypeScript permet l’héritage entre classes.

```typescript
class Employe extends Personne {
  private poste: string;

  constructor(nom: string, prenom: string, poste: string) {
    super(nom, prenom);
    this.poste = poste;
  }

  public sePresenter(): string {
    return `${super.sePresenter()} et je suis ${this.poste}`;
  }
}

let employe = new Employe("Smith", "Jane", "Développeur");
console.log(employe.sePresenter());
```

### 1.3. Exercice TypeScript
- Créez une classe `Travail` avec des propriétés `intitule`, `lieu` et `salaire` (avec des restrictions).
- Implémentez des getters et setters pour valider le salaire (entre 0 et 25000).
- Créez une méthode qui affiche toutes les informations du travail.

---

## 2. Utilisation d'Angular

### 2.1. Les Pipes

Les **pipes** transforment les données dans le template avant de les afficher.

#### Pipes intégrés
- `uppercase` : transforme un texte en majuscules.
- `date` : formate une date.
- `currency` : formate un nombre en devise.

Exemple :
```html
<p>{{ utilisateur.nom | uppercase }}</p>
<p>{{ utilisateur.dateDeNaissance | date:'shortDate' }}</p>
<p>{{ utilisateur.salaire | currency:'EUR' }}</p>
```

#### Pipe personnalisé
Vous pouvez créer des pipes personnalisés en Angular.

```typescript
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'exponentiel' })
export class ExponentielPipe implements PipeTransform {
  transform(valeur: number, exponent: number = 1): number {
    return Math.pow(valeur, exponent);
  }
}
```

Utilisation :
```html
<p>{{ 2 | exponentiel:3 }}</p>  <!-- Affiche 8 (2^3) -->
```

### 2.2. Cycle de vie d’un composant

Les composants Angular ont plusieurs étapes de cycle de vie, chacune avec des **hooks**.

#### Liste des hooks principaux :
- `ngOnChanges` : appelé lorsque les données liées au composant changent.
- `ngOnInit` : appelé une fois, après l'initialisation du composant.
- `ngOnDestroy` : appelé juste avant la destruction du composant.

Exemple :
```typescript
export class MonComposant implements OnInit, OnDestroy {
  ngOnInit() {
    console.log("Composant initialisé");
  }

  ngOnDestroy() {
    console.log("Composant détruit");
  }
}
```

### 2.3. Gestion des événements

#### Event binding
Liaison d'événements du DOM à une méthode du composant.

Exemple :
```html
<button (click)="onClick()">Cliquez-moi</button>
```

Dans le composant :
```typescript
export class MonComposant {
  onClick() {
    console.log("Bouton cliqué");
  }
}
```

### 2.4. Les formulaires

#### Formulaires Template-driven
Les formulaires **pilotés par la vue** sont basés sur le modèle défini directement dans le template.

Exemple :
```html
<form #userForm="ngForm" (ngSubmit)="onSubmit()">
  <input name="nom" [(ngModel)]="user.nom" required>
  <button type="submit">Soumettre</button>
</form>
```

#### Formulaires réactifs
Les **formulaires réactifs** sont définis dans le code TypeScript à l'aide du `FormBuilder`.

Exemple :
```typescript
this.formulaireUtilisateur = this.fb.group({
  nom: ['', [Validators.required, Validators.minLength(3)]],
  email: ['', [Validators.required, Validators.email]]
});
```

### 2.5. Le routage

Angular dispose d’un module de **routage** pour la navigation entre composants.

#### Déclaration des routes
```typescript
import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'about', component: AboutComponent }
];
```

#### Navigation dans le template
```html
<nav>
  <a routerLink="/">Accueil</a>
  <a routerLink="/about">À propos</a>
</nav>
<router-outlet></router-outlet>
```

### 2.6. Les appels d’API et services HTTP

Le module `HttpClient` permet de réaliser des requêtes HTTP.

#### Exemple d’un service API :
```typescript
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private http: HttpClient) {}

  getUsers() {
    return this.http.get('https://jsonplaceholder.typicode.com/users');
  }
}
```

#### Utilisation dans un composant :
```typescript
export class UserComponent implements OnInit {
  users: any[];

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.apiService.getUsers().subscribe((data) => {
      this.users = data;
    });
  }
}
```

### 2.7. Observables et subscribe

Les **observables** sont des flux de données asynchrones qui peuvent émettre des valeurs multiples.

#### Exemple avec un observable et `subscribe` :
```typescript
import { of } from 'rxjs';

const observable = of(1, 2, 3);

observable.subscribe({
  next: x => console.log('Valeur émise : ' + x),
  error: err => console.error('Erreur : ' + err),
  complete: () => console.log('Observable complété')
});
```

---

[...retour en arriere](../menu.md)