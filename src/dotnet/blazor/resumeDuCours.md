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

### aperçu du cyle de vie des composants razor

Il existe différentes fonctions qui sont appelées automatique à certains moments bien spécifiques de la vie des composants razor. Il est ainsi possible de surcharger ces méthodes pour ajouter des comportements/actions précis à effectuer à ces moments là.

Le cycle de vie des composants et les composants par extension on l'air d'être des sujets particulièrement complets et complexe, alors voilà simplement la liste (dans leur ordre d'appel) des [fonctions liées au cycle de vie des composants razor.](./cycleVieComposantsRazor.md)

### Table HTML

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

### Blazorise DataGrid

Blazorise est une bibliothèque d'interface utilisateur open-source pour le framework blazor.
Elle met à disposition énormément d'outils par le biais de nugets pour faire de beaux affichages, de belles interfaces et des traitements complexes de manière efficace.

Pour utiliser les outils de Blazorise, il faut utiliser le concept de Nugets de .NET. En gros, ce sont des extensions qui permettent d'ajouter des fonctionnalités et d'utiliser du code déjà existant pour certaines fonctionnalités, plutôt que de tout refaire soit même from Scratch. 

Il est très simple d'ajouter des Nugets à son projet, on peut directement passer par le gestionnaire de nugets en version graphique intégré à Visual studio, à retouver dans l'onglet "Outils", ou bien on peut passer par le gestionnaire en ligne de commande appelé CLI.

Ici : [vidéos expliquant le fonctionnement des Nugets en .NET](https://youtu.be/WW3bO1lNDmo?si=Wc_5x-SbsEzxIbHC)

Le cours montre donc un exemple d'utilisation de la Datagrid blazorise. Toutes les infos et la doc de Blazorise de manière générale sont [ici](https://blazorise.com/).

La doc de Blazorise est très bien faite pour comprendre le Datagrid, donc je ne reviens pas plus que ça sur l'exemple du cours.
Par contre ils font référence à la notion de pagination, qui est importante à maitriser mais toujours très bien expliquée dans la doc.

## Ajouter un item

### Localstorage et bouton d'ajout

On installe un Nuget qui va servir à manipuler le LocalStorage.
Le LocalStorage est en gros un moyen de stocker des données directement du côté du navigateur web client. Cela ressemble énormément au concept des Cookies, mais à ce qu'il semblerait, le LocalStorage permet de stocker des quantités bien plus importantes de données, et il ne semble pas y avoir de notion de péremption de la donnée comme avec les cookies.

Le Nuget indiqué dans le cours et utilisé pour manipuler facilement le LocalStorage s'appelle `Blazored.LocalStorage`, `Blazored` étant une biblio de nugets créée par une personne douée en .NET...

On commence par rajouter un bouton au dessus de la Datagrid pour rajouter du contenu.
Le bouton se charge simplement de rediriger vers une autre page, spécialisée pour l'ajout.

```html
<div>
    <NavLink class="btn btn-primary" href="Add" Match="NavLinkMatch.All">
        <i class="fa fa-plus"></i> Ajouter
    </NavLink>
</div>
<datagrid....>
    ...
    ...
</datagrid>
```

Ce qui est intéressant, c'est de voir que le bouton a directement avec ce code un joli style et un icon "+" à côté. Cela vient directement du CSS et des frameworks associés... (`<i ...></i>` est utilisé en html de manière générale pour ajouter des icônes).

On va ensuite écrire un bout de code dans la fonction de cycle de vie de composant `OnAfterRenderAsync`, de manière à ce que si le LocalStorage est vide, on aille le remplir avec les données stockées dans le fichier .json créé plus haut.

Voilà le bout de code en question : 

```c#
// Dans les propriétés de la classe codebehind :
[Inject]
// Le but de cette interface est de nous permettre de réaliser les manipulations liées au LocalStorage...
public ILocalStorageService LocalStorage { get; set; }

// Dans le OnAfterRenderAsync :
// On récupère les données depuis le localStorage
var currentData = await LocalStorage.GetItemAsync<Utilisateur[]>("data");

if (currentData == null)
{
    // Si les données récupérées sont nulles, on vient choper les données du json et on les envoie dans le localStorage
	var originalData = Http.GetFromJsonAsync<Utilisateur[]>($"{NavigationManager.BaseUri}data/utilisateursFake.json").Result;
	await LocalStorage.SetItemAsync("data", originalData);
}

```

### Formulaire de création et modèle associé

En Blazor, pour créer un formulaire, il lui fait une classe qui lui serve de modèle. Cette classe contient ainsi les prioriétés qui récupèreront les valeurs insérées/modifiées par l'utilisateur.

Voilà donc un exemple type de classe modèle pour un formulaire :
 ```c#
 public class ItemModel
{
    // Pas d'étiquette car aucune règles précises à lui attribuer...
    public int Id { get; set; }
    
    // Required pour indiquer qu'il faut obligatoirement le saisir
    [Required]
    // On impose aussi une taille max 
    [StringLength(50, ErrorMessage = "The display name must not exceed 50 characters.")]
    public string DisplayName { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "The name must not exceed 50 characters.")]
    [RegularExpression(@"^[a-z''-'\s]{1,50}$", ErrorMessage = "Only lowercase characters are accepted.")]
    public string Name { get; set; }

    [Required]
    [Range(1, 125)]
    public int MaxDurability { get; set; }
    
    [Required]
    // On force pour qu'il soit forcément vrai : 
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the terms.")]
    public bool AcceptCondition { get; set; }

    // On envoie un msg si l'image n'est pas saisie
    [Required(ErrorMessage = "The image of the item is mandatory!")]
    // Le tableau de byte est ce qui permet de stocker l'image...
    public byte[] ImageContent { get; set; }
}
 ```

On voit ainsi qu'il existe une multitude de d'attributs que l'on peut mettre au dessus des priopriétés pour s'assurer qu'elles soient remplies correctement dans le formulaire.

Voilà un [resume des attributs standards les plus communs](./attributsCommuns.md)

Il est aussi possible de créer ses propres attributs de validation, voilà un exemple classique :

```c#
public class ClassicMovieAttribute : ValidationAttribute
{
    // L'attribut peut prendre des paramètres comme year ici : 
    public ClassicMovieAttribute(int year)
    {
        Year = year;
    }

    public int Year { get; }

    // On spécifie un message d'erreur 
    public string GetErrorMessage() =>
        $"Classic movies must have a release year no later than {Year}.";

    // C'est la méthode à surcharger issue de ValidationAttribute qui va permettre de savoir si la propriété (passée en paramètre depuis l'object value) est valide ou non...
    protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
    {
        var movie = (Movie)validationContext.ObjectInstance;
        var releaseYear = ((DateTime)value).Year;

        if (movie.Genre == Genre.Classic && releaseYear > Year)
        {
            return new ValidationResult(GetErrorMessage());
        }

        return ValidationResult.Success;
    }
}
```

Voilà maintenant une manière de l'utiliser dans un modèle de formulaire :

```c#
[ClassicMovie(1980)]
public DateTime ReleaseDate { get; set; }
```

Ce qui est important de noter, c'est que lorsque l'on utilise l'attribut, ce n'est pas la peine de mettre le "Attribute" pourtant présent à la fin du nom de la classe, c# fait quand même le lien car il s'agit d'une convention.

Mais de ce que j'ai cru comprendre, on aurait quand même pu laisser le "Attribute" à la fin et ça aurait marché...

Maintenant, on passe à la création du formulaire côté vue, et au final, ça ressemble pas mal à ce qui se fait en html/php de manière classique...

Voilà la syntaxe classique d'un formulaire :

```html
<!-- Définit la page associée à ce composant Blazor -->
@page "/add"

<!-- En-tête HTML de la page -->
<h3>Add</h3>

<!-- Formulaire d'édition Blazor -->
<EditForm Model="@itemModel" OnValidSubmit="@HandleValidSubmit">
    <!-- Ajout d'un composant de validation basé sur les DataAnnotations -->
    <DataAnnotationsValidator />
    <!-- Affiche un résumé des erreurs de validation -->
    <ValidationSummary />

    <!-- Champ de saisie pour le Display Name -->
    <p>
        <label for="display-name">
            Display name:
            <!-- Lien bidirectionnel avec la propriété DisplayName de l'itemModel -->
            <InputText id="display-name" @bind-Value="itemModel.DisplayName" />
        </label>
    </p>

    <!-- Champ de saisie pour le Name -->
    <p>
        <label for="name">
            Name:
            <!-- Lien bidirectionnel avec la propriété Name de l'itemModel -->
            <InputText id="name" @bind-Value="itemModel.Name" />
        </label>
    </p>

    <!-- Champ de saisie numérique pour le Stack Size -->
    <p>
        <label for="stack-size">
            Stack size:
            <!-- Lien bidirectionnel avec la propriété StackSize de l'itemModel -->
            <InputNumber id="stack-size" @bind-Value="itemModel.StackSize" />
        </label>
    </p>

    <!-- Champ de fichier pour l'image de l'item -->
    <p>
        <label>
            Item image:
            <!-- Appelle la méthode LoadImage lorsque le fichier est changé -->
            <InputFile OnChange="@LoadImage" accept=".png" />
        </label>
    </p>

    <!-- Case à cocher pour Accept Condition -->
    <p>
        <label>
            Accept Condition:
            <!-- Lien bidirectionnel avec la propriété AcceptCondition de l'itemModel -->
            <InputCheckbox @bind-Value="itemModel.AcceptCondition" />
        </label>
    </p>

    <!-- Bouton de soumission du formulaire -->
    <button type="submit">Submit</button>
</EditForm>

```

Il est important de mettre le `DataAnnotationsValidator` car c'est ce qui fait que au moment de la soumission du formulaire par le client, les attributs spécifiés pour chaque propriétés vont être vérifiés, et le formulaire ne sera envoyé côté serveur que si tout est valide.

S'il y a des erreurs de remplissage, elles apparaitront là où le ` <ValidationSummary />` a été placé.

En ce qui concerne le code behind du composant contenant le formulaire, il faut simplement redéfinir la fonction `HandleValidSubmit`, que l'on a spéficié dans le formulaire comme se déclenchant à chaque évènement OnValidSubmit (`OnValidSubmit="@HandleValidSubmit"`).

En effet, il existe 3 types d'évènements dans le genre auxquels ont peut associer des méthodes comme on vient de le faire :

* `OnSubmit` : qui s'active à chaque soumission de formulaire, qu'il soit valide ou non.

* `OnValidSubmit` & `OnInvalidSubmit` : qui s'active respectivement si un formulaire est correct ou non.

Dans la méthode `HandleValidSubmit`, on va donc faire en sorte d'utiliser les priopriétés tout juste remplies dans le modèle pour créer un nouvel élément dans le cas présent :

```c#
...
...
[Inject]
public NavigationManager NavigationManager { get; set; }
...
...

private async void HandleValidSubmit() {

    // On traite les données soumises lors du formulaire...
    maClasse a = new maClasse(FormulaireModele.Id, FormulaireModele.Nom);

    liste.add(a);
    ...
    ...
    ...

    // On fait en sorte qu'une fois le formulaire soumis et valide, on redirige vers une page :
    NavigationManager.NavigateTo("/.../.../unEndroit...");

    
}
```

A retrouver [ici](./imageFormulaire.md) le traitement d'une image soumise dans un formulaire.

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
