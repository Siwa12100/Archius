# Lancer un conteneur

[...retour au sommaire](../sommaire.md)

---

## Docker hub

Docker hub est la registry officielle de docker. C'est un logitiel qui permet de partager des images à d'autres personnes.

Dans l'écosystème docker, il permet :

* de distribuer aux devs des images prêts à l'emploi et de les versionner

* De jouer des suites de tests pendant l'intégration continue

* des déployer des applications sur des environnements de dvlp et de production

## Démarrer un conteneur

Pour démarrer un conteneur, il suffit de faire `docker run nomDeImage`. Docker va vérifier si l'image est déjà présente sur la machine, et si ce n'est pas le cas, il ira la télécharger depuis le docker hub automatiquement.

### Démarrer un serveur avec un conteneur

En utilisant la commande `docker run -d -p 8080:80 nginx`, on démarre un conteneur avec l'image nginx.

* `-d` permet d'indiquer que l'on détache le conteneur du processus de la console (pour pouvoir continuer à utiliser la console).

`-p 8080:80` permet d'indiquer que l'on redirige le port 8080 de notre machine vers le port 80 du conteneur. Cela permet de se connecter à notre machine depuis le port 8080 et d'accéder au port 80 du conteneur (où se situe le serveur Nginx...).

### Entrer dans le conteneur

Si notre conteneur a un linux comme os, on peut rentrer dans le conteneur en faisant `docker exec -ti idDuConteneur bash`.

Ensuite il est ainsi possible de faire des commandes comme `cd` ou autre, dans le conteneur.

### Arrêter un conteneur

Il suffit d'utiliser la commande `docker stop idDuConteneur`.Ensuite, il est possible de supprimer le conteneur à l'arrêt avec `docker rm idConteneur`, ou bien de le relancer avec `docker start idConteneur`.

### Recupérer une image d'un conteneur

`docker pull nomImage` permet de récupérer l'image depuis le docker hub, sans pour autant la lancer.

### Afficher ensemble des conteneurs existants

Pour afficher l'ensemble des conteneurs, il est possible de faire `docker ps`.

### Afficher l'ensemble des images sur le système

Pour afficher l'ensemble des images présentes sur le système, il suffit de faire `docker images -a`.

### Nettoyer son système

La commande `docker system prune` permet de :

* Supprimer l'ensemble des **conteneurs qui ne tournent pas.**
* Supprimer l'ensemble des **réseaux utilisés par aucun conteneur.**
* Supprimer l'ensemble des **images non utilisées.**
* Supprimer l'ensemble des **caches pour la création d'images.**