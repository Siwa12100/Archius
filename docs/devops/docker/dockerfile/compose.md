# Guide détaillé sur Docker Compose

[...retour menu sur docker](../sommaire.md)

---

**Docker Compose** est un outil qui simplifie la gestion d’applications multi-conteneurs. Il permet de définir et de gérer plusieurs conteneurs à l’aide d’un seul fichier YAML, automatisant le démarrage, l'arrêt et l'orchestration des conteneurs. Ce guide couvrira en détail Docker Compose, sa syntaxe, son fonctionnement, et comment configurer une interaction entre un service **Spring Boot** et une base de données **MongoDB** avec persistance des données via des volumes.

## 1. Introduction à Docker Compose

### 1.1. Qu'est-ce que Docker Compose ?

Docker Compose permet de définir des environnements multi-conteneurs dans un fichier texte appelé **`docker-compose.yml`**. Ce fichier décrit les services, réseaux et volumes nécessaires à l’application. Compose permet ensuite de gérer facilement ces conteneurs à l’aide de simples commandes.

### 1.2. Cas d’utilisation

Docker Compose est idéal pour les environnements de développement ou les petits projets nécessitant plusieurs conteneurs interconnectés. Cela inclut des configurations comme :
- Une application web connectée à une base de données.
- Des services back-end connectés à des microservices.

## 2. Syntaxe et structure du fichier `docker-compose.yml`

Le fichier `docker-compose.yml` suit la syntaxe YAML. Voici les principaux éléments utilisés dans ce fichier :

### 2.1. Structure de base

Voici une structure simple d'un fichier `docker-compose.yml` :

```yaml
version: '3'  # Version du fichier Docker Compose

services:  # Définition des services
  app:  # Nom du service
    image: app_image  # Image Docker à utiliser pour le service
    ports:
      - "8080:8080"  # Mapping des ports entre l'hôte et le conteneur
    networks:
      - app_network  # Réseau auquel le conteneur est connecté

networks:  # Définition des réseaux
  app_network:
    driver: bridge  # Utilisation du réseau de type "bridge"
```

### 2.2. Explication des principaux blocs

- **`version`** : Indique la version de Docker Compose utilisée. La version `3` est la plus courante et compatible avec les fonctionnalités actuelles de Docker.
- **`services`** : Déclare les services qui représentent chaque conteneur Docker. Chaque service peut avoir des paramètres comme l'image Docker, les ports, les volumes, les variables d’environnement, etc.
- **`networks`** : Définit les réseaux sur lesquels les services doivent communiquer. Par défaut, Docker Compose crée un réseau isolé pour l’application si aucun n’est spécifié.

### 2.3. Principales instructions dans `docker-compose.yml`

- **`image`** : L’image Docker à utiliser pour le conteneur. Elle peut être tirée d’un registre Docker (comme Docker Hub) ou construite localement.
- **`build`** : Si vous devez construire une image à partir d’un Dockerfile local.
- **`ports`** : Mappage des ports entre l’hôte et le conteneur (ex : `8080:80`).
- **`volumes`** : Permet de monter des volumes entre l’hôte et le conteneur. Cela garantit la persistance des données.
- **`environment`** : Définit les variables d'environnement pour les services. Cela est souvent utilisé pour passer des informations sensibles comme des identifiants ou des URL de base de données.
- **`depends_on`** : Permet de spécifier les dépendances entre services. Par exemple, indiquer que le service `app` dépend du service `db` (la base de données).

---

## 3. Gestion des volumes et persistance des données

Dans Docker, les volumes sont utilisés pour persister les données, c’est-à-dire qu’ils permettent de stocker les informations indépendamment du cycle de vie des conteneurs. Lorsqu'un conteneur est supprimé, ses volumes peuvent être conservés pour éviter la perte de données.

### 3.1. Utilisation des volumes dans Docker Compose

Les volumes peuvent être définis directement dans le fichier `docker-compose.yml` pour garantir que certaines données (comme celles d’une base de données) soient conservées même si le conteneur est arrêté ou supprimé.

#### Exemple de configuration d’un volume pour MongoDB :

```yaml
services:
  mongodb:
    image: mongo:latest
    ports:
      - "27017:27017"
    volumes:
      - mongodb_data:/data/db  # Montre un volume pour stocker les données de MongoDB
    networks:
      - spring_network

volumes:
  mongodb_data:
```

### 3.2. Persistance des données avec Docker Compose

Lorsque des volumes sont configurés, les données sont persistées sur l'hôte. Même si le conteneur MongoDB est arrêté ou supprimé, les données restent disponibles dans le volume monté. Cela garantit la **durabilité des données**.

---

## 4. Utilisation de Docker Compose pour un service Spring Boot et MongoDB

Prenons un cas pratique où vous avez un service **Spring Boot** qui se connecte à une base de données **MongoDB** dans deux conteneurs séparés. Nous allons configurer Docker Compose pour gérer ces conteneurs, les connecter via un réseau Docker, et persister les données de MongoDB dans un volume.

### 4.1. Prérequis

Avant de commencer, assurez-vous que :
- Vous avez un projet **Spring Boot** qui utilise MongoDB comme base de données.
- Vous avez Docker et Docker Compose installés sur votre machine.

### 4.2. Exemple de fichier `docker-compose.yml`

Voici un exemple de fichier `docker-compose.yml` pour un service Spring Boot interagissant avec MongoDB :

```yaml
version: '3.8'

services:
  spring_service:
    image: spring_service_image  # Image Docker pour le service Spring Boot
    ports:
      - "8080:8080"  # Expose le service sur le port 8080
    environment:
      - MONGO_HOST=mongodb  # Nom du conteneur MongoDB pour la connexion
      - MONGO_PORT=27017  # Port de MongoDB
      - MONGO_DB=mydatabase  # Nom de la base de données
    depends_on:
      - mongodb  # S'assure que MongoDB est lancé avant Spring Boot
    networks:
      - spring_network

  mongodb:
    image: mongo:latest  # Utilise l'image officielle de MongoDB
    ports:
      - "27017:27017"  # Expose MongoDB sur le port 27017
    volumes:
      - mongodb_data:/data/db  # Persist les données de MongoDB dans un volume
    networks:
      - spring_network

volumes:
  mongodb_data:  # Volume pour persister les données MongoDB

networks:
  spring_network:
    driver: bridge
```

### 4.3. Explication détaillée

- **Service Spring Boot (`spring_service`)** :
  - Ce service utilise l'image Docker `spring_service_image` (qui doit contenir votre projet Spring Boot).
  - Le service est configuré pour communiquer avec MongoDB en utilisant le nom du conteneur `mongodb` (grâce au réseau `spring_network`).
  - Les variables d’environnement permettent de configurer la connexion à MongoDB (hôte, port, nom de la base de données).

- **MongoDB (`mongodb`)** :
  - Ce service utilise l’image officielle `mongo:latest`.
  - Les données sont persistées grâce au volume `mongodb_data`, monté sur le répertoire `/data/db` de MongoDB.
  - MongoDB est exposé sur le port `27017`, mais il est également accessible depuis l'application Spring via le réseau Docker.

- **Volumes et réseaux** :
  - Le volume `mongodb_data` garantit que les données de MongoDB sont conservées même après l'arrêt ou la suppression du conteneur.
  - Le réseau `spring_network` permet aux deux services de communiquer entre eux via leurs noms de conteneurs (`mongodb` et `spring_service`).

### 4.4. Commandes Docker Compose pour gérer ces services

#### 4.4.1. Démarrer les services

Pour démarrer tous les services définis dans le fichier `docker-compose.yml`, utilisez la commande suivante :
```bash
docker-compose up -d
```
L’option `-d` exécute les conteneurs en arrière-plan.

#### 4.4.2. Vérifier l’état des services

Pour voir l'état des services en cours d'exécution :
```bash
docker-compose ps
```

#### 4.4.3. Redémarrer les services après un arrêt

Lorsque vous arrêtez Docker Compose et que vous le redémarrez, les volumes définis conservent leurs données. Si vous avez correctement configuré le volume pour MongoDB, les données seront **toujours présentes** après redémarrage.

**Exemple** :
1. Arrêter les services :
   ```bash
   docker-compose down
   ```
2. Redémarrer les services :
   ```bash
   docker-compose up -d
   ```

Dans cet exemple, les données MongoDB stockées dans le volume `mongodb_data` seront persistées et accessibles après le redémarrage.

#### 4.4.4. Supprimer les volumes

Si vous souhaitez **supprimer** les volumes lorsque vous arrêtez vos conteneurs, utilisez l'option `--volumes` :
```bash
docker-compose down --volumes


```
Cela supprimera tous les volumes associés, et les données MongoDB seront perdues.

---

## 5. Gestion des variables d’environnement dans Docker Compose

Les variables d’environnement sont souvent utilisées pour transmettre des informations de configuration aux services. Dans notre cas, elles sont utilisées pour configurer la connexion de Spring Boot à MongoDB.

### 5.1. Définir des variables d’environnement dans `docker-compose.yml`

Voici un exemple de configuration de variables d’environnement pour un service Spring Boot :

```yaml
environment:
  - MONGO_HOST=mongodb
  - MONGO_PORT=27017
  - MONGO_DB=mydatabase
```

Ces variables peuvent ensuite être utilisées dans le fichier `application.properties` ou `application.yml` de Spring Boot :

**Exemple dans `application.properties`** :
```properties
spring.data.mongodb.host=${MONGO_HOST}
spring.data.mongodb.port=${MONGO_PORT}
spring.data.mongodb.database=${MONGO_DB}
```

### 5.2. Fichier `.env` pour Docker Compose

Docker Compose peut également utiliser un fichier `.env` pour stocker des variables d'environnement. Ce fichier peut être utilisé pour gérer des informations sensibles ou spécifiques à un environnement (comme les mots de passe).

**Exemple de fichier `.env`** :
```env
MONGO_HOST=mongodb
MONGO_PORT=27017
MONGO_DB=mydatabase
```

Docker Compose chargera automatiquement ces variables à partir du fichier `.env` si elles ne sont pas spécifiées directement dans le fichier `docker-compose.yml`.

---

[...retour menu sur docker](../sommaire.md)