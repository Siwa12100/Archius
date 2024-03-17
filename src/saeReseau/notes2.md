# Guide d'Administration Réseau

[...retour à la V1](./notes1.md)

---

## Plan d'adressage

- **Admin** : adresse IP statique configurée sur eth0.
- **Client** : adresse IP statique configurée sur eth1.
- **Web** : adresse IP privée statique configurée sur eth0, accessible via HTTP.
- **DB** : adresse IP privée statique configurée sur eth0, accessible uniquement par Web et Admin.
- **Firewall** : adresse IP statique configurée sur eth1 pour le réseau externe, et eth0 pour le réseau interne.

## Configuration des machines

### Admin

```bash
auto lo
iface lo inet loopback

auto eth0
iface eth0 inet static
   address 30.1.2.3
   netmask 255.0.0.0
```

### Client

```bash
auto lo
iface lo inet loopback

auto eth1
iface eth1 inet static
   address 30.1.3.4
   netmask 255.0.0.0
```

### Web

```bash
auto lo
iface lo inet loopback

auto eth0
iface eth0 inet static
  address 192.168.9.2
  netmask 255.255.255.0
  gateway 192.168.9.1
```

### DB

```bash
auto lo
iface lo inet loopback

auto eth0
iface eth0 inet static
  address 192.168.9.3
  netmask 255.255.255.0
  gateway 192.168.9.1
```

### Firewall

```bash
auto lo
iface lo inet loopback

auto eth1
iface eth1 inet static
  address 30.1.4.5
  netmask 255.0.0.0

auto eth0
iface eth0 inet static
  address 192.168.9.1
  netmask 255.255.255.0
```

**Application de la configuration** : Exécutez sur chaque machine :

```bash
systemctl restart networking
```

## Services nécessaires

### Configuration du serveur Web (Apache2)

**Sur la machine Web** :

```bash
systemctl enable apache2
systemctl start apache2
```

**Vérification du statut** :

```bash
systemctl status apache2
```

### Configuration du serveur de base de données (MariaDB)

**Sur la machine DB** :

```bash
systemctl enable mariadb
systemctl start mariadb
```

**Modification de la configuration pour accepter les connexions distantes** :

```ini
bind-address = 0.0.0.0
```

**Appliquez les changements** :

```bash
systemctl restart mariadb
```

### Création de la base de données et de l'utilisateur

**Sur la machine DB**, accédez au shell MySQL :

```sql
mysql
```

**Exécutez les commandes suivantes** :

```sql
CREATE DATABASE nomdb;
CREATE USER 'nomUser'@'%' IDENTIFIED BY 'password';
GRANT ALL on nomdb.* to 'nomUser'@'%';
```

## Configuration du Firewall

### Activation du forwarding IP

```bash
sysctl -w net.ipv4.ip_forward=1
```

### Configuration d'Iptables

#### Effacer toutes les règles existantes

```bash
iptables -F
iptables -t nat -F
```

#### Règles pour le firewall

- **Accepter SSH et HTTP entrants pour Admin et Web** :
  - Ce script doit être ajusté pour limiter l'accès à Web et DB conformément aux exigences du sujet.

```bash
# Accepter SSH et HTTP pour Admin et Web uniquement
iptables -A INPUT -p tcp -s 30.1.2.3 --match multiport --dports 22,80 -j ACCEPT
iptables -A INPUT -p tcp -s 192.168.9.2 --match multiport --dports 22,80 -j ACCEPT

# Rejeter tout autre trafic entrant sur l'interface externe
iptables -A INPUT -i eth0 -j REJECT
```

#### Redirection de ports

- **Redirection de port pour SSH** :
 

 - Ajustement nécessaire pour s'assurer que la redirection fonctionne conformément aux exigences.

```bash
# Redirection de port pour SSH vers Web
iptables -t nat -A PREROUTING -p tcp -d 30.1.4.5 --dport 2222 -j DNAT --to 192.168.9.2:22
# Redirection de port pour SSH vers DB
iptables -t nat -A PREROUTING -p tcp -d 30.1.4.5 --dport 2223 -j DNAT --to 192.168.9.3:22
```

#### Configuration NAT pour accéder à Internet

```bash
# Masquerading pour le réseau interne
iptables -t nat -A POSTROUTING -s 192.168.9.0/24 -o eth1 -j MASQUERADE
```

## SSH Key Authentication

**Sur le firewall**, générez les clés SSH et configurez `sshd_config` :

```bash
ssh-keygen -t rsa
nano /etc/ssh/sshd_config
```

**Ajoutez ou modifiez les lignes suivantes** :

```bash
Port 22
Port 2222
Port 2223
```

**Redémarrez le service SSH** :

```bash
systemctl restart ssh
```

## Évaluation et compétences attendues

- Assurez-vous que toutes les adresses IP publiques et privées sont correctement configurées.
- Vérifiez que les services Web et de base de données sont opérationnels et accessibles depuis les bons emplacements.
- Assurez-vous que la redirection de port et le masquerading sont bien configurés sur le firewall.
- Testez les connexions SSH pour s'assurer qu'elles sont sécurisées et fonctionnent correctement.
- **Nouvellement ajouté** : Configurez le firewall pour restreindre l'accès à la base de données uniquement à la machine Web et à l'Admin, et assurez-vous que le serveur Web est accessible par les machines sur Internet (Client et Admin) mais protégé contre les accès non autorisés.

En suivant ce guide enrichi, vous devriez être en mesure de configurer votre réseau conformément aux exigences de votre sujet, tout en respectant les meilleures pratiques en matière de sécurité et de configuration réseau.