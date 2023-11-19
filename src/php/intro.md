# PHP - Introduction

[... retour à l'accueil](../../README.md)

## Notions de base

Un **site web statique** n'est constitué **que de html et de css**, il ne peut donc pas être mis à jour automatiquement : il faut passer par le code source. 

Un **site web dynamique** utilise donc de son côté d'autres languages en plus de css & html. 
Le contenu du site peut ainsi changer sans l'intervation d'un webmaster. 

Dans le cadre d'un site statique, le client se contente de faire une requête au serveur web, et lui renvoie simplement le code de la page demandée. 

Alors que dans le cadre d'un **site dynamique**, le client fait la requête, **la page est "générée" par le serveur** (il la prépare en gros), et puis ensuite il lui envoie la page générée. 
Ainsi à chaque nouvelle requête de client, la page est générée de nouveau. 

### Les languages

* **Html :** va être utilisé pour structurer et définir les différents éléments du site. 

* **css :** va permettre de mettre en forme le site et de lui ajouter des détails. 

* **php :** va permettre de rendre le site dynamique. C'est li qui génère la page web.

On utilisera aussi un **sgbd** pour la bdd...




## Configuration de la machine pour Dev

Pour pouvoir lire du PHP, il faut donc faire en sorte que notre ordi se comporte comme un serveur.

Pour cela, on a besoin de :

* **Apache :** C'est le serveur web. C'est lui qui va délivrer les pages web aux visiteurs. Pour autant, il ne gère que html et css et doit donc être complété par d'autres programmes pour avoir un site dynamique. 

* **PHP :** C'est un plugin pour Apache qui le rend capable de traiter des pages dynamique en php. 

* **Le SGBD :** Le logitiel de gestion de base de données permettrait d'enregistrer les données du site de manière organisée. 


### Installation de XAMPP (pour linux)
---
 Il s'agit du pack Apache + PHP qui permettra de simuler le serveur Web. XAMPP ne fonctionne que sous linux...

XAMPP contient : X, Apache, MySQL, Perl, PHP.

Voilà le lien pour le télécharger : https://www.apachefriends.org/fr/index.html

Ensuite, on a juste y mettre les bons droits à l'installeur :
`chmod 755 xampp-linux-*-installer.run`
Et on exécute : 
`./xampp-linux-*-installer.run`

On a ensuite `/opt/lampp/lampp start (ou stop...)` pour allumer et éteindre le service. 

Ensuite, on peut aller dans le dossier `/opt/lampp/htdocs` pour créer des dossiers/fichiers et aller voir le résultat sur le web en chercher `https://localhost` dans son navigateur. 


On peut aussi exécuter nos fichiers en php depuis le terminal en faisant `php -S localhost:8080` puis en allant dans le navigateur et en cherchant `http://localhost:8080/index.php (ou le nom du fichier en question...)`.  


# Sommaire
### Les bases : 
- [1.) bases du langage](./bases/bases.md)
- [2.) Syntaxe principale](./bases/syntaxe.md)
- [3.) Les fonctions en PHP](./bases/fonctions.md)
- [4.) Include et structuration en blocs](./bases/blocs.md) 
- [5.) Passage de paramètres via l'URL & méthode GET](./bases/paramUrl.md)
- [6.) Passer des paramètres via l'URL et les utiliser](./bases/paramUrl.md)
- [7.) L'envoi de fichiers via formulaires](./bases/envoiFichiers.md)



# Quelques liens utiles
- [Messages d'erreur courants en php (openclassroom)](https://openclassrooms.com/fr/courses/918836-concevez-votre-site-web-avec-php-et-mysql/4240011-au-secours-mon-script-plante)
