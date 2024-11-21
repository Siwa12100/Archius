# Guide : Build, Push et Déployer une Image Docker sur un VPS avec Drone CI

[Menu CI](../menu.md)

---

## Prérequis

### Sur le VPS
1. **Docker** et **Docker Compose** installés :
   ```bash
   sudo apt update && sudo apt install -y docker.io docker-compose
   ```
2. Assurez-vous que votre serveur dispose des ports nécessaires ouverts pour accéder à votre application.

### Sur Drone CI
1. Une instance fonctionnelle de **Drone CI**.
2. Les secrets nécessaires configurés dans Drone CI (voir plus bas).

### Sur votre infrastructure Docker Registry
1. Un **Docker Registry privé** (auto-hébergé ou un service comme Docker Hub) configuré pour recevoir les images Docker.
2. Un accès sécurisé au registry avec :
   - Nom de domaine ou IP du registry (ex. `registry.ecirada.valorium-mc.fr`).
   - Authentification par nom d'utilisateur/mot de passe.

---

## Étape 1 : Préparer l'Image Docker

### 1.1 Créer un `Dockerfile`
Dans votre projet, ajoutez un fichier `Dockerfile` pour construire l’image Docker. Voici un exemple pour une application frontend :
```Dockerfile
# Utiliser une image Node.js pour builder le projet
FROM node:16 as build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Utiliser une image NGINX pour servir l’application
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

### 1.2 Structure du projet
Assurez-vous que votre projet est bien organisé. Par exemple :
```
drosolab-frontend/
├── Dockerfile
├── package.json
├── src/
├── public/
└── dist/ (généré après le build)
```

---

## Étape 2 : Configurer Drone CI pour le Build et le Push

### 2.1 Ajouter les secrets dans Drone CI
Les secrets sont des informations sensibles (comme les mots de passe ou tokens). Voici comment les ajouter dans Drone CI :
1. **Accédez à l'interface Drone CI.**
2. Allez dans les **Settings** de votre dépôt.
3. Ajoutez les secrets suivants :
   - `registry_username`: Le nom d'utilisateur pour accéder au registry.
   - `registry_password`: Le mot de passe pour accéder au registry.
   - `ssh_user`: Le nom d'utilisateur pour accéder au VPS via SSH.
   - `ssh_password`: Le mot de passe SSH pour accéder au VPS.

### 2.2 Ajouter le pipeline Drone CI
Ajoutez le fichier `.drone.yml` à la racine de votre projet avec le contenu suivant :
```yaml
kind: pipeline
type: docker
name: default

steps:
  - name: build-push-image
    image: plugins/docker
    settings:
      registry: registry.ecirada.valorium-mc.fr
      repo: registry.ecirada.valorium-mc.fr/drosolab
      username:
        from_secret: registry_username
      password:
        from_secret: registry_password
      dockerfile: drosolab-frontend/Dockerfile
      context: drosolab-frontend
      tags:
        - latest
    depends_on:
      - tests

  - name: deploiment-vps-dev
    image: appleboy/drone-ssh
    settings:
      host: 149.7.5.30
      username: 
        from_secret: ssh_user
      password: 
        from_secret: ssh_password
      port: 22
      script:
        - docker ps -q --filter "name=drosolab-front-dev" | xargs -r docker stop
        - docker ps -a -q --filter "name=drosolab-front-dev" | xargs -r docker rm
        - docker images -q registry.ecirada.valorium-mc.fr/drosolab:latest | xargs -r docker rmi
        - docker pull registry.ecirada.valorium-mc.fr/drosolab:latest
        - docker run -d --restart=always -p 21002:80 --name drosolab-front-dev registry.ecirada.valorium-mc.fr/drosolab:latest
    when:
      branch:
        - dev
    depends_on:
      - build-push-image
```

**Explications :**
- **`build-push-image`** : Étape pour construire l’image Docker et la pousser vers le registry.
  - `registry`: URL de votre registry Docker.
  - `repo`: Nom du repository dans le registry.
  - `dockerfile`: Chemin vers le `Dockerfile`.
  - `context`: Répertoire où se trouve le projet.
  - `tags`: Balises appliquées à l’image (ex. `latest`).
- **`deploiment-vps-dev`** : Étape pour déployer l’image sur un VPS via SSH.
  - `host`: Adresse IP du VPS.
  - `script`: Liste des commandes exécutées sur le VPS.

---

## Étape 3 : Déployer et Tester

### 3.1 Vérifier le pipeline Drone CI
1. Poussez les modifications (incluant `.drone.yml`) vers la branche configurée (ex. `dev`).
2. Drone CI déclenchera automatiquement le pipeline.

### 3.2 Étapes exécutées par le pipeline

#### Build et Push
1. Drone CI construit l’image à partir du `Dockerfile`.
2. L’image est taggée et poussée vers le Docker Registry privé.

#### Déploiement sur le VPS
1. Drone CI se connecte au VPS via SSH.
2. Il arrête et supprime tout container existant correspondant à l’application.
3. Il supprime les anciennes images locales de l’application pour éviter des conflits.
4. Il télécharge (pull) la dernière image depuis le registry.
5. Il exécute un nouveau container avec la dernière version de l’application.

---

## Étape 4 : Sécuriser le Pipeline

### 4.1 Utiliser HTTPS pour le Registry
Assurez-vous que votre Docker Registry est sécurisé avec un certificat SSL :
- Si vous utilisez Let’s Encrypt :
  ```bash
  sudo certbot certonly --standalone -d registry.ecirada.valorium-mc.fr
  ```
- Configurez le registry pour utiliser HTTPS (voir le guide précédent).

### 4.2 Protéger les secrets Drone CI
- Ajoutez les secrets uniquement dans l’interface Drone CI et ne les incluez jamais dans le code source.
- Limitez les permissions des secrets pour éviter les fuites.

---

## Étape 5 : Résolution des Problèmes

### Erreur 1 : Impossible de pusher l’image
**Cause :** Problème d’authentification ou de configuration du registry.
**Solution :**
- Vérifiez que les secrets `registry_username` et `registry_password` sont corrects.
- Assurez-vous que le registry est accessible via HTTPS ou une configuration correcte pour les certificats auto-signés.

### Erreur 2 : Le container ne démarre pas sur le VPS
**Cause :** Problème dans le script SSH.
**Solution :**
- Connectez-vous manuellement au VPS pour exécuter les commandes et identifier les erreurs.
- Vérifiez que les ports nécessaires sont ouverts.

### Erreur 3 : Le pipeline Drone CI échoue
**Cause :** Mauvaise configuration dans le `.drone.yml`.
**Solution :**
- Vérifiez les chemins (`dockerfile`, `context`) et la syntaxe YAML.

---

## Étape 6 : Optimisations et Bonnes Pratiques

- **Tagging des images :** Utilisez des tags significatifs (par exemple, `v1.0`, `commit-sha`).
- **Logs :** Surveillez les logs de Drone CI pour identifier rapidement les problèmes.
- **Backups :** Sauvegardez régulièrement les données du Docker Registry.

---

[Menu CI](../menu.md)