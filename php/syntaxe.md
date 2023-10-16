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


### If ... else et switch 
Ils fonctionnent de manière totalement classique. C'est absolument la même chose qu'en C ou en java...

**Syntaxe du if ... else :**
```php
if (... != ....){
    faire ...
}
elseif (autre condition renvoyant un bool) {
    sinon si ... faire ....
}
else {
    sinon faire....
}
```

**Syntaxe du switch :**
```php
switch($variable){

    case 1 : ....
    break;

    case 2 : ...
    break;

    default : ...
}
```


### Les ternaires
C'est une manière peut fréquente mais très compacte de gérer les conditions. 

**Syntaxe :**
```php
$userAge = 24;
$isAdult = ($userAge >= 18) ? true : false;
```

Ce code permet de mettre true si la condition est validée, sinon faut.

```php
$isAdult = ($userAge >= 18);
```
Cette version là est encore plus contacte, on met dans la variable directement la valeur renvoyée par le test. 


### Les tableaux
Le tableau se déclare avec des `[]`. 

**Exemple :** `$tab = ['coucou', 23, 'voiture]`;

On y accède avec le `$tableau[indice]`. 

**Exemple :** `echo $tab[2]`;

Il est aussi possible de créer des tableaux 2D en insérant un tableau dans un autre tableau. 

**Exemple :**
```php
    $tab = [2, 1, 4];
    $tab2D = [23, 'coucou', $tab];
```

### La boucle while
Là encore, c'est absolument pareil qu'en C ou en Java. 

**Syntaxe :**
```php
$lines = 0;
while ($lines <= 100) {
    // on fait des actions 
    $lines++; // pour incrémenter
}
```

### La boucle for
Toujours très similaire au C ou au Java. 

**Syntaxe :**
```php
for ($lines = 0; $lines <= 70; $lines++){
    // faire les actions que l'on souhaite
}
```



