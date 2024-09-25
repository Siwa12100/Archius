# TP Angular – tuto

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

**Objectif :** Créer une application Angular en utilisant la commande CLI.

1. **Installation d'Angular CLI** :
   Si ce n'est pas déjà fait, installe Angular CLI via npm :
   ```bash
   npm install -g @angular/cli
   ```

2. **Création du projet** :
   Utilise la commande suivante pour générer un nouveau projet :
   ```bash
   ng new book-app
   ```
   Réponds aux questions de configuration et entre dans le dossier du projet :
   ```bash
   cd book-app
   ```

3. **Lancement du serveur de développement** :
   Lance le serveur de développement pour visualiser le projet :
   ```bash
   ng serve --open
   ```
   Cela ouvrira automatiquement ton application dans un navigateur.

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
   Ce modèle sera utilisé dans toute l'application pour manipuler les livres.

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
   Crée le service BookService :
   ```bash
   ng generate service book
   ```

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

3. **Importer `HttpClientModule`** dans `app.module.ts` pour que les appels HTTP fonctionnent :
   ```typescript
   import { HttpClientModule } from '@angular/common/http';

   @NgModule({
     declarations: [AppComponent],
     imports: [BrowserModule, HttpClientModule],
     bootstrap: [AppComponent]
   })
   export class AppModule {}
   ```

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
   import { BookService } from '../book.service';
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
   import { BookService } from '../book.service';
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

---

## TP2: Améliorations avec MatAngular et gestion des formulaires

### Étape 1: Afficher la date avec un pipe

Pour afficher la date dans un format spécifique (ex: Mardi 30 octobre 1990), utilise le `DatePipe` dans le template de `book-list.component.html` :

```html
{{ book.publicationDate | date:'EEEE dd MMMM yyyy' }}
```

---

### Étape 2: Méthode `addBook` dans BookService

Dans `book.service.ts`, ajoute la méthode `addBook` qui ajoute un nouveau livre à la liste `books`.

```typescript
addBook(book: Book): void {
  const maxId = Math.max(...this.books.map(b => b.id), 0);
  book.id = maxId + 1;
  this.books.push(book);
}
```

---

### Étape 3: Utiliser MatAngular pour les formulaires

Après avoir installé **MatAngular**, tu peux améliorer ton formulaire avec des composants Material Design.

1. **Ajouter MatInput et MatButton** :

   Commence par importer les modules nécessaires dans `app.module.ts` :
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

2. **Mettre à jour le formulaire dans `book-form.component.html`** pour utiliser les composants MatAngular :

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

3. **Ajout du menu MatAngular** :
   Crée un menu avec deux liens : Liste des livres et Ajouter un livre. Cela te permettra d’avoir une navigation basique sans encore utiliser le routage.

   Dans `app.component.html` :
   ```html
   <mat-menu #appMenu="matMenu">
     <button mat-menu-item (click)="goToBooks()">Liste des livres</button>
     <button mat-menu-item (click)="goToAddBook()">Ajouter un livre</button>
   </mat-menu>

   <button mat-button [matMenuTriggerFor]="appMenu">Menu</button>
   ```

   Ensuite, ajoute des méthodes dans `app.component.ts` pour gérer les actions :
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

---

## TP3: Gestion des routes, API et affichage des détails du livre

### Étape 1: Gestion des doublons dans `addBook`

Modifie la méthode `addBook` dans `BookService` pour vérifier si un livre avec le même titre et le même auteur existe déjà avant de l'ajouter :

```typescript
addBook(book: Book): void {
  const duplicate = this.books.find(b => b.title === book.title && b.author === book.author);
  if (!duplicate) {
    const maxId = Math.max(...this.books.map(b => b.id), 0);
    book.id = maxId + 1;
    this.books.push(book);
  } else {
    console.error('Book already exists');
  }
}
```

### Étape 2: Routage et affichage du détail d'un livre

1. **Configurer le routage** :

   Dans `app-routing.module.ts`, configure les routes comme demandé :
   ```typescript
   const routes: Routes = [
     { path: '', component: HomeComponent },
     { path: 'books', component: BookListComponent },
     { path: 'book/add', component: BookFormComponent },
     { path: 'book/:id', component: BookDetailComponent },
     { path: '**', redirectTo: '/' }  // Gérer les routes malformées
   ];
   ```

2. **Créer le composant `book-detail`** pour afficher les détails d'un livre via l'ID :

   ```bash
   ng generate component book-detail --skip-tests --inline-style
   ```

   Dans `book-detail.component.ts`, récupère l’ID du livre à partir de l’URL :
   ```typescript
   import { ActivatedRoute } from '@angular/router';
   import { BookService } from '../book.service';

   export class BookDetailComponent implements OnInit {
     book: Book | undefined;

     constructor(private route: ActivatedRoute, private bookService: BookService) {}

     ngOnInit(): void {
       const id = Number(this.route.snapshot.paramMap.get('id'));
       this.book = this.bookService.getAll().find(b => b.id === id);
     }
   }
   ```

   Le template `book-detail.component.html` :
   ```html
   <div *ngIf="book">
     <h2>{{ book.title }}</h2>
     <p>Author: {{ book.author }}</p>
     <p>Publication Date: {{ book.publicationDate | date:'longDate' }}</p>
   </div>
   <div *ngIf="!book">
     <p>Book not found.</p>
   </div>
   ```

3. **Ajouter des liens dans la liste des livres pour afficher le détail d'un livre** :

   Dans `book-list.component.html` :
   ```html
   <ul>
     <li *ngFor="let book of books">
       <a [routerLink]="['/book', book.id]">{{ book.title }}</a> by {{ book.author }}
     </li>
   </ul>
   ```

### Étape 3: Charger et ajouter des livres via une API

1. **Charger les livres depuis une API** :

   Modifie `BookService` pour charger les livres depuis l'API :
   ```typescript
   private apiUrl = 'https://664ba07f35bbda10987d9f99.mockapi.io/api/books';

   getAll(): Observable<Book[]> {
     return this.http.get<Book[]>(this.apiUrl);
   }
   ```

2. **Ajouter un livre via l'API** :

   Dans `BookService`, modifie la méthode `addBook` pour envoyer une requête POST à l'API :
   ```typescript
   addBook(book: Book): Observable<Book> {
     return this.http.post<Book>(this.apiUrl, book);
   }
   ```

   Dans `book-form.component.ts`, appelle cette méthode lors de la validation du formulaire :
   ```typescript
   addBook(): void {
     this.bookService.addBook(this.newBook).subscribe(
       (newBook) => console.log('Book added:', newBook),
       (error) => console.error('Error:', error)
     );
   }
   ```

---

[...retour au menu sur Angular](../menu.md)