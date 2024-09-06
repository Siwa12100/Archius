# Redis

Redis (Remote Dictionary Server) est une base de données en mémoire, open-source, souvent utilisée pour la mise en cache, le stockage de données temporaires, et même comme une file d'attente distribuée. Redis est extrêmement rapide, car il stocke toutes ses données en mémoire, ce qui permet des opérations très rapides sur les données (à la différence de nombreuses autres bases de données qui stockent principalement les données sur disque).

Voici un aperçu détaillé de Redis et de son paradigme :

## 1. **Présentation de Redis**
Redis est une base de données NoSQL, souvent qualifiée de "structure de données en mémoire". Voici quelques-unes de ses caractéristiques principales :

- **In-memory** : Redis stocke ses données en mémoire RAM, ce qui permet des lectures et écritures extrêmement rapides. Les données peuvent être persistées sur disque pour la durabilité.
  
- **Multi-modèle** : Redis peut être utilisé comme une base de données clé-valeur, une file d’attente, un broker de messages, ou même une base de données orientée graphes. Il prend en charge plusieurs types de structures de données :
  - **String** : Chaînes de caractères binaires ou texte.
  - **List** : Listes ordonnées de chaînes.
  - **Set** : Ensembles non ordonnés de chaînes uniques.
  - **Hash** : Collections de paires clé-valeur.
  - **Sorted Set** : Ensembles de chaînes ordonnés avec des scores associés.
  - **Stream** : Pour la gestion de flux de données en temps réel.
  
- **Persistance** : Bien que Redis soit une base de données en mémoire, il offre des options pour la persistance des données (RDB snapshots, journaux AOF) afin d'assurer que les données ne sont pas perdues en cas de redémarrage.

- **Réplicabilité et Haute Disponibilité** : Redis supporte la réplication maître-esclave, ce qui permet d'avoir plusieurs instances de Redis synchronisées et une tolérance aux pannes.

- **Exécution distribué** : Redis est souvent utilisé dans des architectures distribuées où plusieurs instances sont interconnectées pour gérer une grande charge de données en temps réel.

## 2. **Paradigme NoSQL et NoCode**
Redis fait partie du mouvement **NoSQL**, qui signifie "Not Only SQL". Cela décrit des bases de données qui ne sont pas strictement relationnelles comme SQL. Les bases NoSQL sont conçues pour traiter de grandes quantités de données non structurées ou semi-structurées, et Redis en est un parfait exemple.

Le paradigme **NoCode**, par contre, désigne une approche où des utilisateurs non développeurs peuvent créer des applications ou manipuler des systèmes complexes sans avoir besoin d'écrire du code. En ce qui concerne Redis, on ne peut pas dire que c'est véritablement une base de données **NoCode** à proprement parler, car son utilisation nécessite des commandes CLI (command line interface) ou des appels API. Cependant, il existe des outils qui permettent d'interagir avec Redis dans un environnement visuel, sans avoir besoin d’écrire des scripts complexes :

- **RedisInsight** : C'est une interface graphique (GUI) qui permet d’interagir avec Redis de manière visuelle. Elle permet aux utilisateurs de visualiser et manipuler les données dans la base sans avoir à écrire de code.
  
- **Outils d'orchestration NoCode** : Dans certaines plateformes d'orchestration NoCode, Redis peut être intégré comme un backend de cache ou une solution de mise en file d'attente sans que l'utilisateur ne manipule directement les commandes Redis.

## 3. **Cas d'utilisation de Redis**
Redis est utilisé dans plusieurs cas d’utilisation courants :

- **Cache** : Il est souvent utilisé pour stocker des données fréquemment accédées et éviter de lourdes requêtes vers une base de données principale.
- **Mise en file d'attente** : Redis est utilisé comme une file d’attente distribuée pour gérer des files de travail dans des architectures d’applications microservices.
- **Sessions** : De nombreuses applications web utilisent Redis pour stocker les sessions des utilisateurs.
- **Analytics en temps réel** : Redis, avec ses structures de données rapides, est un choix populaire pour l'analyse des journaux et les tableaux de bord en temps réel.

---

Voici un guide détaillé pour chacune des étapes de ton TP sur Redis. Je vais t'expliquer chaque commande avec soin, en détaillant la syntaxe, le fonctionnement, et les notions sous-jacentes.

---

### **Partie I - Commandes avancées**

#### 1. Définir la clé "KEY1" avec la valeur "VALUE1"
```bash
SET KEY1 "VALUE1"
```
- **Explication** : La commande `SET` permet de définir une clé avec une valeur. Ici, `KEY1` est le nom de la clé, et "VALUE1" est la valeur associée à cette clé. Si la clé existe déjà, elle sera écrasée par la nouvelle valeur.

#### 2. Utiliser la commande `INCRBY` pour ajouter 5 à une clé numérique "KEY2", initialisée à 2
```bash
SET KEY2 2
INCRBY KEY2 5
```
- **Explication** : `INCRBY` permet d'incrémenter la valeur d'une clé numérique par un nombre donné. La première commande initialise `KEY2` avec une valeur de 2, et la seconde commande ajoute 5 à cette valeur, faisant passer `KEY2` à 7.

#### 3. Utiliser la commande `BLMOVE` pour démontrer l'opération de rotation de liste
```bash
LPUSH mylist1 "A" "B" "C"
BLMOVE mylist1 mylist2 LEFT RIGHT
LRANGE mylist2 0 -1
```
- **Explication** : La commande `LPUSH` insère les éléments "A", "B", et "C" dans `mylist1`. `BLMOVE` déplace un élément de `mylist1` (de la gauche) à `mylist2` (à droite). Cela permet de transférer des éléments d’une liste à une autre. `LRANGE` affiche tous les éléments de `mylist2` après l'opération.

#### 4. Utiliser des ensembles triés pour modéliser un système de classement des joueurs
```bash
ZADD rankings 100 "player1" 200 "player2" 150 "player3"
ZRANGE rankings 0 -1 WITHSCORES
```
- **Explication** : `ZADD` ajoute des joueurs avec leurs scores dans un ensemble trié (`rankings`). Le score détermine l'ordre des éléments. `ZRANGE` affiche tous les joueurs dans l'ordre de leur score (du plus bas au plus haut). L'option `WITHSCORES` permet de voir le score associé à chaque joueur.

#### 5. Utiliser un Hash pour stocker des informations sur un livre
```bash
HSET book:title "1984"
HSET book:author "George Orwell"
HSET book:publisher "Secker & Warburg"
HSET book:year 1949
HGETALL book
```
- **Explication** : Les commandes `HSET` stockent des paires clé-valeur dans un hachage (`book`). Chaque champ (`title`, `author`, etc.) contient des informations sur le livre. `HGETALL` récupère toutes les informations stockées dans le hachage.

#### 6. Définir un délai d'expiration sur une clé avec `EXPIRE`
```bash
SET tempkey "temporary value"
EXPIRE tempkey 60
```
- **Explication** : `EXPIRE` définit un délai d'expiration en secondes pour la clé `tempkey`. Ici, la clé sera supprimée automatiquement après 60 secondes.

#### 7. Utiliser la commande `MOVE` pour déplacer une clé vers une autre base de données
```bash
SET mykey "value"
MOVE mykey 1
```
- **Explication** : Redis permet de travailler avec plusieurs bases de données dans une même instance. `MOVE` déplace la clé `mykey` vers la base de données indexée 1 (par défaut, Redis travaille dans la base 0).

#### 8. Utiliser la commande `MSET` pour définir des valeurs pour plusieurs clés à la fois
```bash
MSET key1 "value1" key2 "value2" key3 "value3"
```
- **Explication** : `MSET` permet de définir plusieurs paires clé-valeur en une seule commande. Cela est plus efficace que d’utiliser plusieurs commandes `SET` distinctes.

#### 9. Utiliser les commandes `SUBSCRIBE` et `PUBLISH` pour démontrer Pub/Sub
```bash
SUBSCRIBE channel1
```
Dans une autre session Redis CLI :
```bash
PUBLISH channel1 "Hello from Redis!"
```
- **Explication** : Le modèle Pub/Sub (publication/abonnement) permet d’envoyer des messages à plusieurs clients abonnés à un canal spécifique. `SUBSCRIBE` s'abonne à un canal (`channel1`), et `PUBLISH` publie un message sur ce canal.

#### 10. Créer une base de données d'informations sur les étudiants
```bash
HMSET student:100 name "John Doe" address "123 Main St" dob "2000-01-01" courses "Math,Science"
HGETALL student:100
```
- **Explication** : Ici, un hachage est utilisé pour stocker les informations sur un étudiant avec l'ID 100. La commande `HMSET` permet de définir plusieurs champs dans le hachage (`name`, `address`, etc.).

#### 11. Utiliser `WATCH` pour démarrer une transaction optimiste
```bash
WATCH student:100
MULTI
HSET student:100 name "John Smith"
EXEC
```
- **Explication** : `WATCH` permet de surveiller une clé avant une transaction. Si la clé est modifiée par un autre client pendant la transaction, celle-ci échoue. `MULTI` démarre la transaction, et `EXEC` exécute toutes les commandes dans la transaction.

#### 12. Utiliser `HSET` et `HGET` pour manipuler les données des étudiants
```bash
HSET student:101 name "Jane Doe" address "456 Elm St"
HGET student:101 name
```
- **Explication** : `HSET` permet de définir des champs dans un hachage, et `HGET` récupère la valeur d'un champ spécifique.

---

### **Partie II - Transactions et scripting Lua**

#### 1. Utiliser les commandes `MULTI`, `EXEC` et `DISCARD`
```bash
MULTI
SET key1 "value1"
SET key2 "value2"
EXEC
```
- **Explication** : `MULTI` démarre une transaction, dans laquelle plusieurs commandes peuvent être regroupées. `EXEC` exécute toutes les commandes. Si une erreur survient, la transaction entière échoue. `DISCARD` annule une transaction non exécutée.

#### 2. Scripts Lua pour Redis

1. **Opérations arithmétiques de base** (ajout de deux nombres) :
```lua
return ARGV[1] + ARGV[2]
```
Appeler avec :
```bash
EVAL "return ARGV[1] + ARGV[2]" 0 5 3
```

2. **Manipuler des structures de données Redis** (ajouter à une liste et récupérer la taille) :
```lua
redis.call("LPUSH", KEYS[1], ARGV[1])
return redis.call("LLEN", KEYS[1])
```
Appeler avec :
```bash
EVAL "redis.call('LPUSH', KEYS[1], ARGV[1]); return redis.call('LLEN', KEYS[1])" 1 mylist "element"
```

3. **Combiner plusieurs commandes Redis** (ajouter un joueur et récupérer le classement) :
```lua
redis.call("ZADD", KEYS[1], ARGV[1], ARGV[2])
return redis.call("ZRANK", KEYS[1], ARGV[2])
```
Appeler avec :
```bash
EVAL "redis.call('ZADD', KEYS[1], ARGV[1], ARGV[2]); return redis.call('ZRANK', KEYS[1], ARGV[2])" 1 rankings 300 "player4"
```

#### 3. Utiliser `EVAL` pour exécuter des scripts sur le serveur
```bash
EVAL "return ARGV[1] + ARGV[2]" 0 7 8
```
- **Explication** : La commande `EVAL` permet d'exécuter un script Lua directement sur le serveur Redis. L'avantage est que les opérations sont exécutées atomiquement et de manière plus performante que de faire plusieurs appels Redis.

#### 4. Explication du scripting côté serveur
Le scripting côté serveur avec Lua permet d'exécuter des commandes complexes directement sur le serveur Redis, ce qui peut améliorer la performance en réduisant le nombre de round-trips entre le client et le serveur. Cela est particulièrement utile pour des opérations complexes ou atomiques. Lua est intégré directement dans Redis, donc l'exécution des scripts est très rapide et évite le besoin d'un langage externe pour des calculs complexes.

---

Voilà une explication ultra détaillée pour chaque commande et concept !