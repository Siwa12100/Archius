# Illustration du résumé de cours 1 

[...retour en arriere](../menu.md)

---

## Structure du projet

Voici une structure simplifiée du projet Angular que nous allons créer :

```
/src
 ├── /app
 |    ├── /services
 |    |    └── utilisateur.service.ts
 |    ├── /components
 |    |    ├── utilisateur
 |    |    |   ├── utilisateur.component.ts
 |    |    |   └── utilisateur.component.html
 |    |    └── formulaire-utilisateur
 |    |        ├── formulaire-utilisateur.component.ts
 |    |        └── formulaire-utilisateur.component.html
 |    ├── app.module.ts
 |    ├── app.component.ts
 |    └── app.component.html
 ├── /assets
 └── index.html
```

## 1. Création d'un projet Angular

1. Crée le projet Angular en utilisant la commande suivante :
   ```bash
   ng new projet-angular17-demo
   ```

2. Une fois le projet créé, accède à son répertoire :
   ```bash
   cd projet-angular17-demo
   ```

## 2. Service : utilisateur.service.ts

Le service `UtilisateurService` gère les données des utilisateurs.

```typescript
// src/app/services/utilisateur.service.ts
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilisateurService {
  utilisateurs = [
    { id: 1, nom: 'John Doe', email: 'john@example.com' },
    { id: 2, nom: 'Jane Smith', email: 'jane@example.com' }
  ];

  getUtilisateurs() {
    return this.utilisateurs;
  }

  ajouterUtilisateur(utilisateur: { nom: string, email: string }) {
    const nouvelUtilisateur = {
      id: this.utilisateurs.length + 1,
      ...utilisateur
    };
    this.utilisateurs.push(nouvelUtilisateur);
  }
}
```

## 3. Composant : utilisateur.component.ts

Ce composant affiche la liste des utilisateurs récupérés via le service `UtilisateurService`.

```typescript
// src/app/components/utilisateur/utilisateur.component.ts
import { Component, OnInit } from '@angular/core';
import { UtilisateurService } from '../../services/utilisateur.service';

@Component({
  selector: 'app-utilisateur',
  templateUrl: './utilisateur.component.html'
})
export class UtilisateurComponent implements OnInit {
  utilisateurs: any[] = [];

  constructor(private utilisateurService: UtilisateurService) {}

  ngOnInit() {
    this.utilisateurs = this.utilisateurService.getUtilisateurs();
  }
}
```

## 4. Template : utilisateur.component.html

Le template utilise `ngFor` pour afficher la liste des utilisateurs.

```html
<!-- src/app/components/utilisateur/utilisateur.component.html -->
<h2>Liste des utilisateurs</h2>
<ul>
  <li *ngFor="let utilisateur of utilisateurs">
    {{ utilisateur.nom }} - {{ utilisateur.email }}
  </li>
</ul>
```

## 5. Formulaire réactif : formulaire-utilisateur.component.ts

Ce composant contient un formulaire réactif pour ajouter un nouvel utilisateur.

```typescript
// src/app/components/formulaire-utilisateur/formulaire-utilisateur.component.ts
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UtilisateurService } from '../../services/utilisateur.service';

@Component({
  selector: 'app-formulaire-utilisateur',
  templateUrl: './formulaire-utilisateur.component.html'
})
export class FormulaireUtilisateurComponent {
  formulaireUtilisateur: FormGroup;

  constructor(private fb: FormBuilder, private utilisateurService: UtilisateurService) {
    this.formulaireUtilisateur = this.fb.group({
      nom: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit() {
    if (this.formulaireUtilisateur.valid) {
      this.utilisateurService.ajouterUtilisateur(this.formulaireUtilisateur.value);
      this.formulaireUtilisateur.reset();
    }
  }
}
```

## 6. Template du formulaire : formulaire-utilisateur.component.html

Ce template inclut un formulaire réactif avec la gestion des erreurs et du binding.

```html
<!-- src/app/components/formulaire-utilisateur/formulaire-utilisateur.component.html -->
<h2>Ajouter un utilisateur</h2>
<form [formGroup]="formulaireUtilisateur" (ngSubmit)="onSubmit()">
  <label for="nom">Nom :</label>
  <input id="nom" formControlName="nom">
  <div *ngIf="formulaireUtilisateur.get('nom').invalid && formulaireUtilisateur.get('nom').touched">
    <small *ngIf="formulaireUtilisateur.get('nom').errors?.required">Nom requis</small>
    <small *ngIf="formulaireUtilisateur.get('nom').errors?.minlength">Le nom doit avoir au moins 3 caractères</small>
  </div>

  <label for="email">Email :</label>
  <input id="email" formControlName="email">
  <div *ngIf="formulaireUtilisateur.get('email').invalid && formulaireUtilisateur.get('email').touched">
    <small *ngIf="formulaireUtilisateur.get('email').errors?.required">Email requis</small>
    <small *ngIf="formulaireUtilisateur.get('email').errors?.email">Email invalide</small>
  </div>

  <button type="submit" [disabled]="formulaireUtilisateur.invalid">Ajouter</button>
</form>
```

## 7. AppModule : app.module.ts

Assure-toi d’importer les modules nécessaires dans ton fichier `app.module.ts`.

```typescript
// src/app/app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { UtilisateurComponent } from './components/utilisateur/utilisateur.component';
import { FormulaireUtilisateurComponent } from './components/formulaire-utilisateur/formulaire-utilisateur.component';
import { UtilisateurService } from './services/utilisateur.service';

@NgModule({
  declarations: [
    AppComponent,
    UtilisateurComponent,
    FormulaireUtilisateurComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule
  ],
  providers: [UtilisateurService],
  bootstrap: [AppComponent]
})
export class AppModule { }
```

## 8. AppComponent : app.component.ts

Le composant principal `AppComponent` sert de point d'entrée à l'application.

```typescript
// src/app/app.component.ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  titre = 'Projet Angular 17 - Gestion des utilisateurs';
}
```

### 9. Template principal : app.component.html

Affiche la liste des utilisateurs et le formulaire d’ajout.

```html
<!-- src/app/app.component.html -->
<h1>{{ titre }}</h1>
<app-utilisateur></app-utilisateur>
<app-formulaire-utilisateur></app-formulaire-utilisateur>
```

### 10. Exemple final

L'exécution de ce projet permet :
- D'afficher une liste d'utilisateurs avec **ngFor**.
- D'ajouter un nouvel utilisateur via un formulaire réactif.
- D'utiliser des **services** pour la gestion des données.
- D'intégrer des **directives** et des **bindings** pour interagir avec la vue.

---

Exécuter avec `ng serve` pour voir le rendu dans le navigateur?

---

[...retour en arriere](../menu.md)