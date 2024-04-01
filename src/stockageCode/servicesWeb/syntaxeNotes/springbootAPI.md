# Côté API Springboot

[...retour au sommaire](../sommaire.md)

---

Créer une API REST avec Spring Boot est un processus structuré qui tire parti de la puissance de Spring pour simplifier le développement d'applications web. Voici un guide détaillé, utilisant un modèle `Livre` pour illustrer les opérations CRUD via les méthodes HTTP GET, POST, PUT et DELETE.

## Guide pour Créer une API REST avec Spring Boot

## Prérequis

- JDK 8 ou plus récent.
- Maven ou Gradle pour la gestion de projet.
- Spring Boot.
- Un IDE tel que IntelliJ IDEA, Eclipse, ou Spring Tool Suite.

### Initialisation du Projet

Utilisez [Spring Initializr](https://start.spring.io/) pour générer et télécharger le squelette de votre projet. Sélectionnez les dépendances Web, JPA, H2 (pour la base de données en mémoire lors du développement), et Lombok (pour réduire le boilerplate code).

### Création du Modèle `Livre`

```java
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity
public class Livre {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;
    private String titre;
    private String auteur;
    private String contenu;

    // Constructeurs, getters et setters
}
```

## Création du Repository

```java
import org.springframework.data.repository.CrudRepository;

public interface LivreRepository extends CrudRepository<Livre, Long> {
}
```

## Création du Contrôleur

Le contrôleur gère les requêtes HTTP et interagit avec la couche de service pour exécuter la logique métier.

```java
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/livres")
public class LivreController {

    @Autowired
    private LivreService livreService;

    // Méthodes CRUD
}
```

## Implémentation des Méthodes CRUD

### GET - Lire des Livres

```java
@GetMapping
public Iterable<Livre> getAllLivres() {
    return livreService.findAll();
}

@GetMapping("/{id}")
public Livre getLivreById(@PathVariable Long id) {
    return livreService.findById(id).orElseThrow(() -> new LivreNotFoundException(id));
}
```

#### POST - Créer un Livre

```java
@PostMapping
public Livre createLivre(@RequestBody Livre livre) {
    return livreService.save(livre);
}
```

#### PUT - Mettre à Jour un Livre

```java
@PutMapping("/{id}")
public Livre updateLivre(@RequestBody Livre newLivre, @PathVariable Long id) {
    return livreService.findById(id)
        .map(livre -> {
            livre.setTitre(newLivre.getTitre());
            livre.setAuteur(newLivre.getAuteur());
            livre.setContenu(newLivre.getContenu());
            return livreService.save(livre);
        })
        .orElseGet(() -> {
            newLivre.setId(id);
            return livreService.save(newLivre);
        });
}
```

### DELETE - Supprimer un Livre

```java
@DeleteMapping("/{id}")
public void deleteLivre(@PathVariable Long id) {
    livreService.deleteById(id);
}
```

## Gestion des Exceptions

Utilisez `@ControllerAdvice` pour gérer les exceptions globalement.

```java
@ControllerAdvice
public class LivreNotFoundAdvice {
    @ResponseBody
    @ExceptionHandler(LivreNotFoundException.class)
    @ResponseStatus(HttpStatus.NOT_FOUND)
    String livreNotFoundHandler(LivreNotFoundException ex) {
        return ex.getMessage();
    }
}
```

## Configuration de l'Application

Configurez votre `application.properties` pour personnaliser le comportement de l'application, comme la connexion à la base de données.

```properties
spring.datasource.url=jdbc:h2:mem:testdb
spring.datasource.driverClassName=org.h2.Driver
spring.datasource.username=sa
spring.datasource.password=password
spring.jpa.database-platform=org.hibernate.dialect.H2Dialect
```

## Lancement de l'Application

Exécutez la classe principale de votre application Spring Boot pour démarrer votre application. Spring Boot se chargera de configurer et de lancer le serveur web embarqué, rendant votre API disponible pour les requêtes HTTP.

```java
@SpringBootApplication
public class Application {
    public static void main(String[] args) {
        SpringApplication.run(Application.class, args);
    }
}
```

## Création d'Exceptions Personnalisées

1. **Définir une Exception Personnalisée**

   Créez une classe d'exception personnalisée pour gérer des situations spécifiques, par exemple `LivreNotFoundException`.

   ```java
   public class LivreNotFoundException extends RuntimeException {
       public LivreNotFoundException(Long id) {
           super("Livre non trouvé : " + id);
       }
   }
   ```

## Gestion des Codes d'Erreur

2. **Gestion Globale des Exceptions**

   Utilisez `@ControllerAdvice` pour gérer les exceptions de manière globale. Vous pouvez ensuite utiliser `@ExceptionHandler` pour capturer des types d'exceptions spécifiques et renvoyer les bons codes d'erreur.

   ```java
   @ControllerAdvice
   public class LivreExceptionAdvice {

       @ResponseBody
       @ExceptionHandler(LivreNotFoundException.class)
       @ResponseStatus(HttpStatus.NOT_FOUND)
       public String livreNotFoundHandler(LivreNotFoundException ex) {
           return ex.getMessage();
       }
   }
   ```

Cette configuration assure que lorsque `LivreNotFoundException` est levée, un code d'erreur 404 (Not Found) est automatiquement renvoyé avec un message d'erreur pertinent.

## Implémentation de HATEOAS

3. **Ajouter des Liens avec HATEOAS**

   Spring HATEOAS fournit des abstractions pour créer des liens Web suivant les principes HATEOAS. L'idée est d'enrichir les réponses avec des liens vers d'autres ressources pertinentes.

   - **Ajoutez la dépendance Spring HATEOAS dans votre `pom.xml` :**

     ```xml
     <dependency>
         <groupId>org.springframework.boot</groupId>
         <artifactId>spring-boot-starter-hateoas</artifactId>
     </dependency>
     ```

   - **Enrichissez vos objets de réponse avec des liens :**

     Modifiez le contrôleur pour retourner des ressources enrichies de liens. Utilisez `EntityModel<T>`, `CollectionModel<T>`, et `WebMvcLinkBuilder` pour construire les liens.

     ```java
     @RestController
     public class LivreController {

         @GetMapping("/livres/{id}")
         public EntityModel<Livre> one(@PathVariable Long id) {
             Livre livre = repository.findById(id)
                     .orElseThrow(() -> new LivreNotFoundException(id));

             return EntityModel.of(livre,
                     linkTo(methodOn(LivreController.class).one(id)).withSelfRel(),
                     linkTo(methodOn(LivreController.class).all()).withRel("livres"));
         }
     }
     ```

Dans cet exemple, `EntityModel<Livre>` encapsule un `Livre` et ses liens associés. `linkTo` et `methodOn` sont utilisés pour construire des liens vers d'autres méthodes du contrôleur, rendant votre API plus navigable et découvrable.

---

[...retour au sommaire](../sommaire.md)
