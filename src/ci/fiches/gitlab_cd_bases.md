# Introduction à la CD Gitlab (Continuous Deployment)

[...retour sommaire sur CI/CD](../menu.md)

---

La **CD (Continuous Deployment)** est une extension de la CI (Continuous Integration), où le déploiement du code en production ou en environnement cible (comme staging) est automatisé. Avec la CD, une fois que les tests CI passent avec succès, le pipeline déclenche automatiquement le déploiement sans intervention manuelle. Cela permet de rendre le processus de mise en production plus rapide, répétable, et moins sujet aux erreurs humaines.

GitLab propose une implémentation native de la CD via son système CI/CD. Le déploiement continu est intégré dans le fichier `.gitlab-ci.yml`, qui contient à la fois les jobs de CI (build, test) et de CD (deploy).

## Différence entre **Continuous Deployment** et **Continuous Delivery**

- **Continuous Deployment** : Chaque modification du code qui passe les tests est **automatiquement déployée** en production sans intervention humaine.
- **Continuous Delivery** : Le code est automatiquement préparé pour un déploiement, mais une **intervention manuelle** est nécessaire pour approuver le déploiement final en production.

## Étapes générales d'un pipeline CD

1. **Build** : Construction de l'application, génération de fichiers exécutables ou artefacts.
2. **Test** : Exécution des tests unitaires et d'intégration pour valider la stabilité et la qualité du code.
3. **Linting** : Vérification des bonnes pratiques dans les fichiers de configuration (comme les Dockerfiles).
4. **Deploy** : Déploiement de l'application vers l'environnement de production ou staging.

## Docker et Continuous Deployment

Pour les projets modernes, **Docker** est souvent utilisé pour containeriser les applications et assurer un déploiement cohérent entre les environnements. Un **Dockerfile** est utilisé pour définir l'image Docker d'une application, qui inclut l'environnement d'exécution (par exemple, PHP 8), les dépendances et les configurations.

## Dockerfile
Le fichier **Dockerfile** définit l'environnement dans lequel ton application va tourner. Il doit suivre les bonnes pratiques pour assurer un environnement sécurisé et efficace. Un outil comme **Hadolint** est utilisé pour s'assurer que ces bonnes pratiques sont respectées.

**Exemple de Dockerfile pour une application PHP** :
```Dockerfile
# Utilise PHP 8 avec Apache
FROM php:8.0-apache

# Installe les extensions nécessaires
RUN docker-php-ext-install pdo pdo_mysql

# Définit le répertoire de travail
WORKDIR /var/www/html

# Copie le contenu de l'application dans le conteneur
COPY . /var/www/html

# Expose le port 80 pour l'accès web
EXPOSE 80

# Définit le point d'entrée (démarrage d'Apache)
CMD ["apache2-foreground"]
```

## Vérification des bonnes pratiques avec **Hadolint**

**Hadolint** est un outil open source pour vérifier les bonnes pratiques dans les Dockerfiles. Il peut être intégré dans GitLab CI/CD pour automatiser cette vérification. Hadolint analyse le fichier Dockerfile pour s'assurer qu'il respecte les normes de sécurité, de performance, et de maintenance.

## Mise en place de GitLab CD

GitLab CD est configuré à l'aide du fichier `.gitlab-ci.yml`. Pour la CD, nous ajoutons généralement une ou plusieurs étapes de déploiement après les étapes de build et de test. Le déploiement peut se faire via des scripts personnalisés, des outils comme Docker, ou même sur Kubernetes si l'application est cloud-native.


## Exemple de pipeline GitLab CI/CD avec Docker et Hadolint

Voici un exemple complet d'un fichier `.gitlab-ci.yml` pour un pipeline CI/CD, incluant la construction, les tests, le linting avec Hadolint, et le déploiement continu d'une application PHP dans un conteneur Docker :

```yaml
stages:
  - build    # Étape de construction
  - test     # Étape de tests unitaires
  - lint     # Étape de vérification des Dockerfiles avec Hadolint
  - deploy   # Étape de déploiement continu

# Job de Build : Construction de l'application PHP
build-job:
  stage: build
  image: composer:latest
  script:
    - echo "📦 Début de la phase de build"
    - composer install           # Installe les dépendances PHP via Composer
  cache:
    paths:
      - vendor/                  # Mise en cache du dossier des dépendances
  artifacts:
    paths:
      - vendor/                  # Les dépendances sont sauvegardées en tant qu'artefacts pour le job suivant
  only:
    - merge_requests             # Exécution du build uniquement pour les merge requests

# Job de Test : Exécution des tests unitaires PHP
test-job:
  stage: test
  image: php:8.0-cli
  before_script:
    - apt-get update && apt-get install -y curl git unzip
    - curl -sS https://getcomposer.org/installer | php
    - mv composer.phar /usr/local/bin/composer
  script:
    - echo "🧪 Début de la phase de tests"
    - composer require phpunit/phpunit --dev   # Installation de PHPUnit pour exécuter les tests unitaires
    - php ./vendor/bin/phpunit --version
    - php ./vendor/bin/phpunit --filter "/(testExample)( .*)?$/" ./tests/ExampleTest.php
  dependencies:
    - build-job                                # Les tests dépendent du build
  only:
    - merge_requests

# Job de Lint : Vérification des bonnes pratiques dans Dockerfile avec Hadolint
lint-dockerfile:
  stage: lint
  image: hadolint/hadolint:latest              # Utilise l'image Hadolint pour vérifier le Dockerfile
  script:
    - echo "🔍 Vérification des bonnes pratiques dans Dockerfile"
    - hadolint Dockerfile                      # Exécute Hadolint pour vérifier le fichier Dockerfile
  only:
    - merge_requests                           # Ce job s'exécute pour les merge requests et sur la branche main
    - main

# Job de Déploiement : Déploiement continu avec Docker
deploy-job:
  stage: deploy
  image: docker:latest                         # Utilise Docker pour construire et déployer l'image
  services:
    - docker:dind                              # Active Docker-in-Docker (dind) pour permettre la construction et le push d'images Docker
  before_script:
    - docker login -u "$CI_REGISTRY_USER" -p "$CI_REGISTRY_PASSWORD" "$CI_REGISTRY"
  script:
    - echo "🚀 Construction de l'image Docker"
    - docker build -t "$CI_REGISTRY_IMAGE:$CI_COMMIT_REF_NAME" .   # Construit l'image Docker
    - docker push "$CI_REGISTRY_IMAGE:$CI_COMMIT_REF_NAME"         # Pousse l'image dans le registre Docker de GitLab
    - echo "🚀 Déploiement sur le serveur"
    - ssh user@your-server.com 'docker pull "$CI_REGISTRY_IMAGE:$CI_COMMIT_REF_NAME" && docker-compose up -d'  # Déploie l'image sur le serveur
  only:
    - main                                                        # Déploiement uniquement sur la branche main
  environment:
    name: production                                               # Définit l'environnement de production
    url: https://your-production-site.com                          # URL de l'environnement de production
```

### Explications détaillées des nouvelles sections :

#### **Lint Dockerfile avec Hadolint**
- **Image Hadolint** : Utilise l'image Docker officielle de Hadolint pour analyser et vérifier les bonnes pratiques dans le Dockerfile.
- **Script** : `hadolint Dockerfile` vérifie que le Dockerfile respecte les règles de sécurité, de performance et de maintenabilité. En cas de non-respect des normes, le job échouera et affichera les erreurs à corriger.

#### **Docker-in-Docker (dind)**
- **services: docker:dind** : Active Docker-in-Docker (dind) pour permettre de construire et pousser des images Docker dans GitLab. Cela permet d'utiliser Docker à l'intérieur même du conteneur exécutant le pipeline.

#### **Push Docker Image et Déploiement**
- **docker build** : Construit une nouvelle image Docker à partir du Dockerfile dans le projet.
- **docker push** : Pousse cette image dans le registre Docker de GitLab (accessible via `CI_REGISTRY`).
- **ssh et docker-compose** : Utilise SSH pour se connecter à un serveur distant et y déployer l'image Docker en production en exécutant des commandes `docker-compose` pour lancer les services.

---

## Détails supplémentaires sur le Continuous Deployment :

### 1. **Environnements GitLab**
GitLab permet de définir différents **environnements** dans le pipeline CI/CD, comme **staging** ou **production**. Cela aide à structurer les étapes de déploiement et à gérer plusieurs environnements simultanément.

### 2. **Variables d'environnement sensibles**
Pour gérer des informations sensibles comme des clés SSH ou des tokens d'authentification Docker, GitLab te permet de définir des **variables secrètes** dans la configuration CI/CD, accessibles via `$CI_REGISTRY_USER` et `$CI_REGISTRY_PASSWORD`.

### 3. **Hadolint et Dockerfiles optimisés**
L'utilisation de **Hadolint** dans le pipeline CI/CD permet de garantir que les **Dockerfiles** sont bien écrits et optimisés pour éviter des problèmes comme la taille d'image excessive, les vulnérabilités de sécurité, ou des configurations inefficaces.

---

[...retour sommaire sur CI/CD](../menu.md)
