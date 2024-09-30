# Création, Utilisation, et Intégration des Composants en Angular 18 (Standalone)

[...retour menu sur Angular](../../angular/menu.md)

---

## 1. **Création d'un Composant avec la CLI Angular**

Avec Angular 18, les composants peuvent être créés en tant que **standalone components** (indépendants), ce qui signifie qu'ils ne sont plus nécessairement liés à un `NgModule`. Tu peux générer des composants à l'aide de la **CLI Angular**.

### Commande pour créer un composant standalone :
```bash
ng generate component components/mon-composant --skip-tests --inline-style --standalone
```

### Options importantes :
- **`--skip-tests`** : Ne crée pas de fichier de test (`.spec.ts`).
- **`--inline-style`** : Le CSS sera directement intégré dans le fichier TypeScript, plutôt que dans un fichier séparé `.css`.
- **`--standalone`** : Crée un composant standalone, indépendant de tout `NgModule`.

### Résultat de la commande :
Cela génère un fichier `mon-composant.component.ts` dans le dossier `components` sans fichier de test et avec un style intégré directement dans le fichier TypeScript.

---

## 2. **Anatomie d'un Composant Standalone en Angular 18**

Voici les éléments qui constituent un composant standalone :

### Exemple de fichier `mon-composant.component.ts` :
```typescript
import { Component } from '@angular/core';

@Component({
  selector: 'app-mon-composant',  // Le sélecteur HTML pour utiliser ce composant
  standalone: true,               // Indique que ce composant est standalone
  template: `<h2>Bonjour, je suis un composant standalone!</h2>`, // Template inline
  styles: [`h2 { color: blue; }`]  // Styles inline
})
export class MonComposant {
  // Logique du composant
  message: string = 'Composant standalone avec Angular 18';
}
```

### Explication des parties du composant :
1. **`selector`** : Le sélecteur HTML qui sera utilisé pour insérer le composant dans un template parent.
2. **`standalone`** : Déclare que ce composant est indépendant et ne fait pas partie d'un `NgModule`.
3. **`template`** : Contient le code HTML du composant (peut être inline ou dans un fichier séparé).
4. **`styles`** : Définit les styles CSS du composant (peut être inline ou dans un fichier `.css` séparé).
5. **`class`** : La classe TypeScript où tu définis les propriétés et méthodes du composant.

---

## 3. **Les Fichiers Associés à un Composant**

Quand tu crées un composant avec Angular (sauf si tu utilises `--inline-style` ou `--inline-template`), plusieurs fichiers sont générés :

1. **Fichier TypeScript (`.ts`)** : Contient la classe du composant et sa logique.
2. **Fichier HTML (`.html`)** : Contient le template (l'interface utilisateur).
3. **Fichier CSS/SCSS (`.css` ou `.scss`)** : Contient les styles appliqués uniquement à ce composant.

### Exemple d'un composant avec des fichiers séparés :
```typescript
import { Component } from '@angular/core';

@Component({
  selector: 'app-liste-livres',
  standalone: true,
  templateUrl: './liste-livres.component.html',  // Le template est dans un fichier séparé
  styleUrls: ['./liste-livres.component.css']    // Les styles sont dans un fichier séparé
})
export class ListeLivresComponent {
  livres = ['Livre 1', 'Livre 2', 'Livre 3'];
}
```

- **`liste-livres.component.ts`** : Contient la logique du composant.
- **`liste-livres.component.html`** : Contient le template HTML.
- **`liste-livres.component.css`** : Contient les styles CSS.

---

## 4. **Les Modules et Composants en Angular 18**

En **Angular 18**, les composants standalone n'ont pas besoin d'être déclarés dans un `NgModule`. Cela simplifie leur utilisation, car tu peux les importer directement dans d'autres composants, services, ou modules sans les ajouter dans la section `declarations` d'un module.

### Comment importer un composant standalone dans un autre composant ?

Si tu veux utiliser un composant standalone comme `ListeLivresComponent` dans un autre composant (par exemple `AppComponent`), il suffit de l'importer dans la section `imports` du décorateur `@Component`.

#### Exemple d'intégration :
```typescript
import { Component } from '@angular/core';
import { ListeLivresComponent } from './components/liste-livres/liste-livres.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ListeLivresComponent],  // Import du composant standalone
  template: `
    <h1>Mon Application</h1>
    <liste-livres></liste-livres>  <!-- Utilisation du sélecteur du composant -->
  `,
  styleUrls: ['./app.component.css']
})
export class AppComponent {}
```

### Intégration dans le template :
Dans le fichier `app.component.html`, tu peux maintenant utiliser le composant via son **sélecteur**.

```html
<h1>Bienvenue sur mon application !</h1>
<liste-livres></liste-livres>  <!-- Sélecteur du composant ListeLivres -->
```

---

## 5. **Services et Injection de Dépendances dans un Composant Standalone**

Les **services** sont des objets réutilisables qui contiennent la logique métier, comme la récupération de données depuis une API ou la gestion de l'état global de l'application. Ils sont injectés dans les composants via l'**injection de dépendances**.

### Création d’un service avec la CLI :
```bash
ng generate service services/livre --skip-tests --flat
```

### Exemple d’un service `LivreService` :
```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'  // Le service est disponible dans toute l'application
})
export class LivreService {
  private apiUrl = 'https://api.example.com/livres';

  constructor(private http: HttpClient) {}

  getLivres(): Observable<any> {
    return this.http.get(this.apiUrl);
  }
}
```

### Utilisation d’un service dans un composant :
Pour utiliser ce service dans un composant standalone comme `ListeLivresComponent`, il suffit de l’injecter dans le constructeur.

#### Exemple d’injection de service :
```typescript
import { Component, OnInit } from '@angular/core';
import { LivreService } from '../services/livre.service';

@Component({
  selector: 'liste-livres',
  standalone: true,
  templateUrl: './liste-livres.component.html',
  styleUrls: ['./liste-livres.component.css']
})
export class ListeLivresComponent implements OnInit {
  livres: any[] = [];

  constructor(private livreService: LivreService) {}

  ngOnInit(): void {
    this.livreService.getLivres().subscribe(data => {
      this.livres = data;
    });
  }
}
```

### Intégration du service dans le composant :
Dans ce cas, le `LivreService` est injecté directement dans le composant via le **constructeur**. Lors de l'initialisation du composant, il appelle le service pour récupérer les livres.

---

## 6. **Standalone Components et Angular Modules**

Même si les composants sont standalone, tu peux toujours les intégrer dans un `NgModule` si nécessaire, par exemple dans des contextes spécifiques où les modules sont encore utiles pour organiser le code (comme les modules de fonctionnalités).

#### Exemple d'intégration dans un module :
```typescript
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListeLivresComponent } from './components/liste-livres/liste-livres.component';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ListeLivresComponent  // Le composant standalone est importé ici
  ]
})
export class LivreModule {}
```

Dans cet exemple, même si `ListeLivresComponent` est standalone, tu peux l'importer dans un `NgModule` comme un module de fonctionnalités.

---

## 7. **Bonnes Pratiques pour Structurer un Projet Angular avec Standalone Components**

### Organisation des fichiers et dossiers :
Voici une structure recommandée pour un projet Angular bien organisé avec des composants standalone.

```
src/
  app/
    components/
      liste-livres/
        liste-livres.component.ts
        liste-livres.component.html
        liste-livres.component.css
    services/
      livre.service.ts
    app.component.ts
    app.component.html
    app.component.css
```

### Utiliser des **services** dans des dossiers

 séparés :
- Place tes services dans un dossier `services/` pour mieux organiser la logique métier.
- Les **composants** sont dans des dossiers `components/`, chaque composant ayant son propre sous-dossier pour contenir les fichiers associés (TS, HTML, CSS).

### Standalone components et modularité :
- Les **standalone components** simplifient l'architecture, mais tu peux toujours regrouper des composants dans des modules si cela aide à organiser l'application.
- Utilise l'injection de dépendances pour éviter de dupliquer la logique métier dans les composants.

---

## Conclusion

Les **composants standalone** introduits dans **Angular 18** simplifient la gestion et l'intégration des composants, en les rendant indépendants des modules. Cette approche réduit la complexité des projets tout en permettant une modularité plus flexible.

Cette documentation te permet de :
- **Créer des composants** avec la CLI et comprendre leur structure.
- **Utiliser et intégrer des composants standalone** dans d'autres parties de l'application.
- **Organiser ton projet** avec des services, des composants, et des modules bien structurés.

En suivant ces bonnes pratiques, tu seras capable de développer des applications Angular modernes et bien organisées.