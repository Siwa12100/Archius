# Images & partage de fichiers

[...retour au sommaire](../sommaire.md)

---

## Partager des images docker

Pour partager des images Docker, on dispose de plusieurs options.

### 1. **Créer notre propre registre Docker :**

- Docker Hub impose des limitations sur le nombre de tirs d'images (pulls) en une période de temps. Pour éviter ces limitations, on peut créer notre propre registre Docker en utilisant Docker lui-même.

- On lance un serveur de registre Docker conteneurisé avec la commande suivante :

  ```bash
  docker run -d -p 5000:5000 --restart=always --name registry -v data:/var/lib/registry registry:2
  ```

  Ici, notre registre est opérationnel, lié au port 5000, et utilise un volume local pour le stockage persistant des images.

- Supposons qu'on ait une image locale, par exemple, Alpine 3.15. On peut la télécharger sur notre registre en deux commandes :

  ```bash
  docker tag alpine:3.15 localhost:5000/alpine:3.15
  docker push localhost:5000/alpine:3.15
  ```

**Explications :**

Ces deux commandes Docker sont utilisées pour taguer une image locale (dans cet exemple, Alpine 3.15) avec un nouveau nom et pousser cette image taguée vers un registre Docker local hébergé sur localhost:5000.

- Les autres machines sur le même réseau peuvent tirer cette image depuis le registre. Assurez-vous que Docker autorise les registres non sécurisés dans la configuration du démon Docker.

- Pour autoriser les registres non sécurisés, modifiez `/etc/docker/daemon.json` avec la configuration suivante :

  ```json
  {
    "insecure-registries": ["Votre_machine_IP:Votre_machine_port"]
  }
  ```

  Redémarrez ensuite le démon Docker avec `systemctl restart docker`.

### 2. **Utilisation de docker load/save :**

- Si on ne peut pas configurer un registre local, ou si on a une machine isolée, on peut déployer une image Docker à partir d'une archive tar.

- Pour créer une archive à partir d'une image existante, utilisez la commande :

  ```bash
  docker image save alpine:3.15 -o alpine_3.15.tar
  ```

- Pour charger l'archive à partir d'une archive, utilisez la commande :

  ```bash
  docker image load -i alpine_3.15.tar
  ```

- Cette méthode est moins recommandée pour un usage en production, car le format en couches de l'image ne peut pas être optimisé, et le nom de l'archive peut être sans rapport avec le nom et la version de l'image.

En résumé, la création d'un registre Docker local est la méthode privilégiée pour partager des images, offrant une solution plus robuste et évolutive. Cependant, l'utilisation de `docker load/save` peut être une option rapide dans des situations temporaires ou isolées.

## Partage de contenu

Pour partager du contenu entre les conteneurs ou le système hôte dans Docker, plusieurs mécanismes sont disponibles.

### 1. **Bind Mounts :**

Le binding consiste à partager un fichier ou un dossier existant sur le système de fichiers hôte avec un conteneur donné. Pour utiliser les bind mounts, vous pouvez utiliser la commande `docker run` avec l'option `--mount` ou `-v` :

```bash
docker run --mount src=/path/on/host,dst=/path/in/container imageName
```

- Si le fichier ou le répertoire existe sur le système hôte, il sera partagé avec le conteneur.
- Si le fichier ou le répertoire n'existe pas, l'option `-v` le créera sur l'hôte, tandis que l'option `--mount` générera une erreur.

### 2. **Volumes Docker :**

Les volumes Docker sont des conteneurs de stockage multiplateformes, gérés par l'API Docker. Ils peuvent être partagés entre les conteneurs et ont un cycle de vie distinct. Voici quelques commandes pour gérer les volumes :

- Créer un volume :
  ```bash
  docker volume create myVolume
  ```

- Supprimer un volume :
  ```bash
  docker volume rm myVolume
  ```

- Lister les volumes existants :
  ```bash
  docker volume ls
  ```

- Exécuter un conteneur avec un volume monté à un emplacement donné :
  ```bash
  docker run -v myVolume:/mnt/myVolume imageName
  ```

- Syntaxe alternative avec `--mount` :
  ```bash
  docker run --mount src=myVolume,dst=/mnt/myVolume imageName
  ```

- Spécifier une permission de montage du volume :
  ```bash
  # Syntaxe -v
  docker run -v monVolume:/mnt/monVolume:ro nomImage

  # Syntaxe --mount
  docker run --mount src=myVolume,dst=/mnt/myVolume,readonly imageName
  ```

- Si le volume n'existe pas, Docker le créera automatiquement. Les volumes ne font pas partie de l'image Docker et se comportent comme un support de stockage externe.

### 3. **tmpfs :**

Docker sur Linux offre une option supplémentaire appelée tmpfs. Elle permet au conteneur de créer des fichiers en dehors de ses couches, stockés temporairement dans la mémoire de l'hôte. Voici comment utiliser tmpfs :

- Pas d'argument `src` :
  ```bash
  docker run --mount type=tmpfs,dst=/path/in/container imageName
  ```

- Syntaxe alternative avec `--tmpfs` :
  ```bash
  docker run --tmpfs /path/in/container imageName
  ```

- Utile pour stocker des fichiers sensibles temporairement, car ils ne persistent que dans la mémoire de l'hôte.
  
- Contrairement aux volumes et aux bind mounts, tmpfs ne peut pas être partagé entre les conteneurs.

Chacun de ces mécanismes offre des avantages spécifiques en fonction des besoins de votre application, de la persistance des données et de la confidentialité des fichiers. Vous pouvez choisir celui qui correspond le mieux à vos exigences.