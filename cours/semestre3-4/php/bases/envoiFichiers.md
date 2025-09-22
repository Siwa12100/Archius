# Permettre l'envoi de fichiers

[...retour au sommaire](../intro.md)

[...retour à la page précédente](./formulairesSecurises.md)

---

## Permettre d'envoyer un fichier dans un formulaire 

Pour laisser à l'utilisateur déposer un fichier dans un formulaire, les modifications sont plutôt simples au final. Il s'agit de : 

* rajouter l'attribut `enctype="multipart/form-data"` à la balise `form` : pour informer le navigateur qu'il s'apprête à envoyer des fichiers. 

* utiliser le type `file` de la balise `input`, de manière assez classique au final...

**Exemple :**
```php
<form method="POST" action="..." enctype="multipart/form-data">
    <input type="file" name="image" id="image">
    ...
    ...
</form>
```

## La récupération du fichier

### Securisation du fichier 

Avant de pouvoir récupérer le fichier et l'utiliser sans soucis, il est question de le traiter pour s'assurer qu'il nous convient et qu'il n'est pas dangereux. 

Avant traitement, le fichier est donc par défaut stocké dans un dossier temporaire, en attente qu'on l'accepte ou non. 

Pour chaque fichier envoyé, on possède une variable superglobale `$_FILES` qui permet d'accéder à ses informations. 
Pour accéder au fichier, on spécifie quel est le nom du champ du formulaire qui a permis d'envoyer le fichier. 
Par exemple dans le code au dessus, l'input avait pour name "image", donc pour accéder au fichier envoyé, on va utiliser `$_FILES['image']`. 

Voilà comment accéder aux informations des fichiers et ce à quoi elles correspondent : 

* `$_FILES['nomDuChamp]['name']` : Pour avoir le nom du fichier envoyé 

* `$_FILES['nomDuChamp]['type']` : Pour récupérer le type du fichier 

* `$_FILES['nomDuChamp]['size']` : Pour récupérer la taille (en octets) du fichier 

* `$_FILES['nomDuChamp]['tmp_name']` : Pour récupérer l'emplacement temporaire du fichier 

* `$_FILES['nomDuChamp]['error']` : Si il y a eu une erreur, la valeur est de 0, sinon elle a une autre valeur indicative de l'erreur produite...


Voilà les différents tests que va devoir passer le fichier : 

* `isset()` : pour s'assurer qu'un fichier a bien été envoyé 

* `$_FILES['nomDuChamp]['error'] == 0` : pour s'assurer qu'il n'y a pas eu d'erreurs 

* `//['size']` <= valSouhaite : pour vérifier qu'il ne dépasse pas une certaine taille 

* verification que l'extension du fichier est bien dans la liste des extensions autorisées 


**Voilà donc le code de vérification classique expliqué en détail :**
```php
// On vérifie d'abord que la variable pour le champ souhaité existe et 
// qu'il n'y a pas d'erreurs 
if (isset($_FILES['nomDuChamp']) && $_FILES['nomDuChamp']['error'] == 0) {

    // On vérifie ensuite que la taille du fichier soit inférieur ou égale 
    // à 1 Mo environ 
    if ($_FILES['nomDuChamp']['size'] <= 1000000) {

        // On récupère un tableau contenant des informations sur le fichier 
        // grâce à la fonction pathinfo, à qui on a simplement à passer 
        // le chemin vers le fichier 
        $infosSurFichier = pathinfo($_FILES['nomDuChamp']['name']);

        // De toutes les infos récupérées, on garde simplement l'extension du fichier
        $extensionDuFichier = $infosSurFichier['extension'];
        // On défini la liste des extensions que l'on accepte 
        $extensionsAcceptees = ['jpg', 'txt', 'md', 'png',];

        // On vérifie que l'extension du fichier soit bien présente dans la 
        // liste des extensions acceptées
        if (in_array($extensionDuFichier, $extensionsAcceptees)) {

            // code d'acceptation du fichier....
        }
    }
}
```

### La validation définitive du fichier 

Une fois que l'on est arrivé au coeur des if et donc que toutes les étapes de vérification ont été passées, on va pouvoir officiellement récupérer le fichier et le stocker à son emplacement définitif.

Pour cela, on va utiliser le code suivant : 
```php
move_uploaded_file($_FILES['nomDuChamp']['tmp_name'], 'uploads/' . 
    basename($_FILES['nomDuChamp']['name']));
```

On utilise ainsi la fonction `move_uploaded_file()`, qui prend en paramètres : 

* Le nom temporaire du fichier (récupéré avec le 'tmp_name')

* Le nom définitif du fichier. Dans le cas présent, on précise logiquement le chemin au passage, en indiquant le dossier uploads et concaténant le nom du dossier avec basename(...) qui permet d'extraire le nom du fichier actuel et de le réutiliser pour le fichier final. 


---

[8.) Système de connexion (suite des notes...)](./connexion.md)