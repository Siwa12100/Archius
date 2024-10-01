# Hadoop: Commandes et Exploration de HDFS

[...retour menu sur parad. BDD](../menu.md)

---

## Introduction à Hadoop et HDFS

Hadoop est un framework open-source qui permet de stocker et de traiter de grandes quantités de données dans un environnement distribué. Hadoop inclut plusieurs sous-systèmes, notamment **HDFS (Hadoop Distributed File System)** pour le stockage distribué et **MapReduce** pour le traitement des données. 

Dans cette documentation, nous allons explorer les commandes de base pour interagir avec HDFS, qui est un système de fichiers distribué conçu pour supporter de grandes quantités de données. La commande principale utilisée est `hadoop fs`, qui permet de manipuler les fichiers dans HDFS.

## Prérequis

- Hadoop est déjà installé et configuré sur votre machine.
- Votre utilisateur dans ce TP est `training` avec le mot de passe `training`.

## Commandes Hadoop de Base

### 1. Accéder à HDFS

Pour accéder au sous-système **FsShell** (sous-système associé à HDFS), vous devez utiliser la commande `hadoop fs`. Cette commande vous permet d'interagir avec HDFS de manière similaire à la manière dont vous interagissez avec un système de fichiers local.

```bash
$ hadoop fs
```

### 2. Lister le contenu du répertoire racine HDFS

Pour afficher le contenu du répertoire racine `/` dans HDFS :

```bash
$ hadoop fs -ls /
```

Cela vous montrera plusieurs entrées, y compris le répertoire `/user`, où chaque utilisateur a un répertoire personnel. Dans ce TP, votre répertoire personnel est `/user/training`.

### 3. Lister le contenu du répertoire `/user`

Pour afficher le contenu du répertoire `/user` :

```bash
$ hadoop fs -ls /user
```

### 4. Lister le contenu de votre répertoire personnel

Pour afficher le contenu de votre répertoire personnel `/user/training` :

```bash
$ hadoop fs -ls /user/training
```

## Gestion de fichiers dans HDFS

### 1. Copier un fichier dans HDFS

#### Créer un fichier local

Créez un fichier texte local sur votre système Linux appelé `poeme.txt` et y mettez un poème ou un texte simple.

#### Copier le fichier vers HDFS

Utilisez la commande suivante pour copier le fichier `poeme.txt` vers votre répertoire HDFS personnel :

```bash
$ hadoop fs -put poeme.txt /user/training/
```

#### Vérifier la copie

Utilisez la commande `ls -R` pour lister les fichiers de manière récursive et vérifier que le fichier a bien été copié :

```bash
$ hadoop fs -ls -R /user/training/
```

### 2. Lire un fichier dans HDFS

#### Afficher le contenu d'un fichier

Pour afficher le contenu du fichier `poeme.txt` dans HDFS :

```bash
$ hadoop fs -cat /user/training/poeme.txt
```

Si le fichier est trop long, vous pouvez utiliser la commande `more` pour paginer la sortie :

```bash
$ hadoop fs -cat /user/training/poeme.txt | more
```

#### Afficher la fin d'un fichier

Pour afficher les derniers kilo-octets (Ko) d'un fichier :

```bash
$ hadoop fs -tail /user/training/poeme.txt
```

### 3. Supprimer un fichier dans HDFS

Pour supprimer un fichier de HDFS :

```bash
$ hadoop fs -rm /user/training/poeme.txt
```

### 4. Copier un fichier depuis le système local vers HDFS

La commande `hadoop fs -copyFromLocal` fonctionne de manière similaire à `hadoop fs -put`. Utilisez cette commande pour remettre le fichier `poeme.txt` dans HDFS après l'avoir supprimé :

```bash
$ hadoop fs -copyFromLocal poeme.txt /user/training/
```

### 5. Gestion des permissions

#### Changer les permissions d'un fichier

Pour ajouter des droits d'écriture pour les autres utilisateurs et le groupe sur le fichier `poeme.txt` :

```bash
$ hadoop fs -chmod go+w /user/training/poeme.txt
```

#### Vérifier les permissions

Pour vérifier les permissions du fichier et les informations de propriétaire :

```bash
$ hadoop fs -ls /user/training/
```

#### Enlever les permissions de lecture pour les autres utilisateurs

Pour retirer les droits de lecture pour les autres utilisateurs sur le fichier `poeme.txt` :

```bash
$ hadoop fs -chmod go-r /user/training/poeme.txt
```

### 6. Déplacer un fichier dans HDFS

Pour déplacer un fichier d'un répertoire à un autre dans HDFS, vous pouvez utiliser la commande `hadoop fs -mv`. Par exemple, pour déplacer `poeme.txt` dans un dossier appelé `dossier` :

```bash
$ hadoop fs -mv /user/training/poeme.txt /user/training/dossier/poeme.txt
```

Vérifiez ensuite que le fichier a bien été déplacé :

```bash
$ hadoop fs -ls -R /user/training/
```

### 7. Récupérer un fichier depuis HDFS vers le système local

Pour télécharger un fichier de HDFS vers votre système de fichiers local, vous pouvez utiliser la commande `hadoop fs -get`. Par exemple, pour récupérer le fichier `poeme.txt` et le renommer en `demat.txt` :

```bash
$ hadoop fs -get /user/training/dossier/poeme.txt demat.txt
```

---

[...retour menu sur parad. BDD](../menu.md)
