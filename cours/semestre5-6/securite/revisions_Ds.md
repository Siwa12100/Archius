# Revisions DS Securite

## Amphi 1 

### 1. **Introduction à la cybersécurité**

La cybersécurité est un domaine essentiel pour protéger les systèmes d'information. Ce cours introduit la sécurité informatique et ses enjeux croissants dans un monde de plus en plus numérique. On y présente les statistiques qui montrent que les attaques sont en constante augmentation.

### 2. **Statistiques de la cybersécurité (2019 et 2023)**
En 2019, plusieurs chiffres clés montrent l'ampleur des cyberattaques :
- 92% des malwares sont diffusés via des emails.
- 1,76 milliard de données ont été volées en janvier 2019.
- Le coût global du cybercrime s'élevait à 2 millions de milliards de dollars en 2019.
- 91% des cyberattaques sont initiées par un phishing via email.
- Les ransomwares devaient coûter environ 11,5 milliards de dollars cette année-là.

En 2023, le temps moyen pour détecter une violation de données est de six mois. Les petites entreprises sont ciblées dans 43% des cas, et une attaque de ransomware survient toutes les 14 secondes.

### 3. **Les cinq familles de cybercriminalité**
- **Escroquerie** : Le phishing est la forme d’escroquerie la plus courante, où les victimes sont trompées pour révéler des informations sensibles.
- **Sabotage** : Exemple avec Stuxnet en 2010, un malware qui a ciblé des installations nucléaires iraniennes. En 2012, 35 000 ordinateurs de Saudi Aramco ont été effacés.
- **Ransomwares** : Le célèbre **WannaCry** du 12 mai 2017 en est un exemple. Ce type de malware crypte les fichiers d'une victime et exige une rançon pour les déchiffrer.
- **Espionnage** : Snowden a révélé en 2013 des programmes de surveillance massive. On parle de différentes formes d'espionnage : **Little Brother** (individuel), **Medium Brother** (entreprise), et **Big Brother** (gouvernement).
- **Déstabilisation** : Cette catégorie inclut le defacement de sites web, les trojans, les botnets, et les réseaux de zombies.

### 4. **Pourquoi les attaques sont-elles en augmentation ?**
Les cyberattaques sont rapides, à grande échelle, et semi-automatisées. Cependant, l'anonymat qu'elles semblent offrir est souvent une illusion. Internet a été conçu pour fonctionner, mais pas nécessairement pour être sécurisé.

### 5. **Agences pour la sécurité informatique**
Diverses agences ont été créées pour lutter contre les cyberattaques, comme l'ANSSI en France (créée en 2009) qui est l'autorité nationale de sécurité informatique.

### 6. **Backdoors**
Un backdoor est une faille volontairement introduite dans un système. Un exemple notable est le backdoor de la NSA dans le générateur de nombres aléatoires **Dual_EC_DRBG**, révélé par Snowden en 2013. Ce backdoor permettait à la NSA d'accéder à des systèmes sécurisés.

### 7. **La sécurité et vous**
L'utilisateur doit être acteur de sa propre sécurité. Cela inclut des mesures telles que l'authentification, la gestion des mots de passe et l'utilisation de solutions de chiffrement.

### 8. **Chiffrement des emails (PGP)**
**PGP** (**Pretty Good Privacy**) est un logiciel inventé par Phil Zimmermann en 1991. Il permet de chiffrer, déchiffrer, et signer des emails. Le processus pour l'utiliser consiste à :
1. Télécharger et installer GPG.
2. Générer une paire de clés (minimum 4096 bits).
3. Importer des clés.
4. Chiffrer et envoyer des emails.

Cependant, malgré son efficacité, PGP est peu utilisé à cause de sa complexité. Beaucoup de personnes trouvent difficile de comprendre le fonctionnement de la gestion des clés.

### 9. **Le cadre juridique**
Le **RGPD** (Règlement Général sur la Protection des Données) a été mis en place en mai 2018 pour renforcer les droits des utilisateurs concernant leurs données personnelles. Cela inclut des sanctions pour non-conformité (jusqu’à 20 millions d’euros ou 4% du chiffre d'affaires global), ainsi que des droits supplémentaires comme le droit à l'oubli et la portabilité des données.

Le **Cyber Resilience Act** (adopté en 2019) vise à améliorer la sécurité des produits dès leur conception, avec des certifications en trois niveaux :
- **Niveau élémentaire** : Évaluation par les fabricants.
- **Niveau substantiel** : Certificat par un organisme de conformité.
- **Niveau élevé** : Tests de pénétration obligatoires réalisés par un tiers.

### 10. **Les mots de passe**
Les mots de passe sont souvent la première ligne de défense, mais aussi l'une des plus faibles. On observe que les mots de passe les plus courants restent très simples, comme "123456" ou "password". Les recommandations incluent :
- Ne jamais réutiliser un mot de passe.
- Utiliser des mots de passe longs et complexes.
- Changer régulièrement de mot de passe.
- Utiliser des gestionnaires de mots de passe comme **KeePass**.

Le stockage sécurisé des mots de passe implique des techniques comme le **hachage avec salt** ou l'utilisation d'algorithmes lents comme **bcrypt**.

### 11. **Attaques par brute force**
Le temps nécessaire pour deviner un mot de passe dépend de plusieurs facteurs : nombre de caractères, vitesse du processeur, etc. Par exemple, pour un PC de 3 GHz avec 8 cœurs, un mot de passe de 12 caractères peut prendre plus d'un siècle à être craqué par brute force.

### 12. **Cryptographie**
- **Symétrique** : Utilise une seule clé pour le chiffrement et le déchiffrement. Exemples : **DES**, **AES**.
- **Asymétrique** : Utilise une paire de clés (publique et privée). Exemples : **RSA**, **ElGamal**.

Comparaison entre les deux :
- Les clés asymétriques sont plus grandes, mais le chiffrement symétrique est plus rapide.
- Les signatures numériques ne peuvent être réalisées qu'avec un système asymétrique.

### 13. **Fonctions de hachage**
Les fonctions de hachage, comme **SHA-1** ou **SHA-3**, sont utilisées pour garantir l'intégrité des données. Elles ont trois propriétés fondamentales :
- **Résistance à la pré-image** : Il est difficile de trouver un message à partir d'un hachage donné.
- **Résistance à la seconde pré-image** : Il est difficile de trouver un deuxième message ayant le même hachage qu'un autre message.
- **Résistance aux collisions** : Il est difficile de trouver deux messages différents qui produisent le même hachage.

### 14. **Signatures numériques**
Elles permettent de vérifier l'authenticité d'un document. Une signature est générée avec une clé privée et vérifiée avec une clé publique. Par exemple, avec RSA, la signature est calculée par \( md \mod n \).

### 15. **Histoire de la cryptographie**
On retrace l'histoire de la cryptographie depuis l'Antiquité :
- Les Grecs utilisaient la **Scytale** (transposition).
- Les Romains utilisaient le **chiffre de César** (substitution).
- Au 16ème siècle, **Vigenère** a introduit la substitution polyalphabétique.
- Pendant la Seconde Guerre mondiale, la machine **Enigma** a été utilisée par les nazis pour chiffrer leurs communications.

### 16. **One-Time Pad (Chiffrement Vernam)**
C'est un algorithme de chiffrement parfaitement sûr, où chaque bit du message est chiffré avec un bit d'une clé aléatoire de même longueur que le message.

## Amphi 2

### 1. **Fonctionnement du Web et de l'Internet**
   - **Le Web** est l'une des applications qui fonctionnent sur l'Internet. Il utilise des protocoles comme **HTTP** pour permettre aux navigateurs web de communiquer avec des serveurs.
   - **L'Internet**, quant à lui, est le réseau mondial qui relie les ordinateurs entre eux via des protocoles comme **IP** (Internet Protocol). HTTP fonctionne sur ce réseau, mais il existe d'autres services, comme l'email ou le FTP (protocole de transfert de fichiers).

### 2. **Applications Web**
   - Les applications web sont des programmes que nous utilisons via un navigateur. Cela inclut des services comme **Amazon** pour le shopping en ligne, **Ebay** pour les enchères, **Facebook** pour les réseaux sociaux, et **YouTube** pour les vidéos.
   - Les applications web fonctionnent avec plusieurs langages de programmation comme **HTML** (pour structurer les pages), **JavaScript** (pour rendre les pages interactives), et **PHP** ou **Java** (pour gérer la logique côté serveur).

### 3. **IANA (Internet Assigned Numbers Authority)**
   - **IANA** est une organisation responsable de la coordination des adresses IP et des noms de domaine. Par exemple, lorsque tu tapes une adresse comme *google.com*, IANA assure que cela correspond à une adresse IP unique afin que ton navigateur trouve le bon serveur.

### 4. **HTTP (Hypertext Transfer Protocol)**
   - **HTTP** est le protocole utilisé par le Web pour permettre à ton navigateur (le client) de demander des pages web aux serveurs. Ce protocole fonctionne avec d'autres couches sous-jacentes comme **TCP** et **IP**, qui transportent réellement les données.

### 5. **DNS (Domain Name System)**
   - Le **DNS** est un système qui fait correspondre les noms de domaine (comme *google.com*) aux adresses IP. C'est un peu comme un annuaire qui traduit les noms faciles à retenir en adresses compréhensibles pour les ordinateurs.

### 6. **Surface d'attaque du Web**
   - **Surface d'attaque** désigne tous les points d'entrée potentiels pour une cyberattaque. Sur le Web, cela peut inclure les faiblesses des navigateurs, les erreurs dans les langages de programmation, ou les formulaires en ligne mal sécurisés. Le Web est un vaste écosystème, et chaque composant peut devenir une cible.

### 7. **S/MIME (Secure/Multipurpose Internet Mail Extensions)**
   - **S/MIME** est une norme pour sécuriser les emails. Il permet de chiffrer les messages et de les signer numériquement. Cela garantit la **confidentialité** (seul le destinataire peut lire le message), **l'intégrité** (le message n'a pas été modifié), et l'**authenticité** (on peut prouver qui a envoyé le message).

### 8. **Vulnérabilité Efail**
   - La vulnérabilité **Efail** exploitait des faiblesses dans **S/MIME** et **PGP**, deux technologies de chiffrement des emails. En interceptant des emails chiffrés, un attaquant pouvait les modifier de manière subtile pour que, lorsque la victime les ouvrait, le contenu déchiffré soit envoyé à l'attaquant.

### 9. **Cookies**
   - Les **cookies** sont des petits fichiers stockés par ton navigateur lorsque tu visites un site web. Ils permettent, par exemple, de te garder connecté à un site ou de mémoriser tes préférences. Certains cookies, cependant, sont utilisés pour suivre ton activité en ligne, ce qui pose des problèmes de respect de la vie privée.

### 10. **Suivi (Tracking) et Privacy**
   - De nombreux sites web utilisent des techniques de **suivi passif** pour identifier et tracer les visiteurs. Par exemple, les sites peuvent collecter des informations sur le navigateur que tu utilises, les polices installées sur ton ordinateur, ou même le matériel audio/vidéo que tu utilises. Tout cela permet aux entreprises de te suivre même sans cookies.

### 11. **Google Safe Browsing**
   - **Google Safe Browsing** est un service qui permet de protéger les utilisateurs contre les sites malveillants ou de phishing. Google maintient une liste de ces sites, et ton navigateur peut vérifier chaque URL que tu visites pour s'assurer qu'elle ne figure pas sur cette liste noire.

### 12. **Shodan**
   - **Shodan** est un moteur de recherche qui permet de trouver des appareils connectés à Internet. Cela inclut des caméras de surveillance, des serveurs ou même des routeurs. Il est souvent utilisé par les chercheurs en sécurité pour identifier les systèmes vulnérables.

### 13. **Homograph Attack**
   - Une **homograph attack** est une attaque où l'on utilise des caractères qui se ressemblent pour tromper les utilisateurs. Par exemple, remplacer la lettre **l** (minuscule) par un **I** (majuscule). Cela permet de créer des adresses web malveillantes qui semblent légitimes à première vue.

### 14. **Click Hijacking**
   - Le **click hijacking** est une attaque où une page web malveillante piège l'utilisateur en rendant un bouton invisible. Par exemple, tu penses cliquer sur un bouton inoffensif, mais en réalité, tu cliques sur un bouton qui fait une action non désirée, comme partager des informations sensibles.

### 15. **SSL et TLS**
   - **SSL** (Secure Sockets Layer) et **TLS** (Transport Layer Security) sont des protocoles qui sécurisent les communications sur Internet, par exemple, en chiffrant les échanges entre ton navigateur et un serveur (ce qui donne **HTTPS**). TLS est une version améliorée et plus sécurisée de SSL.
   - **TLS 1.2** et **TLS 1.3** utilisent des algorithmes cryptographiques comme **RSA** et **Diffie-Hellman** pour chiffrer les données et assurer l'intégrité et l'authenticité des communications.

### 16. **Attaques sur TLS**
   - **CBC Padding Oracle Attack** : Exploite une vulnérabilité dans le mode de chiffrement CBC utilisé par TLS, permettant à un attaquant de déchiffrer des informations sensibles.
   - **Poodle Attack** : Cette attaque permet de déchiffrer les communications en SSL 3.0, en utilisant une faiblesse dans la manière dont le chiffrement CBC est implémenté.
   - **DROWN** : Utilise des faiblesses dans SSLv2 pour déchiffrer des communications sécurisées par des serveurs vulnérables, compromettant ainsi les données échangées.

### 17. **Firewall et Proxy**
   - **Firewall** : Un **pare-feu** est un système de sécurité qui filtre le trafic réseau entrant et sortant en fonction de règles définies. Il peut bloquer des adresses IP malveillantes ou autoriser uniquement certaines connexions.
   - **Proxy** : Un **proxy** agit comme intermédiaire entre un utilisateur et un serveur. Il peut, par exemple, masquer l'identité du serveur ou filtrer le contenu accessible pour l'utilisateur.
   - **Reverse Proxy** : Un proxy inverse se situe entre les utilisateurs et les serveurs web pour protéger les serveurs en masquant leur localisation réelle.

### 18. **SSH et VPN**
   - **SSH** (Secure Shell) : Utilisé pour accéder à distance à des serveurs de manière sécurisée en chiffrant les communications. Les utilisateurs génèrent une paire de clés (privée et publique) pour authentifier leurs connexions.
   - **VPN** (Virtual Private Network) : Un VPN permet de créer une connexion sécurisée entre ton ordinateur et un réseau distant. Cela permet de naviguer sur Internet de manière plus sécurisée, en particulier lorsque tu utilises des réseaux publics.

## Amphi 3

### 1. **SQL Injection (SQLi)**
   - **SQL** est un langage utilisé pour interagir avec des bases de données relationnelles. Il permet de définir des données et de les manipuler (insertion, mise à jour, suppression, requêtes).
   - Une **injection SQL** est une attaque où l'attaquant manipule une requête SQL pour accéder à des informations sensibles ou modifier la base de données.
   - **Exemple d'injection** : Lors de la connexion à un site, un attaquant peut entrer des commandes SQL dans le champ utilisateur ou mot de passe pour tromper le système. Par exemple, un mot de passe comme `anything' OR '1'='1` pourrait donner accès à des informations.
   - Ces attaques sont classées parmi les plus dangereuses par l'**OWASP**, notamment en 2021 où elles occupaient la première place des vulnérabilités Web.

### 2. **Buffer Overflow (BOF)**
   - Un **buffer overflow** survient lorsque trop de données sont écrites dans un espace mémoire réservé (un buffer), provoquant un dépassement qui peut corrompre des zones mémoire adjacentes.
   - Cela peut entraîner un comportement inattendu, un crash ou même permettre à un attaquant d'exécuter du code malveillant en réécrivant l'adresse de retour d'une fonction.
   - Les attaques de BOF sont des failles classiques de sécurité, largement exploitées depuis les années 90.

### 3. **Return-Oriented Programming (ROP)**
   - Le **ROP** est une technique d'exploitation avancée qui permet à un attaquant de manipuler des morceaux de code légitimes déjà présents en mémoire pour contourner des protections comme **DEP** (Data Execution Prevention).
   - En utilisant des gadgets (petits segments de code) déjà présents, un attaquant peut reconstruire une chaîne d’instructions malveillantes.

### 4. **Accès aux fichiers sous Linux : ACL (Access Control List)**
   - Les **listes de contrôle d'accès (ACL)** permettent de gérer de façon fine les droits d'accès aux fichiers sur un système Linux. Elles étendent les permissions standards **UGO** (User, Group, Other) et **RWX** (Read, Write, eXecute).
   - Un administrateur peut définir des permissions spécifiques pour des utilisateurs ou groupes avec des commandes comme `setfacl` pour modifier les ACL ou `getfacl` pour les visualiser.
   - Par exemple : `setfacl -m u:alice:rw- fichier.txt` donne à **Alice** des permissions de lecture et d’écriture sur un fichier.

### 5. **Authentification sous Linux**
   - Linux utilise plusieurs fichiers et mécanismes pour gérer l'authentification :
     - **/etc/passwd** : Contient les informations de base des utilisateurs.
     - **/etc/shadow** : Contient les mots de passe sous forme hachée, ce qui rend difficile de les lire directement.
   - Le système d'authentification peut être renforcé avec **PAM (Pluggable Authentication Modules)**, qui permet de configurer et d'ajuster finement la manière dont l’authentification est gérée. **PAM** est utilisé pour mettre en place des règles comme le verrouillage après un certain nombre d'échecs de connexion (ex. : `pam_tally2.so`).
   - **MFA (Multi-Factor Authentication)** est une méthode d'authentification utilisant plusieurs facteurs (par exemple, un mot de passe et une application d’authentification comme Google Authenticator) pour renforcer la sécurité.

### 6. **SBSFU (Secure Boot and Secure Firmware Update)**
   - **SBSFU** est une technologie qui assure que seuls les firmwares signés numériquement peuvent être installés sur un appareil. Cela garantit que les mises à jour de firmware proviennent d'une source officielle et évite que des logiciels malveillants ne soient installés.
   - Le **Secure Boot** vérifie l'image du firmware avant son exécution pour s'assurer qu'elle est légitime et non modifiée. Cela aide à protéger les appareils contre les attaques dès leur démarrage.

### 7. **RowHammer**
   - **RowHammer** est une attaque matérielle qui exploite des faiblesses dans la mémoire **DRAM** pour modifier des données sans accès direct.
   - En répétant des accès à certaines cellules de mémoire, un attaquant peut provoquer des perturbations électriques dans les cellules voisines, modifiant ainsi leurs données, ce qui peut permettre d’altérer des informations critiques.

### 8. **Biométrie**
   - La **biométrie** repose sur l'identification des individus à partir de caractéristiques physiques ou comportementales uniques (empreintes digitales, reconnaissance faciale, etc.).
   - Cette technologie est utilisée dans les passeports électroniques, les smartphones et pour l'authentification sécurisée.
   - Bien que robuste, la biométrie peut être vulnérable à des attaques de contrefaçon ou de manipulation si des mesures de sécurité adéquates ne sont pas mises en place.

### 9. **Side Channel Attacks**
   - Les **attaques par canal auxiliaire** exploitent des informations indirectes (comme la consommation d’énergie, le temps d’exécution, ou les émissions électromagnétiques) pour découvrir des clés secrètes ou d'autres informations sensibles.
   - Par exemple, une **attaque par analyse de puissance** peut observer les fluctuations de consommation d'énergie lors d'un calcul cryptographique pour en déduire des informations sur la clé utilisée.

### 10. **ToR (The Onion Router)**
   - **ToR** est un réseau qui anonymise les communications en les acheminant à travers plusieurs relais cryptés. Cela permet aux utilisateurs de masquer leur identité en ligne.
   - Il est souvent utilisé pour contourner la censure et protéger la vie privée. Cependant, **ToR** est aussi parfois utilisé à des fins illicites sur le **Dark Web**.

### 11. **PKI (Public Key Infrastructure)**
   - La **PKI** repose sur des certificats numériques pour établir une chaîne de confiance. Les certificats **X.509** sont utilisés pour valider les identités sur des réseaux sécurisés (comme les sites HTTPS).
   - Chaque certificat est signé par une **Autorité de Certification (CA)**, qui garantit que la clé publique associée appartient bien à l'entité indiquée.

## Amphi 4

### 1. **XSS (Cross-Site Scripting)**
   - **Principe** : Il s'agit d'une injection de code malveillant dans une page web, souvent en JavaScript, qui s'exécute côté client. Un exemple de code : `<script>alert('Bonjour!')</script>`.
   - **Types d'attaques XSS** :
     - **Réfléchie** : Le code est temporairement injecté via la réponse du serveur.
     - **Persistante** : Le code est stocké sur le serveur et exécuté par les utilisateurs.
     - **Basée sur le DOM** : Le code est exécuté uniquement côté client, sans intervention du serveur.
   - **Effets possibles** :
     - **Defacement** : Changer l'apparence du site (exemple : afficher un message d'attaque).
     - **Redirection** : Rediriger les utilisateurs vers un autre site.
     - **Vol de cookies** : Récupérer les cookies d'authentification de l'utilisateur, permettant à l'attaquant de se connecter en tant que lui.

   - **Contremesures** :
     - Assainir et valider strictement les entrées.
     - Appliquer des règles de sécurité pour les cookies (e.g., HttpOnly, Secure).
     - Utiliser des **Content Security Policies (CSP)** pour limiter les sources de scripts.

### 2. **CSRF (Cross-Site Request Forgery)**
   - **Principe** : L'attaquant incite un utilisateur authentifié à exécuter des actions non désirées sur un site auquel il est connecté, comme un transfert d'argent. Par exemple, un formulaire caché en HTML pourrait être soumis automatiquement, exploitant l'authentification active de la victime.
   - **Contremesures** :
     - Implémenter des tokens CSRF pour vérifier l'origine des requêtes.
     - Limiter la durée de vie des sessions et invalider les sessions inactives.
     - Utiliser la politique de même origine (same-origin policy) pour restreindre les requêtes entre différents domaines.

### 3. **SSRF (Server-Side Request Forgery)**
   - **Principe** : L'attaquant utilise le serveur cible pour envoyer des requêtes à d'autres serveurs, souvent internes, et contourner les restrictions de pare-feu.
   - **Exemples** : Un fichier uploadé contenant une URL malveillante pourrait être récupéré par le serveur. Les fonctions PHP comme `file_get_contents()` peuvent être exploitées dans ce type d'attaque.
   - **Contremesures** :
     - Utiliser des listes blanches pour autoriser uniquement certaines adresses IP ou DNS.
     - Restreindre les protocoles autorisés (exemple : bloquer FTP si non nécessaire).

### 4. **LFI (Local File Inclusion) et RFI (Remote File Inclusion)**
   - **Principe** : Permet à un attaquant d'exécuter des fichiers sur le serveur en manipulant les paramètres d'URL.
     - **LFI** : Inclusion de fichiers locaux (exemple : `/etc/passwd`).
     - **RFI** : Inclusion de fichiers distants (exemple : un fichier malveillant hébergé sur un autre serveur).
   - **Contremesures** :
     - Désactiver l'option `allow_url_include` dans le fichier `php.ini`.
     - Filtrer et valider les entrées pour éviter l'inclusion de fichiers non autorisés.

### 5. **JWT (JSON Web Token)**
   - **Principe** : Un **JWT** est un jeton d'authentification structuré en trois parties : Header, Payload, et Signature. Il permet l'authentification stateless, souvent utilisé dans les API et les applications mobiles.
     - **Header** : Indique l'algorithme de hachage (ex. : HMAC, SHA256).
     - **Payload** : Contient les informations sur l'utilisateur (ex. : ID, nom).
     - **Signature** : Assure que le jeton n'a pas été modifié.
   - **Attaques possibles** :
     - **Signature stripping** : Modifier l'algorithme en `none` pour contourner la vérification de signature.
     - **HMAC spoofing** : Utiliser la clé publique RSA pour signer des jetons après avoir changé l'algorithme en HMAC.
   - **Contremesures** :
     - Vérifier que l'algorithme de signature est conforme.
     - Utiliser des clés sécurisées pour signer et vérifier les jetons.

### 6. **Shodan**
   - **Principe** : Un moteur de recherche pour les appareils connectés à Internet. Il permet de découvrir des systèmes vulnérables, comme des caméras de surveillance ou des serveurs mal configurés.
   - **Usage** : Utilisé pour l'audit de sécurité, mais également exploité par des attaquants pour détecter des failles dans les infrastructures connectées.

### 7. **nmap**
   - **Principe** : Outil open source utilisé pour l'exploration réseau et l'audit de sécurité. Il permet de scanner des adresses IP pour découvrir les ports ouverts et les services actifs.
   - **Exemple** : Une commande comme `nmap -p80,443 localhost` scanne les ports 80 (HTTP) et 443 (HTTPS) d'une machine locale.
   - **Contremesures** :
     - N'utiliser cet outil que sur des systèmes autorisés pour éviter des actions illégales, car le scan non autorisé est illégal dans plusieurs pays.

### 8. **Forensic (investigation numérique)**
   - **Principe** : L'enquête numérique vise à analyser les systèmes après une attaque pour identifier la source de la menace. Elle se déroule souvent en plusieurs étapes :
     - **Acquisition** des données.
     - **Analyse** pour identifier les actions de l'attaquant.
     - **Remédiation** pour corriger les vulnérabilités.
     - **Rapport** sur les découvertes.
   - **Défis** :
     - Accéder à la mémoire vive.
     - Analyser un volume massif de données pour identifier les éléments pertinents.

### 9. **TEE (Trusted Execution Environment) et TPM (Trusted Platform Module)**
   - **TEE** : C'est un environnement sécurisé dans lequel du code sensible peut s'exécuter en toute sécurité, isolé du reste du système.
   - **TPM** : Un module matériel utilisé pour sécuriser les processus liés aux clés cryptographiques.
   - **Exemple** : **Intel SGX** est une implémentation de TEE qui divise la mémoire en enclaves protégées. Cependant, des attaques comme **Plundervolt** et **SGAxe** ont montré que même ces environnements peuvent être compromis.
  
### 10. **Kerberos**
   - **Principe** : Système d'authentification utilisant un tiers de confiance (AS - Authentication Server) et des tickets pour authentifier les utilisateurs de manière sécurisée.
   - **Fonctionnement** : 
     - Le client demande un ticket auprès de l'AS.
     - L'AS délivre un **Ticket Granting Ticket (TGT)**.
     - Le client utilise ce TGT pour demander des services auprès du **Ticket Granting Service (TGS)**.
   - **Utilité** : Largement utilisé dans les environnements où la sécurité des identités est cruciale, comme dans les grandes entreprises.

## Tp 1

Voici une explication détaillée des concepts et méthodologies abordés dans ce **TP de sécurité** en cryptographie et réseaux, afin que tu puisses bien comprendre les notions clés pour un QCM.

### 1. **Comparaison HTTP vs HTTPS (Exercice 1)**

   - **HTTP (Hypertext Transfer Protocol)** : Les requêtes et réponses HTTP sont envoyées en clair, ce qui signifie que tout le contenu, y compris les informations sensibles (mots de passe, identifiants), peut être intercepté par un attaquant lors d'une attaque de type "Man-in-the-Middle" (MitM).
   - **HTTPS (HTTP Secure)** : Basé sur le protocole TLS (Transport Layer Security), HTTPS chiffre les communications après une négociation sécurisée, garantissant la **confidentialité** (les données ne peuvent pas être lues par des tiers) et l'**intégrité** (les données ne peuvent pas être modifiées à leur insu). Cela protège contre les interceptions et les usurpations de session.
   - **Analyse des trames PCAP** : Dans le TP, tu es amené à analyser des fichiers `.pcapng` pour observer ces différences entre HTTP et HTTPS. Pour filtrer les données, on peut utiliser les adresses IP et les protocoles dans un outil comme **Wireshark**.

### 2. **Digicode et Probabilité (Exercice 2)**

   - **Nombre de combinaisons possibles** : Le digicode comporte 4 chiffres, avec 10 possibilités pour chaque chiffre. Ainsi, Alice peut générer \( 10^4 \) combinaisons possibles, soit 10 000 codes.
   - **Optimisation des essais de Bob** : Bob peut utiliser une stratégie d'élimination en observant si la lumière est rouge (erreur) ou verte (bon chiffre). Dans le pire des cas, pour chaque chiffre, il fera 10 essais, soit un total de 40 essais maximum (10 essais par chiffre et 4 chiffres à deviner).
   - **Méthodologie** : Il s'agit ici d'une application de la combinatoire et des probabilités, avec un scénario de recherche séquentielle optimisée.

### 3. **Attaque par Malleabilité en Chiffrement CBC (Exercice 3)**

   - **CBC (Cipher Block Chaining)** : Mode de chiffrement symétrique utilisé pour protéger les messages. Chaque bloc de texte clair est XORé avec le bloc chiffré précédent avant d'être chiffré. Cela améliore la sécurité par rapport au mode **ECB**, où des blocs identiques génèrent des blocs chiffrés identiques.
   - **Malleabilité** : En CBC, il est possible pour un attaquant de modifier certains blocs de texte chiffré pour provoquer des modifications contrôlées dans le texte déchiffré. Par exemple, un attaquant pourrait modifier le nombre de pizzas commandées dans un message (de 2 à 200) en altérant les blocs chiffrés.
   - **Méthodologie** : Tu dois calculer un nouvel IV et de nouveaux blocs chiffrés pour manipuler le message déchiffré, en exploitant la propriété du XOR. L'énoncé te guide pour choisir des valeurs spécifiques de IV, C1, C2 et C3.

### 4. **Hachage et Bruteforce (Exercice 4)**

   - **Hachage avec Salt** : Un mot de passe peut être haché avec un algorithme comme **MD5crypt**. Le salt est une valeur aléatoire ajoutée au mot de passe avant le hachage pour rendre plus difficile les attaques par tables arc-en-ciel.
   - **Bruteforce** : Le TP te demande de retrouver des mots de passe à partir de leurs hachages, en utilisant un script bash et l'outil **openssl** pour générer les hachages correspondants. L'idée est de tester des mots de passe à partir d'une base de données (comme **phpbb**).
   - **Méthodologie** : Tu compares le hachage généré à partir du mot de passe testé avec le hachage cible. Si les deux correspondent, tu as trouvé le mot de passe.

### 5. **Recherche de CVE (Exercice 5)**

   - **CVE (Common Vulnerabilities and Exposures)** : Une CVE est une vulnérabilité publiquement connue, souvent exploitée par les attaquants pour compromettre des systèmes. Tu es invité à rechercher une CVE spécifique pour pénétrer un site web configuré dans un environnement Docker.
   - **Méthodologie** : Après avoir lancé un serveur Docker avec un site vulnérable, tu utilises des bases de données de vulnérabilités comme le **NVD (National Vulnerability Database)** pour trouver une faille exploitable. Il s'agit de comprendre comment exploiter une faille documentée.

### 6. **Attaque au Président (Exercice 6)**

   - **GPG (GNU Privacy Guard)** : Un outil de chiffrement et de signature numérique qui utilise la cryptographie à clé publique. Cet exercice t'invite à manipuler des clés GPG pour envoyer, signer et vérifier des messages entre le président et la secrétaire d'une entreprise fictive.
   - **Chiffrement asymétrique** : L'idée est que chaque acteur possède une clé publique (pour chiffrer les messages) et une clé privée (pour déchiffrer et signer).
   - **Méthodologie** :
     - Importer des clés publiques et privées avec `gpg --import`.
     - Chiffrer des messages avec `gpg --encrypt`.
     - Signer des messages avec `gpg --sign`.
     - Vérifier les signatures avec `gpg --verify`.
   - L'exercice te fait alterner entre le rôle de secrétaire et de président, te permettant de manipuler les clés et d'apprendre à reconnaître les attaques par substitution de clé.

### 7. **Audit SSL/TLS des Sites Bancaires (Exercice 7)**

   - **SSL/TLS** : Protocoles qui assurent la sécurité des communications sur Internet. **TLS** (Transport Layer Security) est l'évolution de **SSL** (Secure Sockets Layer), offrant une meilleure sécurité.
   - **Audit avec SSL Labs** : Tu es invité à utiliser le site **SSL Labs** pour tester la configuration SSL/TLS de plusieurs banques. Le test analyse des éléments comme les versions de TLS, les suites de chiffrement supportées, et la solidité des certificats.
   - **Méthodologie** :
     - Accéder au site **SSL Labs** et lancer un test sur les domaines bancaires.
     - Analyser les résultats : versions de TLS supportées, longueur des clés RSA, support de Forward Secrecy, etc.
     - Classer les banques en fonction de la robustesse de leur configuration SSL/TLS.

---

### Concepts clés à maîtriser :
- **HTTPS** vs **HTTP** : Confidentialité et intégrité des données.
- **Modes de chiffrement symétrique** : ECB et CBC, avec une attention particulière à la malleabilité en CBC.
- **Hachage avec salt** et bruteforce de mots de passe.
- **Cryptographie asymétrique** avec GPG pour le chiffrement, la signature et la vérification.
- **Audit de la sécurité SSL/TLS** : Importance des protocoles à jour, des certificats robustes, et des suites de chiffrement.

## Tp 2 

### 1. **Sécurité SSH (Exercice 1)**

   - **SSH (Secure Shell)** : Un protocole réseau qui permet de se connecter à distance de manière sécurisée. SSH chiffre les communications, assurant ainsi confidentialité et intégrité.
   - **Audit SSH avec ssh-audit** :
     - L'outil **ssh-audit** analyse les configurations du serveur SSH (127.0.0.1 dans ce cas) pour vérifier la robustesse des algorithmes utilisés.
     - **Key Exchange Algorithms (Algorithmes d'échange de clés)** : Ceux-ci permettent de sécuriser l'établissement de la connexion. Certains algorithmes comme **sntrup761x25519** sont modernes et résistants aux attaques quantiques.
     - **Host Key Algorithms (Algorithmes de clé d'hôte)** : Ils déterminent comment le serveur s'authentifie. Les algorithmes **ed25519** et **rsa-sha2-512** sont préférés pour leur sécurité.
     - **MACs (Message Authentication Codes)** : Utilisés pour garantir l'intégrité des messages échangés. Les versions basées sur **SHA-2** sont privilégiées pour éviter les vulnérabilités de **SHA-1**.
     - **Ciphers (Chiffres)** : Les algorithmes de chiffrement comme **AES** sont largement utilisés en raison de leur sécurité. Les algorithmes vulnérables comme **chacha20-poly1305** sont désactivés.
     - **Attaques DHEat DoS** : L'audit signale également des attaques potentielles par déni de service (DoS) en utilisant des algorithmes de Diffie-Hellman avec une configuration non optimisée.

   - **Modification du fichier sshd_config** : 
     - On modifie la configuration SSH pour améliorer la sécurité en restreignant l'utilisation des algorithmes vulnérables et en privilégiant ceux considérés comme sécurisés. Après chaque modification, il est important de redémarrer le service SSH avec `systemctl restart sshd`.

### 2. **HTTPS et Cryptographie avec OpenSSL (Exercice 2)**

   - **Certificat SSL/TLS** : Les certificats SSL/TLS assurent que les communications entre le navigateur et le serveur sont chiffrées. Le certificat est émis par une **autorité de certification (CA)**.
   - **Création d'une Root CA** :
     - **Générer une clé RSA** : Avec OpenSSL, on génère une clé privée de 4096 bits qui servira à signer les certificats. La clé est utilisée pour créer un **certificat auto-signé** pour une autorité racine (root CA).
     - **Création d'une autorité intermédiaire** : Cette autorité intermédiaire est signée par la root CA. Les certificats intermédiaires permettent d'ajouter une couche de confiance, utilisée par de nombreux navigateurs pour valider les certificats.
     - **PKCS12 et PEM** : Les certificats et clés sont exportés au format **PKCS12** (format binaire) et **PEM** (format texte), ce dernier étant utilisé par les serveurs web pour configurer SSL.
   
   - **Configuration du serveur Apache** :
     - Apache doit être configuré pour utiliser ces nouveaux certificats. On modifie le fichier **default-ssl.conf** pour indiquer les bons chemins des fichiers de clé et certificat. Les modules SSL doivent également être activés dans Apache (`a2enmod ssl`).
     - On vérifie le bon fonctionnement de HTTPS via le navigateur et on s'assure que les certificats sont bien importés dans le navigateur.

### 3. **EFAIL (Exercice 3)**

   - **EFAIL** : Une attaque sur le chiffrement des emails qui permet à un attaquant de manipuler des parties du texte chiffré pour modifier ou révéler des parties du texte en clair après déchiffrement. Elle exploite la malleabilité des messages chiffrés en mode **CBC** (Cipher Block Chaining).
   - **Modification du fichier efail_exercice.py** :
     - Le TP te demande de compléter la fonction **efail()** pour manipuler le **ciphertext** (texte chiffré) et son **IV** (vecteur d'initialisation) afin de reconstruire le texte original dans les emails interceptés. Le but est de remplacer certaines parties du message pour voir comment l'attaque fonctionne.
     - L'attaque modifie les blocs de chiffrement pour obtenir des informations en clair à partir d'un message chiffré, ce qui démontre les faiblesses de certains modes de chiffrement lorsque la malleabilité n'est pas correctement protégée.

### 4. **TLS 1.3 (Exercice 4)**

   - **TLS (Transport Layer Security)** : La version 1.3 de TLS améliore la sécurité par rapport aux versions précédentes, en supprimant certains algorithmes faibles et en rendant les négociations plus rapides.
   - **Vérification des versions TLS** :
     - Utilisation de la commande **openssl s_client** pour vérifier que les sites web comme **google.com** et **ayesh.me** supportent les versions TLS 1.2 et 1.3.
     - **testssl.sh** : Ce script permet de tester la configuration SSL/TLS d'un site. Il vérifie si les versions TLS et les suites de chiffrement sont à jour et sécurisées.
   
   - **Configuration TLS sur Apache** :
     - Le TP te demande de jouer avec les paramètres cryptographiques d'Apache, comme les **ciphersuites** et les **courbes elliptiques**. Ces paramètres sont définis dans le fichier **ssl.conf** et influencent la sécurité des connexions.
     - On peut forcer l'utilisation de certaines ciphersuites comme **TLS_AES_128_GCM_SHA256** ou restreindre les courbes elliptiques à celles qui sont résistantes aux attaques, comme **X25519**.

### Concepts clés à maîtriser :
1. **SSH** : Importance du chiffrement des communications et de la configuration sécurisée des algorithmes dans SSH.
2. **SSL/TLS et certificats** : Compréhension des certificats, autorité de certification, et configuration SSL sur les serveurs web.
3. **EFAIL** : Attaque exploitant la malleabilité du chiffrement CBC pour manipuler des emails chiffrés.
4. **TLS 1.3** : Améliorations de la sécurité dans la version 1.3 de TLS et configuration des ciphersuites et courbes elliptiques.

## Tp 3

### 1. **Buffer Overflow (Exercice 1)**

   - **Buffer Overflow (BoF)** : Un **débordement de buffer** se produit lorsqu'un programme écrit plus de données dans un espace mémoire (buffer) que ce que celui-ci peut contenir. Cela peut écraser des zones mémoire adjacentes et entraîner des comportements imprévisibles, comme l'exécution de code malveillant.
   - **Code de l'exercice** :
     - Le programme comporte une variable `modified` qui est initialisée à 0. L'objectif est de modifier cette variable à partir de l'entrée utilisateur sans modifier directement le code source.
     - **Analyse** : Lorsque l'utilisateur entre une chaîne de caractères trop longue pour le buffer (64 octets), cette chaîne peut écraser la mémoire où est stockée la variable `modified`. En fournissant une entrée spécialement conçue, tu peux donc écraser cette variable et déclencher le message "you have changed the ‘modified’ variable".
     - **Méthodologie** : En injectant une chaîne plus longue que 64 caractères, on provoque un débordement qui modifie la valeur de `modified`. L'idée est d'exploiter cette vulnérabilité pour contrôler le programme.

### 2. **Integer Overflow (Exercice 2)**

   - **Integer Overflow** : Cette vulnérabilité survient lorsque les calculs effectués sur des variables de type entier dépassent la capacité maximale de ces variables, entraînant des résultats incorrects ou imprévisibles.
   - **Analyse du code** :
     - Le programme effectue des opérations avec des entiers non signés (`unsigned char`), dont la taille maximale est 255. En provoquant un dépassement (somme de `len1` et `len2` supérieure à 255), il est possible d'affecter la variable `d.secret`, et ainsi de la modifier sans accès direct.
     - **Méthodologie** : En manipulant les longueurs d'entrée (`len1` et `len2`), tu peux provoquer un dépassement d'entier et ajuster la valeur de `d.secret`. L'objectif est de définir cette variable à la valeur hexadécimale `0x42` en contrôlant les entrées fournies au programme.

### 3. **Reverse Engineering (Exercice 3)**

   - **Reverse Engineering avec Ghidra** : Cet exercice consiste à analyser un programme compilé pour architecture ARM à l'aide de l'outil de rétro-ingénierie **Ghidra**. L'objectif est de comprendre comment le programme fonctionne et de retrouver un mot de passe à partir de l'analyse du code binaire.
   - **Méthodologie** :
     - Utiliser Ghidra pour désassembler le binaire et observer le code source reconstitué.
     - Analyser les conditions de comparaison (par exemple, la vérification du mot de passe) et déterminer comment contourner les mécanismes de sécurité du programme en trouvant la bonne entrée (mot de passe).

### 4. **Return-Oriented Programming (Exercice 4)**

   - **Return-Oriented Programming (ROP)** : Une attaque **ROP** permet à un attaquant d'exécuter du code malveillant en utilisant des fragments de code légitime déjà présent dans la mémoire du programme. Cela contourne des mesures de sécurité comme **DEP** (Data Execution Prevention).
   - **ROPgadget** :
     - Cet outil permet de trouver des gadgets (fragments de code se terminant par une instruction de retour `RET`) dans un binaire, qui peuvent être chaînés pour exécuter du code arbitraire sans avoir besoin d'injecter de nouveau code.
     - **Exercice** : Utiliser `ROPgadget` pour analyser le binaire compilé à partir du fichier `vulnerable.c`. Ce fichier contient une vulnérabilité de débordement de buffer, et l'objectif est d'exploiter cette vulnérabilité pour créer une chaîne ROP qui ouvre un shell sur la machine.
   - **Méthodologie** :
     - Compiler le binaire avec les options nécessaires pour désactiver certaines protections comme le **stack protector**.
     - Utiliser `ROPgadget` pour identifier les gadgets dans le binaire.
     - Écrire un script Python avec la bibliothèque **pwn** pour envoyer la chaîne de gadgets au programme, exploitant ainsi la vulnérabilité pour obtenir un accès shell.

   - **Mécanismes de défense** :
     - **DEP (Data Execution Prevention)** : Cette mesure empêche l'exécution de code dans certaines régions de la mémoire réservées aux données. Elle protège contre les attaques de type buffer overflow en empêchant l'exécution de code injecté.
     - **ASLR (Address Space Layout Randomization)** : Cette technique rend plus difficile l'exploitation des vulnérabilités en modifiant de manière aléatoire l'emplacement des segments mémoire lors du chargement d'un processus. Cela complique la création de chaînes ROP efficaces.

   - **Preuve que DEP et ASLR sont actifs** :
     - Utilisation de la commande `readelf -l rop` pour vérifier que le segment de pile n'est pas exécutable.
     - La commande `cat /proc/sys/kernel/randomize_va_space` doit retourner 2 pour indiquer que l'ASLR est activé.

### 5. **BoF (Buffer Overflow) le retour (Exercice 5)**

   - **Exploitation d'un débordement de buffer** : Le programme de cet exercice contient une vulnérabilité similaire à celle du premier exercice. L'objectif est de modifier la variable `modified` pour qu'elle prenne la valeur spécifique `0x61626364`.
   - **Méthodologie** :
     - En exploitant la vulnérabilité de débordement de buffer, il est possible d'écraser la variable `modified` en fournissant une chaîne d'entrée spécialement construite.
     - L'objectif est d'injecter une valeur particulière (hexadécimale) pour obtenir la sortie voulue du programme.

---

### Concepts clés à maîtriser :
1. **Buffer Overflow (BoF)** : Comprendre comment un débordement de buffer permet de manipuler la mémoire d'un programme et d'exécuter du code arbitraire.
2. **Integer Overflow** : Savoir comment un dépassement d'entier peut être exploité pour modifier des variables critiques.
3. **Reverse Engineering** : Utiliser des outils comme Ghidra pour analyser des binaires et comprendre leur fonctionnement.
4. **Return-Oriented Programming (ROP)** : Exploitation avancée de vulnérabilités pour contourner les protections comme DEP et ASLR.
5. **Mécanismes de défense (DEP, ASLR)** : Comprendre comment ces techniques renforcent la sécurité et compliquent l'exploitation des vulnérabilités.

## Tp 4

### Exercice 1 : Analyse d'une trace réseau post-mortem

Cet exercice implique l'analyse de fichiers logs et d'une trace réseau pour identifier une attaque et comprendre comment elle s'est déroulée. Voici les étapes importantes :

1. **Adresse du serveur attaqué** : L'adresse IP du serveur cible de l'attaque doit être trouvée dans la trace PCAP en analysant les paquets réseau avec **Wireshark**.
   
2. **Nature de l'attaque** : L'attaque pourrait être un vol de données ou une compromission de système. Il s'agit de vérifier les types de paquets envoyés et reçus pour détecter des anomalies, comme des tentatives de connexion suspectes ou des transferts de données non autorisés.

3. **Dispositif source de l'attaque** : Identifier l'adresse IP ou MAC de l'appareil à l'origine de l'attaque, souvent trouvé dans les en-têtes des paquets capturés.

4. **Identifiants de connexion** : Les logs doivent révéler les tentatives de connexion avec des identifiants (login/password). Le but est d'extraire les derniers identifiants utilisés par l'attaquant.

5. **Page d'authentification compromise** : Il faut déterminer quelle page Web (comme `login.php`) a été utilisée par l'attaquant pour accéder au système.

### Exercice 2 : Analyse de logs

Cet exercice repose sur l'analyse des logs d'accès pour détecter une attaque par brute-force sur un service ERP non à jour. Voici les points clés :

1. **Début de l'attaque brute-force** : Les requêtes répétées dans les logs (généralement des requêtes POST) révèlent le moment où l'attaque brute-force a commencé. Ces requêtes visent souvent une page de connexion pour deviner les identifiants de manière répétée.

2. **Extensions de fichiers testées** : Après avoir obtenu l'accès, l'attaquant essaie plusieurs extensions de fichiers (comme `.php`, `.phtml`, `.php3`, etc.) pour tenter d'exploiter des vulnérabilités sur des scripts PHP. Les logs révèlent les extensions essayées.

3. **Extension vulnérable utilisée** : À partir des logs, il faut identifier l'extension qui a permis à l'attaquant d'exploiter le serveur (par exemple `.phar`).

4. **Commandes shell exécutées** : Une fois le fichier vulnérable identifié, l'attaquant utilise des commandes shell pour exécuter des actions sur le serveur, comme télécharger des fichiers ou exécuter des scripts malveillants.

### Exercice 3 : Analyse post-mortem d'un système de fichier Android

Cet exercice porte sur l'analyse d'un système de fichier Android compromis. Voici les principales étapes :

1. **Chemin de la base de données des SMS/MMS** : Les SMS et MMS sont souvent stockés dans une base de données SQLite. Il faut trouver le chemin vers cette base, souvent situé dans le répertoire `com.android.providers.telephony/databases/`.

2. **Recherche de SMS/MMS contenant des identifiants** : En explorant la table des messages dans la base de données, il s'agit de retrouver des SMS/MMS mentionnant des identifiants de connexion ERP compromis. Le champ `id` du message et l'heure d'envoi doivent être notés.

3. **Identifiants compromis et chemin des fichiers** : Le fichier contenant les identifiants est souvent stocké dans un répertoire accessible à partir du système de fichier. Il faut également retrouver le mot de passe associé à l'utilisateur RH.

4. **Nom de l'application malveillante et permissions** : À l'aide des fichiers de système, il faut identifier l'application malveillante installée et ses permissions. Les permissions critiques (accès SMS, démarrage automatique, etc.) doivent être relevées. De plus, la date d'installation de l'application doit être déterminée.

5. **Analyse des événements du calendrier** : En examinant la base de données des événements du calendrier, il est possible de détecter des événements ou rendez-vous suspects autour de la date d'installation de l'application malveillante, ce qui peut indiquer un lien entre ces événements et l'attaque.

### Exercice 4 : Reverse Engineering Android

Cet exercice consiste à analyser un fichier APK malveillant pour comprendre son comportement.

1. **Langage de l'application** : En regardant les fichiers du projet (souvent `.java` ou `.xml`), il est possible de déterminer le langage utilisé pour développer l'application (généralement Java ou Kotlin).

2. **Permissions de l'application** : Avec la commande **`aapt dump permissions`**, il est possible d'extraire les permissions que l'application requiert, comme l'accès aux SMS ou à Internet.

3. **Version de la librairie `androidx:core:core`** : En explorant le répertoire `META-INF` dans le fichier APK décompressé, on peut retrouver la version de cette librairie utilisée par l'application.

4. **Langage du code décompilé** : En décompilant le fichier APK avec un outil comme **JADX**, le code source peut être récupéré. Le langage généré par la décompilation est souvent du Java, même si l'application a été développée en Kotlin.

5. **Adresse du serveur de réception des SMS** : L'analyse des fichiers Java dans le répertoire `output` permet de retrouver l'adresse du serveur vers lequel les SMS sont envoyés.

6. **Analyse du fichier `ReverseShell.java`** : Ce fichier est souvent utilisé pour établir une connexion inversée (reverse shell), permettant à l'attaquant de prendre le contrôle du téléphone. Il est important de comprendre son fonctionnement pour évaluer l'étendue de la compromission.

---

### Concepts clés à maîtriser :
1. **Analyse de traces réseau (Wireshark)** : Savoir identifier des attaques à partir de paquets capturés.
2. **Logs d'accès (brute-force)** : Comprendre comment une attaque brute-force se manifeste dans les logs.
3. **SQLite (SMS/MMS sur Android)** : Maîtriser les commandes SQLite pour extraire des données critiques.
4. **Permissions Android** : Savoir identifier et analyser les permissions d'une application Android malveillante.
5. **Reverse Engineering d'APK** : Utiliser des outils comme `aapt` et `JADX` pour extraire et analyser les informations d'une application Android.
