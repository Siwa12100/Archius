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

---

En résumé, cette présentation couvre une vaste gamme de sujets allant des bases de la cybersécurité et de la cryptographie à des questions plus pratiques telles que la gestion des mots de passe, le chiffrement des emails, et les cadres juridiques comme le RGPD. Les menaces et les mesures de protection sont également largement abordées, avec des exemples historiques et modernes.