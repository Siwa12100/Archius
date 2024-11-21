# Configuration Complète de SonarQube pour un Projet Vue.js avec Jest

[Menu Vue.js](../menu.md)
[Menu CI](../../ci/menu.md)

---

Cette documentation explique comment configurer SonarQube pour analyser un projet Vue.js, collecter des métriques de qualité de code et des rapports de couverture générés par Jest. Ce guide est générique et s'applique à tout projet Vue.js configuré avec Jest.

---

## **1. Objectif**

- **Configurer SonarQube pour un projet Vue.js** avec TypeScript et Jest.
- **Analyser la qualité du code**, les duplications, les bugs, et les vulnérabilités.
- **Inclure la couverture des tests unitaires** dans les métriques.

---

## **2. Installation des Outils**

### **Prérequis**
1. Un projet Vue.js configuré avec Jest et TypeScript.
2. Un serveur SonarQube fonctionnel avec un projet configuré.
3. Un token SonarQube pour l’authentification.

### **Installation des Dépendances**
Ajoutez les outils nécessaires au projet :

```bash
# Installez Jest et les dépendances nécessaires si ce n’est pas encore fait
npm install --save-dev jest ts-jest @vue/test-utils vue-jest

# Installez un scanner Sonar en local (optionnel, pour des tests locaux)
npm install --save-dev sonarqube-scanner
```

---

## **3. Configuration de SonarQube**

### **3.1. Fichier `sonar-project.properties`**

Créez un fichier nommé `sonar-project.properties` à la racine de votre projet pour définir les paramètres d’analyse de SonarQube.

Voici un exemple adapté à un projet Vue.js avec TypeScript et Jest :

```properties
# Identification du projet
sonar.projectKey=vue_project_key               # Remplacez par l'identifiant unique de votre projet
sonar.organization=your_organization_name      # Si applicable, ajoutez votre organisation (SonarCloud)
sonar.host.url=https://sonarqube.example.com   # URL de votre serveur SonarQube
sonar.login=${SONAR_TOKEN}                     # Utilisation d'un token sécurisé passé en CI/CD

# Répertoires du projet
sonar.sources=src                              # Répertoire contenant les fichiers source
sonar.exclusions=src/**/*.spec.ts,src/**/*.test.ts,node_modules/**,dist/**  # Fichiers exclus de l’analyse

# Tests et couverture
sonar.tests=src                                # Répertoire contenant les fichiers de tests
sonar.test.inclusions=src/**/*.spec.ts         # Inclusions des fichiers de test
sonar.javascript.lcov.reportPaths=coverage/lcov.info  # Rapport de couverture généré par Jest

# Qualité du code
sonar.coverage.exclusions=src/**/*.vue         # Exclure les fichiers Vue des métriques de couverture

# TypeScript et configuration spécifique
sonar.typescript.tsconfigPath=tsconfig.json    # Chemin vers le fichier tsconfig.json

# Performances
sonar.javascript.node.maxspace=4096           # Ajout de mémoire pour analyser de grands projets
```

**Points importants :**
1. **sonar.projectKey** : Identifiant unique de votre projet, à configurer sur le serveur SonarQube.
2. **sonar.login** : Utilisation d’un token sécurisé (ne jamais inclure directement dans le fichier).
3. **sonar.javascript.lcov.reportPaths** : Indique où SonarQube trouvera le rapport de couverture généré par Jest.

---

## **4. Intégration dans la CI/CD**

### **Pipeline Générique CI/CD (Drone)**

Voici une configuration YAML générique pour exécuter les tests Jest, collecter la couverture et analyser le projet avec SonarQube.

```yaml
# Étape 1 : Installation des dépendances
- name: install-dependencies
  image: node:18
  commands:
    - echo "Installing dependencies..."
    - npm install

# Étape 2 : Exécution des tests et génération de la couverture
- name: run-tests
  image: node:18
  volumes:
    - name: coverage-volume
      path: /project/coverage
  commands:
    - echo "Running Jest tests..."
    - npx jest --coverage
    - echo "Coverage report contents:"
    - ls -l coverage

# Étape 3 : Analyse de SonarQube
- name: sonar-analysis
  image: sonarsource/sonar-scanner-cli:latest
  environment:
    SONAR_TOKEN:
      from_secret: sonar_token
  volumes:
    - name: coverage-volume
      path: /project/coverage
  commands:
    - echo "Running SonarScanner..."
    - sonar-scanner
  depends_on:
    - run-tests
```

### **Explication des Étapes**

1. **Installation des dépendances :**
   - Cette étape installe les dépendances nécessaires au projet.
   - Utilise une image Node.js (`node:18`) compatible avec Vue.js et Jest.

2. **Exécution des tests Jest :**
   - Les tests sont exécutés avec `jest --coverage`, générant un rapport de couverture dans le dossier `coverage`.
   - Le volume partagé (`coverage-volume`) garantit que les données sont accessibles pour l’étape suivante.

3. **Analyse SonarQube :**
   - Utilise l’image officielle de SonarScanner pour exécuter l’analyse.
   - Les variables d’environnement, comme le token, sont passées de manière sécurisée.

---

## **5. Rapports de Couverture avec Jest**

### **Génération du Rapport**
Jest génère automatiquement un rapport de couverture avec l’option `--coverage`. Voici comment l’activer :

1. **Commandes Jest :**
   ```bash
   npx jest --coverage
   ```
   Les fichiers de couverture seront générés dans le répertoire `coverage`.

2. **Structure du Rapport :**
   ```
   coverage/
   ├── lcov-report/
   │   ├── index.html   # Rapport HTML consultable dans un navigateur
   ├── lcov.info        # Rapport utilisé par SonarQube
   ```

---

## **6. Analyse avec SonarQube**

### **Exécution en Local**
Avant d’exécuter le pipeline CI, vous pouvez tester localement l’analyse SonarQube avec le scanner CLI :

```bash
sonar-scanner \
  -Dsonar.projectKey=vue_project_key \
  -Dsonar.sources=src \
  -Dsonar.tests=src \
  -Dsonar.javascript.lcov.reportPaths=coverage/lcov.info \
  -Dsonar.host.url=https://sonarqube.example.com \
  -Dsonar.login=YOUR_SONAR_TOKEN
```

### **Résultats Attendus**
Une fois l’analyse terminée, vous verrez dans SonarQube :
- **Couverture des tests unitaires** (basée sur `lcov.info`).
- **Bugs, vulnérabilités, et code smells.**
- **Duplications et métriques de maintenabilité.**

---

## **7. Dépannage et Bonnes Pratiques**

### **Problèmes Courants**
1. **Couverture non détectée :**
   - Assurez-vous que `coverage/lcov.info` est généré dans le bon répertoire.
   - Vérifiez que `sonar.javascript.lcov.reportPaths` pointe correctement vers ce fichier.

2. **Erreur `sonar.login` :**
   - Si vous utilisez un pipeline CI, passez le token via une variable d’environnement sécurisée.

3. **Dossier `node_modules` analysé par erreur :**
   - Ajoutez `node_modules/**` à `sonar.exclusions` dans `sonar-project.properties`.

### **Bonnes Pratiques**
- **Ne jamais inclure le token directement dans les fichiers de configuration.**
- **Configurez des seuils de qualité dans SonarQube :** Refusez les builds si la couverture descend sous un certain seuil.
- **Vérifiez localement avant d’exécuter en CI/CD :**
  ```bash
  npm run test -- --coverage
  npx sonar-scanner
  ```

---

[Menu Vue.js](../menu.md)
[Menu CI](../../ci/menu.md)