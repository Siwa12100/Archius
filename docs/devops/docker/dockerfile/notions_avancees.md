# Dockerfile - Notions avancées

[...retour menu sur docker](../sommaire.md)

---

Dockerfile est un outil puissant et flexible pour créer des images Docker, et au-delà des instructions de base que nous avons vues précédemment, il existe des concepts plus avancés pour gérer des cas plus complexes ou optimiser vos images. Dans cette section, nous allons explorer ces concepts avancés en détail, avec des exemples et des explications approfondies pour chaque instruction.

## 1. `ENV` : Définition de variables d'environnement

### 1.1. Utilité
L'instruction `ENV` permet de définir des **variables d'environnement** dans l'image Docker, qui peuvent être utilisées à l'exécution du conteneur ou au sein du Dockerfile lors de la construction. Ces variables peuvent définir des configurations ou des valeurs utilisées par l'application.

### 1.2. Syntaxe
```Dockerfile
ENV <variable>=<valeur>
```

### 1.3. Exemple
```Dockerfile
ENV APP_ENV=production
ENV DEBUG=0
```
Dans cet exemple, deux variables sont définies :
- `APP_ENV` est définie à `production`, une variable souvent utilisée pour configurer l'application en mode production.
- `DEBUG` est désactivé avec `DEBUG=0`.

### 1.4. Utilisation des variables dans un Dockerfile
Une variable d'environnement définie avec `ENV` peut être réutilisée plus tard dans le Dockerfile. Par exemple :

```Dockerfile
ENV APP_ENV=production
RUN echo "L'environnement d'application est $APP_ENV"
```

### 1.5. Utilisation dans l'application
Ces variables sont également disponibles pour l'application à l'exécution du conteneur. Vous pouvez accéder aux variables d'environnement via le code de votre application, comme avec `process.env` en Node.js ou `os.environ` en Python.

---

## 2. `ARG` : Variables d'arguments de build

### 2.1. Utilité
`ARG` définit des variables disponibles uniquement **pendant la phase de construction** d'une image Docker. Elles ne persistent pas dans le conteneur à l'exécution. C'est utile pour passer des arguments à la construction, comme des versions spécifiques de logiciels.

### 2.2. Syntaxe
```Dockerfile
ARG <nom>=<valeur_par_défaut>
```

### 2.3. Exemple
```Dockerfile
ARG VERSION=1.0.0
RUN echo "Building version $VERSION"
```

Vous pouvez également passer une valeur pour cette variable lorsque vous exécutez la commande `docker build` :
```bash
docker build --build-arg VERSION=2.0.0 .
```
Cela remplace la valeur par défaut `1.0.0` par `2.0.0` pendant la construction.

### 2.4. Différence entre `ARG` et `ENV`
- `ARG` est uniquement utilisé pendant la phase de **construction** et n'est pas disponible dans l'image finale.
- `ENV` est accessible pendant la **construction et l'exécution** de l'image.

---

## 3. `VOLUME` : Déclaration de volumes pour la persistance des données

### 3.1. Utilité
L'instruction `VOLUME` crée un **point de montage** sur le système hôte pour un chemin spécifique dans le conteneur. Les données écrites dans ce chemin seront persistantes, même si le conteneur est supprimé. Cela permet de stocker des fichiers de manière indépendante du cycle de vie des conteneurs, comme des bases de données ou des fichiers de configuration.

### 3.2. Syntaxe
```Dockerfile
VOLUME [<chemin_dans_conteneur>]
```

### 3.3. Exemple
```Dockerfile
VOLUME /data
```
Cela signifie que le répertoire `/data` dans le conteneur sera monté sur un volume du système hôte.

### 3.4. Utilisation pratique
Lors de l'exécution du conteneur, Docker gérera automatiquement le volume. Si vous supprimez un conteneur, les données dans `/data` seront conservées sur le système hôte.

---

## 4. `USER` : Spécifier un utilisateur non-root

### 4.1. Utilité
L'instruction `USER` spécifie l'utilisateur sous lequel les commandes `RUN`, `CMD`, `ENTRYPOINT` et d'autres instructions seront exécutées. Par défaut, les conteneurs s'exécutent en tant que **root**, ce qui peut représenter un risque de sécurité. Il est recommandé d'utiliser un utilisateur non privilégié lorsque c'est possible.

### 4.2. Syntaxe
```Dockerfile
USER <nom_utilisateur>[:<groupe>]
```

### 4.3. Exemple
```Dockerfile
RUN adduser --disabled-password --gecos '' appuser
USER appuser
```
Cet exemple crée un utilisateur `appuser` et indique que toutes les commandes suivantes dans le Dockerfile seront exécutées sous cet utilisateur.

### 4.4. Sécurité
Utiliser un utilisateur non-root dans les conteneurs est une **bonne pratique de sécurité**, car cela limite l'accès aux fichiers système sensibles si une application dans le conteneur est compromise.

---

## 5. `HEALTHCHECK` : Surveillance de l'état du conteneur

### 5.1. Utilité
L'instruction `HEALTHCHECK` permet de définir une commande qui vérifie régulièrement l'état de santé du conteneur. Si le conteneur ne répond pas correctement, Docker peut redémarrer automatiquement le conteneur ou marquer son état comme "unhealthy".

### 5.2. Syntaxe
```Dockerfile
HEALTHCHECK [OPTIONS] CMD <command>
```
- **OPTIONS** : Par exemple, `--interval=<durée>` pour définir l'intervalle de temps entre les vérifications.
- **CMD** : La commande à exécuter pour vérifier l'état.

### 5.3. Exemple
```Dockerfile
HEALTHCHECK --interval=30s --timeout=10s \
  CMD curl -f http://localhost/ || exit 1
```
Cet exemple effectue une requête HTTP toutes les 30 secondes sur `http://localhost/` et renvoie un état "unhealthy" si la requête échoue.

### 5.4. Utilisation avec Docker
Vous pouvez vérifier l'état de santé d'un conteneur avec la commande :
```bash
docker ps
```
Vous verrez l'état de chaque conteneur, y compris s'il est "healthy" ou "unhealthy".

---

## 6. `LABEL` : Ajout de métadonnées

### 6.1. Utilité
`LABEL` permet d'ajouter des **métadonnées** à une image Docker sous forme de paires clé-valeur. Ces labels peuvent contenir des informations sur l'auteur, la version, ou d'autres données de gestion.

### 6.2. Syntaxe
```Dockerfile
LABEL <clé>=<valeur> ...
```

### 6.3. Exemple
```Dockerfile
LABEL maintainer="email@example.com"
LABEL version="1.0"
```
Ici, l'image est étiquetée avec des informations sur le mainteneur et la version.

### 6.4. Utilité dans les registres Docker
Les labels sont souvent utilisés dans les registres d'images comme Docker Hub ou GitLab CI pour documenter l'image, indiquer l'équipe responsable, ou marquer des versions spécifiques.

---

## 7. `STOPSIGNAL` : Spécifier un signal d'arrêt

### 7.1. Utilité
`STOPSIGNAL` permet de définir le signal que Docker enverra au processus principal du conteneur pour demander son arrêt. Par défaut, Docker utilise le signal `SIGTERM`, mais certaines applications nécessitent un autre signal.

### 7.2. Syntaxe
```Dockerfile
STOPSIGNAL <signal>
```

### 7.3. Exemple
```Dockerfile
STOPSIGNAL SIGTERM
```
Cela indique à Docker d'envoyer le signal `SIGTERM` pour arrêter le conteneur. Vous pouvez aussi utiliser des signaux comme `SIGINT` ou d'autres selon les besoins de l'application.

---

## 8. `ONBUILD` : Instructions différées pour les images de base

### 8.1. Utilité
L'instruction `ONBUILD` permet de définir des actions qui ne seront exécutées **que lorsque l'image sera utilisée comme base pour une autre image**. Cela permet d'ajouter des comportements par défaut aux images de base sans les exécuter immédiatement.

### 8.2. Syntaxe
```Dockerfile
ONBUILD <instruction>
```

### 8.3. Exemple
```Dockerfile
ONBUILD COPY . /app
ONBUILD RUN make /app
```
Dans cet exemple, si cette image est utilisée comme base dans un autre Dockerfile, les fichiers seront copiés dans `/app` et le programme `make` sera exécuté dans le nouveau Dockerfile.

---

## 9. Multi-stage builds : Construction d'images multi-étapes

### 9.1. Utilité
Les **images multi-étages** permettent de séparer la phase de construction d'une application de la phase de production. Cette approche permet de créer des images finales plus légères en n'incluant que les fichiers nécessaires à l'exécution, tout en gardant les outils de build dans une étape précédente.

### 9.2. Syntaxe
```Dockerfile
FROM <image_de_base> AS

 <nom_étape>
```

### 9.3. Exemple
```Dockerfile
# Étape 1 : Compilation
FROM golang:1.16 AS builder
WORKDIR /app
COPY . .
RUN go build -o myapp

# Étape 2 : Image de production
FROM alpine:latest
COPY --from=builder /app/myapp /usr/local/bin/myapp
CMD ["myapp"]
```
Dans cet exemple :
- **Étape 1** : Utilise l'image `golang` pour compiler une application Go.
- **Étape 2** : Utilise une image légère `alpine` et copie l'exécutable Go compilé depuis la première étape. Cela crée une image de production minimale sans les outils de compilation.

### 9.4. Avantages
- **Réduction de la taille des images** : En supprimant les outils de build.
- **Séparation des préoccupations** : On peut utiliser des environnements distincts pour la compilation et l'exécution.

---

## 10. `SHELL` : Personnalisation du shell utilisé

### 10.1. Utilité
Par défaut, Docker utilise `/bin/sh` pour exécuter les commandes dans les conteneurs. L'instruction `SHELL` permet de changer ce comportement pour utiliser un autre shell, comme `bash` ou `powershell` (sous Windows).

### 10.2. Syntaxe
```Dockerfile
SHELL ["<shell>", "<option1>", "<option2>"]
```

### 10.3. Exemple
```Dockerfile
SHELL ["/bin/bash", "-c"]
```
Cela remplace le shell par défaut `/bin/sh` par `/bin/bash`.

---

[...retour menu sur docker](../sommaire.md)