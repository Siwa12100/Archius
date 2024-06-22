# Utilisation de SCP et SSHFS
---

[...retour au menu](../menu.md)

---

## I. SCP : Secure Copy

SCP permet de transférer des fichiers et des dossiers entre un client SSH et un serveur SSH en utilisant une syntaxe similaire à `cp` sous Linux.

### Transférer un fichier avec SCP
Pour envoyer un fichier d'une machine locale à une machine distante :
```bash
scp /chemin/local/vers/le/fichier utilisateur@ip_distant:/chemin/distant/vers/le/dossier
```
**Exemple :**
```bash
scp /home/mickael/data/Fichier2 root@192.168.10.131:/var/www/
```

#### Télécharger un fichier avec SCP
Pour télécharger un fichier d'une machine distante à ta machine locale :
```bash
scp utilisateur@ip_distant:/chemin/distant/vers/le/fichier /chemin/local/vers/le/dossier
```
**Exemple :**
```bash
scp root@192.168.10.131:/var/www/Fichier2 /home/mickael/data/
```

### Transférer un dossier avec SCP (récursivité)
Pour transférer un dossier entier, utilise l'option `-r` :
```bash
scp -r /chemin/local/vers/le/dossier utilisateur@ip_distant:/chemin/distant/vers/le/dossier
```
**Exemple :**
```bash
scp -r /home/mickael/data/ root@192.168.10.131:/var/www/
```

### Spécifier un port différent
Si le serveur SSH utilise un port autre que le port par défaut (22), utilise l'option `-P` :
```bash
scp -r -P port_numéro /chemin/local/vers/le/dossier utilisateur@ip_distant:/chemin/distant/vers/le/dossier
```
**Exemple :**
```bash
scp -r -P 7256 /home/mickael/data/ root@192.168.10.131:/var/www/
```

## II. SSHFS : SSH FileSystem

SSHFS permet de monter un répertoire d'une machine distante sur ta machine locale, offrant ainsi une vue en temps réel du répertoire distant comme s'il était local.

### Installation de SSHFS
Pour installer SSHFS sur Debian/Ubuntu :
```bash
sudo apt-get install sshfs
```
Pour installer SSHFS sur CentOS/RHEL :
```bash
sudo yum install fuse-sshfs
```

### Monter un répertoire distant
Pour monter un répertoire distant :
```bash
sshfs utilisateur@ip_distant:/chemin/distant/vers/le/dossier /chemin/local/vers/le/point_de_montage
```
**Exemple :**
```bash
mkdir /mnt/data
sshfs mickael@192.168.10.1:/home/mickael/data /mnt/data
```

### Vérifier le montage
Pour vérifier que le montage a été effectué correctement, utilise la commande `mount` :
```bash
mount | grep sshfs
```

### Avantages et cas d'utilisation
Monter un répertoire distant permet de manipuler les fichiers en temps réel. Toute modification sur le répertoire distant est immédiatement visible. C'est utile pour :
- Travailler sur des fichiers situés sur un serveur distant comme s'ils étaient locaux.
- Synchroniser des données entre plusieurs machines.
- Faciliter les sauvegardes et la gestion de fichiers à distance.

### Démonter un répertoire distant
Une fois terminé, démonte le répertoire pour couper la liaison SSHFS :
```bash
umount /chemin/local/vers/le/point_de_montage
```
**Exemple :**
```bash
umount /mnt/data
```

### Transférer et manipuler des fichiers via SSHFS
Avec SSHFS, télécharger et uploader des fichiers sur un serveur VPS distant devient simple. Une fois le répertoire monté, utilise les commandes Linux classiques (`cp`, `mv`, `rm`, etc.) pour manipuler les fichiers.

#### Exemple de téléchargement
Pour copier un fichier du répertoire monté à un répertoire local :
```bash
cp /mnt/data/fichier_local /chemin/local/vers/le/dossier/
```

#### Exemple d'upload
Pour copier un fichier local vers le répertoire monté :
```bash
cp /chemin/local/vers/le/fichier /mnt/data/
```

---

Avec SCP et SSHFS, gérer des fichiers entre machines distantes devient intuitif et efficace. SCP est idéal pour des transferts rapides, tandis que SSHFS permet un accès continu et en temps réel aux fichiers distants.