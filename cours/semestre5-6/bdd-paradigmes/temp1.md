# Correction détaillée du TP 3 : Nouveaux paradigmes de bases de données (Hadoop)

Ce TP a pour objectif de vous initier à la configuration d'une machine virtuelle pour exécuter Hadoop, et à l'utilisation de HDFS (Hadoop Distributed File System) pour la gestion des fichiers dans un environnement distribué. Il inclut également l’exécution d’un programme Hadoop (WordCount). Voici une correction détaillée et expliquée étape par étape.

## 1. Configuration de la machine virtuelle Hadoop

### 1.1 Télécharger l'image de la VM

- **Lien de téléchargement** : 
  [Cloudera-Udacity-Training-VM-4.1.1.c.zip](http://content.udacity-data.com/courses/ud617/Cloudera-Udacity-Training-VM-4.1.1.c.zip) (4,7 Go)
  
  **Remarque** : L'image VM est volumineuse, il est recommandé d'utiliser une connexion rapide et stable.

### 1.2 Configurer la VM avec VirtualBox

- Ouvrez **VirtualBox**.
- **Créer une nouvelle VM** :
  - Cliquez sur `New` pour créer une nouvelle machine virtuelle.
  - **Nom de la VM** : par exemple "hadoop".
  - **Type** : Linux.
  - **Version** : Ubuntu (64-bit).
- Sélectionnez l’option **Use an existing virtual hard drive file** et naviguez vers le fichier `Cloudera-Training-VM-4.1.1.c.vmdk`.

### 1.3 Configurer la connexion réseau

- Dans l’onglet **Fichier** de VirtualBox, allez dans **Outils** > **Gestionnaire réseau**.
- Créez un nouveau réseau interne si aucun n'est créé.
- Sélectionnez la machine virtuelle Hadoop que vous venez de créer.
- Dans l'onglet **Configuration**, allez dans **Réseau**, puis sélectionnez **Réseau privé**.
- Sélectionnez l'interface réseau interne que vous avez créée.

## 2. Commandes de base d’Hadoop

Hadoop est déjà installé et configuré sur la machine virtuelle. La commande `hadoop` est subdivisée en plusieurs sous-systèmes, dont `FsShell`, utilisé pour interagir avec HDFS.

### 2.1 Exploration de HDFS

#### Lancer `FsShell`

```bash
$ hadoop fs
```
Cette commande lance le sous-système associé à HDFS, permettant de manipuler des fichiers sur le système distribué.

#### Lister le contenu du répertoire racine de HDFS

```bash
$ hadoop fs -ls /
```

Cela permet de lister les répertoires à la racine de HDFS, où vous trouverez notamment le répertoire `/user`. Ce dernier contient des répertoires "home" pour chaque utilisateur.

#### Afficher le contenu du répertoire `/user`

```bash
$ hadoop fs -ls /user
```

Cela listera les répertoires des utilisateurs. Votre répertoire personnel est `/user/training`.

#### Afficher le contenu de votre répertoire personnel

```bash
$ hadoop fs -ls /user/training
```

Cela permet de vérifier que vous avez bien accès à votre répertoire personnel dans HDFS.

### 2.2 Gestion des fichiers dans HDFS

#### Création d’un fichier local

Créez un fichier texte local `poeme.txt` avec le texte de votre choix. Ce fichier sera ensuite manipulé dans HDFS.

#### Copier le fichier vers HDFS

```bash
$ hadoop fs -put poeme.txt /user/training/
```

Cette commande copie le fichier `poeme.txt` depuis votre système de fichiers local vers votre répertoire HDFS personnel.

#### Vérifier la présence du fichier dans HDFS

```bash
$ hadoop fs -ls -R /user/training/
```

Cela listera récursivement tous les fichiers et dossiers dans `/user/training` pour confirmer que le fichier a bien été copié.

#### Afficher le contenu du fichier dans HDFS

```bash
$ hadoop fs -cat /user/training/poeme.txt
```

Cette commande affiche le contenu du fichier `poeme.txt` directement dans la console.

#### Supprimer le fichier dans HDFS

```bash
$ hadoop fs -rm /user/training/poeme.txt
```

Cela supprime le fichier `poeme.txt` de HDFS.

#### Copier le fichier à nouveau dans HDFS

```bash
$ hadoop fs -copyFromLocal poeme.txt /user/training/
```

Cette commande est similaire à `hadoop fs -put`, elle permet de copier un fichier local vers HDFS.

#### Modifier les permissions du fichier

Ajouter des droits d’écriture pour le groupe et les autres utilisateurs :

```bash
$ hadoop fs -chmod go+w /user/training/poeme.txt
```

Retirer les droits de lecture pour le groupe et les autres utilisateurs :

```bash
$ hadoop fs -chmod go-r /user/training/poeme.txt
```

#### Déplacer le fichier dans un autre répertoire

```bash
$ hadoop fs -mv /user/training/poeme.txt /user/training/dossier/poeme.txt
```

Cela déplace le fichier `poeme.txt` vers le répertoire `dossier`.

#### Récupérer un fichier depuis HDFS vers le système local

```bash
$ hadoop fs -get /user/training/dossier/poeme.txt demat.txt
```

Cette commande copie le fichier `poeme.txt` depuis HDFS vers votre système local et le renomme `demat.txt`.

## 3. Exécution d’un premier programme Hadoop (WordCount)

### 3.1 Télécharger et configurer le projet WordCount

- Ouvrez **Eclipse** et importez le code de WordCount. Vous pouvez trouver ce code sur Moodle.
- Ajoutez les fichiers `.jar` nécessaires à votre projet en allant dans les **Propriétés** > **Bibliothèques** et en ajoutant les `.jar` d’Hadoop.

### 3.2 Transférer des fichiers vers la VM avec SCP ou SFTP

- **SCP** : pour transférer un fichier depuis votre machine locale vers la machine virtuelle, utilisez la commande suivante :

```bash
$ scp some_file.txt training@192.168.1.101:/home/training/
```

- **SFTP** : alternative à SCP, pour une session interactive :

```bash
$ sftp training@192.168.1.101
```
Ensuite, entrez le mot de passe (`training`) et transférez les fichiers.

### 3.3 Compiler votre code avec `javac`

Compilez le code en utilisant la commande `javac`, en précisant le classpath Hadoop :

```bash
$ javac -classpath $(hadoop classpath) -d . WordCount.java
```

### 3.4 Créer un fichier `.jar`

Une fois le code compilé, créez un fichier `.jar` avec la commande suivante :

```bash
$ jar -cvf wordcount.jar demos
```

Cette commande crée un fichier `.jar` à partir de votre projet.

### 3.5 Exécuter le programme WordCount sur Hadoop

Pour exécuter le programme sur Hadoop, utilisez la commande suivante :

```bash
$ hadoop jar wordcount.jar demos.WordCount <input_dir> <output_dir>
```

- `<input_dir>` : le répertoire contenant les fichiers à analyser (par exemple, `/user/training/input`).
- `<output_dir>` : le répertoire où Hadoop écrira les résultats (par exemple, `/user/training/output`).

### 3.6 Vérifier les résultats

Après l'exécution du programme, listez le contenu du répertoire de sortie :

```bash
$ hadoop fs -ls /user/training/output
```

Affichez les résultats du programme WordCount :

```bash
$ hadoop fs -cat /user/training/output/part-00000
```

Cela vous montrera le nombre d’occurrences de chaque mot dans le fichier traité.

## Conclusion

Ce TP vous permet de comprendre la configuration d'une machine virtuelle pour exécuter un cluster Hadoop, l’utilisation des commandes de base d’HDFS, et l’exécution de programmes MapReduce (comme WordCount). Une bonne maîtrise de ces concepts vous aidera à manipuler des fichiers dans un environnement distribué et à exécuter des tâches de traitement de données à grande échelle sur Hadoop.