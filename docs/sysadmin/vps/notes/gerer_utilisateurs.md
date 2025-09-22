# Administration des Utilisateurs sur un Serveur Linux

[...retorn en rèire](../menu.md)

---

## Table des Matières

1. [Création d'un Utilisateur](#création-dun-utilisateur)
2. [Modification d'un Utilisateur](#modification-dun-utilisateur)
3. [Gestion des Permissions](#gestion-des-permissions)
4. [Configuration de l'Accès SSH](#configuration-de-laccès-ssh)
5. [Gestion des Permissions Sudo](#gestion-des-permissions-sudo)
6. [Sécurisation des Fichiers de l'Utilisateur](#sécurisation-des-fichiers-de-lutilisateur)

## Création d'un Utilisateur

Pour créer un nouvel utilisateur, utiliser la commande `adduser`.

### Syntaxe

```sh
sudo adduser nom_utilisateur
```

- **adduser** : Commande pour ajouter un nouvel utilisateur.
- **nom_utilisateur** : Nom du nouvel utilisateur.

### Exemple

Pour créer un utilisateur nommé `john` :

```sh
sudo adduser john
```

### Détails

- La commande `adduser` crée un nouveau répertoire personnel pour l'utilisateur sous `/home/nom_utilisateur`.
- Vous serez invité à définir un mot de passe pour le nouvel utilisateur.
- Vous pouvez également remplir des informations supplémentaires telles que le nom complet, le numéro de téléphone, etc. Ces champs sont facultatifs.

## Modification d'un Utilisateur

### Changer le Nom d'un Utilisateur

Utiliser `usermod` pour changer le nom d'un utilisateur.

```sh
sudo usermod -l nouveau_nom ancien_nom
```

- **usermod** : Modifie un compte utilisateur.
- **-l nouveau_nom** : Définit le nouveau nom d'utilisateur.
- **ancien_nom** : Le nom actuel de l'utilisateur.

### Exemple

Pour changer le nom de `john` en `johndoe` :

```sh
sudo usermod -l johndoe john
sudo usermod -d /home/johndoe -m johndoe
```

### Changer le Mot de Passe d'un Utilisateur

Utiliser `passwd` pour changer le mot de passe d'un utilisateur.

```sh
sudo passwd nom_utilisateur
```

### Exemple

Pour changer le mot de passe de `johndoe` :

```sh
sudo passwd johndoe
```

## Gestion des Permissions

### Comprendre les Permissions

Les permissions sous Linux sont définies par trois ensembles pour chaque fichier ou répertoire : le propriétaire, le groupe, et les autres. Les permissions sont :
- **r** : Lecture
- **w** : Écriture
- **x** : Exécution

### Modifier les Permissions

Utiliser `chmod` pour modifier les permissions.

```sh
chmod permissions chemin
```

- **chmod** : Change les permissions d'un fichier ou répertoire.
- **permissions** : Les nouvelles permissions.
- **chemin** : Chemin du fichier ou répertoire.

### Exemple

Pour donner au propriétaire toutes les permissions et empêcher les autres utilisateurs de voir ou modifier les fichiers dans le répertoire personnel de `johndoe` :

```sh
chmod 700 /home/johndoe
chmod -R 700 /home/johndoe
```

### Modifier le Propriétaire

Utiliser `chown` pour changer le propriétaire d'un fichier ou répertoire.

```sh
sudo chown propriétaire:group chemin
```

- **chown** : Change le propriétaire d'un fichier ou répertoire.
- **propriétaire:group** : Nouvel utilisateur et groupe propriétaire.
- **chemin** : Chemin du fichier ou répertoire.

### Exemple

Pour changer le propriétaire du répertoire personnel de `johndoe` à `johndoe` et le groupe à `johndoe` :

```sh
sudo chown johndoe:johndoe /home/johndoe
```

## Configuration de l'Accès SSH

### Changer le Port SSH

Modifier le fichier de configuration SSH pour changer le port par défaut.

#### Ouvrir le Fichier de Configuration

```sh
sudo nano /etc/ssh/sshd_config
```

#### Modifier le Port

Trouver et modifiez la ligne suivante :

```sh
#Port 22
```

Décommenter-la et changez `22` pour le port de votre choix, par exemple `2222` :

```sh
Port 2222
```

#### Redémarrer le Service SSH

```sh
sudo systemctl restart sshd
```

### Configurer l'Authentification par Clé SSH

#### Générer une Clé SSH sur le Client

```sh
ssh-keygen -t rsa -b 4096 -C "votre_email@example.com"
```

#### Copier la Clé Publique sur le Serveur

```sh
ssh-copy-id nom_utilisateur@adresse_ip
```

#### Configurer SSH pour Utiliser la Clé

Ajouter la clé publique au fichier `~/.ssh/authorized_keys` de l'utilisateur sur le serveur.

## Gestion des Permissions Sudo

### Donner les Permissions Sudo à un Utilisateur

Ajouter l'utilisateur au groupe `sudo`.

```sh
sudo usermod -aG sudo nom_utilisateur
```

### Configurer `sudo` pour Demander le Mot de Passe une Seule Fois

Modifier le fichier de configuration sudoers.

#### Ouvrir le Fichier de Configuration

```sh
sudo visudo
```

#### Ajouter une Configuration

Ajouter la ligne suivante pour l'utilisateur spécifique :

```sh
Defaults:nom_utilisateur timestamp_timeout=0
```

Exemple pour `johndoe` :

```sh
Defaults:johndoe timestamp_timeout=0
```

## Sécurisation des Fichiers de l'Utilisateur

### Modifier les Permissions du Répertoire Personnel

```sh
chmod 700 /home/nom_utilisateur
chmod -R 700 /home/nom_utilisateur
```

### Modifier les Permissions des Fichiers Individuels

Utiliser `chmod` et `chown` pour définir des permissions spécifiques pour des fichiers individuels.

### Exemple

Pour sécuriser un fichier spécifique pour `johndoe` :

```sh
chmod 600 /home/johndoe/fichier.conf
sudo chown johndoe:johndoe /home/johndoe/fichier.conf
```
