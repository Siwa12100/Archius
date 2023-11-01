# Programmation système
_-> Notes prises dans le cadre de mon cours de prog. système, semestre 3 BUT Info UCA._


## Rapide introduction
Un système est un programme central qui fait l'interface entre le matériel et les applications. 

Les différentes couches sont ainsi, hiérarchiquement :
* Les utilisateurs 
* Les applications 
* Le système d'exploitation 
* Le matériel 

Une **API** (Application programming interface) spécialisée dans les appels système va nous permettre d'accéder à la couche système, depuis la couche applicative. 

Les appels système sont en fait ce qui fait le lien entre les couches basses appelées mode noyaux, et les couches hautes appelées mode utilisateur. 

**POSIX :** Portable Operating System Interface. 
C'est ce qui définit sous Unix : 
* Les commandes shells de base 
* l'api des appels systèmes 
* les extensions "temps réel"
* l'api des threads


## Les processus
Un processus est l'instance dynamique d'un programme.

Au final, un programme n'est que du code et des données, mais quand il va s'agir de le faire exécuter par le système, plein d'autres informations vont rentrer en compte, et c'est tout cet ensemble que l'on appelle un processus. 

Et c'est ainsi grâce à ces informations "supplémentaires" que le système sera en mesure de manipuler des processus, et par extension, d'en exécuter plusieurs à la fois en alternant entre eux.

Les processus ont tous une relation père fils : c'est l'arborescence de processus. 
