# Activer HTTPS sur un VPS avec Let's Encrypt (NGINX & Apache)

[Menu vps](../menu.md)
---

## Sommaire

- [Activer HTTPS sur un VPS avec Let's Encrypt (NGINX \& Apache)](#activer-https-sur-un-vps-avec-lets-encrypt-nginx--apache)
  - [Menu vps](#menu-vps)
  - [Sommaire](#sommaire)
  - [Introduction](#introduction)
  - [Prérequis](#prérequis)
  - [Let's Encrypt et Certbot : Concepts Clés](#lets-encrypt-et-certbot--concepts-clés)
    - [Qu'est-ce que Let's Encrypt ?](#quest-ce-que-lets-encrypt-)
    - [Certificats SSL/TLS : Définition et Importance](#certificats-ssltls--définition-et-importance)
  - [Installation de Certbot](#installation-de-certbot)
    - [Installation sur Ubuntu/Debian](#installation-sur-ubuntudebian)
  - [Configuration HTTPS](#configuration-https)
    - [Avec NGINX](#avec-nginx)
    - [Avec Apache2](#avec-apache2)
  - [Méthodes de Validation](#méthodes-de-validation)
    - [Validation HTTP-01](#validation-http-01)
    - [Validation DNS-01 pour Wildcard](#validation-dns-01-pour-wildcard)
  - [Automatisation avec Cron](#automatisation-avec-cron)
  - [Vérification et Dépannage](#vérification-et-dépannage)
    - [Vérifications de base](#vérifications-de-base)
    - [Logs utiles](#logs-utiles)
  - [Références](#références)

---

## Introduction

**HTTPS** est la norme actuelle pour sécuriser les communications web. Il protège la confidentialité des données échangées entre le serveur et le client, assure l'identité du serveur et prévient les attaques de type man-in-the-middle. **Let's Encrypt** offre des certificats SSL/TLS gratuits et automatisés, accessibles via l'outil **Certbot**.

---

## Prérequis

Pour suivre ce guide, vous devez disposer de :
- Un **VPS** avec accès root ou sudo.
- Un nom de domaine pointant vers le VPS.
- Un serveur web : **NGINX** ou **Apache2**.
- Une distribution Linux, comme Ubuntu ou Debian.

---

## Let's Encrypt et Certbot : Concepts Clés

### Qu'est-ce que Let's Encrypt ?

**Let's Encrypt** est une autorité de certification (CA) qui fournit des certificats SSL/TLS gratuits. Ses objectifs principaux sont :
- **Automatisation** : Via le protocole **ACME** (Automated Certificate Management Environment).
- **Facilité d'utilisation** : Permettre à tous d’activer HTTPS simplement.
- **Sécurité** : Améliorer la sécurité globale des sites web.

### Certificats SSL/TLS : Définition et Importance

Un **certificat SSL/TLS** :
- Chiffre les communications entre le client et le serveur.
- Assure l'identité du serveur.
- Protège contre les attaques par interception.

Types de certificats :
- **Certificats standards** : Liés à un ou plusieurs domaines spécifiques.
- **Certificats wildcard** : Sécurisent un domaine et tous ses sous-domaines (`*.exemple.com`).

---

## Installation de Certbot

### Installation sur Ubuntu/Debian

Pour **NGINX** :
```bash
sudo apt update
sudo apt install certbot python3-certbot-nginx -y
```

Pour **Apache2** :
```bash
sudo apt install certbot python3-certbot-apache -y
```

---

## Configuration HTTPS

### Avec NGINX

1. Exécutez la commande suivante pour obtenir et configurer automatiquement un certificat :
   ```bash
   sudo certbot --nginx -d exemple.com -d www.exemple.com
   ```

2. Redémarrez NGINX pour appliquer la configuration :
   ```bash
   sudo systemctl reload nginx
   ```

### Avec Apache2

1. Exécutez la commande suivante pour obtenir et configurer automatiquement un certificat :
   ```bash
   sudo certbot --apache -d exemple.com -d www.exemple.com
   ```

2. Redémarrez Apache pour appliquer les modifications :
   ```bash
   sudo systemctl reload apache2
   ```

---

## Méthodes de Validation

### Validation HTTP-01

La validation **HTTP-01** vérifie le contrôle sur un domaine en plaçant un fichier temporaire sur le serveur web.

- Certbot crée un fichier temporaire dans `.well-known/acme-challenge/`.
- Let's Encrypt accède au fichier pour valider le domaine.

### Validation DNS-01 pour Wildcard

La validation **DNS-01** est utilisée pour les certificats wildcard. Elle requiert :
- L’ajout d’un enregistrement **TXT** spécifique dans la configuration DNS du domaine.

Procédure :
1. Lancez Certbot :
   ```bash
   sudo certbot certonly --manual -d *.exemple.com --preferred-challenges dns-01
   ```
2. Ajoutez un enregistrement **TXT** :
   ```plaintext
   _acme-challenge.exemple.com
   ```

---

## Automatisation avec Cron

Let's Encrypt délivre des certificats valides pour **90 jours**. Il est recommandé de les renouveler tous les **60 jours**.

1. Créez un script pour le renouvellement automatique :
   ```bash
   sudo nano /opt/ssl/renew_le_certificate.sh
   ```

   Contenu :
   ```bash
   #!/bin/bash
   certbot renew --quiet
   systemctl reload nginx  # Ou apache2 selon le serveur utilisé
   ```

2. Ajoutez une tâche Cron pour exécuter ce script quotidiennement :
   ```bash
   sudo crontab -e
   ```

   Ligne à ajouter (exécution à 2h30 chaque jour) :
   ```plaintext
   30 2 * * * /opt/ssl/renew_le_certificate.sh
   ```

---

## Vérification et Dépannage

### Vérifications de base

1. Testez l'accès HTTPS à votre domaine dans un navigateur.
2. Vérifiez la validité du certificat avec [SSL Labs](https://www.ssllabs.com/ssltest/).

### Logs utiles

En cas d’erreur, consultez les logs du serveur web :
- Pour **NGINX** :
  ```bash
  sudo tail -f /var/log/nginx/error.log
  ```
- Pour **Apache2** :
  ```bash
  sudo tail -f /var/log/apache2/error.log
  ```

---

## Références

- [Let's Encrypt](https://letsencrypt.org/)
- [Certbot Documentation](https://certbot.eff.org/)
- [NGINX SSL Configuration](https://nginx.org/en/docs/http/configuring_https_servers.html)
- [Apache SSL Configuration](https://httpd.apache.org/docs/2.4/ssl/)

---

[Menu vps](../menu.md)
