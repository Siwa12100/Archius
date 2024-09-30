# `Observable<any>`, Appels Asynchrones, Directives, Attributs des Composants et Cycle de Vie des Composants en Angular

[...retour menu sur Angular](../menu.md)

---

## 1. **Observable et Appels Asynchrones en Angular**

### 1.1. **Qu'est-ce qu'un `Observable` ?**

Un **`Observable`** est un **pattern** de programmation réactive fourni par la bibliothèque **RxJS** (Reactive Extensions for JavaScript). Il est utilisé pour gérer des flux de données **asynchrones**, comme les réponses d’API ou des événements utilisateur, et est omniprésent dans les projets Angular, notamment pour les requêtes HTTP.

### 1.2. **D'où vient `Observable<any>` ?**

L’`Observable` provient de **RxJS** et est intégré nativement dans Angular. Il est utilisé avec le module **`HttpClient`** pour gérer les requêtes HTTP. Le type générique `<any>` permet de définir le type de données que l'`Observable` émettra. Dans les projets professionnels, il est conseillé d'utiliser des types spécifiques pour assurer la sécurité du typage.

#### Exemple d'importation :
```typescript
import { Observable } from 'rxjs';
```

### 1.3. **Appels Asynchrones avec `HttpClient` et `Observable`**

Lorsque tu fais des appels asynchrones avec **`HttpClient`** en Angular, tu reçois un `Observable`. Cet `Observable` émet une valeur lorsque les données sont prêtes (par exemple, lorsque l'appel HTTP est complété). Les composants Angular souscrivent à ces `Observable` pour recevoir et traiter les données.

#### Exemple d'un service Angular qui fait un appel HTTP asynchrone :
```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Livre } from '../modeles/livre';

@Injectable({
  providedIn: 'root'
})
export class LivreService {
  private apiUrl = 'https://api.example.com/livres';

  constructor(private http: HttpClient) {}

  getLivres(): Observable<Livre[]> {
    return this.http.get<Livre[]>(this.apiUrl);  // Retourne un Observable de tableau de Livre
  }
}
```

- **Le service `LivreService`** fait une requête HTTP GET à une API et retourne un `Observable<Livre[]>`.
- Le composant peut **souscrire** à cet `Observable` pour recevoir la liste des livres dès qu'elle est disponible.

### 1.4. **Souscription à un `Observable` dans un Composant**

Dans un composant, la souscription (`subscribe()`) à un `Observable` est souvent faite dans la méthode **`ngOnInit()`**, qui fait partie du cycle de vie du composant. Cette souscription permet de réagir lorsque les données sont émises par l'`Observable`.

#### Exemple de souscription à un `Observable` :
```typescript
import { Component, OnInit } from '@angular/core';
import { LivreService } from '../services/livre.service';
import { Livre } from '../modeles/livre';

@Component({
  selector: 'app-liste-livres',
  templateUrl: './liste-livres.component.html',
  styleUrls: ['./liste-livres.component.css']
})
export class ListeLivresComponent implements OnInit {
  livres: Livre[] = [];

  constructor(private livreService: LivreService) {}

  ngOnInit(): void {
    this.livreService.getLivres().subscribe(
      (data) => {
        this.livres = data;  // Réception des données de l'Observable
      },
      (error) => {
        console.error('Erreur lors de la récupération des livres', error);
      }
    );
  }
}
```

### 1.5. **Gestion des Erreurs et Désabonnement**

Lorsque tu souscris à un `Observable`, il est également important de **gérer les erreurs** et de **désabonner** l'Observable lorsque le composant est détruit pour éviter les fuites de mémoire.

#### Gestion des erreurs et désabonnement :
```typescript
import { Component, OnInit, OnDestroy } from '@angular/core';
import { LivreService } from '../services/livre.service';
import { Livre } from '../modeles/livre';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-liste-livres',
  templateUrl: './liste-livres.component.html',
  styleUrls: ['./liste-livres.component.css']
})
export class ListeLivresComponent implements OnInit, OnDestroy {
  livres: Livre[] = [];
  private subscription: Subscription;

  constructor(private livreService: LivreService) {}

  ngOnInit(): void {
    this.subscription = this.livreService.getLivres().subscribe(
      (data) => {
        this.livres = data;
      },
      (error) => {
        console.error('Erreur lors de la récupération des livres', error);
      }
    );
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();  // Désabonnement pour éviter les fuites de mémoire
    }
  }
}
```

### 1.6. **Comparaison avec les Appels Asynchrones dans Blazor**

En **Blazor**, les appels asynchrones sont généralement gérés avec les **`Task`** et les mots-clés **`async`** / **`await`**. Un appel HTTP en Blazor retourne une `Task`, et l'application attend que la tâche soit terminée avant de traiter les données.

En **Angular**, les appels asynchrones sont gérés avec **RxJS** et les **`Observable`**, qui fournissent un flux de données réactif auquel tu peux souscrire. Contrairement aux `Task` de Blazor qui retournent une seule valeur, les `Observable` peuvent émettre plusieurs valeurs au fil du temps.

---

## 2. **Les Directives en Angular : Boucles, Conditions, et Affichage de Données**

### 2.1. **`*ngFor` : Boucles dans les Templates**

La directive `*ngFor` permet de boucler sur une collection de données et de générer un élément HTML pour chaque élément de la collection.

#### Exemple :
```html
<ul>
  <li *ngFor="let livre of livres">{{ livre.titre }}</li>
</ul>
```

Dans cet exemple, chaque livre de la collection `livres` sera affiché sous forme de liste dans un élément `<li>`.

### 2.2. **`*ngIf` : Conditions dans les Templates**

La directive `*ngIf` permet d’afficher ou de masquer un élément HTML en fonction d’une condition.

#### Exemple :
```html
<p *ngIf="livres.length > 0">Il y a des livres disponibles.</p>
<p *ngIf="livres.length === 0">Aucun livre disponible.</p>
```

### 2.3. **`[ngClass]` et `[ngStyle]` : Appliquer des Classes et Styles Dynamiquement**

- **`[ngClass]`** permet d'ajouter ou de retirer des classes CSS en fonction de conditions.
- **`[ngStyle]`** permet d'appliquer des styles dynamiques à un élément.

#### Exemple :
```html
<p [ngClass]="{'en-stock': livre.enStock}">{{ livre.titre }}</p>
<p [ngStyle]="{'color': livre.enStock ? 'green' : 'red'}">{{ livre.titre }}</p>
```

---

## 3. **Attributs des Composants et Leur Affichage dans la Vue**

### 3.1. **Qu'est-ce qu'un Attribut de Composant ?**

Un **attribut de composant** en Angular correspond à une **propriété** définie dans la classe du composant et qui peut être affichée dans le template HTML via des mécanismes d'**interpolation** ou d'**Input**.

### 3.2. **Interpolation des Attributs dans la Vue**

Pour afficher une propriété du composant dans la vue, tu utilises l’**interpolation** avec la syntaxe `{{ ... }}`.

#### Exemple :
```typescript
export class AppComponent {
  titre = 'Mon Application Angular';
}
```

```html
<h1>{{ titre }}</h1>
```

### 3.3. **Passer des Données à un Composant Enfant avec `@Input`**

La **décoration `@Input`** permet de passer des données d'un composant parent à un composant enfant. Cette technique est couramment utilisée pour passer des paramètres dynamiques.

#### Exemple simple avec une chaîne de caractères :
**Composant enfant :**
```typescript
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-enfant',
  template: `<p>{{ data }}</p>`
})
export class EnfantComponent {
  @Input() data: string;  // Le parent peut passer une chaîne de caractères ici
}
```

**Composant parent :**
```html
<app-enfant [data]="parentData"></app-enfant>  <!-- Transmission de la donnée -->
```

### 3.4. **Passer des Données Complexes (ex: Liste) à un Composant Enfant**

Tu peux également passer des structures de données plus complexes comme des tableaux, des objets, ou des listes entre composants.

#### Exemple avec une liste :
**Composant enfant :**
```typescript
import { Component, Input } from '@angular/core';
import { Livre } from '../modeles/livre';



@Component({
  selector: 'app-liste-livres',
  template: `
    <ul>
      <li *ngFor="let livre of livres">{{ livre.titre }}</li>
    </ul>
  `
})
export class ListeLivresComponent {
  @Input() livres: Livre[];  // Le parent peut passer une liste de livres
}
```

**Composant parent :**
```typescript
export class ParentComponent {
  parentLivres: Livre[] = [
    { titre: 'Livre 1' },
    { titre: 'Livre 2' }
  ];
}
```

**Template du parent :**
```html
<app-liste-livres [livres]="parentLivres"></app-liste-livres>
```

### 3.5. **Gérer les Valeurs Nulles et Undefined**

En Angular, tu peux gérer les valeurs nulles ou indéfinies dans les templates avec le **`safe navigation operator`** (`?.`), ce qui est particulièrement utile lorsque tu ne sais pas si les données seront toujours présentes.

#### Exemple :
```html
<p>{{ livre?.titre }}</p>
```

Dans cet exemple, si `livre` est `null` ou `undefined`, Angular n’essaiera pas d’accéder à `livre.titre`, évitant ainsi une erreur.

### 3.6. **Comparaison avec Blazor : Types Nullables**

En **Blazor**, on utilise fréquemment les **types nullables** (ex. `string?`, `int?`) pour gérer les scénarios où une donnée peut être nulle. Angular, bien que ne disposant pas de types nullables natifs, permet une gestion similaire via le `safe navigation operator` et l'utilisation de la gestion des `null` et `undefined`.

---

## 4. **Cycle de Vie des Composants en Angular**

### 4.1. **Les Différentes Méthodes de Cycle de Vie**

Angular fournit plusieurs méthodes qui sont appelées à différents moments du cycle de vie d'un composant. Voici les principales :

1. **`ngOnInit()`** : Appelé une fois que le composant est initialisé. C’est l'endroit idéal pour démarrer les appels asynchrones ou initialiser des données.
2. **`ngOnChanges()`** : Appelé chaque fois qu'un `@Input` change de valeur.
3. **`ngDoCheck()`** : Appelé à chaque cycle de détection de changement.
4. **`ngAfterViewInit()`** : Appelé après que la vue du composant et ses enfants ont été initialisés.
5. **`ngOnDestroy()`** : Appelé juste avant que le composant soit détruit. C'est ici que tu désabonnes les `Observable` pour éviter les fuites de mémoire.

#### Exemple de cycle de vie :
```typescript
export class AppComponent implements OnInit, OnDestroy {
  ngOnInit() {
    console.log('Composant initialisé');
  }

  ngOnDestroy() {
    console.log('Composant détruit');
  }
}
```

### 4.2. **Comparaison avec Blazor**

En **Blazor**, les cycles de vie des composants sont similaires, avec des méthodes telles que :
- `OnInitializedAsync()`, qui est comparable à `ngOnInit()`.
- `OnParametersSetAsync()`, qui est similaire à `ngOnChanges()` pour gérer les changements de paramètres.
- `Dispose()`, qui correspond à `ngOnDestroy()`.

Les deux frameworks ont des méthodes de cycle de vie similaires, mais Blazor utilise des **`Task`** pour les méthodes asynchrones, tandis qu'Angular utilise les **`Observable`** pour gérer les flux de données.

---

[...retour menu sur Angular](../menu.md)