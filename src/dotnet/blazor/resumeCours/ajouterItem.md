# Ajouter un item

[retour au sommaire](./sommaire.md)

---

## Localstorage et bouton d'ajout

On installe un Nuget qui va servir à manipuler le LocalStorage.
Le LocalStorage est en gros un moyen de stocker des données directement du côté du navigateur web client. Cela ressemble énormément au concept des Cookies, mais à ce qu'il semblerait, le LocalStorage permet de stocker des quantités bien plus importantes de données, et il ne semble pas y avoir de notion de péremption de la donnée comme avec les cookies.

Le Nuget indiqué dans le cours et utilisé pour manipuler facilement le LocalStorage s'appelle `Blazored.LocalStorage`, `Blazored` étant une biblio de nugets créée par une personne douée en .NET...

Attention, bien penser à rajouter `builder.Services.AddBlazoredLocalStorage();` dans le `Program.cs`.

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

## Formulaire de création et modèle associé

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

Voilà un [resume des attributs standards les plus communs](./notesSupplementaires/attributsCommuns.md)

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

A retrouver [ici](./notesSupplementaires/imageFormulaire.md) le traitement d'une image soumise dans un formulaire.

Ensuite, voilà comment faire pour afficher proprement l'image rentrée dans le formulaire au sein d'une datagrid.

Pour commencer, on doit injecter ceci dans le codebehind :

```c#
[Inject]
public IWebHostEnvironment WebHostEnvironment { get; set; }
```

Ensuite, dans la datagrid, on affiche l'image, et si on ne la trouve pas, on affiche une image par défaut : 

```html
<!-- Définition d'une colonne de données pour un composant Telerik Grid -->
<DataGridColumn TItem="Item" Field="@nameof(Item.Id)" Caption="Image">

    <!-- Définition d'un modèle d'affichage personnalisé pour cette colonne -->
    <DisplayTemplate>

        <!-- Vérification de l'existence d'un fichier d'image spécifique dans le répertoire "wwwroot/images/" -->
        @if (File.Exists($"{WebHostEnvironment.WebRootPath}/images/{context.Name}.png"))
        {
            <!-- Si l'image spécifique existe, affiche cette image -->
            <img src="images/@(context.Name).png" class="img-thumbnail" title="@context.DisplayName" alt="@context.DisplayName" style="max-width: 150px"/>
        }
        else
        {
            <!-- Si l'image spécifique n'existe pas, affiche une image par défaut -->
            <img src="images/default.png" class="img-thumbnail" title="@context.DisplayName" alt="@context.DisplayName" style="max-width: 150px"/>
        }

    </DisplayTemplate>
</DataGridColumn>
```

---

[retour au sommaire](./sommaire.md)
