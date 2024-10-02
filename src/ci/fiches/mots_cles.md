# Guide Pro sur GitLab CI/CD

[...retour menu sur ci /cd](../menu.md)

---

## Introduction à GitLab CI/CD

GitLab CI/CD permet d'automatiser les processus d'intégration continue (CI) et de déploiement continu (CD). Un pipeline CI/CD définit un ensemble d'étapes ou **jobs** à exécuter pour tester, valider et déployer un projet.

### Fichier `.gitlab-ci.yml`

Le fichier `.gitlab-ci.yml` est utilisé pour définir un pipeline GitLab. Il contient la définition de **jobs**, **stages** (étapes), et la configuration des images, scripts, et autres éléments de votre pipeline.

## Concepts et Syntaxe Essentiels

### 1. `stages`
#### Définition :
`stages` définit les différentes étapes par lesquelles passent vos jobs dans un pipeline CI/CD.

#### Utilité :
Les **stages** permettent d'organiser les jobs en différentes phases, par exemple : **build**, **test**, et **deploy**. Chaque job est assigné à un stage et les jobs d'un même stage s'exécutent en parallèle. Les stages s'exécutent séquentiellement.

#### Syntaxe :
```yaml
stages:
  - build
  - test
  - deploy
```

#### Exemple :
```yaml
stages:
  - compile
  - test
  - deploy
```
Ici, tous les jobs dans le stage **compile** seront exécutés en parallèle. Ensuite, les jobs du stage **test** s'exécuteront une fois que le stage **compile** est terminé, et ainsi de suite.

### 2. `stage`
#### Définition :
`stage` attribue un job à un stage particulier.

#### Utilité :
Cela permet de placer des jobs dans les étapes définies par la directive `stages`. Si un job ne spécifie pas de `stage`, il sera attribué au stage par défaut.

#### Syntaxe :
```yaml
job_name:
  stage: test
  script:
    - echo "Running tests"
```

#### Exemple :
```yaml
compile-job:
  stage: compile
  script:
    - gcc main.c -o main
```

### 3. `script`
#### Définition :
La clé `script` contient les commandes à exécuter dans le job.

#### Utilité :
Les commandes spécifiées dans `script` définissent ce que le job doit accomplir (tests, compilation, déploiement, etc.).

#### Syntaxe :
```yaml
job_name:
  script:
    - echo "This is my script"
```

#### Exemple :
```yaml
build-job:
  stage: build
  script:
    - make
    - make test
```

### 4. `artifacts`
#### Définition :
`artifacts` spécifie les fichiers ou répertoires qui doivent être sauvegardés après l'exécution d'un job pour être utilisés dans les jobs suivants ou téléchargeables via l'interface GitLab.

#### Utilité :
Cela permet de persister des fichiers générés pendant un job pour une utilisation ultérieure.

#### Syntaxe :
```yaml
job_name:
  artifacts:
    paths:
      - build/
```

#### Exemple :
```yaml
build-job:
  stage: build
  script:
    - make
  artifacts:
    paths:
      - build/
```

### 5. `paths`
#### Définition :
`paths` est utilisé dans la configuration des `artifacts` pour spécifier quels fichiers ou répertoires doivent être stockés comme artifacts.

#### Syntaxe :
```yaml
artifacts:
  paths:
    - my-folder/
```

#### Exemple :
```yaml
test-job:
  stage: test
  script:
    - ./run_tests.sh
  artifacts:
    paths:
      - test-results/
```

### 6. `rules`
#### Définition :
`rules` permet de définir les conditions dans lesquelles un job doit être exécuté ou non.

#### Utilité :
Cela permet de personnaliser l'exécution des jobs en fonction de branches, de tags, ou d'autres variables.

#### Syntaxe :
```yaml
job_name:
  rules:
    - if: '$CI_COMMIT_REF_NAME == "main"'
```

#### Exemple :
```yaml
deploy-job:
  stage: deploy
  script:
    - ./deploy.sh
  rules:
    - if: '$CI_COMMIT_REF_NAME == "main"'
    - when: manual
```

### 7. `when`
#### Définition :
`when` spécifie quand un job doit être exécuté. Il peut être défini comme `on_success` (par défaut), `on_failure`, ou `manual`.

#### Utilité :
Cela permet de contrôler si un job doit s'exécuter après le succès ou l'échec d'un autre, ou si l'exécution doit être manuelle.

#### Syntaxe :
```yaml
job_name:
  when: manual
```

#### Exemple :
```yaml
deploy-job:
  stage: deploy
  script:
    - ./deploy.sh
  when: manual
```

### 8. `image`
#### Définition :
`image` définit l'image Docker utilisée pour exécuter le job.

#### Utilité :
Chaque job s'exécute dans un **job container** qui est basé sur une image Docker. Utiliser des images spécifiques permet de s'assurer que le job dispose de l'environnement nécessaire.

#### Syntaxe :
```yaml
job_name:
  image: node:14
```

#### Exemple :
```yaml
build-job:
  stage: build
  image: maven:3.6.3-jdk-8
  script:
    - mvn install
```

### 9. `needs`
#### Définition :
`needs` permet de spécifier les jobs dont celui-ci dépend. Cela permet d'exécuter des jobs en parallèle tout en respectant les dépendances.

#### Syntaxe :
```yaml
job_name:
  needs:
    - compile-job
```

#### Exemple :
```yaml
test-job:
  stage: test
  needs:
    - build-job
  script:
    - ./run_tests.sh
```

### 10. `job`
#### Définition :
Un **job** est une unité de travail dans GitLab CI/CD. Chaque job peut contenir un script, une image, des dépendances et d'autres configurations.

#### Syntaxe :
```yaml
job_name:
  script:
    - echo "This is a job"
```

#### Exemple :
```yaml
compile-job:
  stage: compile
  script:
    - gcc main.c -o main
```

### 11. `allow_failure`
#### Définition :
`allow_failure` permet de continuer l'exécution du pipeline même si ce job échoue.

#### Utilité :
Cela est utile lorsque certaines parties du pipeline ne sont pas critiques et ne doivent pas bloquer les autres étapes.

#### Syntaxe :
```yaml
job_name:
  allow_failure: true
```

#### Exemple :
```yaml
lint-job:
  stage: test
  script:
    - npm run lint
  allow_failure: true
```

### 12. `variables`
#### Définition :
`variables` permet de définir des variables d'environnement à utiliser dans les jobs.

#### Syntaxe :
```yaml
variables:
  MY_VAR: "some value"
```

#### Exemple :
```yaml
deploy-job:
  stage: deploy
  script:
    - echo $MY_VAR
  variables:
    MY_VAR: "production"
```

### 13. `include`
#### Définition :
`include` permet d'importer des fichiers YAML externes dans votre pipeline.

#### Utilité :
Cela permet de réutiliser des configurations communes provenant d'autres projets ou fichiers.

#### Syntaxe :
```yaml
include:
  - local: '/path/to/another.yml'
```

#### Exemple :
```yaml
include:
  - project: 'iut/ci/ci-gen'
    ref: 'main'
    file:
      - 'deploy/.docker-deploy.yml'
```

---

[...retour menu sur ci /cd](../menu.md)