# Déployer un Docker Registry Privé avec Docker Compose

[Menu CI](../menu.md)

---

## Étape 1 : Installation des dépendances

### 1.1 Mettre à jour le serveur
Assurez-vous que votre système est à jour :
```bash
sudo apt update && sudo apt upgrade -y
```

### 1.2 Installer Docker
Installez Docker avec le script officiel :
```bash
curl -fsSL https://get.docker.com -o get-docker.sh
sudo sh get-docker.sh
```

Vérifiez l'installation :
```bash
docker --version
```

### 1.3 Installer Docker Compose
Installez Docker Compose :
```bash
sudo apt install docker-compose -y
```

Vérifiez l'installation :
```bash
docker-compose --version
```

---

## Étape 2 : Préparer l’environnement

### 2.1 Organiser les répertoires
Créez une structure de dossiers pour organiser les fichiers nécessaires :
```bash
mkdir -p ~/docker-registry/{data,certs,auth}
cd ~/docker-registry
```
- `data/` : Stockage persistant des images.
- `certs/` : Stockage des certificats SSL.
- `auth/` : Fichier d'authentification pour gérer les utilisateurs.

### 2.2 Obtenir ou générer des certificats SSL

#### Avec Let’s Encrypt (Recommandé)
1. Installez Certbot :
   ```bash
   sudo apt install certbot -y
   ```
2. Générez des certificats SSL pour votre domaine :
   ```bash
   sudo certbot certonly --standalone -d your-domain.com
   ```
3. Copiez les certificats dans le dossier `certs/` :
   ```bash
   sudo cp /etc/letsencrypt/live/your-domain.com/fullchain.pem ~/docker-registry/certs/domain.crt
   sudo cp /etc/letsencrypt/live/your-domain.com/privkey.pem ~/docker-registry/certs/domain.key
   ```

#### Certificat auto-signé (Pour les tests)
Générez un certificat auto-signé :
```bash
openssl req -newkey rsa:4096 -nodes -sha256 -keyout certs/domain.key -x509 -days 365 -out certs/domain.crt
```

### 2.3 Configurer l’authentification
Créez un fichier `htpasswd` pour gérer les utilisateurs :
```bash
docker run --entrypoint htpasswd httpd:2 -Bbn username password > auth/htpasswd
```
Remplacez `username` et `password` par vos identifiants. Pour ajouter d'autres utilisateurs :
```bash
docker run --entrypoint htpasswd httpd:2 -B username2 password2 >> auth/htpasswd
```

---

## Étape 3 : Configurer Docker Compose

Créez un fichier `docker-compose.yml` dans le répertoire `~/docker-registry` :
```bash
nano docker-compose.yml
```

Ajoutez le contenu suivant :
```yaml
version: '3'

services:
  registry:
    image: registry:2
    container_name: registry
    ports:
      - "5000:5000"
    environment:
      REGISTRY_STORAGE_FILESYSTEM_ROOTDIRECTORY: /var/lib/registry
      REGISTRY_HTTP_TLS_CERTIFICATE: /certs/domain.crt
      REGISTRY_HTTP_TLS_KEY: /certs/domain.key
      REGISTRY_AUTH: htpasswd
      REGISTRY_AUTH_HTPASSWD_REALM: "Registry Realm"
      REGISTRY_AUTH_HTPASSWD_PATH: /auth/htpasswd
    volumes:
      - ./data:/var/lib/registry
      - ./certs:/certs
      - ./auth:/auth
    restart: always
```

**Explications** :
- `REGISTRY_STORAGE_FILESYSTEM_ROOTDIRECTORY` : Définit l'emplacement des images Docker.
- `REGISTRY_HTTP_TLS_CERTIFICATE` et `REGISTRY_HTTP_TLS_KEY` : Sécurisent le registry avec HTTPS.
- `REGISTRY_AUTH` : Active l'authentification.
- Les volumes assurent la persistance des données, certificats et fichiers d'authentification.

---

## Étape 4 : Lancer et tester le Docker Registry

### 4.1 Démarrer le service
Lancez le Docker Registry en arrière-plan :
```bash
docker-compose up -d
```

Vérifiez que le container est en cours d’exécution :
```bash
docker ps
```

### 4.2 Tester le registry
Connectez-vous à votre Docker Registry :
```bash
docker login your-domain.com:5000
```
Entrez le nom d'utilisateur et le mot de passe configurés.

Poussez une image Docker vers le registry :
1. Taggez une image :
   ```bash
   docker tag ubuntu:latest your-domain.com:5000/ubuntu
   ```
2. Poussez l'image :
   ```bash
   docker push your-domain.com:5000/ubuntu
   ```

Récupérez une image depuis le registry :
```bash
docker pull your-domain.com:5000/ubuntu
```

---

## Étape 5 : Configurations avancées

### 5.1 Augmenter la taille des uploads (optionnel)
Modifiez le fichier `nginx.conf` si vous utilisez Nginx pour un proxy :
```bash
sudo nano /etc/nginx/nginx.conf
```
Ajoutez ou modifiez :
```nginx
http {
    client_max_body_size 16384m;
}
```
Redémarrez Nginx :
```bash
sudo systemctl restart nginx
```

### 5.2 Planifier le renouvellement des certificats (Let’s Encrypt)
Ajoutez une tâche cron pour renouveler automatiquement les certificats :
```bash
sudo crontab -e
```
Ajoutez la ligne suivante :
```cron
0 3 * * * certbot renew --quiet && docker-compose restart
```

---

## Étape 6 : Maintenance et sauvegardes

### Sauvegarder les données
Copiez le répertoire `data/` vers un emplacement sécurisé :
```bash
tar -czvf registry-backup.tar.gz ~/docker-registry/data
```

### Mettre à jour les images Docker
Mettez à jour l’image Docker Registry :
```bash
docker-compose pull
docker-compose up -d
```

---

## Étape 7 : Intégration dans un pipeline CI/CD

Ajoutez votre Docker Registry dans vos workflows GitHub Actions ou autres outils CI/CD :
```yaml
name: Build and Push

on:
  push:
    branches:
      - main

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Build and push Docker image
        run: |
          echo ${{ secrets.REGISTRY_PASSWORD }} | docker login your-domain.com:5000 -u ${{ secrets.REGISTRY_USERNAME }} --password-stdin
          docker build -t your-domain.com:5000/my-app:$GITHUB_SHA .
          docker push your-domain.com:5000/my-app:$GITHUB_SHA
```

---

[Menu CI](../menu.md)
