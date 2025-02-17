# Déployer un Serveur Drone CI avec GitHub

[Menu CI](../menu.md)

---

Ce tutoriel explique étape par étape comment configurer un serveur Drone CI sur un VPS, en intégrant GitHub pour la gestion des dépôts et Docker pour l'exécution des pipelines.

Inpirée de la doc [ici](https://medium.com/@pavanbelagatti/continuous-integration-self-hosting-drone-ci-1959b961ef5e)

---

## Prérequis

### 1. Compte GitHub
- Un compte GitHub actif.
- Droits d'accès aux dépôts que vous souhaitez intégrer à Drone.

### 2. Serveur VPS
- Un VPS avec une distribution Linux (Ubuntu recommandé).
- Configuration minimale :
  - **RAM :** 2 Go.
  - **Processeur :** 2 cœurs.
  - **Stockage :** 10 Go minimum.
  - **Port 80** ouvert pour HTTP.
- Docker et Docker Compose installés.

### 3. Domaine ou Adresse IP publique
- Une adresse IP publique ou un domaine pour accéder au serveur Drone.
- Si vous n'avez pas de domaine, utilisez directement l'IP publique.

---

## Étape 1 : Installer Docker et Docker Compose

### Installation de Docker
1. Connectez-vous à votre VPS via SSH.
2. Exécutez les commandes suivantes :
   ```bash
   sudo apt update
   sudo apt install -y apt-transport-https ca-certificates curl software-properties-common
   curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -
   sudo add-apt-repository "deb [arch=amd64] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable"
   sudo apt update
   sudo apt install -y docker-ce
   ```

3. Vérifiez que Docker est installé :
   ```bash
   docker --version
   ```

### Installation de Docker Compose
1. Téléchargez Docker Compose :
   ```bash
   sudo curl -L "https://github.com/docker/compose/releases/latest/download/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
   ```

2. Rendez-le exécutable :
   ```bash
   sudo chmod +x /usr/local/bin/docker-compose
   ```

3. Vérifiez l'installation :
   ```bash
   docker-compose --version
   ```

---

## Étape 2 : Configurer une Application OAuth sur GitHub

Drone CI utilise OAuth pour s'authentifier auprès de GitHub. Suivez ces étapes pour configurer votre application OAuth.

### 1. Accédez à GitHub
- Connectez-vous à votre compte GitHub.
- Allez dans **Settings > Developer settings > OAuth Apps** : [Lien direct vers OAuth Apps](https://github.com/settings/developers).

### 2. Créez une Nouvelle Application OAuth
1. Cliquez sur **New OAuth App**.
2. Renseignez les champs :
   - **Application name :** `Drone CI`.
   - **Homepage URL :** `http://<IP-PUBLIC-VPS>` (remplacez `<IP-PUBLIC-VPS>` par l'adresse IP publique de votre VPS ou votre domaine).
   - **Authorization callback URL :** `http://<IP-PUBLIC-VPS>/login`.
3. Cliquez sur **Register application**.

### 3. Sauvegardez les Informations OAuth
Une fois l'application créée :
- Notez **Client ID**.
- Cliquez sur **Generate a new client secret** et sauvegardez **Client Secret**.

---

## Étape 3 : Créer les Fichiers de Configuration sur le VPS

### 1. Fichier `.env`
1. Connectez-vous à votre VPS.
2. Créez un fichier `.env` dans votre répertoire de travail :
   ```bash
   vim .env
   ```

3. Ajoutez le contenu suivant, en remplaçant les valeurs :
   ```env
   # Configuration serveur Drone
   DRONE_SERVER_HOST=<IP-PUBLIC-VPS>
   DRONE_SERVER_PROTO=http
   DRONE_GITHUB_CLIENT_ID=<VOTRE_CLIENT_ID>
   DRONE_GITHUB_CLIENT_SECRET=<VOTRE_CLIENT_SECRET>
   DRONE_RPC_SECRET=<SECRET_DRONE>
   DRONE_USER_CREATE=username:<VOTRE_USERNAME_GITHUB>,admin:true

   # Configuration Runner Drone
   DRONE_RPC_HOST=<IP-PUBLIC-VPS>
   DRONE_RPC_PROTO=http
   DRONE_RUNNER_NAME="Drone.io_runner"
   ```

- **Remplacez :**
  - `<IP-PUBLIC-VPS>` : Par l'adresse IP publique ou le domaine.
  - `<VOTRE_CLIENT_ID>` et `<VOTRE_CLIENT_SECRET>` : Par les valeurs générées sur GitHub.
  - `<SECRET_DRONE>` : Générez un secret avec cette commande :
    ```bash
    openssl rand -hex 16
    ```
  - `<VOTRE_USERNAME_GITHUB>` : Par votre nom d'utilisateur GitHub.

---

### 2. Fichier `docker-compose.yml`
1. Créez un fichier `docker-compose.yml` :
   ```bash
   vim docker-compose.yml
   ```

2. Collez le contenu suivant :
   ```yaml
   version: '3.8'

   services:
     drone:
       image: drone/drone:2
       volumes:
         - /var/run/docker.sock:/var/run/docker.sock
         - ./volumes/drone:/data
       restart: always
       ports:
         - 80:80
       env_file:
         - .env

     drone-runner:
       image: drone/drone-runner-docker:1
       command: agent
       restart: always
       depends_on:
         - drone
       volumes:
         - /var/run/docker.sock:/var/run/docker.sock
       env_file:
         - .env
   ```

---

## Étape 4 : Lancer Drone CI

1. Démarrez les services Drone CI :
   ```bash
   sudo docker-compose up -d
   ```

2. Vérifiez que les conteneurs sont en cours d'exécution :
   ```bash
   sudo docker ps
   ```

3. Accédez à l'interface Drone :
   - URL : `http://<IP-PUBLIC-VPS>`.

---

## Étape 5 : Configurer un Projet avec Drone CI

### 1. Créer un Nouveau Dépôt GitHub
1. Créez un dépôt sur GitHub.
2. Ajoutez un fichier `.drone.yml` à la racine du projet :
   ```yaml
   kind: pipeline
   type: docker
   name: default

   steps:
     - name: build
       image: alpine
       commands:
         - echo "Hello, Drone CI!"
   ```

### 2. Ajouter le Projet à Drone
1. Connectez-vous à l'interface Drone CI.
2. Cliquez sur **Sync** pour synchroniser vos dépôts GitHub.
3. Ajoutez le dépôt contenant `.drone.yml`.

---

## Étape 6 : Secrets pour Docker Hub (Optionnel)

1. Dans l'interface Drone, accédez au projet.
2. Ajoutez des secrets :
   - **docker_username** : Votre nom d'utilisateur Docker Hub.
   - **docker_password** : Votre mot de passe Docker Hub.

3. Modifiez le `.drone.yml` pour inclure une étape de publication :
   ```yaml
   steps:
     - name: publish
       image: plugins/docker
       settings:
         username:
           from_secret: docker_username
         password:
           from_secret: docker_password
         repo: your-dockerhub-username/your-repo
   ```

---

## Étape 7 : Résolution des Problèmes

### Problème : Échec de l'authentification GitHub
- **Cause :** Mauvaise configuration de l'application OAuth.
- **Solution :** Vérifiez les URLs configurées sur GitHub (Homepage et Callback).

### Problème : Erreur de connexion Runner-Serveur
- **Cause :** Mauvaise valeur pour `DRONE_RPC_SECRET`.
- **Solution :** Assurez-vous que le même secret est utilisé dans le fichier `.env` et dans la configuration du Runner.

---

[Menu CI](../menu.md)