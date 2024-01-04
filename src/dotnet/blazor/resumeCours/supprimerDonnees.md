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
// On passe une première valeur, avec un nom pour la reconnaitre par la suite
<CascadingValue Value="@parentCascadeParameter1" Name="CascadeParam1">
    // on passe une seconde valeur...
    <CascadingValue Value="@ParentCascadeParameter2" Name="CascadeParam2">
        // les composants enfants qui récupèreront les valeurs sont à placer ici...
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

Ensuite, on a juste à récupérer dans n'importe quel composant enfin de la hiérarchie les paramètres. Etant donné qu'on a indiqué un nom pour chacun, cela aide à ne pas se mélanger. 

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

---

[retour au sommaire](./sommaire.md)