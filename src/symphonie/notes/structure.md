# Structure d’un Projet Symfony

[Retour menu Symphony](../menu.md)

---

## **1. Explication des Dossiers Principaux**

Lorsqu’un projet Symfony est généré, il adopte une structure bien définie. Chaque dossier a un rôle spécifique qui contribue à l’organisation et à la maintenabilité de l’application.

### **1.1 Dossier `src/`**

Le répertoire `src/` contient tout le code source de l’application. C’est ici que se trouvent les composants métiers et les éléments essentiels.

#### Sous-dossiers principaux :
- **`Controller/`** : 
  Contient les contrôleurs, responsables de la gestion des requêtes HTTP et des réponses. Exemple : 

  ```php
  namespace App\Controller;

  use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
  use Symfony\Component\HttpFoundation\Response;

  class ExempleController extends AbstractController
  {
      public function index(): Response
      {
          return new Response('Hello Symfony!');
      }
  }
  ```

- **`Entity/`** : 
  Contient les entités Doctrine qui représentent les tables de la base de données. Chaque entité est une classe PHP mappée à une table.

- **`Repository/`** : 
  Contient les classes de repository qui permettent de manipuler les entités via des requêtes spécifiques. 

- **`Service/`** : 
  Emplacement pour les services métiers. Les services encapsulent la logique métier et sont injectés dans les contrôleurs.

- **`Security/`** (si activé) : 
  Contient les classes liées à la sécurité, telles que les gestionnaires d’authentification.

---

### **1.2 Dossier `config/`**

Le répertoire `config/` centralise la **configuration de l’application**.

#### Fichiers importants :
- **`services.yaml`** : 
  Déclare les services, leurs paramètres et la configuration de l’autowiring.

  ```yaml
  services:
      App\Service\:
          resource: '../src/Service/'
          autowire: true
          autoconfigure: true
  ```

- **`routes.yaml`** : 
  Définit les routes de l’application.

  ```yaml
  index:
      path: /
      controller: App\Controller\ExempleController::index
  ```

- **`packages/`** : 
  Contient des fichiers de configuration pour des packages spécifiques comme Doctrine, Twig, etc.

- **`bundles.php`** : 
  Liste les bundles enregistrés dans l’application.

---

### **1.3 Dossier `public/`**

Le dossier `public/` est le **point d’entrée HTTP** de l’application. 

- Contient le fichier `index.php`, responsable de l’initialisation de Symfony.
- C’est ici que doivent être placés les fichiers accessibles publiquement, comme les **images** ou les **fichiers CSS/JS compilés**.

---

### **1.4 Dossier `templates/`**

Le dossier `templates/` contient les **fichiers de vues Twig**. Bien que Twig soit souvent utilisé dans des applications web classiques, il peut aussi servir pour des réponses JSON formatées.

- Exemple d’un fichier Twig pour une page simple :

  ```twig
  <!DOCTYPE html>
  <html>
      <head>
          <title>{{ title }}</title>
      </head>
      <body>
          <h1>{{ content }}</h1>
      </body>
  </html>
  ```

Dans le cadre d’une **API REST**, ce dossier est rarement utilisé.

---

### **1.5 Dossiers `var/` et `cache/`**

- **`var/`** : Stocke les fichiers temporaires, comme les journaux d’exécution (`logs`) et le cache de production.
  
- **`cache/`** (sous `var/`) : Contient des fichiers cache pour optimiser les performances.

  Utiliser cette commande pour vider le cache :
  
  ```bash
  symfony console cache:clear
  ```

---

## **2. Introduction au Bundle System**

Symfony repose sur un **système de bundles** qui permet de modulariser les fonctionnalités.

### **2.1 Qu’est-ce qu’un Bundle ?**

Un **bundle** est un paquet de code réutilisable qui peut inclure :
- Des **contrôleurs**.
- Des **services**.
- Des **fichiers de configuration**.
- Des **assets** (CSS, JS).

Symfony est livré avec des bundles natifs pour gérer des fonctionnalités standard, comme le routage, le cache, et la sécurité.

### **2.2 Bundles standards vs Bundles tiers**

#### **Bundles standards**

Ce sont des bundles fournis avec Symfony ou installés automatiquement avec l’application. Exemples :
- **FrameworkBundle** : Le cœur de Symfony.
- **TwigBundle** : Fournit l’intégration avec le moteur de templates Twig.
- **DoctrineBundle** : Intègre Doctrine ORM.

#### **Bundles tiers**

Ils sont développés par la communauté et permettent d’ajouter des fonctionnalités supplémentaires.

Quelques exemples utiles :
- **LexikJWTAuthenticationBundle** : Gestion des tokens JWT.
- **FOSUserBundle** : Gestion des utilisateurs.
- **API Platform** : Pour créer des API REST complètes avec documentation automatique.

### **2.3 Installer un bundle tiers**

Utiliser Composer pour installer un bundle tiers. Exemple avec **DoctrineFixturesBundle** pour les données de test :

```bash
composer require doctrine/doctrine-fixtures-bundle
```

Une fois installé, il est automatiquement ajouté au fichier `bundles.php` :

```php
return [
    Doctrine\Bundle\FixturesBundle\DoctrineFixturesBundle::class => ['dev' => true, 'test' => true],
];
```

### **2.4 Enregistrer un bundle manuellement**

Dans certains cas, un bundle peut nécessiter une activation manuelle en ajoutant une ligne dans `config/bundles.php` :

```php
return [
    Symfony\Bundle\FrameworkBundle\FrameworkBundle::class => ['all' => true],
];
```

---

[Retour menu Symphony](../menu.md)