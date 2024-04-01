# Springboot côté client

[...retour au sommaire](../sommaire.md)

---

Pour créer un client en Spring Boot qui consomme des services RESTful en utilisant un modèle de `Livre`, vous pouvez utiliser la classe `RestTemplate`. `RestTemplate` offre une approche simple pour faire des requêtes HTTP, y compris GET, POST, PUT, et DELETE. Voici comment implémenter ces opérations, avec des exemples basés sur un modèle de `Livre` ayant des attributs tels que `id`, `titre`, et `auteur`.

## Utilisation de RestTemplate dans Spring Boot

### Prérequis

- Ajoutez la dépendance Spring Web dans votre fichier `pom.xml` pour utiliser `RestTemplate`.

  ```xml
  <dependency>
      <groupId>org.springframework.boot</groupId>
      <artifactId>spring-boot-starter-web</artifactId>
  </dependency>
  ```

### Modèle de Livre

Supposons que vous ayez un modèle de `Livre` comme suit :

```java
public class Livre {
    private Long id;
    private String titre;
    private String auteur;
    // Constructeurs, getters et setters
}
```

## Exemples de Méthodes pour Consommer une API RESTful

### GET - Récupérer un Livre

Pour récupérer un livre par son `id`.

```java
public Livre getLivre(Long id) {
    String url = "http://localhost:8080/api/livres/{id}";
    RestTemplate restTemplate = new RestTemplate();
    return restTemplate.getForObject(url, Livre.class, id);
}
```

### POST - Créer un Livre

Pour créer un nouveau livre.

```java
public Livre createLivre(Livre livre) {
    String url = "http://localhost:8080/api/livres";
    RestTemplate restTemplate = new RestTemplate();
    return restTemplate.postForObject(url, livre, Livre.class);
}
```

### PUT - Mettre à Jour un Livre

Pour mettre à jour un livre existant.

```java
public void updateLivre(Long id, Livre livre) {
    String url = "http://localhost:8080/api/livres/{id}";
    RestTemplate restTemplate = new RestTemplate();
    restTemplate.put(url, livre, id);
}
```

### DELETE - Supprimer un Livre

Pour supprimer un livre par son `id`.

```java
public void deleteLivre(Long id) {
    String url = "http://localhost:8080/api/livres/{id}";
    RestTemplate restTemplate = new RestTemplate();
    restTemplate.delete(url, id);
}
```

## Configuration du RestTemplate

Pour un usage plus avancé, comme l'ajout d'intercepteurs ou la configuration de la sérialisation/désérialisation, vous pouvez configurer `RestTemplate` comme un bean Spring :

```java
@Bean
public RestTemplate restTemplate() {
    return new RestTemplate();
}
```

Cela vous permet d'injecter `RestTemplate` dans vos composants Spring et de profiter de la configuration personnalisée.

## Sécurité

Si l'API que vous consommez nécessite une authentification, `RestTemplate` offre des moyens de configurer des intercepteurs pour ajouter des headers d'authentification à chaque requête. Pour une authentification basique, par exemple :

```java
restTemplate.getInterceptors().add(
    new BasicAuthenticationInterceptor("username", "password"));
```

### Conclusion

`RestTemplate` est un outil puissant et flexible pour créer des clients REST en Spring Boot, permettant de consommer facilement des services web. En utilisant des opérations CRUD de base illustrées avec un modèle de `Livre`, vous pouvez interagir avec n'importe quelle API RESTful. Pour des cas d'utilisation avancés, notamment la gestion des erreurs ou l'authentification, Spring offre des mécanismes supplémentaires pour étendre les capacités de `RestTemplate`.

---

[...retour au sommaire](../sommaire.md)
