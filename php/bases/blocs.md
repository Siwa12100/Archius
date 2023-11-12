# Include & Blocs fonctionnels

[...retour au sommaire](../intro.md)

[...retour à la page précédente](./fonctions.md)

Dans cette partie, on va simplement voir l'utilisation du `require` ou `include`. En soit, c'est vraiment tout con mais ce qu'il permet est vraiment très utile et va complètement changer la façon de structurer le code, en enlevant la grande majorité de la duplication de code. 

### require et include
Ces deux fonctions sont similaires et permettent d'inclure le contenu d'un autre fichier dans le fichier courant. 

La différence entre les deux fonctions est que `require` va générer une **erreur fatale** et arrête l'exécution si la page n'est pas trouvée, alors que le `include` ne va faire qu'un avertissement, laissant **le script continuer à s'exécuter**.

**Syntaxe :**
```php
include('chemin/vers/page.php');
```

### Decoupage en blocs
Le soucis lorsque l'on n'utilise que de l'html & css, c'est que l'on doit se taper à remodifier à chaque fois par exemple le header ou le footer dans toutes les pages dès qu'il y a quelque chose à changer. 

Mais l'utilisation du `include` va permettre de mettre le header dans un fichier `header.php` par exemple et le footer dans un fichier `footer.php`, de manière à faire un include en début et fin de page seulement.

Cela diminue grandement la répétition de code et le travail de maintenance du code. 

Dans le même sens, à l'image de fichiers .h en C par exemple, on va pouvoir par exemple stocker toutes les variables du projet dans un fichier `variables.php` ou même toutes les fonctions dans un fichier `fonctions.php`. 

**Voilà ainsi à quoi pourrait ressembler une page maintenant :**
```php
<!DOCTYPE html>
<html>
    <head> 
        <meta charset="utf-8"/>
        <title> Nom du site... </title>
    </head>
    <body> 
        <?php
            include ('header.php');
        ?>

        Le reste de la page....

        <?php
            include ('footer.php');
        ?>
    </body>
</html>
```

---
[5.) Passage de paramètres via l'URL & méthode GET (suite des notes...)](./paramUrl.md)