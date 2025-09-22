# Securité tp 5- Shana & Jean

## Exercice 1

### 1.

La page de Shodan présente des informations sur les différents services et domaines associés au domaine `uca.fr`, qui correspond à l'Université Clermont Auvergne.
Sur cette page, on peut voir les types de serveurs, les enregistrements DNS (A, MX, NS, TXT, etc.), ainsi que d'autres informations techniques telles que des sous-domaines et des adresses IP associées.

### 2.

L'ip de la machine de codefirst est 193.49.118.214. Les ports ouverts sont 80 et 443. 

### 3.

La page Shodan pour l'IP `45.33.32.156` fournit des informations sur un hôte nommé `scanme.nmap.org`, qui est un domaine connu pour être utilisé à des fins de test par les développeurs de Nmap. Cette machine est hébergée par Linode, un fournisseur cloud, et tourne sous un système d'exploitation Linux.

Les services qui tournent sur cette machine incluent :
- **SSH (port 22)** avec le service **OpenSSH 6.6.1p1** sur Ubuntu.
- **HTTP (port 80)** avec le serveur **Apache HTTPD 2.4.7** sur Ubuntu.
- **NTP (port 123, UDP)**, un service de synchronisation de temps.

### Vulnérabilités détectées :

Shodan indique plusieurs vulnérabilités potentielles sur cette machine, notamment liées à **Apache HTTP Server** :

- **CVE-2024-40898** (SSRF dans Apache HTTP sur Windows, mais non applicable ici puisque l’OS est Linux).
  
- **CVE-2022-31813** : un risque lié à l'utilisation des en-têtes `X-Forwarded-*` qui pourrait permettre de contourner l'authentification basée sur IP.
  
- **CVE-2021-44790** : un débordement de tampon possible dans le parseur multipart de **mod_lua**, pouvant entraîner une exploitation par des requêtes malveillantes.

