# Services Web RESTful

[...retour au sommaire](../sommaire.md)

---

[--> Diapos du cours](../diapos/cours-rest.pdf)

## Introduction

Les services web sont des instances d'objets ou de ressources déployées sur Internet, facilitant l'accès à des fonctionnalités pour diverses applications. Cette technologie représente une évolution des systèmes distribués, adoptant une architecture orientée service (SOA) et normalisée par le W3C.

### Caractéristiques des Services Web

- **Langage et Plateforme Agnostiques** : Un service web peut être développé dans n'importe quel langage et sur n'importe quelle plateforme.
- **Interopérabilité** : Il doit être invoqué par n'importe quel autre service.

### Évolution Technologique

Avant l'avènement des services web, des technologies telles que RMI et Corba étaient utilisées, impliquant l'utilisation de démons particuliers. Les services web préfèrent l'utilisation de serveurs web sur différentes plateformes, avec les messages transitant par le protocole HTTP. L'utilisation de ports ouverts facilite l'accessibilité sans problème de pare-feu.

### Utilisation d'HTTP

- **Protocole Internet** : Couramment utilisé pour les services web.
- **Accepté par les Pare-feu** : Généralement autorisé, garantissant une accessibilité plus aisée.
- **Disponibilité Universelle** : Présent sur toutes les plateformes.
- **Mode Non Connecté** : Fonctionne de manière asynchrone.

### Sérialisation des Données

Les requêtes et réponses des services web doivent être sérialisées pour une réutilisation efficace. Les formats courants incluent XML et JSON.

### Types d'Architectures de Services Web

1. **Services SOAP (Simple Object Access Protocol)**
   - Protocole basé sur XML.
   - Utilise HTTP pour le transfert des données.
   - Introduit une certaine complexité mais offre des normes de sécurité.

2. **Services REST (Representational State Transfer)**
   - Transfert sur HTTP sans protocole XML implicite.
   - Stateless : Les services n'ont pas de mémoire des requêtes précédentes.
   - Utilisation directe des méthodes HTTP (PUT, GET, etc.).
   - Plus simple à mettre en œuvre, bien qu'initialement sans protocole de sécurité standard.

## RESTful Web Services

### Bonnes Pratiques HTTP

- Utilisation des méthodes HTTP :
  - **POST :** Ajout
  - **PUT :** Modification
  - **GET :** Lecture
  - **DELETE :** Suppression

### HATEOAS (Hypermedia as the Engine of Application State)

- Contrainte de l'architecture REST :
  - Le client doit connaître en permanence les ressources qu'il peut appeler.

### Exemple d'une Requête GET

```http
GET /account/12345 HTTP/1.1
Host: somebank.org
Accept: application/xml
```

### Réponse à la Requête GET

```http
HTTP/1.1 200 OK
Content-Type: application/xml
Content-Length: ...

<?xml version="1.0"?>
<account>
  <account_number>12345</account_number>
  <balance currency="usd">100.00</balance>
  <link rel="deposit" href="/account/12345/deposit" />
  <link rel="withdraw" href="/account/12345/withdraw" />
  <link rel="transfer" href="/account/12345/transfer" />
  <link rel="close" href="/account/12345/close" />
</account>
```

## Micro-Services

Les micro-services représentent une approche de développement logiciel où une application est construite comme une collection de petits services indépendants, chacun dédié à une fonctionnalité spécifique. Plusieurs frameworks légers facilitent la mise en œuvre de micro-services, parmi lesquels on retrouve :

- Dropwizard
- Spring Boot
- Spotify Apollo
- Spark (Java)
- Kumuluzee (J2EE)
- Flask (Python)
- Sinatra (Ruby)
- Vert.x (Polyglotte)

Deux frameworks spécifiques, **Micronaut** et **Quarkus**, sont également mentionnés.

### Micro-Services et Web Service Composition

La composition de services web implique l'interaction de plusieurs services web entre eux, qu'ils soient déployés sur le même serveur ou sur différents serveurs/clouds. On distingue deux types de composition :

1. **Orchestration**
2. **Chorégraphie**

### Gestion des Erreurs dans la Composition

La gestion des erreurs dans la composition de services web peut être complexe. En cas d'erreur remontée par un composant, elle doit être traitée par le service appelant pour un retour approprié vers le client.

### Orchestration des Services

L'orchestration des services web se produit lorsqu'un service web coordonne l'exécution d'autres services. Cela implique la création d'un processus global avec des appels vers d'autres services et la gestion des erreurs. Les compositions peuvent être simples en utilisant des langages tels que Java, mais dans des cas plus complexes, des méta-langages comme BPEL (Business Process Execution Language) sont nécessaires.

### Langages BPMN / BPEL

BPEL est un langage basé sur XML qui décrit comment les services web interagissent en suivant des stimuli extérieurs. La gestion des erreurs est intégrée au processus, avec des mécanismes de repli et de re-exécution du processus.

### Caractéristiques du Langage BPEL

- Définition des partenaires
- Utilisation de variables, assignation de valeurs (assign)
- Activités basiques (invoque, receive, reply, wait, throw)
- Activités structurées (while, switch, sequence, pick (temporisation))
- Corrélation = session
- Scope : découpage d'un processus en plusieurs parties
- Manipulation des gestionnaires (compensation, fault, event)

---

[...retour au sommaire](../sommaire.md)