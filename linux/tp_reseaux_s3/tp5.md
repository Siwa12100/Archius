# TP 5 - Reseau

### Préparation du réseau
---
Dans le cadre du réseau, on commence par ouvrir le réseau fixme de vdn et on exécute configAll.
Vraiment importer de ne pas avoir de passphrase sur la clé ssh utilisé par VDN. 


### Q.1) Pas d'accès internet depuis le réseau local
---
Sur la machine Passerelle, il faut rentrer : 
`iptables -t nat -A POSTROUTING -o eth0 -j MASQUERADE`


### Q.2) Appolo est aveugle
---
Il faut penser à donner l'adresse de la passerelle 192.168.3.1 en gateway dans le `/etc/network/interfaces` de appolo. 


### Q.3) Administration du serveur web
---
Alors déjà, il faut bien comprendre que cela n'a aucun lien avec l'accès à un serveur web. Il est question dans cette question de réussir à se connecter à la **machine** du serveur web, pour pouvoir ensuite l'administrer. 

