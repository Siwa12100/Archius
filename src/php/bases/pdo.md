# Interagir avec une BDD grâce à PDO

[...retour au sommaire](../intro.md)

[...retour à la page précédente](./sessionsCookies.md)

---

Avant de commencer, il est important de savoir que l'outil phpMyAdmin permet une administration graphique de la BDD. Il est fourni directement avec XAMPP. 
Dans ces notes, le but n'est pas de reprendre les notions de SQL comme la création des tables ou autre, mais simplement de voir comment communiquer en SQL avec la BBD, depuis le php grâce à PDO...

## PDO

PDO (PHP Data Objects) est une extension de php. c'est grâce à ses méthodes que l'on va pouvoir accéder à la bdd.

On va commencer par faire instancier PDO, pour représenter notre base de données. 
Voilà le code classique pour faire cela : 
```php

try {

    $bdd = new PDO(
        // On précise le type de bdd et l'adresse ip de la bddd
        // On donne aussi le nom de la bdd 
        'mysql:host=localhost; dbname=MaBdd; charset=utf-8', 
        // Identifiant de la personne en bdd
        root, 
        // Mot de passe lié à l'identifiant en bdd 
        root,
        // Cet argument est facultatif, mais il permet une meilleure prise en charge 
        // des erreurs sql 
        [PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION],
    )
} catch (Exception $e) {
    // prise en charge de l'exception...
}
```

Pour communiquer ensuite en sql avec la bdd, c'est aussi simple.

**Voilà le code classique pour cela :**
```php
// la requête à faire quoi, on peut mettre des inserts, des updates....
$requete = 'SELECT * FROM .... ';

// Ensuite, on a en quelque sorte ce qui va stocker la requête sous forme d'objet 
$ObjRequete = $bdd -> prepare($requete); 

// Une fois la requête préparée, on l'exécute...
$ObjRequete -> execute();

// Et on peut récupérer les résultats dans un tableau...
$resultats = $ObjRequete -> FetchAll(); 
```

---
[...retour au sommaire(fin des notes sur les bases de php)](../intro.md)