# Doc Blazor - Notes

[... retour à l'intro](./introBlazor.md)

---

## Introduction

Il existe deux versions du framework Blazor :

* **WebAssembly** : Il fonctionne de manière très similaire aux frameworks comme Angular, React, Vue (-> Application à page unique SPA). En gros, l'application s'exécute du côté du client. Ca demande donc plus de performances pour le client, c'est un peu plus lent que la version serveur, mais ça permet de limiter les instabilités liées à la connexion et rend possible le mode hors ligne.

* **Server :** Il utilise l'application ASP.NET Core qui permet d'intégrer la fonctionnalité côté serveur. En gros, les fichiers sont exécutés du côté du serveur et ne sont envoyées que les pages finales au client. La connexion entre le client et le serveur se fait par le biais de la technologie SignalR.

**Fichiers principaux de l'application :**

* `Program.cs` : Il est en charge de référencer le composant racine Blazor, appelé `App` par convention.

* `App.razor` : Il défini le composant racine ainsi que la page par défaut, souvent appelée `Index.razor` et située dans le dossier Page.

* `Index.html` : Le point d'entrée de l'application, situé dans le dossier `wwwroot`.

Il est nécessaire de séparer le code de la vue. Pour cela, à chaque fichier.razor, on associe un fichier .razor.cs du même nom, qui fait office de code behind.

Dans ce fichier, on déclare une classe qui contient le code.

**Exemple classique :**
```c#
public partial class MaClasse {
    
    private void getMessage() {
        ...;
        ...;
    }

    
    protected override void OnInitialized() {
        ...;
        ...;
    }
}
```

Le mot clé `partial` permet de dire que la déclaration de cette classe peut se faire dans plusieurs fichiers différents.

Le but de cela est par exemple d'avoir la même classe utilisée dans des fichiers de codebehind différents, lorsque l'on souhaite utiliser les mêmes méthodes dans les composants, ou des choses dans le genre...

On déclare l'URL d'une page en mettant tout en haut du fichier `@page "url/de/la/page"`. 
On peut ensuite naviguer entre les pages en utilisant un objet `NavigationManager`. 

**Exemple de navigation depuis le code :**

```c#
[Inject]
...
...
public NavigationManager manager {get; set}

private void NavigateTo() {
    manager.NavigateTo("/chemin/vers/page");
}

...
...
```

Pour créer une nouvelle page, il suffait d'ajouter un composant .razor dans le dossier des pages.

## Affichage de données

Comme en HTML, on peut utiliser la balise  `<nav> ... </nav>` pour permettre la navigation entre pages.

Pour créer un lien vers une autre page, il est possible d'utiliser le composant `navlink`. Cela marche un peu comme le `[...](.../...)` en markdown.

**Exemple :**

```html
<NavLink href="/chemin/vers/page"> Nom du lien... </NavLink>
```

Il est ainsi courant d'avoir une `nav` avec des `navlink` à l'intérieur...

### Les layouts

Le but des layouts de de factoriser le code de la vue se répétant dans plusieurs pages, pour éviter la duplication de code et faciliter la modification.

Les headers, footers et autres parties dans le genre sont ainsi souvent présents dans des layouts.

Les layouts utilisent comme les autres composants l'extension `.razor` et sont stockés dans le dossier `Shared`.

Ils sont composés de deux éléments principaux :

* `@inherits LayoutComponentBase` : Pour indiquer qu'il s'agit d'un layout, à disposer tout en haut du fichier.

* `@Body` : Qui permet d'indiquer le contenu de la page entourée par le layout.

**Syntaxe classique du Layout :**
```html
@inherits LayoutComponentBase

<header>
    ...
    ...
</header>

<nav>
    ...
    ...
</nav>

@Body

<footer>
    ...
    ...
</footer>
```

Pour appliquer un layout, il suffit d'aller en haut de la page sur laquelle on veut appliquer le layout et sous la déclaration de l'url, on rajoute : `@layout chemin.vers.le.layout`.

**Important :** En gros on remplace le `/` classique du chemin par des `.` et on enlève le `.razor` à la fin du nom du layout.

Si je veux importer un layout **sur** plusieurs pages, je peux :

* Créer un fichier `_Imports.razor` dans un dossier contenant les pages auxquels on veut appliquer le layout.

* Ajouter le `@layout ....` dans le `_Imports.razor` et du coup penser à l'enlever des pages directement.
  
Il est aussi possible de préciser un layout par défaut à l'application directement dans le code du fichier `App.razor`.

**A noter :** Les layouts peuvent faire référence à d'autres layouts de manière à les imbriquer entre eux.

### Ajouter une partie modèle

Dans le cadre de la doc, on utilise une petite classe de manière à s'entrainer à gérer des données.

On créé donc un dossier pour contenir le modèle, que l'on appelle `Models`.

On créé ensuite une classe pour mettre en forme des données.

**Exemple :**

```c#
public class Item {
    public int Id{ get; set;}
    ...
    ...
    public List<string> ListeDeNoms {get; set;}
    ...
    ...
}
```

Dans le cadre de la doc, un fichier `fake-data.json` est installé dans le dossier `wwwroot`.
De ce que j'ai cru comprendre, ce dossier est utilisé pour stocker des données statiques du projet, comme du css.

Il semblerait qu'il puisse aussi permettre de stocker des fichiers comme du json pour garder des données en dur. Je ne sais pas encore si c'est une bonne pratique, mais en tout cas c'est possible.

Il va donc être question de réussir à désérialiser les infos pour les mettre dans des instances de la classe Item.

Pour rappel, le processus de serialisation/deserialisation consiste à mettre sous la forme d'un flux d'octets (= fichiers comme json ou xml en gros...), pour les stocker, les transférer sur un réseau, puis les récupérer et les retransformer en instances de classes dans le code par exemple.

### Utiliser des données

Pour désérialiser les données, on peut utiliser un objet `HttpClient`.
Pour commencer, il faut donc activer la prise en charge du `HttpClient` dans l'application.

Pour information, le `HttpClient` est la classe qui va permettre d'envoyer des requêtes http/https, et de recevoir leurs réponses, dans le cadre du framework Blazor.

Pour cela, on rajoute la ligne : `builder.Services.AddHttpClient()` dans le fichier `Program.cs`.

Utile pour continuer : [rappel sur l'asynchronisme en c#](./asynchronieNotes.md)

Donc, une fois le client http activé, dans le codebehind de la page où on veut récupérer les données par exemple, on va appeler la méthode suivante :

```c#
// Dans la méthode OnInitializedAsync() d'un 
// composant par exemple :
...;
...;
Item[] tab;
tab = await Http.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json");
...;
...;
```

Il faut aussi penser à déclarer au sein de la classe un client http et un NavigationManager comme ceci :
```c#
[Inject]
public HttpClient Http { get; set; }

[Inject]
public NavigationManager NavigationManager { get; set; }
```

Je pense qu'on va le revoir dans la suite de la donc, mais apparemment le `[Inject] sert à faire une injection de dépendance directement gérée par Blazor.

En gros, on a pas besoin de faire un new ... pour le HttpClient et le NavigationManager, c'est automatiquement gérer et on peut donc directement les utiliser...

Pour afficher les données, on peut ensuite faire la chose suivante dans la vue associée :

```html
<h3> Liste de valeurs ... : </h3>

@if (tab != null) {
    foreach(var item in tab) {
        <div> @item.Id</div>
    }
}
```

Je ne sais pas s'il y a d'autres possibilités, mais en tout cas en s'assurant bien que `tab` dans le code au dessus soit bien un attribut de la classe du codebehind du composant razor, ça marche.

### aperçu du cyle de vie des composants razor

Il existe différentes fonctions qui sont appelées automatique à certains moments bien spécifiques de la vie des composants razor. Il est ainsi possible de surcharger ces méthodes pour ajouter des comportements/actions précis à effectuer à ces moments là.

Le cycle de vie des composants et les composants par extension on l'air d'être des sujets particulièrement complets et complexe, alors voilà simplement la liste (dans leur ordre d'appel) des [fonctions liées au cycle de vie des composants razor.](./cycleVieComposantsRazor.md)



## Ajouter un item

## DI & IOC

## Modifier un Item

## Supprimer un Item

## Globalisation et localisation

## Composant Razor

## API

## Configuration

## Utiliser les Logs

## Déployer l'application

## Autres notions

### Websocket

### Graphql

### Authentification
