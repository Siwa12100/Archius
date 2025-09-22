# Tests

[...retour menu symphony](../menu.md)

---

## **1. Types de tests dans Symfony**

### **1.1 Tests unitaires**

Les **tests unitaires** visent à valider le comportement d'une unité de code isolée, généralement une méthode ou une classe. Ils garantissent que les composants de base fonctionnent comme prévu.

#### Objectifs des tests unitaires :
- Valider la **logique métier** de manière isolée.
- Garantir la robustesse des **services**, **utilitaires** ou autres classes métiers.
- Prévenir les régressions après des modifications de code.

##### Exemple : Tester une méthode de calcul dans un service.

### **1.2 Tests fonctionnels**

Les **tests fonctionnels** vérifient le comportement global de l'application en simulant des interactions utilisateur ou des requêtes HTTP.

#### Objectifs des tests fonctionnels :
- Vérifier l'intégration des différents composants (contrôleurs, services, bases de données).
- Tester les **endpoints** de l'API.
- S'assurer que les routes et les réponses HTTP sont correctes.

##### Exemple : Tester un endpoint de création d’utilisateur.

---

## **2. Mise en place des tests**

### **2.1 Installation de PHPUnit**

PHPUnit est un framework de tests en PHP utilisé par Symfony.

#### Installation via Composer :

```bash
composer require --dev phpunit/phpunit
```

Symfony fournit un environnement de test préconfiguré, avec des fichiers par défaut dans `phpunit.xml.dist` et un répertoire `tests/`.

### **2.2 Création de tests unitaires**

Les **tests unitaires** permettent de valider le comportement des classes et méthodes.

#### Exemple : Tester un service

Service `DiscountService.php` dans `src/Service/` :

```php
namespace App\Service;

class DiscountService
{
    public function calculateDiscount(float $price, float $discount): float
    {
        if ($discount < 0 || $discount > 100) {
            throw new \InvalidArgumentException('Le pourcentage de réduction doit être entre 0 et 100.');
        }

        return $price - ($price * $discount / 100);
    }
}
```

##### Création du test dans `tests/Service/DiscountServiceTest.php` :

```php
namespace App\Tests\Service;

use App\Service\DiscountService;
use PHPUnit\Framework\TestCase;

class DiscountServiceTest extends TestCase
{
    private DiscountService $discountService;

    protected function setUp(): void
    {
        $this->discountService = new DiscountService();
    }

    public function testCalculateDiscount(): void
    {
        $result = $this->discountService->calculateDiscount(100, 20);
        $this->assertEquals(80, $result);
    }

    public function testCalculateDiscountWithInvalidPercentage(): void
    {
        $this->expectException(\InvalidArgumentException::class);
        $this->discountService->calculateDiscount(100, 120);
    }
}
```

##### Explications :
- **`setUp()`** : Initialise les dépendances avant chaque test.
- **`assertEquals`** : Vérifie que le résultat attendu est correct.
- **`expectException`** : Vérifie qu’une exception est levée dans certaines conditions.

---

### **2.3 Création de tests fonctionnels**

Les **tests fonctionnels** simulent des requêtes HTTP pour valider le comportement des **contrôleurs** et **routes**.

#### Exemple : Tester un contrôleur d'API

Méthode de contrôleur `UserController` :

```php
namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\Routing\Annotation\Route;

class UserController extends AbstractController
{
    #[Route('/users/{id}', name: 'user_detail', methods: ['GET'])]
    public function detail(int $id): JsonResponse
    {
        // Simuler un utilisateur
        $user = ['id' => $id, 'name' => 'John Doe'];
        return $this->json($user);
    }
}
```

##### Création du test fonctionnel dans `tests/Controller/UserControllerTest.php` :

```php
namespace App\Tests\Controller;

use Symfony\Bundle\FrameworkBundle\Test\WebTestCase;

class UserControllerTest extends WebTestCase
{
    public function testGetUserDetail(): void
    {
        $client = static::createClient();
        $client->request('GET', '/users/1');

        $this->assertResponseIsSuccessful();
        $this->assertJson($client->getResponse()->getContent());

        $data = json_decode($client->getResponse()->getContent(), true);
        $this->assertEquals(1, $data['id']);
        $this->assertEquals('John Doe', $data['name']);
    }
}
```

##### Explications :
- **`createClient()`** : Simule un client HTTP.
- **`assertResponseIsSuccessful()`** : Vérifie que le statut HTTP est `200`.
- **`assertJson()`** : Valide que la réponse est bien au format JSON.
- **`json_decode`** : Convertit la réponse JSON en tableau pour des vérifications détaillées.

---

## **3. Commandes pour exécuter les tests**

### **3.1 Exécution des tests avec la CLI**

Pour exécuter tous les tests de l'application :

```bash
php bin/phpunit
```

Exécuter un fichier de test spécifique :

```bash
php bin/phpunit tests/Service/DiscountServiceTest.php
```

Exécuter un test spécifique dans un fichier :

```bash
php bin/phpunit --filter testCalculateDiscount
```

### **3.2 Génération de rapports de couverture**

Les **rapports de couverture** montrent le pourcentage de code couvert par les tests.

#### Installation de Xdebug pour mesurer la couverture :

```bash
sudo apt-get install php-xdebug
```

#### Générer un rapport de couverture HTML :

```bash
php bin/phpunit --coverage-html var/coverage
```

Les résultats sont accessibles dans le dossier `var/coverage`.

#### Exemple de visualisation d'un rapport :

Le rapport affiche les fichiers testés, les lignes couvertes, et les éventuelles lacunes. Cela permet d’identifier les parties du code nécessitant des tests supplémentaires.

---

## **4. Bonnes pratiques pour les tests**

- **Organiser les tests par type** : `tests/Service` pour les tests unitaires, `tests/Controller` pour les tests fonctionnels.
- **Donner des noms explicites aux méthodes de test** : Cela facilite la lecture et la maintenance.
- **Mocker les dépendances** pour isoler les unités testées et éviter les tests longs ou instables.
- **Automatiser l’exécution des tests** dans le pipeline CI/CD pour garantir la qualité en continu.

---

[...retour menu symphony](../menu.md)