# Manipulation de Fichiers et de Dossiers en Kotlin (avec Maven)

---

[...retorn en rèire](../sommaire.md)

---

## 1. Introduction
Kotlin fournit plusieurs classes et extensions pour manipuler les fichiers et dossiers de manière simple et efficace. Cette documentation explique en détail comment lire, écrire, créer et supprimer des fichiers et des dossiers en Kotlin, tout en respectant les conventions de projet Maven.

## 2. Structure des Fichiers dans un Projet Maven
Dans un projet Maven, les fichiers sont généralement organisés comme suit :

```
mon-projet/
│── src/
│   ├── main/
│   │   ├── kotlin/            # Code source Kotlin
│   │   ├── resources/         # Ressources (fichiers de configuration, données...)
│   ├── test/
│   │   ├── kotlin/            # Tests unitaires
│   │   ├── resources/         # Ressources pour les tests
│── target/                    # Dossier de compilation (généré après `mvn package`)
│── pom.xml                    # Fichier de configuration Maven
```

### Où Placer les Fichiers à Manipuler ?
- **Fichiers de configuration et données statiques** → `src/main/resources/`
- **Fichiers utilisés uniquement pour les tests** → `src/test/resources/`
- **Fichiers générés ou temporaires** → Dossier temporaire système (`java.io.tmpdir`) ou un sous-dossier de `target/`

## 3. Accéder aux Fichiers dans le Projet
### Chemins Absolus vs Relatifs
- **Chemin absolu** : Indique l’emplacement complet du fichier sur le système (ex: `C:\Users\monutilisateur\monprojet\data.txt` sur Windows ou `/home/monutilisateur/monprojet/data.txt` sur Linux/Mac).
- **Chemin relatif** : Défini par rapport au répertoire de travail actuel (`user.dir`).

#### Déterminer la Racine du Projet
```kotlin
val rootDir = File(System.getProperty("user.dir"))
println("Racine du projet : ${rootDir.absolutePath}")
```

#### Accéder aux fichiers dans `resources`
```kotlin
val resourceFile = File(ClassLoader.getSystemResource("data.txt").toURI())
println("Fichier dans resources : ${resourceFile.absolutePath}")
```

## 4. Manipulation de Fichiers

### 4.1 Création d'un Fichier
```kotlin
val file = File("data.txt")
if (file.createNewFile()) {
    println("Fichier créé : ${file.absolutePath}")
} else {
    println("Le fichier existe déjà")
}
```

### 4.2 Écriture dans un Fichier
#### Écrire une chaîne de caractères
```kotlin
file.writeText("Ceci est un exemple de texte écrit dans un fichier en Kotlin.")
```

#### Ajouter du texte à un fichier existant
```kotlin
file.appendText("\nAjout d'une nouvelle ligne.")
```

### 4.3 Lecture d'un Fichier
#### Lire tout le contenu en une seule fois
```kotlin
val contenu = file.readText()
println("Contenu du fichier : \n$contenu")
```

#### Lire ligne par ligne
```kotlin
file.forEachLine { line ->
    println("Ligne : $line")
}
```

### 4.4 Suppression d'un Fichier
```kotlin
if (file.delete()) {
    println("Fichier supprimé avec succès.")
} else {
    println("Impossible de supprimer le fichier.")
}
```

## 5. Manipulation de Dossiers

### 5.1 Création d'un Dossier
```kotlin
val dir = File("monDossier")
if (dir.mkdir()) {
    println("Dossier créé : ${dir.absolutePath}")
} else {
    println("Le dossier existe déjà.")
}
```

### 5.2 Création d'un Dossier avec Sous-Dossiers
```kotlin
val nestedDirs = File("monDossier/sousDossier1/sousDossier2")
if (nestedDirs.mkdirs()) {
    println("Dossiers créés : ${nestedDirs.absolutePath}")
} else {
    println("Les dossiers existent déjà.")
}
```

### 5.3 Lister les Fichiers d'un Dossier
```kotlin
dir.listFiles()?.forEach { file ->
    println("${if (file.isDirectory) "[D]" else "[F]"} ${file.name}")
}
```

### 5.4 Supprimer un Dossier et son Contenu
```kotlin
fun deleteRecursively(file: File): Boolean {
    return file.deleteRecursively()
}

deleteRecursively(dir)
```

## 6. Gestion des Chemins en Kotlin
Kotlin propose `java.nio.file.Path` pour gérer les chemins plus proprement.

### Exemple d'utilisation de `Paths`
```kotlin
import java.nio.file.Paths

val path = Paths.get("src/main/resources/data.txt")
println("Chemin absolu : ${path.toAbsolutePath()}")
```

## 7. Lecture et Écriture avec `java.nio.file`

### Écriture avec `Files.write`
```kotlin
import java.nio.file.Files
import java.nio.file.Paths
import java.nio.charset.StandardCharsets

val path = Paths.get("data.txt")
Files.write(path, listOf("Première ligne", "Deuxième ligne"), StandardCharsets.UTF_8)
```

### Lecture avec `Files.readAllLines`
```kotlin
val lines = Files.readAllLines(path, StandardCharsets.UTF_8)
lines.forEach { println(it) }
```

---

[...retorn en rèire](../sommaire.md)
