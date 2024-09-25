# TP Angular – Tuto

[...retour au menu sur Angular](../menu.md)

---

## Table des matières

- [TP Angular – tuto](#tp-angular--tuto)
  - [Table des matières](#table-des-matières)
  - [TP1: Création d'une application Angular et gestion des livres](#tp1-création-dune-application-angular-et-gestion-des-livres)
    - [Étape 1: Créer un projet Angular avec la CLI](#étape-1-créer-un-projet-angular-avec-la-cli)
    - [Étape 2: Créer un modèle Book](#étape-2-créer-un-modèle-book)
    - [Étape 3: Création d'un stub JSON et du service BookService](#étape-3-création-dun-stub-json-et-du-service-bookservice)
    - [Étape 4: Création des composants book-list et book-form](#étape-4-création-des-composants-book-list-et-book-form)
  - [TP2: Améliorations avec MatAngular et gestion des formulaires](#tp2-améliorations-avec-matangular-et-gestion-des-formulaires)
    - [Étape 1: Afficher la date avec un pipe](#étape-1-afficher-la-date-avec-un-pipe)
    - [Étape 2: Méthode `addBook` dans BookService](#étape-2-méthode-addbook-dans-bookservice)
    - [Étape 3: Utiliser MatAngular pour les formulaires](#étape-3-utiliser-matangular-pour-les-formulaires)
  - [TP3: Gestion des routes, API et affichage des détails du livre](#tp3-gestion-des-routes-api-et-affichage-des-détails-du-livre)
    - [Étape 1: Gestion des doublons dans `addBook`](#étape-1-gestion-des-doublons-dans-addbook)
    - [Étape 2: Routage et affichage du détail d'un livre](#étape-2-routage-et-affichage-du-détail-dun-livre)
    - [Étape 3: Charger et ajouter des livres via une API](#étape-3-charger-et-ajouter-des-livres-via-une-api)

---

## TP1: Création d'une application Angular et gestion des livres

### Étape 1: Créer un projet Angular avec la CLI

**Objectif :** Créer une application Angular en utilisant la commande CLI, en spécifiant l'option pour créer un projet avec des modules si nécessaire.

1. **Installation d'Angular CLI** :
   Si ce n'est pas déjà fait, installe Angular CLI via npm :
   ```bash
   npm install -g @angular/cli
   ```

2. **Création du projet avec modules** :
   Par défaut, Angular 17/18 utilise les **composants autonomes**. Si tu souhaites avoir un projet avec `app.module.ts`, utilise l'option `--no-standalone` :

   ```bash
   ng new book-app --no-standalone
   ```
   Cela créera un projet avec la structure modulaire traditionnelle, y compris le fichier `app.module.ts`.

   Si tu préfères les composants autonomes, tu peux simplement exécuter :
   
   ```bash
   ng new book-app
   ```

   Ensuite, entre dans le dossier du projet :
   ```bash
   cd book-app
   ```

3. **Lancement du serveur de développement** :
   Lance le serveur de développement pour visualiser le projet :
   ```bash
   ng serve --open
   ```

---

### Étape 2: Créer un modèle Book

**Objectif :** Créer une interface TypeScript pour représenter un modèle de livre.

1. **Créer le modèle Book** :
   Dans le dossier `src/app`, crée un fichier `book.ts` :
   ```typescript
   export interface Book {
     id: number;
     title: string;
     author: string;
     publicationDate: Date;
   }
   ```

---

### Étape 3: Création d'un stub JSON et du service BookService

**Objectif :** Créer un service Angular pour gérer les livres.

1. **Stub JSON** :
   Crée un fichier `books.json` dans `src/assets/` avec les données suivantes :
   ```json
   [
     { "id": 1, "title": "Book 1", "author": "Author 1", "publicationDate": "2023-01-01" },
     { "id": 2, "title": "Book 2", "author": "Author 2", "publicationDate": "2022-05-15" },
     { "id": 3, "title": "Book 3", "author": "Author 3", "publicationDate": "2021-09-10" }
   ]
   ```

2. **Création du service BookService** :
   Si tu as créé un projet avec modules, génère le service comme suit :
   ```bash
   ng generate service services/book
   ```

   Sinon, si tu utilises des composants autonomes, génère le service normalement.

   Ensuite, dans `book.service.ts`, ajoute le code suivant pour charger les livres depuis le stub JSON :

   ```typescript
   import { Injectable } from '@angular/core';
   import { HttpClient } from '@angular/common/http';
   import { Book } from './book';
   import { Observable } from 'rxjs';

   @Injectable({
     providedIn: 'root'
   })
   export class BookService {
     private booksUrl = 'assets/books.json';  // URL du stub JSON

     constructor(private http: HttpClient) {}

     getAll(): Observable<Book[]> {
       return this.http.get<Book[]>(this.booksUrl);
     }
   }
   ```

3. **Si tu utilises des modules :**
   Dans `app.module.ts`, importe `HttpClientModule` pour que les appels HTTP fonctionnent :
   ```typescript
   import { HttpClientModule } from '@angular/common/http';

   @NgModule({
     declarations: [AppComponent],
     imports: [BrowserModule, HttpClientModule],
     bootstrap: [AppComponent]
   })
   export class AppModule {}
   ```

   Si tu utilises des composants autonomes, il suffit d'importer `HttpClientModule` directement dans les composants nécessaires.

---

### Étape 4: Création des composants book-list et book-form

**Objectif :** Créer une liste de livres et un formulaire d'ajout.

1. **Créer le composant `book-list`** :
   ```bash
   ng generate component book-list --skip-tests --inline-style
   ```

   Ensuite, dans `book-list.component.ts`, utilise `BookService` pour récupérer et afficher les livres :

   ```typescript
   import { Component, OnInit } from '@angular/core';
   import { BookService } from '../services/book.service';
   import { Book } from '../book';

   @Component({
     selector: 'app-book-list',
     templateUrl: './book-list.component.html'
   })
   export class BookListComponent implements OnInit {
     books: Book[] = [];

     constructor(private bookService: BookService) {}

     ngOnInit(): void {
       this.bookService.getAll().subscribe(data => {
         this.books = data;
       });
     }
   }
   ```

   Dans `book-list.component.html` :
   ```html
   <ul>
     <li *ngFor="let book of books">
       {{ book.title }} by {{ book.author }} ({{ book.publicationDate | date:'longDate' }})
     </li>
   </ul>
   ```

2. **BONUS : Créer le composant `book-form`** :
   ```bash
   ng generate component book-form --skip-tests --inline-style
   ```

   Dans `book-form.component.ts`, ajoute un formulaire pour créer un livre et appeler une méthode dans `BookService` pour ajouter ce livre à la liste :

   ```typescript
   import { Component } from '@angular/core';
   import { BookService } from '../services/book.service';
   import { Book } from '../book';

   @Component({
     selector: 'app-book-form',
     templateUrl: './book-form.component.html'
   })
   export class BookFormComponent {
     newBook: Book = { id: 0, title: '', author: '', publicationDate: new Date() };

     constructor(private bookService: BookService) {}

     addBook() {
       this.bookService.addBook(this.newBook);
     }
   }
   ```

   Dans `book-form.component.html` :
   ```html
   <form (ngSubmit)="addBook()">
     <input [(ngModel)]="newBook.title" name="title" placeholder="Title" required>
     <input [(ngModel)]="newBook.author" name="author" placeholder="Author" required>
     <input [(ngModel)]="newBook.publicationDate" name="publicationDate" type="date" required>
     <button type="submit">Add Book</button>
   </form>
   ```
## TP2: Améliorations avec MatAngular et gestion des formulaires

### Étape 1: Afficher la date avec un pipe

Pour afficher la date dans un format spécifique (par exemple : *Mardi 30 octobre 1990*), tu peux utiliser le `DatePipe` dans le template de `book-list.component.html` :

```html
{{ book.publicationDate | date:'EEEE dd MMMM yyyy' }}
```

Le `DatePipe` est intégré dans Angular et permet de formater les dates de différentes manières. Dans cet exemple, nous utilisons le format long avec le jour de la semaine, le jour numérique, le mois complet et l'année.

---

### Étape 2: Méthode `addBook` dans BookService

Dans `book.service.ts`, tu peux ajouter une méthode `addBook` qui va permettre d’ajouter un nouveau livre à la liste des livres dans le service.

Voici comment implémenter cette méthode pour qu'elle génère automatiquement un `id` basé sur l'ID le plus élevé existant :

```typescript
addBook(book: Book): void {
  const maxId = Math.max(...this.books.map(b => b.id), 0);
  book.id = maxId + 1;
  this.books.push(book);
}
```

Cette méthode vérifie d'abord l'ID le plus élevé existant dans la liste des livres, puis assigne à ce nouveau livre un ID unique.

---

### Étape 3: Utiliser MatAngular pour les formulaires

Après avoir installé **Angular Material**, tu peux améliorer ton formulaire avec des composants Material Design pour une interface utilisateur plus agréable.

1. **Installation de Angular Material** :

   Si ce n'est pas déjà fait, installe Angular Material en utilisant la commande suivante :

   ```bash
   ng add @angular/material
   ```

   Angular CLI te demandera de choisir un thème, activer l'animation et d'autres options de configuration.

2. **Ajouter MatInput et MatButton** :

   Ensuite, dans `app.module.ts` (ou directement dans ton composant si tu utilises les composants autonomes), importe les modules nécessaires pour utiliser les champs de saisie (`MatInputModule`), le bouton (`MatButtonModule`), et le sélecteur de dates (`MatDatepickerModule` et `MatNativeDateModule`) :

   ```typescript
   import { MatInputModule } from '@angular/material/input';
   import { MatButtonModule } from '@angular/material/button';
   import { MatDatepickerModule } from '@angular/material/datepicker';
   import { MatNativeDateModule } from '@angular/material/core';

   @NgModule({
     imports: [
       BrowserModule,
       MatInputModule,
       MatButtonModule,
       MatDatepickerModule,
       MatNativeDateModule,
       // autres imports
     ],
     bootstrap: [AppComponent]
   })
   export class AppModule {}
   ```

3. **Modifier le formulaire dans `book-form.component.html`** :

   Mets à jour le formulaire HTML pour utiliser les composants Angular Material :

   ```html
   <form (ngSubmit)="addBook()">
     <mat-form-field>
       <mat-label>Title</mat-label>
       <input matInput [(ngModel)]="newBook.title" name="title" required>
     </mat-form-field>

     <mat-form-field>
       <mat-label>Author</mat-label>
       <input matInput [(ngModel)]="newBook.author" name="author" required>
     </mat-form-field>

     <mat-form-field>
       <mat-label>Publication Date</mat-label>
       <input matInput [matDatepicker]="picker" [(ngModel)]="newBook.publicationDate" name="publicationDate" required>
       <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
       <mat-datepicker #picker></mat-datepicker>
     </mat-form-field>

     <button mat-flat-button color="primary" type="submit">Add Book</button>
   </form>
   ```

   Ce formulaire utilise des composants Material Design pour les champs de texte et le sélecteur de dates, offrant une meilleure expérience utilisateur.

4. **Ajouter un menu avec MatAngular** :

   Tu peux également ajouter un menu de navigation avec Angular Material pour simplifier la navigation entre les pages de l’application :

   Dans `app.component.html`, ajoute ce code pour créer un menu simple :

   ```html
   <mat-menu #appMenu="matMenu">
     <button mat-menu-item (click)="goToBooks()">Liste des livres</button>
     <button mat-menu-item (click)="goToAddBook()">Ajouter un livre</button>
   </mat-menu>

   <button mat-button [matMenuTriggerFor]="appMenu">Menu</button>
   ```

   Et dans `app.component.ts`, ajoute les méthodes associées :

   ```typescript
   export class AppComponent {
     goToBooks() {
       console.log('Liste des livres');
     }

     goToAddBook() {
       console.log('Ajouter un livre');
     }
   }
   ```

Cela met en place un menu simple sans encore toucher au routage, ce qui sera abordé dans le TP3.

---

## TP3: Gestion des routes, API et affichage des détails du livre

### Étape 1: Gestion des doublons dans `addBook`

Pour éviter les doublons dans la liste des livres, modifie la méthode `addBook` dans `BookService` pour vérifier si un livre avec le même titre et le même auteur existe déjà :

```typescript
addBook(book: Book): void {
  const duplicate = this.books.find(b => b.title === book.title && b.author === book.author);
  if (!duplicate) {
    const maxId = Math.max(...this.books.map(b => b.id), 0);
    book.id = maxId + 1;
    this.books.push(book);
  } else {
    console.error('Le livre existe déjà');
  }
}
```

### Étape 2: Routage et affichage du détail d'un livre

1. **Configurer le routage** :

   Dans `app-routing.module.ts`, configure les routes pour l'application :

   ```typescript
   const routes: Routes = [
     { path: '', component: HomeComponent },
     { path: 'books', component: BookListComponent },
     { path: 'book/add', component: BookFormComponent },
     { path: 'book/:id', component: BookDetailComponent },
     { path: '**', redirectTo: '/' }  // Gérer les routes malformées
   ];
   ```

2. **Créer le composant `book-detail`** :

   Génère un nouveau composant `book-detail` pour afficher les détails d'un livre spécifique basé sur l'ID récupéré depuis l'URL :

   ```bash
   ng generate component book-detail --skip-tests --inline-style
   ```

   Dans `book-detail.component.ts`, récupère l’ID du livre à partir de l’URL et utilise `BookService` pour afficher ses détails :

   ```typescript
   import { ActivatedRoute } from '@angular/router';
   import { BookService } from '../services/book.service';
   import { Book } from '../book';

   export class BookDetailComponent implements OnInit {
     book: Book | undefined;

     constructor(private route: ActivatedRoute, private bookService: BookService) {}

     ngOnInit(): void {
       const id = Number(this.route.snapshot.paramMap.get('id'));
       this.book = this.bookService.getAll().find(b => b.id === id);
     }
   }
   ```

   Et dans `book-detail.component.html` :

   ```html
   <div *ngIf="book">
     <h2>{{ book.title }}</h2>
     <p>Author: {{ book.author }}</p>
     <p>Publication Date: {{ book.publicationDate | date:'longDate' }}</p>
   </div>
   <div *ngIf="!book">
     <p>Le livre n'a pas été trouvé.</p>
   </div>
   ```

3. **Ajouter des liens pour afficher les détails des livres** :

   Dans `book-list.component.html`, mets à jour la liste des livres pour qu’elle comporte des liens vers la page de détails :

   ```html
   <ul>
     <li *ngFor="let book of books">
       <a [routerLink]="['/book', book.id]">{{ book.title }}</a> par {{ book.author }}
     </li>
   </ul>
   ```

### Étape 3: Charger et ajouter des livres via une API

1. **Charger les livres depuis une API externe** :

   Pour charger les livres depuis une API distante, modifie `BookService` pour utiliser l'URL suivante : `https://664ba07f35bbda10987d9f99.mockapi.io/api/books`.

   ```typescript
   private apiUrl = 'https://664ba07f35bbda10987d9f99.mockapi.io/api/books';

   getAll(): Observable<Book[]> {
     return this.http.get<Book[]>(this.apiUrl);
   }
   ```

2. **Ajouter un livre via l'API** :

   Modifie également `addBook` pour envoyer une requête POST à l'API lors de l'ajout d’un nouveau livre :

   ```typescript
   addBook(book: Book): Observable<Book> {
     return this.http.post<Book>(this.apiUrl, book);
   }
   ```

   Dans `book-form.component.ts`, appelle cette méthode lors de la validation du formulaire :

   ```typescript
   addBook(): void {
     this.bookService.addBook(this.newBook).subscribe(
       (newBook) => console.log('Livre ajouté :', newBook),
       (...newBook) => console.log('Livre ajouté :', newBook),
       (error) => console.error('Erreur lors de l\'ajout du livre :', error)
     );
   }
   ```

Cette méthode envoie une requête POST à l'API pour ajouter un nouveau livre et gère la réponse ou les erreurs via des `Observable`.

### BONUS : Gérer les routes malformées/fausses

Pour gérer les routes qui ne sont pas valides ou inexistantes, ajoute une route "wildcard" à la fin de ta configuration de routage dans `app-routing.module.ts` :

```typescript
{ path: '**', redirectTo: '/' }  // Redirige vers la page d'accueil pour les routes non valides
```

Cela redirigera automatiquement toute URL incorrecte vers la page d'accueil ou une autre page de ton choix, évitant ainsi les erreurs 404.

---

[...retour au menu sur Angular](../menu.md)
