# tp 1 - Reseau : Routage statique et dynamique

### Le concept du routage 
Le routage est ce qui permet l'interconnexion entre les différents réseaux.

Il fonctionne avant tout à partir du fait qu'une machine possède une carte réseau dans un réseau, et une autre carte réseau dans un autre réseau, de manière à jouer un rôle de passerelle entre les 2.

Il existe ainsi le routage statique en enregistrant manuellement les routes vers les différents réseaux, et le routage dynamique, qui permet de déléguer cette tâche à des programmes spécialisés. 

Dans ce tp, nous avons vu les 2 manière de faire. 

## Routage statique
Ce type de routage consiste à définir pour chaque machine dans le réseau : 

- Sa route **par défaut** : le routeur qui lui permet d'accéder à la plus grosse partie du réseau *(dont souvent internet...).*

- Pour chaque **réseaux/machines auxquels la route par défaut ne permet d'accéder** : il faut définir quel est **le routeur qui lui le permet**.

Sous linux, les routes statiques peuvent être définies dans le fichier `/etc/network/infercaces`.

__Important :__ Si l'on modifie ce fichier, il faudra faire en sorte que l'ensemble des cartes réseaux le relisent pour que les modifications soient prises en compte. Voilà différentes manières de le faire : 
* redémarrer la machine *(mais pas le plus opti et conventionnel)*
* utiliser la commande `systemctl restart networking`
* Et si la commande précédente n'a pas marché, il faut redémarrer les cartes réseaux à la main en faisant `ifdown eth...` puis `ifup eth...` pour la rallumer. 

### Configuration statique de r2
---
> Dans le cadre de vdn, pour commencer, il faut bien sélectionner le réseau > **routing-static**.
>Ensuite, il faut exécuter le script **configAll**. 

Maintenant, voici la table de routage de r2, c'est à dire la table dans laquelle on indique quel routeur prendre pour accéder à l'ensemble des réseaux : 

Desti. ; routeur à prendre ; inter. réseau de r2 

\* ; 192.168.4.2 ; eth1 // le * pour dire "tous les réseaux = gateway..." 

192.168.1.0 ; 192.168.2.1 ; eth0 

192.168.3.0 ; 192.168.2.1 ; eth0 

192.168.8.0 ; 192.168.5.2 ; eth2 

On va ensuite modifier le `etc/network/interfaces` pour y implémenter ce qui est noté dans la table au dessus.

Pour chaque ligne dans la table, on va aller sous l'eth.. qui correspond et écrire la ligne `up route add -net [le réseau destination finissant bien pas .0] netmask 255.255.255.0 gw [l'ip du routeur vers lequel se diriger]`. 
Seule exception, pour la route Gateway, il faut l'indiquer sous l'interface réseau correspondante de cette manière `gateway [ip du routeur représentant le gw]`. 

Extrait du fichier pour l'interface eth2 de r2 :
```
auto eth2
iface eth2 inet static
address 192.168.5.1 // adresse eth2 de r2 
netmask 255.255.255.0
up route add -net 192.168.8.0 netmask 255.255.255.0 gw 192.168.5.2
 ```

 __Important :__ En plus d'actualiser les interfaces pour qu'elles prennent en compte les modifications du fichier précédent, il faut penser à activer l'option routage de la machine. Pour cela, on peut : 
 * Taper la commande `sysctl -w net.ipv4.ip_forward=1` mais cela ne dure que jusqu'au prochain redémarrage de la machine 
 * Décommenter la ligne `net.ipv4.ip_forward=1` dans le fichier `/etc/sysctl.conf` pour le rendre opérationnel à chaque redémarrage de la machine. 


 ### Reconfiguration du réseau avec R2 coupé
 ---
 Dans le cas présent, on considère que r2 est coupé. Le réseau ne fonctionne donc plus puisque les routeurs qui avaient l'habitude de rediriger vers r2 redirigent maintenant vers une machine coupée...

 Il faut donc aller dans chaque machine qui mentionne R2 dans son `/etc/network/interfaces` et remplacer R2 par un autre routeur à emprunter.

 __Attention :__ Je m'étais trompé en pensant qu'il fallait supprimer les lignes mentionnant les réseaux directement relier à r2 (réseau 2, 5 et 4) mais c'est une erreur, sinon le script ping without r2 ne marche pas. Pour ces réseaux ci, il faut aussi indiquer une nouvelle route pour y accéder (quitte à changer l'interface réseau utilisée initialement). 


 ## Routage dynamique

> Déjà en ce qui concerne VDN, il faut bien penser à prendre le réseau routing ospf et exécuter le script configAll.

Pour avoir les informations sur les routes connues par une machine, il faut faire la commande `ip route show` (ou alors `route`, mais c'est moins précis). 

En faisant `ip route show`, je vais voir : 
- Les routes vers les réseaux où la machine a directement un pied (via une interface réseau). Il y aura affiché dans le terminal pour chaque ligne, le réseau en question, l'interface réseau ayant un pied devant, "kernel" qui indique que c'est une route statique, puis à la fin l'ip que la machine actuelle possède sur ce réseau ci. 
- Les routes définies dynamiquement. Pour chaque ligne, on aura donc le réseau, puis `via ...ip...` où ip donne l'ip du routeur à emprunter pour accéder au réseau, l'interface réseau utilisée ensuite et pour finir "zebra" qui indique que c'est une route dynamique. 

Si je veux savoir la route empruntée pour aller d'une machine A à une machine B, j'ai juste à aller sur ma machine A et rentrer `traceroute ip` pour qu'il me liste les différents routeurs empruntés. 
Dans le cas de la question du tp, c'est donc `traceroute s2` qu'il faut rentrer depuis S1. 


 






