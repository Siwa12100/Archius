# Déployer et Configurer NGINX en Conteneur avec Docker Compose pour HTTPS

[Menu VPS](../menu.md)

---

## Sommaire

- [Déployer et Configurer NGINX en Conteneur avec Docker Compose pour HTTPS](#déployer-et-configurer-nginx-en-conteneur-avec-docker-compose-pour-https)
  - [Sommaire](#sommaire)
  - [Introduction](#introduction)
    - [Objectif de cette documentation](#objectif-de-cette-documentation)
    - [Pourquoi déployer NGINX dans un conteneur Docker ?](#pourquoi-déployer-nginx-dans-un-conteneur-docker-)
    - [Qu'est-ce qu'un reverse proxy et son rôle dans notre cas ?](#quest-ce-quun-reverse-proxy-et-son-rôle-dans-notre-cas-)
      - [Dans notre cas, le reverse proxy (NGINX) va :](#dans-notre-cas-le-reverse-proxy-nginx-va-)
  - [Déploiement de NGINX en Conteneur Docker](#déploiement-de-nginx-en-conteneur-docker)
    - [1. Configuration de Docker Compose](#1-configuration-de-docker-compose)
    - [Explications du fichier](#explications-du-fichier)
      - [1. **nginx-proxy**](#1-nginx-proxy)
      - [2. **letsencrypt-companion**](#2-letsencrypt-companion)
      - [3. **webapp**](#3-webapp)
    - [2. Démarrage des Conteneurs](#2-démarrage-des-conteneurs)
  - [Exécute les conteneurs en arrière-plan. Vous gardez le terminal disponible tout en maintenant les services actifs.](#exécute-les-conteneurs-en-arrière-plan-vous-gardez-le-terminal-disponible-tout-en-maintenant-les-services-actifs)
  - [Configurer NGINX en Proxy Inverse](#configurer-nginx-en-proxy-inverse)
  - [Activer HTTPS avec Let's Encrypt](#activer-https-avec-lets-encrypt)
    - [Fonctionnement](#fonctionnement)
    - [Vérification](#vérification)
    - [Avertissement](#avertissement)
  - [Ajouter un Nouveau Conteneur avec HTTPS : Explications selon Divers Scénarios](#ajouter-un-nouveau-conteneur-avec-https--explications-selon-divers-scénarios)
    - [Notions Clés Avant de Commencer](#notions-clés-avant-de-commencer)
    - [Scénario 1 : Ajouter un Nouveau Service dans le Même Docker Compose que NGINX Proxy](#scénario-1--ajouter-un-nouveau-service-dans-le-même-docker-compose-que-nginx-proxy)
      - [Exemple de Configuration](#exemple-de-configuration)
      - [Explication Logique](#explication-logique)
    - [Scénario 2 : Sécuriser un Conteneur Lancé Indépendamment, sans Docker Compose](#scénario-2--sécuriser-un-conteneur-lancé-indépendamment-sans-docker-compose)
      - [Étapes à Suivre](#étapes-à-suivre)
      - [Explication Logique](#explication-logique-1)
    - [Scénario 3 : Sécuriser un Service d’un Autre Docker Compose](#scénario-3--sécuriser-un-service-dun-autre-docker-compose)
      - [Étapes à Suivre](#étapes-à-suivre-1)
      - [Explication Logique](#explication-logique-2)
  - [Dépannage et Vérification](#dépannage-et-vérification)
    - [Vérification des Logs](#vérification-des-logs)
    - [Tester l'accès HTTPS](#tester-laccès-https)

---

## Introduction

Dans cette documentation, nous allons configurer **NGINX** pour qu'il serve de **proxy inverse** à plusieurs applications conteneurisées avec **Docker**, tout en profitant de la sécurité **HTTPS** grâce à **Let's Encrypt**. Le tout sera orchestré avec **Docker Compose**, afin de simplifier la gestion et le déploiement des services.

### Objectif de cette documentation

L'objectif est de :
1. **Déployer NGINX dans un conteneur Docker** pour centraliser la gestion des connexions entrantes.
2. **Configurer NGINX avec Docker Compose** pour gérer les certificats HTTPS.
3. **Permettre à toutes les applications conteneurisées** de bénéficier de connexions sécurisées via HTTPS.

---

### Pourquoi déployer NGINX dans un conteneur Docker ?

Déployer NGINX dans un conteneur Docker offre plusieurs avantages :
- **Isolation** : Chaque service fonctionne indépendamment des autres, réduisant les conflits liés aux dépendances.
- **Portabilité** : La configuration NGINX est facilement transférable d'un serveur à un autre.
- **Simplicité de gestion** : Mettre à jour ou reconfigurer NGINX est plus facile en changeant simplement l'image Docker ou les fichiers de configuration.
- **Intégration avec Docker Compose** : Cela permet de gérer l'ensemble de vos services (backend, frontend, proxy) de manière centralisée et coordonnée.

---

### Qu'est-ce qu'un reverse proxy et son rôle dans notre cas ?

Un **reverse proxy** est un serveur qui agit comme intermédiaire entre les clients (navigateurs, API clients) et les serveurs backend (applications). Voici ses principaux rôles :

1. **Redirection des requêtes** : Le reverse proxy reçoit toutes les requêtes des clients et les redirige vers le service backend approprié.
2. **Sécurité** : Il peut gérer les connexions HTTPS, offrant ainsi un chiffrement des données échangées.
3. **Masquage de l'infrastructure** : Les clients ne voient que le reverse proxy, ce qui cache la structure interne de vos applications.
4. **Optimisation des performances** : Il peut mettre en cache les réponses statiques et réduire la charge sur les serveurs backend.

#### Dans notre cas, le reverse proxy (NGINX) va :
- **Centraliser les connexions HTTPS** pour tous les conteneurs Docker.
- **Simplifier l'accès** en permettant aux utilisateurs de naviguer sur des domaines sécurisés (`https://exemple.com`) sans se soucier des ports internes.
- **Automatiser la gestion des certificats** Let's Encrypt, garantissant des connexions sécurisées sans configuration manuelle complexe.

---

## Déploiement de NGINX en Conteneur Docker

### 1. Configuration de Docker Compose

Créez un fichier **docker-compose.yml** pour déployer NGINX et ses services associés :

```yaml
version: '3.8'

services:
  nginx-proxy:
    image: jwilder/nginx-proxy
    container_name: nginx-proxy
    ports:
      - "80:80"   # Port HTTP
      - "443:443" # Port HTTPS
    volumes:
      - /var/run/docker.sock:/tmp/docker.sock:ro # Nécessaire pour auto-découverte des services
      - ./certs:/etc/nginx/certs                # Certificats SSL
      - ./vhost.d:/etc/nginx/vhost.d            # Configurations spécifiques à chaque service
      - ./html:/usr/share/nginx/html            # Page par défaut
    environment:
      - DEFAULT_HOST=exemple.com    
    networks:
      - nginx-proxy-network
            # Domaine par défaut

  letsencrypt-companion:
    image: jrcs/letsencrypt-nginx-proxy-companion
    container_name: nginx-letsencrypt
    depends_on:
      - nginx-proxy
    environment:
      - NGINX_PROXY_CONTAINER=nginx-proxy
    volumes:
      - /var/run/docker.sock:/var/run/docker.sock:ro
      - ./certs:/etc/nginx/certs
      - ./vhost.d:/etc/nginx/vhost.d
      - ./html:/usr/share/nginx/html
      - ./acme:/etc/acme.sh
    networks:
      - nginx-proxy-network


  webapp:
    image: mon-site-web
    container_name: webapp
    expose:
      - "8080" # Port interne utilisé par l'application
    environment:
      - VIRTUAL_HOST=exemple.com
      - VIRTUAL_PORT=8080
      - LETSENCRYPT_HOST=exemple.com
      - LETSENCRYPT_EMAIL=admin@exemple.com

  fake-service:
    image: nginx:alpine
    container_name: fake-service
    expose:
      - "80"
    environment:
      - VIRTUAL_HOST=devalada.valorium-mc.fr
      - LETSENCRYPT_HOST=devalada.valorium-mc.fr
      - LETSENCRYPT_EMAIL=admin@valorium-mc.fr
    networks:
      - nginx-proxy-network

networks:
  nginx-proxy-network:
    external: true
```

### Explications du fichier

#### 1. **nginx-proxy**

```yaml
services:
  nginx-proxy:
    image: jwilder/nginx-proxy
    container_name: nginx-proxy
    ports:
      - "80:80"   # HTTP
      - "443:443" # HTTPS
    volumes:
      - /var/run/docker.sock:/tmp/docker.sock:ro
      - ./certs:/etc/nginx/certs
      - ./vhost.d:/etc/nginx/vhost.d
      - ./html:/usr/share/nginx/html
    environment:
      - DEFAULT_HOST=exemple.com
```

- **`image: jwilder/nginx-proxy`**  
  Fournit une version préconfigurée de NGINX agissant comme un proxy inverse capable de router dynamiquement les requêtes en fonction des domaines définis.

- **`container_name: nginx-proxy`**  
  Nomme le conteneur pour une meilleure identification lors des diagnostics.

- **`ports`**  
  Ouvre les ports :
  - **80** : Pour les connexions HTTP (utile pour les redirections vers HTTPS ou le challenge Let's Encrypt).
  - **443** : Pour les connexions HTTPS.

- **`volumes`**  
  Permet à NGINX de fonctionner efficacement en partageant des fichiers critiques entre conteneurs :
  1. **`/var/run/docker.sock:/tmp/docker.sock:ro`**  
     Permet à NGINX Proxy de détecter dynamiquement les conteneurs à proxyfier en lisant les événements Docker.
  2. **`./certs:/etc/nginx/certs`**  
     Stocke les certificats SSL/TLS émis par Let's Encrypt.
  3. **`./vhost.d:/etc/nginx/vhost.d`**  
     Contient des configurations spécifiques à chaque domaine/service.
  4. **`./html:/usr/share/nginx/html`**  
     Sert des fichiers statiques pour les pages par défaut (pages d'erreur, de maintenance, etc.).

- **`environment`**  
  - **`DEFAULT_HOST=exemple.com`** : Définit un domaine par défaut à utiliser si aucun domaine explicite n'est spécifié dans les requêtes.

---

#### 2. **letsencrypt-companion**

```yaml
  letsencrypt-companion:
    image: jrcs/letsencrypt-nginx-proxy-companion
    container_name: nginx-letsencrypt
    depends_on:
      - nginx-proxy
    volumes:
      - /var/run/docker.sock:/var/run/docker.sock:ro
      - ./certs:/etc/nginx/certs
      - ./vhost.d:/etc/nginx/vhost.d
      - ./html:/usr/share/nginx/html
```

- **`image: jrcs/letsencrypt-nginx-proxy-companion`**  
  Fournit un service compagnon qui automatise l'obtention et le renouvellement des certificats Let's Encrypt pour les domaines spécifiés.

- **`depends_on`**  
  Garantit que le conteneur `nginx-proxy` est démarré avant le compagnon Let's Encrypt, car ce dernier dépend de NGINX pour valider les certificats.

- **`volumes`**  
  Ces volumes partagés permettent la collaboration avec NGINX :
  - **Certificats SSL/TLS (`./certs`)**.
  - **Configurations spécifiques aux domaines (`./vhost.d`)**.
  - **Fichiers statiques et de logs (`./html`)**.

---

#### 3. **webapp**

```yaml
  webapp:
    image: mon-site-web
    container_name: webapp
    expose:
      - "8080" # Port utilisé par le service en interne
    environment:
      - VIRTUAL_HOST=exemple.com
      - VIRTUAL_PORT=80
      - LETSENCRYPT_HOST=exemple.com
      - LETSENCRYPT_EMAIL=admin@exemple.com
```

- **`image: mon-site-web`**  
  Utilise une image Docker de votre application. Celle-ci peut être une image publique ou personnalisée.

- **`container_name: webapp`**  
  Permet d'identifier facilement ce conteneur spécifique.

- **`expose: "8080"`**  
  Spécifie le port interne de l'application, accessible uniquement par d'autres conteneurs du réseau Docker.

- **`environment`**  
  Définit les variables nécessaires pour que NGINX Proxy et Let's Encrypt fonctionnent avec ce service :
  1. **`VIRTUAL_HOST=exemple.com`**  
     Indique à NGINX Proxy de router les requêtes pour `exemple.com` vers ce conteneur.
  2. **`LETSENCRYPT_HOST=exemple.com`**  
     Spécifie le domaine pour lequel un certificat SSL doit être généré.
  3. **`LETSENCRYPT_EMAIL=admin@exemple.com`**  
     Adresse e-mail pour recevoir des notifications sur l'état des certificats.
  4. `Virtual_Host` permet de préciser le port à utiliser sur le conteneur 

### 2. Démarrage des Conteneurs

Exécutez la commande suivante pour démarrer NGINX, Let's Encrypt et votre application web :

```bash
docker network create nginx-proxy-network
docker compose up -d
```
* **up :**
Lance les services définis dans le fichier docker-compose.yml.

* **-d (detached mode) :**
Exécute les conteneurs en arrière-plan. Vous gardez le terminal disponible tout en maintenant les services actifs.
---

## Configurer NGINX en Proxy Inverse

NGINX est maintenant configuré pour agir comme un proxy inverse. Il redirige les requêtes entrantes vers le conteneur approprié en se basant sur le domaine spécifié dans les variables d'environnement (`VIRTUAL_HOST`).

---

## Activer HTTPS avec Let's Encrypt

### Fonctionnement

Le conteneur **nginx-letsencrypt** s'intègre à NGINX pour obtenir et gérer automatiquement des certificats SSL/TLS de Let's Encrypt.

1. **NGINX** intercepte les requêtes HTTP.
2. Let's Encrypt valide le domaine en utilisant le challenge HTTP-01.
3. Les certificats sont stockés dans le volume partagé `./certs`.

### Vérification

Après avoir démarré les conteneurs, les certificats seront générés automatiquement. Vérifiez que les certificats sont bien en place :

```bash
docker logs nginx-letsencrypt
```

### Avertissement

Attention, parce que les potentiels soucis avec let's Encrypt n'apparaissent pas directement dans le terminal. Donc s'il y a un soucis, gardez en tête que cela peut venir de là. 
Par exemple, un email invalide format à let's Encrypt peut faire échouer la génération d'un certificat.

---

## Ajouter un Nouveau Conteneur avec HTTPS : Explications selon Divers Scénarios

L’ajout de nouveaux services à NGINX Proxy pour bénéficier d’**HTTPS** peut se faire dans plusieurs contextes. Nous allons explorer trois scénarios, en détaillant la **logique sous-jacente** liée aux environnements Docker, aux réseaux et à l’intégration avec **NGINX Proxy**.

---

### Notions Clés Avant de Commencer

- **Réseau Docker** :  
  Docker utilise des réseaux pour permettre aux conteneurs de communiquer entre eux de manière sécurisée et isolée. Dans le cas de **NGINX Proxy**, les services doivent être sur le même réseau pour que NGINX puisse rediriger le trafic.

- **Variables d’Environnement** :  
  Elles permettent de configurer dynamiquement les services sans modifier directement leur code. NGINX Proxy utilise certaines variables (`VIRTUAL_HOST`, `LETSENCRYPT_HOST`, etc.) pour détecter et configurer automatiquement les routes et certificats.

---

### Scénario 1 : Ajouter un Nouveau Service dans le Même Docker Compose que NGINX Proxy

Dans ce scénario, le nouveau service est défini directement dans le **même fichier Docker Compose** que celui de NGINX Proxy. Cela garantit une **configuration centralisée** et une gestion facile.

#### Exemple de Configuration

```yaml
version: '3.8'

services:
  nginx-proxy:
    image: jwilder/nginx-proxy
    container_name: nginx-proxy
    ports:
      - "80:80"
      - "443:443"
    volumes:
      - /var/run/docker.sock:/tmp/docker.sock:ro
      - ./certs:/etc/nginx/certs
      - ./vhost.d:/etc/nginx/vhost.d
      - ./html:/usr/share/nginx/html
    environment:
      - DEFAULT_HOST=default.exemple.com
    networks:
      - nginx-proxy-network

  letsencrypt-companion:
    image: jrcs/letsencrypt-nginx-proxy-companion
    container_name: nginx-letsencrypt
    depends_on:
      - nginx-proxy
    volumes:
      - /var/run/docker.sock:/var/run/docker.sock:ro
      - ./certs:/etc/nginx/certs
      - ./vhost.d:/etc/nginx/vhost.d
      - ./html:/usr/share/nginx/html
    networks:
      - nginx-proxy-network

  nouveau-service:
    image: nouvelle-app
    container_name: nouveau-service
    expose:
      - "8081"
    environment:
      - VIRTUAL_HOST=nouveau.exemple.com
      - LETSENCRYPT_HOST=nouveau.exemple.com
      - LETSENCRYPT_EMAIL=admin@exemple.com
    networks:
      - nginx-proxy-network

networks:
  nginx-proxy-network:
    external: true
```

#### Explication Logique

1. **Réseau Partagé (`nginx-proxy-network`)** :  
   Tous les services sont connectés au même réseau Docker. Cela permet à **NGINX Proxy** de communiquer avec le nouveau service (`nouveau-service`) et de rediriger les requêtes.

2. **Variables d’Environnement** :  
   - **`VIRTUAL_HOST`** : Indique que les requêtes vers `nouveau.exemple.com` doivent être routées vers ce conteneur.
   - **`LETSENCRYPT_HOST`** : Spécifie le domaine pour lequel Let's Encrypt doit générer un certificat.
   - **`LETSENCRYPT_EMAIL`** : Fournit une adresse email pour les notifications (expirations de certificats, erreurs).

3. **Centralisation** :  
   Le fait que tout soit dans un même Docker Compose permet de gérer facilement tous les services.

**Commandes à exécuter** :
```bash
docker-compose up -d
```

---

### Scénario 2 : Sécuriser un Conteneur Lancé Indépendamment, sans Docker Compose

Dans ce cas, le conteneur est lancé **manuellement** avec `docker run`, sans être lié à un fichier Docker Compose. L’objectif est de **l'intégrer au réseau NGINX Proxy** pour profiter de ses fonctionnalités.

#### Étapes à Suivre

1. **Lancer le Conteneur avec les Variables Nécessaires** :
   ```bash
   docker run -d --name conteneur-independant \
     --network nginx-proxy-network \
     -e VIRTUAL_HOST=independant.exemple.com \
     -e LETSENCRYPT_HOST=independant.exemple.com \
     -e LETSENCRYPT_EMAIL=admin@exemple.com \
     your-image
   ```

#### Explication Logique

1. **Connexion au Réseau (`--network nginx-proxy-network`)** :  
   En spécifiant le réseau, tu assures que **NGINX Proxy** peut accéder à ce conteneur et router les requêtes.

2. **Variables d’Environnement** :  
   Même logique que précédemment : elles permettent à NGINX Proxy et Let's Encrypt de configurer automatiquement le routage et le certificat SSL.

3. **Indépendance** :  
   Le conteneur tourne indépendamment de tout fichier Docker Compose mais bénéficie toujours d'HTTPS via NGINX Proxy.

---

### Scénario 3 : Sécuriser un Service d’un Autre Docker Compose

Dans ce scénario, un autre projet sur le même VPS a son propre fichier **Docker Compose**. Tu veux sécuriser certains de ses services sans les intégrer dans le Docker Compose de NGINX Proxy.

#### Étapes à Suivre

1. **Modifier le Docker Compose Externe** :
   Exemple : fichier `docker-compose.api.yml` de l'API backend.

   ```yaml
   version: '3.8'

   services:
     api-backend:
       image: api-backend-image
       container_name: api-backend
       expose:
         - "8080"
       environment:
         - VIRTUAL_HOST=api.exemple.com
         - LETSENCRYPT_HOST=api.exemple.com
         - LETSENCRYPT_EMAIL=admin@exemple.com
       networks:
         - nginx-proxy-network

   networks:
     nginx-proxy-network:
       external: true
   ```

2. **Redéployer l’API** :
   ```bash
   docker-compose -f docker-compose.api.yml up -d
   ```

#### Explication Logique

1. **Réseau Partagé** :  
   Même réseau que celui de **NGINX Proxy**, permettant une communication fluide entre les conteneurs même s'ils sont définis dans différents fichiers Docker Compose.

2. **Variables d’Environnement** :  
   Comme toujours, elles permettent à NGINX Proxy de détecter dynamiquement le conteneur et de configurer les certificats.

---

## Dépannage et Vérification

### Vérification des Logs

- **Logs NGINX** :
  ```bash
  docker logs nginx-proxy
  ```

- **Logs Let's Encrypt** :
  ```bash
  docker logs nginx-letsencrypt
  ```

### Tester l'accès HTTPS

- Accédez à votre site via `https://exemple.com`.
- Utilisez [SSL Labs](https://www.ssllabs.com/ssltest/) pour vérifier la configuration de votre certificat.

---

[Menu VPS](../menu.md)
