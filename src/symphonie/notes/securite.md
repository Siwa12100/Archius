```markdown
# Documentation Symfony : Partie 8 - Sécurité

[...retour menu symphony](../menu.md)

---

## **1. Introduction à la sécurité Symfony**

### **1.1 Gestion des utilisateurs et rôles**

Symfony repose sur un système d’**utilisateurs** et de **rôles** pour gérer l'accès aux différentes parties de l’application.

#### **Création de l’entité User**

Une entité `User` doit implémenter l'interface `UserInterface` de Symfony, qui définit les méthodes nécessaires à la gestion des utilisateurs.

##### Exemple d'entité User avec des rôles :

```php
namespace App\Entity;

use Doctrine\ORM\Mapping as ORM;
use Symfony\Component\Security\Core\User\UserInterface;

#[ORM\Entity]
class User implements UserInterface
{
    #[ORM\Id]
    #[ORM\GeneratedValue]
    #[ORM\Column(type: 'integer')]
    private int $id;

    #[ORM\Column(type: 'string', unique: true)]
    private string $email;

    #[ORM\Column(type: 'string')]
    private string $password;

    #[ORM\Column(type: 'json')]
    private array $roles = [];

    public function getId(): ?int
    {
        return $this->id;
    }

    public function getEmail(): string
    {
        return $this->email;
    }

    public function setEmail(string $email): self
    {
        $this->email = $email;
        return $this;
    }

    public function getPassword(): string
    {
        return $this->password;
    }

    public function setPassword(string $password): self
    {
        $this->password = $password;
        return $this;
    }

    public function getRoles(): array
    {
        return $this->roles ?: ['ROLE_USER'];
    }

    public function setRoles(array $roles): self
    {
        $this->roles = $roles;
        return $this;
    }

    public function eraseCredentials() {}
}
```

#### **Champs obligatoires pour UserInterface** :
- `getRoles()` : Retourne les rôles de l'utilisateur.
- `getPassword()` : Retourne le mot de passe hashé.
- `getUsername()` : Alias pour l'identifiant utilisateur (souvent `email`).
- `eraseCredentials()` : Supprime les données sensibles après usage.

### **1.2 Utilisation du composant Security**

Le **composant Security** permet de centraliser :
- **L'authentification** : Valider l’identité des utilisateurs.
- **L'autorisation** : Contrôler les permissions selon les rôles.

Configurer les utilisateurs dans `security.yaml` :

```yaml
security:
    providers:
        app_user_provider:
            entity:
                class: App\Entity\User
                property: email # Champ utilisé pour identifier l'utilisateur

    encoders:
        App\Entity\User:
            algorithm: bcrypt

    firewalls:
        main:
            anonymous: true
            stateless: true # Pour une API REST
            jwt: 
                auth_header: Authorization
                auth_header_prefix: Bearer
                query_parameter: token
                provider: app_user_provider
```

---

## **2. Authentification**

### **2.1 Configuration de JWT**

Le **JSON Web Token (JWT)** est une méthode standard pour sécuriser l'authentification dans une API REST. Le bundle **LexikJWTAuthenticationBundle** facilite son implémentation dans Symfony.

#### **Installation de LexikJWTAuthenticationBundle**

```bash
composer require lexik/jwt-authentication-bundle
```

#### **Génération des clés JWT**

Créer un répertoire pour les clés et générer une clé privée :

```bash
mkdir config/jwt
openssl genrsa -out config/jwt/private.pem -aes256 4096
```

Générer la clé publique à partir de la clé privée :

```bash
openssl rsa -pubout -in config/jwt/private.pem -out config/jwt/public.pem
```

#### **Configuration des clés dans `.env`**

Ajouter les chemins et la phrase secrète :

```env
JWT_SECRET_KEY=%kernel.project_dir%/config/jwt/private.pem
JWT_PUBLIC_KEY=%kernel.project_dir%/config/jwt/public.pem
JWT_PASSPHRASE=votre_passphrase
```

#### **Configuration dans `lexik_jwt_authentication.yaml`**

Configurer le bundle pour utiliser les clés JWT :

```yaml
lexik_jwt_authentication:
    secret_key: '%env(resolve:JWT_SECRET_KEY)%'
    public_key: '%env(resolve:JWT_PUBLIC_KEY)%'
    pass_phrase: '%env(JWT_PASSPHRASE)%'
    token_ttl: 3600 # Durée de vie du token en secondes
```

### **2.2 Génération des tokens JWT**

Créer une route pour authentifier l'utilisateur et générer un token :

```php
namespace App\Controller;

use Lexik\Bundle\JWTAuthenticationBundle\Services\JWTTokenManagerInterface;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\Security\Core\User\UserInterface;
use Symfony\Component\Routing\Annotation\Route;

class AuthController
{
    #[Route('/api/login', name: 'api_login', methods: ['POST'])]
    public function login(JWTTokenManagerInterface $jwtManager, UserInterface $user): JsonResponse
    {
        return new JsonResponse([
            'token' => $jwtManager->create($user)
        ]);
    }
}
```

### **2.3 Requête avec token**

Les clients doivent inclure le token dans les en-têtes HTTP pour chaque requête :

```http
GET /api/resource HTTP/1.1
Authorization: Bearer <votre_token_jwt>
```

---

## **3. Autorisation**

### **3.1 Contrôle d’accès avec annotations**

Limiter l'accès à certaines actions en utilisant des annotations comme `@IsGranted` :

```php
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\Routing\Annotation\Route;
use Sensio\Bundle\FrameworkExtraBundle\Configuration\IsGranted;

class AdminController extends AbstractController
{
    #[Route('/admin/dashboard', name: 'admin_dashboard')]
    #[IsGranted('ROLE_ADMIN')]
    public function dashboard(): JsonResponse
    {
        return new JsonResponse(['message' => 'Bienvenue, Admin']);
    }
}
```

### **3.2 Contrôle d’accès global dans `security.yaml`**

Définir des règles d'accès pour des routes spécifiques :

```yaml
security:
    access_control:
        - { path: ^/admin, roles: ROLE_ADMIN }
        - { path: ^/api, roles: ROLE_USER }
```

### **3.3 Vérification dynamique avec `isGranted`**

Utiliser `isGranted` dans le code pour vérifier dynamiquement l'accès :

```php
if (!$this->isGranted('ROLE_ADMIN')) {
    throw $this->createAccessDeniedException('Accès refusé.');
}
```

---

[...retour menu symphony](../menu.md)