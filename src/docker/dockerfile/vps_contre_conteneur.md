# Fonctionnement interne d’un conteneur Docker

[...retour menu sur docker](../sommaire.md)

---

Les conteneurs Docker offrent un environnement isolé pour exécuter des applications de manière légère et portable. Bien qu'ils partagent certaines similarités avec les serveurs traditionnels, comme un VPS (Virtual Private Server), leur gestion du réseau, des données et de l’administration système est très différente. Dans cette documentation, nous allons explorer en profondeur le fonctionnement interne d'un conteneur Docker, ainsi que ses différences avec un serveur VPS, en mettant l'accent sur la gestion du réseau, la persistance des données, et ce qu'il se passe lorsqu'un conteneur est arrêté ou supprimé.

## 1. Le système de fichiers à l’intérieur d’un conteneur

### 1.1. Structure du système de fichiers

Le système de fichiers d'un conteneur Docker est isolé du système d'exploitation hôte. Il est constitué de **couches immuables** provenant de l'image Docker, avec une **couche en lecture/écriture** où toutes les modifications effectuées durant l'exécution du conteneur sont stockées. Ce système est similaire à un environnement Linux traditionnel, avec des répertoires comme `/bin`, `/usr`, `/etc`, et `/var`. Cependant, cette isolation signifie que toutes les modifications sont locales au conteneur et ne modifient pas le système d'exploitation de l'hôte.

#### Exemple d'arborescence :
* / : Le répertoire racine du conteneur.
* /bin : Les binaires système (shell, bash, etc.).
* /usr : Applications installées et leurs dépendances.
* /var : Répertoire pour les logs, caches, et autres données variables.
* /etc : Fichiers de configuration du système.

### 1.2. Persistance des fichiers et des données

Contrairement à un serveur traditionnel (VPS), où les données sont toujours persistantes sur le disque, un conteneur Docker ne conserve ses modifications qu’en mémoire temporaire, dans une couche en lecture/écriture. Lorsque le conteneur est arrêté, ces modifications sont toujours présentes, mais si le conteneur est supprimé, toutes ces modifications sont perdues **à moins que** des volumes Docker ne soient utilisés pour persister les données.

#### Comparaison avec un VPS :
- **Sur un VPS** : Les modifications des fichiers sont persistantes sur le disque, même si le serveur est redémarré ou mis hors ligne. Le système de fichiers est toujours disponible.
- **Sur un conteneur Docker** : Les modifications ne sont persistantes que si des volumes sont utilisés. Si un conteneur est supprimé, tout ce qui n’a pas été sauvegardé dans un volume est perdu.

---

## 2. Réseau et administration système

### 2.1. Gestion des ports et des interfaces réseau

Dans Docker, chaque conteneur possède sa propre interface réseau isolée, qui fonctionne de manière similaire à un serveur Linux. Cependant, les ports d’un conteneur ne sont pas accessibles depuis l’extérieur tant que vous ne les **mappez pas explicitement** avec les ports de l’hôte. Par défaut, Docker utilise un réseau de type **bridge** (pont), mais vous pouvez configurer d'autres réseaux, comme le **mode host** ou des réseaux personnalisés.

#### Mappage des ports

Lorsque vous démarrez un conteneur, vous pouvez mapper ses ports internes sur ceux de l'hôte pour qu'ils soient accessibles de l’extérieur. Cette étape est cruciale pour rendre les services exécutés dans le conteneur disponibles.

**Exemple de mappage de ports :**
```bash
docker run -p 8080:80 myapp
```
Ici, le port 80 du conteneur est mappé sur le port 8080 de l'hôte. Les requêtes faites sur `localhost:8080` seront redirigées vers le port 80 à l'intérieur du conteneur.

#### Comparaison avec un VPS :
- **Sur un VPS** : Tous les ports sont exposés et accessibles via l'adresse IP du serveur, à moins qu’ils ne soient bloqués par un pare-feu (comme `iptables` ou `ufw`).
- **Sur un conteneur Docker** : Les ports sont isolés et n’accèdent pas directement à l’extérieur. Vous devez explicitement spécifier les ports que vous souhaitez exposer en utilisant l’option `-p`.

### 2.2. Communication entre conteneurs

Docker permet la communication entre conteneurs à l’aide de réseaux internes. Les conteneurs peuvent communiquer en utilisant leur nom comme adresse. Cette architecture facilite les **applications multi-conteneurs**, où chaque service est isolé dans son propre conteneur mais reste interconnecté avec d’autres services.

**Exemple** : Un conteneur exécutant un serveur web peut se connecter à un autre conteneur exécutant une base de données sans exposer les ports au monde extérieur.

#### Comparaison avec un VPS :
- **Sur un VPS** : Tous les services tournant sur le VPS partagent la même interface réseau et les mêmes ports. La gestion de la communication entre services doit être faite manuellement via des configurations réseau ou des proxies.
- **Sur un conteneur Docker** : La gestion des réseaux et des communications entre services est plus fine et isolée. Chaque conteneur peut avoir sa propre interface réseau sans affecter l’hôte ou d’autres conteneurs.

---

## 3. Ce qu’il se passe quand un conteneur est à l’arrêt

### 3.1. Conteneur à l'arrêt

Lorsqu’un conteneur Docker est **arrêté**, son système de fichiers et ses modifications sont conservés. Les processus à l’intérieur du conteneur sont terminés, mais les données sont toujours accessibles et le conteneur peut être redémarré avec son état inchangé.

**Commande pour arrêter un conteneur** :
```bash
docker stop <nom_du_conteneur>
```
Un conteneur arrêté est inactif, ne consomme plus de ressources système (CPU, mémoire), mais ses fichiers et configurations sont conservés dans la couche en lecture/écriture.

**Commande pour redémarrer un conteneur** :
```bash
docker start <nom_du_conteneur>
```
Cela relance le conteneur avec l’ensemble de ses modifications précédentes intactes.

#### Comparaison avec un VPS arrêté :
- **Sur un VPS** : Quand un VPS est arrêté, toutes ses données et ses fichiers sont toujours sur le disque dur. Lorsqu’il est redémarré, le serveur reprend son exécution avec le même état de fichiers, processus et configurations.
- **Sur un conteneur Docker** : Un conteneur arrêté conserve également ses fichiers et configurations, mais il est plus facile à redémarrer rapidement et consomme moins de ressources. Un conteneur Docker n’a pas d’overhead lié à un système d’exploitation complet.

### 3.2. Suppression d'un conteneur

Si vous **supprimez** un conteneur Docker (avec `docker rm`), toutes les modifications qui n'ont pas été sauvegardées dans des volumes sont **perdues**. Seule l'image de base reste disponible.

#### Comparaison avec un VPS supprimé :
- **Sur un VPS** : Si un VPS est supprimé (c’est-à-dire que sa machine virtuelle est détruite), toutes ses données sont également perdues, sauf si elles sont sauvegardées sur un autre disque ou un service de stockage.
- **Sur un conteneur Docker** : La suppression d’un conteneur est similaire, mais Docker permet de relancer une image très rapidement. Si vous avez utilisé des volumes pour les données critiques, ces données ne sont pas perdues.

---

## 4. Gestion des volumes et persistance des données

### 4.1. Notion de montage

Un volume Docker est une méthode permettant de **persister des données** en dehors du cycle de vie d’un conteneur. Lorsque vous montez un volume, vous créez un lien entre un répertoire de l’hôte et un répertoire à l'intérieur du conteneur. Les données dans ce volume sont **partagées en temps réel** entre l’hôte et le conteneur.

#### Fonctionnement du montage en temps réel

Quand vous montez un volume, toute modification faite dans le répertoire du conteneur est instantanément visible sur l'hôte, et inversement. Cela permet de persister des données au-delà de la durée de vie du conteneur, ce qui est essentiel pour les bases de données ou d’autres données critiques.

**Exemple de montage** :
```bash
docker run -v /chemin/sur/hote:/data myapp
```
Dans cet exemple :
- Tout fichier créé dans `/data` dans le conteneur sera visible instantanément dans `/chemin/sur/hote` sur l’hôte.
- Toute modification faite sur le système hôte dans `/chemin/sur/hote` sera immédiatement reflétée dans le conteneur.

#### Pas une copie : 
Il ne s'agit pas d'une simple copie des fichiers, mais d'un véritable **montage en temps réel**. Les modifications apportées dans le répertoire du conteneur ou sur l'hôte sont instantanément partagées.

### 4.2. Partage entre plusieurs conteneurs

Un volume peut être monté sur plusieurs conteneurs simultanément. Cela permet de partager des données entre plusieurs services, comme une application web accédant aux mêmes fichiers qu’un service de traitement de données dans un autre conteneur.

**Exemple** :
```bash
docker run -v /chemin/sur/hote:/data conteneur1
docker run -v /chemin/sur/hote:/data conteneur2
```
Les deux conteneurs accèdent et modifient les fichiers dans `/data`, de manière synchronisée avec l’hôte.

### 4.3. Différences avec un VPS

Sur un VPS, tout ce qui est écrit sur le disque est persisté

 par défaut, sans besoin de configuration supplémentaire. Sur Docker, il faut **explicitement monter des volumes** pour s’assurer que les données sont sauvegardées en dehors du conteneur.

- **VPS** : Toutes les modifications sur le système de fichiers sont persistantes.
- **Docker** : Les modifications ne sont persistées que dans les volumes. Si un conteneur est supprimé, seules les données des volumes seront conservées.

---

## 5. Conteneur à l’arrêt vs VPS

### 5.1. Similitudes

- **Conservation des données** : Que ce soit sur un VPS ou un conteneur Docker, arrêter l’instance ne signifie pas perdre les données. Les fichiers et configurations restent intacts.
- **Redémarrage** : Un VPS et un conteneur arrêté peuvent être redémarrés avec leur état précédent.

### 5.2. Différences

- **Poids et overhead** :
  - **VPS** : Un VPS consomme plus de ressources, car il inclut un système d’exploitation complet.
  - **Docker** : Un conteneur Docker est plus léger, car il partage le noyau de l’hôte et n’a pas besoin d’un système d’exploitation complet.

- **Redémarrage** :
  - **VPS** : Le redémarrage d’un VPS peut être plus lent, car le système d’exploitation doit être initialisé.
  - **Docker** : Relancer un conteneur est instantané, car seule l'application est redémarrée.

- **Persistance des modifications** :
  - **VPS** : Toute modification faite sur un VPS est persistante par défaut.
  - **Docker** : Sans volumes, les modifications dans un conteneur ne sont pas persistées si celui-ci est supprimé.

---

## Conclusion

Docker offre une approche différente de l'isolation et de la gestion des ressources par rapport à un VPS traditionnel. Bien qu’un conteneur à l’arrêt conserve ses fichiers et configurations, tout comme un VPS, la persistance des données dépend de la gestion explicite des volumes. L’isolation réseau dans Docker est plus stricte et nécessite une configuration spécifique pour rendre des ports accessibles, tandis que sur un VPS, les services sont exposés directement. Docker est idéal pour des environnements légers et éphémères, tandis que les VPS sont mieux adaptés pour des environnements plus persistants et autonomes, avec un accès plus direct aux ressources matérielles.

--- 

[...retour menu sur docker](../sommaire.md)