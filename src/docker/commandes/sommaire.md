# Résumé bases de docker

[...retour au sommaire](../sommaire.md)

---

## Commande `docker run`

- **Syntaxe de base** : `docker run [OPTIONS] IMAGE [COMMAND] [ARG...]`
- **Options principales** :
  - `-d`, `--detach` : Exécute le conteneur en arrière-plan.
  - `-p`, `--publish` : Publie les ports du conteneur vers l'hôte.
  - `--name` : Nomme le conteneur.
  - `-v`, `--volume` : Monte un volume.
  - `--rm` : Supprime automatiquement le conteneur lorsque celui-ci s'arrête.
  - `-e`, `--env` : Définit une variable d'environnement.
- **Exemple** : `docker run -d --name mon-nginx -p 8080:80 nginx`

## Commandes Liées aux Images

- **Lister les images** : `docker images`
- **Construire une image** : `docker build -t monimage:tag .`
- **Supprimer une image** : `docker rmi monimage:tag`
- **Tirer une image** : `docker pull monimage:tag`
- **Pousser une image** : `docker push monimage:tag`

## Commandes Liées aux Réseaux

- **Lister les réseaux** : `docker network ls`
- **Créer un réseau** : `docker network create mon_reseau`
- **Inspecter un réseau** : `docker network inspect mon_reseau`
- **Supprimer un réseau** : `docker network rm mon_reseau`
- **Connecter un conteneur à un réseau** : `docker network connect mon_reseau mon_conteneur`
- **Déconnecter un conteneur d'un réseau** : `docker network disconnect mon_reseau mon_conteneur`

## Commandes Liées aux Volumes

- **Créer un volume** : `docker volume create mon_volume`
- **Lister les volumes** : `docker volume ls`
- **Inspecter un volume** : `docker volume inspect mon_volume`
- **Supprimer un volume** : `docker volume rm mon_volume`

## Autres Commandes Docker Importantes

- **Voir les logs d'un conteneur** : `docker logs mon_conteneur`
- **Exécuter une commande dans un conteneur en cours d'exécution** : `docker exec -it mon_conteneur bash`
- **Arrêter un conteneur** : `docker stop mon_conteneur`
- **Démarrer un conteneur arrêté** : `docker start mon_conteneur`
- **Supprimer un conteneur** : `docker rm mon_conteneur`

## Commandes à Utiliser dans un Dockerfile

- `FROM` : Définit l'image de base.
- `RUN` : Exécute des commandes.
- `CMD` : Définit la commande par défaut.
- `ENTRYPOINT` : Configure un conteneur pour s'exécuter comme un exécutable.
- `EXPOSE` : Expose des ports.
- `ENV` : Définit des variables d'environnement.
- `COPY` / `ADD` : Copie des fichiers depuis le système de fichiers local vers l'image.
- `VOLUME` : Crée un point de montage pour un volume.
- `WORKDIR` : Définit le répertoire de travail.
- `USER` : Définit l'utilisateur (par UID/GID).

## Docker Compose

- **Syntaxe de base d'un fichier `docker-compose.yml`** :
  ```yaml
  version: '3'
  services:
    web:
      image: nginx:alpine
      ports:
        - "80:80"
      volumes:
        - webdata:/var/www/html
  volumes:
    webdata:
  ```

- **Commandes CLI Docker Compose** :
  - **Démarrer les services** : `docker-compose up`
  - **Arrêter les services** : `docker-compose down`
  - **Construire ou reconstruire les services** : `docker-compose build`
  - **Voir les logs** : `docker-compose logs`
  - **Exécuter une commande dans un service** : `docker-compose exec service_name command`

Cette vue d'ensemble couvre les bases des commandes Docker, du Dockerfile et de Docker Compose, avec les options et commandes les plus couramment utilisées. Pour chaque catégorie, des exemples sont fournis pour illustrer l'utilisation typique.

---

[...retour au sommaire](../sommaire.md)
