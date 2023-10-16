# PHP - Les bases

### Les balises
Le code php vient s'insérer au milieu du code html. Il s'agit donc de venir placer des bouts de php dans les parties en html, quand il s'agit des parties dynamiques d'une page. 

Les balises php ont a forme suivante : `<?php ... le code ... ?>`
Il existe aussi des balises de la forme `<? ... ?>`, `<% ... %>` et `<?= ... =?>`mais il ne vaut mieux pas les utiliser dans de manière générale. 

**Exemple :**
```html
<!DOCTYPE html>
<html>
    <head>
        <title>Ceci est une page de test <?php /* Code PHP */ ?></title>
        <meta charset="utf-8" />
    </head>
```

### Afficher du texte
Pour afficher du texte en php, on utilise l'instruction `echo`. 

**Exemple :** `<?php echo "Hello World !"; ?>`

Il est possible avec `echo` d'afficher des balises HTML. 

**Exemple :** `<?php echo "Hello <strong> World ! </strong>";  ?>`

On peut utiliser un `\` pour empêcher un caractère spécial d'être interprété dans le message affiché. 

On peut ensuité évidemment utiliser cette fonctionnalité pour afficher du texte du php directement dans la page en html.

**Exemple :**
```php
<!DOCTYPE html>
<html>
    <head>
        <title>Notre première instruction : echo</title>
        <meta charset="utf-8" />
    </head>
    <body>
        <h2>Affichage de texte avec PHP</h2>
        
        <p>
            Cette ligne a été écrite entièrement en HTML.<br />
            <?php echo("Celle-ci a été écrite entièrement en PHP."); ?>
        </p>
    </body>
</html>
```

### Les erreurs
Par défaut en php, les erreurs ne s'affichent pas dans le navigateur, le but étant d'éviter de donner des informations potentiellement importantes aux utilisateurs. 

Le fichier de configuration de php se nomme php.init.
Pour le repérer,le mieux est de passer par les informations données par la commande `phpinfo()`. 

Pour cela, il suffit de créer un fichier php que l'on nomme de préférence en `info.php`. 
Dedans, on exécute simplement la fonction juste au dessus. 

**Le fichier en question :**
```php
<?php 
phpinfo();
?>
```
Dans le tableau que ce fichier génèrera, il est possible ainsi de trouver le chemin vers le php.init.

Voici ensuite les valeurs à modifier dans le `php.init` : 
* `error_reporting` --> `E_ALL`
* `display_errors` --> `ON`