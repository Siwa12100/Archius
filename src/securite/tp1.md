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

Les hachages de mots de passe commençant par `$1$` sont issus de l'algorithmes **MD5crypt** *([source](https://files-radiatorsoftware-com.translate.goog/radiator/ref/User-Password.html?_x_tr_sl=en&_x_tr_tl=fr&_x_tr_hl=fr&_x_tr_pto=rq))*.