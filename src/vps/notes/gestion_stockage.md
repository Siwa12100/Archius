# Notes sur la gestion et le nettoyage du stockage sur un VPS Debian

[...retour au sommaire](../menu.md)

---

## 1. Inspection du Stockage

### 1.1. Vérification de l'Espace Utilisé et Disponible

- **Commande `df`** : Affiche l'utilisation de l'espace disque des systèmes de fichiers.

  ```sh
  df -h
  ```

  - `-h` : Affiche les tailles de manière lisible (e.g., K, M, G).

### 1.2. Détails sur l'Utilisation des Fichiers et Répertoires

- **Commande `du`** : Affiche l'utilisation de l'espace disque des fichiers et des répertoires.

  ```sh
  du -sh /chemin/vers/repertoire
  ```

  - `-s` : Résumé, affiche uniquement le total.
  - `-h` : Format lisible (e.g., K, M, G).

- **Commande `ncdu`** : Outil interactif pour analyser l'utilisation du disque (nécessite une installation).

  ```sh
  sudo apt-get install ncdu
  ncdu /
  ```

### 1.3. Surveillance en Temps Réel

- **Commande `iostat`** : Fournit des statistiques sur l'entrée/sortie des périphériques de stockage.

  ```sh
  sudo apt-get install sysstat
  iostat
  ```

- **Commande `iotop`** : Affiche les processus les plus gourmands en I/O (nécessite une installation).

  ```sh
  sudo apt-get install iotop
  sudo iotop
  ```

---

## 2. Nettoyage du Stockage

### 2.1. Identification des Fichiers et Répertoires Grands et Anciens

- **Commande `find`** : Trouve les fichiers selon différents critères (e.g., taille, date).

  - Trouver les fichiers de plus de 100M :

    ```sh
    find / -type f -size +100M
    ```

  - Trouver les fichiers modifiés depuis plus de 30 jours :

    ```sh
    find / -type f -mtime +30
    ```

### 2.2. Suppression des Fichiers Temporaires et Caches

- **Commande `apt-get clean`** : Nettoie le cache des paquets téléchargés.

  ```sh
  sudo apt-get clean
  ```

- **Commande `journalctl --vacuum-time`** : Supprime les journaux système de plus de X jours.

  ```sh
  sudo journalctl --vacuum-time=30d
  ```

- **Commande `tmpwatch`** : Supprime les fichiers temporaires (nécessite une installation).

  ```sh
  sudo apt-get install tmpwatch
  sudo tmpwatch -am 30 /tmp
  ```

### 2.3. Gestion des Snapshots et Backups

- **Liste des snapshots** : Si vous utilisez un système de fichiers comme Btrfs ou LVM.

  ```sh
  sudo btrfs subvolume list /
  sudo lvs
  ```

- **Suppression des anciens snapshots** : 

  ```sh
  sudo btrfs subvolume delete /path/to/snapshot
  sudo lvremove /dev/vgname/lvname
  ```

---

## 3. Automatisation et Surveillance

### 3.1. Scripts de Nettoyage Régulier

- **Créer un script de nettoyage** : Par exemple, un script pour nettoyer les fichiers temporaires et les caches.

  ```sh
  #!/bin/bash
  sudo apt-get clean
  sudo journalctl --vacuum-time=30d
  sudo tmpwatch -am 30 /tmp
  ```

- **Planifier le script avec `cron`** :

  ```sh
  sudo crontab -e
  ```

  Ajouter la ligne suivante pour exécuter le script chaque semaine :

  ```plaintext
  0 2 * * 0 /chemin/vers/script-de-nettoyage.sh
  ```

### 3.2. Surveillance Automatisée

- **Configurer `monit`** pour surveiller l'espace disque (nécessite une installation) :

  ```sh
  sudo apt-get install monit
  sudo nano /etc/monit/monitrc
  ```

  Ajouter une règle pour surveiller l'espace disque :

  ```plaintext
  check filesystem rootfs with path /
    if space usage > 80% then alert
  ```

  Redémarrer `monit` :

  ```sh
  sudo service monit restart
  ```

---

[...retour au sommaire](../menu.md)