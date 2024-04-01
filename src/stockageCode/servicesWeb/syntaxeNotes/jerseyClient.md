# Jersey côté client

[...retour au sommaire](../sommaire.md)

---

Voici un guide complet utilisant Markdown pour créer un client en Jersey, illustré par des méthodes CRUD sur une ressource modèle `Livre`. Ce guide comprend des exemples de code pour chaque verbe HTTP (GET, POST, PUT, DELETE) avec des paramètres, ainsi que des explications sur la configuration nécessaire pour un client Jersey.

## Création d'un Client en Jersey

### Prérequis

Pour travailler avec un client Jersey, vous aurez besoin de :

- JDK (Java Development Kit), version 8 ou ultérieure.
- Un environnement de développement intégré (IDE) ou un éditeur de texte.
- Maven pour la gestion des dépendances.
- L'accès à une API RESTful. Pour cet exemple, nous utiliserons une ressource `Livre`.

### Configuration de Maven

Ajoutez la dépendance Jersey Client à votre fichier `pom.xml` :

```xml
<dependencies>
    <dependency>
        <groupId>org.glassfish.jersey.core</groupId>
        <artifactId>jersey-client</artifactId>
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

### Exemples de Méthodes CRUD

#### GET

Récupère un `Livre` par son titre.

```java
public class LivreClient {
    private Client client = ClientBuilder.newClient();
    private String BASE_URI = "https://apiSiwa/livres";

    public String getLivre(String titre) {
        WebTarget webTarget = client.target(BASE_URI).queryParam("titre", titre);
        Invocation.Builder invocationBuilder = webTarget.request(MediaType.APPLICATION_JSON);
        Response response = invocationBuilder.get();
        if (response.getStatus() == 200) {
            return response.readEntity(String.class);
        } else {
            return "Livre non trouvé";
        }
    }
}
```

#### POST

Crée un nouveau `Livre`.

```java
public void createLivre(Livre livre) {
    WebTarget webTarget = client.target(BASE_URI);
    Invocation.Builder invocationBuilder = webTarget.request(MediaType.APPLICATION_JSON);
    Response response = invocationBuilder.post(Entity.entity(livre, MediaType.APPLICATION_JSON));
    System.out.println("Status: " + response.getStatus());
}
```

#### PUT

Met à jour un `Livre` existant.

```java
public void updateLivre(String titre, Livre livre) {
    WebTarget webTarget = client.target(BASE_URI).path(titre);
    Invocation.Builder invocationBuilder = webTarget.request(MediaType.APPLICATION_JSON);
    Response response = invocationBuilder.put(Entity.entity(livre, MediaType.APPLICATION_JSON));
    System.out.println("Status: " + response.getStatus());
}
```

#### DELETE

Supprime un `Livre` par son titre.

```java
public void deleteLivre(String titre) {
    WebTarget webTarget = client.target(BASE_URI).path(titre);
    Invocation.Builder invocationBuilder = webTarget.request();
    Response response = invocationBuilder.delete();
    System.out.println("Status: " + response.getStatus());
}
```

### Configuration du Client Jersey

Pour des besoins avancés comme l'authentification ou la configuration SSL, Jersey permet de personnaliser le client :

```java
ClientConfig clientConfig = new ClientConfig();
clientConfig.register(MyClientResponseFilter.class);
clientConfig.register(new AnotherClientFilter());
Client client = ClientBuilder.newClient(clientConfig);
```

Pour l'**authentification HTTP Basic** :

```java
HttpAuthenticationFeature feature = HttpAuthenticationFeature.basic("user", "password");
client.register(feature);
```

Pour la **configuration SSL** :

```java
SSLContext sslContext = SSLContext.getInstance("TLSv1.2");
sslContext.init(null, new TrustManager[]{new X509TrustManager() {
    // Implémentation des méthodes de gestionnaire de confiance
}}, new SecureRandom());
client = ClientBuilder.newBuilder().sslContext(sslContext).build();
```

### Utilisation

Pour utiliser le client `LivreClient`, instanciez-le et appelez la méthode désirée :

```java
public static void main(String[] args) {
    LivreClient client = new LivreClient();
    Livre livre = new Livre("Titre", "Description", "Contenu");
    
    // Exemple d'appel de méthode
    client.createLivre(livre);
}
```

Ce guide offre une vue d'ensemble sur la façon de construire et d'utiliser un

 client Jersey pour interagir avec une API RESTful, en prenant l'exemple d'une ressource `Livre`.


---

[...retour au sommaire](../sommaire.md)