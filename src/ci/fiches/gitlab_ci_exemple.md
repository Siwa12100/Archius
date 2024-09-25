# Gitlab CI : Exemple concret sur projet PHP

[...retour sommaire sur CI/CD](../menu.md)

---

## Structure et fonctionnement du projet

Le projet semble être un site web PHP permettant de gérer une bibliothèque. Voici un résumé de la structure :

- **Templates/** : Contient les fichiers HTML servant de modèles pour le rendu des pages.
- **Traitement/** : Contient les fichiers PHP responsables de la logique côté serveur.
- **bfmw/** : Un sous-répertoire avec une structure similaire, contenant des fichiers HTML, PHP, CSS, et JS spécifiques à une partie du projet.
- **common/** : Contient les utilitaires généraux, les tests et les modèles.
- **js/** et **css/** : Répertoires contenant les fichiers JavaScript et CSS pour le frontend.
- **Tests** : Se trouvent dans `common/Tests`, où les tests unitaires sont écrits pour PHPUnit.

Le projet utilise **Composer** pour la gestion des dépendances, et il est configuré pour utiliser **PHPUnit** pour les tests unitaires.

## Mise en place de la CI

La mise en place de la CI repose sur **GitLab CI/CD** avec un fichier `.gitlab-ci.yml` configuré pour automatiser deux phases essentielles :
1. **Build** : Installation des dépendances avec Composer.
2. **Tests** : Exécution des tests unitaires avec PHPUnit.

### 1. Le fichier `.gitlab-ci.yml`

Ton fichier actuel est fonctionnel, mais on peut l’améliorer pour qu'il soit plus robuste et optimisé. Voici une version améliorée qui inclut l'installation des dépendances, l'exécution des tests, et quelques ajustements.

```yaml
# Définition des différentes étapes (stages) de notre pipeline CI
# Nous avons deux étapes : "build" (construction du projet) et "test" (exécution des tests)
stages:
  - build   # La première étape, pour la construction du projet
  - test    # La deuxième étape, pour l'exécution des tests unitaires

# Job de build : il installe les dépendances PHP avec Composer
build-job:
  # Le job appartient à l'étape "build"
  stage: build
  
  # L'image Docker utilisée pour ce job est "composer:latest", une image qui contient Composer préinstallé
  image: composer:latest
  
  # Le script qui sera exécuté par ce job :
  script:
    - echo "📦 Début de la phase de build !"  # Message pour signaler le début du build
    - composer install                       # Installe les dépendances PHP à partir du fichier composer.json
  
  # Configuration du cache pour le dossier vendor, qui contient les dépendances PHP
  # Cela permet de ne pas réinstaller les dépendances à chaque exécution du pipeline, ce qui accélère le processus
  cache:
    paths:
      - vendor/                              # On met en cache le dossier vendor où sont installées les dépendances
  
  # Artifacts : les fichiers générés par ce job qui peuvent être utilisés par les jobs suivants
  # Ici, nous sauvegardons les dépendances (dossier vendor) pour qu'elles puissent être réutilisées dans le job de test
  artifacts:
    paths:
      - vendor/                              # On sauvegarde le dossier vendor comme artifact
  
 
# Job de test : il exécute les tests unitaires avec PHPUnit
test-job:
  # Ce job appartient à l'étape "test"
  stage: test
  
  # L'image Docker utilisée ici est "php:8.0-cli", qui contient PHP 8.0 sans Composer
  image: php:8.0-cli
  
  # Avant de pouvoir exécuter les tests, nous avons besoin d'installer quelques outils, comme Composer
  # Cela se fait via le "before_script" qui s'exécute avant le script principal
  before_script:
    # Met à jour les paquets de l'environnement et installe curl, git et unzip, nécessaires pour installer Composer
    - apt-get update && apt-get install -y curl git unzip
    
    # Télécharge et installe Composer, qui n'est pas présent par défaut dans cette image Docker
    - curl -sS https://getcomposer.org/installer | php  # Télécharge l'installateur de Composer
    
    # Déplace le fichier composer.phar (l'exécutable de Composer) dans un répertoire accessible globalement
    - mv composer.phar /usr/local/bin/composer           # Déplace Composer pour le rendre utilisable avec la commande "composer"
  
  # Script principal qui exécute les tests unitaires
  script:
    - echo "🧪 Début de la phase de tests !"               # Message indiquant le début de l'exécution des tests
    
    # Installe PHPUnit en tant que dépendance de développement
    # Cela permet de s'assurer que nous utilisons la version correcte de PHPUnit, même si elle n'est pas dans composer.json
    - composer require phpunit/phpunit --dev
    
    # Vérifie que PHPUnit est bien installé en affichant sa version
    - php ./vendor/bin/phpunit --version
    
    # Exécute un test unitaire spécifique défini dans le fichier de test WS_UtilTest.php
    # L'option --filter permet de cibler une méthode spécifique dans le test, ici "testCallWebService"
    - php ./vendor/bin/phpunit --filter "/(testCallWebService)( .*)?$/" ./common/Tests/WS_UtilTest.php
    
    # Exécute un autre test unitaire spécifique dans le même fichier, cette fois pour la méthode "testAllKeysToUppercase"
    - php ./vendor/bin/phpunit --filter "/(testAllKeysToUppercase)( .*)?$/" ./common/Tests/WS_UtilTest.php
  
  # Le job de test dépend du job de build
  # Cela signifie qu'il utilisera les fichiers générés (les artefacts) par le job de build, notamment le dossier vendor qui contient les dépendances
  dependencies:
    - build-job                                   # Ce job dépend du job de build
```

### Explications détaillées de la CI :

#### 1. **Stages (Étapes)**
Les étapes définies dans ce fichier sont :
- **Build** : Installation des dépendances via Composer.
- **Test** : Exécution des tests unitaires avec PHPUnit.

#### 2. **Jobs (Tâches)**
Deux jobs sont définis : 
- **build-job** : Télécharge et installe les dépendances via Composer.
- **test-job** : Exécute les tests unitaires après avoir récupéré les dépendances du job `build-job`.

#### 3. **Cache**
Le dossier `vendor/`, qui contient les dépendances installées, est mis en cache pour éviter de télécharger les dépendances à chaque exécution du pipeline, ce qui accélère la CI.

#### 4. **Artifacts**
- Le job `build-job` enregistre les dépendances installées en tant qu’artefact, qui est réutilisé par le job `test-job`.
- Les résultats des tests sont également sauvegardés dans un rapport JUnit que tu pourras consulter directement dans l'interface GitLab.

#### 5. **Utilisation de PHPUnit**
- Dans le job de test, PHPUnit est installé si nécessaire.
- Ensuite, les tests spécifiques sont exécutés via l'option `--filter` pour cibler des méthodes précises.

### 2. Variables d'environnement et notifications

Si tu utilises des variables d'environnement (comme des clés d'API), tu peux les définir dans GitLab (section **Settings > CI/CD > Variables**) et les appeler dans ton fichier `.gitlab-ci.yml`.

Exemple de définition de variable dans `.gitlab-ci.yml` :
```yaml
variables:
  ENVIRONMENT: production
```

### 3. Résultats et analyse des tests
GitLab permet d'afficher directement les rapports de tests (JUnit, par exemple) pour analyser les erreurs ou succès des tests exécutés. Le fichier ci-dessus génère un fichier `report.xml` qui peut être visualisé via GitLab.

### 4. Notifications
Tu peux aussi configurer des notifications Slack ou par e-mail pour être averti de l’état du pipeline à chaque exécution. Cela peut se faire via des **Webhooks** dans GitLab.

---

[...retour sommaire sur CI/CD](../menu.md)
