# Docker Compose - Notes

[...retour au sommaire](../sommaire.md)

---

## Qu'est-ce que Docker Compose ?

Docker Compose est un outil qui permet de définir et de gérer des applications multi-conteneurs Docker. Avec un simple fichier YAML, Docker Compose vous permet de configurer les services, les réseaux et les volumes nécessaires pour votre application, puis de créer et démarrer l'ensemble de l'infrastructure définie avec une seule commande.

### Avantages de Docker Compose

- **Automatisation**: Simplifie le déploiement d'applications multi-conteneurs.
- **Facilité d'utilisation**: Permet de décrire en langage humain (YAML) l'infrastructure nécessaire pour votre application.
- **Portabilité**: Facilite le transfert de votre application entre différents environnements.

### Installation

Docker Compose est inclus dans les installations de Docker Desktop pour Windows et Mac. Pour Linux, vous pouvez l'installer séparément.

```bash
sudo curl -L "https://github.com/docker/compose/releases/download/1.29.2/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose
```

## Fichier `docker-compose.yml` Exemple : WordPress

```yaml
version: '3.8'

services:
  db:
    image: mysql:5.7
    volumes:
      - db_data:/var/lib/mysql
    environment:
      MYSQL_ROOT_PASSWORD: example

  wordpress:
    depends_on:
      - db
    image: wordpress:latest
    ports:
      - "8000:80"
    environment:
      WORDPRESS_DB_HOST: db:3306
      WORDPRESS_DB_PASSWORD: example

volumes:
  db_data:
```

## Commandes Principales

- **Lancer des services**: `docker-compose up -d`
- **Arrêter des services**: `docker-compose stop`
- **Supprimer des services**: `docker-compose down`
- **Lister les images**: `docker-compose images`
- **Lister les conteneurs**: `docker-compose ps`

## Volume et Réseau

- Les **volumes** permettent de persister et de partager des données entre conteneurs et l'hôte.
- Les **réseaux** permettent de faciliter la communication entre conteneurs, isolant votre application du reste de l'environnement.

## Gestion des Environnements de Développement

Docker Compose simplifie la mise en place d'environnements de développement en permettant de définir des volumes pour le code source, ce qui permet de voir les modifications en temps réel sans reconstruire les images.

## Docker Compose pour le Déploiement

Bien que Docker Compose soit principalement conçu pour le développement, il peut également être utilisé pour le déploiement en production, en particulier dans des environnements simples ou pour des applications de petite à moyenne taille.

## Kubernetes et Docker Compose

Kubernetes est une solution plus évolutive et puissante pour l'orchestration de conteneurs. Docker Compose peut servir de point de départ pour la migration vers Kubernetes, grâce à des outils comme Kompose qui convertissent les fichiers `docker-compose.yml` en configurations Kubernetes.

---

[...retour au sommaire](../sommaire.md)
