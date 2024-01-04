# Les composants génériques & templates

[retour aux sommaire des composants](../composant.md)

---

Il est possible de créer des composants assez généraux, qui peuvent afficher différents objets à l'intérieur de manières différentes, tout en définissant quand même certains détails visuels.

C'est à dire que l'on peut par exemple passer au même composant à différents endroits des listes d'objets différents, avec une vue différente pour les afficher, mais il sera quand même en mesure de s'y retrouver et d'afficher le tout.

Voilà un exemple classique d'un composant dans le genre. 

**tout d'abord, sa vue :**

```html
<!-- AfficheurVehicules.razor -->

<!-- On image qu'on veut un composant qui permettent d'afficher 
différents types de véhicules... -->

@typeparam Vehicules <!-- On image que Vehicules est une classe... -->

<div>

    @titreDuComposant

    @if (listeVehicules.Count != 0) {

        foreach(var v in listeVehicules) {

            @maTemplate(v);
        }
    }
</div>
```

**Maintenant dans le code du composant :**

```c#
[Parameter]
public List<Vehicules> listeVehicules {get; set;}

[Parameter]
public RenderFragment<Vehicules> maTemplate {get; set;}

[Parameter]
public RenderFragment titreDuComposant {get; set;}
```

En gros dans ce composant, on a :

* La liste de véhicules à afficher

* La template qui donne le code pour afficher chaque véhicule unique et qui prend en paramètre donc un véhicule (c'est le `RenderFragment<Vehicules>`)

* La template qui affiche le titre du composant, mais qui ne prend aucun paramètre (`RenderFragment`)
  
`RenderFragment`, qu'il prenne un paramètre ou non, est en fait un type qui prend du code html. C'est une manière de stocker en quelque sorte du code html sois forme d'objet.

Ce qui est intéressant, c'est que pour les 3 propriétés de mon composant, autant la liste de véhicules que les 2 templates, elles ne possèdent encore aucun contenu. Etant donné que ce sont des propriétés `[Parameter]`, c'est en fait au moment de leur utilisation dans la vue que les propriétés seront remplies, par le biais de paramètres.

**Justement, voilà une utilisation classique du composant :**

```html
...
...
<!-- On déclare le composant. Par le biais du passage de paramètres, 
on commence par lui donner une liste de véhicules pour sa propriété 
listeVehcules-->
<AfficheurVehicules listeVehicules="maListeDuCodeBehind">

    <!-- Dans le même sens, en utilisant le passage par paramètre, 
    on donne à la propriété titreDuComposant du contenu html, en l'occurence
    un titre h3...-->
    <titreDuComposant>
        <h3> Voilà le titre du composant ! </h3>
    </titreDuComposant>

    <!-- De la même manière, on donne du code à la propriété maTemplate-->
    <maTemplate>

        <!-- On utilise le mot "context" pour faire référence à l'objet qui 
        a été passé en paramètre du RenderFragment (un véhicule dans le cas
        présent) -->
        <h1> @context.id</h1>
        <h1> @context.nom</h1>
        <h1> @context.nom</h1>
        ...
        ...
    </maTemplate>

</AfficheurVehicules>
```

**Et pour finir, le codebehind :**

```c#
public List<Vehicules> maListeDuCodeBehind {get; set;}

protected override Task OnAfterRenderAsync(bool firstRender)
    {
        // On charge la liste de véhicules
        LoadVehicules();
        // On indique à l'application que les données ont changées
        StateHasChanged();
        // On appelle la méthode de base...
        return base.OnAfterRenderAsync(firstRender);
    }

    public void LoadVehicules()
    {
        this.maListeDuCodeBehind = new List<Vehicules>;

        this.maListeDuCodeBehind.add(new Vehicules(...,...., ....));
        this.maListeDuCodeBehind.add(new Vehicules(...,...., ....));
        this.maListeDuCodeBehind.add(new Vehicules(...,...., ....));      
    }

    ...
    ...
```

À l'intérieur de la méthode OnAfterRenderAsync on appele la méthode StageHasChanged() pour refléter les modifications de données dans le composant. Vous pouvez essayer de supprimer la méthode StateHasChanged() et vous pouvez observer une page vide sans afficher de données car nous remplissons des données avant de rendre Html.

---

[retour aux sommaire des composants](../composant.md)