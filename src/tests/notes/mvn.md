## Introduction à Maven

[...retorn en arrièr](../menu.md)

---

## Le fichier `pom.xml`

e fichier `pom.xml` est le cœur d'un projet Maven.l contient toutes les informations nécessaires à Maven pour construire le projet, telles que les dépendances, les plugins, les configurations de build et les informations sur le projet.
### Structure de base d'un `pom.xml`

``xml
<project xmlns="http://maven.apache.org/POM/4.0.0"
         xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         xsi:schemaLocation="http://maven.apache.org/POM/4.0.0 http://maven.apache.org/xsd/maven-4.0.0.xsd">
    <modelVersion>4.0.0</modelVersion>

    <groupId>com.example</groupId>
    <artifactId>mon-projet</artifactId>
    <version>1.0-SNAPSHOT</version>
    <packaging>jar</packaging>

    <name>Mon Projet</name>
    <description>Une brève description de mon projet</description>

    <dependencies>
        <!-- Dépendances du projet -->
    </dependencies>

    <build>
        <plugins>
            <!-- Plugins Maven -->
        </plugins>
    </build>
</project>
```
**Éléments clés :**

- **`groupId`** : dentifie de manière unique l'organisation ou le groupe auquel appartient le projet.- **`artifactId`** : om unique du projet ou du module.- **`version`** : ersion actuelle du projet.- **`packaging`** : ype de packaging du projet (par exemple, `jar`, `war`).- **`dependencies`** : iste des bibliothèques externes nécessaires au projet.- **`build`** : onfigurations de build, y compris les plugins Maven.
---

## Gestion des dépendances avec Maven

aven simplifie la gestion des dépendances en téléchargeant automatiquement les bibliothèques nécessaires depuis des dépôts centralisés.our ajouter une dépendance, il suffit de la déclarer dans la section `<dependencies>` du `pom.xml`.
### Ajouter JUnit et Selenium comme dépendances

our intégrer JUnit et Selenium dans ton projet, ajoute les dépendances suivantes :
``xml
<dependencies>
    <!-- Dépendance JUnit 5 -->
    <dependency>
        <groupId>org.junit.jupiter</groupId>
        <artifactId>junit-jupiter-api</artifactId>
        <version>5.10.0</version>
        <scope>test</scope>
    </dependency>

    <!-- Dépendance Selenium -->
    <dependency>
        <groupId>org.seleniumhq.selenium</groupId>
        <artifactId>selenium-java</artifactId>
        <version>4.27.0</version>
    </dependency>
</dependencies>
```
**Explications :**

- **JUnit 5** : tilisé pour écrire et exécuter des tests unitaires. Le `scope` est défini à `test` car JUnit est uniquement nécessaire lors de l'exécution des tests.- **Selenium** : ibliothèque pour automatiser les navigateurs web.
---

## Commandes Maven essentielles

aven propose plusieurs commandes pour gérer le cycle de vie du projet :
- **`mvn clean`** : upprime le répertoire `target` où sont générés les fichiers compilés et les artefacts de build.- **`mvn compile`** : ompile le code source du projet.- **`mvn test-compile`** : ompile les sources de test.- **`mvn test`** : xécute les tests unitaires en utilisant le framework de test spécifié (par exemple, JUnit).- **`mvn package`** : ompile le code, exécute les tests et empaquette l'application (par exemple, en un fichier JAR ou WAR).- **`mvn install`** : nstalle l'artefact généré dans le dépôt local de Maven (`~/.m2/repository`), le rendant disponible pour d'autres projets locaux.- **`mvn clean install`** : ombine les commandes `clean` et `install` pour nettoyer le projet et réinstaller l'artefact.- **`mvn dependency:purge-local-repository`** : ettoie le cache des dépendances locales, forçant Maven à retélécharger les dépendances lors de la prochaine build.
---

Voici toutes les étapes **avec les commandes CLI** à exécuter et le **contenu du `pom.xml`** idéal pour ton TP Selenium.

---

## 🚀 **1. Initialiser le Projet Maven**
Tout d'abord, ouvre un terminal et exécute la commande suivante pour créer un **nouveau projet Maven** :

```bash
mvn archetype:generate \
  -DgroupId=com.example \
  -DartifactId=tp-selenium \
  -DarchetypeArtifactId=maven-archetype-quickstart \
  -DinteractiveMode=false
```

**Explication des options :**
- **`-DgroupId=com.example`** → Identifiant du groupe (peut être ton package Java).
- **`-DartifactId=tp-selenium`** → Nom du projet Maven.
- **`-DarchetypeArtifactId=maven-archetype-quickstart`** → Modèle de projet Maven standard.
- **`-DinteractiveMode=false`** → Génère le projet sans demander d’informations supplémentaires.

Après exécution, un dossier **`tp-selenium/`** est créé avec une structure de projet Maven.

---

## 📄 **2. Éditer le `pom.xml`**
Ensuite, ouvre le fichier `tp-selenium/pom.xml` et remplace son contenu par ceci :

```xml
<project xmlns="http://maven.apache.org/POM/4.0.0"
         xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         xsi:schemaLocation="http://maven.apache.org/POM/4.0.0 http://maven.apache.org/xsd/maven-4.0.0.xsd">
    <modelVersion>4.0.0</modelVersion>

    <!-- Infos du projet -->
    <groupId>com.example</groupId>
    <artifactId>tp-selenium</artifactId>
    <version>1.0-SNAPSHOT</version>
    <packaging>jar</packaging>

    <name>TP Selenium WebDriver</name>
    <description>Projet Maven pour tester des applications web avec Selenium</description>

    <!-- Spécification de la version de Java -->
    <properties>
        <maven.compiler.source>17</maven.compiler.source>
        <maven.compiler.target>17</maven.compiler.target>
    </properties>

    <!-- Dépendances -->
    <dependencies>
        <!-- Selenium WebDriver -->
        <dependency>
            <groupId>org.seleniumhq.selenium</groupId>
            <artifactId>selenium-java</artifactId>
            <version>4.27.0</version>
        </dependency>

        <!-- JUnit 5 -->
        <dependency>
            <groupId>org.junit.jupiter</groupId>
            <artifactId>junit-jupiter-api</artifactId>
            <version>5.10.0</version>
            <scope>test</scope>
        </dependency>

        <dependency>
            <groupId>org.junit.jupiter</groupId>
            <artifactId>junit-jupiter-engine</artifactId>
            <version>5.10.0</version>
            <scope>test</scope>
        </dependency>

        <!-- Apache Commons IO (pour gérer les fichiers, ex: captures d'écran) -->
        <dependency>
            <groupId>commons-io</groupId>
            <artifactId>commons-io</artifactId>
            <version>2.11.0</version>
        </dependency>
    </dependencies>

    <!-- Plugins Maven -->
    <build>
        <plugins>
            <!-- Plugin pour compiler le code avec Java 17 -->
            <plugin>
                <groupId>org.apache.maven.plugins</groupId>
                <artifactId>maven-compiler-plugin</artifactId>
                <version>3.8.1</version>
                <configuration>
                    <source>17</source>
                    <target>17</target>
                </configuration>
            </plugin>

            <!-- Plugin pour exécuter les tests -->
            <plugin>
                <groupId>org.apache.maven.plugins</groupId>
                <artifactId>maven-surefire-plugin</artifactId>
                <version>3.0.0-M7</version>
            </plugin>
        </plugins>
    </build>
</project>
```

---

## ⚡ **3. Installer les Dépendances**
Une fois le `pom.xml` mis à jour, exécute cette commande pour **télécharger toutes les dépendances nécessaires** :

```bash
mvn clean install
```

---

## 🔍 **4. Exécuter un Premier Test avec Selenium**
Maintenant, crée un fichier Java de test Selenium dans le dossier `src/test/java/com/example/` :

📌 **Fichier : `src/test/java/com/example/SeleniumTest.java`**
```java
import org.junit.jupiter.api.*;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.firefox.FirefoxDriver;

public class SeleniumTest {
    WebDriver driver;

    @BeforeEach
    void setUp() {
        driver = new FirefoxDriver();
    }

    @Test
    void testGoogleSearch() {
        driver.get("https://www.google.com");
        Assertions.assertEquals("Google", driver.getTitle(), "Le titre de la page ne correspond pas.");
    }

    @AfterEach
    void tearDown() {
        driver.quit();
    }
}
```

---

## 🎯 **5. Exécuter les Tests Maven**
Lance les tests avec :

```bash
mvn test
```

Si tout fonctionne, Maven affichera `BUILD SUCCESS`.

---

## 🚀 **6. Commandes Maven Importantes**
Voici un **récapitulatif** des commandes Maven essentielles :

| **Commande**                        | **Description** |
|--------------------------------------|----------------|
| `mvn clean`                          | Supprime les fichiers compilés dans `target/`. |
| `mvn compile`                        | Compile le projet Java. |
| `mvn test`                           | Exécute les tests unitaires avec JUnit. |
| `mvn package`                        | Compile et génère un fichier JAR/WAR. |
| `mvn install`                        | Installe l'artefact dans le dépôt local Maven (`~/.m2/repository`). |
| `mvn dependency:tree`                | Affiche l'arbre des dépendances. |
| `mvn dependency:purge-local-repository` | Vide le cache des dépendances Maven. |
| `mvn exec:java -Dexec.mainClass="com.example.Main"` | Exécute la classe principale du projet. |

---

## ✅ **7. Résumé des Étapes**
1️⃣ **Créer le projet Maven**  
```bash
mvn archetype:generate \
  -DgroupId=com.example \
  -DartifactId=tp-selenium \
  -DarchetypeArtifactId=maven-archetype-quickstart \
  -DinteractiveMode=false
```

2️⃣ **Mettre à jour le `pom.xml`** avec Selenium, JUnit et Java 17.

3️⃣ **Télécharger les dépendances**  
```bash
mvn clean install
```

4️⃣ **Créer un test Selenium** (voir `SeleniumTest.java`).

5️⃣ **Exécuter les tests**  
```bash
mvn test
```

6️⃣ **Utiliser les commandes Maven** pour compiler, exécuter et gérer le projet.

---

[...retorn en arrièr](../menu.md)