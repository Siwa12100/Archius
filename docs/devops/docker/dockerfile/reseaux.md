# Gestion des réseaux Docker - Interactions via terminal - Cmmunication entre services Spring Boot

[...retour menu sur docker](../sommaire.md)

---

Docker offre une infrastructure complète pour gérer les réseaux, interagir avec des conteneurs via le terminal, et permettre la communication entre services dans différents conteneurs. Cette documentation vous guidera à travers ces concepts, en couvrant également les commandes Docker pour la manipulation des réseaux, la gestion des ports, et la configuration de services Spring Boot pour la communication inter-conteneurs.

## 1. Les réseaux Docker

Les réseaux Docker permettent aux conteneurs de communiquer entre eux et avec l’extérieur. Docker gère plusieurs types de réseaux, chacun ayant un rôle spécifique dans la manière dont les conteneurs s’interconnectent.

### 1.1. Types de réseaux Docker

#### 1.1.1. Réseau **bridge** (par défaut)
Le réseau **bridge** est le mode réseau par défaut pour les conteneurs Docker. Si vous ne spécifiez aucun réseau au démarrage du conteneur, Docker le connecte automatiquement à un réseau bridge. Ce type de réseau permet aux conteneurs de communiquer entre eux via un réseau privé interne, mais ils ne sont pas accessibles directement depuis l’extérieur (à moins de mapper des ports).

- **Fonctionnement** :
  - Les conteneurs connectés à un réseau **bridge** peuvent se joindre via leurs adresses IP ou leurs noms de conteneur.
  - Pour accéder aux services à l'extérieur du conteneur, vous devez mapper les ports du conteneur à ceux de l’hôte.

**Exemple de création et d’utilisation d’un réseau `bridge`** :
```bash
# Créer un réseau personnalisé "mon_reseau"
docker network create mon_reseau

# Démarrer deux conteneurs et les connecter au réseau "mon_reseau"
docker run -d --name service1 --network mon_reseau service1_image
docker run -d --name service2 --network mon_reseau service2_image
```

Les deux conteneurs peuvent maintenant communiquer entre eux en utilisant leurs noms (`service1` et `service2`), sans avoir besoin de connaître leurs adresses IP respectives.

#### 1.1.2. Réseau **host**
Le réseau **host** supprime l'isolation réseau entre le conteneur et l'hôte. Le conteneur partage l'interface réseau de l'hôte, ce qui signifie qu’il accède directement aux services réseau de l’hôte, sans translation d'adresses IP.

- **Utilisation** : Utilisé principalement dans des cas où des performances réseau maximales sont requises ou lorsqu’un service doit avoir un accès complet à l'interface réseau de l'hôte.

**Exemple d’utilisation** :
```bash
docker run --network host my_image
```
Le conteneur accède directement aux interfaces réseau de l’hôte, et les ports ouverts dans le conteneur sont automatiquement accessibles à partir de l’hôte sans mapping.

#### 1.1.3. Réseau **overlay**
Le réseau **overlay** est utilisé pour connecter des conteneurs sur plusieurs hôtes Docker (par exemple dans une configuration Swarm). Il permet à des services Docker situés sur des hôtes différents de communiquer comme s’ils étaient sur le même réseau local.

- **Utilisation** : Dans des configurations distribuées, lorsque vous avez besoin que des conteneurs sur différents serveurs communiquent entre eux de manière sécurisée.

**Exemple de création d’un réseau `overlay`** :
```bash
docker network create -d overlay mon_overlay
```

#### 1.1.4. Réseau **macvlan**
Le réseau **macvlan** permet d'attribuer une adresse IP unique à chaque conteneur. Les conteneurs sont traités comme des périphériques réseau physiques et obtiennent une adresse MAC et une adresse IP directement sur le réseau de l’hôte.

- **Utilisation** : Lorsque les conteneurs doivent avoir leur propre IP sur le réseau local, par exemple pour interagir directement avec d'autres périphériques sur le même réseau que l’hôte.

### 1.2. Commandes Docker pour la gestion des réseaux

#### 1.2.1. Créer un réseau

Vous pouvez créer un réseau personnalisé en fonction de vos besoins en utilisant la commande `docker network create`.

- **Exemple** (réseau `bridge`) :
  ```bash
  docker network create my_bridge_network
  ```
  Cela crée un réseau de type **bridge** que vous pouvez utiliser pour connecter vos conteneurs.

- **Exemple** (réseau `overlay`) :
  ```bash
  docker network create -d overlay my_overlay_network
  ```

#### 1.2.2. Lister les réseaux Docker

Pour voir tous les réseaux disponibles sur votre hôte Docker, utilisez la commande suivante :
```bash
docker network ls
```

#### 1.2.3. Connecter un conteneur à un réseau existant

Vous pouvez ajouter un conteneur à un réseau existant en utilisant `docker network connect`.

- **Exemple** :
  ```bash
  docker network connect my_bridge_network my_container
  ```

#### 1.2.4. Déconnecter un conteneur d'un réseau

Pour retirer un conteneur d’un réseau, utilisez la commande `docker network disconnect` :

- **Exemple** :
  ```bash
  docker network disconnect my_bridge_network my_container
  ```

#### 1.2.5. Inspecter un réseau

Pour voir les détails d’un réseau, comme les conteneurs qui y sont connectés et leurs adresses IP :
```bash
docker network inspect my_bridge_network
```

#### 1.2.6. Supprimer un réseau

Vous pouvez supprimer un réseau Docker lorsqu'il n'est plus utilisé avec la commande `docker network rm`.

- **Exemple** :
  ```bash
  docker network rm my_bridge_network
  ```

---

## 2. Gestion des ports Docker

### 2.1. Mappage des ports

Dans Docker, les conteneurs ont leurs propres interfaces réseau, et leurs ports sont isolés de l’extérieur par défaut. Pour rendre les services accessibles depuis l’hôte ou l’extérieur, vous devez **mapper les ports** du conteneur vers les ports de l’hôte. Cela permet de rediriger le trafic entrant sur l’hôte vers le conteneur.

**Exemple de mappage de ports** :
```bash
docker run -d -p 8080:80 myapp
```
Dans cet exemple :
- Le port `8080` de l’hôte est mappé sur le port `80` du conteneur.
- Toute requête envoyée à `localhost:8080` sur l’hôte sera redirigée vers le service qui écoute sur le port `80` dans le conteneur.

### 2.2. Utilisation des ports avec des réseaux Docker

Si vous utilisez un réseau Docker interne (comme un réseau **bridge**), les conteneurs peuvent communiquer entre eux sans mappage de ports. Cependant, pour accéder à un conteneur depuis l’extérieur (hors du réseau Docker), vous devrez toujours mapper ses ports avec ceux de l’hôte.

---

## 3. Interagir avec un conteneur via le terminal

Docker vous permet d’interagir directement avec un conteneur à travers un terminal. Cela peut inclure l'exécution de commandes, l'ouverture d'une session shell interactive, ou le suivi des logs.

### 3.1. Exécuter une commande à l’intérieur d’un conteneur

Pour exécuter une commande dans un conteneur en cours d'exécution, vous pouvez utiliser la commande `docker exec`. Cette méthode est pratique pour administrer un conteneur sans avoir à ouvrir une session interactive complète.

**Exemple** :
```bash
docker exec <options> <nom_du_conteneur> <commande>
```
**Exemple concret** :
```bash
docker exec -it my_container ls /app
```
Cela exécute la commande `ls /app` dans le conteneur `my_container` et affiche le contenu du répertoire `/app`.

### 3.2. Ouvrir une session terminal interactive dans un conteneur

Si vous souhaitez interagir directement avec le système de fichiers et les processus d’un conteneur, vous pouvez ouvrir une session shell interactive dans le conteneur.

**Exemple** (avec `bash`) :
```bash
docker exec -it my_container /bin/bash
```
Cela vous ouvre un terminal interactif à l'intérieur du conteneur, où vous pouvez exécuter des commandes comme si vous étiez directement connecté au système.

**Exemple** (avec `sh` si `bash` n’est pas disponible) :
```bash
docker exec -it my_container /bin/sh
```

### 3.3. Démarrer un conteneur avec un terminal interactif

Vous pouvez également lancer un conteneur avec un terminal interactif dès son démarrage.

**Commande** :
```bash
docker run -it <image> /bin/bash
```
L’option `-it` combine `-i` (interactive) et `-t` (pseudo-tty) pour vous donner un terminal interactif dans le conteneur au démarrage.

### 3.4. Accéder aux logs d’un conteneur

Pour suivre les logs générés par un conteneur, vous pouvez utiliser la commande `docker logs`. Cela vous permet de visualiser la sortie standard (stdout) et les erreurs (stderr) du conteneur.

**Commande** :
```bash
docker logs <nom_du_conteneur>
```

**Pour suivre les logs en temps réel

** :
```bash
docker logs -f <nom_du_conteneur>
```
L’option `-f` permet de suivre les logs en temps réel, comme la commande `tail -f` sur un fichier log classique. Cela est utile pour surveiller ce qui se passe dans un conteneur en production ou en développement sans avoir à ouvrir une session interactive.

---

## 4. Utilisation des réseaux Docker pour la communication entre services Spring Boot dans différents conteneurs

Docker permet de configurer des réseaux isolés pour que les conteneurs puissent communiquer entre eux sans exposer les services à l’extérieur. Prenons l'exemple d'une architecture client-serveur avec deux applications Spring Boot distinctes : un **client** et un **service API**, exécutés dans des conteneurs différents. Nous allons voir comment utiliser Docker pour permettre à ces deux services de communiquer.

### 4.1. Création d'un réseau Docker pour Spring Boot

Tout d'abord, vous devez créer un réseau Docker dédié à vos conteneurs Spring Boot pour qu’ils puissent communiquer directement sans exposer leurs ports à l'extérieur.

**Commande pour créer un réseau** :
```bash
docker network create spring_network
```
Ce réseau permet d'isoler la communication entre les conteneurs sans avoir à exposer les services directement via des ports mappés sur l’hôte.

### 4.2. Démarrer le service Spring Boot (API REST)

Ensuite, vous pouvez démarrer le conteneur qui exécute le service Spring Boot. Ce service sera un serveur REST API qui attend des requêtes HTTP du client.

**Exemple de commande** :
```bash
docker run -d --name spring_service --network spring_network -p 8080:8080 spring_service_image
```

- `--network spring_network` : Cela connecte le conteneur au réseau `spring_network` que nous avons créé précédemment.
- `-p 8080:8080` : Cela expose le port `8080` du conteneur à l’extérieur pour que vous puissiez également tester l'API depuis l’hôte si nécessaire.

### 4.3. Démarrer le client Spring Boot

Maintenant, vous pouvez démarrer un conteneur séparé qui contient le **client Spring Boot**, conçu pour interroger le service API. Le client doit également être connecté au même réseau Docker pour pouvoir joindre le service.

**Exemple de commande** :
```bash
docker run -d --name spring_client --network spring_network spring_client_image
```

Le client Spring Boot est maintenant connecté au même réseau que le service.

### 4.4. Configuration du client Spring Boot pour communiquer avec le service

Dans le code du client Spring Boot, vous pouvez désormais référencer le service Spring Boot en utilisant **le nom du conteneur** au lieu de l'adresse IP ou de `localhost`. Grâce au réseau Docker, chaque conteneur est accessible par son nom comme s'il était un hôte sur le réseau local.

Voici un exemple de requête HTTP effectuée par le client Spring Boot pour interroger l’API du service :

```java
import org.springframework.web.client.RestTemplate;
import org.springframework.http.ResponseEntity;

public class ApiClient {

    public static void main(String[] args) {
        RestTemplate restTemplate = new RestTemplate();
        
        // Utilisez "spring_service" comme hostname, qui est le nom du conteneur du service API
        String url = "http://spring_service:8080/api/resource";
        
        // Effectuer la requête GET
        ResponseEntity<String> response = restTemplate.getForEntity(url, String.class);
        System.out.println("Response: " + response.getBody());
    }
}
```

- **`http://spring_service:8080/api/resource`** : Le nom `spring_service` fait référence au conteneur qui héberge le service Spring Boot. Docker résout automatiquement ce nom en l’adresse IP du conteneur sur le réseau `spring_network`.
- **Port 8080** : Le port interne du conteneur où le service écoute.

### 4.5. Cas pratique avec Docker Compose

Une alternative plus simple pour gérer plusieurs conteneurs et réseaux est **Docker Compose**, qui permet de configurer les conteneurs, réseaux et volumes dans un fichier `docker-compose.yml`. Voici un exemple pour gérer les conteneurs du client et du service Spring Boot.

**Fichier `docker-compose.yml`** :
```yaml
version: '3'
services:
  spring_service:
    image: spring_service_image
    networks:
      - spring_network
    ports:
      - "8080:8080"  # Expose pour test, mais pas nécessaire pour le client
  
  spring_client:
    image: spring_client_image
    networks:
      - spring_network
    depends_on:
      - spring_service

networks:
  spring_network:
    driver: bridge
```

- **`spring_service`** : Conteneur pour le service Spring Boot. Il est connecté au réseau `spring_network`.
- **`spring_client`** : Conteneur pour le client Spring Boot, également connecté au même réseau.
- **`networks`** : Définit le réseau Docker personnalisé pour la communication entre les conteneurs.

**Commande pour démarrer avec Docker Compose** :
```bash
docker-compose up -d
```

Cette commande démarre automatiquement les deux services (client et API) et configure le réseau pour qu’ils puissent communiquer.

---

[...retour menu sur docker](../sommaire.md)