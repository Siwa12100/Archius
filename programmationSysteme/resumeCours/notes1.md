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

En mémoire, est présente pour chaque processus ce qui s'appelle zone U (= zone utilisateur). Elle donne des informations sur le contexte d'exécution du processus, d'un point de vue de l'utilisateur et contient :

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
        On commence par cloner le processus père. A ce moment là, dans le code du père, child prendra la valeur du PID du fils. 

        Dans le code du fils, child prendra la valeur 0. 

        S'il y a un soucis, évidemment le fils ne sera pas créé et child contiendra la valeur -1 
    */
    pid_t child = fork();
    switch(child) {

        case -1 : 
            /*
                Si child vaut -1, ça veut dire que l'on est resté dans le processus père, et qu'il y a eu une erreur pendant le fork. Bah du coup on le précise, et on quitte en renvoyant le code retour de la dernière fonction système exécutée, le fork dans le cas actuel...
            */
            perror("erreur pendant le fork");
            exit(errno);
        
        case 0 :
            /*
                Si on passe ici, c'est qu'on est dans le processus fils. Du coup on fait le code qu'on veut donner au processus fils, puis on se barre avec le break...
            */

            // code que l'on veut donner au fils...
            break;
        
        default :
            /*
                Si on arrive ici, c'est que child ne vaut ni 0 ni -1, donc au final c'est qu'on est chez le père, qui a bien pris la valeur du PID du fils (qui évidemment n'est ni 0 ou -1). On fait le code du père et on se barre aussi...
            */

            //code que l'on veut donner au père...
    }
    return 0;
}
```

