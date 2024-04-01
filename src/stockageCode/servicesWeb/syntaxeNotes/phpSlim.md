# API avec PHP Slim

[...retour au sommaire](../sommaire.md)

---

Créer une API en PHP en utilisant le micro-framework Slim est un choix excellent pour développer des applications web légères et performantes. Slim est particulièrement adapté pour la construction d'API RESTful grâce à sa facilité de gestion des routes et des requêtes HTTP. Voici comment vous pouvez utiliser Slim pour créer une API avec un modèle de `Livre` pour illustrer les opérations CRUD de base.

## Définir le Modèle `Livre`

Avant de définir les routes, vous devez avoir une classe `Livre` ou un tableau associatif pour simuler une base de données en mémoire pour cet exemple. Voici une structure de classe simple en PHP :

```php
class Livre {
    public $id;
    public $titre;
    public $auteur;

    public function __construct($id, $titre, $auteur) {
        $this->id = $id;
        $this->titre = $titre;
        $this->auteur = $auteur;
    }
}
```

## Installation de Slim et Configuration Initiale

Utilisez Composer pour installer Slim et configurer votre projet. Créez un fichier `composer.json` avec le contenu suivant ou utilisez la commande `composer require slim/slim "^4.0"` dans votre terminal.

```json
{
    "require": {
        "slim/slim": "^4.0"
    }
}
```

Après l'installation, incluez l'autoload de Composer et initialisez votre application Slim :

```php
require __DIR__ . '/vendor/autoload.php';

use Slim\Factory\AppFactory;

$app = AppFactory::create();
```

## Définition des Routes

### GET - Lire des Livres

```php
$app->get('/livres', function ($request, $response, $args) {
    // Simuler la récupération de tous les livres
    $livres = [
        new Livre(1, "Livre 1", "Auteur 1"),
        new Livre(2, "Livre 2", "Auteur 2")
    ];
    $payload = json_encode($livres);
    $response->getBody()->write($payload);
    return $response->withHeader('Content-Type', 'application/json');
});
```

### POST - Créer un Livre

```php
$app->post('/livres', function ($request, $response, $args) {
    // Simuler la création d'un livre
    $body = $request->getParsedBody();
    $livre = new Livre($body['id'], $body['titre'], $body['auteur']);
    // Ajouter le livre à la base de données
    $payload = json_encode($livre);
    $response->getBody()->write($payload);
    return $response->withHeader('Content-Type', 'application/json');
});
```

### PUT - Mettre à Jour un Livre

```php
$app->put('/livres/{id}', function ($request, $response, $args) {
    // Simuler la mise à jour d'un livre
    $id = $args['id'];
    $body = $request->getParsedBody();
    $livre = new Livre($id, $body['titre'], $body['auteur']);
    // Mettre à jour le livre dans la base de données
    $payload = json_encode($livre);
    $response->getBody()->write($payload);
    return $response->withHeader('Content-Type', 'application/json');
});
```

### DELETE - Supprimer un Livre

```php
$app->delete('/livres/{id}', function ($request, $response, $args) {
    // Simuler la suppression d'un livre
    $id = $args['id'];
    // Supprimer le livre de la base de données
    return $response->withStatus(204);
});
```

## Exécution de l'Application

N'oubliez pas d'ajouter `$app->run();` à la fin de votre fichier pour démarrer l'application Slim.

```php
$app->run();
```

Créer un projet API RESTful avec Slim PHP nécessite une compréhension de quelques concepts clés tels que Composer, l'autoloading PSR-4, et la structure de base du projet. Voici un guide détaillé étape par étape pour vous aider à démarrer.

## Initialisation du Projet avec Composer

### 1. Installer Composer

Assurez-vous que [Composer](https://getcomposer.org/) est installé sur votre système. Composer est un outil de gestion des dépendances pour PHP qui vous permet d'importer des bibliothèques tierces dans votre projet.

### 2. Créer un Nouveau Projet

Ouvrez un terminal et créez un nouveau dossier pour votre projet, puis naviguez dans ce dossier :

```bash
mkdir mon-api-slim
cd mon-api-slim
```

### 3. Initialiser Composer dans Votre Projet

Exécutez la commande suivante pour initialiser un nouveau projet Composer. Cela créera un fichier `composer.json` dans votre répertoire de projet :

```bash
composer init
```

Suivez les instructions à l'écran pour configurer votre projet. Vous pouvez choisir les valeurs par défaut pour commencer rapidement.

### 4. Ajouter Slim et PSR-7

Ajoutez Slim et une implémentation PSR-7 à votre projet en utilisant Composer :

```bash
composer require slim/slim "^4.0"
composer require slim/psr7
```

## Configuration de l'Autoloading PSR-4

### 1. Configurer PSR-4 dans Composer.json

Pour utiliser l'autoloading PSR-4, vous devez le configurer dans votre fichier `composer.json`. Ajoutez la section `autoload` pour spécifier le namespace de votre application et le répertoire correspondant :

```json
{
    "require": {
        "slim/slim": "^4.0",
        "slim/psr7": "^1.4"
    },
    "autoload": {
        "psr-4": {
            "App\\": "src/"
        }
    }
}
```

Ici, `App\\` est le namespace que vous utiliserez pour votre application, et `src/` est le répertoire où vos classes PHP seront stockées.

### 2. Générer le Fichier Autoload

Après avoir ajouté la configuration d'autoloading à `composer.json`, exécutez la commande suivante pour générer le fichier `autoload` :

```bash
composer dump-autoload
```

## Structure de Répertoire de Base

Créez les répertoires nécessaires pour votre application en suivant la structure recommandée :

```plaintext
mon-api-slim/
├─ src/
│  ├─ Controller/
│  ├─ Middleware/
│  ├─ Model/
├─ public/
│  ├─ index.php
├─ vendor/
├─ composer.json
```

## Exemple de Code de Démarrage

### Fichier d'Entrée (public/index.php)

Créez un fichier `index.php` dans le répertoire `public/`. Ce fichier sera le point d'entrée de votre application Slim.

```php
<?php
use Slim\Factory\AppFactory;

require __DIR__ . '/../vendor/autoload.php';

$app = AppFactory::create();

// Définir une route de test
$app->get('/hello/{name}', function ($request, $response, $args) {
    $name = $args['name'];
    $response->getBody()->write("Hello, $name");
    return $response;
});

$app->run();
```

## Exécution de Votre Application

Pour exécuter votre application, utilisez le serveur de développement intégré de PHP :

```bash
php -S localhost:8000 -t public
```

Naviguez ensuite vers `http://localhost:8000/hello/world` dans votre navigateur pour voir l'API en action.

## Conclusion

En suivant ces étapes, vous avez configuré un projet Slim PHP avec l'autoloading PSR-4, prêt pour le développement d'une API RESTful. Vous pouvez maintenant continuer à développer votre application en ajoutant des modèles, des contrôleurs et des middlewares selon vos besoins.

---

[...retour au sommaire](../sommaire.md)
