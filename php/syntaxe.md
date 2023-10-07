# PHP - La syntaxe

### Les variables
---
En php, il est important de garder en tête que la varibale n'existe que tant que la page est en cours de génération. 
Ensuite une fois la génération terminée, elles sont supprimées de la mémoire car ne servent plus à rien.

Les différents types de variables sont : 
* Les chaînes de caractères : `string`
* les entiers : `int`
* les nombres décimaux : `float`
* les booléens : `bool`
* Rien : `NULL`

La déclaration de variable se fait de la manière suivante : `$age = 12;`. 

Pour information en ce qui concerne les chaînes de caractères, le fonctionnement est comme le bash, `" ... "` interprète les caractères spéciaux mais pas les ` ... `. 
En plus de cela, il faut savoir que les `.` permette de concacténer des strings. 

**Exemple :** `echo "coucou"."tout le monde....";`

Pour afficher le contenu d'une variable, on utilise le `$` dans un `echo` : `echo $age`. 

Pour les nombres, on a bien les `+ ; - ; * ; / ; %` qui fonctionnent de manière classique.


### If ... else
---
Ils fonctionnent de manière totalement classique. 

**Syntaxe :**
```php
if (... != .... ){
    faire ...
}
elseif (autre condition renvoyant un bool] {
    sinon si ... faire ....
}
else {
    sinon faire....
}
```

