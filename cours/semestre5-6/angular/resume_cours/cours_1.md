# Cours 1 Angular 17 - Résumé

[...retour en arriere](../menu.md)

---

## 1. Introduction à Angular

### 1.1. Qu’est-ce qu’Angular ?
Angular est un **framework open-source** maintenu par Google. Il permet de créer des applications web dynamiques appelées **Single Page Applications (SPA)**. Angular utilise **TypeScript** comme langage principal et repose sur un environnement **Node.js** pour la compilation et le développement.

Caractéristiques principales :
- Utilise HTML, CSS, et TypeScript.
- Suit une architecture basée sur les **composants** et les **modules**.
- Angular compile le code pour l’optimiser (via AoT ou JIT compilation).
- Facilité de tests automatisés grâce à la structure modulaire.
  
Exemple de création d’une simple application Angular :
```bash
ng new mon-projet-angular
```

### 1.2. Versions d'Angular
- **AngularJS (2010)** : première version, écrite en JavaScript pur.
- **Angular 2 (2016)** : une réécriture complète utilisant TypeScript.
- Depuis, les versions sont régulièrement mises à jour avec Angular 17 étant la plus récente.

Angular 17 inclut :
- Un ensemble de bibliothèques de base : `@angular/core`, `@angular/router`, etc.
- Un environnement optimisé pour le développement d'applications complexes.

---

## 2. Architecture d'une application Angular

Une application Angular suit une architecture modulaire divisée en **front-end** et **back-end**.

### 2.1. Structure de base
Le front-end se compose des **composants**, **templates** (HTML), **styles** (CSS), et de la **logique de présentation**. Le back-end repose souvent sur une API qui fournit les données.

### 2.2. L'architecture complète
- **Composants** : élément fondamental de l'interface utilisateur.
- **Modules** : groupements de composants.
- **Services** : logique métier et accès aux données.
- **Routing** : navigation entre différentes vues de l’application.
  
---

## 3. Créer un projet Angular

### 3.1. Environnement de travail
Angular utilise **Node.js** pour installer et exécuter des outils via `npm`.

- **IDE recommandé** : Visual Studio Code.
- **Commandes essentielles** :
  - `ng new mon-projet` : crée un nouveau projet Angular.
  - `npm start` ou `ng serve` : lance un serveur de développement.

### 3.2. Structure d’un projet Angular
Un projet Angular est structuré comme suit :
- **src/** : contient le code source de l’application.
- **app/** : contient les composants et la logique applicative.
- **assets/** : contient les ressources (images, styles, etc.).
- **index.html** : page HTML principale.
- **main.ts** : point d'entrée de l'application.
  
---

## 4. TypeScript et Angular

Angular repose sur **TypeScript**, un sur-ensemble de JavaScript avec typage fort et des fonctionnalités orientées objet.

### 4.1. Présentation de TypeScript
- Les fichiers **.ts** sont compilés en **JavaScript**.
- TypeScript permet de détecter les erreurs au moment de la compilation grâce à un typage fort.
  
### 4.2. Types dans TypeScript
TypeScript supporte plusieurs types basiques :
- `number`
- `boolean`
- `string`
- `array`
- `object`

Exemple :
```typescript
let nom: string = "Angular";
let nombre: number = 17;
let tableau: number[] = [1, 2, 3];
```

### 4.3. Les interfaces
Les **interfaces** définissent les structures d'objets et les contrats dans le code.

Exemple d’interface :
```typescript
interface Utilisateur {
  id: number;
  nom: string;
}
let utilisateur1: Utilisateur = { id: 1, nom: "John Doe" };
```

---

## 5. Utilisation d'Angular

### 5.1. Les composants
Les composants sont la brique fondamentale d’une application Angular. Ils sont constitués de :
- **Classe TypeScript** : contenant la logique métier.
- **Template HTML** : définissant la vue.
- **Styles CSS** : définissant l'apparence du composant.

Exemple de composant :
```typescript
@Component({
  selector: 'app-exemple',
  templateUrl: './exemple.component.html',
  styleUrls: ['./exemple.component.css']
})
export class ExempleComponent {
  titre: string = 'Hello, Angular!';
}
```

### 5.2. Le binding
Le **binding** permet de lier les données entre la vue et le modèle.

#### 5.2.1. Interpolation de texte
Utilisé pour afficher des données dans le template.
```html
<p>{{ titre }}</p>
```

#### 5.2.2. Property binding
Liaison d'une propriété HTML à une valeur dans le composant.
```html
<img [src]="imageUrl">
```

#### 5.2.3. Event binding
Liaison d’un événement à une méthode du composant.
```html
<button (click)="onClick()">Cliquez-moi</button>
```

#### 5.2.4. Two-way binding
Pour synchroniser des données entre le modèle et la vue.
```html
<input [(ngModel)]="nom">
```

### 5.3. Les directives
Les **directives** modifient le comportement des éléments HTML.

#### 5.3.1. Directives structurelles
Exemples :
- **ngIf** : affiche ou masque un élément.
  ```html
  <p *ngIf="condition">Texte conditionnel</p>
  ```
- **ngFor** : itère sur une collection.
  ```html
  <ul>
    <li *ngFor="let item of items">{{ item }}</li>
  </ul>
  ```

### 5.4. L’injection de dépendances
L’injection de dépendances est utilisée pour fournir des services à travers l’application.

Exemple de service :
```typescript
@Injectable({
  providedIn: 'root',
})
export class ExempleService {
  getData(): string {
    return 'Données du service';
  }
}
```

Pour utiliser un service dans un composant :
```typescript
constructor(private exempleService: ExempleService) {}

ngOnInit() {
  console.log(this.exempleService.getData());
}
```

---

## Conclusion

Angular 17 est un framework robuste et complet pour le développement d'applications web modernes. Maîtriser les concepts de **composants**, **binding**, **directives**, et **services** est essentiel pour créer des applications performantes et maintenables.

Pour aller plus loin :
- Explorez **RxJS** pour gérer des flux de données asynchrones.
- Apprenez à créer et manipuler des **modules Angular**.
- Mettez en place des **tests unitaires** pour assurer la fiabilité de votre application.

---

[...retour en arriere](../menu.md)