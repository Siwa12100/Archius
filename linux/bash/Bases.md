# Notions de bases du language BASH

## Les variables

### Les variables locales 
Il s'agit de variables qui ne sont valables que dans l'instance du shell où elles ont été créées. Si le shell est fermé, elles disparaissent, et elles ne peuvent pas être utilisées dans d'autres shells ou processus sur la machine.  
 
 *Exemple : * `maVariable = 3`

### Les constantes
Une constante est une variable en lecture seule en quelque sorte, qui ne peut pas être modifiée. 

*Exemple :* `declare -r maConstante = 261`

### Les variables d'environnement
Les variables d'environnement quand à elles peuvent être utilisées en dehors du shell où elles sont été créées. 
Par contre, il faut penser à les noter en quelque par comme dans le .bashrc si l'on souhaite les sauvegarder y compris après la fermeture du shell de base. 

Pour créer une variable d'environnement, il faut utiliser `export maVariable`. 

*Exemple :* `export maVariable = 10`

### Dans les scripts
* `$0` : renvoie le nom de la commande ( c'est à dire du script)
* `$1, $2, ....` : renvoie le premier argument passé en paramètre, le second...
* `$*` : Liste de tous les arguments passés au script 
* `$?` : Renvoie le code de retour de la dernière commande 


## Les quotes
* `'.....' ` : texte interprété littéralement, **aucune interprétation de contenu.**
* `"....."`  : texte interprété littéralement, mais **les ` , les $ et les " sont interprétés.**
* `\` : pour protéger un caractère. 
  
## Le Shebang
Il permet de préciser le language qui doit être interprété et est symbolisé par un `#!`. 
Dans le cadre de bash, c'est ainsi `#!/bin/bash`. 

