# Gestion des Erreurs et Exceptions

[Retour menu Symfony](../menu.md)

---

## **1. Gestion des erreurs HTTP**

### **1.1 Utilisation des codes HTTP dans les réponses**

Une API REST bien conçue repose sur l’utilisation correcte des **codes HTTP** pour indiquer l’état des réponses. Voici les principaux codes utilisés :

| **Code** | **Description**                           |
|----------|-------------------------------------------|
| `200 OK` | Requête réussie avec un contenu.          |
| `201 Created` | Ressource créée avec succès.           |
| `204 No Content` | Requête réussie sans contenu de réponse. |
| `400 Bad Request` | Données invalides ou malformées.   |
| `401 Unauthorized` | Authentification requise.         |
| `403 Forbidden` | Accès refusé malgré une authentification. |
| `404 Not Found` | Ressource non trouvée.              |
| `500 Internal Server Error` | Erreur interne du serveur.  |

### **1.2 Création de messages d’erreur personnalisés**

Symfony permet de personnaliser les réponses d’erreur pour fournir des messages clairs et pertinents.

#### Exemple d'erreur JSON :

```php
use Symfony\Component\HttpFoundation\JsonResponse;

return new JsonResponse([
    'error' => 'Ressource non trouvée',
    'code' => 404
], 404);
```

#### Ajouter des détails sur l'erreur :

```php
return new JsonResponse([
    'error' => 'Données invalides',
    'details' => [
        'champ_nom' => 'Le nom est obligatoire.',
        'champ_email' => 'Email invalide.'
    ]
], 400);
```

---

## **2. Exceptions personnalisées**

### **2.1 Création d’une exception personnalisée**

Les **exceptions personnalisées** permettent de mieux structurer la gestion des erreurs métier. 

#### Exemple : `ResourceNotFoundException`

Créer une classe d'exception dans `src/Exception` :

```php
namespace App\Exception;

use Symfony\Component\HttpKernel\Exception\NotFoundHttpException;

class ResourceNotFoundException extends NotFoundHttpException
{
    public function __construct(string $message = 'Ressource non trouvée')
    {
        parent::__construct($message);
    }
}
```

### **2.2 Utilisation d’une exception personnalisée**

Dans un service ou un contrôleur, lancer cette exception lorsque la ressource est introuvable :

```php
namespace App\Service;

use App\Exception\ResourceNotFoundException;
use App\Repository\UserRepository;

class UserService
{
    private UserRepository $userRepository;

    public function __construct(UserRepository $userRepository)
    {
        $this->userRepository = $userRepository;
    }

    public function getUserById(int $id)
    {
        $user = $this->userRepository->find($id);

        if (!$user) {
            throw new ResourceNotFoundException("Utilisateur avec l'ID $id introuvable.");
        }

        return $user;
    }
}
```

Dans le contrôleur, il n’est pas nécessaire de gérer cette exception explicitement, car Symfony la convertira automatiquement en réponse HTTP 404.

---

## **3. Gestion globale des erreurs**

Symfony permet de capturer et de personnaliser la gestion des erreurs globales via des **event listeners**.

### **3.1 Event Listener pour capturer les exceptions**

Créer un event listener dans `src/EventListener/ExceptionListener.php` pour intercepter toutes les exceptions :

```php
namespace App\EventListener;

use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\HttpKernel\Event\ExceptionEvent;
use Symfony\Component\HttpKernel\Exception\HttpExceptionInterface;

class ExceptionListener
{
    public function onKernelException(ExceptionEvent $event)
    {
        $exception = $event->getThrowable();
        $response = new JsonResponse();

        if ($exception instanceof HttpExceptionInterface) {
            $response->setStatusCode($exception->getStatusCode());
            $response->setData([
                'error' => $exception->getMessage(),
                'code' => $exception->getStatusCode()
            ]);
        } else {
            $response->setStatusCode(500);
            $response->setData([
                'error' => 'Erreur interne du serveur.',
                'code' => 500
            ]);
        }

        $event->setResponse($response);
    }
}
```

### **3.2 Enregistrement de l’event listener**

Déclarer l’event listener dans `services.yaml` :

```yaml
services:
    App\EventListener\ExceptionListener:
        tags:
            - { name: kernel.event_listener, event: kernel.exception }
```

### **3.3 Retourner des erreurs structurées**

Avec cet event listener, toutes les exceptions non gérées retourneront une réponse JSON claire et structurée :

#### Exemple de réponse pour une ressource introuvable :

```json
{
    "error": "Utilisateur avec l'ID 10 introuvable.",
    "code": 404
}
```

#### Exemple de réponse pour une erreur interne :

```json
{
    "error": "Erreur interne du serveur.",
    "code": 500
}
```

---

[Retour menu Symfony](../menu.md)