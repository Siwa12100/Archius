# Modifier des données

[retour au sommaire](./sommaire.md)

---

## Résumé de ce que fait le cours

Pour résumer, le cours sur cette partie fait les choses suivantes :

* Ajoute à l'interface puis implémente au service de gestion de données créé dans la partie précédente les méthodes nécessaires à la modification d'un item. Une des deux nouvelles méthodes permet de récupérer un Item en fonction de son id, et l'autre de modifier un item à partir d'un ItemModel (= le modèle du formulaire permettant d'interagir avec l'item).

* Le cours créé donc une nouvelle page réservée à la modification des données qui prend un id d'item en paramètre dans son url (pour plus d'infos sur le passage de paramètres par url : [ici](./notesSupplementaires/passageParamètres.md)).

* Grâce à l'id passé en paramètre et la nouvelle méthode du service permettant de retrouver un item par son id, la page affiche un formulaire similaire à celui de la création d'item, sauf que là, on affiche les données de l'item que l'on modifie (puisqu'à la différence de l'ajout d'un item, là, il existe déjà).

* Ensuite, lors de la soumission du formulaire, au lieu de rajouter simplement un item, on modifie un item déjà existant. On utilise au passage le patron factory pour cela.

Evidemment, de la même manière que l'on avait ajouté un bouton "Ajouter" pour ajouter un item, on rajoute bien un bouton pour éditer un item.

**Voilà le code dans la datagrid pour cela :**

```html
<DataGridColumn TItem="Item" Field="@nameof(Item.Id)" Caption="Action">
        <DisplayTemplate>
            <a href="Edit/@(context.Id)" class="btn btn-primary"><i class="fa fa-edit"></i> Editer</a>
        </DisplayTemplate>
    </DataGridColumn>
```

## Bouts de code pour illustrer tout cela

### Passer l'id en paramètre :

Voilà comment l'id est passé en paramètre par l'url de la page.

**Dans le code de la vue :**
```html
@page "/edit/{Id:int}"
<h3>Edit</h3>
...
...
```

**Dans le codebehind du composant :**

```c#
public partial class Edit // nom de la classe du codebehind...
{
    ...
    ...
    [Parameter]
    public int Id { get; set; }
    ...
    ...
    ...
}

```

### Modification de l'interface du service :

On ajoute une méthode de recherche par id et de modification d'un item dans l'interface du service.

```c#

public interface IMonService {

    // Autre méthodes du service
    ...
    ...
    Task<Item> getById(int id);
    Task updateItem(int idItem, ItemModel nouvellesValeurs);
}
```

Les nouvelles valeurs passées en paramètre sont donc les propriétés du modèle du formulaire correctement remplies, pour être affectées dans la fonction update aux propriétés de l'item spécifié par son id.

### Création d'une factory :

On utilise une factory pour convertir efficacement les Items en ItemModels, et inversement.

Il est intéressant de constater que l'on va utiliser une classe statique. Il s'agit d'une classe qui ne peut pas être instanciée et qui ne peut contenir que des méthodes statiques.

Voilà le code de manière générale :

```c#
public static class maClasseFactory {

    public static maClasseModel toModel(maClasse i) {

        maClasseModel m = new maClasseModel(i.id, i.name, ...);

        // En gros, on renvoie une maClasseModel (c'est 
        // à dire la version du modèle pour le formulaire) dont 
        // la valeur des propriétés sont les mêmes que celles de l'item
        // passé en paramètre...

        return m;
    }

    public static maClasse Create (maClasseModel m) {

        maClasse a = new maClasse (m.id, m.name, ...);

        // C'est littéralement l'inverse de toModel, à partir d'un modèle, 
        // on fait un item...

        return a;
    }

    public static void update(maClasse a, maClasseModel m) {

        // On donne aux valeurs de la classe celles du modèle :
        a.id = m.id;
        a.name = m.name;
        a.val = m.val;
        ...
        ...
    }
}
```

### Afficher les données de l'item au chargement du formulaire 

A la différence de la création, là, on veut que le formulaire affiche les données de l'item à modifier avant même de commencer la modification.

Pour cela, on fait en sorte, à l'aide d'une factory, de créer un ItemModel qui contienne les données de l'item à modifier.

On fait cela dans la méthode de cycle de vie de composant `OnInitializedAsync()`.

**code en question :**

```c#
protected override async Task OnInitializedAsync()
{
    ...
    ...
    // On chope l'item à modifier grâce à la nouvelle méthode de récupération 
    // par id du service que l'on a créé auparavant...
    Item item = IMonService.getById(this.id);

    //itemModel correspond à l'instance du modèle utilisée dans le formulaire :
    itemModel = ItemFactory.ToModel(item, fileContent);

    ...
    ...
}
```

### Modifier l'item à la soumission du formulaire

Avec tout le travail fait au préalable, le code de la fonction activée lorsque le formulaire est soumis (et bien valide) devient très simple :

```c#
private async void HandleValidSubmit()
    {   
        // On modifie l'item
        await DataService.updateItem(Id, itemModel);
        // On change de page 
        NavigationManager.NavigateTo("list");
    }
```

Et c'est la fonction updateItem qui utilisera la factory créée juste au dessus (et plus précisemment la fonction Update), pour mettre à jour l'item.

Voilà le code en question, qui en plus sauvegarde l'item modifié dans le localStorage :

```c#
public async Task Update(int id, ItemModel model)
{
    ...
    // On modifie l'item passé en paramètre
    ItemFactory.updateItem(item, model);

    // On a pas le contexte du début du code, mais en gros on sauvegarde les modifs...
    await _localStorage.SetItemAsync("data", currentData);
}
```
---

[retour au sommaire](./sommaire.md)