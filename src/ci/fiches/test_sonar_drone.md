# Configurer Sonar pour un projet et l'appliquer dans une CI Drone - Généralités

[Menu Ci](../menu.md)

---

## Exemple concret avec Vue.js, jest et Drone

[A retrouver ici](../../vue2/notes/test_sonar_drone.md)

---

## **1. Configurer SonarQube pour le projet**
### **Fichier de configuration `sonar-project.properties`**
1. Créez un fichier `sonar-project.properties` à la racine du projet.
2. Configurez les paramètres de base :
   - **Clé et nom du projet :**
     ```properties
     sonar.projectKey=project-key
     sonar.projectName=Project Name
     sonar.projectVersion=1.0
     ```
   - **Sources du projet :**
     ```properties
     sonar.sources=src
     sonar.exclusions=**/node_modules/**,**/tests/**,**/coverage/**
     ```
   - **Fichiers de tests et couverture (si applicable) :**
     - Pour JavaScript/TypeScript :
       ```properties
       sonar.javascript.lcov.reportPaths=coverage/lcov.info
       ```
     - Pour Java (JaCoCo) :
       ```properties
       sonar.coverage.jacoco.xmlReportPaths=target/site/jacoco/jacoco.xml
       ```
     - Adaptez pour d'autres langages en suivant la [documentation officielle de SonarQube](https://docs.sonarqube.org/latest/analysis/coverage/).

3. Ajoutez l’URL et le token SonarQube dans la configuration :
   ```properties
   sonar.host.url=http://<sonar-server-url>
   sonar.login=${SONAR_TOKEN}
   ```

---

## **2. Configurer la CI Drone**

### **Étape 1 : Lancer les tests**
1. Exécutez les tests dans une étape dédiée.
2. Assurez-vous que les rapports de couverture sont générés et accessibles (e.g., via des fichiers comme `lcov.info`, `jacoco.xml`, etc.).
3. Utilisez un volume partagé pour stocker les rapports afin qu’ils soient accessibles à d’autres étapes.

#### **Exemple Drone (tests JavaScript avec Jest) :**
```yaml
- name: run-tests
  image: node:18
  volumes:
    - name: shared-volume
      path: /reports
  commands:
    - npm install
    - npm test -- --coverage
    - cp -r coverage/lcov.info /reports/lcov.info
```

#### **Exemple Drone (tests Java avec Maven et JaCoCo) :**
```yaml
- name: run-tests
  image: maven:3.8.6-jdk-11
  volumes:
    - name: shared-volume
      path: /reports
  commands:
    - mvn clean test
    - cp target/site/jacoco/jacoco.xml /reports/jacoco.xml
```

---

### **Étape 2 : Scanner avec SonarQube**
1. Utilisez l’image Docker officielle `sonarsource/sonar-scanner-cli`.
2. Montez le volume partagé contenant les rapports générés dans l’étape précédente.
3. Exécutez `sonar-scanner` en s’assurant que les chemins vers les rapports sont configurés dans `sonar-project.properties`.

#### **Exemple Drone :**
```yaml
- name: sonar_scan
  image: sonarsource/sonar-scanner-cli:latest
  environment:
    SONAR_TOKEN:
      from_secret: sonar_token
  volumes:
    - name: shared-volume
      path: /reports
  commands:
    - sonar-scanner
```

---

## **3. Déclaration du volume partagé**
Dans Drone, utilisez un volume partagé temporaire pour transmettre les fichiers entre les étapes :

```yaml
volumes:
  - name: shared-volume
    temp: {}
```

---

## **4. Points de vigilance**

### **Assurez-vous que Sonar voit les rapports de couverture :**
1. **Chemin correct dans `sonar-project.properties` :**
   - Vérifiez que le chemin des rapports (e.g., `coverage/lcov.info`) correspond à l’endroit où le fichier est généré et stocké dans le volume partagé.
2. **Compatibilité des formats de rapport :**
   - Le fichier de couverture doit être dans un format pris en charge par SonarQube (e.g., LCOV, JaCoCo, Cobertura, etc.).

### **Ajoutez des dépendances entre étapes :**
- Assurez-vous que l’étape `sonar_scan` dépend de l’étape `run-tests` pour garantir que les tests sont terminés avant d'exécuter l’analyse :
  ```yaml
  depends_on:
    - run-tests
  ```

---

## **5. Résolution des problèmes courants**

1. **Couverture à 0% dans SonarQube :**
   - Vérifiez que le rapport est bien généré (`ls -l /path/to/report` dans Drone).
   - Assurez-vous que le chemin configuré dans `sonar-project.properties` correspond exactement.

2. **Problème d’accès au serveur SonarQube :**
   - Vérifiez que l’URL du serveur (`sonar.host.url`) et le token d’accès (`SONAR_TOKEN`) sont corrects.

3. **Rapports introuvables entre étapes :**
   - Confirmez que les fichiers sont bien écrits dans le volume partagé et qu’ils sont accessibles dans les étapes suivantes (`ls -l /shared-volume`).

---

## **6. Exemple Drone général**

Voici une configuration Drone complète pour un projet générique avec SonarQube :

```yaml
kind: pipeline
type: docker
name: default

steps:
  - name: run-tests
    image: node:18
    volumes:
      - name: shared-volume
        path: /reports
    commands:
      - npm install
      - npm test -- --coverage
      - cp coverage/lcov.info /reports/lcov.info

  - name: sonar_scan
    image: sonarsource/sonar-scanner-cli:latest
    environment:
      SONAR_TOKEN:
        from_secret: sonar_token
    volumes:
      - name: shared-volume
        path: /reports
    commands:
      - sonar-scanner
    depends_on:
      - run-tests

volumes:
  - name: shared-volume
    temp: {}
```

---

[Menu CI](../menu.md)