### Utilisation de la CLI en Angular 18

[...retour menu sur Angular](../menu.md)

L’**Angular CLI** (Command Line Interface) est un outil puissant qui permet de créer, gérer, et développer des projets Angular de manière rapide et efficace. Voici un guide pour comprendre comment l’utiliser pour créer un projet, installer des dépendances, et générer des services et des composants de manière structurée.

---

## 1. **Créer un nouveau projet Angular**

Pour créer un nouveau projet Angular, commence par installer la CLI globale d’Angular si ce n’est pas encore fait :

```bash
npm install -g @angular/cli
```

Ensuite, pour créer un nouveau projet :

```bash
ng new nom-du-projet
```

### Options à l’installation :
Lors de la création du projet, Angular te pose quelques questions. Voici quelques réponses recommandées pour un projet simple :
- **Would you like to add Angular routing?** Répondre `y` ou `n` selon tes besoins.
- **Which stylesheet format would you like to use?** Choisis parmi `CSS`, `SCSS`, `Sass`, `Less` ou `Stylus` selon ta préférence.

### Exemple :
```bash
ng new mon-projet
```

Une fois le projet créé, navigue dans le dossier du projet :
```bash
cd mon-projet
```

---

## 2. **Installer des dépendances**

Lors de la création d'un projet avec `ng new`, les dépendances de base d’Angular sont automatiquement installées. Cependant, tu peux installer d’autres dépendances avec `npm` ou `yarn`.

### Exemple :
Pour installer **`@angular/material`** (Angular Material) :

```bash
ng add @angular/material
```

Si tu souhaites ajouter une autre bibliothèque non spécifique à Angular, comme `lodash` :

```bash
npm install lodash
```

---

## 3. **Générer un service avec la CLI**

Pour générer un service dans un dossier spécifique, comme `services`, utilise la commande suivante :

```bash
ng generate service services/mon-service --skip-tests
```

### Options importantes pour générer un service :
- **`--skip-tests`** : Pour ne pas générer de fichier de test pour ce service.
- **`--flat`** : Si tu ne veux pas créer un dossier séparé pour le service (génère seulement le fichier `.service.ts` dans le dossier indiqué).

### Exemple :
```bash
ng generate service services/utilisateur --skip-tests --flat
```

Cela va créer un fichier `utilisateur.service.ts` directement dans le dossier `services` sans créer de dossier séparé pour le service.

---

## 4. **Générer un composant avec la CLI**

Pour générer un composant, tu peux utiliser la commande suivante. Par exemple, si tu veux créer un composant dans un dossier `components` :

```bash
ng generate component components/mon-composant --skip-tests --inline-style
```

### Options importantes pour générer un composant :
- **`--skip-tests`** : Pour ne pas générer de fichier de test pour le composant.
- **`--inline-style`** : Pour ne pas créer de fichier CSS, mais plutôt intégrer le style directement dans le composant.
- **`--inline-template`** : Si tu veux éviter de créer un fichier `.html` séparé et inclure le template directement dans le fichier TypeScript.

### Exemple :
```bash
ng generate component components/liste-livres --skip-tests --inline-style
```

Cela va créer les fichiers nécessaires pour un composant `liste-livres`, mais sans le fichier de test ni de CSS. Le style sera intégré directement dans le fichier `.ts`.

---

## 5. **Organiser les services et les composants générés**

Pour bien structurer ton projet, il est recommandé d'organiser tes services et composants dans des répertoires spécifiques.

### Organisation des dossiers :
```
src/
  app/
    services/
      utilisateur.service.ts
    components/
      liste-livres/
        liste-livres.component.ts
        liste-livres.component.html
```

- Les **services** sont placés dans un dossier `services/` pour centraliser la logique métier et les appels à des APIs.
- Les **composants** sont organisés dans des dossiers `components/` ou selon des **modules de fonctionnalités** pour organiser les vues et la logique de l'interface utilisateur.

---

## 6. **Importer et utiliser les services et composants dans Angular 18**

### 6.1. **Importer et utiliser un service**

Après avoir généré un service, tu dois l’importer dans les composants ou services qui en ont besoin, puis l'injecter via le constructeur.

#### Exemple d’utilisation d’un service :

Supposons que tu aies généré un service `UtilisateurService` dans `services/utilisateur.service.ts`.

```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UtilisateurService {
  private apiUrl = 'https://api.example.com/utilisateurs';

  constructor(private http: HttpClient) {}

  getUtilisateurs(): Observable<any> {
    return this.http.get(this.apiUrl);
  }
}
```

Dans un composant, tu peux maintenant injecter ce service pour l'utiliser :

```typescript
import { Component, OnInit } from '@angular/core';
import { UtilisateurService } from '../services/utilisateur.service';

@Component({
  selector: 'app-liste-utilisateurs',
  templateUrl: './liste-utilisateurs.component.html',
  standalone: true
})
export class ListeUtilisateursComponent implements OnInit {

  utilisateurs: any[] = [];

  constructor(private utilisateurService: UtilisateurService) {}

  ngOnInit(): void {
    this.utilisateurService.getUtilisateurs().subscribe((data) => {
      this.utilisateurs = data;
    });
  }
}
```

### 6.2. **Importer et utiliser un composant**

Avec Angular 18 et les **standalone components**, tu dois importer explicitement les composants dans chaque fichier où tu souhaites les utiliser. Par exemple, si tu veux afficher ton `ListeLivresComponent` dans un autre composant, il te suffit de l'importer comme ceci :

```typescript
import { Component } from '@angular/core';
import { ListeLivresComponent } from './components/liste-livres/liste-livres.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ListeLivresComponent], // Import du composant standalone
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  titre = 'Application Angular';
}
```

Dans le fichier `app.component.html`, tu peux alors utiliser le sélecteur du composant :

```html
<h1>{{ titre }}</h1>
<liste-livres></liste-livres>
```

---

[...retour menu sur Angular](../menu.md)