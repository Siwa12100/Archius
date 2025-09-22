# Les volumes

[...retour au cours](./dockerFichier.md)

---

## 1. Un volume docker

Un volume dans Docker est un moyen de stocker des données en dehors du système de fichiers du conteneur, permettant ainsi de partager et de persister ces données entre les différents conteneurs. Les volumes sont indépendants de la durée de vie du conteneur, ce qui signifie qu'ils peuvent survivre à l'arrêt et au redémarrage d'un conteneur.

## 2. Pourquoi utiliser des volumes ?

- **Persistance des données :** Les volumes permettent de stocker des données de manière persistante, même lorsque les conteneurs associés sont arrêtés ou supprimés. Cela est crucial pour les applications nécessitant la conservation de données au fil du temps.

- **Partage de données :** Les volumes facilitent le partage de données entre différents conteneurs d'une même application. Cela peut être utile pour partager des fichiers de configuration, des bases de données, des journaux, etc.

- **Séparation des préoccupations :** En utilisant des volumes, on peut séparer les données de l'application elle-même, ce qui rend la gestion des données plus flexible et indépendante du cycle de vie des conteneurs.

## Création et gestion des volumes

### Créer un volume

Pour créer un volume, on peut utiliser la commande `docker volume create` :

```bash
docker volume create monVolume
```

### Lister les volumes existants

On peut afficher la liste des volumes existants avec la commande `docker volume ls` :

```bash
docker volume ls
```

### Supprimer un volume

Si vous n'avez plus besoin d'un volume, vous pouvez le supprimer avec la commande `docker volume rm` :

```bash
docker volume rm monVolume
```

## Utilisation des volumes avec les conteneurs

### Montage d'un volume dans un conteneur

Pour utiliser un volume dans un conteneur, vous pouvez spécifier le volume lors du lancement du conteneur à l'aide de l'option `-v` ou `--mount` :

```bash
docker run -v monVolume:/chemin/dans/conteneur imageName
```

ou avec `--mount` :

```bash
docker run --mount src=monVolume,dst=/chemin/dans/conteneur imageName
```

### Spécifier des permissions de montage

Vous pouvez spécifier des permissions de montage pour un volume. Par exemple, pour monter un volume en lecture seule, vous pouvez utiliser :

```bash
docker run -v monVolume:/chemin/dans/conteneur:ro imageName
```

## Persistance des volumes

Les volumes sont persistants même après l'arrêt ou la suppression du conteneur auquel ils sont attachés. Ils doivent être explicitement supprimés à l'aide de la commande `docker volume rm` s'ils ne sont plus nécessaires.

## Volumes anonymes et nommés

- **Volumes anonymes :** Si vous ne spécifiez pas de nom lors de la création du volume, Docker lui attribue un nom unique. Par exemple, `docker volume create` sans argument crée un volume anonyme.

- **Volumes nommés :** Vous pouvez spécifier un nom lors de la création du volume pour créer un volume nommé. Par exemple, `docker volume create monVolume` crée un volume nommé "monVolume".

### 7. **Volumes Docker vs Montages Bind :**

- Les volumes Docker sont gérés par Docker lui-même et sont plus flexibles, tandis que les montages bind dépendent du système de fichiers de l'hôte.

- Les volumes Docker peuvent être partagés entre les conteneurs, tandis que les montages bind sont spécifiques à un conteneur.

- Les volumes Docker sont généralement préférés pour la persistance des données et le partage entre les conteneurs.

## Exemple concret

```bash
# Création d'un volume nommé "monVolume"
docker volume create monVolume

# Lancement d'un conteneur avec le volume monté
docker run -v monVolume:/app/data monImage

# Utilisation du volume dans un autre conteneur
docker run -v monVolume:/app/data autreImage
```

---

[...retour au cours](./dockerFichier.md)
