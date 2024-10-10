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