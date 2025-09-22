# Controleurs, services et DI

[Retour menu Symphony](../menu.md)

---

## **1. Contrôleurs**

Les **contrôleurs** sont responsables de traiter les requêtes et de renvoyer des réponses. Dans une API REST, il est crucial de standardiser les réponses au format **JSON**.

### **1.1 Création d’un contrôleur**

Un contrôleur est une classe dans le répertoire `src/Controller` qui gère des routes spécifiques. Chaque méthode correspond généralement à une route.

#### Générer un contrôleur avec Symfony CLI :

```bash
symfony console make:controller NomDuControleur
```

Cela génère un fichier `NomDuControleur.php` avec un exemple de méthode.

#### Exemple de contrôleur :

```php
namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\Routing\Annotation\Route;

class UserController extends AbstractController
{
    #[Route('/users', name: 'user_list', methods: ['GET'])]
    public function list(): JsonResponse
    {
        $users = [
            ['id' => 1, 'name' => 'John Doe'],
            ['id' => 2, 'name' => 'Jane Doe']
        ];

        return new JsonResponse($users);
    }
}
```

### **1.2 Gestion des routes avec annotations**

Les routes associent des URL spécifiques aux méthodes des contrôleurs.

#### Syntaxe de base :

```php
#[Route('/chemin', name: 'nom_route', methods: ['GET', 'POST'])]
```

#### Exemple d’utilisation :

```php
#[Route('/users/{id}', name: 'user_detail', methods: ['GET'])]
public function detail(int $id): JsonResponse
{
    $user = ['id' => $id, 'name' => 'User '.$id];
    return new JsonResponse($user);
}
```

### **1.3 Utilisation de JsonResponse**

Dans une API REST, il est essentiel de **toujours retourner des données au format JSON**. Symfony fournit la classe `JsonResponse` pour cet usage.

#### Exemple simple :

```php
use Symfony\Component\HttpFoundation\JsonResponse;

return new JsonResponse(['status' => 'success']);
```

#### Personnalisation de la réponse HTTP :

```php
return new JsonResponse(['error' => 'Resource not found'], 404);
```

---

## **2. Services**

Les **services** permettent d’encapsuler la logique métier, rendant les contrôleurs plus légers et l’application plus maintenable.

### **2.1 Pourquoi utiliser des services ?**

- **Séparation des préoccupations** : Garde les contrôleurs concentrés sur la gestion des requêtes.
- **Réutilisabilité** : Un service peut être utilisé par plusieurs contrôleurs.
- **Facilité de test** : Les services isolent la logique métier, facilitant les tests unitaires.

### **2.2 Création d’un service**

Un service est une classe PHP située dans le répertoire `src/Service`.

#### Générer un service avec Symfony CLI :

```bash
symfony console make:service NomDuService
```

#### Exemple de service :

```php
namespace App\Service;

class UserService
{
    public function getAllUsers(): array
    {
        return [
            ['id' => 1, 'name' => 'John Doe'],
            ['id' => 2, 'name' => 'Jane Doe']
        ];
    }
}
```

---

## **3. Injection de Dépendances**

L’**injection de dépendances** permet d’automatiser l’instanciation des services et leur gestion tout au long du cycle de vie de l’application.

### **3.1 Conteneur de services Symfony**

Symfony utilise un **conteneur de services** pour gérer les dépendances. Ce conteneur permet d’instancier automatiquement les classes nécessaires, de gérer leur cycle de vie, et de faciliter leur injection dans d’autres classes.

#### Configuration automatique des services dans `services.yaml` :

```yaml
services:
    App\Service\:
        resource: '../src/Service/*'
        autowire: true
        autoconfigure: true
```

- **`autowire: true`** : Symfony détecte automatiquement les dépendances et les injecte.
- **`autoconfigure: true`** : Configure automatiquement les services avec les bons tags.

### **3.2 Types d’injection**

Symfony propose plusieurs méthodes pour injecter des dépendances.

#### **Injection par constructeur** (recommandée)

Les dépendances sont injectées via le constructeur, assurant que la classe ne puisse pas être instanciée sans elles.

```php
namespace App\Controller;

use App\Service\UserService;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\Routing\Annotation\Route;

class UserController
{
    private UserService $userService;

    public function __construct(UserService $userService)
    {
        $this->userService = $userService;
    }

    #[Route('/users', name: 'user_list', methods: ['GET'])]
    public function list(): JsonResponse
    {
        $users = $this->userService->getAllUsers();
        return new JsonResponse($users);
    }
}
```

#### **Injection par méthode**

Permet d’injecter des services uniquement quand ils sont nécessaires dans une méthode spécifique.

```php
public function setUserService(UserService $userService): void
{
    $this->userService = $userService;
}
```

#### **Injection par propriété** (moins recommandée)

Injecte directement un service dans une propriété publique ou protégée.

```php
#[Required]
public UserService $userService;
```

### **3.3 Gestion des paramètres et des fichiers**

#### **Injection des paramètres**

Les variables définies dans `services.yaml` ou `.env` peuvent être injectées dans les services ou contrôleurs.

Exemple dans `services.yaml` :

```yaml
parameters:
    app.default_limit: 10
```

Injection dans un service :

```php
namespace App\Service;

class PaginationService
{
    private int $defaultLimit;

    public function __construct(int $defaultLimit)
    {
        $this->defaultLimit = $defaultLimit;
    }
}
```

Déclaration dans `services.yaml` :

```yaml
App\Service\PaginationService:
    arguments:
        $defaultLimit: '%app.default_limit%'
```

### **3.4 Cycle de vie et scope des services**

Symfony gère le **cycle de vie** des services grâce au conteneur :
- **Singleton (par défaut)** : Une seule instance de chaque service est partagée dans tout le projet.
- **Scope Request** : Certaines dépendances peuvent être limitées à une requête.

Configurer un service pour qu’il soit recréé à chaque requête :

```yaml
services:
    App\Service\SomeService:
        shared: false
```

---

[Retour menu Symphony](../menu.md)