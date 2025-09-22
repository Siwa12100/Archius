#Mise en place du Déploiement Continu (CD) Gitlab pour un Projet PHP

[...retour sommaire sur CI/CD](../menu.md)

---

## 1. **Présentation du projet**
Ce projet PHP consiste en un site web permettant de gérer une bibliothèque (recherche, ajout, gestion des prêts). Il utilise **Composer** pour la gestion des dépendances PHP, et les tests unitaires sont effectués via **PHPUnit**. Nous avons déjà mis en place la **CI (Continuous Integration)**, qui installe les dépendances, exécute les tests, et vérifie que le code est stable.

L'objectif de cet exercice est d'ajouter le **déploiement continu (CD)** via Docker, en veillant à respecter les bonnes pratiques de Dockerfile avec **Hadolint**.


## 2. **Étapes de mise en place du déploiement continu (CD)**

### A. **Ajout de Dockerfiles pour le projet**

Chaque projet PHP devra être déployé dans un conteneur Docker avec PHP 8 et un serveur web (Apache ou Nginx). Nous allons créer un **Dockerfile** qui installe PHP 8, expose le port 80 et déploie le projet dans le conteneur.

### B. **Validation des bonnes pratiques avec Hadolint**

Hadolint est un outil qui vérifie les bonnes pratiques lors de l'écriture d'un Dockerfile. Il nous aidera à nous assurer que notre Dockerfile respecte toutes les conventions.

### C. **Déploiement continu**

Nous allons configurer les jobs de déploiement continu (CD) dans notre pipeline CI/CD pour :
1. **Construire l'image Docker** de notre projet PHP.
2. **Vérifier les bonnes pratiques du Dockerfile** avec Hadolint.
3. **Déployer l'image** sur un environnement de production/staging en utilisant Docker Compose ou Kubernetes.

### D. **Gestion des bases de données**

Pour le bon fonctionnement de l'application, il est nécessaire de s'assurer que la base de données (par exemple, MySQL) est disponible dans l'environnement Docker.


## 3. **Dockerfile pour le projet PHP**

Voici un exemple de **Dockerfile** qui installe PHP 8 avec Apache, configure l'application et expose le port 80 :

```Dockerfile
# Utilisation de l'image officielle de PHP avec Apache
FROM php:8.0-apache

# Installation des extensions PHP nécessaires (ex: PDO pour MySQL)
RUN docker-php-ext-install pdo pdo_mysql

# Définition de l'environnement de travail
WORKDIR /var/www/html

# Copier le contenu du projet dans le répertoire du serveur
COPY . /var/www/html

# Configuration d'Apache
RUN a2enmod rewrite

# Variables d'environnement pour la configuration du projet
ENV CONTAINER_PATH=/var/www/html
ENV LIBRARY_URL=https://library.example.com
ENV BACKEND_URL=https://backend.example.com

# Expose le port 80 pour l'accès web
EXPOSE 80

# Commande de démarrage (Apache)
CMD ["apache2-foreground"]
```

### Explication détaillée du Dockerfile :
- **FROM php:8.0-apache** : On utilise une image Docker préconfigurée avec PHP 8 et Apache.
- **RUN docker-php-ext-install pdo pdo_mysql** : Installe les extensions PHP nécessaires pour interagir avec une base de données MySQL.
- **WORKDIR /var/www/html** : Définit le répertoire de travail dans le conteneur.
- **COPY . /var/www/html** : Copie le code source du projet dans le conteneur.
- **RUN a2enmod rewrite** : Active le module `mod_rewrite` d'Apache, souvent utilisé dans les applications PHP.
- **ENV** : Définit des variables d'environnement (CONTAINER_PATH, LIBRARY_URL, BACKEND_URL) pour la configuration du projet.
- **EXPOSE 80** : Expose le port 80 pour accéder à l'application.
- **CMD ["apache2-foreground"]** : Commande de démarrage d'Apache.

## 4. **Mise en place des jobs de déploiement continu avec GitLab CI/CD**

Nous allons ajouter les jobs suivants dans le pipeline CI/CD :
1. **Build Docker Image** : Construction de l'image Docker à partir du Dockerfile.
2. **Check Dockerfile with Hadolint** : Vérification des bonnes pratiques du Dockerfile.
3. **Deploy Docker Image** : Déploiement de l'image Docker sur le serveur.

## 5. **Fichier `.gitlab-ci.yml` avec CD**

Voici le fichier `.gitlab-ci.yml` complété pour intégrer le déploiement continu (CD) :

```yaml
stages:
  - build   # Étape de build
  - test    # Étape de tests
  - lint    # Étape de vérification des Dockerfiles avec Hadolint
  - deploy  # Étape de déploiement

# Job de Build : Construction de l'application PHP avec Docker
build-job:
  stage: build
  image: composer:latest
  script:
    - echo "📦 Début de la phase de build !"
    - composer install
  cache:
    paths:
      - vendor/
  artifacts:
    paths:
      - vendor/
  only:
    - merge_requests

# Job de Test : Exécution des tests unitaires
test-job:
  stage: test
  image: php:8.0-cli
  before_script:
    - apt-get update && apt-get install -y curl git unzip
    - curl -sS https://getcomposer.org/installer | php
    - mv composer.phar /usr/local/bin/composer
  script:
    - echo "🧪 Début de la phase de tests !"
    - composer require phpunit/phpunit --dev
    - php ./vendor/bin/phpunit --filter "/(testCallWebService)( .*)?$/" ./common/Tests/WS_UtilTest.php
    - php ./vendor/bin/phpunit --filter "/(testAllKeysToUppercase)( .*)?$/" ./common/Tests/WS_UtilTest.php
  dependencies:
    - build-job
  only:
    - merge_requests

# Job Lint : Vérification des bonnes pratiques du Dockerfile avec Hadolint
lint-dockerfile:
  stage: lint
  image: hadolint/hadolint:latest
  script:
    - echo "🔍 Vérification des bonnes pratiques dans Dockerfile"
    - hadolint Dockerfile    # Vérifie le Dockerfile avec Hadolint
  only:
    - merge_requests
    - main

# Job de Déploiement : Construction et déploiement de l'image Docker
deploy-job:
  stage: deploy
  image: docker:latest
  services:
    - docker:dind
  before_script:
    - docker login -u "$CI_REGISTRY_USER" -p "$CI_REGISTRY_PASSWORD" "$CI_REGISTRY"
  script:
    - echo "🚀 Construction de l'image Docker"
    - docker build -t "$CI_REGISTRY_IMAGE:$CI_COMMIT_REF_NAME" .
    - docker push "$CI_REGISTRY_IMAGE:$CI_COMMIT_REF_NAME"    # Pousse l'image sur le registre GitLab
    - echo "🚀 Déploiement sur le serveur"
    - ssh user@your-server.com 'docker pull "$CI_REGISTRY_IMAGE:$CI_COMMIT_REF_NAME" && docker-compose up -d'
  only:
    - main                # Le déploiement ne s'exécute que sur la branche main
  environment:
    name: production       # Environnement de production
    url: https://your-production-url.com
```

## Explication des jobs :

### 1. **lint-dockerfile (Hadolint)**
- **Image** : Utilise l'image Docker officielle de **Hadolint**.
- **Script** : Exécute Hadolint pour vérifier le Dockerfile.
- **only** : Ce job s'exécute sur les **merge requests** et sur la branche **main**.

### 2. **deploy-job (Déploiement Docker)**
- **Image** : Utilise l'image Docker avec Docker-in-Docker (dind) pour construire et pousser l'image Docker.
- **before_script** : Connecte le job au registre Docker de GitLab.
- **Script** : 
  - Construit l'image Docker du projet.
  - Pousse l'image dans le registre Docker.
  - Déploie l'image sur un serveur distant via SSH et Docker Compose.

- **only** : Ce job s'exécute uniquement sur la branche **main**.
- **environment** : Définit le déploiement sur l'environnement de **production** avec une URL accessible publiquement.

---

[...retour sommaire sur CI/CD](../menu.md)
