# DI & IOC

[retour au sommaire](./sommaire.md)

---

La DI est un principe de conception dans lequel les dépendances d'un composant sont injectées dans celui-ci plutôt que d'être créées à l'intérieur du composant. Une dépendance est une classe, un service ou un objet dont un autre composant a besoin pour accomplir sa tâche. Plutôt que de créer explicitement ces dépendances à l'intérieur du composant, on les fournit de l'extérieur.

Et en gros, l'IOC (inversion de contrôle) est ce qui va nous permettre de gérer l'injection de dépendance dans le cadre de Blazor.
De ce que j'ai cru comprendre, c'est le "conteneur" qui va gérer les instances des classes dont dépendent les objets dans lesquelles ont les a injectées.

Ce qui est intéressant, c'est donc que l'on va pouvoir en quelque sorte encapsuler des fonctionnalités utiles à plusieurs endroits de l'application dans des services, et les appeler à différents endroits de l'application.

Cela ressemble un peu à des managers, sauf qu'il y a une notion de gestion des instances plus propre...

Pour créer un service, il va donc pour commencer être question de créer une interface qui spécifie les différentes fonctions de ce service. 

Voilà un exemple simple :

```c#
public interface IMonService {
    
    Task add(maClasse a);
    String getMessage();
    Task<int> getNombre();
}
```

Ensuite, il nous faut évidemment au moins une classe qui implémente cette interface.
Ce qu'il est important de savoir, c'est que si cette classe dépend elle même d'autres services, il est possible de les injecter de manière assez simple, comme dans cette exemple :

```c#
public class monService : IMonService {

    // le framework va directement faire l'injection de dépendance
    // en voyant écrit HttpClient, sans que l'on ai à mettre le 
    // [Inject]
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    private int attribut;

    // On a juste à passer les dépendances en paramètre...
    public monService(int val, HttpClient h, ILocalStorageService i) {
        _http = h;
        _localStorage = i;
        attribut = val;
    }

    ...
    ...
    public async Task add(maClasse a) {
        ...
        ...
    }

    ...
    ...
}
```

Ensuite, on a juste à aller dans le `Program.cs` et ajouter le service à l'application en rajoutant : 

* en Blazor server : `services.AddSingleton<IMyService, MyService>();`

* en Blazor assembly : `builder.Services.AddSingleton<IMyService, MyService>();`

Là, on a utiliser `AddSingleton`, mais en réalité il existe 3 manières d'ajouter un service à l'application, qui sont résumés [ici](./notesSupplementaires/ajouterService.md).

Pour finir, voilà un exemple rapide d'utilisation du service dans une classe de codebehind par exemple :

```c#
...
...
[Inject]
private IMonService monService {get; set;}

...
...
// Dans une méthode :
int val = await monService.getNombre();
..
...
```

---

[retour au sommaire](./sommaire.md)