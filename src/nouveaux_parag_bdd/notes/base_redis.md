# Les bases de Redis

[...retour en arriere](../menu.md)

---

Bien sûr ! Voici une explication détaillée de Redis, en utilisant la structure en titres que tu as demandée.

## Redis : Qu'est-ce que c'est ?

Redis (REmote DIctionary Server) est une base de données NoSQL de type **clé-valeur** qui fonctionne en mémoire, ce qui la rend extrêmement rapide pour la gestion de données. Elle a été créée par Salvatore Sanfilippo en 2009. Redis est principalement utilisé comme **cache**, **file d'attente**, ou pour des opérations nécessitant un traitement rapide des données.

### Principales caractéristiques de Redis

#### 1. **Stockage en mémoire**
Redis stocke toutes les données en mémoire (RAM), contrairement aux bases de données traditionnelles qui utilisent le stockage sur disque. Cela permet une vitesse de lecture et d’écriture très rapide. Redis offre cependant une persistance optionnelle via des snapshots ou un log des opérations.

#### 2. **Modèle clé-valeur**
Redis utilise un modèle simple clé-valeur, où chaque clé est unique, et la valeur associée peut être de différents types de données. Ce modèle est flexible et efficace pour stocker différents types d’informations.

#### 3. **Types de données avancés**
Redis supporte plusieurs types de données complexes, pas seulement des chaînes de caractères comme certaines autres bases de données NoSQL. Voici quelques types de données disponibles :
- **String** : La base, une simple chaîne de texte.
- **List** : Une liste de chaînes de caractères, triée par ordre d'insertion.
- **Set** : Un ensemble non ordonné de chaînes uniques.
- **Hash** : Une structure similaire à un dictionnaire avec des paires clé-valeur.
- **Sorted Set** : Un ensemble de chaînes de caractères, mais avec un score numérique, permettant de trier les éléments.
- **HyperLogLog** : Une structure probabiliste pour estimer des cardinalités.
- **Stream** : Un log d'événements.

#### 4. **Persistance optionnelle**
Bien que Redis fonctionne principalement en mémoire, il est possible de rendre les données persistantes sur disque, via deux méthodes :
- **RDB (Redis Database Backup)** : Crée des snapshots de la base de données à des intervalles définis.
- **AOF (Append-Only File)** : Enregistre chaque écriture dans un fichier de log qui peut être rejoué pour restaurer l’état de la base.

#### 5. **Replication et haute disponibilité**
Redis supporte la réplication maître-esclave, permettant de répliquer les données sur plusieurs serveurs pour assurer une disponibilité même en cas de panne. Avec Redis Sentinel, on peut gérer automatiquement la bascule des maîtres en cas de défaillance.

#### 6. **Transactions**
Redis supporte des transactions via le mécanisme de **MULTI**, permettant d’exécuter plusieurs commandes de manière atomique.

#### 7. **Lua Scripting**
Redis permet d’exécuter des scripts **Lua** directement sur le serveur. Cela réduit le nombre de round-trips nécessaires entre le client et le serveur, ce qui améliore les performances pour certaines opérations complexes.

### 8. **Cluster Redis**
Redis peut être utilisé en mode cluster pour répartir les données sur plusieurs nœuds, offrant ainsi une meilleure scalabilité.

### Usages principaux de Redis

#### 1. **Cache**
Redis est fréquemment utilisé comme cache en raison de sa vitesse. On peut y stocker des données fréquemment consultées afin de réduire la charge des systèmes de bases de données plus lents.

#### 2. **Sessions**
Redis est souvent utilisé pour stocker des sessions utilisateur dans des applications web. La rapidité d'accès aux données et la gestion facile de l'expiration (TTL) des clés en font une solution idéale pour ce type de besoin.

#### 3. **File d'attente (queues)**
Grâce aux types de données comme les **listes**, Redis est utilisé pour implémenter des systèmes de file d’attente, où des tâches ou messages peuvent être traités de manière asynchrone.

#### 4. **Comptage et statistiques en temps réel**
Avec ses commandes spécifiques comme **INCR** et ses structures comme **HyperLogLog**, Redis est idéal pour effectuer des opérations de comptage rapide, comme les vues de page, le nombre de clics, etc.

#### 5. **Système de publication/abonnement (Pub/Sub)**
Redis fournit un système simple de publication et d’abonnement, permettant à des applications de diffuser des messages à plusieurs clients abonnés à un canal spécifique.

#### 6. **Stockage de données géospatiales**
Avec ses commandes géospatiales, Redis est aussi utilisé pour stocker et interroger des données de localisation (longitude et latitude) et effectuer des requêtes géospatiales.

---

# Commandes principales expliquées

## `SET` : Définir une clé avec une valeur

La commande `SET` permet de créer une nouvelle clé dans Redis et de lui assigner une valeur. Si la clé existe déjà, sa valeur sera remplacée.

### Syntaxe :
```
SET key value
```

### Exemple :
```shell
SET user:1000 "John Doe"
```

Cela crée la clé `user:1000` avec la valeur "John Doe".  
Vous pouvez vérifier la valeur de la clé avec :
```shell
GET user:1000
```

### Explication :
- **Clé** : Une chaîne unique utilisée pour identifier une valeur dans Redis.
- **Valeur** : Peut être une chaîne, un nombre, ou même un autre type de données.

---

## `INCRBY` : Incrémenter une clé numérique d'une valeur donnée

La commande `INCRBY` permet d’incrémenter une clé contenant un entier par un nombre spécifié. Si la clé n'existe pas, elle sera initialisée à zéro avant d’être incrémentée.

### Syntaxe :
```
INCRBY key increment
```

### Exemple :
```shell
SET page_views 10
INCRBY page_views 5
GET page_views
```

Résultat :
```
15
```

### Explication :
- **INCRBY** : Incrémente la clé d’une valeur entière.
- **Clé inexistante** : Si la clé n'existe pas, elle sera créée avec une valeur initiale de 0.

---

## `BLMOVE` : Rotation entre deux listes avec blocage

La commande `BLMOVE` permet de déplacer un élément d’une liste source vers une liste destination, avec une possibilité de blocage en attente si l'une des listes est vide.

### Syntaxe :
```
BLMOVE source destination LEFT|RIGHT LEFT|RIGHT timeout
```

### Exemple :
```shell
RPUSH queue1 "item1" "item2" "item3"
RPUSH queue2 "item4" "item5"

BLMOVE queue1 queue2 LEFT RIGHT 0
```

Ce code prend l’élément de gauche de `queue1` ("item1") et l’ajoute à droite de `queue2`.  
Résultat pour `queue2` :
```shell
LRANGE queue2 0 -1
```

```
["item4", "item5", "item1"]
```

### Explication :
- **LEFT|RIGHT** : Indique où prendre (gauche/droite) et où ajouter (gauche/droite) l'élément.
- **timeout** : Le temps en secondes à attendre si l'une des listes est vide.

---

## `ZADD` / `Sorted Sets` : Créer un système de classement avec des ensembles triés

La commande `ZADD` permet d'ajouter des éléments dans un ensemble trié, où chaque élément est associé à un score. Les ensembles triés sont souvent utilisés pour créer des systèmes de classement (ranking).

### Syntaxe :
```
ZADD key score member
```

### Exemple :
```shell
ZADD leaderboard 100 "Player1"
ZADD leaderboard 150 "Player2"
ZADD leaderboard 120 "Player3"
ZRANGE leaderboard 0 -1 WITHSCORES
```

Résultat :
```
1) "Player1"
2) "100"
3) "Player3"
4) "120"
5) "Player2"
6) "150"
```

### Explication :
- **ZADD** : Ajoute un élément à l'ensemble avec un score.
- **ZRANGE** : Retourne les éléments dans l'ordre croissant des scores.

### Cas d'utilisation :
- Créer des classements de joueurs pour un jeu en ligne, triés par score.

---

## `HSET` / `Hash` : Stocker des informations dans une structure de hachage

La commande `HSET` permet de stocker des paires clé-valeur dans un hachage, un peu comme une table ou un dictionnaire.

### Syntaxe :
```
HSET key field value
```

### Exemple :
```shell
HSET book:1 title "Redis Essentials"
HSET book:1 author "John Doe"
HGETALL book:1
```

Résultat :
```
1) "title"
2) "Redis Essentials"
3) "author"
4) "John Doe"
```

### Explication :
- **HSET** : Stocke une paire clé-valeur dans un hachage.
- **HGETALL** : Récupère toutes les paires clé-valeur d’un hachage.

### Cas d'utilisation :
- Stocker des informations détaillées sur des objets complexes, comme un livre.

---

## `EXPIRE` : Définir un délai d’expiration sur une clé

La commande `EXPIRE` définit une durée de vie sur une clé, après laquelle la clé sera supprimée automatiquement.

### Syntaxe :
```
EXPIRE key seconds
```

### Exemple :
```shell
SET session:123 "active"
EXPIRE session:123 60
TTL session:123
```

Résultat :
```
(integer) 60
```

Après 60 secondes, la clé `session:123` sera supprimée.

### Explication :
- **EXPIRE** : Définir une durée de vie en secondes sur une clé.
- **TTL** : Récupérer le temps restant avant l’expiration de la clé.

### Cas d'utilisation :
- Gérer les sessions utilisateur avec un temps de vie limité.


## `MOVE` : Déplacer une clé vers une autre base de données

La commande `MOVE` permet de déplacer une clé d'une base de données à une autre dans Redis. Cela est utile si vous souhaitez organiser vos données dans différentes bases de données, disponibles sur le même serveur Redis.

### Syntaxe :
```
MOVE key db
```

### Exemple :
```shell
SET mykey "some value"
MOVE mykey 1
SELECT 1
GET mykey
```

Résultat :
```
"some value"
```

### Explication :
- **MOVE** : Déplace la clé `mykey` de la base de données actuelle (par défaut `0`) vers la base de données `1`.
- **SELECT** : Permet de changer de base de données.

### Cas d'utilisation :
- Organiser vos données dans différentes bases de données Redis selon des catégories ou des priorités.

---

## `MSET` : Définir plusieurs clés à la fois

La commande `MSET` permet de définir plusieurs paires clé-valeur en une seule commande, ce qui est plus efficace que de faire plusieurs `SET` séparés.

### Syntaxe :
```
MSET key1 value1 key2 value2 ...
```

### Exemple :
```shell
MSET key1 "value1" key2 "value2" key3 "value3"
GET key1
GET key2
GET key3
```

Résultat :
```
"value1"
"value2"
"value3"
```

### Explication :
- **MSET** : Associe plusieurs clés avec des valeurs en une seule commande.

### Cas d'utilisation :
- Pour initialiser rapidement plusieurs variables ou informations à la fois.

---

## `SUBSCRIBE` / `PUBLISH` : Implémenter le système Pub/Sub (publication/abonnement)

Redis supporte le modèle de publication et d'abonnement (Pub/Sub) où les clients peuvent s'abonner à des canaux spécifiques, et d'autres clients peuvent publier des messages sur ces canaux. Tous les abonnés recevront les messages publiés.

### Syntaxe :
- **Pour publier un message** :
```
PUBLISH channel message
```

- **Pour s'abonner à un canal** :
```
SUBSCRIBE channel
```

### Exemple :
1. Abonnez-vous à un canal dans un premier terminal :
```shell
SUBSCRIBE mychannel
```

2. Publiez un message sur ce canal dans un deuxième terminal :
```shell
PUBLISH mychannel "Hello, Redis!"
```

Résultat dans le premier terminal :
```
1) "message"
2) "mychannel"
3) "Hello, Redis!"
```

### Explication :
- **SUBSCRIBE** : Permet de s'abonner à un canal spécifique pour recevoir les messages publiés sur ce canal.
- **PUBLISH** : Publie un message sur un canal, qui sera reçu par tous les abonnés.

### Cas d'utilisation :
- Systèmes de messagerie en temps réel, notifications push, chat en ligne, etc.

---

## `Hash` / `List` : Créer une base de données avec des informations sur les étudiants

Les `Hash` et `List` sont des structures de données utilisées pour stocker des informations plus complexes. Par exemple, on peut créer une base de données pour des étudiants où chaque étudiant a des informations détaillées (nom, adresse, etc.) et une liste de cours suivis.

### Hash - Stocker des informations sur les étudiants :
```
HSET student:1000 name "Alice" age 22 address "123 Main St"
HGETALL student:1000
```

Résultat :
```
1) "name"
2) "Alice"
3) "age"
4) "22"
5) "address"
6) "123 Main St"
```

### List - Stocker la liste des cours suivis :
```
LPUSH courses:1000 "Math" "Science" "History"
LRANGE courses:1000 0 -1
```

Résultat :
```
1) "History"
2) "Science"
3) "Math"
```

### Explication :
- **HSET** : Ajoute des champs à un hachage, où chaque champ représente une information sur l'étudiant.
- **LPUSH** : Ajoute des éléments à une liste, ici représentant les cours suivis par l'étudiant.
- **LRANGE** : Récupère tous les éléments d'une liste.

### Cas d'utilisation :
- Créer des bases de données structurées, comme des systèmes de gestion des étudiants, des employés, etc.

---

## `WATCH` : Commencer une transaction optimiste

La commande `WATCH` est utilisée pour observer des clés dans Redis. Si l'une des clés surveillées est modifiée avant que la transaction ne soit exécutée, celle-ci sera annulée. Cela permet de gérer les conflits dans des environnements multi-utilisateurs.

### Syntaxe :
```
WATCH key1 key2 ...
MULTI
...  # série de commandes
EXEC
```

### Exemple :
```shell
WATCH balance:1000
GET balance:1000   # suppose que la balance est à 100

MULTI
DECRBY balance:1000 50
EXEC
```

Résultat :
```
(integer) 50
```

Si une autre opération modifie `balance:1000` avant l'exécution du `EXEC`, la transaction échouera.

### Explication :
- **WATCH** : Surveille les clés pour détecter des modifications avant l'exécution de la transaction.
- **MULTI/EXEC** : Exécute une série de commandes dans une transaction atomique.

### Cas d'utilisation :
- Gestion de transactions optimistes dans des environnements avec concurrence, comme pour des systèmes de paiement.

---

## `HSET` / `HGET` : Manipuler des données dans un hachage

La commande `HSET` permet de définir des champs spécifiques dans un hachage, tandis que `HGET` permet de récupérer la valeur d'un champ spécifique.

### Syntaxe :
- **Ajouter ou modifier des champs** :
```
HSET key field value
```

- **Récupérer un champ spécifique** :
```
HGET key field
```

### Exemple :
```shell
HSET user:2000 name "Bob" email "bob@example.com"
HGET user:2000 name
HGETALL user:2000
```

Résultat pour `HGET user:2000 name` :
```
"Bob"
```

Résultat pour `HGETALL user:2000` :
```
1) "name"
2) "Bob"
3) "email"
4) "bob@example.com"
```

### Explication :
- **HSET** : Ajoute ou met à jour un champ dans un hachage.
- **HGET** : Récupère la valeur d’un champ spécifique dans un hachage.

### Cas d'utilisation :
- Stocker et accéder rapidement à des informations structurées, telles que les profils utilisateur.

---

## `MULTI` / `EXEC` / `DISCARD` : Effectuer des transactions Redis

### Transactions dans Redis

Les transactions Redis permettent d'exécuter plusieurs commandes de manière atomique. Cela signifie que toutes les commandes d'une transaction sont exécutées en une seule fois, sans être interrompues par d'autres commandes.

### Syntaxe :
1. **Démarrer une transaction** :
```
MULTI
```
2. **Exécuter la transaction** :
```
EXEC
```
3. **Annuler la transaction** :
```
DISCARD
```

### Exemple :
```shell
MULTI
SET user:1000 "John Doe"
INCRBY balance:1000 50
EXEC
```

Résultat :
```
OK
(integer) 50
```

Si une commande échoue au milieu d'une transaction, les autres commandes ne seront pas affectées.

### Explication :
- **MULTI** : Déclare une transaction dans Redis.
- **EXEC** : Exécute toutes les commandes de la transaction.
- **DISCARD** : Annule la transaction avant son exécution.

### Cas d'utilisation :
- Effectuer plusieurs mises à jour de données de manière atomique (ex : débiter un compte et créditer un autre).

---

## Scripting Lua : Écrire des scripts pour des opérations arithmétiques, de manipulation de données et autres

### Lua dans Redis

Redis permet d'exécuter des scripts en Lua, un langage de programmation léger et puissant. Les scripts Lua permettent d'exécuter plusieurs commandes Redis de manière atomique, comme dans une transaction, mais avec plus de flexibilité. Redis garantit que chaque script Lua est exécuté sans interruption.

### Syntaxe Lua dans Redis

Les scripts Lua s'exécutent dans Redis avec la commande `EVAL` :

```shell
EVAL "script" numkeys key [key ...] arg [arg ...]
```

- **script** : Le script Lua à exécuter.
- **numkeys** : Le nombre de clés que le script va utiliser.
- **key** : Les clés Redis sur lesquelles le script va agir.
- **arg** : Les arguments supplémentaires utilisés dans le script.

### Exemple simple : Opérations arithmétiques

Ce script Lua additionne deux valeurs stockées dans des clés Redis :

```lua
EVAL "return redis.call('INCRBY', KEYS[1], ARGV[1]) + redis.call('INCRBY', KEYS[2], ARGV[2])" 2 key1 key2 10 20
```

- **redis.call** : Exécute une commande Redis dans Lua.
- **KEYS[1]** et **KEYS[2]** : Référence les clés passées au script.
- **ARGV[1]** et **ARGV[2]** : Référence les arguments passés au script.

### Explication :
- Ce script incrémente les valeurs des clés `key1` et `key2` par `10` et `20` respectivement, puis retourne leur somme.

### Cas d'utilisation :
- Calculer des totaux, mettre à jour plusieurs clés de manière atomique.

---

## `EVAL` : Exécuter des scripts Lua sur le serveur

La commande `EVAL` permet d'exécuter un script Lua directement sur le serveur Redis. Cela permet de minimiser les aller-retours entre le client et le serveur, et de garantir que toutes les commandes dans le script sont exécutées de manière atomique.

### Syntaxe :
```
EVAL "script" numkeys key [key ...] arg [arg ...]
```

### Exemple :
Ce script récupère la valeur de deux clés, les additionne et renvoie le résultat :

```lua
EVAL "local val1 = redis.call('GET', KEYS[1]); local val2 = redis.call('GET', KEYS[2]); return tonumber(val1) + tonumber(val2);" 2 key1 key2
```

### Explication :
- **local val1 = redis.call('GET', KEYS[1])** : Récupère la valeur de la clé `key1`.
- **tonumber(val1)** : Convertit la valeur en nombre.
- **return** : Renvoie le résultat de l'addition des deux valeurs.

---

## Scripting côté serveur : Concept et optimisation des opérations Redis via des scripts Lua

### Pourquoi utiliser Lua avec Redis ?

Utiliser des scripts Lua dans Redis présente plusieurs avantages :
1. **Réduction des aller-retours** : Plutôt que d'envoyer plusieurs commandes depuis le client, un seul script Lua peut effectuer toutes les opérations sur le serveur Redis.
2. **Atomicité** : Toutes les opérations exécutées par un script Lua sont atomiques, ce qui signifie qu'elles seront exécutées en entier sans interruption.
3. **Optimisation des performances** : Lua permet d'écrire des scripts complexes sans nécessiter plusieurs requêtes entre le client et le serveur, ce qui accélère les performances.

### Exemple d'utilisation pratique :
Supposons que vous souhaitiez implémenter un système de retrait bancaire qui vérifie si un utilisateur a suffisamment de fonds avant d'effectuer le retrait. Voici un script Lua pour cela :

```lua
EVAL "
local balance = redis.call('GET', KEYS[1])
if tonumber(balance) >= tonumber(ARGV[1]) then
    redis.call('DECRBY', KEYS[1], ARGV[1])
    return balance - ARGV[1]
else
    return 'Insufficient funds'
end" 1 user:1000 50
```

Ce script :
- Récupère le solde de l'utilisateur.
- Vérifie si le solde est suffisant.
- Si oui, diminue le solde du montant spécifié et renvoie le nouveau solde.
- Si non, renvoie un message indiquant que les fonds sont insuffisants.

---

## Ce qu'il faut savoir sur Lua

### Introduction à Lua

Lua est un langage de script léger conçu pour être rapide et facile à intégrer dans d'autres applications (comme Redis). Il est populaire pour sa simplicité et sa flexibilité. Voici quelques concepts de base à connaître pour travailler avec Lua dans Redis.

### Variables et types de données

En Lua, les variables sont créées simplement en les déclarant avec `local` (ou sans rien, mais `local` est recommandé) :
```lua
local x = 10
local name = "Alice"
```

Lua prend en charge les types de données suivants :
- **number** : Nombres (entiers et flottants).
- **string** : Chaînes de caractères.
- **boolean** : `true` ou `false`.
- **table** : Une structure de données clé-valeur similaire à un dictionnaire en Python.

### Tables en Lua

Les **tables** sont des structures de données très puissantes en Lua, utilisées pour stocker des listes ou des paires clé-valeur.

#### Exemple de table :
```lua
local student = {
    name = "John",
    age = 21,
    courses = {"Math", "Science"}
}
```

Accéder aux valeurs dans une table :
```lua
print(student.name)  -- "John"
print(student.courses[1])  -- "Math"
```

### Boucles et conditions

#### Condition `if` :
```lua
local x = 5
if x > 3 then
    print("x is greater than 3")
else
    print("x is less than or equal to 3")
end
```

#### Boucle `for` :
```lua
for i = 1, 10 do
    print(i)
end
```

### Fonctions

Les fonctions en Lua sont définies comme ceci :
```lua
local function add(a, b)
    return a + b
end

local result = add(3, 5)
print(result)  -- 8
```

### Utilisation des commandes Redis dans Lua

Pour exécuter une commande Redis dans un script Lua, vous utiliserez la fonction `redis.call`. Voici un exemple pour incrémenter une clé :
```lua
redis.call('INCRBY', 'counter', 10)
```

Vous pouvez combiner les fonctionnalités de Lua avec les commandes Redis pour écrire des scripts puissants et complexes.

---

[...retour en arriere](../menu.md)