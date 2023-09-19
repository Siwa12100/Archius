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




## Le if
La structure `if` en Bash est utilisée pour prendre des décisions conditionnelles dans un script. Elle permet d'exécuter certaines commandes uniquement si une condition spécifiée est évaluée comme vraie (valeur de retour non nulle) ou fausse (valeur de retour nulle). Voici tout ce que vous devez savoir sur les instructions `if` en scripts Bash :

### Syntaxe de base de la structure `if` :

```bash
if CONDITION
then
    # Commandes à exécuter si la condition est vraie
else
    # Commandes à exécuter si la condition est fausse
fi
```

- `CONDITION` : C'est la condition que vous évaluez. Si cette condition est vraie (c'est-à-dire qu'elle renvoie une valeur de retour non nulle, souvent 0), alors le bloc d'instructions sous `then` est exécuté. Sinon, le bloc d'instructions sous `else` (s'il est présent) est exécuté.

- `then` : Marque le début du bloc d'instructions à exécuter si la condition est vraie.

- `else` (optionnel) : Vous pouvez inclure un bloc `else` pour spécifier des commandes à exécuter si la condition est fausse.

- `fi` : Marque la fin de la structure `if`.

### Exemples d'utilisation de la structure `if` :

1. **Exemple simple avec une condition** :

   ```bash
   age=20

   if [ "$age" -ge 18 ]
   then
       echo "Vous êtes majeur."
   else
       echo "Vous êtes mineur."
   fi
   ```

   Dans cet exemple, le script vérifie si la variable `age` est supérieure ou égale à 18. Si c'est le cas, il affiche "Vous êtes majeur." Sinon, il affiche "Vous êtes mineur."

2. **Utilisation de conditions complexes** :

   ```bash
   nom="Alice"
   age=25

   if [ "$nom" = "Alice" ] && [ "$age" -ge 18 ]
   then
       echo "Bienvenue, Alice majeure."
   else
       echo "Accès refusé."
   fi
   ```

   Cette fois, le script vérifie à la fois si le nom est "Alice" et si l'âge est supérieur ou égal à 18 pour permettre l'accès.

3. **Conditions multiples avec `elif`** :

   ```bash
   note=75

   if [ "$note" -ge 90 ]
   then
       echo "A"
   elif [ "$note" -ge 80 ]
   then
       echo "B"
   elif [ "$note" -ge 70 ]
   then
       echo "C"
   else
       echo "D"
   fi
   ```

   Vous pouvez utiliser `elif` pour évaluer plusieurs conditions en cascade. Dans cet exemple, le script attribue une lettre de grade en fonction de la note.

4. **Utilisation de la négation (`!`)** :

   ```bash
   age=15

   if ! [ "$age" -ge 18 ]
   then
       echo "Vous n'êtes pas majeur."
   else
       echo "Vous êtes majeur."
   fi
   ```

   L'opérateur `!` inverse la condition. Ici, il vérifie si l'âge n'est pas supérieur ou égal à 18.

5. **Conditions sur les fichiers** :

   ```bash
   fichier="/chemin/vers/monfichier.txt"

   if [ -e "$fichier" ]
   then
       echo "Le fichier existe."
   else
       echo "Le fichier n'existe pas."
   fi
   ```

   Vous pouvez utiliser des conditions pour vérifier l'existence de fichiers (`-e`), de répertoires (`-d`), etc.

### Conseils et astuces :

- Les opérateurs de comparaison couramment utilisés sont `-eq` (égal), `-ne` (différent), `-lt` (inférieur), `-le` (inférieur ou égal), `-gt` (supérieur) et `-ge` (supérieur ou égal).

- Vous pouvez utiliser des opérateurs logiques comme `&&` (et) et `||` (ou) pour combiner des conditions.

- Les variables dans les conditions doivent être entourées de guillemets doubles (`"$variable"`) pour gérer correctement les espaces et les caractères spéciaux.

- Vous pouvez également utiliser la commande `test` à la place de `[ ]` pour les conditions. Par exemple, `test "$age" -ge 18`.

- La structure `if` peut être imbriquée, c'est-à-dire que vous pouvez avoir une structure `if` à l'intérieur d'une autre.

- L'utilisation de la commande `case` est une alternative à la structure `if` pour gérer des conditions multiples de manière plus élégante.

La structure `if` est essentielle pour la programmation en shell, car elle permet d'ajouter des logiques conditionnelles à vos scripts pour prendre des décisions en fonction des données ou des conditions d'environnement. Elle offre une grande flexibilité pour automatiser des tâches en fonction de scénarios spécifiques.



## Les comparaisons 

Pour utiliser efficacement les comparaisons dans une instruction `if` en script Bash, vous devez comprendre les opérateurs de comparaison et les règles de syntaxe associées. Voici tout ce que vous devez savoir pour utiliser les comparaisons dans un `if` en Bash :

### Opérateurs de comparaison les plus courants :

- **`-eq`** : Égal à. Vérifie si deux valeurs sont égales.
- **`-ne`** : Différent de. Vérifie si deux valeurs sont différentes.
- **`-lt`** : Inférieur à. Vérifie si la première valeur est inférieure à la deuxième.
- **`-le`** : Inférieur ou égal à. Vérifie si la première valeur est inférieure ou égale à la deuxième.
- **`-gt`** : Supérieur à. Vérifie si la première valeur est supérieure à la deuxième.
- **`-ge`** : Supérieur ou égal à. Vérifie si la première valeur est supérieure ou égale à la deuxième.

### Utilisation dans une instruction `if` :

```bash
if [ condition ]
then
    # Commandes à exécuter si la condition est vraie
else
    # Commandes à exécuter si la condition est fausse
fi
```

- `condition` : C'est l'expression de comparaison que vous évaluez. Vous pouvez utiliser les opérateurs de comparaison pour créer cette condition. La condition doit être entourée de `[` et `]` (crochets) pour indiquer à Bash qu'il s'agit d'une condition.

### Exemples d'utilisation d'opérateurs de comparaison dans un `if` :

1. **Égalité (`-eq` et `-ne`)** :

   ```bash
   age=20

   if [ "$age" -eq 18 ]
   then
       echo "Vous avez 18 ans."
   else
       echo "Vous n'avez pas 18 ans."
   fi
   ```

2. **Infériorité (`-lt` et `-le`)** :

   ```bash
   nombre=5

   if [ "$nombre" -lt 10 ]
   then
       echo "Le nombre est inférieur à 10."
   else
       echo "Le nombre est supérieur ou égal à 10."
   fi
   ```

3. **Supériorité (`-gt` et `-ge`)** :

   ```bash
   score=85

   if [ "$score" -ge 80 ]
   then
       echo "Bravo, vous avez obtenu un score élevé."
   else
       echo "Votre score n'est pas élevé."
   fi
   ```

### Combinaison d'opérateurs de comparaison :

Vous pouvez combiner plusieurs expressions de comparaison dans une seule condition en utilisant les opérateurs logiques `&&` (et) et `||` (ou). Par exemple :

```bash
age=22

if [ "$age" -ge 18 ] && [ "$age" -le 30 ]
then
    echo "Vous êtes un jeune adulte."
else
    echo "Vous n'êtes pas un jeune adulte."
fi
```

Dans cet exemple, la condition vérifie si l'âge est à la fois supérieur ou égal à 18 et inférieur ou égal à 30 pour déterminer si quelqu'un est un "jeune adulte".

### Considérations importantes :

- Assurez-vous de mettre des espaces autour des crochets `[` et `]` pour éviter des erreurs de syntaxe.

- Lorsque vous comparez des chaînes de caractères, utilisez `=` pour l'égalité et `!=` pour la différence. Par exemple, `if [ "$nom" = "Alice" ]`.

- Vous pouvez également utiliser la commande `[[ ... ]]` (double crochets) qui offre des fonctionnalités de comparaison plus avancées et est généralement préférée pour les scripts Bash modernes.

- Pour les fichiers et les répertoires, vous pouvez utiliser des opérateurs tels que `-f` (fichier existe), `-d` (répertoire existe), `-r` (lecture autorisée), `-w` (écriture autorisée), etc., dans vos conditions.

- Les opérateurs de comparaison peuvent également être utilisés dans d'autres contextes que les instructions `if`, comme dans les boucles `for`, `while` et les tests conditionnels.

En maîtrisant les opérateurs de comparaison et les règles de syntaxe associées, vous serez en mesure de créer des instructions `if` puissantes pour prendre des décisions conditionnelles dans vos scripts Bash.



## Les séquences 
La génération de séquences en script Bash peut être utile pour effectuer des itérations ou créer des listes de nombres, de lettres ou d'autres éléments. Voici ce que vous devez savoir pour générer des séquences en Bash :

### Génération de séquences numériques :

1. **Utilisation de la boucle `for`** :

   Vous pouvez utiliser une boucle `for` pour générer une séquence numérique en spécifiant une plage de nombres. Par exemple, pour générer une séquence de 1 à 10 :

   ```bash
   for nombre in {1..10}
   do
       echo $nombre
   done
   ```

   Cela affichera les nombres de 1 à 10.

2. **Utilisation de `seq`** :

   La commande `seq` est conçue pour générer des séquences numériques. Vous pouvez spécifier la valeur de départ, la valeur de fin et l'incrément. Par exemple :

   ```bash
   for nombre in $(seq 1 2 10)
   do
       echo $nombre
   done
   ```

   Cela génère la séquence 1, 3, 5, 7, 9.

### Génération de séquences de lettres :

1. **Utilisation de `printf`** :

   Vous pouvez utiliser `printf` pour générer une séquence de lettres en utilisant les codes ASCII. Par exemple, pour générer une séquence de lettres de A à Z :

   ```bash
   for lettre in {A..Z}
   do
       echo $lettre
   done
   ```

2. **Utilisation de `seq` avec des lettres** :

   Vous pouvez également utiliser `seq` avec des codes ASCII pour générer des séquences de lettres. Par exemple :

   ```bash
   for lettre in $(seq -f "$(printf '%b' '\\%o')" 65 90)
   do
       echo $lettre
   done
   ```

   Cela génère la séquence de A à Z.

### Génération de séquences personnalisées :

Vous pouvez également générer des séquences personnalisées en utilisant des boucles et des conditions. Par exemple, pour générer une séquence de nombres pairs de 2 à 10 :

```bash
for nombre in {2..10}
do
    if [ $((nombre % 2)) -eq 0 ]
    then
        echo $nombre
    fi
done
```

Cela affichera les nombres pairs de 2 à 10.

### Utilisation de variables pour la génération de séquences :

Vous pouvez également utiliser des variables pour définir la plage de valeurs dans une séquence. Par exemple, pour générer une séquence numérique à partir de variables :

```bash
debut=5
fin=15

for nombre in $(seq $debut $fin)
do
    echo $nombre
done
```

Cela générera une séquence de 5 à 15 en utilisant les valeurs des variables `debut` et `fin`.

En comprenant ces techniques, vous pouvez générer des séquences numériques, de lettres ou même des séquences personnalisées pour automatiser des tâches spécifiques dans vos scripts Bash.


## Le while
Les boucles `while` en Bash permettent d'exécuter un ensemble de commandes tant qu'une condition spécifiée est évaluée comme vraie. Voici tout ce que vous devez savoir sur les boucles `while` en scripts Bash :

### Syntaxe de base de la boucle `while` :

```bash
while CONDITION
do
    # Commandes à exécuter tant que la condition est vraie
done
```

- `CONDITION` : C'est la condition que vous évaluez à chaque itération de la boucle. Tant que cette condition est vraie (c'est-à-dire qu'elle renvoie une valeur de retour non nulle, souvent 0), le bloc d'instructions sous `do` est exécuté. La boucle continue d'itérer jusqu'à ce que la condition devienne fausse.

- `do` : Marque le début du bloc d'instructions à exécuter tant que la condition est vraie.

- `done` : Marque la fin de la boucle `while`.

### Exemples d'utilisation de la boucle `while` :

1. **Utilisation avec une variable de comptage** :

   ```bash
   compteur=1

   while [ $compteur -le 5 ]
   do
       echo "Itération $compteur"
       compteur=$((compteur + 1))
   done
   ```

   Dans cet exemple, la boucle `while` exécute les commandes tant que le compteur est inférieur ou égal à 5. À chaque itération, le compteur est incrémenté de 1.

2. **Lecture de lignes d'un fichier** :

   ```bash
   while read ligne
   do
       echo "Ligne lue : $ligne"
   done < fichier.txt
   ```

   Cette boucle lit chaque ligne du fichier `fichier.txt` et l'affiche à l'écran.

3. **Utilisation avec une condition complexe** :

   ```bash
   nombre=1

   while [ $nombre -le 10 ] && [ $nombre -ne 5 ]
   do
       echo "Nombre : $nombre"
       nombre=$((nombre + 1))
   done
   ```

   La boucle `while` exécute les commandes tant que le nombre est inférieur ou égal à 10 et n'est pas égal à 5.

4. **Utilisation de la commande `true`** :

   ```bash
   while true
   do
       echo "Cette boucle ne s'arrête jamais."
   done
   ```

   Cette boucle s'exécute indéfiniment car la condition est toujours vraie (la commande `true` renvoie toujours 0).

5. **Utilisation de la commande `false`** :

   ```bash
   while false
   do
       echo "Cette boucle ne s'exécute jamais."
   done
   ```

   À l'inverse, cette boucle ne s'exécute jamais car la condition est toujours fausse (la commande `false` renvoie toujours 1).

### Conseils et astuces :

- Assurez-vous que la condition finira par devenir fausse à un moment donné pour éviter des boucles infinies.

- Vous pouvez utiliser des opérateurs logiques (`&&`, `||`) pour combiner des conditions dans la clause `while`.

- Utilisez des commandes pour modifier les variables de contrôle à l'intérieur de la boucle, sinon vous risquez de créer des boucles infinies.

- N'oubliez pas d'utiliser `[ ]` pour encadrer les conditions et de mettre des espaces autour des crochets pour éviter des erreurs de syntaxe.

- Vous pouvez également utiliser la commande `break` pour sortir prématurément d'une boucle `while` si une condition spécifique est remplie.

Les boucles `while` sont utiles pour exécuter des commandes de manière répétitive tant qu'une condition est vraie. Elles sont particulièrement adaptées lorsque vous ne connaissez pas à l'avance le nombre d'itérations nécessaires. Cependant, assurez-vous de gérer correctement les conditions pour éviter les boucles infinies.