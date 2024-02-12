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