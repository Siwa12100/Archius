# Les formulaires sécurisés

[...retour au sommaire](../intro.md)

[...retour à la page précédente](./paramUrl.md)

---

### Utiliser la méthode post

Il existe deux manières d'envoyer des données issues d'un formulaire : 

* `get` : (vu au chapitre d'avant), qui permet de passer par l'URL (donc max 256 caractères en général) et de récupérer les données avec le tableau `$_GET`. 

* `post` : Les données ne transiteront plus à la vue de tout le monde dans l'URL. Par contre, il faudra toujours faire bien attention à les filtrer à leur récupération. 
  
Pour rappel, on spécifie ces méthodes dans l'attribut `method` d'une balise `form`. 
Il vaut ainsi mieux privilégier de manière générale la méthode `post`. 

la variable pour récupérer les informations sera dorénavant ainsi `$_POST`, logiquement. 

**Pratique intéressante :**
Il est possible par le biais d'un formulaire de faire passer des données en paramètre, sans que l'utilisateur ne s'en rende compte, en utilisant un input de type `hidden`. Dans ce sens, rien de s'affichera pour l'utilisateur, mais le tableau contenant les paramètres aura bien une association clé / valeur avec `hidden` dedans. 

### Prévenir les failles XSS 

Une faille XSS consiste à injecter du code HTML à l'intérieur d'un formulaire par exemple. Le soucis, c'est qu'au sein de code HTML, à l'aide de balises `<script> ... </script>`, il est possible d'injecter du javascript par exemple. 

Pour ignorer le code HTML, il faut donc utiliser la fonction `htmlspecialchars()` lorsque l'on récupère les données d'un post ou d'un get. En soit, c'est ultra simple à appliquer...

**Exemple :**
```php
<?php
    echo htmlspecialchars($_POST['name']);
?>
```

[7.) L'envoi de fichiers(suites des notes...)](./envoiFichiers.md)
---