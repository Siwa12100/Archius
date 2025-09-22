# Guide d'Administration Réseau

[...retour au sommaire](../../README.md)
[version 2 ici](./notes2.md)

---

## Plan d'adressage

### Configuration des machines

**Admin**
```plaintext
auto lo
iface lo inet loopback

auto eth0
iface eth0 inet static
   address 30.1.2.3
   netmask 255.0.0.0
```
**Client**
```plaintext
auto lo
iface lo inet loopback

auto eth1
iface eth1 inet static
   address 30.1.3.4
   netmask 255.0.0.0
```
**Web**
```plaintext
auto lo
iface lo inet loopback

auto eth0
iface eth0 inet static
  address 192.168.9.2
  netmask 255.255.255.0
  gateway 192.168.9.1
```
**DB**
```plaintext
auto lo
iface lo inet loopback

auto eth0
iface eth0 inet static
  address 192.168.9.3
  netmask 255.255.255.0
  gateway 192.168.9.1
```

**Firewall**
```plaintext
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
Pour appliquer la configuration, exécutez sur chaque machine :
```bash
systemctl restart networking
```

## Services nécessaires

### Configuration du serveur Web (Apache2)
Sur la machine Web :
```bash
systemctl enable apache2
systemctl start apache2
```
Vérification du statut :
```bash
systemctl status apache2
```

### Configuration du serveur de base de données (MariaDB)
Sur la machine DB :
```bash
systemctl enable mariadb
systemctl start mariadb
```
Modification de la configuration pour accepter les connexions distantes :
```plaintext
bind-address = 0.0.0.0
```
Appliquez les changements :
```bash
systemctl restart mariadb
```

### Création de la base de données et de l'utilisateur
Sur la machine DB, accédez au shell MySQL :
```bash
mysql
```
Exécutez les commandes suivantes :
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

**Effacer toutes les règles existantes**
```bash
iptables -F
iptables -t nat -F
```

**Règles pour le firewall (à intégrer dans un script, par exemple `fermeDehors.sh`)**

1. Accepter SSH et HTTP entrants :
```bash
iptables -A INPUT -p tcp --match multiport --dports 22,80 -j ACCEPT
```
2. Rejeter tout autre trafic entrant sur l'interface externe :
```bash
iptables -A INPUT -i eth0 -j REJECT
```

**Redirection de ports (à intégrer dans `forward.sh`)**

1. Redirection de port pour SSH :
```bash
iptables -t nat -A PREROUTING -p tcp -d 30.1.4.5 --dport 2222 -j DNAT --to 192.168.9.2:22
iptables -t nat -A PREROUTING -p tcp -d 30.1.4.5 --dport 2223 -j DNAT --to 192.168.9.3:22
```

**Configuration NAT pour accéder à Internet (à intégrer dans `local.sh`)**

1. Masquerading pour le réseau interne :
```bash
iptables -t nat -A POSTROUTING -s 192.168.9.0/24 -o eth1 -j MASQUERADE
```

### SSH Key Authentication

Sur le firewall, générez les clés SSH et configurez `sshd_config` :
```bash
ssh-keygen -t rsa
nano /etc/ssh/sshd_config
```
Ajoutez ou modifiez les lignes suivantes :
```plaintext
Port 22
Port 2222


Port 2223
```
Redémarrez le service SSH :
```bash
systemctl restart ssh
```

Sur le client, pour se connecter au firewall :
```bash
ssh -p 2222 root@firewall
```
Pour se connecter à Web via le firewall :
```bash
ssh -p 2222 root@web
```
Pour se connecter à DB via le firewall :
```bash
ssh -p 2223 root@db
```

## Évaluation et compétences attendues

- Assurez-vous que toutes les adresses IP publiques et privées sont correctement configurées.
- Vérifiez que les services Web et de base de données sont opérationnels et accessibles depuis les bons emplacements.
- Assurez-vous que la redirection de port et le masquerading sont bien configurés sur le firewall.
- Testez les connexions SSH pour s'assurer qu'elles sont sécurisées et fonctionnent correctement.

En suivant ce guide, vous devriez être en mesure de configurer votre réseau selon les exigences de votre sujet.

Pensez à ajuster les adresses IP et noms de réseau (ethX, ethY) en fonction de votre configuration spécifique et vérifiez que les services sont installés sur vos machines Debian avant de tenter de les activer. Pour la configuration iptables et le forwarding, assurez-vous de les adapter en fonction de la configuration réseau réelle et de l'architecture de votre pare-feu. En outre, pour la configuration de base de données et les règles de pare-feu, soyez certain de les sécuriser davantage en fonction des meilleures pratiques et de la politique de sécurité de votre organisation.

---

[...retour au sommaire](../../README.md)