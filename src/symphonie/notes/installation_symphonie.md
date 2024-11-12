# Documentation Symfony : Partie 1 - Installation et Configuration

[...retour menu symphonie](../menu.md)

---

## **1. Installation de Symfony**

### **1.1 Prérequis**

Avant d’installer Symfony, s’assurer que l’environnement est correctement configuré.

- **PHP** : Symfony nécessite PHP 8.1 ou supérieur. Vérifier la version avec la commande suivante :

    ```bash
    php -v
    ```

- **Composer** : Gestionnaire de dépendances pour PHP. Vérifier son installation :

    ```bash
    composer -v
    ```

- **Symfony CLI** : Un outil officiel pour créer et gérer des projets Symfony. Installer Symfony CLI depuis [https://symfony.com/download](https://symfony.com/download). Vérifier son bon fonctionnement avec :

    ```bash
    symfony -v
    ```

- **Base de données** : Une base comme MySQL, PostgreSQL ou SQLite, selon le choix du projet.

### **1.2 Installation via la CLI Symfony**

Utiliser Symfony CLI pour créer un nouveau projet Symfony.

#### Créer un nouveau projet avec les composants essentiels :

```bash
symfony new my_project --webapp
```

Quelques options utiles :
- `--webapp` : Installe une application web complète, incluant les bundles essentiels (Twig, Doctrine, etc.).
- `--full` : Inclut tous les composants Symfony disponibles.
- `--version=x.y` : Crée un projet basé sur une version spécifique de Symfony.

Exemple de création d’un projet minimaliste pour une API backend uniquement :

```bash
symfony new my_api --version=6.3
```

### **1.3 Structure initiale d’un projet Symfony**

Après la création, voici une structure typique de projet :

```
my_project/
├── config/         # Configuration des services, routes, doctrine, etc.
├── public/         # Contient les fichiers accessibles publiquement (index.php).
├── src/            # Code source de l'application.
│   ├── Controller/ # Contient les contrôleurs de l'application.
│   ├── Entity/     # Entités de Doctrine pour les bases de données.
│   ├── Repository/ # Classes pour interroger la base via Doctrine.
│   └── Service/    # Contient la logique métier.
├── templates/      # Fichiers de vues (non utilisé dans une API REST).
├── var/            # Fichiers temporaires (logs, cache).
├── vendor/         # Dépendances installées via Composer.
└── .env            # Variables d'environnement.
```

Chaque dossier a son rôle :
- `config/` contient les fichiers de configuration comme `services.yaml`, `routes.yaml`.
- `src/` est le répertoire principal du code métier.

---

## **2. Utilisation de Composer**

### **2.1 Qu’est-ce que Composer ?**

Composer est un outil de gestion des dépendances pour PHP. Il permet d’installer, mettre à jour et supprimer des bibliothèques, tout en gérant un **autoloader** automatique.

### **2.2 Commandes essentielles**

#### Ajouter une dépendance :

```bash
composer require nom/dependance
```

Par exemple, pour ajouter le bundle Doctrine pour la gestion des bases de données :

```bash
composer require doctrine/orm
```

Cela ajoute la dépendance dans le fichier `composer.json`.

#### Mettre à jour les dépendances :

Pour mettre à jour toutes les dépendances selon les versions spécifiées :

```bash
composer update
```

Pour mettre à jour une dépendance spécifique :

```bash
composer update nom/dependance
```

#### Supprimer une dépendance :

```bash
composer remove nom/dependance
```

Exemple : 

```bash
composer remove symfony/twig-bundle
```

Cela supprime le bundle Twig, si non nécessaire pour une API.

### **2.3 Les fichiers Composer**

#### `composer.json`
Ce fichier liste les dépendances du projet et leurs versions :

```json
{
    "require": {
        "symfony/framework-bundle": "^6.3",
        "doctrine/orm": "^2.12"
    }
}
```

#### `composer.lock`
Enregistre les versions exactes des dépendances installées, assurant une cohérence entre les environnements. Il ne doit pas être modifié manuellement. Lors de l’installation sur une nouvelle machine :

```bash
composer install
```

Cette commande lit `composer.lock` et installe exactement les mêmes versions des dépendances.

---

## **3. Configuration de l’environnement**

### **3.1 Les fichiers `.env` et `.env.local`**

Symfony utilise des fichiers `.env` pour la configuration des variables d’environnement.

#### Fichier `.env`

Contient les paramètres par défaut de l’environnement. Exemple de contenu typique :

```env
APP_ENV=dev
APP_SECRET=abc123
DATABASE_URL="mysql://user:password@127.0.0.1:3306/mydb"
```

- **APP_ENV** : Définit l’environnement (développement ou production).
- **DATABASE_URL** : Configuration de la connexion à la base de données.

#### Fichier `.env.local`

Ce fichier est destiné à des paramètres spécifiques à la machine locale (développement) et ne doit pas être versionné (inclus dans `.gitignore`).

Exemple :

```env
APP_ENV=prod
DATABASE_URL="mysql://prod_user:prod_password@127.0.0.1:3306/proddb"
```

### **3.2 Utilisation des variables d’environnement dans le code**

Les variables d’environnement peuvent être utilisées directement via `$_ENV` ou injectées dans des services :

#### Exemple 1 : Lecture directe dans un contrôleur
```php
$databaseUrl = $_ENV['DATABASE_URL'];
```

#### Exemple 2 : Utilisation via le conteneur de services Symfony
```php
public function __construct(string $databaseUrl)
{
    $this->databaseUrl = $databaseUrl;
}
```

Configurer cette variable dans `services.yaml` :

```yaml
parameters:
    database_url: '%env(DATABASE_URL)%'
```

### **3.3 Gestion des environnements : dev, test, prod**

Symfony permet de basculer entre différents environnements via `APP_ENV`.

- **Développement (`dev`)** :
  - Affiche les erreurs avec une trace détaillée.
  - Active le profiler Symfony.

- **Production (`prod`)** :
  - Optimise les performances.
  - Cache activé.

Utiliser cette commande pour vider le cache :

```bash
php bin/console cache:clear --env=prod
```

Pour exécuter des commandes dans un environnement spécifique :

```bash
php bin/console doctrine:migrations:migrate --env=prod
```

---

[Retour menu Symphonie](../menu.md)

