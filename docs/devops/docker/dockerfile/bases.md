# Image docker - Bases et syntaxe

[...retour menu sur docker](../sommaire.md)

---

## 1. Les éléments fondamentaux du Dockerfile

Un **Dockerfile** est un fichier texte qui contient une série d'instructions, chacune formant une étape de la construction d'une image Docker. À chaque instruction, une nouvelle couche est ajoutée à l'image.

### 1.1. Structure d’un Dockerfile

Exemple simple de Dockerfile :

```Dockerfile
# Image de base à partir de laquelle nous construisons
FROM ubuntu:20.04

# Installation de curl
RUN apt-get update && apt-get install -y curl

# Copie des fichiers locaux dans l'image
COPY . /app

# Définir le répertoire de travail
WORKDIR /app

# Exposer le port 8080
EXPOSE 8080

# Commande par défaut pour démarrer l'application
CMD ["./myapp"]
```

### 1.2. Explication détaillée des instructions

#### 1.2.1. `FROM`

L'instruction `FROM` est **obligatoire** dans tout Dockerfile. Elle spécifie l'image de base à partir de laquelle on va construire l'image. L'image de base peut être une distribution Linux comme `ubuntu`, `alpine`, ou une image préconfigurée pour des environnements spécifiques comme `node`, `python`, etc.

- **Syntaxe :**
  ```Dockerfile
  FROM <image>[:<tag>] [AS <alias>]
  ```
  - `<image>` : Le nom de l'image de base.
  - `<tag>` : La version spécifique de l'image. Par défaut, Docker utilise `latest` si aucun tag n'est précisé.
  - `AS <alias>` : Utilisé pour identifier une étape de construction dans les images multi-étages.

- **Exemples :**
  ```Dockerfile
  FROM ubuntu:20.04
  ```
  - Utilise Ubuntu 20.04 comme base.

  ```Dockerfile
  FROM node:14-alpine
  ```
  - Utilise l'image légère `node` basée sur Alpine Linux, qui est idéale pour réduire la taille des images.

#### 1.2.2. `RUN`

L'instruction `RUN` exécute une commande dans l'image pendant sa construction. Chaque `RUN` ajoute une nouvelle couche à l'image. Les commandes typiques incluent l'installation de packages ou la configuration de l'environnement.

- **Syntaxe :**
  ```Dockerfile
  RUN <command>
  ```
  - `<command>` : La commande shell ou un tableau d'arguments à exécuter dans l'image.

- **Exemples :**
  ```Dockerfile
  RUN apt-get update && apt-get install -y curl
  ```
  - Met à jour les paquets et installe `curl`.

  ```Dockerfile
  RUN mkdir /app && chown -R user:user /app
  ```
  - Crée un répertoire `/app` et en donne la propriété à l'utilisateur `user`.

- **Astuce :** Combinez plusieurs commandes en une seule pour minimiser le nombre de couches. Exemple :
  ```Dockerfile
  RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*
  ```
  - Ici, la suppression des caches APT à la fin minimise la taille de l'image.

#### 1.2.3. `COPY` et `ADD`

Les instructions `COPY` et `ADD` permettent de copier des fichiers du système hôte vers l'image Docker. La différence principale est que `ADD` peut gérer des fichiers compressés et télécharger des fichiers via des URLs, tandis que `COPY` est une simple copie.

- **Syntaxe :**
  ```Dockerfile
  COPY <source> <destination>
  ADD <source> <destination>
  ```
  - `<source>` : Le chemin du fichier ou répertoire à copier sur l'hôte.
  - `<destination>` : Le chemin dans l'image Docker.

- **Exemples :**
  ```Dockerfile
  COPY ./src /app/src
  ```
  - Copie le dossier `src` de l'hôte dans le répertoire `/app/src` de l'image.

  ```Dockerfile
  ADD https://example.com/file.tar.gz /app
  ```
  - Télécharge et extrait automatiquement l'archive dans `/app`.

- **Astuce :** Utilisez `COPY` à moins que vous n'ayez vraiment besoin des fonctionnalités spécifiques de `ADD`.

#### 1.2.4. `WORKDIR`

`WORKDIR` définit le répertoire de travail pour toutes les commandes qui suivent cette instruction dans le Dockerfile. Cela simplifie la gestion des chemins relatifs.

- **Syntaxe :**
  ```Dockerfile
  WORKDIR <path>
  ```
  - `<path>` : Le chemin du répertoire de travail à utiliser.

- **Exemples :**
  ```Dockerfile
  WORKDIR /app
  ```
  - Définit `/app` comme le répertoire de travail, ce qui signifie que toutes les commandes suivantes utiliseront ce chemin par défaut.

- **Astuce :** Utilisez `WORKDIR` plutôt que de définir manuellement des chemins absolus dans plusieurs commandes `RUN`.

#### 1.2.5. `EXPOSE`

L'instruction `EXPOSE` indique à Docker que le conteneur utilisera un certain port. Cette instruction est informative et ne mappe pas automatiquement le port sur l'hôte. Vous devez utiliser `-p` ou `--publish` lors de l'exécution du conteneur pour rendre le port accessible.

- **Syntaxe :**
  ```Dockerfile
  EXPOSE <port> [<protocol>]
  ```
  - `<port>` : Le numéro du port.
  - `<protocol>` : Facultatif, peut être `tcp` (par défaut) ou `udp`.

- **Exemples :**
  ```Dockerfile
  EXPOSE 8080
  ```
  - Informe Docker que l'application écoutera sur le port 8080.

#### 1.2.6. `CMD`

`CMD` spécifie la commande par défaut qui sera exécutée quand le conteneur démarre. Si vous spécifiez plusieurs `CMD`, seule la dernière est prise en compte. `CMD` est souvent utilisé en conjonction avec `ENTRYPOINT` pour fournir des arguments par défaut.

- **Syntaxe :**
  ```Dockerfile
  CMD ["executable", "param1", "param2"]
  CMD command param1 param2
  ```
  - La première forme est une commande en mode **exec** (meilleure pratique), la seconde en mode **shell**.

- **Exemples :**
  ```Dockerfile
  CMD ["node", "server.js"]
  ```
  - Exécute `node server.js` quand le conteneur démarre.

  ```Dockerfile
  CMD /bin/bash
  ```
  - Lance un shell interactif.

- **Astuce :** Si vous voulez rendre les arguments modifiables via `docker run`, utilisez `CMD`. Si la commande doit être fixe, utilisez plutôt `ENTRYPOINT`.

#### 1.2.7. `ENTRYPOINT`

`ENTRYPOINT` est similaire à `CMD` mais ne peut pas être remplacé par des arguments passés à `docker run`. Cela est utile pour des conteneurs qui doivent exécuter une commande fixe, comme un serveur web.

- **Syntaxe :**
  ```Dockerfile
  ENTRYPOINT ["executable", "param1", "param2"]
  ENTRYPOINT command param1 param2
  ```

- **Exemples :**
  ```Dockerfile
  ENTRYPOINT ["python"]
  CMD ["app.py"]
  ```
  - Ici, `python` est toujours exécuté, mais l'utilisateur peut modifier l'argument (`app.py`).

- **Astuce :** Combinez `ENTRYPOINT` et `CMD` pour définir des valeurs par défaut tout en permettant des ajustements via `docker run`.

## 2. Construction et gestion des images Docker

### 2.1. Commandes Docker pour la construction d’images

#### 2.1.1. `docker build`
Cette commande construit une image Docker à partir du Dockerfile dans le répertoire courant (ou dans un chemin spécifié). Vous pouvez tagger l’image pendant la construction pour lui donner un nom et une version.

- **Syntaxe :**
  ```bash
  docker build [options] <path>
  ```

- **Options importantes :**
  - `-t <nom:tag>` : Taguer l'image avec un nom et une version (`nom` est le nom de l'image et `tag` est sa version).
  - `.` : Le répertoire où Docker cherche le Dockerfile.

- **Exemple :**
  ```bash
  docker build -t monapp:v1 .
  ```
  - Construis l'image depuis le Dockerfile dans le répertoire courant et lui attribue le nom `monapp` avec la version `v

1`.

#### 2.1.2. Autres commandes utiles

- **Lister les images locales :**
  ```bash
  docker images
  ```
  - Affiche les images disponibles localement sur votre machine.

- **Supprimer une image :**
  ```bash
  docker rmi <image_id>
  ```
  - Supprime une image Docker spécifique.

- **Taguer une image pour la pousser vers un registre :**
  ```bash
  docker tag monapp:v1 monrepo/monapp:v1
  ```

- **Pousser une image vers un registre :**
  ```bash
  docker push monrepo/monapp:v1
  ```
  - Pousse l'image vers Docker Hub ou un autre registre.

---

[...retour menu sur docker](../sommaire.md)