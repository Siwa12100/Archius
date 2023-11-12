# Programmation système

[...retour au sommaire](../intro.md)

## Rapide introduction

Un système est un programme central qui fait l'interface entre le matériel et les applications.

Les différentes couches sont ainsi, hiérarchiquement :

* Les utilisateurs
* Les applications
* Le système d'exploitation
* Le matériel

Le système permet de masquer le matériel à l'utilisateur, de gérer les processus, la mémoire, le système de fichiers et le réseau. 

Une **API** (Application programming interface) spécialisée dans les appels système va nous permettre d'accéder à la couche système, depuis la couche applicative.

Les appels système sont en fait ce qui fait le lien entre les couches basses appelées mode noyaux, et les couches hautes appelées mode utilisateur.

**POSIX :** Portable Operating System Interface.
C'est ce qui définit les standards sous Unix de:

* commandes shells de base
* l'api des appels systèmes
* les extensions "temps réel"
* l'api des threads

--> Le but de l'ensemble du cours est ainsi de se pencher sur les différents appels systèmes de l'api Système (POSIX).

## Les processus

### infos générales 
Un processus est l'instance dynamique d'un programme.

Au final, un programme n'est que du code et des données, mais quand il va s'agir de le faire exécuter par le système, plein d'autres informations vont rentrer en compte, et c'est tout cet ensemble que l'on appelle un processus.

Et c'est ainsi grâce à ces informations "supplémentaires" que le système sera en mesure de manipuler des processus, et par extension, d'en exécuter plusieurs à la fois en alternant entre eux.

En mémoire, est présente pour chaque processus ce qui s'appelle une zone U (= zone utilisateur). Elle donne des informations sur le contexte d'exécution du processus, d'un point de vue de l'utilisateur et contient :

* **Uid :** l'id de l'utilisateur qui a lancé le processus
* Le **compteur de temps** durant lequel le processus s'est exécuté, du point de vue du noyau, et de celui de l'utilisateur
* **Masque des signaux :** Les signaux ignorés ou non par le processus
* **Le repertoire courant** du processus et **la racine** du système de fichier
* Les **limites** d'un point de vue des resssources système accordées au processus
* La **table de références des descripteurs de fichiers** pour savoir quels sont les fichiers ouverts par le processus
* Le **masque de création des fichiers par défaut**, pour connaitre les permissions des fichiers créés par le processus
  
### Exécution des processus

Un processeur ne peut exécuter qu'un seul processus à la fois, donc le système bascule d'un processus à l'autre pour permettre le multi-tâches...

Une fois le procesus créé, il passe donc entre l'état **prêt à être exécuté**, **en cours d'éxécution**, **endormi**, puis une fois terminé, il passe en mode **zombie** (le temps que son père récupère son PID, on y revient après...), puis une fois cela fait, **il disparait**.

Le processus initial du système s'appelle **swapper**, et il donne naissance au processus **init**, qui lui donne ensuite naissance à tous les autres processus.
Si un père meurt avant d'avoir récupéré le PID de son fils --> c'est le processus **init** qui se charge de récupérer tous les processus orphelins.

### Gestion des erreurs systèmes

Les fonctions systèmes comme `fork()` (vu juste après...) renvoient des codes de retour en fonction de leur succès ou non.

On utilise la bibliothèque `<errno.h>` pour les analyser et les notifier :

* `errno` : C'est une variable globale qui prend la valeur du code retour de la dernière fonction système exécutée.
* `perror(const char * msg)` : on passe une chaîne de caractères à la fonction qui correspond à la description de l'erreur qui vient d'arriver. Ce message sera ainsi affiché sur la sortie d'erreur standard. On l'utilise souvent lorsque l'on constate un code retour négatif et que l'on veut expliquer pourquoi on en arrive là avant de partir avec un `exit(errno)`.

*infos utile :* la commande shell `strace` permet de tracer les appels systèmes et les signaux.

### Création de processus

Chaque processus dispose d'un PID (processus ID) et d'un PPID (Parent processus ID), il s'agit de types pid_t (= int en vrai).

La commande PS permet d'afficher les processus actifs dans le système.

Pour donner naissance à un processus, il faut que le père se clone, puis donne son nouveau code au fils et attende sa mort. Pour cela, 3 fonctions de l'API système sont utilisées :

* `fork()` : permet au processus de se cloner
* `exec()` : permet de remplacer le vieux code du père par le nouveau bon code du fils (-> s'appelle une "primitive de recouvrement").
* `wait()` : permet d'être notifié de la mort d'un fils et de récupérer des infos dessus...

**Le fork :**

Lors d'un fork, toutes les données du père (contenues dans la zone U) sont dupliquées, à l'exception évidemment du PID & du PPID qui sont adaptées en fonction...
Les descripteurs de fichiers sont aussi conservés --> donc le fils et le père ont les même fichiers d'ouverts.
Et pour finir, évidemment, les stats du fils sont remises à 0...

Voilà l'exemple classique d'un fork :

```c
#include <sys/types.h>
#include <unistd.h>

int main(void) {
    /* 
        On commence par cloner le processus père. A ce moment là, dans le code du père, 
        child prendra la valeur du PID du fils. 

        Dans le code du fils, child prendra la valeur 0. 

        S'il y a un soucis, évidemment le fils ne sera pas créé et child contiendra
         la valeur -1 
    */
    pid_t child = fork();
    switch(child) {

        case -1 : 
            /*
                Si child vaut -1, ça veut dire que l'on est resté dans le processus père, 
                et qu'il y a eu une erreur pendant le fork. Bah du coup on le précise, 
                et on quitte en renvoyant le code retour de la dernière 
                fonction système exécutée, le fork dans le cas actuel...
            */
            perror("erreur pendant le fork");
            exit(errno);
        
        case 0 :
            /*
                Si on passe ici, c'est qu'on est dans le processus fils. 
                Du coup on fait le code qu'on veut donner au processus fils,
                puis on se barre avec le break...
            */

            // code que l'on veut donner au fils...
            break;
        
        default :
            /*
                Si on arrive ici, c'est que child ne vaut ni 0 ni -1, 
                donc au final c'est qu'on est chez le père, qui a bien pris 
                la valeur du PID du fils (qui évidemment n'est ni 0 ou -1).
                On fait le code du père et on se barre aussi...
            */

            //code que l'on veut donner au père...
    }
    return 0;
}
```

**Le wait :**

La fonction `wait` ou `waitpid` permettent à un processus père de se mettrent en pause, en attendant qu'un processus fils se termine.

Voici les deux fonctions principales de `<wait.h>` :

* `pid_t wait(int * status)` : Cette fonction met en pause le processus père, puis retourne le PID du dernier fils qui vient de se terminer, et met dans l'entier status un code, qui pourra être utilisé pour avoir des informations sur la mort du fils.
* `pid_t waitpid(pid_t wpid, int * status, int options)` : Même chose que le simple wait, sauf que le père attend la mort d'un fils bien précis dont le pid est passé en paramètre. En ce qui concerne l'entier option, on peut simplement mettre 0 si on veut pas avoir d'options supplémentaires...
* `WEXISTATUTS(status)` : Une fois le wait fait, on peut utiliser cette fonction qui envoie une chaine de caractères donnant des informations détaillées en fonction du status dont la valeur a été définie lors du wait...

Ca va de soit, mais les waits sont donc utilisés dans le code du père...

Entre le moment où le fils est mort et où son père le récupère dans le wait, le fils est en mode **"zombie"**. Si le père est mort avant d'avoir récupéré son fils, **c'est le processus init qui récupèrera le fils** zombie au final...

**Le exec :**

Le exec de son côté va être utilisé du côté du fils, pour remplacer le vieux code du père par le nouveau code du fils.

La fonction de la famille des exec la plus utile à mon sens est `execl`, voilà un exemple : 
```c
pid_t child = fork(); // on fait le fork classique...
switch (child) {

    case 0 : 
        execl('./chemin/vers/le/nouveau/programme', 'nom_du_programme', 'argument1', 'argument2', (char *)NULL);

        // suite du code mais là on s'en fou...
}
```
Dans le `execl` du dessus, d'abord on spécifie le chemin vers le programme qu'on veut utiliser (comme une commande bash, un autre executable en C, ...), on met le nom du programme à côté (= souvent le nom de l'exécutable en soit, ça correspond à l'argument 0), puis on met la liste des arguments passés en paramètre du programme.
On fini par un `(char *)NULL` pour indiquer la fin de la liste d'arguments.

Juste pour information, on a aussi la fonction `execlp` qui est absolument identique à `execl`, mais si le programme à exécuter est dans le path, on a pas besoin de spécifier en premier paramètre le chemin entier, on peut mettre simplement le nom du programme (et on continue aussi de donner le nom du programme en second paramètre par contre attention). 

### Arguments dans le main 

On peut avoir ce prototype dans le main :

```c
int main(int argc, char * argv[]) { ...}
```

* `argc` : correspond au nombre de paramètres passés au programme
* `argv` : c'est le tableau qui stocke les différents arguments passés...

### Utilisation de variables d'environnement

Voilà la liste des fonctions disponibles dans `<stdlib.h>` pour manipuler les variables d'environnement en C : 
 --> [getenv et setenv notes](../fonctions/getenv_et_setenv.md)

### Les threads

Un processus peut se scinder en plusieurs fils d'exécution, c'est ce que l'on appelle des threads.
Ils s'exécutent de façon concurrente mais partagent la même mémoire...

Pour gérer les threads, on passe par des api spécifiques...
*(on a pas trop vu les threads donc pas la peine de trop approfondir encore ici...)*

