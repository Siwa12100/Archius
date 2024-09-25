# Documentation GitLab CI : Base d'une CI

[...retour sommaire sur CI/CD](../menu.md)

---

## Qu'est-ce que la CI (Continuous Integration) ?

La **CI** ou **Continuous Integration** est un processus automatisé dans lequel le code ajouté à un projet est régulièrement testé et intégré. L’objectif principal de la CI est de s'assurer que chaque modification du code est valide et ne casse pas l'application.

### Objectifs de la CI :
- Automatisation de la construction (build) et des tests.
- Détection rapide des bugs grâce à des tests réguliers.
- Facilitation du déploiement.

## Fonctionnement d'une CI avec GitLab

GitLab propose une fonctionnalité native de **GitLab CI/CD** pour automatiser ces tâches via des **pipelines**. Un pipeline est une série de **jobs** organisés dans des **stages** (étapes) qui exécutent les tâches définies dans un fichier `.gitlab-ci.yml`.

### Principales étapes d'une CI :
1. **Build** : Construction du projet (génération d’un binaire, empaquetage du code, etc.).
2. **Test** : Exécution des tests unitaires, d'intégration ou fonctionnels.
3. **Deploy** : Déploiement du projet dans un environnement donné (staging, production).

## Création du fichier `.gitlab-ci.yml`

Le fichier **`.gitlab-ci.yml`** contient la configuration des pipelines GitLab CI/CD. Il se situe à la racine du dépôt GitLab et spécifie les jobs à exécuter, les étapes du pipeline, les variables, etc.

### Structure de base du `.gitlab-ci.yml`

```yaml
stages:        # Déclaration des étapes principales du pipeline
  - build      # Étape de construction du projet
  - test       # Étape de tests
  - deploy     # Étape de déploiement

build-job:     # Job pour la construction
  stage: build
  script:
    - echo "Building the project..."
    - mvn clean package

test-job:      # Job pour les tests
  stage: test
  script:
    - echo "Running tests..."
    - mvn test

deploy-job:    # Job pour le déploiement
  stage: deploy
  script:
    - echo "Deploying the project..."
    - scp target/*.jar user@server:/path/to/deploy
```

### Explication des éléments :
- **`stages`** : Définit les étapes dans l'ordre où elles doivent s'exécuter.
- **`build-job`, `test-job`, `deploy-job`** : Déclaration des jobs à exécuter pour chaque étape.
- **`stage`** : Définit à quelle étape appartient chaque job.
- **`script`** : Contient les commandes à exécuter dans chaque job (ex. compilation, tests, déploiement).

## Syntaxe détaillée et options du fichier `.gitlab-ci.yml`

### 1. **Stages**
Les stages permettent d’organiser les jobs en étapes logiques. GitLab exécute les jobs d’une étape uniquement si ceux des étapes précédentes ont réussi.

```yaml
stages:
  - build
  - test
  - deploy
```

### 2. **Jobs**
Un job correspond à une tâche spécifique dans un pipeline. Chaque job peut exécuter un ou plusieurs scripts. Les jobs dans un même stage s'exécutent en parallèle, tandis que les stages eux-mêmes s'exécutent séquentiellement.

Exemple de job :
```yaml
build-job:
  stage: build
  script:
    - echo "Building the project"
    - mvn clean install
```

### 3. **Variables d'environnement**
Les variables d’environnement permettent de stocker des informations nécessaires à la construction, aux tests ou au déploiement.

Exemple :
```yaml
variables:
  DATABASE_URL: "mysql://user:password@localhost/db"
```

### 4. **Artifacts et Cache**
- **Artifacts** : Les fichiers générés par un job et stockés pour être utilisés par d'autres jobs (ex. les résultats de build ou de tests).
- **Cache** : Stocke des fichiers communs entre jobs pour éviter de les télécharger ou générer à nouveau.

Exemple d’utilisation des artifacts et du cache :
```yaml
build-job:
  stage: build
  script:
    - mvn clean package
  artifacts:
    paths:
      - target/*.jar
  cache:
    paths:
      - .m2/repository/
```

### 5. **Conditions (rules, only/except)**
Les jobs peuvent être exécutés en fonction de certaines conditions (par exemple, uniquement sur la branche `main` ou lors de la création d'un tag).

Exemple avec `rules` :
```yaml
deploy-job:
  stage: deploy
  script:
    - echo "Deploying to production"
  rules:
    - if: '$CI_COMMIT_BRANCH == "main"'
```

### 6. **Déclenchement manuel ou automatique des jobs**
Certains jobs, comme ceux de déploiement, peuvent être déclenchés manuellement.

Exemple de job manuel :
```yaml
deploy-job:
  stage: deploy
  script:
    - echo "Deploying to production"
  when: manual
```

## Runners : exécution des jobs

Les jobs sont exécutés par des **runners**, qui sont des machines ou des conteneurs capables d'exécuter des scripts. Il existe deux types de runners :
- **Shared runners** : Fournis par GitLab et disponibles pour tous les projets.
- **Specific runners** : Installés sur vos propres serveurs pour exécuter des jobs dans un environnement contrôlé.

## Exemples concrets

### Exemple 1 : Pipeline CI basique pour un projet Java avec Maven

```yaml
stages:
  - build
  - test
  - deploy

variables:
  MAVEN_OPTS: "-Dmaven.test.failure.ignore=true"

build-job:
  stage: build
  script:
    - mvn clean install
  artifacts:
    paths:
      - target/*.jar

test-job:
  stage: test
  script:
    - mvn test

deploy-job:
  stage: deploy
  script:
    - echo "Deploying to production server..."
    - scp target/*.jar user@production-server:/path/to/deploy
  only:
    - main
```

### Exemple 2 : Pipeline CI avec Docker

Cet exemple montre comment construire une image Docker et la déployer sur un registre privé.

```yaml
stages:
  - build
  - test
  - deploy

build-job:
  stage: build
  script:
    - docker build -t myapp:latest .

test-job:
  stage: test
  script:
    - echo "Running tests..."
    - docker run myapp:latest npm test

deploy-job:
  stage: deploy
  script:
    - echo "Pushing Docker image to registry..."
    - docker login -u "$CI_REGISTRY_USER" -p "$CI_REGISTRY_PASSWORD" $CI_REGISTRY
    - docker push $CI_REGISTRY/myapp:latest
  only:
    - main
```

## Cas d'usage communs

1. **Tests automatisés à chaque commit** : Lorsqu'un développeur pousse du code, des tests sont automatiquement exécutés.
2. **Déploiement en production** : Les modifications sur la branche `main` peuvent déclencher automatiquement le déploiement en production après la réussite des tests.
3. **Vérification du code sur plusieurs branches** : Les jobs peuvent être configurés pour s’exécuter uniquement sur certaines branches ou environnements.

---

[...retour sommaire sur CI/CD](../menu.md)