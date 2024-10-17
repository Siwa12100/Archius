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

