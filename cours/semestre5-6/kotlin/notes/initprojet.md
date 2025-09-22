# Créer et gérer un projet Kotlin avec Maven

---

[...retorn en rèire](../sommaire.md)

---

## **1. Maven : Qu'est-ce que c'est et pourquoi l'utiliser ?**

Maven est un **outil de gestion de projet** utilisé principalement pour les projets Java et Kotlin. Il permet de :

✔ **Créer des projets facilement** avec une structure standardisée  
✔ **Gérer automatiquement les dépendances** (bibliothèques externes)  
✔ **Compiler, tester et packager** ton code en une seule commande  
✔ **Automatiser le déploiement et l’exécution** de ton projet  
✔ **Assurer la reproductibilité** du build (exécuter le projet sur n’importe quelle machine sans problème)

Maven fonctionne avec un **fichier de configuration central** appelé `pom.xml` (**Project Object Model**), qui définit toutes les informations nécessaires au projet.

---

## **2. Initialiser un projet Kotlin avec Maven**
### 📌 **Commande pour générer un projet Kotlin avec Maven**
```sh
mvn archetype:generate \
    -DarchetypeGroupId=org.jetbrains.kotlin \
    -DarchetypeArtifactId=kotlin-archetype-jvm \
    -DarchetypeVersion=1.4.32 \
    -DgroupId=com.monnom.monprojet \
    -DartifactId=monprojet \
    -Dversion=1.0-SNAPSHOT \
    -Dpackage=com.monnom.monprojet \
    -DinteractiveMode=false
```

---

### **2.1 Explication détaillée des paramètres de la commande**
Maven utilise un **archétype** pour générer la structure de base du projet. Chaque paramètre a un rôle spécifique :

#### **💡 Partie 1 : Choisir l'archétype Maven**
| Paramètre | Explication |
|-----------|------------|
| `-DarchetypeGroupId=org.jetbrains.kotlin` | Définit le **groupe** de l'archétype, c’est-à-dire l’organisation qui a créé cet archétype. Ici, `org.jetbrains.kotlin` indique que l’archétype provient de JetBrains. |
| `-DarchetypeArtifactId=kotlin-archetype-jvm` | Spécifie **quel archétype** utiliser. Un **archétype** est un modèle de projet préconfiguré. Ici, `kotlin-archetype-jvm` génère un projet Kotlin prêt à être exécuté sur la JVM. |
| `-DarchetypeVersion=1.4.32` | Définit la **version spécifique de l'archétype** que l'on souhaite utiliser. On peut trouver les versions disponibles sur [Maven Central](https://search.maven.org/). |

> **Pourquoi ces trois paramètres sont nécessaires ?**  
> Ils indiquent à Maven **quel modèle** de projet utiliser pour générer une structure adaptée à Kotlin.

---

#### **💡 Partie 2 : Définir les informations du projet**
| Paramètre | Explication |
|-----------|------------|
| `-DgroupId=com.monnom.monprojet` | Définit le **nom du groupe** auquel appartient ton projet. C'est souvent un **nom de domaine inversé** (`com.monnom`), ce qui permet d'éviter les conflits avec d'autres projets. |
| `-DartifactId=monprojet` | C'est **le nom unique de ton projet**. Cela correspondra au nom du dossier créé. |
| `-Dversion=1.0-SNAPSHOT` | Définit la **version initiale du projet**. `SNAPSHOT` signifie que c'est une version en cours de développement (non stable). Une version stable serait, par exemple, `1.0.0`. |
| `-Dpackage=com.monnom.monprojet` | Définit **le package de base** pour ton code source. Cela va créer une structure de dossiers cohérente sous `src/main/kotlin/com/monnom/monprojet/`. |
| `-DinteractiveMode=false` | Désactive le **mode interactif** pour éviter que Maven ne pose des questions à chaque étape. Cela permet d'automatiser la création du projet. |

> **Pourquoi définir un `groupId`, `artifactId` et `package` ?**  
> Ces valeurs permettent d'organiser ton code et de garantir une **unicité** entre tes différents projets.

---

## **3. Structure générée du projet Maven Kotlin**
Après l’exécution de la commande, Maven crée la structure suivante :

```
monprojet/
├── pom.xml
└── src/
    ├── main/
    │   ├── kotlin/
    │   │   └── com/monnom/monprojet/
    │   │       └── App.kt
    └── test/
        ├── kotlin/
        │   └── com/monnom/monprojet/
        │       └── AppTest.kt
```

- **`pom.xml`** : Fichier central de configuration de Maven.
- **`src/main/kotlin`** : Répertoire principal pour le code source Kotlin.
- **`src/test/kotlin`** : Répertoire pour les tests unitaires.

---

## **4. Comprendre et modifier `pom.xml`**
Le fichier `pom.xml` est **essentiel** car il contient :
- Les informations du projet (nom, version…)
- Les dépendances (bibliothèques externes)
- Les plugins de compilation et d’exécution

### 📌 **Exemple de `pom.xml` configuré pour Kotlin**
```xml
<project xmlns="http://maven.apache.org/POM/4.0.0"
         xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         xsi:schemaLocation="http://maven.apache.org/POM/4.0.0 http://maven.apache.org/xsd/maven-4.0.0.xsd">
    <modelVersion>4.0.0</modelVersion>

    <groupId>com.monnom.monprojet</groupId>
    <artifactId>monprojet</artifactId>
    <version>1.0-SNAPSHOT</version>

    <properties>
        <kotlin.version>1.8.20</kotlin.version>
    </properties>

    <dependencies>
        <dependency>
            <groupId>org.jetbrains.kotlin</groupId>
            <artifactId>kotlin-stdlib</artifactId>
            <version>${kotlin.version}</version>
        </dependency>
    </dependencies>

    <build>
        <plugins>
            <plugin>
                <groupId>org.jetbrains.kotlin</groupId>
                <artifactId>kotlin-maven-plugin</artifactId>
                <version>${kotlin.version}</version>
                <executions>
                    <execution>
                        <id>compile</id>
                        <goals>
                            <goal>compile</goal>
                        </goals>
                    </execution>
                    <execution>
                        <id>test-compile</id>
                        <goals>
                            <goal>test-compile</goal>
                        </goals>
                    </execution>
                </executions>
            </plugin>
        </plugins>
    </build>
</project>
```

---

## **5. Gérer les dépendances Maven**
Maven permet d'ajouter facilement des bibliothèques externes.

### 📌 **Ajouter une dépendance (exemple : Tesseract OCR)**
Commande Maven pour ajouter une dépendance **sans modifier `pom.xml`** :
```sh
mvn dependency:add-dependency -DgroupId=net.sourceforge.tess4j -DartifactId=tess4j -Dversion=4.5.5
```

---

## **6. Compiler et exécuter son projet**
Une fois le projet configuré, voici les commandes essentielles :

📌 **Compiler le projet :**
```sh
mvn compile
```

📌 **Exécuter le projet :**
```sh
mvn exec:java -Dexec.mainClass="com.monnom.monprojet.AppKt"
```

📌 **Créer un JAR exécutable :**
```sh
mvn package
java -jar target/monprojet-1.0-SNAPSHOT.jar
```

---

[...retorn en rèire](../sommaire.md)