# TP 1 securite

## Exercice 1

En comparant les trames HTTP et HTTPS, on observe que le contenu des requêtes et réponses HTTP est visible en clair, exposant les données sensibles à une interception. En revanche, en HTTPS, après la phase de négociation TLS, tout le trafic est chiffré, rendant illisible les échanges. HTTPS assure donc la confidentialité et l'intégrité des données, tandis qu'HTTP reste vulnérable aux attaques comme l'espionnage et l'usurpation de session.

## Exercice 2

### 1.)
Le nombre de codes que peut générer Alice est `10 ^ 4` puisqu'elle a 10 choix pour chaque chiffre, et qu'il y a 4 chiffres à choisir. 

### 2.)
Si Bob s'y prend bien, c'est à dire que pour chaque chiffre du code à tester, il procède par élimination en regardant si la led est rouge ou verte, il trouvera chaque chiffre du code en, au pire, 10 essais. 
Dans ce sens, étant donné qu'il y a 4 chiffres à trouver, il fera dans le pire des cas `10 * 4` tentatives.

## Exercice 3

Nous connaissons P'1 (0111 1111 0111) et C1. L'objectif est de calculer IV'. Supposons que C1 = 1001 0110 1100.

On calcule : IV′ = P′1 ⊕ C1 = 111010011011.

On a ensuite : P′3 = C′2 ⊕ C′1

Nous connaissons P'3 et nous allons choisir C'1, puis calculer C'2. On choisit C'1 = 0000 0000 0000. 

On calcule donc : C′ 2 = P′3 ⊕ C′1 = 100010011011.

D'après l'énoncé, on a ainsi : C′3 = C1. Cela signifie que C'3 = C1 = 1001 0110 1100.

À partir de C'3, les blocs chiffrés sont copiés des blocs du message original.

## Exercice 4

### 1.)

Les hachages de mots de passe commençant par `$1$` sont issus de l'algorithme **MD5crypt** *([source](https://files-radiatorsoftware-com.translate.goog/radiator/ref/User-Password.html?_x_tr_sl=en&_x_tr_tl=fr&_x_tr_hl=fr&_x_tr_pto=rq))*.

De plus, la commande `openssl passwd -1 -salt BBBB 'TOTO'` qui nous renvoie `$1$BBBB$/bQvefstOPD7TtXSYKAvJ/` nous permet d'en déduire que le salt est le passage suivant le `$1$`. On en déduit donc que dans le cas du hash que l'on analyse, le salt est `AAAA`.

On vérifie ensuite que le mot de passe du hash analysé est `!!1331xxx`. Pour cela on lance la commande `openssl passwd -1 -salt AAAA '!!1331xxx'` qui nous renvoie bien le hash que l'on cherche `$1$AAAA$H9wXcd/WaaomJUgWKFspy.`.

### 2.)

Pour trouver les mots de passe, nous avons utiliser le script suivant :

```bash
#!/bin/bash

if [ $# -ne 2 ]; then
    exit 1
fi

fichier_mdp=$1
hash_recherche=$2
echo "hash rech. : $hash_recherche"
sel=$(echo $hash_recherche | cut -d'$' -f3)
echo "sel : $sel"

while IFS= read -r mot_de_passe
do
    mdp_test=$(printf '%q' "$mot_de_passe")
    hash_calcule=$(openssl passwd -1 -salt "$sel" "$mot_de_passe")
    echo "Test du mot de passe : $mdp_test"
    echo "Hash calculé : $hash_calcule"
    
    if [ "$hash_calcule" == "$hash_recherche" ]; then
        echo "Mot de passe trouvé : $mot_de_passe"
        exit 0
    fi
done < "$fichier_mdp"

echo "Mot de passe non trouvé dans la liste."
exit 1
```

Nous avons donc trouvé les mots de passe suivants :

* `$1$BABA$DOzBWHNx08SgVSX/YuYvC/` --> batman 

* `$1$CACA$XLWo4OqFFCYICqYrZ0y5i/` --> enigma

## Exercice 5

Nous avons trouvé les réponses sur le [site suivant](https://nvd.nist.gov/vuln/detail/CVE-2014-7232). 

* user : `insite` ---> mdp : `2getin`
* user : `xruser` ---> mdp : `4$xray`
* user : `root` ---> mdp : `#superxr`

## Exercice 6

### 1.)

Commande réalisée : `gpg --import keys/secretary.prv`.

### 2.)
**Message reçue :**

* URGENT: Demande de virement en urgence.
Bonjour, je te contacte car je suis en voyage depuis quelques jours avec les collégues.
On est sur le point de signer un gros contrat mais il nous manque des fonds pour les convaincre.
Peux-tu me faire un virement au RIB ci-joint ? Pas besoin d'une grosse somme, 10.000 euros devraient suffir.

### 3.)

Commande réalisée : `gpg --import keys/president.pub`

### 4.)

Commande réalisée : `gpg -r president --sign --encrypt q2/message2.txt`

### 5.)

Commande réalisées :

```bash
gpg --delete-secret-keys secretary
gpg --import keys/president.prv
```

### 6.)

Commandes réalisées :

```bash
gpg --encrypt -r president q3/message3.txt
cat q3/message3.txt
```

**Message :**

* URGENT: C'est une arnaque ! 
Bonjour, je vous remercie pour votre message, je n'ai absolument pas demandé de virement !!

### 7.)

Commandes réalisées :

```bash
gpg --delete-secret-key president
gpg --import keys/secretary.prv
```

### 8.)

Commande réalisée : `gpg --verify q3/message3.txt.sig q3/message3.txt`

### 9.)

Commande réalisée : `gpg --import keys/.attacker.pub`

### 10.)

Commande réalisée : `gpg --verify q4/message1.txt.sig output.txt`

### 11.) 

Commandes réalisées :

```bash
gpg --list-keys
gpg --list-options show-photos --fingerprint AD1D5FA5298C9352D67A8059C3BD7A0737788DB5
``` 

On récupère la photo avec "Martin Coquin"...

## Exercice 7


### 1. **mabanque.bnpparibas**
   - **Faiblesses :**
     - Utilise encore **TLS 1.0 et TLS 1.1**, qui sont des versions obsolètes et non sécurisées.
     - Le certificat **RSA de 2048 bits** est moins robuste que les certificats de 4096 bits observés sur d'autres domaines.
   - **Forces :**
     - Bonne reconnaissance du certificat par les navigateurs, mais cela n'atténue pas les faiblesses des protocoles plus anciens.

   **Justification du classement :** L'utilisation de TLS 1.0 et TLS 1.1 est un sérieux problème de sécurité, car ces protocoles sont vulnérables. Bien que le certificat soit valide et reconnu, l'usage de ces anciens protocoles le place en dernière position.

### 2. **www.labanquepostale.fr**
   - **Faiblesses :**
     - **TLS 1.1** activé, ce qui n'est plus recommandé.
     - Quelques faiblesses mineures dans certaines suites de chiffrement.
   - **Forces :**
     - Utilisation du **TLS 1.2** avec Forward Secrecy, ce qui est un standard plus sécurisé.
     - Certificat **RSA de 4096 bits**, ce qui assure une forte protection.

   **Justification du classement :** Bien que la banque postale utilise TLS 1.2 avec une bonne longueur de clé, la présence de TLS 1.1 et certaines faiblesses de chiffrement réduisent sa sécurité, mais elle reste meilleure que BNP Paribas.

### 3. **www.societegenerale.fr**
   - **Faiblesses :**
     - Certaines suites de chiffrement considérées comme faibles, bien qu'elles soient optionnelles.
   - **Forces :**
     - Pas de TLS 1.0 ni 1.1, seulement **TLS 1.2**.
     - Certificat **RSA de 4096 bits** avec un algorithme de signature robuste.
     - Bon support des navigateurs et des clients simulés.

   **Justification du classement :** Société Générale présente une configuration de sécurité solide avec des protocoles à jour et un bon chiffrement. Quelques suites de chiffrement faibles l'empêchent d’atteindre le plus haut niveau, mais elle reste très sécurisée.

### 4. **www.credit-agricole.fr**
   - **Faiblesses :**
     - N'utilise pas encore **TLS 1.3**, mais cela n'affecte pas trop la sécurité globale.
   - **Forces :**
     - Le certificat **RSA de 4096 bits** avec **SHA256withRSA** assure une excellente sécurité.
     - Support uniquement du **TLS 1.2**, avec des suites de chiffrement qui incluent **Forward Secrecy**.
     - Aucune version obsolète de TLS activée.
     - A+ est la meilleure note possible, ce qui reflète l’excellence de la configuration.

   **Justification du classement :** Le Crédit Agricole se distingue par une configuration de sécurité quasi parfaite, avec des suites de chiffrement robustes, un certificat solide, et aucun protocole obsolète activé.

### Classement final :
1. **mabanque.bnpparibas** (le moins sécurisé)
2. **www.labanquepostale.fr**
3. **www.societegenerale.fr**
4. **www.credit-agricole.fr** (le plus sécurisé)
   

Ce classement est basé sur la gestion des certificats, des protocoles TLS, et des faiblesses dans les suites de chiffrement, avec un accent particulier sur l'exclusion des versions obsolètes de TLS qui sont très vulnérables.