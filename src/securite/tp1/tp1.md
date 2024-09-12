# TP 1 securite

## Exercice 1

En comparant les trames HTTP et HTTPS, on observe que le contenu des requêtes et réponses HTTP est visible en clair, exposant les données sensibles à une interception. En revanche, en HTTPS, après la phase de négociation TLS, tout le trafic est chiffré, rendant illisible les échanges. HTTPS assure donc la confidentialité et l'intégrité des données, tandis qu'HTTP reste vulnérable aux attaques comme l'espionnage et l'usurpation de session.

## Exercice 2

### 1.)
Le nombre de codes que peut générer Alice est `10 ^ 4` puisqu'elle a 10 choix pour chaque chiffre, et qu'il y a 4 chiffres à choisir. 

### 2.)
Si Bob s'y prend bien, c'est à dire que pour chaque chiffre du code à tester, il procède par élimination en regardant si la led est rouge ou verte, il trouvera chaque chiffre du code en, au pire, 10 essais. 
Dans ce sens, étant donné qu'il y a 4 chiffres à trouver, il fera dans le pire des cas `10 * 4` tentatives.

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

## Exercice 7

