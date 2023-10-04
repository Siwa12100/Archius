# PHP - Bases
---

## Notions de base
---
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