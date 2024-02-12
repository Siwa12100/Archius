# Lancer un conteneur & commandes de base

[...retour au sommaire](../sommaire.md)

---

## Docker Hub

Docker Hub est la registry officielle de Docker, offrant une plateforme centralisée pour le partage d'images. Dans l'écosystème Docker, Docker Hub joue un rôle essentiel en permettant :

* La distribution d'images prêtes à l'emploi, avec une gestion de versions facilitée.
* L'exécution de suites de tests pendant l'intégration continue.
* Le déploiement d'applications sur des environnements de développement et de production.

## Démarrer un conteneur

Pour lancer un conteneur, la commande de base est `docker run nomDeImage`. Docker vérifie si l'image est déjà présente sur la machine, et si ce n'est pas le cas, il la télécharge automatiquement depuis Docker Hub.

### Démarrer un serveur avec un conteneur

Utilisez la commande `docker run -d -p 8080:80 nginx` pour démarrer un conteneur avec l'image Nginx. Les options utilisées sont :

* `-d` : Détache le conteneur du processus de la console, permettant ainsi de continuer à utiliser la console.
* `-p 8080:80` : Redirige le port 8080 de la machine vers le port 80 du conteneur, facilitant l'accès au serveur Nginx.

* `--rm` : Permet la suppression automatique à l'arrêt du conteneur.

### Entrer dans le conteneur

Si le conteneur utilise un système d'exploitation Linux, vous pouvez entrer dans le conteneur avec la commande `docker exec -ti idDuConteneur bash`. À l'intérieur, vous avez la possibilité d'exécuter des commandes comme `cd` et d'interagir avec le système.

### Arrêter un conteneur

Pour arrêter un conteneur, utilisez la commande `docker stop idDuConteneur`. Ensuite, vous pouvez le supprimer avec `docker rm idConteneur` ou le redémarrer avec `docker start idConteneur`.

### Récupérer une image d'un conteneur

La commande `docker pull nomImage` permet de récupérer l'image depuis Docker Hub sans la lancer. Cela peut être utile lorsque vous souhaitez précharger une image sans démarrer un conteneur immédiatement.

### Afficher l'ensemble des conteneurs existants

La commande `docker ps` affiche tous les conteneurs en cours d'exécution. Si vous souhaitez voir tous les conteneurs, y compris ceux qui ne sont pas en cours d'exécution, utilisez `docker ps -a`.

### Afficher l'ensemble des images sur le système

Utilisez `docker images -a` pour afficher toutes les images présentes sur le système, y compris celles qui ne sont pas en cours d'utilisation.

### Nettoyer son système

La commande `docker system prune` permet de :

* Supprimer tous les **conteneurs qui ne tournent pas.**
* Supprimer tous les **réseaux non utilisés par les conteneurs.**
* Supprimer toutes les **images non utilisées.**
* Supprimer tous les **caches pour la création d'images.**

### Copier des fichiers entre l'hôte et le conteneur

Pour copier des fichiers entre l'hôte et le conteneur, utilisez la commande `docker cp`. Par exemple :

```bash
docker cp fichier.txt idDuConteneur:/chemin/dans/le/conteneur
```

### Voir les journaux d'un conteneur

Pour afficher les journaux d'un conteneur, utilisez la commande `docker logs idDuConteneur`.

### Exécuter une commande ponctuelle dans un conteneur

Utilisez `docker exec` pour exécuter une commande ponctuelle dans un conteneur. Par exemple :

```bash
docker exec idDuConteneur echo "Hello World"
```

---

[...retour au sommaire](../sommaire.md)