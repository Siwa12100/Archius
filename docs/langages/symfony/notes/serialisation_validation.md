# Validation et Sérialisation

[Retour menu Symfony](../menu.md)

---

## **1. Validation des données**

Symfony fournit un système de **validation** robuste via le composant **Validator**. Ce système permet de s'assurer que les données respectent des contraintes spécifiques.

### **1.1 Ajout de contraintes dans les entités**

Les contraintes de validation sont définies directement dans les entités, sous forme d'**annotations** ou d'**attributs PHP**.

#### Exemple d'entité avec contraintes :

```php
namespace App\Entity;

use Doctrine\ORM\Mapping as ORM;
use Symfony\Component\Validator\Constraints as Assert;

#[ORM\Entity]
class User
{
    #[ORM\Id]
    #[ORM\GeneratedValue]
    #[ORM\Column(type: 'integer')]
    private int $id;

    #[ORM\Column(type: 'string', length: 100)]
    #[Assert\NotBlank(message: "Le nom ne peut pas être vide.")]
    #[Assert\Length(
        min: 3,
        max: 100,
        minMessage: "Le nom doit comporter au moins {{ limit }} caractères.",
        maxMessage: "Le nom ne peut pas dépasser {{ limit }} caractères."
    )]
    private string $nom;

    #[ORM\Column(type: 'string', unique: true)]
    #[Assert\NotBlank]
    #[Assert\Email(message: "L'adresse email '{{ value }}' n'est pas valide.")]
    private string $email;

    #[ORM\Column(type: 'integer')]
    #[Assert\Positive(message: "L'âge doit être un nombre positif.")]
    #[Assert\LessThan(
        value: 150,
        message: "L'âge doit être inférieur à {{ compared_value }}."
    )]
    private int $age;

    // Getters et setters...
}
```

### **1.2 Contraintes disponibles dans Symfony**

Symfony propose un grand nombre de contraintes pour valider les données. Voici les principales :

#### **Contraintes de base :**
- **`NotBlank`** : Vérifie que la valeur n'est pas vide.
- **`NotNull`** : Vérifie que la valeur n'est pas `null`.
- **`Length`** : Vérifie la longueur d'une chaîne.
- **`Email`** : Valide un email.
- **`Positive`** et **`Negative`** : Vérifie si un nombre est positif ou négatif.
- **`Regex`** : Valide une chaîne en fonction d'une expression régulière.

#### **Contraintes numériques :**
- **`Range`** : Vérifie si un nombre est compris entre deux valeurs.
- **`GreaterThan`**, **`LessThan`** : Compare la valeur à un seuil.
- **`DivisibleBy`** : Vérifie si un nombre est divisible par un autre.

#### **Contraintes de comparaison :**
- **`EqualTo`**, **`NotEqualTo`** : Compare la valeur avec une autre.
- **`IdenticalTo`**, **`NotIdenticalTo`** : Vérifie si deux valeurs sont strictement identiques ou non.

#### **Contraintes liées aux dates :**
- **`Date`** : Valide une date.
- **`DateTime`** : Valide une date et une heure.
- **`Time`** : Valide une heure.
- **`LessThanOrEqual`** et **`GreaterThanOrEqual`** peuvent également être appliquées aux dates.

#### **Contraintes de collection :**
- **`Collection`** : Valide un tableau de valeurs avec des contraintes sur chaque clé.

Exemple :

```php
$constraints = new Assert\Collection([
    'nom' => new Assert\Length(['min' => 3]),
    'email' => new Assert\Email(),
    'age' => new Assert\Positive()
]);
```

#### **Contraintes personnalisées :**
Créer des contraintes personnalisées en cas de besoin spécifique.

---

### **1.3 Utilisation du composant Validator**

Le service **ValidatorInterface** permet de valider des objets ou des tableaux de données.

#### Validation d'une entité :

```php
use Symfony\Component\Validator\Validator\ValidatorInterface;
use Symfony\Component\HttpFoundation\JsonResponse;

$errors = $validator->validate($user);

if (count($errors) > 0) {
    $errorMessages = [];
    foreach ($errors as $error) {
        $errorMessages[] = $error->getPropertyPath() . ': ' . $error->getMessage();
    }

    return new JsonResponse(['errors' => $errorMessages], 400);
}
```

#### Validation d'un tableau de données :

```php
use Symfony\Component\Validator\Constraints as Assert;

$data = ['email' => 'invalid-email'];
$constraints = new Assert\Collection([
    'email' => [new Assert\NotBlank(), new Assert\Email()]
]);

$errors = $validator->validate($data, $constraints);
```

---

## **2. Sérialisation des entités**

La **sérialisation** permet de convertir des objets PHP en **JSON** ou **XML** pour les transmettre dans des réponses HTTP, et inversement.

### **2.1 Introduction au Serializer**

Le composant **Serializer** de Symfony offre des outils pour transformer des objets en formats standardisés et vice-versa.

#### Installation du Serializer :

```bash
composer require symfony/serializer
```

### **2.2 Conversion d’objets en JSON/XML**

#### Sérialiser un objet en JSON :

```php
use Symfony\Component\Serializer\SerializerInterface;
use Symfony\Component\HttpFoundation\JsonResponse;

class UserController
{
    private SerializerInterface $serializer;

    public function __construct(SerializerInterface $serializer)
    {
        $this->serializer = $serializer;
    }

    #[Route('/users/{id}', methods: ['GET'])]
    public function detail(int $id): JsonResponse
    {
        $user = $this->getUserById($id); // Exemple
        $jsonData = $this->serializer->serialize($user, 'json');

        return new JsonResponse($jsonData, 200, [], true);
    }
}
```

#### Désérialiser un JSON en objet :

```php
$jsonData = '{"nom": "John Doe", "email": "john@example.com", "age": 30}';
$user = $serializer->deserialize($jsonData, User::class, 'json');
```

---

### **2.3 Personnalisation des groupes de sérialisation**

Les groupes de sérialisation permettent de personnaliser les données exposées selon le contexte.

#### Définir des groupes :

```php
use Symfony\Component\Serializer\Annotation\Groups;

class User
{
    #[Groups(['user:read'])]
    private int $id;

    #[Groups(['user:read', 'user:write'])]
    private string $nom;

    #[Groups(['user:read', 'user:write'])]
    private string $email;

    #[Groups(['user:write'])]
    private int $age;

    // Getters et setters...
}
```

#### Utiliser des groupes dans le Serializer :

```php
$jsonData = $serializer->serialize($user, 'json', ['groups' => 'user:read']);
```

---

[Retour menu Symfony](../menu.md)