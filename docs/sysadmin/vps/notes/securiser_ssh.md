### 📌 **Changement de port SSH, Forçage de la connexion par clé SSH, Sécurisation générale**

---

[...retorn en rèire](../menu.md)

---

## 🔹 **1. Changer le port SSH sur un VPS Debian**
Changer le port par défaut (22) du serveur SSH permet de réduire les attaques automatisées.

### **Étape 1 : Modifier la configuration de SSH**
Éditez le fichier de configuration SSH :
```bash
sudo nano /etc/ssh/sshd_config
```

Recherchez la ligne :
```bash
#Port 22
```
Et modifiez-la en remplaçant `22` par un autre port (exemple : `2222`) :
```bash
Port 2222
```

### **Étape 2 : Autoriser le nouveau port dans le pare-feu**
Si vous utilisez **UFW**, ajoutez la nouvelle règle :
```bash
sudo ufw allow 2222/tcp
```
Si vous utilisez **iptables** :
```bash
sudo iptables -A INPUT -p tcp --dport 2222 -j ACCEPT
```

### **Étape 3 : Redémarrer le service SSH**
```bash
sudo systemctl restart ssh
```

### **Étape 4 : Tester la connexion avant de fermer la session actuelle**
Dans une nouvelle fenêtre de terminal :
```bash
ssh -p 2222 user@vps_ip
```

Si la connexion fonctionne, vous pouvez désactiver l’ancien port :
```bash
sudo ufw deny 22/tcp
```

---

## 🔹 **2. Forcer la connexion SSH par clé (désactiver l’authentification par mot de passe)**
Par défaut, SSH permet l’authentification par mot de passe. Pour la désactiver :

### **Étape 1 : Générer une clé SSH (si ce n’est pas déjà fait)**
Sur votre **machine locale** :
```bash
ssh-keygen -t rsa -b 4096 -C "mon-vps"
```
Cela crée deux fichiers :
- **`~/.ssh/id_rsa`** (clé privée)
- **`~/.ssh/id_rsa.pub`** (clé publique)

### **Étape 2 : Ajouter la clé publique au VPS**
Sur votre **machine locale** :
```bash
ssh-copy-id -p 2222 user@vps_ip
```
Ou manuellement :
```bash
cat ~/.ssh/id_rsa.pub | ssh -p 2222 user@vps_ip "mkdir -p ~/.ssh && cat >> ~/.ssh/authorized_keys"
```

### **Étape 3 : Désactiver l’authentification par mot de passe**
Éditez le fichier de configuration SSH :
```bash
sudo nano /etc/ssh/sshd_config
```

Recherchez et modifiez ces lignes :
```bash
PasswordAuthentication no
PubkeyAuthentication yes
```

### **Étape 4 : Redémarrer SSH et tester**
```bash
sudo systemctl restart ssh
```
Puis testez :
```bash
ssh -p 2222 user@vps_ip
```

Si tout fonctionne bien, essayez une connexion sans clé (`ssh -o PreferredAuthentications=password -p 2222 user@vps_ip`). Si elle échoue, c’est que l’authentification par mot de passe est bien désactivée.

---

## 🔹 **3. Sécuriser un VPS Debian : Bonnes pratiques**
### 🔥 **a) Bloquer les tentatives de connexion SSH bruteforce**
Avec **Fail2Ban** :
```bash
sudo apt install fail2ban -y
```
Configurer **Fail2Ban** :
```bash
sudo nano /etc/fail2ban/jail.local
```
Ajoutez :
```ini
[sshd]
enabled = true
port = 2222
maxretry = 3
findtime = 10m
bantime = 1h
```
Redémarrer le service :
```bash
sudo systemctl restart fail2ban
```

### 🔥 **b) Désactiver l’accès root via SSH**
Dans `/etc/ssh/sshd_config`, changez :
```bash
PermitRootLogin no
```
Puis redémarrez SSH :
```bash
sudo systemctl restart ssh
```

### 🔥 **c) Mettre à jour régulièrement le système**
```bash
sudo apt update && sudo apt upgrade -y
```

### 🔥 **d) Configurer un pare-feu (UFW)**
Activer UFW et n’autoriser que les connexions essentielles :
```bash
sudo ufw default deny incoming
sudo ufw default allow outgoing
sudo ufw allow 2222/tcp  # Autoriser SSH
sudo ufw allow 80/tcp    # HTTP
sudo ufw allow 443/tcp   # HTTPS
sudo ufw enable
```

### 🔥 **e) Vérifier les connexions suspectes**
```bash
sudo netstat -tulnp
sudo lsof -i -P -n | grep LISTEN
```

---

[...retorn en rèire](../menu.md)