# Ajouter un système de connexion 

[...retour au sommaire](../intro.md)

[...retour à la page précédente](./envoiFichiers.md)

--- 

Il va tout simplement être question d'utiliser les connaissances vues jusque ici pour créer la logique du système de connexion. 

La logique va se découper en plusieurs bouts de code. Tout d'abord, sur la page responsable de la connexion des utilisateurs, nous aurons des bouts de code pour : 

* voir si l'utilisateur a rempli le formulaire de connexion, et si oui vérifier les infos et le "connecter" au site.

* Voir si un utilisateur est bien connecté, et si ce n'est pas le cas afficher le formulaire de connexion...
  

Et il y aura aussi la page d'accueil principale, qui contiendra : 

* Les headers & footers classiques...

* l'inclusion de la page de connexion 

* l'affichage du contenu du site réservé aux utilisateurs bien connectés 


## La page de connexion 

Pour commencer, on regarde si le formulaire à déjà été rempli. 

Il sera déclaré plus tard dans la page, pour déjà pour comprendre le code qui suit, il faut simplement avoir en tête que le formulaire possède deux champs, un email et un mot de passe.

**Voilà le code expliqué en détail pour inspecter le formulaire :**

```php
    // On regarde si le formulaire a été rempli ou non en 
    // vérifiant que les variables qu'il renvoie existent bien 
    if (isset($_POST['email']) && isset($_POST['password'])) {

        // petit indice pour voir si l'utilisateur a bien été trouvé...
        $verif = 1;
        // On parcours la liste des utilisateurs connus, et on regarde à chaque fois
        // si les informations envoyées par le formulaire correspondent bien à 
        // un utilisateur connu 
        foreach ($users as user) {
            // On regarde pour chaque user connu (qui sont contenus dans le tableau déclaré
            // au préalable dans le code $users), si les infos des mails et des mdp sont 
            // les mêmes
            if ($user['email'] === $_POST[email] 
                && $user['password'] === $_POST['password']) {
                    // Si c'est bien le cas, on met dans la variable contenant l'utilisateur connecté l'utilisateur ayant rempli le formulaire 
                    $loggedUser = [
                        'email' => $user['email'],
                    ];
                    // On indique qu'un utilisateur a bien été trouvé...
                    $verif = 0;
                }
        }

        if ($verif == 1) {

            // on traite le fait si l'on souhaite qu'aucun n'utilisateur ne soit encore connecté car il n'a pas été trouvé dans la liste des utilisateurs...
        }
    }
```

Ensuite, on va voir si du coup un utilisateur a bien pu se connecter par le biais du formulaire. 
Si ce n'est pas le cas, soit au final parce qu'il n'a pas rempli le formulaire ou alors parce ce que les infos rentrées ne correspondaient à aucun utilisateur, on soumet le formulaire de connexion. 

**voilà le code pour cela :**
```php
// On regarde si la variable contenant l'utilisateur connecté existe bien 
if (!isset($loggedUser)) {
    // Si ce n'est pas le cas, on affiche (soit pour la première fois, soit à nouveau...)
    // le formulaire de connexion...
    <form... > 
        ...
        ...
        on présente le formulaire en gros quoi...
        <input type="submit" value="clique ici ptn !!">
    </form>
} else {

    // on peut afficher un message de succès de connexion par exemple si on veut 
}

```

Donc en gros, le code de la page de connexion.php en entier ressemble globalement à ceci : 

```php
<?php
    // On regarde si le formulaire a été rempli ou non en 
    // vérifiant que les variables qu'il renvoie existent bien 
    if (isset($_POST['email']) && isset($_POST['password'])) {

        // petit indice pour voir si l'utilisateur a bien été trouvé...
        $verif = 1;
        // On parcours la liste des utilisateurs connus, et on regarde à chaque fois
        // si les informations envoyées par le formulaire correspondent bien à 
        // un utilisateur connu 
        foreach ($users as user) {
            // On regarde pour chaque user connu (qui sont contenus dans le tableau déclaré
            // au préalable dans le code $users), si les infos des mails et des mdp sont 
            // les mêmes
            if ($user['email'] === $_POST[email] 
                && $user['password'] === $_POST['password']) {
                    // Si c'est bien le cas, on met dans la variable contenant l'utilisateur connecté l'utilisateur ayant rempli le formulaire 
                    $loggedUser = [
                        'email' => $user['email'],
                    ];
                    // On indique qu'un utilisateur a bien été trouvé...
                    $verif = 0;
                }
        }

        if ($verif == 1) {

            // on traite le fait si l'on souhaite qu'aucun n'utilisateur ne soit encore connecté car il n'a pas été trouvé dans la liste des utilisateurs...
        }
    }

    // ==================================================================

    // On regarde si la variable contenant l'utilisateur connecté existe bien 
    if (!isset($loggedUser)) {
        // Si ce n'est pas le cas, on affiche (soit pour la première fois, soit à nouveau...)
        // le formulaire de connexion...
        <form... > 
            ...
            ...
            on présente le formulaire en gros quoi...
            <input type="submit" value="clique ici ptn !!">
        </form>
    } else {

        // on peut afficher un message de succès de connexion par exemple si on veut 
    }
?>
```

## La page principale du site

Cette page va afficher les infos classiques de l'accueil. Ensuite elle va appeler la page de connexion, qui si l'utilisateur n'a pas rempli le formulaire (ou mal rempli), va afficher de nouveau le formulaire. 
Et pour finir, s'il existe bien un utilisateur de connecté, alors la page principale va pouvoir afficher les informations réservées aux utilisateurs connectés. 

Donc pour commencer, on a le haut de page classique, puis l'inclusion de la page de connexion. 
**Voilà le code pour cela :**
```php
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="UTF-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Page d'accueil</title>
        <link
            href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" 
            rel="stylesheet"
        >
    </head>
    <body class="d-flex flex-column min-vh-100">
        <div class="container">

        <!-- Navigation -->
        <?php include_once('header.php'); ?>

        <!-- Inclusion du formulaire de connexion -->


        <?php include_once('login.php'); ?>
```

Une fois ceci fait, c'est à dire que le haut de la page est défini et qu'on a ajouté la page de connexion, on a plus qu'à voir si au final, on a bien un utilisateur de connecté, et si oui, on lui affiche ce qu'on veut. 

**Voilà le code :**
```php
        <!-- Si l'utilisateur existe..... -->
        <?php 
            if(isset($loggedUser)) {
                On fait ce qu'on veut ici...
                On affiche ce qu'on veut à l'utilisateur connecté...
            }
         ?>
    </div>

    <?php include_once('footer.php'); ?>
</body>
</html>
```


Si on récapitule comment ça se passe lors de l'arrivée de quelqu'un sur le site au final en gros : 

* Le nouveau gars se connecte. 
  
* Il commence par voir le haut de la page d'accueil, puis la page de connexion arrive. 
  
* Vu qu'il n'a encore jamais eu accès au formulaire, le haut de la page de connexion détectera que les variables contenant les infos du formulaire sont vides.


* Aucune variable loggedUser ne sera créée, et donc la seconde partie de la page de connexion, au moment de l'isset sur loggedUser, renverra faux...


* On rentre donc dans la condition, et le formulaire est affiché, l'utilisateur va donc pouvoir le remplir...


* Vu que dans l'attribut action du formualaire on précise qu'on retourne sur la page home.php, on va être envoyé de nouveau sur la page principale, mais cette fois avec les informations du formulaire de l'utilisateur. 


* Donc quand dans la page principale on va repasser par la page de connexion, les variables existeront, et si les informations sont bonnes, alors loggedUser contiendra bien l'utilisateur. 


* Le formulaire ne s'affichera donc pas, on repartira sur la fin de la page de connexion, où l'isset détectera bien le loggedUser, et affichera donc le contenu réservé aux personnes connectées.  
  

--- 

[9.) Sessions et cookies (...suite des notes)](./sessionsCookies.md)