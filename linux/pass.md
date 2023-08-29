Pass est un logitiel de gestion des mots de passe directement intégré au terminal (et qui possède un important nombre de plugins pour l'utiliser dans diverses situations). 
Il fonctionne avec un système de cryptage par clé (gpg) et git, ce qui permet de synchroniser ses mots de passe sur différents appareils. 

Voici l'ensemble des commandes importantes pour créer puis utiliser pass. 


Création d'un espace pass : 

- gpg --full-generate-key : Pour créer une nouvelle clé crytograhique à utiliser avec pass.
Choisir RSA comme type de clé, et pour la taille de la clé, prendre 4096 (le max) dans l'idéal. 
Il demandera de rentrer un nom et un email (important pour manipuler la clé par la suite).
Il va être demandé de rentrer un mot de passe pendant la création de la clé, c'est vraiment important d'avoir un mot de passe solide et de s'en souvenir. En effet, c'est avec ce mot de passe que l'on s'authentifera régulièrement ensuite par la suite sur pass. 


- gpg --list-secret-key : Va nous permettre de récupérer l'ID de la clé que l'on souhaite utiliser. Il s'agit d'une longue liste de chiffres et lettres affichés après avoir rentré la commande. 

- pass init '... l'id de la clé récupéré au dessus...' : Va initialiser le pass. 


Commandes principales de gestion du pass : 

pass fonctionne à partir d'un système de fichiers accessible par défaut dans le ~/password-store. 
Il est ainsi possible de créer des sous dossiers ainsi que des fichiers (cryptés) dont le nom est l'endroit où est demandé le mot de passe (github, facebook,...) et le contenu le mot de passe en question. 

- pass insert Github/personnal : Va demander de rentrer le mot de passe que l'on souhaite enregistrer. S'il n'existe pas encore, un dossier Github va ainsi être créé, contenu le fichier personnal (contenant lui même le mot de passe en question de manière crypté). 

- pass generate Github/work : Va agir de la même manière, en créant un nouveau fichier work dans le dossier Github. La différence est que le mot de passe sera généré directement par pass. Il est possible de préciser une valeur à la fin de la commande (22 par exemple...) pour indiquer le nombre de caractères que l'on souhaite que le mot de passe fasse. 

- pass rm Github/work : Pour supprimer le mot de passe work pour Github. 

- pass Github/personnal : Va afficher le mot de passe de Github/personnal.

- pass -c Github/personnal : Va copier le mot de passe dans le presse papier (pendant 45 secondes).

- pass : Va afficher l'arboressence des mots de passe (dossiers, sous dossiers...).



Synchronisation avec Git (et github) : 

Pour commencer, il faut créer un repo là où l'on souhaite stocker les données. Dans mon cas, j'ai créer un repo sur github que j'ai appelé pass. 

- pass git init : permet d'initialiser en local le repo git. 

- pass git remote add origin git@github.com:Siwa12100/....... : Va permettre de faire le lien entre le dépot local (fait avec le pass git init) et le repo distant créé sur github. Le origin est dans ce cas une sorte d'alias (donc on met le nom qu'on veut) faisant référence à l'url du début distant (donné en fin de commande). 

- pass git push origin main : Pour pousser les modifications sur le dépot distant de manière classique. 


Synchronisation de pass sur une autre machine : 

Pour commencer, il faut installer pass (sudo apt install pass). Ensuite il faut faire un git clone et renommer le dossier ainsi récupéré en .password-store dans le home. 

Faire la commande "pass" à ce moment là nous permet de bien voir que pass à récupérer correctement les informations contenues dans le répo. 

Mais pour pouvoir accéder aux mots de passe, il faut donner au pass de la nouvelle machine la clé de cryptage générée précédemment. 

Sur la machine initiale, il faut donc créer un dossier, se rendre dedans et faire :
-  gpg --output public.pgp --armor --export le_mail_de_la_cle 
-  gpg --output private.pgp --armor --export le_mail_de_la_cle 

Il faut ensuite transférer ce dossier dans le home de la nouvelle machine. 
Dans la nouvelle machine,on se rend de nouveau dans le dossier, et on fait : 
- gpg import private.pgp
- pgp import public.pgp 

Normalement, pass devant dorénavant fonctionner, reconnaitre la clé et laisser l'accès aux mots de passe. 

Il semblerait qu'il soit aussi bon pour finir de faire gpg --edit-key le_mail_de_la_cle, de rentrer trust et de définir le niveau 5 (puis taper save pour quitter l'interface gpg) pour renforcer les droits donnés à cette clé. 


Ressources utiles : 
https://www.youtube.com/watch?v=FhwsfH2TpFA