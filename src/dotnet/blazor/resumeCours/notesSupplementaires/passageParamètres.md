# Passage de paramètres par URL et entre composants


## Passage par URL

Il est possible de passer des données en paramètres à des composants par le biais des urls.

Pour commencer, il faut spécifier le fait que l'on puisse passer des paramètres par le biais de l'url, au niveau du composant. Voilà comment faire :

```html
<!-- Dans le composant en question : -->
@page "/chemin/vers/la/page/{Val:int}
@page "/chemin/vers/la/page/{Verif:bool}/{texte?}
...
...
```

Il est intéressant de noter ainsi différentes choses :

* Un composant peut avoir plusieurs url, tant qu'elles restent différentes dans les paramètres qu'elles prennent.

* En mettant un `?` à la fin, on peut dire qu'un paramètre est facultatif.

* Il n'est pas la peine de préciser le type quand c'est un String (`{text?}`).

Ensuite, dans le codebehind, il suffit de faire ceci pour récupérer le paramètre :

```c#
[Parameter]
private int id {get; set;}
```

On remarque ainsi que les paramètres par URL ne prennent pas en compte la casse, en effet on a `Id` dans l'url, `id` dans le code mais cela va quand même faire la liaison.

Si l'on veut mettre une valeur par défaut à un paramètre (si rien n'est donné dans l'url en gros...), on peut mettre par exemple le code suivant dans le OnInitialized() :

```c#
Texte = Texte ?? "Texte par défaut...";
```

## Paramètres de composants

### Définition de Paramètres
Les paramètres de composants sont des mécanismes permettant de passer des données à un composant Blazor depuis son parent. Ces paramètres sont déclarés en tant que propriétés publiques C# avec l'attribut `[Parameter]`.

### Utilisation des Paramètres
Une fois déclarés, les paramètres peuvent être utilisés dans le code C# du composant ainsi que dans le code HTML associé.

### Passage de Valeurs aux Paramètres
Lorsqu'un composant est utilisé dans une autre partie de l'application, les valeurs des paramètres peuvent être fournies pour personnaliser le comportement du composant.

### Exemple Pratique

Imaginons un composant de bouton réutilisable qui peut être configuré avec différents libellés et couleurs.

```csharp
// ButtonComponent.razor

@code {
    [Parameter]
    public string Label { get; set; }

    [Parameter]
    public string Color { get; set; } = "default";
}

<button style="background-color: @Color;">@Label</button>
```

Dans cet exemple :

- `Label` est un paramètre de type chaîne pour définir le texte du bouton.
- `Color` est un paramètre de type chaîne avec une valeur par défaut "default" pour définir la couleur de fond du bouton.

Utilisation de ce composant dans une page :

```html
<!-- MyPage.razor -->

<ButtonComponent Label="Cliquez-moi" Color="green" />
<ButtonComponent Label="Appuyez ici" />
```

Dans ce cas, nous avons utilisé le composant `ButtonComponent` deux fois, en fournissant des valeurs différentes pour les paramètres `Label` et `Color`. La première instance a une couleur verte, la seconde utilise la couleur par défaut et a un libellé différent.

### Considérations Importantes

1. **Immutable After Render (Immuable après le rendu) :**
   - Les paramètres de composants sont généralement immuables après le rendu initial. Cela signifie que si tu changes la valeur d'un paramètre dans le composant, ces changements ne seront pas reflétés dans l'interface utilisateur. Les changements de valeurs des paramètres doivent être gérés à un niveau supérieur de l'application.

2. **Héritage des Paramètres :**
   - Les composants enfants héritent des paramètres de leurs composants parents. Cela signifie que si un composant parent définit des paramètres, ces paramètres sont disponibles pour tous les composants enfants.

3. **Rafraîchissement Automatique :**
   - Les composants sont automatiquement actualisés lorsque les valeurs de leurs paramètres changent, ce qui permet de rendre dynamiquement des composants en fonction des données fournies.

En résumé, les paramètres de composants sont un mécanisme puissant pour créer des composants réutilisables et configurables dans Blazor. Ils permettent une personnalisation flexible des composants tout en favorisant la réutilisabilité du code.