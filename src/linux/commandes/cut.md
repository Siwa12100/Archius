# Commande - Cut 
La commande cut est utilisée pour extraire des parties spécifiques de lignes de texte de fichiers (ou bien de flux de données), en fonction de critères tels que des délimiteurs ou des positions de caractères. 

*Syntaxe de base :* `cut [options] [fichier]`

#### Options courantes 
* `-d` '...' : Spécifie un délimiteur personnalisé pour séparer les champs. Par défaut, c'est l'espace qui est utilisé comme délimiteur. 

    *Exemple :* `cut -d ',' fichier.csv` pour mettre la , en délimiteur 

* `-f Champs` : Sélectionne les champs à extraire

    *Exemple :* `cut -f 3 fichier` : Sélectionne le 3 champs de la ligne 

    *Exemple :* `cut -f 4,8 fichier` : Sélectionne les champs 4 et 8 de la ligne 

    *Exemple :* `cut -f 3-7 fichier` : Sélectionne les champs 3 à 7 de la ligne

* `-c ....` : Sélectionne les caractères à une position définie sur une ligne 

    *Exemple :* `cut -c 5, 6, 7 fichier ` : Sélectionne les caractères 6, 5 et 7 de chaque ligne 

    *Exemple :* `cut -c 3-7 fichier` : Sélectionne les caractères de 3 à 7 dans chaque ligne
    