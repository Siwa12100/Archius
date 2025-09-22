# Guide Détaillé : Utilisation de MongoDB avec Docker, Shell MongoDB et URLs de Connexion

[Retour menu Mongo](../menu.md)

---

## 1. Exposition de MongoDB dans un Conteneur Docker

### Configuration et Lancement de MongoDB avec Docker Compose
Pour exécuter MongoDB avec Docker Compose, voici un fichier `docker-compose.yml` typique :

```yaml
version: '3.9'

services:
  mongodb:
    image: mongo:6.0
    container_name: mongodb_valorium
    ports:
      - "127.0.0.1:27017:27017"
    environment:
      MONGO_INITDB_ROOT_USERNAME: root
      MONGO_INITDB_ROOT_PASSWORD: examplepassword
    volumes:
      - ./data_mongo_valorium:/data/db
    networks:
      - mongo_network

networks:
  mongo_network:
    driver: bridge
```

### Explications
1. **Exposition du port à `127.0.0.1`:**
   - En spécifiant `127.0.0.1:27017:27017`, MongoDB est accessible uniquement en local sur la machine hôte.
   - Cela empêche l'accès externe à MongoDB, améliorant ainsi la sécurité.

2. **Variables d'environnement :**
   - `MONGO_INITDB_ROOT_USERNAME` et `MONGO_INITDB_ROOT_PASSWORD` permettent de créer un utilisateur administrateur au démarrage.

3. **Volumes :**
   - `./data_mongo_valorium:/data/db` persiste les données MongoDB sur l’hôte pour éviter leur perte lors de la suppression du conteneur.

4. **Réseau Docker :**
   - `mongo_network` est un réseau Docker isolé pour permettre la communication entre conteneurs si besoin.

### Lancer et Tester
1. Lancez MongoDB avec Docker Compose :
   ```bash
   docker-compose up -d
   ```

2. Testez la connexion :
   ```bash
   mongosh mongodb://root:examplepassword@127.0.0.1:27017
   ```

---

## 2. Administration de MongoDB avec `mongosh`

`mongosh` est un client en ligne de commande pour interagir avec MongoDB.

### Connexion au Serveur MongoDB
Pour se connecter :
```bash
mongosh mongodb://localhost:27017
```

### Commandes de Base pour Administrer MongoDB

#### Créer une Base de Données
Dans MongoDB, une base de données est créée automatiquement lors de l’insertion de la première collection ou document.
```javascript
use db-siwa; // Switch ou création d'une base de données
```

#### Créer une Collection
```javascript
db.createCollection("collection_name");
```

#### Insérer un Document dans une Collection
```javascript
db.collection_name.insertOne({ "name": "Siwa", "age": 25 });
```

#### Lister les Bases de Données
```javascript
show dbs;
```

#### Supprimer une Base de Données
```javascript
db.dropDatabase();
```

---

### Gestion des Utilisateurs

#### Créer un Utilisateur
Les utilisateurs sont liés à une base de données spécifique (par exemple, `admin`).

1. Passez à la base `admin` :
   ```javascript
   use admin;
   ```

2. Créez l’utilisateur :
   ```javascript
   db.createUser({
       user: "Siwa",
       pwd: "securepassword",
       roles: [
           { role: "readWrite", db: "db-siwa" }
       ]
   });
   ```

#### Modifier un Utilisateur
Pour modifier un mot de passe :
```javascript
db.updateUser("Siwa", { pwd: "newsecurepassword" });
```

#### Lister les Utilisateurs
```javascript
db.getUsers();
```

#### Supprimer un Utilisateur
```javascript
db.dropUser("Siwa");
```

---

## 3. Construction d'URLs MongoDB

### Structure d'une URL MongoDB
Une URL MongoDB suit la structure suivante :
```text
mongodb://<username>:<password>@<host>:<port>/<database>?<options>
```

### Exemple pour Spring Boot
```properties
spring.data.mongodb.uri=mongodb://Siwa:securepassword@127.0.0.1:27017/db-siwa?authSource=admin
```
- **`Siwa`** : Nom de l’utilisateur.
- **`securepassword`** : Mot de passe fictif.
- **`127.0.0.1:27017`** : Hôte et port.
- **`db-siwa`** : Base de données cible.
- **`authSource=admin`** : Indique que l’authentification doit être effectuée contre la base `admin`.

### Exemple pour VS Code
L’extension MongoDB de VS Code accepte les URLs MongoDB avec la même syntaxe :
```text
mongodb://Siwa:securepassword@127.0.0.1:27017/db-siwa?authSource=admin
```

### Encodage des Caractères Spéciaux
Si le mot de passe contient des caractères comme `@`, encodez-les :
- **`@`** : `%40`
- **`:`** : `%3A`

#### Exemple Avant Encodage
```text
mongodb://Siwa:@password@127.0.0.1:27017/db-siwa
```

#### Exemple Après Encodage
```text
mongodb://Siwa:%40password@127.0.0.1:27017/db-siwa
```

---

## 4. Importance de `?authSource=admin`

### Pourquoi Utiliser `authSource=admin` ?
Lorsque l’utilisateur est créé dans une base comme `admin`, MongoDB tente par défaut d’authentifier l’utilisateur dans la base de données cible (ici `db-siwa`). Si l'utilisateur n'existe que dans `admin`, l’authentification échoue.

Ajoutez `authSource=admin` pour spécifier que l’authentification doit utiliser la base `admin` :
```properties
spring.data.mongodb.uri=mongodb://Siwa:securepassword@127.0.0.1:27017/db-siwa?authSource=admin
```

### Conséquences Sans `authSource`
L’erreur suivante peut apparaître :
```
Authentication failed. The connection string contains invalid user information.
```
Cela indique que MongoDB ne trouve pas l'utilisateur dans la base de données cible.

---

[Retour menu Mongo](../menu.md)