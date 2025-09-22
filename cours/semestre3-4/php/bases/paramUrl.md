# Passage de paramètres via l'URL & méthode GET

[...retour au sommaire](../intro.md)

[...retour à la page précédente](./blocs.md)

Une URL (Uniform Resource Locator) représente l'adresse d'une page web aux yeux d'un navigateur, elle commence généralement par http (ou https...). 

Il est ainsi possible, par le biais d'URL, de passer des informations par le biais de paramètre à la page visée par une URL, et c'est ce qui est expliqué ici. 

### Envoyer des paramètres dans une URL

Il est possible, dans une URL, de mettre des paramètres, qui sont des informations que la page appelée dans l'URL recevra. 
Dans une URL, ces paramètres sont notés sous la forme `nom=valeur` et sont séparés par un `&` entre eux. 

**Voilà un exemple :**
```
https://www.monsite.com/page1.php?nom=dupont&prenom=Louis
```
On constate que juste avant de commencer à noter les paramètres, on met un `?`.
De cette manière, on peut écrire autant de paramètres que l'on souhaite, tant que l'on respecte la taille max d'une URL (en général 256 caractères...). 

Pour créer un lien avec des paramètres, on peut donc créer des URL en HTML qui contiennent des paramètres, par exemple : 
```html
<a href="bonjour.php?nom=dupont&amp;prenom=Jean"> Clique ici ! </a> 
```
Il est important de noter que les `&` qui servent à séparer les paramètres dans une URL doivent être dans le cas présent notés `&amp;` dans le code.
Ce code permet donc de créer un lien, qui sera affiché comme "Clique ici" pour l'utilisateur, et qui amènera donc vers la page bonjour.php, en lui passant les paramètres spécifiés. 


### Faire circuler des informations avec des formulaires et HTTP GET
Au lieu de directement inscrire les paramètres en dur dans le code, il est aussi possible de les faire générer à l'aide d'un formulaire, avec la méthode HTTP GET. 

Pour commencer, voilà un rappel du fonctionnement des formulaires en html & css (essentiel de maîtriser cela pour la suite) : [Les formulaires en html & css](../../htmlCss/fichiers/formulaires.md). 

Ainsi, si l'on fait un formulaire en html utilisant bien la méthode get, les données soumises se retrouveront bien dans l'URL menant vers la page spécifiée dans l'attribut `action` de la balise `form` du formulaire. 


### Récupérer les paramètres en PHP

Pour récupérer les données dans la page à qui elles ont été envoyées, c'est particulièrement simple.
En fait, elles sont stockées dans une variable superglobable appelée `$_GET` *(qui est un tableau)*. 
Donc on a juste à faire un `$_GET[...]` en mettant en crochet la valeur de l'attribut `name`, spécifié au moment de l'input dans le formulaire. 

**Exemple :**
```html
--> Dans le formulaire html : 
<input type="text" name="prenom" id="prenom">
```
```php
--> Dans le php : 
<h1> Prenom : <?php echo $_GET['prenom'] ?>
```
De cette manière, on affiche le prenom renseigné dans le formulaire. 

### Commencer à traiter les données reçues

N'importe qui peut bricoler une URL et passer les paramètres qu'il souhaite, sans forcément passer par le formulaire ou même en remplissant avec des valeurs qui essayeront de foutre le bordel sur le site. 
Il faut donc filtrer les données reçues. 

Il faut commencer par voir si les valeurs que l'on souhaite utiliser on bien été renseignées à la page. 

Pour vérifier si une variable existe, on utilise la fonction `isset()`. 

**Exemple :**
```php
<?php
    if (!isset($_GET['nom']) || !isset($_GET['prenom'])) {

        echo ('Les attributs ont été mal renseignés....');
        return;
    }
?>
```
Ce petit bout de code affiche un message d'erreur et arrête l'exécution du script (avec `return`) si il n'existe pas de variable nom ou bien prenom dans le `$_GET`. 

Deux autres fonctions php vont nous aider à inspecter les paramètres, il s'agit de `filter_var()` et `empty()`. 

**Voilà un exemple :**
```php
<?php
    if (!isset($_GET['email'])) {

        if (!filter_var($_GET['email'], FILTER_VALIDATE_EMAIL) || empty($_GET['email'])) {
            // gestion de l'erreur...
            return;
        }
    }
?>
```
Dans ce code, on commence par voir si la variable `email` existe bien.
Mais ensuite, on utilise : 
* `filter_var(...)` : Pour s'assurer que le contenu de la variable est bien conforme à ce qu'elle est censée contenir, c'est à dire un email. Pour cette fonction, on passe à chaque fois en premier paramètre la variable à vérifier, et la seconde valeur correspond au type auquel la variable doit correspondre, dans le cas présent un email. Mais on aura très bien pu mettre `FILTER_VALIDATE_INT` pour vérifier que ce soit un entier, ou même `FILTER_VALIDATE_IP` pour vérifier que ce soit une ip. 

* `empty()` : Bon là ça parle de soit... La variable peut exister mais être vide, et la fonction verifie donc que ce ne soit bien pas le cas. 



---
[6.) Les formulaires sécurisés (suite des notes...)](./formulairesSecurises.md)

