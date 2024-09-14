# Déployer Redis avec Docker et utilisation depuis projet Java

[...retour en arriere](../menu.md)

---

## Lancer Redis dans un conteneur Docker

### 1. Démarrer Redis dans Docker

Pour démarrer Redis dans un conteneur Docker, il faut simplement exécuter la commande suivante. Cette commande téléchargera automatiquement l'image Redis depuis Docker Hub et démarrera un conteneur Redis :

```bash
docker run -d --name redis-server -p 6379:6379 redis
```

- `-d` : Lance le conteneur en arrière-plan.
- `--name redis-server` : Donne un nom au conteneur pour faciliter sa gestion.
- `-p 6379:6379` : Expose le port 6379 du conteneur Redis à l'extérieur (ici sur ton VPS).

Redis sera alors accessible via le port 6379 de ton VPS.

### 2. Montage d'un volume pour persister les données

Pour que Redis puisse sauvegarder les données même si le conteneur est redémarré, il est recommandé de monter un volume qui persiste les fichiers Redis sur le disque de l'hôte (ton VPS). La commande suivante permet de monter un volume qui persiste les données dans un dossier spécifique :

```bash
docker run -d --name redis-server -p 6379:6379 \
-v /chemin/vers/dossier/persistant:/data \
redis
```

- `/chemin/vers/dossier/persistant` : Le chemin sur l'hôte où les données de Redis seront stockées de manière persistante.
- `/data` : Le répertoire dans le conteneur Redis où les données sont enregistrées.

### 3. Utilisation d'un fichier `redis.conf` personnalisé

Pour personnaliser la configuration de Redis, notamment pour ajouter une authentification par mot de passe, il est possible de monter un fichier `redis.conf` personnalisé. Voici comment procéder :

1. **Télécharger le fichier `redis.conf` par défaut** sur ton VPS :

    ```bash
    wget https://raw.githubusercontent.com/redis/redis/7.0/redis.conf
    ```

2. **Lancer Redis avec le fichier de configuration personnalisé** :

    ```bash
    docker run -d --name redis-server -p 6379:6379 \
    -v /chemin/vers/dossier/persistant:/data \
    -v /chemin/vers/redis.conf:/usr/local/etc/redis/redis.conf \
    redis redis-server /usr/local/etc/redis/redis.conf
    ```

Cette commande lance Redis avec un fichier de configuration personnalisé (`redis.conf`), permettant une personnalisation complète de l'instance Redis.

---

## Configurer l'authentification avec un mot de passe

Pour sécuriser l'accès à Redis avec un mot de passe, il faut modifier le fichier `redis.conf` et y ajouter la directive `requirepass`. 

1. Ouvre le fichier `redis.conf` que tu as téléchargé ou monté et modifie la ligne suivante pour ajouter un mot de passe :

    ```bash
    requirepass senhal
    ```

2. Redémarre Redis pour que le changement prenne effet :

    ```bash
    docker restart redis-server
    ```

Désormais, Redis demandera le mot de passe **`senhal`** pour toute connexion.

---

## Se connecter à Redis depuis Java avec JedisPooled

### 1. Dépendance Maven pour Jedis

Pour se connecter à Redis depuis un projet Java, il est nécessaire d'ajouter la dépendance **Jedis** dans ton projet Maven. Voici la dépendance à inclure dans le fichier `pom.xml` :

```xml
<dependency>
    <groupId>redis.clients</groupId>
    <artifactId>jedis</artifactId>
    <version>4.0.1</version> <!-- Assure-toi de toujours vérifier la dernière version -->
</dependency>
```

Cela permettra d'utiliser **JedisPooled** pour gérer efficacement les connexions à Redis.

### 2. Code Java pour se connecter à Redis avec mot de passe

Voici le code Java pour se connecter à Redis en utilisant **JedisPooled**, avec l'authentification par mot de passe configurée dans `redis.conf` :

```java
import redis.clients.jedis.JedisPooled;
import redis.clients.jedis.DefaultJedisClientConfig;
import redis.clients.jedis.HostAndPort;

public class ExempleRedis {
    public static void main(String[] args) {
        // Adresse du VPS où tourne Redis et le port
        HostAndPort address = new HostAndPort("149.7.5.30", 6379);

        // Configuration de l'utilisateur et du mot de passe
        JedisClientConfig config = DefaultJedisClientConfig.builder()
                .user("default")   // Utilisateur par défaut
                .password("senhal") // Mot de passe configuré dans redis.conf
                .build();

        // Création d'un objet JedisPooled pour se connecter à Redis
        JedisPooled jedis = new JedisPooled(address, config);

        // Exemple d'utilisation : ajouter et récupérer une clé
        jedis.set("foo", "bar");
        System.out.println(jedis.get("foo"));  // Affiche "bar"

        // Fermer la connexion lorsque c'est terminé
        jedis.close();
    }
}
```

### Explication :
- **`HostAndPort address = new HostAndPort("149.7.5.30", 6379);`** : Spécifie l'adresse IP et le port de ton instance Redis qui tourne sur ton VPS.
- **`DefaultJedisClientConfig.builder()`** : Utilise une configuration client pour définir le mot de passe d'authentification et l'utilisateur (ici, `default`).
- **`JedisPooled jedis = new JedisPooled(address, config);`** : Crée une connexion à Redis en utilisant l'adresse et la configuration fournies.

Ce code te permet de te connecter à ton instance Redis de manière sécurisée et d'exécuter des commandes Redis depuis Java.

---

[...retour en arriere](../menu.md)