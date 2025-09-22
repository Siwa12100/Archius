# 🔒 Documentation : Sécurisation de MongoDB avec TLS/SSL, Certificats et Automatisation avec Cron

[...retorn en arrièr](../menu.md)

---

## Sommaire

- [🔒 Documentation : Sécurisation de MongoDB avec TLS/SSL, Certificats et Automatisation avec Cron](#-documentation--sécurisation-de-mongodb-avec-tlsssl-certificats-et-automatisation-avec-cron)
  - [Sommaire](#sommaire)
  - [0. Explication de SSL/TLS et leur utilité dans MongoDB](#0-explication-de-ssltls-et-leur-utilité-dans-mongodb)
    - [**Pourquoi SSL/TLS est indispensable pour MongoDB ?**](#pourquoi-ssltls-est-indispensable-pour-mongodb-)
      - [🔹 **Qu'est-ce que SSL/TLS ?**](#-quest-ce-que-ssltls-)
      - [🔹 **Pourquoi MongoDB doit utiliser TLS ?**](#-pourquoi-mongodb-doit-utiliser-tls-)
      - [🔹 **Comment fonctionne TLS avec MongoDB ?**](#-comment-fonctionne-tls-avec-mongodb-)
  - [1. Introduction](#1-introduction)
  - [2. Prérequis](#2-prérequis)
  - [3. Génération des certificats TLS avec Let's Encrypt](#3-génération-des-certificats-tls-avec-lets-encrypt)
    - [🔹 Installer Certbot (si ce n'est pas déjà fait)](#-installer-certbot-si-ce-nest-pas-déjà-fait)
    - [🔹 Générer un certificat SSL avec Let's Encrypt](#-générer-un-certificat-ssl-avec-lets-encrypt)
    - [🔹 Fusionner la clé privée et le certificat](#-fusionner-la-clé-privée-et-le-certificat)
    - [🔹 Définir les bonnes permissions](#-définir-les-bonnes-permissions)
  - [4. Configuration de MongoDB avec TLS](#4-configuration-de-mongodb-avec-tls)
  - [5. Déploiement de MongoDB avec Docker](#5-déploiement-de-mongodb-avec-docker)
  - [6. Automatisation avec Cron](#6-automatisation-avec-cron)
    - [🔹 Qu'est-ce que `cron` ?](#-quest-ce-que-cron-)
    - [🔹 Vérifier que `cron` est actif](#-vérifier-que-cron-est-actif)
  - [7. Script de renouvellement automatique du certificat](#7-script-de-renouvellement-automatique-du-certificat)

---

## 0. Explication de SSL/TLS et leur utilité dans MongoDB

### **Pourquoi SSL/TLS est indispensable pour MongoDB ?**

#### 🔹 **Qu'est-ce que SSL/TLS ?**
SSL (**Secure Sockets Layer**) et TLS (**Transport Layer Security**) sont des protocoles de chiffrement permettant de sécuriser les échanges entre un client et un serveur en assurant :
- **La confidentialité** : les données échangées sont chiffrées et ne peuvent être lues par un attaquant.
- **L'intégrité** : les données ne peuvent être modifiées ou corrompues pendant leur transmission.
- **L'authentification** : le serveur prouve son identité au client via un certificat numérique.

**TLS est la version améliorée et sécurisée de SSL, et c'est celui qui est recommandé pour toutes les communications réseau aujourd'hui.**

---

#### 🔹 **Pourquoi MongoDB doit utiliser TLS ?**
Par défaut, MongoDB ne chiffre pas ses communications, ce qui pose plusieurs risques majeurs :
- **Les données circulent en clair** : toute personne malveillante interceptant le trafic peut voir les informations sensibles stockées dans la base.
- **Attaque de type Man-in-the-Middle (MITM)** : sans chiffrement, un attaquant peut intercepter et modifier les requêtes envoyées au serveur MongoDB.
- **Accès non autorisé** : si une connexion est compromise, un attaquant pourrait exécuter des requêtes et exfiltrer des données.

💡 **Activer TLS pour MongoDB permet donc de garantir une connexion sécurisée entre les applications clientes et le serveur de base de données.**

---

#### 🔹 **Comment fonctionne TLS avec MongoDB ?**
MongoDB peut être configuré pour **exiger** l'utilisation de TLS en forçant toutes les connexions à être chiffrées.  
Il repose sur **un certificat SSL/TLS** qui prouve son identité et chiffre les données échangées.

MongoDB nécessite un **fichier `.pem` unique** qui contient :
1. **Le certificat public** (`fullchain.pem`), utilisé pour authentifier le serveur.
2. **La clé privée** (`privkey.pem`), permettant de chiffrer les connexions.

💡 **MongoDB ne supporte pas deux fichiers séparés, d’où la nécessité de fusionner ces fichiers avant utilisation.**

## 1. Introduction
Cette documentation couvre toutes les étapes nécessaires pour configurer un serveur MongoDB avec TLS/SSL afin de sécuriser les communications. Elle inclut la génération et l'utilisation de certificats SSL, la configuration de MongoDB pour accepter uniquement des connexions sécurisées, ainsi que la mise en place d'un script de renouvellement automatique des certificats Let's Encrypt.

---

## 2. Prérequis
Avant de commencer, assurez-vous d'avoir :
- Un serveur Linux avec **Docker** installé.
- Un **nom de domaine** pointant vers votre serveur.
- **Certbot** installé pour générer les certificats Let's Encrypt.
- MongoDB **6.0** en tant que service Docker.
- **Cron** installé et fonctionnel pour l'automatisation des tâches.

---

## 3. Génération des certificats TLS avec Let's Encrypt

### 🔹 Installer Certbot (si ce n'est pas déjà fait)
```bash
sudo apt update && sudo apt install certbot -y
```

### 🔹 Générer un certificat SSL avec Let's Encrypt
```bash
sudo certbot certonly --standalone -d ioa-pais.fr --agree-tos -m votremail@example.com --non-interactive
```

Les certificats seront enregistrés dans :
```
/etc/letsencrypt/live/ioa-pais.fr/fullchain.pem
/etc/letsencrypt/live/ioa-pais.fr/privkey.pem
```

### 🔹 Fusionner la clé privée et le certificat
```bash
sudo cat /etc/letsencrypt/live/ioa-pais.fr/fullchain.pem /etc/letsencrypt/live/ioa-pais.fr/privkey.pem > /etc/letsencrypt/live/ioa-pais.fr/mongodb.pem
```

### 🔹 Définir les bonnes permissions
```bash
sudo chmod 600 /etc/letsencrypt/live/ioa-pais.fr/mongodb.pem
```

---

## 4. Configuration de MongoDB avec TLS

Modifier le fichier de configuration `mongod.conf` :
```yaml
security:
  authorization: enabled

net:
  port: 27017
  bindIp: 0.0.0.0
  tls:
    mode: requireTLS
    certificateKeyFile: /etc/ssl/mongodb.pem
    CAFile: /etc/ssl/fullchain.pem
    disabledProtocols: TLS1_0,TLS1_1
    allowConnectionsWithoutCertificates: true
```

---

## 5. Déploiement de MongoDB avec Docker

Créer le fichier `docker-compose.yml` :
```yaml
services:
  mongodb:
    image: mongo:6.0
    container_name: mongodb
    ports:
      - "43002:27017"
    environment:
      MONGO_INITDB_ROOT_USERNAME: "admin"
      MONGO_INITDB_ROOT_PASSWORD: "SuperMotDePasseSecurisé"
    volumes:
      - ./data_mongo:/data/db
      - ./mongod.conf:/etc/mongod.conf
      - /etc/letsencrypt/live/ioa-pais.fr/fullchain.pem:/etc/ssl/fullchain.pem
      - /etc/letsencrypt/live/ioa-pais.fr/mongodb.pem:/etc/ssl/mongodb.pem
    command: ["mongod", "--config", "/etc/mongod.conf"]
    networks:
      - mongo_network
    restart: unless-stopped

networks:
  mongo_network:
    driver: bridge
```

Lancer MongoDB :
```bash
docker-compose up -d
```

---

## 6. Automatisation avec Cron

### 🔹 Qu'est-ce que `cron` ?
`cron` est un service Linux permettant d'exécuter **des tâches automatiques à des intervalles définis**. Il est utilisé ici pour gérer le renouvellement des certificats et le redémarrage du proxy Docker.

### 🔹 Vérifier que `cron` est actif
```bash
sudo systemctl status cron
```
Si le service n'est pas actif, démarrez-le :
```bash
sudo systemctl start cron
sudo systemctl enable cron
```

---

## 7. Script de renouvellement automatique du certificat

Créer le script `renouvellement-certificats-ssl-tls.sh` :
```bash
#!/bin/bash

echo "🛑 Arrêt du reverse proxy..."
docker stop nginx-proxy

echo "🔄 Renouvellement des certificats..."
certbot renew --standalone --force-renewal

echo "🚀 Redémarrage du reverse proxy..."
docker start nginx-proxy

echo "🔄 Mise à jour du certificat MongoDB..."
bash ~/serveurs/mongodb/renouvellement-pem-mongo.sh

echo "✅ Tout est mis à jour !"
```

Ajouter une tâche CRON :
```bash
crontab -e
```
Et ajouter la ligne suivante :
```bash
0 3 * * 1 /bin/bash /chemin/vers/renouvellement-certificats-ssl-tls.sh >> /var/log/certbot-renew.log 2>&1
```

---

[...retorn en arrièr](../menu.md)





