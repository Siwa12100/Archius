# Compilation, Exécution et Gestion d’un Projet Kotlin avec Maven

---

[...retorn en rèire](../sommaire.md)

---

## **1. Maven : Comment fonctionne l'exécution et la gestion du projet ?**

Maven repose sur un **cycle de vie du build** qui est une série d’étapes standard pour construire un projet. Ces étapes incluent :

1. **Validation** (`validate`) : Vérifie si le projet est bien structuré.
2. **Compilation** (`compile`) : Compile le code source.
3. **Tests Unitaires** (`test`) : Exécute les tests unitaires.
4. **Packaging** (`package`) : Regroupe le projet sous forme d’un **JAR** ou **WAR**.
5. **Vérification** (`verify`) : Exécute des tests supplémentaires.
6. **Installation** (`install`) : Installe le package localement pour d’autres projets.
7. **Déploiement** (`deploy`) : Déploie l’application sur un serveur distant.

Tu peux **exécuter chaque étape individuellement** avec `mvn <étape>`.

---

## **2. Compilation du projet**

### 📌 **Compiler le projet**
```sh
mvn compile
```
Cette commande :
✅ **Télécharge toutes les dépendances** définies dans `pom.xml` (si elles ne sont pas déjà en cache).  
✅ **Compile le code source Kotlin en bytecode Java** (`.class`).  
✅ Stocke les fichiers compilés dans le dossier `target/classes/`.  

👉 **À savoir** :
- Si une erreur de compilation est détectée, la commande s’arrête immédiatement.
- Maven **ne génère pas de JAR** à cette étape, il fait uniquement la compilation.

### 📌 **Recompiler le projet en nettoyant les anciens fichiers**
```sh
mvn clean compile
```
✅ **Supprime les fichiers temporaires** dans `target/` avant de recompiler.  
✅ Utile pour éviter d’avoir des fichiers obsolètes ou corrompus.  

---

## **3. Exécution du projet**
Maven ne fournit **pas nativement** de méthode pour exécuter une application Kotlin. On utilise un **plugin Maven** pour cela.

### 📌 **Ajouter le plugin d’exécution**
Ajoute ce plugin dans ton `pom.xml` :
```xml
<build>
    <plugins>
        <plugin>
            <groupId>org.codehaus.mojo</groupId>
            <artifactId>exec-maven-plugin</artifactId>
            <version>3.1.0</version>
            <executions>
                <execution>
                    <goals>
                        <goal>java</goal>
                    </goals>
                </execution>
            </executions>
        </plugin>
    </plugins>
</build>
```

Une fois le plugin ajouté, **tu peux exécuter ton programme avec la commande :**
```sh
mvn exec:java -Dexec.mainClass="com.monnom.monprojet.AppKt"
```
✅ **Lance l’application en exécutant la classe `AppKt`**.  
✅ **Automatiquement compile et exécute** ton projet dans la même commande.  

👉 **Attention !** Si ton fichier `App.kt` ne contient pas de fonction `main()`, cette commande ne fonctionnera pas.

### 📌 **Exemple de fichier `App.kt` pour tester**
```kotlin
package com.monnom.monprojet

fun main() {
    println("🚀 Hello, Maven avec Kotlin !")
}
```

---

## **4. Créer un JAR exécutable**
Un **JAR (Java Archive)** est un fichier contenant **tout le code compilé et les dépendances nécessaires** pour exécuter l’application sans Maven.

### 📌 **Générer un JAR**
```sh
mvn package
```
✅ **Compile le projet et génère un fichier JAR** dans `target/monprojet-1.0-SNAPSHOT.jar`.  

👉 **Attention** :
- Par défaut, Maven ne met **pas** les dépendances dans le JAR, ce qui peut empêcher son exécution.  
- Pour un **JAR exécutable**, il faut ajouter un **manifest**.

### 📌 **Exécuter le JAR généré**
```sh
java -jar target/monprojet-1.0-SNAPSHOT.jar
```
🚀 **Lance l’application sans avoir besoin de Maven**.  
💡 Utile pour **déployer ton projet sur un serveur ou partager l’application**.  

---

## **5. Créer un JAR avec toutes les dépendances**
Si ton projet dépend de bibliothèques externes, il faut **créer un JAR avec dépendances incluses**.

### 📌 **Ajouter le plugin `shade`**
Ajoute ceci dans `pom.xml` :
```xml
<build>
    <plugins>
        <plugin>
            <groupId>org.apache.maven.plugins</groupId>
            <artifactId>maven-shade-plugin</artifactId>
            <version>3.4.1</version>
            <executions>
                <execution>
                    <phase>package</phase>
                    <goals>
                        <goal>shade</goal>
                    </goals>
                </execution>
            </executions>
        </plugin>
    </plugins>
</build>
```
Ensuite, pour générer un JAR contenant **tout le projet + dépendances** :
```sh
mvn package
```
Puis exécuter :
```sh
java -jar target/monprojet-1.0-SNAPSHOT.jar
```
✅ Ce JAR pourra **s’exécuter sur n’importe quelle machine** sans besoin d’installer les dépendances séparément.

---

## **6. Autres Commandes Maven Essentielles**
### 📌 **Lister les dépendances du projet**
```sh
mvn dependency:tree
```
✅ **Affiche toutes les dépendances installées** et leurs versions.

### 📌 **Mettre à jour les dépendances**
```sh
mvn versions:display-dependency-updates
```
✅ **Liste les versions plus récentes disponibles** pour les dépendances utilisées.

### 📌 **Nettoyer le projet**
```sh
mvn clean
```
✅ **Supprime le dossier `target/`** et les fichiers compilés.

### 📌 **Exécuter les tests**
```sh
mvn test
```
✅ **Exécute tous les tests unitaires** situés dans `src/test/kotlin/`.

### 📌 **Installer le projet localement (pour l’utiliser dans d’autres projets)**
```sh
mvn install
```
✅ **Installe le projet dans le dépôt Maven local**, permettant de l’utiliser dans d’autres projets.

---

## **7. Résumé des commandes clés**
| Commande | Description |
|----------|------------|
| `mvn compile` | Compile le projet. |
| `mvn clean compile` | Nettoie puis compile. |
| `mvn exec:java -Dexec.mainClass="com.monnom.monprojet.AppKt"` | Exécute l’application. |
| `mvn package` | Génère un JAR. |
| `java -jar target/monprojet-1.0-SNAPSHOT.jar` | Exécute le JAR généré. |
| `mvn dependency:tree` | Liste les dépendances. |
| `mvn test` | Exécute les tests. |
| `mvn clean` | Supprime les fichiers compilés. |
| `mvn install` | Installe le projet localement. |

---

[...retorn en rèire](../sommaire.md)
