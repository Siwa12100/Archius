# TP 2 securite

## Exercice 1

### 1.)

 Il commence par afficher des informations générales sur le serveur, comme la version du logiciel OpenSSH utilisé et la compression activée. Ensuite, il énumère les algorithmes d'échange de clés (key exchange) disponibles, notamment des algorithmes comme sntrup761x25519 et curve25519-sha256, avec des avertissements concernant les courbes elliptiques suspectées d'être compromises par la NSA. 

L’audit couvre également les algorithmes de clés hôtes (host keys), de chiffrement (encryption) et de code d'authentification des messages (MAC). Il identifie des vulnérabilités, comme l’utilisation d'algorithmes obsolètes ou dangereux tels que SHA-1 ou chacha20-poly1305, et propose des recommandations pour les supprimer. Finalement, il avertit de la possibilité d’une attaque DHEat DoS et fournit des guides pour renforcer la sécurité du serveur.

### 2.)

La version de SSH utilisée est OpenSSH 9.2p1 sur Debian.

### 3.)

Modifications effectuées :

```bash
# Ciphers and keying
Ciphers aes256-ctr,aes256-gcm@openssh.com,aes192-ctr,aes128-ctr,aes128-gcm@openssh.com

# HostKey algorithms
HostKeyAlgorithms ssh-ed25519,rsa-sha2-512,rsa-sha2-256

# MAC algorithms
MACs hmac-sha2-256-etm@openssh.com,hmac-sha2-512-etm@openssh.com,umac-128-etm@openssh.com

# Key exchange algorithms
KexAlgorithms curve25519-sha256,curve25519-sha256@libssh.org,sntrup761x25519-sha512@openssh.com,diffie-hellman-group16-sha512,diffie-hellman-group18-sha512
```

### Explication des changements :

- **Ciphers** : Les algorithmes comme `chacha20-poly1305@openssh.com` sont vulnérables (CVE-2023-48795). J'ai donc sélectionné des algorithmes AES sécurisés.
- 
- **HostKeyAlgorithms** : Les courbes elliptiques `nistp*` sont suspectées d'être backdoorées par la NSA, donc seules les clés `ed25519` et `rsa-sha2` sont utilisées.
- 
- **MACs** : Les algorithmes SHA-1 sont désactivés en faveur des versions SHA-2 plus sécurisées.
- 
- **KexAlgorithms** : Désactivation des algorithmes utilisant les courbes `nistp*` et conservation des algorithmes sécurisés comme `curve25519` et `sntrup761x25519`.

## Exercice 2



