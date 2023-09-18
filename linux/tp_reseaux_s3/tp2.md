# tp 2 - Reseau : TCP et UPD 

### Utiliser Socat comme un intermédiaire entre Firefox & Opale
---
On commence par lancer socat, qui va écouter ce qui arrive sur le port 2000 de la machine et le renvoyer sur le port 80 du serveur opale ayant l'adresse ip opale.iut.uca.fr. 

La commande pour faire cela est `socat TCP:LISTEN:2000 TCP:opale.iut.uca.fr:80`. 

Ensuite, il nous suffit d'aller dans un navigateur web, Firefox par exemple, et d'envoyer une requête sur le port 2000 de notre machine. Il faut donc tout simplement taper dans la barre de rechercher de firefox `http://localhost:2000`.

Le navigateur va ainsi rediriger vers le serveur web d'opale, car socat va s'occuper de faire la passerelle entre le port 2000 de localhost et le port 80 de opale. 