# Doctrine ORM

[Retour menu Symfony](../menu.md)

---

Doctrine ORM est un composant essentiel pour gérer la persistance des données dans Symfony. Il permet de manipuler des objets PHP tout en interagissant avec une base de données relationnelle.

---

## **1. Introduction à Doctrine**

### **1.1 Qu’est-ce qu’un ORM ?**

Un **ORM (Object-Relational Mapping)** est une technique qui permet de convertir des objets en tables relationnelles et vice-versa. Plutôt que d’écrire des requêtes SQL, les développeurs manipulent des objets métier.

Avantages :
- **Abstraction des requêtes SQL** : Pas besoin d'écrire directement du SQL pour les opérations courantes.
- **Facilité de maintenance** : Les modifications du modèle métier sont reflétées dans la base via les entités.
- **Portabilité** : Fonctionne avec plusieurs systèmes de bases de données (MySQL, PostgreSQL, SQLite, etc.).

### **1.2 Pourquoi utiliser Doctrine avec Symfony ?**

Doctrine est l’ORM par défaut dans Symfony et s’intègre parfaitement avec le framework. 

- **Automatisation** : Génère automatiquement les tables à partir des entités.
- **Gestion des relations** : Gère les relations complexes entre entités (OneToOne, OneToMany, ManyToMany).
- **Migrations** : Facilite la mise à jour de la structure de la base de données.

---

## **2. Création et gestion des entités**

### **2.1 Déclaration des entités et leurs attributs**

Une **entité** représente une table dans la base de données. Chaque entité est une classe PHP mappée à une table via des annotations ou des attributs PHP.

#### Générer une entité avec Symfony CLI :

```bash
symfony console make:entity NomEntite
```

L’assistant demande d’ajouter des champs :

```bash
Nouveau champ (e.g. name): nom
Type de champ (e.g. string) [string]: string
```

Exemple d’entité `User` avec des attributs :

```php
namespace App\Entity;

use Doctrine\ORM\Mapping as ORM;

#[ORM\Entity]
class User
{
    #[ORM\Id]
    #[ORM\GeneratedValue]
    #[ORM\Column(type: 'integer')]
    private int $id;

    #[ORM\Column(type: 'string', length: 100)]
    private string $nom;

    #[ORM\Column(type: 'string', unique: true)]
    private string $email;

    public function getId(): int
    {
        return $this->id;
    }

    public function getNom(): string
    {
        return $this->nom;
    }

    public function setNom(string $nom): self
    {
        $this->nom = $nom;
        return $this;
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
}
```

---

### **2.2 Relations entre entités**

Doctrine permet de gérer plusieurs types de relations.

#### **OneToOne** (1:1)

Relation où chaque entité est liée à une autre entité unique.

Exemple : Un **Profil** est associé à un **User**.

```php
#[ORM\OneToOne(targetEntity: Profil::class, cascade: ['persist', 'remove'])]
#[ORM\JoinColumn(nullable: false)]
private Profil $profil;
```

#### **OneToMany** (1:N)

Relation où une entité est liée à plusieurs autres.

Exemple : Un **User** possède plusieurs **Articles**.

```php
#[ORM\OneToMany(mappedBy: 'user', targetEntity: Article::class, cascade: ['persist', 'remove'])]
private Collection $articles;
```

#### **ManyToMany** (N:N)

Relation où plusieurs entités sont liées à plusieurs autres.

Exemple : Un **User** peut appartenir à plusieurs **Groupes**, et chaque **Groupe** peut contenir plusieurs **Users**.

```php
#[ORM\ManyToMany(targetEntity: Groupe::class, inversedBy: 'users')]
#[ORM\JoinTable(name: 'user_groupe')]
private Collection $groupes;
```

---

## **3. Repositories**

Les **repositories** permettent d’interagir avec la base de données via des requêtes spécifiques.

### **3.1 Création de repositories personnalisés**

Lors de la création d’une entité, Symfony génère automatiquement un repository associé dans `src/Repository`.

Exemple : `UserRepository`

```php
namespace App\Repository;

use App\Entity\User;
use Doctrine\Bundle\DoctrineBundle\Repository\ServiceEntityRepository;
use Doctrine\Persistence\ManagerRegistry;

class UserRepository extends ServiceEntityRepository
{
    public function __construct(ManagerRegistry $registry)
    {
        parent::__construct($registry, User::class);
    }

    public function findByEmail(string $email): ?User
    {
        return $this->createQueryBuilder('u')
            ->andWhere('u.email = :email')
            ->setParameter('email', $email)
            ->getQuery()
            ->getOneOrNullResult();
    }
}
```

### **3.2 Utilisation des requêtes DQL et QueryBuilder**

- **DQL (Doctrine Query Language)** : Un langage de requête orienté objet.
- **QueryBuilder** : Permet de construire des requêtes dynamiques.

Exemple avec QueryBuilder :

```php
$users = $this->createQueryBuilder('u')
    ->where('u.nom LIKE :nom')
    ->setParameter('nom', 'John%')
    ->orderBy('u.id', 'DESC')
    ->getQuery()
    ->getResult();
```

---

## **4. Migrations de base de données**

Les **migrations** permettent de synchroniser la structure de la base avec les entités Doctrine.

### **4.1 Génération de migrations**

Après avoir modifié ou ajouté des entités, générer une migration pour refléter ces changements.

```bash
symfony console make:migration
```

Un fichier est créé dans le dossier `migrations/` contenant les instructions SQL nécessaires.

### **4.2 Exécution des migrations**

Pour appliquer les migrations et mettre à jour la base de données :

```bash
symfony console doctrine:migrations:migrate
```

### **4.3 Vérification des migrations**

Lister les migrations appliquées et en attente :

```bash
symfony console doctrine:migrations:status
```

### **4.4 Annuler une migration**

Pour revenir à une migration précédente :

```bash
symfony console doctrine:migrations:rollback
```

---

[Retour menu Symfony](../menu.md)