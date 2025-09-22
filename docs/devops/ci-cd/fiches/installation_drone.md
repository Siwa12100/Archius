# Déployer un Serveur Drone CI avec GitHub et HTTPS

[Menu CI](../menu.md)

---

Ce tutoriel explique comment configurer un serveur **Drone CI** sur un VPS, en intégrant **GitHub** pour la gestion des dépôts, **Docker** pour l'exécution des pipelines et **Let's Encrypt** pour la gestion automatique du HTTPS avec **Nginx Proxy**.

Inspiré de la documentation [ici](https://medium.com/@pavanbelagatti/continuous-integration-self-hosting-drone-ci-1959b961ef5e).

---

## **Prérequis**

### **1. Compte GitHub**
- Un compte GitHub actif.
- Droits d'accès aux dépôts à intégrer à Drone.

### **2. Serveur VPS**
- Un VPS avec une distribution Linux (Ubuntu recommandé).
- Configuration minimale :
  - **RAM :** 2 Go.
  - **Processeur :** 2 cœurs.
  - **Stockage :** 10 Go minimum.
  - **Port 80 et 443** ouverts pour HTTP et HTTPS.
- **Docker et Docker Compose** installés.

### **3. Domaine ou Adresse IP publique**
- Un **domaine** configuré (ex: `drone.ioa-pais.fr`).
- Si vous n'avez pas de domaine, utilisez l'**IP publique**, mais HTTPS nécessitera un certificat autosigné.

---

## **Étape 1 : Configurer une Application OAuth sur GitHub**

### **1. Accédez à GitHub**
- Connectez-vous à GitHub.
- Allez dans **Settings > Developer settings > OAuth Apps** : [Lien direct](https://github.com/settings/developers).

### **2. Créez une Nouvelle Application OAuth**
1. Cliquez sur **New OAuth App**.
2. Renseignez les champs :
   - **Application name :** `Drone CI`.
   - **Homepage URL :** `https://drone.ioa-pais.fr` (remplacez avec votre domaine/IP).
   - **Authorization callback URL :** `https://drone.ioa-pais.fr/login`.
3. Cliquez sur **Register application**.

### **3. Sauvegardez les Informations OAuth**
- Notez **Client ID**.
- Cliquez sur **Generate a new client secret** et sauvegardez **Client Secret**.

---

## **Étape 2 : Créer les Fichiers de Configuration**

### **1. Fichier `.env`**
1. Créez un fichier `.env` :
   ```bash
   vim .env
   ```

2. Ajoutez :
   ```env
   # Configuration serveur Drone
   DRONE_SERVER_HOST=drone.ioa-pais.fr
   DRONE_SERVER_PROTO=https
   DRONE_GITHUB_CLIENT_ID=<VOTRE_CLIENT_ID>
   DRONE_GITHUB_CLIENT_SECRET=<VOTRE_CLIENT_SECRET>
   DRONE_RPC_SECRET=<SECRET_DRONE>
   DRONE_TLS_AUTOCERT=false
   DRONE_COOKIE_SECURE=true
   DRONE_USER_CREATE=username:<VOTRE_USERNAME_GITHUB>,admin:true

   # Configuration Runner Drone
   DRONE_RPC_HOST=drone.ioa-pais.fr
   DRONE_RPC_PROTO=https
   DRONE_RUNNER_NAME="Drone.io_runner"
   ```

- **Remplacez :**
  - `<VOTRE_CLIENT_ID>` et `<VOTRE_CLIENT_SECRET>` par ceux générés sur GitHub.
  - `<SECRET_DRONE>` avec une clé générée via :
    ```bash
    openssl rand -hex 16
    ```
  - `<VOTRE_USERNAME_GITHUB>` par votre nom d'utilisateur GitHub.

---

### **2. Fichier `docker-compose.yml`**
1. Créez un fichier `docker-compose.yml` :
   ```bash
   vim docker-compose.yml
   ```

2. Collez :
   ```yaml
   version: '3.8'

   services:
     drone:
       image: drone/drone:2
       restart: always
       volumes:
         - /var/run/docker.sock:/var/run/docker.sock
         - ./volumes/drone:/data
       ports:
         - 40001:80  # Permet le debug direct
       expose:
         - "80"  # Nginx Proxy doit voir ce port
       env_file:
         - .env
       environment:
         - DRONE_SERVER_HOST=drone.ioa-pais.fr
         - DRONE_SERVER_PROTO=https
         - DRONE_RPC_PROTO=https
         - VIRTUAL_HOST=drone.ioa-pais.fr
         - LETSENCRYPT_HOST=drone.ioa-pais.fr
         - LETSENCRYPT_EMAIL=jean.marcillac12@gmail.com
       networks:
         - nginx-proxy-network

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

   networks:
     nginx-proxy-network:
       external: true
   ```

---

## **Étape 3 : Spécificités de Drone avec HTTPS et Nginx Proxy**

Contrairement à SonarQube ou une registry Docker, **Drone nécessite une configuration précise** pour éviter les erreurs de redirection OAuth et d’accès HTTPS.

### **1. Drone doit connaître son URL publique exacte**
👉 **Problème rencontré :**
- Si Drone pense être accessible en `http://IP:40001`, mais que l'utilisateur y accède via `https://drone.ioa-pais.fr`, il génère des **redirections erronées** et OAuth échoue.
- Erreur observée : `oauth: invalid or missing state`

✅ **Solution** :
- Définir **DRONE_SERVER_HOST** et **DRONE_SERVER_PROTO** correctement dans `.env`.
- Vérifier que l’URL de callback sur GitHub est **`https://drone.ioa-pais.fr/login`**.

### **2. Nginx Proxy gère HTTPS, Drone doit être en HTTP en interne**
👉 **Problème rencontré :**
- Drone ne doit **pas** gérer HTTPS lui-même, car c’est Nginx Proxy qui s’en charge.

✅ **Solution** :
- **Désactiver le TLS automatique** avec `DRONE_TLS_AUTOCERT=false`.
- **Autoriser les cookies sécurisés** avec `DRONE_COOKIE_SECURE=true`.

### **3. Debug avec accès direct**
- **Le port 40001:80** permet d’accéder à Drone en HTTP localement si besoin.
- `curl -vI http://IP:40001` permet de tester sans passer par le proxy.

---

## **Étape 4 : Démarrer Drone CI**
1. Lancer Drone et le runner :
   ```bash
   sudo docker-compose up -d
   ```
2. Vérifier les conteneurs :
   ```bash
   sudo docker ps
   ```
3. Accéder à Drone via **https://drone.ioa-pais.fr**.

---

## **Étape 5 : Ajouter un Projet**
Ajoutez un `.drone.yml` dans votre dépôt :
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

Puis activez-le via l’interface Drone.

---

🎉 **Votre serveur Drone CI est maintenant fonctionnel avec HTTPS et OAuth !** 🚀

[Menu CI](../menu.md)

