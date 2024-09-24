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

Le protocole HTTP utilise le port 80 par défaut, et Apache est probablement configuré pour servir du contenu sur ce port également. Ainsi, même sans HTTPS, le site est accessible via HTTP non sécurisé. Pour forcer l'utilisation de HTTPS, une redirection peut être mise en place dans la configuration d'Apache ou bien on pourrait tout simplement couper l'utilisation de http.

## Exercice 3

### 2.)

Désactiver proxy : 
```bash
unset http_proxy
unset https_proxy
```

### 4.)

Le code : 

```bash
from utils import *
from Crypto.Util.Padding import pad

BLOCK_128_BITS_IN_BYTES = 16

def efail(iv: bytes, ciphertext: bytes, known_plaintext: bytes, begin_replace: bytes, end_replace: bytes):

    assert len(ciphertext) % 16 == 0
    ct_blocks = cut_bytes_in_128bits_blocks(ciphertext)
    assert len(ct_blocks) == len(ciphertext) // 16

    known_plaintext_blocks = cut_bytes_in_128bits_blocks(known_plaintext)
    new_blocks = []

    for replace_text_block in cut_bytes_in_128bits_blocks(pad(begin_replace, BLOCK_128_BITS_IN_BYTES)):
        new_blocks.append(byte_xor(zero_block, replace_text_block))
        new_blocks.append(ct_blocks[0])

    new_ciphertext = new_blocks + ct_blocks[1:]

    for replace_text_block in cut_bytes_in_128bits_blocks(pad(end_replace, BLOCK_128_BITS_IN_BYTES)):
        new_ciphertext.append(byte_xor(zero_block, replace_text_block))
        new_ciphertext.append(ct_blocks[0])

    return new_ciphertext[0], b"".join(new_ciphertext[1:])
```

Le résultat dans la boite mail : 

```html
    <h1>Received emails</h1>
    <p>
    <img ignore="â¼0&nbsp;r·Vèd2" src="/ltipart/encrypted
Bonjour, comment vas-tu ? Juste pour te dire que Pascal ne sera pas au bureau demain.									*KBä¯[xfNô"><img ignore="â¼0&nbsp;r·Vèd2" src="/ltipart/encrypted
Bonjour, comment vas-tu ? Juste pour te dire que Pascal ne sera pas au bureau demain.									*KBä¯[xfNô">Tu as avancé sur le cours de WebSec ?
    </p>
```

### 3.)

Mail présent : ` Tu as avancé sur le cours de WebSec ?`.

## Exercice 4

### 1.)

#### a. **www.google.com**

- **TLS 1.2 :**  
  - **Protocole** : TLSv1.2  
  - **Cipher** : ECDHE-ECDSA-CHACHA20-POLY1305  
  - **Conclusion** : Le site supporte **TLS 1.2**.

- **TLS 1.3 :**  
  - **Protocole** : TLSv1.3  
  - **Cipher** : TLS_AES_256_GCM_SHA384  
  - **Conclusion** : Le site supporte **TLS 1.3**.

#### a2. **ayesh.me**

- **TLS 1.3 :**  
  - **Protocole** : TLSv1.3  
  - **Cipher** : TLS_AES_128_GCM_SHA256  
  - **Conclusion** : Le site supporte **TLS 1.3**.

### Résumé :
- **www.google.com** : Supporte à la fois **TLS 1.2** et **TLS 1.3**.
- **ayesh.me** : Supporte **TLS 1.3**.

### 5.)

```bash
SSLProtocol all -SSLv3 -TLSv1 -TLSv1.1 +TLSv1.2
``` 

### 6.)

```bash
SSLProtocol -all +TLSv1.3 -TLSv1.2
``` 

### 7.)

#### **1. Ciphersuites autorisées**

Dans le fichier `/etc/apache2/mods-available/ssl.conf` :

- Modification de **SSLCipherSuite** pour autoriser uniquement :
  ```bash
  SSLCipherSuite TLS_CHACHA20_POLY1305_SHA256:TLS_AES_128_GCM_SHA256
  ```
- Ensuite, ajout de la ciphersuite **TLS_AES_256_GCM_SHA384** :
  ```bash
  SSLCipherSuite TLS_CHACHA20_POLY1305_SHA256:TLS_AES_128_GCM_SHA256:TLS_AES_256_GCM_SHA384
  ```

Vérification avec `testssl.sh` que les ciphersuites spécifiées sont proposées.

#### **2. Courbes elliptiques (Curves)**

Dans le fichier `/etc/apache2/mods-available/ssl.conf` :

- Modification de **SSLOpenSSLConfCmd** pour autoriser les courbes suivantes :
  ```bash
  SSLOpenSSLConfCmd Curves X25519:sect571r1:secp521r1:secp384r1
  ```
- Ensuite, modification pour ajouter **prime256v1** :
  ```bash
  SSLOpenSSLConfCmd Curves X25519:secp521r1:secp384r1:prime256v1
  ```

Vérification avec `testssl.sh` que les courbes elliptiques spécifiées sont utilisées.




