# Installation et Configuration de SonarQube sur un VPS Privé

[Menu CI](../menu.md)

---

Cette documentation explique toutes les étapes nécessaires pour installer, configurer et faire tourner un serveur SonarQube sur un VPS privé. Elle inclut des solutions aux problèmes courants rencontrés lors de l'installation.

---

## Prérequis

### Configuration minimale requise
- **RAM :** 2 Go minimum (4 Go recommandés).
- **Processeur :** 2 cœurs minimum.
- **Stockage :** Au moins 10 Go d'espace libre.
- **OS :** Linux (Ubuntu recommandé).
- **Docker et Docker Compose :** Installés et configurés.

### Installer Docker et Docker Compose
1. **Mettez à jour le système :**
   ```bash
   sudo apt update && sudo apt upgrade -y
   ```

2. **Installez Docker :**
   ```bash
   sudo apt install docker.io -y
   ```

3. **Activez et démarrez Docker :**
   ```bash
   sudo systemctl enable docker
   sudo systemctl start docker
   ```

4. **Installez Docker Compose :**
   ```bash
   sudo apt install docker-compose -y
   ```

5. **Vérifiez les versions :**
   ```bash
   docker --version
   docker-compose --version
   ```

---

## Configuration de l'environnement

### Organisation des fichiers
Créez un répertoire dédié pour SonarQube :
```bash
mkdir -p /sonar/{data,extensions,logs}
```

### Configuration du réseau Docker
Créez un réseau spécifique pour SonarQube :
```bash
docker network create sonar_network
```

---

## Fichier `docker-compose.yml`

Créez le fichier `docker-compose.yml` dans le répertoire `/sonar` :
```yaml
version: "3.9"

services:
  sonarqube:
    image: sonarqube:lts
    container_name: sonarqube
    networks:
      - sonar_network
    ports:
      - "9000:9000"
    environment:
      - SONAR_JDBC_URL=jdbc:postgresql://db:5432/sonar
      - SONAR_JDBC_USERNAME=sonar
      - SONAR_JDBC_PASSWORD=securepassword
      - SONAR_SEARCH_JAVAOPTS=-Xms512m -Xmx1024m -XX:+HeapDumpOnOutOfMemoryError
    volumes:
      - /sonar/data:/opt/sonarqube/data
      - /sonar/extensions:/opt/sonarqube/extensions
      - /sonar/logs:/opt/sonarqube/logs

  db:
    image: postgres:alpine
    container_name: sonar_postgres
    networks:
      - sonar_network
    environment:
      - POSTGRES_USER=sonar
      - POSTGRES_PASSWORD=securepassword
      - POSTGRES_DB=sonar
    volumes:
      - /sonar/postgres:/var/lib/postgresql/data

networks:
  sonar_network:
    external: true
```

---

## Lancer SonarQube

1. **Démarrez les conteneurs :**
   ```bash
   cd /sonar
   docker-compose up -d
   ```

2. **Vérifiez le statut des conteneurs :**
   ```bash
   docker ps
   ```

3. **Accédez à l'interface SonarQube :**
   - URL : `http://<VPS_IP>:9000`
   - Identifiants par défaut : 
     - **Utilisateur :** admin
     - **Mot de passe :** admin

4. **Changez le mot de passe par défaut pour des raisons de sécurité.**

---

## Résolution des problèmes courants

### **Problème 1 : Mémoire insuffisante**
#### Symptômes :
- SonarQube ne démarre pas ou affiche des erreurs `OutOfMemoryError`.

#### Solution :
- Ajoutez ou augmentez la mémoire SWAP :
  ```bash
  sudo fallocate -l 1G /swapfile
  sudo chmod 600 /swapfile
  sudo mkswap /swapfile
  sudo swapon /swapfile
  echo '/swapfile none swap sw 0 0' | sudo tee -a /etc/fstab
  ```

- Ajustez les paramètres JVM :
  ```yaml
  environment:
    - SONAR_SEARCH_JAVAOPTS=-Xms512m -Xmx2048m
  ```

---

### **Problème 2 : Accès réseau refusé**
#### Symptômes :
- Impossible d'accéder à SonarQube via l'URL.

#### Solution :
- Vérifiez que le port 9000 est ouvert :
  ```bash
  sudo ufw allow 9000
  sudo ufw reload
  ```

- Si vous utilisez un proxy inverse (Nginx) pour HTTPS, configurez-le comme suit :
  ```nginx
  server {
    listen 80;
    server_name yourdomain.com;

    location / {
      proxy_pass http://localhost:9000;
      proxy_set_header Host $host;
      proxy_set_header X-Real-IP $remote_addr;
    }
  }
  ```

---

### **Problème 3 : Base de données inaccessible**
#### Symptômes :
- Erreur de connexion à PostgreSQL.

#### Solution :
- Vérifiez les variables d'environnement pour la connexion JDBC dans `docker-compose.yml`.
- Redémarrez les conteneurs pour appliquer les modifications :
  ```bash
  docker-compose down && docker-compose up -d
  ```

---

### **Problème 4 : Perte de données après redémarrage**
#### Symptômes :
- Logs ou extensions non persistants.

#### Solution :
- Assurez-vous que les volumes sont correctement définis dans `docker-compose.yml`.

---

### **Problème 5 : Lenteur ou instabilité**
#### Symptômes :
- L'interface est lente ou instable.

#### Solution :
- Limitez les ressources CPU et RAM pour PostgreSQL :
  ```yaml
  deploy:
    resources:
      limits:
        memory: 512M
        cpus: "0.5"
  ```

- Mettez à jour les images Docker :
  ```bash
  docker pull sonarqube:lts
  docker pull postgres:alpine
  ```

---

## Maintenance et mises à jour

1. **Sauvegarde des données :**
   - Copiez les répertoires de volumes `/sonar/data`, `/sonar/extensions`, et `/sonar/postgres`.

2. **Mise à jour des images Docker :**
   ```bash
   docker-compose pull
   docker-compose up -d
   ```

3. **Surveillance des logs :**
   ```bash
   docker logs -f sonarqube
   ```

---

[Menu CI](../menu.md)
