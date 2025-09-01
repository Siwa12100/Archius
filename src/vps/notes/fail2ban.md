# 📖 Installation, principe et configuration de Fail2ban

---

[...retorn en rèire](../menu.md)

---

## 1. Introduction

Même avec des mots de passe solides et des ports restreints, un serveur est constamment visé par des tentatives d’accès non autorisées (bruteforce SSH, scans automatisés, bots, etc.).

**Fail2ban** est un outil léger et efficace qui permet de **détecter** ces comportements suspects dans les journaux et de **bloquer automatiquement** les adresses IP malveillantes pendant un certain temps via le firewall.

👉 C’est donc une **couche de protection supplémentaire**, indispensable pour tout VPS ou serveur accessible sur Internet.

---

## 2. Principe de fonctionnement

1. **Analyse des logs**
   Fail2ban surveille les journaux de services (par ex. `/var/log/auth.log` pour SSH ou `/var/log/nginx/access.log` pour Nginx).
   Avec un jeu de **règles (regex)**, il repère les tentatives de connexion échouées ou suspectes.

2. **Application d’une politique**
   Lorsqu’une IP échoue trop souvent (ex. 5 fois en 10 minutes), Fail2ban déclenche une action.

3. **Blocage temporaire (ban)**
   L’IP est ajoutée aux règles du firewall (`iptables`, `nftables`, `firewalld`, etc.), ce qui empêche tout nouvel accès depuis cette IP.
   Après une durée définie (`bantime`), l’IP est automatiquement libérée.

4. **Durcissement progressif**
   Avec l’option de **bantime incrémental**, les récidivistes sont bannis de plus en plus longtemps.

---

## 3. Installation

### Étape 1 : Mettre à jour le système

```bash
sudo apt update && sudo apt upgrade -y
```

### Étape 2 : Installer Fail2ban

```bash
sudo apt install fail2ban -y
```

### Étape 3 : Vérifier l’installation

```bash
fail2ban-client -V
```

Exemple :

```
Fail2Ban v0.11.2
```

---

## 4. Configuration de base

La configuration par défaut se trouve dans :

* `/etc/fail2ban/jail.conf` (⚠️ ne pas modifier directement)
* `/etc/fail2ban/jail.d/` (fichiers personnalisés à créer)

### Étape 1 : Copier le fichier de config

```bash
sudo cp /etc/fail2ban/jail.conf /etc/fail2ban/jail.local
```

👉 On garde l’original intact.

### Étape 2 : Exemple minimal pour SSH

Créer un fichier `/etc/fail2ban/jail.d/sshd.local` :

```ini
[sshd]
enabled = true
backend = systemd
logpath = /var/log/syslog
maxretry = 5
findtime = 10m
bantime  = 30m
```

* `enabled` : active le jail
* `backend = systemd` : lit les logs via journald (utile si `/var/log/auth.log` n’existe pas)
* `logpath` : chemin d’un fichier log valide (ex. `/var/log/syslog`)
* `maxretry` : nombre d’essais avant ban
* `findtime` : fenêtre d’observation
* `bantime` : durée du bannissement

---

## 5. Lancer et tester Fail2ban

### Démarrage et activation

```bash
sudo systemctl enable fail2ban
sudo systemctl start fail2ban
```

### Vérifier l’état

```bash
sudo systemctl status fail2ban
```

### Lister les jails actifs

```bash
sudo fail2ban-client status
```

### Voir le détail d’un jail

```bash
sudo fail2ban-client status sshd
```

Exemple :

```
Status for the jail: sshd
|- Filter
|  |- Currently failed: 0
|  |- Total failed:     5
|  `- Journal matches:  _SYSTEMD_UNIT=sshd.service + _COMM=sshd
`- Actions
   |- Currently banned: 1
   |- Total banned:     1
   `- Banned IP list: 203.0.113.45
```

---

## 6. Résolution des problèmes courants

### ❌ Erreur : *"Have not found any log file for sshd jail"*

* Sur certaines distributions (Debian minimal, VPS cloud), pas de `/var/log/auth.log`.
* Solution → forcer l’usage de journald :

Créer `/etc/fail2ban/jail.d/sshd.local` :

```ini
[sshd]
enabled = true
backend = systemd
logpath = /var/log/syslog
```

Puis redémarrer :

```bash
sudo systemctl restart fail2ban
```

---

### ❌ Erreur : *"fail2ban-client ERROR Failed to access socket path /var/run/fail2ban/fail2ban.sock"*

* Le service n’a pas démarré correctement.
* Vérifier les logs :

```bash
sudo journalctl -u fail2ban -n 50 --no-pager
```

* Corriger la configuration du jail fautif, puis relancer :

```bash
sudo systemctl restart fail2ban
```

---

### ❌ Aucun IP bannie alors que des attaques sont visibles

* Vérifie que le chemin des logs est correct (`logpath`).
* Vérifie que les regex du filtre correspondent bien aux logs.
* Teste en provoquant volontairement 6 erreurs de mot de passe SSH.

---

## 7. Durcissement avancé

### Ignorer certaines IP

```ini
[DEFAULT]
ignoreip = 127.0.0.1/8 ::1 203.0.113.10
```

### Bans progressifs

```ini
[DEFAULT]
bantime.increment = true
bantime.factor    = 1.5
bantime.maxtime   = 12h
```

### Jail “recidive”

Permet de bannir très longtemps une IP qui revient trop souvent :

```ini
[recidive]
enabled  = true
logpath  = /var/log/fail2ban.log
bantime  = 1w
findtime = 1d
maxretry = 10
```

---

## 8. Extension à d’autres services

### Exemple Nginx (bloquer les bots)

Créer `/etc/fail2ban/filter.d/nginx-badbots.conf` :

```ini
[Definition]
failregex = ^<HOST> - .* "(GET|POST) .*(/wp-login\.php|/xmlrpc\.php|/phpmyadmin).*" .*$
```

Puis le jail `/etc/fail2ban/jail.d/nginx.local` :

```ini
[nginx-badbots]
enabled  = true
port     = http,https
filter   = nginx-badbots
logpath  = /var/log/nginx/access.log
maxretry = 10
findtime = 10m
bantime  = 1h
```

---

## 9. Firewall et complémentarité

Fail2ban **n’est pas un firewall**, il s’appuie sur lui.
Il est recommandé de coupler Fail2ban avec un firewall de base comme `ufw` ou `iptables` pour verrouiller encore plus.

Exemple avec `ufw` :

```bash
sudo apt install ufw -y
sudo ufw default deny incoming
sudo ufw default allow outgoing
sudo ufw allow 22/tcp
sudo ufw allow 80/tcp
sudo ufw allow 443/tcp
sudo ufw enable
```

---

[...retorn en rèire](../menu.md)