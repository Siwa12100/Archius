# Lancement d'un Projet Maven (Java/Kotlin)

---

[...retorn en rèire](../sommaire.md)

---

## 1. Introduction

Ce document explique **de A à Z** comment compiler, exécuter et optimiser le lancement d'un projet Maven écrit en **Java** ou **Kotlin**. Nous couvrirons les notions de compilation, de classpath, de passage de paramètres, et bien plus encore.

---

## 2. Comprendre la Compilation et l'Exécution

### 2.1. Compilation : Qu'est-ce que c'est ?

La compilation est le processus de transformation du code source (
Kotlin/Java) en bytecode exécutable par la JVM (**Java Virtual Machine**). Maven utilise des **plugins** pour automatiser cette compilation.

- **Java** : Converti les fichiers `.java` en `.class` via le compilateur `javac`.
- **Kotlin** : Converti les fichiers `.kt` en `.class` via le compilateur Kotlin (`kotlinc`).

Ces fichiers `.class` sont stockés dans `target/classes/`.

### 2.2. Exécution : Qu'est-ce qui se passe ?

Lorsqu'on lance un programme :

1. La JVM charge les fichiers `.class`.
2. Elle exécute la \*\*méthode \*\***`main()`** contenue dans la classe principale.
3. Si des **dépendances** sont requises, elles sont aussi chargées.

---

## **3. Compilation et Exécution avec Maven (Détails approfondis sur le `.jar`)**

### **3.1. Compilation seule**
Si on souhaite uniquement **compiler** le projet (sans l’exécuter), on utilise la commande :
```sh
mvn compile
```
Cela va :
- Analyser les fichiers source (`.java` ou `.kt`).
- Convertir ces fichiers en bytecode `.class` (compréhensible par la JVM).
- Placer les fichiers `.class` compilés dans **`target/classes/`**.

👉 **Mais ces fichiers `.class` ne sont pas autonomes** :  
- Ils ne contiennent pas les **dépendances** du projet.
- Ils doivent être exécutés avec un **classpath** qui spécifie où trouver les autres classes nécessaires.

Pour exécuter un fichier `.class` sans Maven :
```sh
java -cp target/classes mon.package.MaClasse
```
Mais cette méthode devient **impraticable** dès qu’on a **des dépendances externes** (ex: bibliothèques Java comme `slf4j`, `jackson`, `spring`, etc.).

---

### **3.2. Compilation + Packaging (`.jar` : Java Archive)**
Le `.jar` (Java ARchive) est un format de fichier **compressé** qui contient :
- **Les fichiers `.class` compilés** de notre projet.
- Un **fichier `META-INF/MANIFEST.MF`** qui peut préciser **la classe principale**.
- Optionnellement : Les **dépendances** nécessaires.

Pour générer un `.jar`, on exécute :
```sh
mvn package
```
Cela produit un fichier **`target/mon-projet-1.0-SNAPSHOT.jar`**.

#### **Pourquoi générer un `.jar` ?**
✔ **Portabilité** : On peut exécuter le projet sur **n’importe quelle machine** équipée de la JVM.  
✔ **Encapsulation** : Regroupe **tout le code** en un seul fichier.  
✔ **Déploiement facile** : Pas besoin de gérer de nombreux fichiers `.class` dispersés.  

#### **Exécution d'un `.jar`**
Si le `.jar` contient la classe principale définie dans `MANIFEST.MF`, on peut le lancer avec :
```sh
java -jar target/mon-projet-1.0-SNAPSHOT.jar
```
Mais **par défaut**, Maven ne met pas les dépendances dans le `.jar`, donc cette commande échouera si notre projet dépend de bibliothèques externes.

👉 **Solution : Créer un `fat JAR` (ou `uber JAR`)**
Si notre projet dépend d’autres bibliothèques, il faut un `.jar` **auto-exécutable** contenant **toutes les dépendances** :
```xml
<plugin>
    <groupId>org.apache.maven.plugins</groupId>
    <artifactId>maven-shade-plugin</artifactId>
    <version>3.2.4</version>
    <executions>
        <execution>
            <phase>package</phase>
            <goals>
                <goal>shade</goal>
            </goals>
        </execution>
    </executions>
</plugin>
```
Avec cette configuration, la commande :
```sh
mvn package
```
produira un **gros `.jar`** (avec toutes les dépendances incluses), exécutable directement.

---

### **3.3. Compilation + Exécution en une seule commande**
Si on veut **compiler puis exécuter immédiatement** le programme sans créer de `.jar` :
```sh
mvn compile exec:java -Dexec.mainClass="mon.package.MaClasse"
```
Ce que fait cette commande :
1. **Compile** les fichiers source (`.java` ou `.kt`) dans `target/classes/`.
2. **Exécute** directement la classe spécifiée en utilisant le classpath de Maven.

👉 **Avantages :**
- Permet d’exécuter le projet sans devoir créer un `.jar`.
- Gère automatiquement le **classpath** et les **dépendances**.

👉 **Inconvénients :**
- **Plus lent** qu’un exécutable `.jar` car Maven reconstruit souvent le projet.
- **Dépend de Maven**, donc pas idéal pour un déploiement.

### 3.4 **📌 Résumé des méthodes d’exécution**
| Action | Commande | Avantages | Inconvénients |
|--------|---------|-----------|---------------|
| **Compiler uniquement** | `mvn compile` | Vérifie le code, génère les `.class` | Ne permet pas d’exécuter |
| **Compiler + exécuter sans `.jar`** | `mvn compile exec:java -Dexec.mainClass="mon.package.MaClasse"` | Rapide pour du développement | Dépend de Maven |
| **Générer un `.jar`** | `mvn package` | Regroupe le projet en un seul fichier | Ne contient pas les dépendances par défaut |
| **Exécuter un `.jar`** | `java -jar target/mon-projet.jar` | Portable, facile à distribuer | Nécessite un `fat jar` si dépendances |
| **Créer un `.jar` avec toutes les dépendances (`fat jar`)** | `mvn package` avec `maven-shade-plugin` | 100% autonome | Fichier plus lourd |


---

## 4. Exécution d'un Projet Déjà Compilé

Si le projet est déjà compilé (`target/classes` existe) :

### 4.1. Exécution via la JVM sans Maven

```sh
java -cp target/classes mon.package.MaClasse
```

**Explication :**

- `-cp target/classes` : Spécifie le chemin des classes compilées
- `mon.package.MaClasse` : Spécifie la classe contenant `main()`

### 4.2. Exécution d'un `.jar` compilé

Si on a généré un `.jar` via `mvn package` :

```sh
java -jar target/mon-projet-1.0-SNAPSHOT.jar
```

Cela lance le projet sans Maven.

---

## 5. Passage de Paramètres

### 5.1. Avec `mvn exec:java`

```sh
mvn exec:java -Dexec.mainClass="mon.package.MaClasse" -Dexec.args="param1 param2"
```

Les paramètres seront accessibles via `String[] args` dans `main()`.

### 5.2. Avec `java -jar`

```sh
java -jar target/mon-projet.jar param1 param2
```

---

## 6. Gestion des Chemins (Relatifs vs Absolus)

### 6.1. Chemin Relatif

Utilisation d'un chemin relatif dans le projet :

```java
File fichier = new File("data/input.txt");
```

Si on exécute le projet depuis `~/projet/`, il cherchera `~/projet/data/input.txt`.

### 6.2. Chemin Absolu

Si on a un chemin fixe :

```java
File fichier = new File("/home/user/data/input.txt");
```

Cela fonctionne peu importe où on lance le projet.

---

## 7. Gestion des Dépendances

Maven gère les dépendances via le fichier `pom.xml`. Exemple :

```xml
<dependencies>
    <dependency>
        <groupId>org.jetbrains.kotlin</groupId>
        <artifactId>kotlin-stdlib</artifactId>
        <version>1.9.0</version>
    </dependency>
</dependencies>
```

Si on modifie `pom.xml`, il faut exécuter :

```sh
mvn clean package
```

Pour **télécharger et mettre à jour** les bibliothèques.

---

## 8. Optimisation du Temps de Lancement

### 8.1. Lancer plus rapidement

Si le projet est déjà compilé, inutile de recompiler :

```sh
java -cp target/classes mon.package.MaClasse
```

### 8.2. Utiliser le mode `daemon` en Kotlin

Ajoutez dans `pom.xml` :

```xml
<properties>
    <kotlin.compiler.execution.strategy>daemon</kotlin.compiler.execution.strategy>
</properties>
```

Cela **accélère la compilation**.

---

## 9. Résumé des Commandes Essentielles

| Action                                  | Commande                                                        |
| --------------------------------------- | --------------------------------------------------------------- |
| **Compiler**                            | `mvn compile`                                                   |
| **Compiler et exécuter**                | `mvn compile exec:java -Dexec.mainClass="mon.package.MaClasse"` |
| \*\*Compiler et créer un \*\***`.jar`** | `mvn package`                                                   |
| **Exécuter un projet déjà compilé**     | `java -cp target/classes mon.package.MaClasse`                  |
| \*\*Exécuter un \*\***`.jar`**          | `java -jar target/mon-projet.jar`                               |
| **Passer des paramètres**               | `mvn exec:java -Dexec.args="param1 param2"`                     |
| **Nettoyer et recompiler tout**         | `mvn clean package`                                             |

---

[...retorn en rèire](../sommaire.md)