# Configuration de Docker sans sudo sur Linux
---
[...retour arrière](../menu.md)

---

## Introduction

Docker est un outil puissant pour la création et la gestion de conteneurs. Cependant, par défaut, Docker nécessite des permissions root pour fonctionner, ce qui peut poser des problèmes de sécurité et de commodité. Ce guide explique comment configurer Docker pour qu'il puisse être utilisé sans sudo, en ajoutant l'utilisateur au groupe `docker` et en créant un service systemd pour garantir que les permissions appropriées sont appliquées à chaque démarrage.

## Concepts de base

### Docker et le groupe Docker

Docker utilise un socket Unix (`/var/run/docker.sock`) pour communiquer avec le démon Docker (`dockerd`). Par défaut, ce socket appartient à l'utilisateur root et au groupe docker. Les utilisateurs non root peuvent accéder à ce socket en étant ajoutés au groupe docker. Cependant, cette configuration nécessite des ajustements pour être persistante et sécurisée.

### Permissions Unix

Les permissions Unix déterminent qui peut lire, écrire ou exécuter un fichier. Les commandes `chown` et `chmod` sont utilisées pour modifier les propriétaires et les permissions des fichiers et des répertoires.

- `chown` : change l'utilisateur et/ou le groupe propriétaire d'un fichier.
- `chmod` : change les permissions d'un fichier ou d'un répertoire.

## Étapes de configuration

### 1. Ajouter l'utilisateur au groupe Docker

Ajouter l'utilisateur actuel au groupe docker pour lui permettre d'accéder au socket Docker sans sudo.

```sh
sudo usermod -aG docker ${USER}
```

- `usermod` : modifie un compte utilisateur.
- `-aG docker ${USER}` : ajoute (`-a`) l'utilisateur (`${USER}`) au groupe (`-G`) docker.

### 2. Appliquer les nouveaux groupes

Déconnecter et reconnecter pour que les modifications de groupe prennent effet. Utiliser `newgrp` pour appliquer les changements de groupe sans déconnexion.

```sh
newgrp docker
```

### 3. Vérifier l'ajout au groupe Docker

Vérifier que l'utilisateur a bien été ajouté au groupe docker.

```sh
id -nG
```

### 4. Vérifier les permissions du socket Docker

Changer les permissions du socket Docker pour s'assurer que le groupe docker peut y accéder.

```sh
sudo chown root:docker /var/run/docker.sock
sudo chmod 660 /var/run/docker.sock
```

### 5. Redémarrer le service Docker

Redémarrer Docker pour appliquer les changements de permissions.

```sh
sudo systemctl restart docker
```

### 6. Tester l'accès Docker sans sudo

Vérifier que Docker fonctionne sans sudo.

```sh
docker run hello-world
```

### 7. Automatiser la configuration des permissions avec systemd

Créer un service systemd pour garantir que les permissions appropriées sont appliquées à chaque démarrage.

#### Création d'un fichier de service systemd

1. Créer un fichier de service systemd :

   ```sh
   sudo nano /etc/systemd/system/docker-permissions.service
   ```

2. Ajouter les configurations suivantes au fichier :

   ```ini
   [Unit]
   Description=Fix Docker Socket Permissions
   After=docker.service
   PartOf=docker.service

   [Service]
   Type=oneshot
   ExecStart=/bin/bash -c 'chown root:docker /var/run/docker.sock && chmod 660 /var/run/docker.sock'
   RemainAfterExit=true

   [Install]
   WantedBy=multi-user.target
   ```

   - `[Unit]` : décrit le service.
     - `Description` : description du service.
     - `After` : démarre ce service après le service docker.
     - `PartOf` : ce service fait partie de docker.service.
   - `[Service]` : spécifie les détails du service.
     - `Type` : type de service, `oneshot` signifie que le service s'exécute une fois et s'arrête.
     - `ExecStart` : commande à exécuter pour démarrer le service.
     - `RemainAfterExit` : indique que le service est actif même après la fin du processus.
   - `[Install]` : spécifie quand le service doit être démarré.
     - `WantedBy` : groupe de cibles pour le démarrage.

3. Recharger les unités systemd pour appliquer les nouvelles configurations :

   ```sh
   sudo systemctl daemon-reload
   ```

4. Activer et démarrer le service personnalisé :

   ```sh
   sudo systemctl enable docker-permissions.service
   sudo systemctl start docker-permissions.service
   ```

### 8. Vérification du service

#### Vérifier le statut du service

Utiliser `systemctl status` pour vérifier le statut du service.

```sh
sudo systemctl status docker-permissions.service
```

#### Vérifier les logs du service

Utiliser `journalctl` pour afficher les logs du service.

```sh
sudo journalctl -u docker-permissions.service
```

#### Tester après redémarrage

1. Redémarrer la machine :

   ```sh
   sudo reboot
   ```

2. Vérifier les permissions du socket Docker après redémarrage :

   ```sh
   ls -l /var/run/docker.sock
   ```

3. Tester une commande Docker sans sudo :

   ```sh
   docker run hello-world
   ```

---
[...retour arrière](../menu.md)
