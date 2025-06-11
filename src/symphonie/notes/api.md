# 💻 Création d'une API Symfony avec API Platform et consommation depuis un Frontend

[...retorn en rèire](../menu.md)


---

## 1. 🏗 Préparation du projet

### Installation via Composer
```bash
composer create-project symfony/skeleton nom-du-projet
cd nom-du-projet
composer require api symfony/orm-pack maker security lexik/jwt-authentication-bundle
```

### Créer un fichier `.env.local`
```dotenv
DATABASE_URL="mysql://user:password@127.0.0.1:3306/nom_bdd"
```

### Lancer la base de données avec Docker (ex : MySQL)
```yaml
# docker-compose.yml
version: '3.8'
services:
  db:
    image: mysql:8
    environment:
      MYSQL_ROOT_PASSWORD: root
      MYSQL_DATABASE: nom_bdd
    ports:
      - "3306:3306"
    volumes:
      - db_data:/var/lib/mysql
volumes:
  db_data:
```

```bash
docker-compose up -d
```

### Lancer le serveur Symfony
```bash
symfony serve -d
```

---

## 2. 📦 Création des entités avec API Platform

### Exemple : entité `Livre`
```bash
php bin/console make:entity Livre
```
Champs :
- titre (string)
- auteur (string)
- anneePublication (integer)

```php
#[ApiResource]
#[ORM\Entity()]
class Livre
{
    #[ORM\Id, ORM\GeneratedValue, ORM\Column(type: 'integer')]
    private ?int $id = null;

    #[ORM\Column(type: 'string', length: 255)]
    private string $titre;

    #[ORM\Column(type: 'string', length: 255)]
    private string $auteur;

    #[ORM\Column(type: 'integer')]
    private int $anneePublication;
    
    // Getters/setters ...
}
```

### Créer la base
```bash
php bin/console doctrine:database:create
php bin/console make:migration
php bin/console doctrine:migrations:migrate
```

---

## 3. ⚙️ API Platform en action

### Routes générées automatiquement
- GET /api/livres
- GET /api/livres/{id}
- POST /api/livres
- PUT /api/livres/{id}
- DELETE /api/livres/{id}

### Tester avec HTTPie
```bash
http :8000/api/livres
```

---

## 4. 🔐 Authentification JWT (LexikJWTAuthenticationBundle)

### 1. Générer les clés
```bash
mkdir -p config/jwt
openssl genrsa -out config/jwt/private.pem -aes256 4096
openssl rsa -pubout -in config/jwt/private.pem -out config/jwt/public.pem
```

### 2. Configuration `.env`
```dotenv
JWT_PASSPHRASE=mot-de-passe
```

### 3. Config `lexik_jwt_authentication.yaml`
```yaml
lexik_jwt_authentication:
    secret_key: '%kernel.project_dir%/config/jwt/private.pem'
    public_key: '%kernel.project_dir%/config/jwt/public.pem'
    pass_phrase: '%env(JWT_PASSPHRASE)%'
    token_ttl: 3600
```

### 4. Créer une entité `User`
```bash
php bin/console make:user
php bin/console make:migration && php bin/console doctrine:migrations:migrate
```

### 5. Ajouter un endpoint login
```yaml
# config/routes.yaml
api_login_check:
    path: /api/login
```

### 6. Auth via POST
```bash
http POST :8000/api/login username=admin password=admin
```
Retourne un JWT.

### 7. Protéger les ressources
```yaml
# config/packages/security.yaml
security:
    firewalls:
        api:
            pattern: ^/api
            stateless: true
            jwt: ~
    access_control:
        - { path: ^/api/livres, roles: IS_AUTHENTICATED_FULLY }
```

---

## 5. 📲 Consommer depuis un Frontend

### Exemple de requête (JS / Fetch)
```js
const token = 'eyJ...';
fetch('http://localhost:8000/api/livres', {
  headers: {
    'Authorization': `Bearer ${token}`,
    'Content-Type': 'application/json'
  }
})
.then(res => res.json())
.then(data => console.log(data));
```

### Avec axios (React / Vue)
```js
axios.get('http://localhost:8000/api/livres', {
  headers: {
    Authorization: `Bearer ${token}`
  }
})
.then(response => console.log(response.data));
```

---

## 6. 🔁 Aller plus loin : Events, DTOs, Relations

### Ajouter un Event Listener
```bash
php bin/console make:subscriber LivreCreatedSubscriber
```
Dans le subscriber :
```php
public function onKernelView(ViewEvent $event): void
{
    if (!$event->getControllerResult() instanceof Livre || !$event->isMasterRequest()) {
        return;
    }
    // Logique post-création
}
```

### Exemple relation ManyToOne (Livre -> User)
```php
#[ORM\ManyToOne(targetEntity: User::class)]
private ?User $proprietaire = null;
```

### DTO personnalisé + DataTransformer (si on veut éviter l'entité brute)
```php
// src/Dto/LivreOutput.php
class LivreOutput {
    public string $titre;
    public string $auteur;
}
```

---

## ✅ Récapitulatif

- **Symfony + API Platform** permet de générer une API REST riche automatiquement
- **JWT** permet d'ajouter de l'authentification sécurisée
- On peut facilement **consommer l'API depuis le front** avec axios ou fetch
- Les concepts avancés (DTOs, Events, Relations) permettent de construire une architecture propre et solide

---

[...retorn en rèire](../menu.md)
