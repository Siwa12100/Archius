# Illustration du résumé de cours 2 

[...retour en arriere](../menu.md)

---

## Structure du projet

```
/src
 ├── /app
 |    ├── /services
 |    |    └── api.service.ts
 |    ├── /components
 |    |    ├── utilisateur
 |    |    |   ├── utilisateur.component.ts
 |    |    |   └── utilisateur.component.html
 |    |    ├── formulaire-utilisateur
 |    |    |   ├── formulaire-utilisateur.component.ts
 |    |    |   └── formulaire-utilisateur.component.html
 |    |    ├── pipes
 |    |    |   └── exponentiel.pipe.ts
 |    ├── app.module.ts
 |    ├── app.component.ts
 |    └── app.component.html
 ├── /assets
 └── index.html
```

---

## 1. Les Pipes

### Pipe personnalisé : exponentiel.pipe.ts

Ce pipe permet d’augmenter un nombre à une puissance donnée.

```typescript
// src/app/components/pipes/exponentiel.pipe.ts
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'exponentiel'
})
export class ExponentielPipe implements PipeTransform {
  transform(value: number, exponent: number = 1): number {
    return Math.pow(value, exponent);
  }
}
```

### Utilisation dans un template

```html
<!-- src/app/components/utilisateur/utilisateur.component.html -->
<h2>Affichage avec Pipe Exponentiel</h2>
<p>{{ 2 | exponentiel:3 }}</p> <!-- Affiche 8 -->
```

---

## 2. Cycle de vie d’un composant

### Composant avec hooks de cycle de vie

Ce composant utilise plusieurs hooks de cycle de vie comme `ngOnInit`, `ngOnChanges` et `ngOnDestroy`.

```typescript
// src/app/components/utilisateur/utilisateur.component.ts
import { Component, OnInit, OnDestroy, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-utilisateur',
  templateUrl: './utilisateur.component.html'
})
export class UtilisateurComponent implements OnInit, OnChanges, OnDestroy {
  @Input() utilisateur: any;

  constructor() {
    console.log('Appel du constructeur');
  }

  ngOnInit(): void {
    console.log('ngOnInit - Composant initialisé');
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log('ngOnChanges - Changement détecté :', changes);
  }

  ngOnDestroy(): void {
    console.log('ngOnDestroy - Composant détruit');
  }
}
```

---

## 3. Gestion des événements

### Gestion d'un événement `click`

Voici un exemple où un événement `click` déclenche une méthode dans le composant.

```html
<!-- src/app/components/utilisateur/utilisateur.component.html -->
<button (click)="onClick()">Cliquez-moi</button>
```

```typescript
// src/app/components/utilisateur/utilisateur.component.ts
export class UtilisateurComponent {
  onClick(): void {
    console.log("Bouton cliqué !");
  }
}
```

---

## 4. Formulaires réactifs

### Formulaire réactif avec validation

Ce composant gère un formulaire réactif avec des validations (nom requis et email valide).

```typescript
// src/app/components/formulaire-utilisateur/formulaire-utilisateur.component.ts
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-formulaire-utilisateur',
  templateUrl: './formulaire-utilisateur.component.html'
})
export class FormulaireUtilisateurComponent {
  formulaireUtilisateur: FormGroup;

  constructor(private fb: FormBuilder) {
    this.formulaireUtilisateur = this.fb.group({
      nom: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit() {
    if (this.formulaireUtilisateur.valid) {
      console.log("Formulaire valide", this.formulaireUtilisateur.value);
      this.formulaireUtilisateur.reset();
    } else {
      console.log("Formulaire invalide");
    }
  }
}
```

### Template du formulaire

```html
<!-- src/app/components/formulaire-utilisateur/formulaire-utilisateur.component.html -->
<h2>Ajouter un utilisateur</h2>
<form [formGroup]="formulaireUtilisateur" (ngSubmit)="onSubmit()">
  <label for="nom">Nom :</label>
  <input id="nom" formControlName="nom">
  <div *ngIf="formulaireUtilisateur.get('nom').invalid && formulaireUtilisateur.get('nom').touched">
    <small *ngIf="formulaireUtilisateur.get('nom').errors?.required">Le nom est requis</small>
    <small *ngIf="formulaireUtilisateur.get('nom').errors?.minlength">Le nom doit comporter au moins 3 caractères</small>
  </div>

  <label for="email">Email :</label>
  <input id="email" formControlName="email">
  <div *ngIf="formulaireUtilisateur.get('email').invalid && formulaireUtilisateur.get('email').touched">
    <small *ngIf="formulaireUtilisateur.get('email').errors?.required">L'email est requis</small>
    <small *ngIf="formulaireUtilisateur.get('email').errors?.email">L'email est invalide</small>
  </div>

  <button type="submit" [disabled]="formulaireUtilisateur.invalid">Soumettre</button>
</form>
```

---

## 5. Appels d’API et services HTTP

### Service d’appel d’API : api.service.ts

Ce service effectue des requêtes HTTP pour récupérer et afficher des données d’une API externe.

```typescript
// src/app/services/api.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://jsonplaceholder.typicode.com/users';

  constructor(private http: HttpClient) {}

  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
```

### Utilisation du service dans un composant

Le composant récupère la liste des utilisateurs depuis l'API via le service.

```typescript
// src/app/components/utilisateur/utilisateur.component.ts
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-utilisateur',
  templateUrl: './utilisateur.component.html'
})
export class UtilisateurComponent implements OnInit {
  utilisateurs: any[] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.apiService.getUsers().subscribe((data) => {
      this.utilisateurs = data;
    }, error => {
      console.error("Erreur lors de la récupération des utilisateurs", error);
    });
  }
}
```

### Template pour afficher les utilisateurs

```html
<!-- src/app/components/utilisateur/utilisateur.component.html -->
<h2>Liste des utilisateurs</h2>
<ul>
  <li *ngFor="let utilisateur of utilisateurs">
    {{ utilisateur.name }} - {{ utilisateur.email }}
  </li>
</ul>
```

---

## 6. Observables et subscribe

### Utilisation d’un observable dans un service

L’`ApiService` utilise un observable pour récupérer les données de l'API et `subscribe` pour y souscrire dans le composant.

```typescript
// src/app/services/api.service.ts
export class ApiService {
  private apiUrl = 'https://jsonplaceholder.typicode.com/users';

  constructor(private http: HttpClient) {}

  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}

// src/app/components/utilisateur/utilisateur.component.ts
ngOnInit(): void {
  this.apiService.getUsers().subscribe((data) => {
    this.utilisateurs = data;
  });
}
```

---

## 7. AppModule : app.module.ts

Ajoute les composants et services dans le module principal d'Angular.

```typescript
// src/app/app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { UtilisateurComponent } from './components/utilisateur/utilisateur.component';
import { FormulaireUtilisateurComponent } from './components/formulaire-utilisateur/formulaire-utilisateur.component';
import { ExponentielPipe } from './components/pipes/exponentiel.pipe';
import { ApiService } from './services/api.service';

@NgModule({
  declarations: [
    AppComponent,
    UtilisateurComponent,
    FormulaireUtilisateurComponent,
    ExponentielPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [ApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
```

---

[...retour en arriere](../menu.md)