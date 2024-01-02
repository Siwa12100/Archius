# Affichage de données

[retour au sommaire](./sommaire.md)

---

Comme en HTML, on peut utiliser la balise  `<nav> ... </nav>` pour permettre la navigation entre pages.

Pour créer un lien vers une autre page, il est possible d'utiliser le composant `navlink`. Cela marche un peu comme le `[...](.../...)` en markdown.

**Exemple :**

```html
<NavLink href="/chemin/vers/page"> Nom du lien... </NavLink>
```

Il est ainsi courant d'avoir une `nav` avec des `navlink` à l'intérieur...

## Les layouts

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

## Ajouter une partie modèle

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

## Utiliser des données

Pour désérialiser les données, on peut utiliser un objet `HttpClient`.
Pour commencer, il faut donc activer la prise en charge du `HttpClient` dans l'application.

Pour information, le `HttpClient` est la classe qui va permettre d'envoyer des requêtes http/https, et de recevoir leurs réponses, dans le cadre du framework Blazor.

Pour cela, on rajoute la ligne : `builder.Services.AddHttpClient()` dans le fichier `Program.cs`.

Utile pour continuer : [rappel sur l'asynchronisme en c#](./notesSupplementaires/asynchronieNotes.md)

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

Je pense qu'on va le revoir dans la suite de la donc, mais apparemment le `[Inject]` sert à faire une injection de dépendance directement gérée par Blazor.

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

## aperçu du cyle de vie des composants razor

Il existe différentes fonctions qui sont appelées automatique à certains moments bien spécifiques de la vie des composants razor. Il est ainsi possible de surcharger ces méthodes pour ajouter des comportements/actions précis à effectuer à ces moments là.

Le cycle de vie des composants et les composants par extension on l'air d'être des sujets particulièrement complets et complexe, alors voilà simplement la liste (dans leur ordre d'appel) des [fonctions liées au cycle de vie des composants razor.](./notesSupplementaires/cycleVieComposantsRazor.md)

## Table HTML

Il s'agit d'un moyen assez simple et comment d'afficher des données dans la vue. Les tableaux sont quelque chose de commun en HTML de manière générale et ne sont pas propres à Blazor.

**Structure d'un tableau html :**
```html
<table>
    <thead>
        Il s'agit de l'endroit où l'on défini le haut du tableau comme le nom des colonnes. 
        <tr>
            La balise tr (pour table row) permet de définir une ligne du tableau. On commence donc par mettre dans cette 
            premiere ligne les différents titres des colonnes.
            
            On utilise la balise th (pour table header) pour définir chaque titre des colonnes
            <th> Titre de la colonne 1 <th>
            <th> Titre de la colonne 2 <th>
            <th> Titre de la colonne 3 <th>
            ...
            ...
        </tr>
    </thead>
    <tbody>
        Une fois le header du tableau terminé, on passe au corps du tableau, d'où le "tbody".
        Pour chaque Item issu des données, on va utiliser une ligne du tableau pour l'afficher.

        On commence donc par faire un foreach, à l'image de ce qu'on ferait un php...
        @foreach(Var item in items) {

            <tr>
                Pour chaque item, on fait une nouvelle ligne avec "tr"
                On utilise la balise td "table data" pour spécifier le contenu de la colonne, et on affiche la donnée 
                du code behind.
                <td>@item.Id</td>
                <td>@item.Nom</td>
                <td> ... </td>
                ...
                ...
            </tr>
        }
    </tbody>
</table>
```

## Blazorise DataGrid

Blazorise est une bibliothèque d'interface utilisateur open-source pour le framework blazor.
Elle met à disposition énormément d'outils par le biais de nugets pour faire de beaux affichages, de belles interfaces et des traitements complexes de manière efficace.

Pour utiliser les outils de Blazorise, il faut utiliser le concept de Nugets de .NET. En gros, ce sont des extensions qui permettent d'ajouter des fonctionnalités et d'utiliser du code déjà existant pour certaines fonctionnalités, plutôt que de tout refaire soit même from Scratch. 

Il est très simple d'ajouter des Nugets à son projet, on peut directement passer par le gestionnaire de nugets en version graphique intégré à Visual studio, à retouver dans l'onglet "Outils", ou bien on peut passer par le gestionnaire en ligne de commande appelé CLI.

Ici : [vidéos expliquant le fonctionnement des Nugets en .NET](https://youtu.be/WW3bO1lNDmo?si=Wc_5x-SbsEzxIbHC)

Le cours montre donc un exemple d'utilisation de la Datagrid blazorise. Toutes les infos et la doc de Blazorise de manière générale sont [ici](https://blazorise.com/).

La doc de Blazorise est très bien faite pour comprendre le Datagrid, donc je ne reviens pas plus que ça sur l'exemple du cours.
Par contre ils font référence à la notion de pagination, qui est importante à maitriser mais toujours très bien expliquée dans la doc.

---

[retour au sommaire](./sommaire.md)
