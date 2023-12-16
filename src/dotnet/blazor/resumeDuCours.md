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
On peut ensuite naviguer à travailler une page en utilisant un objet `NavigationManager`. 

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

Il est ainsi courant d'avoir une `nav` avec des `n avlink` à l'intérieur...

### Les layouts

Le but des layouts de de factoriser le code de la vue se répérant dans plusieurs pages, pour éviter la duplication de code et faciliter la modification.

Les headers, footers et autres parties dans le genre sont ainsi souvent présents dans des layouts.

Les layouts utilisent comme les autres composants l'extension `.razor` et sont stockés dans le dossier `Shared`.

Ils sont composés de deux éléments principaux :

* `@inherits LayoutComponentBase` : Pour indiquer qu'il s'agit d'un layout, à disposer tout en haut du fichier.

* `@Body` : Qui permet d'indiquer le contenu de la page entourée par le layout.

**Syntaxe classique du Layout :
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

Si je veux importer un layout à plusieurs pages, je peux :

* Créer un fichier `_Imports.razor` dans un dossier contenant les pages auxquels on veut appliquer le layout.

* Ajouter le `@layout ....` dans le `_Imports.razor` et du coup penser à l'enlever des pages directement.
  
Il est aussi possible de préciser un layout par défaut à l'application directement dans le code du fichier `App.razor`.

**A noter :** Les layouts peuvent faire référence à d'autres layouts de manière à les imbriquer entre eux.

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
