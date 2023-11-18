# La création de formulaires

[...retour aux notes php sur les URL](../../php/bases/paramUrl.md)

Pour créer des formulaires, on utilise la balise `<form> ... </form>`. 
Au sein de cette balise, deux attributs sont très importants : 
* `method` : Pour indiquer le moyen par lequel les données sont envoyées. 
* `action` : Pour indiquer  l'adresse de la page (ou du programme) qui va traiter les données saisies.


## La struture générale d'un formulaire

**Syntaxe générale :**
```html
<p> Texte de la page avant le formulaire....</p>

    <form method="get" action"chemin/vers/page.php">
        <p> Contenu du formulaire....</p>
    </form>

<p> Texte après le formulaire.... </p>
```
On peut aussi ne rien mettre dans l'attribut `action` (`action=""`) pour indiquer que l'on ne redirige pas les informations ailleurs, et donc que l'on reste sur la page courante. 


### Insérer des champs de texte

De manière générale pour permettre à l'utilisateur de renseigner du contenu, on utilise la base `<input> ... </input>`. 
Mais il est essentiel de préciser le type de la balise, sinon elle n'a aucune utilité...
Pour cela, on utilise l'attribut `type` de la balise, et on lui assigne la valeur `text` comme ceci : 
```html
<input type="text" name="prenom">
```
On voit aussi qu'il faut donner un nom au champ de remplissage, dans l'exemple ci-dessus, on l'appelle prenom. 

Mais il est important de mettre une sorte de titre au champ de saisie, histoire que l'utilisateur sache ce qu'il est censé saisir...
Pour cela, on utilise la balise `<label> ... </label>`.
Il s'agit ainsi en quelque sorte du titre attribué à un input. 

Pour le mettre en place, il faut commencer par pouvoir indentifier de manière unique l'input auquel on souhaite mettre un titre. Pour cela, on rajoute à l'input un attribut `id` qui sera son identifiant unique. Cet `id` aura dans le cas présent (par convention) la même valeur que l'attribut `name`, mais on verra par la suite que ce n'est pas toujours le cas. 
Voilà donc l'input modifié : 
```html
<input type="text" id="prenom" name="prenom">
```

Ensuite, on rajoute une balise `<label>` qui possède un attribut `for` prenant la valeur de l'id de l'input auquel on souhaite mettre un titre. Et le label contient évidemment le fameux titre que l'on veut attribuer. Voilà un exemple : 
```html
<form method="get" action="....">
    <label for="prenom"> Titre du champ </label>
    <input name="prenom" id="prenom" type=text">
</form> 
```

On peut rajouter des attributs optionnels au `input` de manière à affiner le champ de saisie : 
* `size` : pour spécifier le nombre de caractères que sera en mesure d'afficher le champ à l'écran. 
* `maxlength` : pour spécifier le nombre de caractères max saisissables.
* `placeholder` : Pour afficher légèrement en gris un message dans le champ et donc donner des indications à l'utilisateur...

**Exemple :**
```html
<form method="get" action="....">
    <label for="prenom"> Titre du champ </label>
    <input name="prenom" id="prenom" type="text" size="30" maxlenght="50" placeholder="Quel est votre prénom ?">
</form> 
```

### insérer des champs de plusieurs lignes de texte

Les champs permettant de rentrer plusieurs lignes de texte sont appelés des champs multilignes. Pour les créer, on utilise la balise `<textarea> ... </textarea>`. 
Son fonctionnement est absolument le même de manière générale que l'input, c'est à dire que l'on spécifie de la même manière le name et l'id pour le lier à un label par exemple. 

Par contre, il y a une spéficité au niveau du message par défaut que l'on écrit dans le champ avant que l'utilisateur le remplisse : 
* Soit, comme dans un input, on utilise un attribut `placeholder` : dans ce cas là, le contenu du placeholder apparait dans le champ par défaut, mais si l'utilisateur ne rempli jamais ce champ et envoie le formulaire, la valeur spécifiée dans le placeholder ne sera pas transmise. 

* Soit on spécifie le texte par défaut entre la balise ouvrante et fermante du `textarea` : Dans ce cas là, à l'affichage c'est la même chose, mais si l'utilisateur envoie le formulaire sans changer la valeur du champ, la valeur par défaut sera quand même renvoyée par le formulaire. Exemple : `<textarea name="..." id="..."  > Texte par défaut </textarea>`.


### Diférentes valeurs possibles dans un type d'input

Il est possible de mettre d'autres types que `text` dans un type d'input, voilà les autres : 
* `email`
* `url`
* `tel` : pour un téléphone...
* `number` : pour un nombre entier...
* `range` : pour un curseur entre deux bornes (comme une barre son un peu...)
* `date`
* `search`
  
  Précisions pour le type range, voilà un exemple : 
  ```html
  <label......> .... </label>
  <range id="..." name="..." min="10" max="100" step="1" value="50">
  ```
  * `min` & `max` : Les bornes min et max du curseur...
  * `step` : l'incrémentation du curseur au mouvement de la souris
  * `value` : la valeur par défaut à laquelle est positionné le curseur 

### Faire choisir plusiuers options à l'utilisateur

Pour cela, on utilise le type `checkbox` de l'input. 
De cette manière, avec une liste d'inputs de ce type, l'utilisateur peut cocher plusieurs, une seule ou aucune case. 

**Exemple :**
```html
<input name="camion" id="camion" type="checkbox" checked>
<input name="voiture" id="voiture" type="checkbox">
... on rajoute aussi les labels normalement....
```
On a si on veut l'attribut `checked` qui fait en sorte que la case soit cochée par défaut. 

### Faire un choisir l'utilisateur 1 choix parmis plusieurs 

Pour cela, on utilise le type `radio` de la balise input. Sinon, c'est très similaire à une checkbox. Voilà un exemple : 
```html
<p> Etes vous content ou non ? </p>
    <input type="radio" name="reponseContent" id="ouiContent" value="oui"> <label ...> ...
    <input type="radio" name="reponseContent" id="nonContent" value="non"> <label...> ...
```
On remarque deux choses différentes du checkbox simplement : 
* Les valeurs de name sont toutes les même pour un même type de réponse. Cela permet de faire en sorte que l'utilisateur ne puisse cocher qu'un seul de ces inputs. 
* Les id sont différents pour différencier les différents inputs. 
* Un attribut `value` renseigne la valeur que renvoie l'input : ils doivent absolument tous avoir une valeur différente, sinon il y a un soucis...


### Proposer un menu déroulant

Pour cela, on utilise la balise `<select> ... </select>`. 
Entre les bornes de la balise, on utilise des balises `<option> ... </option>` pour spécifier les différents choix. 

**Exemple :**
```html

<form>
    <select name="pays" id="pays"> 
        <option value="espagne"> Espagne </option>
        <option value="france"> France </option>
    </select>
    
    <label for="pays"> Quel est votre pays ? </label>
</form>
```
Il faut bien penser à remplir l'attribut `value` dans la balise d'option...

## Peaufiner le formulaire 

Voilà quelques autres fonctionnalités pour améliorer un formulaire. 


## Faire envoyer un formulaire

### Regrouper des champs 

Il est possible de regrouper des champs d'un formulaire, c'est à dire créer une sorte d'encadré autour des champs, pour montrer qu'ils sont liés. 
Pour cela, à l'image d'un div classique, on encadre l'ensemble des champs à regrouper dans des balises `<fieldset> ... </fieldset>` . 
Il est aussi possible de donner un titre à un regroupement de champs, en mettant en première balise, au sein des balises fieldset, une balise `<legend> nom... </legend>`. 

**Exemple :**
```html
<form method="get" action="...">
    <fieldset>
        <ledgend> Titre de la catégorie </fieldset>
        <input> ...
        <input> ...
        <label> ...
    </fieldset>

    <fieldset> ..... </fieldset>
</form>
```
Il est évidemment possible ensuite d'améliorer le fieldset avec du css...

### Positionner le curseur automatiquement 

Il est possible de postionner dès le chargement de la page le curseur de l'utilisateur dans un champ bien précis. 

Pour cela, il suffit de rajouter dans le champ que l'on souhaite l'attribut `autofocus`. Evidemment, cela ne marche qu'une seule fois par page...

**Exemple :**
```html
<input type="..." name="..." id="..." autofocus>
```

### Rendre obligatoire le remplissage d'un champ 

Dans le même ordre que le autofocus, on peut rajouter un attribut `required` de la même manière dans un champ, pour le rendre obligatoire à remplir avant l'envoi du formulaire.


## Créer le bouton d'envoi du formulaire

Pour créer le bouton d'envoi, on utilise la balise `<input>`. 
Il y a plusieurs possibilités, mais de manière générale, on spécifie dans l'attribut type d'un input la valeur `submit`. 
On donne aussi le nom du bouton d'envoie, avec l'attribut `value`. 

**Exemple :**
```html
<input type="submit" value="Bouton d'envoi">
```
A ce moment là, on sera redirigé vers la valeur précisée dans l'attribut `action` de la balise `form`. 
Si rien n'a été précisé, alors on reste sur la même page...

---
[...retour aux notes php sur les URL](../../php/bases/paramUrl.md)