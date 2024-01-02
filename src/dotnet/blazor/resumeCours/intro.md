# Introduction

[retour au sommaire](./sommaire.md)

---

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

---

[retour au sommaire](./sommaire.md)
