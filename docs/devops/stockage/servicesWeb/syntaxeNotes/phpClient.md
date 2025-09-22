# Client PHP

[...retour au sommaire](../sommaire.md)

---

Pour créer un client PHP qui utilise Guzzle pour interagir avec une API RESTful, en se basant sur un modèle de `Livre`, voici un guide détaillé avec des exemples pour chaque méthode HTTP : GET, POST, PUT et DELETE. Chaque exemple illustre comment envoyer une requête à un endpoint d'API spécifique et gérer la réponse.

## Installation de Guzzle

Tout d'abord, assurez-vous d'avoir installé Guzzle via Composer. Si ce n'est pas encore fait, exécutez cette commande dans le terminal à la racine de votre projet PHP :

```bash
composer require guzzlehttp/guzzle
```

## Création du Client PHP avec Guzzle

### Initialisation de GuzzleHttp\Client

Avant d'envoyer des requêtes, initialisez un client Guzzle :

```php
require 'vendor/autoload.php';

use GuzzleHttp\Client;

$client = new Client([
    // Base URI is used with relative requests
    'base_uri' => 'https://api.exemple.com',
    // You can set any number of default request options.
    'timeout'  => 2.0,
]);
```

### GET - Récupérer des Informations

Pour récupérer des informations, par exemple, les détails d'un `Livre` :

```php
$response = $client->request('GET', '/livres/1');

echo $response->getStatusCode(); // 200
echo $response->getBody();
```

### POST - Créer une Ressource

Pour créer un nouveau `Livre` :

```php
$response = $client->request('POST', '/livres', [
    'json' => [
        'titre' => 'Nouveau Livre',
        'auteur' => 'Auteur Inconnu',
    ]
]);

echo $response->getStatusCode(); // Exemple : 201
```

### PUT - Mettre à Jour une Ressource

Pour mettre à jour un `Livre` existant :

```php
$response = $client->request('PUT', '/livres/1', [
    'json' => [
        'titre' => 'Titre Mis à Jour',
        'auteur' => 'Auteur Mis à Jour',
    ]
]);

echo $response->getStatusCode(); // Exemple : 200
```

### DELETE - Supprimer une Ressource

Pour supprimer un `Livre` :

```php
$response = $client->request('DELETE', '/livres/1');

echo $response->getStatusCode(); // Exemple : 204
```

## Manipulation des Objets de Réponse

Chaque réponse de Guzzle est une instance de `Psr\Http\Message\ResponseInterface`, vous permettant d'accéder au statut HTTP, aux headers, et au corps de la réponse.

```php
$statusCode = $response->getStatusCode();
$contentType = $response->getHeader('Content-Type')[0];
$body = $response->getBody();
```

## Gestion des Erreurs

Guzzle lance des exceptions pour les erreurs rencontrées pendant les requêtes. Vous pouvez les gérer avec un bloc `try-catch` :

```php
use GuzzleHttp\Exception\ClientException;

try {
    $response = $client->request('GET', '/livres/999');
} catch (ClientException $e) {
    echo $e->getMessage();
    echo $e->getResponse()->getStatusCode();
}
```

## Initialisation du Projet PHP avec Composer et PSR-4

### Étape 1: Installation de Composer

Si Composer n'est pas encore installé sur votre machine, téléchargez-le et installez-le depuis [getcomposer.org](https://getcomposer.org/).

### Étape 2: Création du Projet

Ouvrez un terminal et naviguez jusqu'au répertoire où vous souhaitez créer votre projet. Exécutez la commande suivante pour créer un nouveau dossier pour votre projet :

```bash
mkdir MonProjetPHP
cd MonProjetPHP
```

### Étape 3: Initialiser Composer dans Votre Projet

Dans le répertoire de votre projet, exécutez :

```bash
composer init
```

Suivez les instructions pour définir les propriétés de base de votre projet (nom, description, auteur, etc.). Cela créera un fichier `composer.json` dans votre répertoire de projet.

### Étape 4: Configuration de PSR-4 Autoloading

Modifiez le fichier `composer.json` pour ajouter une configuration d'autoloading en utilisant PSR-4. Cela permettra à Composer de charger automatiquement vos classes PHP sans inclure manuellement les fichiers.

```json
{
    "autoload": {
        "psr-4": {
            "App\\": "src/"
        }
    }
}
```

Dans cet exemple, `"App\\"` est le namespace racine pour votre projet, et `"src/"` est le répertoire contenant vos fichiers PHP. Chaque fois que vous créez une nouvelle classe dans ce répertoire, utilisez le namespace `App\` suivi du chemin de votre classe.

### Étape 5: Générer le Fichier d'Autoload

Après avoir configuré l'autoloading, exécutez la commande suivante pour générer le fichier d'autoload de Composer :

```bash
composer dump-autoload
```

### Structure de Répertoire Recommandée

Votre projet doit maintenant ressembler à cela :

```plaintext
MonProjetPHP/
├── src/
│   └── ...
├── vendor/
│   └── ...
├── composer.json
└── composer.lock
```

- Le répertoire `src/` contiendra vos classes PHP.
- Le répertoire `vendor/` est créé par Composer et contient toutes vos dépendances, ainsi que le système d'autoloading généré.

### Étape 6: Utilisation de l'Autoloading dans Votre Projet

Pour utiliser l'autoloading dans votre projet, incluez le fichier `autoload.php` de Composer au début de votre script PHP principal (par exemple, `index.php`).

```php
require __DIR__ . '/vendor/autoload.php';

// Vous pouvez maintenant utiliser vos classes sans nécessiter d'inclure manuellement leurs fichiers.
use App\ClasseExemple;

$objet = new ClasseExemple();
```

### Étape 7: Ajout de Dépendances avec Composer

Pour ajouter une nouvelle dépendance, par exemple, GuzzleHttp pour les requêtes HTTP, exécutez :

```bash
composer require guzzlehttp/guzzle
```

Composer mettra à jour votre `composer.json` et installera la dépendance dans le répertoire `vendor/`.

## Conclusion

Ce guide a couvert les étapes de base pour initialiser un projet PHP avec Composer et PSR-4 pour l'autoloading. Cette structure de projet facilite la gestion des dépendances et l'organisation de votre code, rendant le développement plus efficace et maintenable.

---

[...retour au sommaire](../sommaire.md)
