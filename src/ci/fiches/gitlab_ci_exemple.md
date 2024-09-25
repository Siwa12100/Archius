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
stages:        # Les étapes principales de la CI
  - build
  - test

# Job de Build : Installation des dépendances
build-job:
  stage: build
  image: composer:latest     # Utilisation de l'image Composer pour l'installation des dépendances
  script:
    - echo "📦 Début de la phase de build !"
    - composer install        # Installation des dépendances via Composer
  cache:
    paths:                    # Cache le dossier des dépendances pour éviter de les re-télécharger
      - vendor/
  artifacts:
    paths:
      - vendor/               # Sauvegarde le dossier des dépendances en tant qu'artefact pour le réutiliser dans d'autres jobs
  only:
    - merge_requests          # N'exécute ce job que sur les branches des pull/merge requests

# Job de Test : Exécution des tests unitaires
test-job:
  stage: test
  image: php:8.0-cli          # Utilisation de l'image PHP 8 pour exécuter les tests
  before_script:              # Installe PHPUnit uniquement si non présent
    - if [ ! -f vendor/bin/phpunit ]; then composer require phpunit/phpunit --dev; fi
  script:
    - echo "🧪 Début de la phase de tests !"
    - vendor/bin/phpunit --version      # Vérifie la version de PHPUnit
    - vendor/bin/phpunit --filter "/(testCallWebService)( .*)?$/" ./common/Tests/WS_UtilTest.php
    - vendor/bin/phpunit --filter "/(testAllKeysToUppercase)( .*)?$/" ./common/Tests/WS_UtilTest.php
  dependencies:
    - build-job               # Utilise les artefacts du job de build (le dossier `vendor/`)
  artifacts:
    when: always               # Enregistre les résultats des tests
    paths:
      - tests/_output/         # Personnalise l'emplacement des résultats
    reports:
      junit: report.xml        # Génère un rapport au format JUnit pour les résultats des tests
  only:
    - merge_requests           # Exécute ce job uniquement lors des pull/merge requests

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

#### 6. **Execution conditionnelle (`only`)**
Les jobs sont configurés pour ne s'exécuter que lors des **merge requests**, ce qui est utile pour limiter l’exécution de la CI aux moments où les contributions doivent être validées avant d'être intégrées dans la branche principale.

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
