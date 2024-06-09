# Configuration et utilisation de SSH sous Linux
---

[...retour en arrrière](../menu.md)

---

## Générer une Paire de Clés SSH

Exécuter dans le terminal :

```bash
ssh-keygen -t rsa -b 4096 -C "your_email@example.com"
```

- `-t rsa` : Type de la clé à créer.
- `-b 4096` : Longueur de la clé en bits.
- `-C` : Commentaire pour identifier la clé.

Choisir l'emplacement du fichier de clé lorsqu'il est demandé et créer une passphrase si nécessaire.

## Copier la Clé Publique sur le Serveur Distant

Utiliser la commande suivante pour automatiser la copie :

```bash
ssh-copy-id -i ~/.ssh/id_rsa.pub user@hostname -p port
```

- `user@hostname` représente le nom d'utilisateur et l'adresse IP ou le nom d’hôte du serveur.
- `-p port` spécifie le port SSH du serveur si différent du port par défaut (22).

## Se Connecter en Spécifiant le Port et la Clé

Pour se connecter en spécifiant un port et une clé :

```bash
ssh -i ~/.ssh/id_rsa -p port user@hostname
```

- `-i ~/.ssh/id_rsa` spécifie le fichier de la clé privée.
- `-p port` spécifie le port SSH.

## Configurer le fichier `.ssh/config`

Ajouter des configurations spécifiques par hôte pour simplifier les connexions SSH :

```plaintext
Host myserver
    HostName server.domain.com
    User myusername
    IdentityFile ~/.ssh/id_rsa
    Port 2222
    AddKeysToAgent yes
    ForwardAgent yes
```

## Copier des Fichiers avec `scp`

Pour copier la clé privée ou tout autre fichier du système local vers le serveur distant, utiliser `scp` :

```bash
scp -P port /path/to/file user@hostname:/path/to/destination
```

- `-P port` : Port du serveur SSH distant.
- `/path/to/file` : Chemin du fichier local à copier.
- `user@hostname` : Nom d'utilisateur et adresse du serveur distant.
- `/path/to/destination` : Chemin de destination sur le serveur.

Après avoir copié la clé publique, utiliser `scp` sans mot de passe en utilisant la clé SSH :

```bash
scp -i ~/.ssh/id_rsa -P port /path/to/file user@hostname:/path/to/destination
```

## Ajouter la Clé Publique sur le Serveur

Pour que la connexion SSH fonctionne sans mot de passe, ajouter la clé publique au fichier `authorized_keys` dans le répertoire `.ssh` du répertoire personnel de l'utilisateur sur le serveur distant :

1. Se connecter au serveur.
2. Créer le répertoire `.ssh` si nécessaire :
   
   ```bash
   mkdir -p ~/.ssh
   ```

3. Ajouter la clé publique à `~/.ssh/authorized_keys` :

   ```bash
   echo public_key_string >> ~/.ssh/authorized_keys
   ```

4. Définir les permissions correctes :

   ```bash
   chmod 700 ~/.ssh
   chmod 600 ~/.ssh/authorized_keys
   ```

## Utilisation de SSH pour Git

### Ajouter la Clé SSH à GitHub

1. Ajouter la clé publique à GitHub via **Settings > SSH and GPG keys > New SSH key**.
2. Utiliser l'alias dans le fichier `~/.ssh/config` pour simplifier la gestion des clés avec GitHub :

```plaintext
Host github.com
    HostName github.com
    User git
    IdentityFile ~/.ssh/id_rsa
    AddKeysToAgent yes
    IdentitiesOnly yes
```

### Cloner et Interagir avec les Dépôts

Cloner un dépôt en utilisant l'URL SSH :

```bash
git clone git@github.com:username/repository.git
```

Les interactions futures avec le dépôt utiliseront automatiquement la configuration SSH définie.

---

[...retour en arrrière](../menu.md)