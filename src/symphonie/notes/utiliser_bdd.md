# Déploiement d’une Base de Données MySQL avec Docker-Compose et Utilisation avec Symfony

[Menu Symfony](../menu.md)

## 1. Introduction
L’objectif de ce cours est de vous guider pas à pas dans le déploiement d’une base de données MySQL avec Docker-Compose, son intégration avec Symfony, et la gestion de différents environnements (développement et production). Nous aborderons également l’importance des fichiers `.env` et `.env.local`.

---

## 2. Pré-requis
Avant de commencer, assurez-vous d’avoir les outils suivants installés :
- Docker et Docker-Compose
- Symfony CLI
- Composer
- MySQL client (optionnel pour vérification)

---

## 3. Configuration de Symfony avec Doctrine

### 3.1 Installation de Doctrine
Dans votre projet Symfony, commencez par installer Doctrine si ce n’est pas encore fait :
```bash
composer require symfony/orm-pack
composer require doctrine/doctrine-bundle
```

### 3.2 Configuration de la Connexion à la Base de Données
Symfony utilise un fichier `.env` pour gérer les variables d’environnement. Voici un exemple de configuration de base :
```env
DATABASE_URL="mysql://user:password@127.0.0.1:3306/nom_de_la_base?serverVersion=8.0"
```
- **`user`** : Nom d’utilisateur MySQL.
- **`password`** : Mot de passe.
- **`127.0.0.1`** : Adresse locale.
- **`nom_de_la_base`** : Nom de la base de données.

---

## 4. Déploiement de MySQL avec Docker-Compose

### 4.1 Création d’un Réseau Docker Personnalisé
Pour permettre aux conteneurs Symfony et MySQL de communiquer, nous créons un réseau Docker :
```bash
docker network create symfony_mysql_network
```

### 4.2 Création du Fichier `docker-compose.yml`
Voici un exemple de fichier `docker-compose.yml` configuré pour MySQL et Symfony :
```yaml
version: '3.8'

services:
  mysql:
    image: mysql:8.0
    container_name: mysql_container
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: root_password
      MYSQL_DATABASE: symfony_db
      MYSQL_USER: symfony_user
      MYSQL_PASSWORD: symfony_pass
    volumes:
      - mysql_data:/var/lib/mysql
    networks:
      - symfony_mysql_network

  symfony:
    image: php:8.2-fpm
    container_name: symfony_container
    volumes:
      - .:/var/www/html
    networks:
      - symfony_mysql_network
    depends_on:
      - mysql

volumes:
  mysql_data:
    driver: local

networks:
  symfony_mysql_network:
    driver: bridge
```

### 4.3 Lancement des Services
Pour lancer les conteneurs en arrière-plan :
```bash
docker-compose up -d
```

### 4.4 Vérification des Conteneurs et du Réseau
Inspectez le réseau pour vérifier la connexion :
```bash
docker network inspect symfony_mysql_network
```

---

## 5. Gestion des Variables d’Environnement avec `.env.local`

### 5.1 Qu’est-ce que `.env.local` ?
Symfony utilise plusieurs fichiers `.env` pour gérer les variables d’environnement. Parmi eux :
- **`.env`** : Fichier par défaut pour les configurations globales.
- **`.env.local`** : Remplace ou complète `.env` pour un environnement local. Il **n’est pas versionné** par Git.

### 5.2 Configuration Locale de la Base de Données
Pour que Symfony se connecte au conteneur MySQL via Docker, ajoutez dans `.env.local` :
```env
DATABASE_URL="mysql://symfony_user:symfony_pass@mysql:3306/symfony_db?serverVersion=8.0"
```
- **`mysql`** : Nom de service Docker pour MySQL.

---

## 6. Différenciation des Environnements (Développement vs Production)

### 6.1 Environnement de Développement
Dans l’environnement de développement :
- Les ports peuvent être exposés pour faciliter l’accès depuis l’hôte.
- Fichier `.env.local` utilisé avec des identifiants locaux.
- Exposez le port MySQL pour des outils comme Adminer :
  ```yaml
  ports:
    - "3306:3306"
  ```

### 6.2 Environnement de Production
Dans l’environnement de production :
- **Ne pas exposer les ports MySQL** pour éviter tout accès externe.
- Utilisez des variables d’environnement sécurisées avec Docker :
  ```yaml
  environment:
    MYSQL_ROOT_PASSWORD: ${MYSQL_ROOT_PASSWORD}
    MYSQL_DATABASE: ${MYSQL_DATABASE}
    MYSQL_USER: ${MYSQL_USER}
    MYSQL_PASSWORD: ${MYSQL_PASSWORD}
  ```

Définissez ces variables dans un fichier `.env.prod` (non versionné) :
```env
MYSQL_ROOT_PASSWORD=secure_root_password
MYSQL_DATABASE=production_db
MYSQL_USER=prod_user
MYSQL_PASSWORD=secure_password
```

---

## 7. Vérification et Migration

### 7.1 Vérification de la Connexion à la Base
Testez la connexion en listant les migrations existantes :
```bash
php bin/console doctrine:migrations:status
```

### 7.2 Exécution des Migrations
Pour appliquer les migrations :
```bash
php bin/console doctrine:migrations:migrate
```

---

## 8. Exportation et Sauvegarde de la Base de Données

### 8.1 Exportation avec `mysqldump`
Pour exporter la base depuis le conteneur Docker :
```bash
docker exec mysql_container /usr/bin/mysqldump -u symfony_user -p symfony_db > backup.sql
```

### 8.2 Automatisation des Sauvegardes
Créez un script pour sauvegarder régulièrement :
```bash
#!/bin/bash
TIMESTAMP=$(date +"%Y%m%d_%H%M%S")
BACKUP_FILE="/path/to/backup/backup_$TIMESTAMP.sql"
docker exec mysql_container /usr/bin/mysqldump -u symfony_user -p symfony_db > $BACKUP_FILE
```

Ajoutez une tâche cron pour automatiser :
```bash
crontab -e
```
Exemple d’exécution quotidienne :
```bash
0 2 * * * /path/to/backup_script.sh
```

---

## 9. Sécurisation des Mots de Passe et Bonnes Pratiques

### 9.1 Variables d’Environnement dans Docker
Utilisez des fichiers `.env` spécifiques à Docker pour stocker les mots de passe :
```yaml
environment:
  MYSQL_ROOT_PASSWORD: ${MYSQL_ROOT_PASSWORD}
```

---

[Menu Symfony](../menu.md)