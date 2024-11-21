# Installation et Configuration de SonarQube sur un VPS Privé

[Menu CI](../menu.md)

---

Cette documentation explique toutes les étapes nécessaires pour installer, configurer et faire tourner un serveur SonarQube sur un VPS privé. Elle inclut des solutions aux problèmes courants rencontrés lors de l'installation et des configurations spécifiques pour garantir un environnement optimisé.

---

## Prérequis

### Configuration minimale requise
- **RAM :** 2 Go minimum (4 Go recommandés).
- **Processeur :** 2 cœurs minimum.
- **Stockage :** Au moins 10 Go d'espace libre.
- **OS :** Linux (Ubuntu recommandé).
- **Docker et Docker Compose :** Installés et configurés.

---

### Étape 1 : Vérifications initiales et mise à jour du VPS

#### 1. Mettez à jour votre système
Avant de commencer, assurez-vous que votre système est à jour :
```bash
sudo apt update && sudo apt upgrade -y
```

#### 2. Vérifiez les ressources système
- Vérifiez la RAM disponible :
  ```bash
  free -h
  ```
- Vérifiez l'espace disque :
  ```bash
  df -h
  ```

#### 3. Configurez le swap si nécessaire
Si votre serveur dispose de moins de 4 Go de RAM, configurez un fichier de swap pour éviter les problèmes de mémoire :
```bash
sudo fallocate -l 2G /swapfile
sudo chmod 600 /swapfile
sudo mkswap /swapfile
sudo swapon /swapfile
echo '/swapfile none swap sw 0 0' | sudo tee -a /etc/fstab
```

**Rôle :** Le swap agit comme de la RAM supplémentaire en cas de surcharge, évitant les erreurs liées au manque de mémoire.

---

## Étape 2 : Installer Docker et Docker Compose

#### 1. Installation de Docker
1. Ajoutez les dépendances nécessaires :
   ```bash
   sudo apt install -y apt-transport-https ca-certificates curl software-properties-common
   ```

2. Ajoutez la clé GPG de Docker :
   ```bash
   curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -
   ```

3. Ajoutez le dépôt Docker à votre système :
   ```bash
   sudo add-apt-repository "deb [arch=amd64] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable"
   ```

4. Installez Docker :
   ```bash
   sudo apt update
   sudo apt install -y docker-ce
   ```

5. Vérifiez l'installation :
   ```bash
   docker --version
   ```

#### 2. Installation de Docker Compose
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

## Étape 3 : Préparer l'environnement

#### 1. Organisez les répertoires
Créez une structure de répertoires pour SonarQube et PostgreSQL :
```bash
mkdir -p /sonar/{data,extensions,logs,postgres}
```

#### 2. Configurez le réseau Docker
Créez un réseau Docker spécifique pour SonarQube :
```bash
docker network create sonar_network
```

**Rôle :** Un réseau Docker dédié permet une communication isolée entre les conteneurs SonarQube et PostgreSQL.

---

## Étape 4 : Créer le fichier `docker-compose.yml`

1. Créez le fichier dans le répertoire `/sonar` :
   ```bash
   cd /sonar
   vim docker-compose.yml
   ```

2. Collez le contenu suivant et adaptez si nécessaire :
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
         - SONAR_SEARCH_JAVAOPTS=-Xms512m -Xmx2048m -XX:+HeapDumpOnOutOfMemoryError
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

## Étape 5 : Lancer SonarQube

1. Lancez les conteneurs :
   ```bash
   docker-compose up -d
   ```

2. Vérifiez que les services sont opérationnels :
   ```bash
   docker ps
   ```

3. Accédez à l'interface web :
   - URL : `http://<IP-PUBLIC-VPS>:9000`
   - Identifiants par défaut :
     - **Utilisateur :** admin
     - **Mot de passe :** admin

4. **Changez immédiatement le mot de passe admin pour des raisons de sécurité.**

---

## Étape 6 : Résolution des Problèmes Courants

### Problème 1 : Mémoire insuffisante
**Symptômes :** Erreurs `OutOfMemoryError`.

**Solution :**
- Augmentez la mémoire swap comme décrit dans l'étape 1.
- Adaptez les options JVM dans `docker-compose.yml` :
  ```yaml
  - SONAR_SEARCH_JAVAOPTS=-Xms512m -Xmx2048m
  ```

---

### Problème 2 : Accès réseau refusé
**Symptômes :** Impossible d'accéder à SonarQube via l'IP publique.

**Solution :**
- Ouvrez le port 9000 dans le pare-feu :
  ```bash
  sudo ufw allow 9000
  sudo ufw reload
  ```

---

### Problème 3 : Perte de données après redémarrage
**Symptômes :** Extensions ou logs manquants.

**Solution :**
- Assurez-vous que les volumes sont correctement configurés dans `docker-compose.yml`.

---

## Étape 7 : Maintenance et Mise à Jour

1. **Sauvegardez les volumes :**
   ```bash
   tar -czf sonar_backup.tar.gz /sonar
   ```

2. **Mettez à jour les images Docker :**
   ```bash
   docker-compose pull
   docker-compose up -d
   ```

3. **Surveillez les logs :**
   ```bash
   docker logs -f sonarqube
   ```

---

[Menu CI](../menu.md)
