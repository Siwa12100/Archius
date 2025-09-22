# Jersey côté API

[...retour au sommaire](../sommaire.md)

---


Voici une version complétée de votre guide pour démarrer avec Jersey, incluant les URL pour appeler chaque méthode CRUD sur une ressource `Livre` à la base de l'URL `https://apiSiwa`.

## Démarrage rapide avec Jersey

### Prérequis

- JDK installé (version 8 ou supérieure)
- Maven pour la gestion des dépendances
- Serveur d'application comme Tomcat ou Payara

### Configuration du projet

1. **Créez un projet Maven** : Vous pouvez utiliser un IDE ou la ligne de commande pour cela. Si vous utilisez la ligne de commande, tapez:

   ```shell
   mvn archetype:generate -DgroupId=com.exemple -DartifactId=projet-jersey -DarchetypeArtifactId=maven-archetype-webapp -DinteractiveMode=false
   ```

2. **Ajoutez les dépendances Jersey** à votre fichier `pom.xml` :

   ```xml
   <dependencies>
       <dependency>
           <groupId>org.glassfish.jersey.containers</groupId>
           <artifactId>jersey-container-servlet</artifactId>
           <version>2.32</version>
       </dependency>
       <dependency>
           <groupId>org.glassfish.jersey.inject</groupId>
           <artifactId>jersey-hk2</artifactId>
           <version>2.32</version>
       </dependency>
       <dependency>
           <groupId>javax.xml.bind</groupId>
           <artifactId>jaxb-api</artifactId>
           <version>2.3.1</version>
       </dependency>
   </dependencies>
   ```

3. **Configurez le servlet de Jersey** dans `web.xml` :

   ```xml
   <servlet>
       <servlet-name>jersey-serlvet</servlet-name>
       <servlet-class>org.glassfish.jersey.servlet.ServletContainer</servlet-class>
       <init-param>
           <param-name>jersey.config.server.provider.packages</param-name>
           <param-value>com.exemple</param-value>
       </init-param>
       <load-on-startup>1</load-on-startup>
   </servlet>
   <servlet-mapping>
       <servlet-name>jersey-serlvet</servlet-name>
       <url-pattern>/api/*</url-pattern>
   </servlet-mapping>
   ```

### Définir une classe de ressource `Livre`

```java
@XmlRootElement
public class Livre {
    private String titre;
    private String description;
    private String contenu;

    // Constructeurs, getters et setters
    public Livre() { }

    public Livre(String titre, String description, String contenu) {
        this.titre = titre;
        this.description = description;
        this.contenu = contenu;
    }

    // Getters et setters
}
```

## Exemples de méthodes CRUD et URL pour les appeler

### READ - GET

```java
@Path("/livres")
public class LivreResource {

    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public Livre getLivre(@QueryParam("titre") String titre) {
        // Simuler la recherche d'un livre par titre
        return new Livre(titre, "Une description", "Le contenu du livre");
    }
}
```

**URL pour appeler cette méthode :** `https://apiSiwa/api/livres?titre=LeTitreDuLivre`

### CREATE - POST

```java
@POST
@Consumes(MediaType.APPLICATION_JSON)
@Produces(MediaType.APPLICATION_JSON)
public Livre createLivre(Livre livre) {
    // Ajouter le livre à la base de données (simulation)
    return livre;
}
```

**URL pour appeler cette méthode :** `https://apiSiwa/api/livres`

### UPDATE - PUT

```java
@PUT
@Path("/{titre}")
@Consumes(MediaType.APPLICATION_JSON)
@Produces(MediaType.APPLICATION_JSON)
public Livre updateLivre(@PathParam("titre") String titre, Livre livre) {
    // Mettre à jour le livre ayant le titre spécifié
    livre.setTitre(titre);
    return livre;
}

```

**URL pour appeler cette méthode :** `https://apiSiwa/api/livres/LeTitreDuLivre`

### DELETE - DELETE

```java
@DELETE
@Path("/{titre}")
public void deleteLivre(@PathParam("titre") String titre) {
    // Supprimer le livre ayant le titre spécifié
}
```

**URL pour appeler cette méthode :** `https://apiSiwa/api/livres/LeTitreDuLivre`

## Lancer le projet

1. **Compilez et packagez** votre application en utilisant Maven :



   ```shell
   mvn package
   ```

2. **Déployez** le fichier `.war` généré dans votre serveur d'application.

3. **Testez** vos endpoints à l'aide d'un outil comme Postman ou Curl.

Ce guide vous donne une introduction complète sur la façon de démarrer avec Jersey pour créer une application Web API RESTful qui gère des ressources `Livre`, y compris les opérations CRUD et les URL pour tester ces opérations.

---

[...retour au sommaire](../sommaire.md)
