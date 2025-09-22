# Théorème CAP

[...retour en arriere](../menu.md)

---

Le **théorème CAP** est un concept fondamental dans le domaine des bases de données distribuées, introduit par le chercheur Eric Brewer en 2000. Il décrit les compromis inévitables entre trois caractéristiques essentielles d'un système distribué : **C**oherence (Consistency), **A**vailability (Disponibilité) et **P**artition tolerance (Tolérance aux partitions). Selon ce théorème, il est impossible pour un système de garantir simultanément ces trois propriétés. Un système distribué peut au maximum offrir deux de ces propriétés, mais jamais les trois en même temps.

### 1. **Définitions des termes du théorème CAP**
- **C - Cohérence (Consistency)** :  
  Cela signifie que chaque lecture (lecture de données) reçoit les données les plus récentes ou à jour. Dans un système cohérent, tous les nœuds du système distribué voient les mêmes données au même moment. Si une mise à jour est effectuée sur un nœud, toutes les lectures ultérieures (sur n’importe quel nœud) retourneront la même valeur mise à jour.
  
- **A - Disponibilité (Availability)** :  
  Disponibilité signifie que chaque requête envoyée au système reçoit une réponse (même si c'est un message d'erreur). Cela veut dire que même en cas de défaillance d'une partie du système, il est possible de répondre aux requêtes. En d'autres termes, le système reste opérationnel et peut traiter les demandes même si certains nœuds sont en panne.

- **P - Tolérance aux partitions (Partition tolerance)** :  
  La tolérance aux partitions fait référence à la capacité du système à continuer de fonctionner correctement même si des parties du réseau sont coupées ou ne peuvent plus communiquer entre elles. Dans un système tolérant aux partitions, les communications entre nœuds peuvent échouer ou être retardées, mais le système doit continuer à fonctionner et accepter les opérations.

### 2. **Les compromis du théorème CAP**
Le théorème CAP stipule qu'il est impossible pour une base de données distribuée de garantir simultanément les trois propriétés (Cohérence, Disponibilité et Tolérance aux partitions). Par conséquent, un système distribué doit faire un compromis entre ces propriétés. Voici les combinaisons possibles :

#### a) **CP - Cohérence et Tolérance aux partitions**  
Un système qui garantit la **cohérence** et la **tolérance aux partitions** privilégie la cohérence des données en cas de panne ou de partition réseau. Cela signifie que, lorsqu'une partition réseau se produit, certaines requêtes peuvent ne pas obtenir de réponse jusqu'à ce que la partition soit résolue pour garantir que toutes les données restent cohérentes. Autrement dit, la disponibilité est sacrifiée temporairement.

Exemples :  
- **MongoDB** en mode strict
- **HBase**

#### b) **AP - Disponibilité et Tolérance aux partitions**  
Un système qui garantit la **disponibilité** et la **tolérance aux partitions** va répondre aux requêtes même en cas de panne réseau ou de partition. Cependant, pour garantir cette disponibilité, il peut sacrifier la cohérence des données, ce qui signifie que les données peuvent être désynchronisées pendant un certain temps. Une application peut donc lire des données qui ne sont pas à jour.

Exemples :  
- **Cassandra**
- **DynamoDB**

#### c) **CA - Cohérence et Disponibilité**

Un système qui garantit la **cohérence** et la **disponibilité** fonctionne correctement tant qu'il n'y a pas de problème de communication (partitionnement) entre les différentes parties du réseau. Dans ce cas, chaque lecture de données sera cohérente et à jour, et toutes les requêtes recevront une réponse. Cependant, en cas de **partition réseau**, le système ne pourra plus garantir la tolérance aux partitions. Autrement dit, le système peut choisir de devenir temporairement **indisponible** pour préserver la cohérence des données.

Cela signifie que lorsqu'une défaillance ou une partition survient, certaines parties du système peuvent devenir inaccessibles (ou cesser de répondre aux requêtes) afin de garantir que les données restent cohérentes. Le compromis ici est que la **disponibilité** n'est pas assurée en cas de partition, mais la cohérence est toujours maintenue.

#### Exemples :
- **MySQL** ou **PostgreSQL** en configuration non distribuée.
- Les systèmes relationnels traditionnels, lorsque déployés dans un environnement non distribué ou avec des partitions strictement évitées, peuvent offrir une cohérence et une disponibilité tant que le réseau reste fonctionnel.

### 3. **Résumé des compromis du théorème CAP**

Le théorème CAP décrit le fait qu’un système distribué doit choisir deux des trois propriétés suivantes :
- **Cohérence** (C)
- **Disponibilité** (A)
- **Tolérance aux partitions** (P)

Voici un résumé des compromis possibles dans les systèmes distribués :

- **CP (Cohérence et Tolérance aux partitions)** : Le système privilégie la cohérence des données en cas de partition, mais sacrifie la disponibilité pendant ce temps.
- **AP (Disponibilité et Tolérance aux partitions)** : Le système continue de répondre aux requêtes en cas de partition, mais peut sacrifier la cohérence des données, ce qui peut entraîner des lectures de données non synchronisées.
- **CA (Cohérence et Disponibilité)** : Le système garantit à la fois la cohérence et la disponibilité tant que le réseau fonctionne correctement, mais il devient vulnérable en cas de partition, car il ne peut tolérer les interruptions de communication.

### 4. **Conclusion sur le théorème CAP**

Le théorème CAP est un cadre théorique qui aide les architectes de systèmes distribués à comprendre les compromis à faire lors de la conception de leurs systèmes. Le choix du compromis dépend des exigences spécifiques de l’application. Par exemple, si l’intégrité des données est prioritaire (comme dans les systèmes bancaires), la **cohérence** sera privilégiée. Si la disponibilité des services est essentielle (comme dans les systèmes de messagerie instantanée), la **disponibilité** sera plus importante, même au prix d’une légère incohérence des données.

Dans tous les cas, il est impossible de concevoir un système distribué qui garantisse simultanément la cohérence, la disponibilité et la tolérance aux partitions.

---

[...retour en arriere](../menu.md)