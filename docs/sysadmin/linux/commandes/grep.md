# Commande - Grep
La commande **grep** permet de rechercher des motifs (patterns) dans des fichiers texte ou des données en flux continues (comme la sortie d'une autre commande).

Elle permet de filtrer les lignes selon un certain motif et d'extraire ainsi les lignes qui le possèdent. 

Syntaxe de base : `grep [options] motif [fichiers(s)]`

#### Options courantes
* `-i` : Ignore la casse lors de la recherche 
* `-v` : Inverse la recherche pour afficher les lignes qui ne correspondent pas au motif spécifié. 
* `-l` : Ne renvoie non pas les lignes qui contiennent le motif mais les fichiers qui le contiennent 
* `-r` : Si l'on passe un répertoire à la commande, cette option permet de faire en sorte de parcourir aussi les fichiers contenus dans les sous répertoires 
 * `-n` : En plus de retourner les lignes qui possèdent le motif, cette option permettra de renvoyer le numéro de la ligne dans le fichier 

* `-c` : Cette option ne renverra pas les lignes contenant le motif dans les fichiers spécifiés, mais bien le nombre de lignes contenant le motif pour chaque fichier spécifié. 
* `-E` : Pour représenter le motif sous forme de regex 