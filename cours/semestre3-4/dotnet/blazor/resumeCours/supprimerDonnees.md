# Supprimer des données

[retour au sommaire](./sommaire.md)

---

Voilà ce que fait dans les grandes lignes le cours :

* Il installe un nuget "Modal" qui permet d'afficher des fenêtres popup à l'utilisateur

* Il créé la fenêtre en question à afficher à l'utilisateur qui lui demande s'il est bien sûr de vouloir supprimer la donnée

* Il rajoute la méthode de suppression au service de gestion des données

* Il ajoute un bouton de suppression de la donnée et la fonction de suppression qui affiche le modal puis supprime potentiellement la donnée

## Notion de cascading Value

Le but de la cascading value est de faire passer des données d'un composant parent vers l'ensemble de ses composants enfants, sans avoir à le préciser explicitement.

Voilà le code qu'il faut mettre dans le code du composant parent :

```html
<!--On passe une première valeur, avec un nom pour la reconnaitre par la suite -->
<CascadingValue Value="@parentCascadeParameter1" Name="CascadeParam1">
    <!-- on passe une seconde valeur... -->
    <CascadingValue Value="@ParentCascadeParameter2" Name="CascadeParam2">
        <!-- les composants enfants qui récupèreront les valeurs sont à placer ici... -->
        ...
        ...
        ...
    </CascadingValue>
</CascadingValue>

```

Et dans le code behind du composant parent, on met : 

```c#
...
...
private CascadingType parentCascadeParameter1 { get; set; }
public CascadingType ParentCascadeParameter2 { get; set; }
...
...
```

Evidemment, on remplace le `CascadingType` par le type de la valeur qu'on veut passer, sachant que cela peut autant être des types classiques comme des int, string, ... que des objets.

Ensuite, on a juste à récupérer les valeurs dans n'importe quel composant de la hiérarchie. Etant donné qu'on a indiqué un nom pour chacun, cela aide à ne pas se mélanger pour les retrouver.

Voilà le code pour récupérer les valeurs des paramètres dans un composant enfant :

```c#
...
...
[CascadingParameter(Name = "CascadeParam1")]
protected CascadingType ChildCascadeParameter1 { get; set; }

[CascadingParameter(Name = "CascadeParam2")]
protected CascadingType ChildCascadeParameter2 { get; set; }
...
...
```

## Blazored Modal

En fait, un modal c'est une petite fenêtre "popup" qui apparait à l'écran de l'utilisateur à un moment donné.

On simplement besoin d'y passer le code de la vue du composant que l'on veut afficher.

Pour utiliser le modal, on installe le nuget `Blazored Modal`.

Ensuite, il faut juste rajouter ceci dans `_Imports.razor` :

 ```c#
@using Blazored.Modal
@using Blazored.Modal.Services
 ```

 Puis, on entoure le composant `Router` de balises qui passent en paramètre en cascade le Modal. Du coup, tous les composants enfants du routeur (c'est à dire l'ensemble de l'application en gros...) pourront accéder à l'instance du modal.

 Dans `App.razor` :

 ```html
 <CascadingBlazoredModal>
    <Router AppAssembly="typeof(Program).Assembly">
        ...
    </Router>
</CascadingBlazoredModal>
 ```

 Pour finir, on a juste à ajouter ceci dans le `Pages/_Layout.cshtml` :

 ```html
 <link href="_content/Blazored.Modal/blazored-modal.css" rel="stylesheet" />

<script src="_content/Blazored.Modal/blazored.modal.js"></script>
 ```

 Pour plus d'informations sur le blazored Modal, la doc est [ici](https://github.com/Blazored/Modal).

 Une fois le modal installé, il faut créer le composant razor qui sera affiche en popup. Voilà le code de celui du cours au niveau de la vue :

 ```html
 <!-- ComposantPourModal.razor -->
 <div class="simple-form">
    
    <p>
        Are you sure you want to delete @item.DisplayName ?
    </p>

    <button @onclick="ConfirmDelete" class="btn btn-primary">Delete</button>

    <button @onclick="Cancel" class="btn btn-secondary">Cancel</button>
</div>
 ```

 Il est très simple, c'est simplement un petit message et deux boutons, un pour accepter la suppresion, l'autre pour la refuser, et chaque bouton a une fonction associée si on clique dessus.

 Voilà des parties du codebehind du composant :

 ```c#
 // grâce aux paramètres en cascade, on récupère l'instance du modal pour l'utiliser
 [CascadingParameter]
public BlazoredModalInstance ModalInstance { get; set; }

// Si il confirme le delete, la fonction qui appelle le modal renverra une sorte de booléen à true
void ConfirmDelete()
    {
        ModalInstance.CloseAsync(ModalResult.Ok(true));
    }

    // si le mec annule la suppresion, on renvoie un "Cancel" équivalent à un booleen false en gros...
    void Cancel()
    {
        ModalInstance.CancelAsync();
    }
 ```

 Ensuite, ailleurs dans le code, voilà comment on utilise le modal :

 ```c#
// On récupère une interface permettant de manipuler le modal
[CascadingParameter] 
public IModalService Modal { get; set; }

// Si le mec sur la vue clique pour supprimer une donnée, dans la fonction de suppresion :
private async void OnDelete(int id)
{
    // On créé des paramètres
    var parameters = new ModalParameters();
    // On leur ajoute le nom de la donnée à delete
    parameters.Add(nameof(Item.Id), id);

    // On affiche le modal en utilisant le composant "DeleteConfirmation" que l'on a fait au dessus
    var modal = Modal.Show<DeleteConfirmation>("Delete Confirmation", parameters);
    // On récupère le résultat de ce qu'à choisi le mec
    var result = await modal.Result;
    // Si il veut annuler, on se tire
    if (result.Cancelled)
    {
        return;
    }
    // Sinon on supprime
    await DataService.Delete(id);

    // Et on change ou on affiche à nouveau la page...
    NavigationManager.NavigateTo("list", true);
}
 ```

---

[retour au sommaire](./sommaire.md)