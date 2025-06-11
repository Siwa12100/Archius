# 🔐 Cours Symfony : Création et Authentification des Utilisateurs (Monolithe)

[...retorn en rèire](../menu.md)

---

## 1. ✨ Génération de l'entité User

### 🔧 Commande CLI
```bash
symfony console make:user
```

### 💬 Questions posées :
```
Name of the security user class [User]: User
Store user data in database with Doctrine? [yes]: yes
Unique property (e.g. email)? [email]: email
Do you want to hash/check user passwords? [yes]: yes
```

### 🧱 Résultat : `src/Entity/User.php`
```php
class User implements UserInterface, PasswordAuthenticatedUserInterface
{
    #[ORM\Id]
    #[ORM\GeneratedValue]
    #[ORM\Column(type: 'integer')]
    private ?int $id = null;

    #[ORM\Column(type: 'string', length: 180, unique: true)]
    private ?string $email = null;

    #[ORM\Column(type: 'json')]
    private array $roles = [];

    #[ORM\Column(type: 'string')]
    private string $password;

    public function getUserIdentifier(): string
    {
        return (string) $this->email;
    }

    public function getRoles(): array
    {
        $roles = $this->roles;
        $roles[] = 'ROLE_USER';
        return array_unique($roles);
    }

    public function getPassword(): string { return $this->password; }
    public function setPassword(string $password): self { $this->password = $password; return $this; }
}
```

---

## 2. 🔐 Configuration du Firewall et Provider

### 📁 `config/packages/security.yaml`
```yaml
security:
  password_hashers:
    Symfony\Component\Security\Core\User\PasswordAuthenticatedUserInterface: 'auto'

  providers:
    app_user_provider:
      entity:
        class: App\Entity\User
        property: email

  firewalls:
    dev:
      pattern: ^/(_(profiler|wdt)|css|images|js)/
      security: false

    main:
      lazy: true
      provider: app_user_provider
      form_login:
        login_path: app_login
        check_path: app_login
      logout:
        path: app_logout

  access_control:
    - { path: ^/admin, roles: ROLE_ADMIN }
    - { path: ^/profile, roles: ROLE_USER }
```

---

## 3. 📝 Formulaire d'inscription (Registration)

### 🚀 Génération automatique
```bash
symfony console make:registration-form
```

### 🔧 Contrôleur généré (`RegistrationController`)
```php
#[Route('/register', name: 'app_register')]
public function register(Request $request, UserPasswordHasherInterface $passwordHasher, EntityManagerInterface $em, Security $security): Response
{
    $user = new User();
    $form = $this->createForm(RegistrationFormType::class, $user);
    $form->handleRequest($request);

    if ($form->isSubmitted() && $form->isValid()) {
        $user->setPassword(
            $passwordHasher->hashPassword(
                $user,
                $form->get('plainPassword')->getData()
            )
        );
        $em->persist($user);
        $em->flush();

        return $security->login($user, 'form_login', 'main');
    }

    return $this->render('registration/register.html.twig', [
        'registrationForm' => $form->createView(),
    ]);
}
```

### 🧩 Formulaire `RegistrationFormType`
```php
$builder
    ->add('email', EmailType::class)
    ->add('plainPassword', PasswordType::class, [
        'mapped' => false,
        'constraints' => [...]
    ])
```

---

## 4. 🔓 Formulaire de Login

### 🛠 Commande CLI
```bash
symfony console make:security:form-login
```

### 🧾 Configuration ajoutée (dans `security.yaml`)
```yaml
firewalls:
  main:
    form_login:
      login_path: app_login
      check_path: app_login
    logout:
      path: app_logout
```

### 🎯 Contrôleur `LoginController`
```php
#[Route('/login', name: 'app_login')]
public function login(AuthenticationUtils $authUtils): Response
{
    return $this->render('security/login.html.twig', [
        'last_username' => $authUtils->getLastUsername(),
        'error' => $authUtils->getLastAuthenticationError(),
    ]);
}
```

---

## 5. 🛡 Rôles et Hiérarchie

### 🌐 Définir la hiérarchie des rôles
```yaml
security:
  role_hierarchy:
    ROLE_MODERATOR: ROLE_USER
    ROLE_ADMIN: [ROLE_MODERATOR, ROLE_DEVELOPER]
```

### ✅ Assigner des rôles en base
```php
$user->setRoles(['ROLE_ADMIN']);
```

---

## 6. 🧪 Tester l’authentification

- Accéder à `/register` → créer un utilisateur
- Aller à `/login` → se connecter
- Utiliser la barre Symfony Web Debug Toolbar pour vérifier l’état d’auth

---

## 🔁 Résumé CLI utile
```bash
symfony new mon-projet --webapp
cd mon-projet
symfony console make:user
symfony console make:registration-form
symfony console make:security:form-login
symfony console make:migration
symfony console doctrine:migrations:migrate
symfony serve -d
```

---

[...retorn en rèire](../menu.md)
