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


# Les boucles

## La boucle for
La boucle `for` en Bash est utilisée pour exécuter un ensemble de commandes un certain nombre de fois, généralement en itérant sur une liste d'éléments. Voici tout ce que vous devez savoir sur la boucle `for` en scripts Bash :

### Syntaxe de base de la boucle `for` :

```bash
for VARIABLE in LISTE
do
    # Commandes à exécuter pour chaque élément de la liste
done
```

- `VARIABLE` : Il s'agit d'une variable qui prend la valeur de chaque élément de la `LISTE` à chaque itération.
- `LISTE` : C'est la liste d'éléments sur laquelle vous itérez. Cette liste peut être définie de différentes manières, comme une liste explicite d'éléments, une plage numérique, ou une expansion de motif.

### Exemples d'utilisation de la boucle `for` :

1. **Itération sur une liste explicite d'éléments** :
   
   ```bash
   for fruit in pomme banane orange
   do
       echo "J'aime les ${fruit}s."
   done
   ```
   
   Dans cet exemple, la boucle `for` itère sur les éléments "pomme", "banane" et "orange", et à chaque itération, la variable `fruit` prend la valeur de l'élément actuel. Les commandes à l'intérieur de la boucle affichent "J'aime les [fruit]s." pour chaque élément.

2. **Itération sur une plage numérique** :

   ```bash
   for nombre in {1..5}
   do
       echo "Numéro : $nombre"
   done
   ```
   
   Cette boucle itère sur les nombres de 1 à 5 et affiche "Numéro : [nombre]" à chaque itération.

3. **Itération sur des fichiers dans un répertoire** :

   ```bash
   for fichier in /chemin/vers/repertoire/*
   do
       echo "Fichier trouvé : $fichier"
   done
   ```
   
   Cette boucle parcourt tous les fichiers du répertoire spécifié et affiche leur nom à chaque itération.

4. **Itération sur des fichiers correspondant à un motif** :

   ```bash
   for fichier in *.txt
   do
       echo "Fichier texte trouvé : $fichier"
   done
   ```
   
   Ici, la boucle itère sur tous les fichiers ayant l'extension ".txt" dans le répertoire courant.

5. **Itération sur une liste de variables** :

   ```bash
   fruits="pomme banane orange"
   for fruit in $fruits
   do
       echo "J'aime les ${fruit}s."
   done
   ```
   
   Vous pouvez également itérer sur une liste stockée dans une variable.

6. **Utilisation de la variable `for`** :

   ```bash
   for ((i = 1; i <= 3; i++))
   do
       echo "Itération numéro $i"
   done
   ```
   
   Cette boucle `for` utilise une syntaxe spéciale pour effectuer une itération numérique de 1 à 3.

### Conseils et astuces :

- Assurez-vous que la liste d'éléments est correctement délimitée pour éviter les erreurs de syntaxe.
- Vous pouvez utiliser la variable `for` à l'intérieur de la boucle pour suivre le nombre d'itérations.
- Vous pouvez combiner des boucles `for` avec des commandes de contrôle de flux (`if`, `while`, etc.) pour créer des scripts plus complexes.
- Pour itérer sur les lignes d'un fichier, vous pouvez utiliser `for ligne in $(cat fichier.txt)` ou `while read ligne; do ... done < fichier.txt`, en fonction de vos besoins.

La boucle `for` est une structure puissante pour automatiser des tâches répétitives dans les scripts Bash. Elle peut être utilisée de nombreuses manières pour parcourir des données, des fichiers ou des séquences numériques, ce qui en fait un outil essentiel pour la programmation en shell.
