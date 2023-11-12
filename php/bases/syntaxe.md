# PHP - La syntaxe
[...retour au sommaire](../intro.md)
[...retour à la page précédente](./bases.md)

### Les variables
En php, il est important de garder en tête que la variable n'existe que tant que la page est en cours de génération. 
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
    $tab = [2, 1, 4,];
    $tab2D = [23, 'coucou', $tab,];
```
Attention à bien mettre la virgule, même après le dernier élément ! 

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

## Compléments sur les tableaux
Voici deux autres manières de déclarer des tableaux numérotés (c'est à dire qu'on associe une clé (un entier) à sa valeur, ce sont les tableaux classiques des autres langages...). 

**Avec le mot clé array :**
```php
$tab = array('coucou', 'camion', 21);
```
Dans ce cas, on utilise des `()` et plus des `[]`. 

**En déclarant les valeurs manuellement :**
```php
$tab[0] = 'coucou';
$tab[1] = 'camion...';
```

### Les tableaux associatifs
Le principe est le même que les tableaux numérotés, mais les clefs ne sont pas des entiers mais des chaînes de caractères. 

**Exemple :**
```php
$article = [
    'titre' => 'Elendil',
    'texte' => 'Bienvenue dans l\'Alliance !'
    'id' => 1728,
    'visible' => true,
];

$article['auteur'] = 'Siwa'; // pour rajouter des valeurs

echo $article['auteur'] // Pour afficher l'une de ses cases
```

### La boucle foreach
Là encore, toujours semblable à du java...

**Exemple :**
```php
$tab1 = ['coucous', 'aligot',];
$tab2 = ['voiture', 'moto',]; 
$tab3 = [$tab1, $tab2,];

foreach($tab3 as &ligne){
    echo $ligne[0]; // pour afficher le premier élément de chaque sous tableau...
}
```

Mais il est aussi possible de récupérer la clé des valeurs d'un tableau à la place des valeurs directement, à l'aide là encore d'un `foreach`. 

**Exemple :**
```php
$recette = [
    'titre' = 'titre de la recette',
    'instruction1' = 'faire cuire....',
    'auteur' = 'Siwa',
];

foreach ($recette as $clef => $valeur){
    echo "$clef : $valeur <br>"; 
}

// ---- Renvoie l'affichage suivant : ------
/* titre : titre de la recette 
   instruction1 : .....
   .... 
*/
```

### Utilisation de print_r
Il s'agit d'une sorte de echo, mais spécialisé pour les tableaux et utilisé pour débugger.
Il est très simple à utiliser, mais son seul défaut est de ne pas renvoyer de code html. Il ne renvoie donc pas de `<br>` pour retourner à la ligne, et il faut donc le prendre en compte. 
Pour se faire, on utilise les balises `<pre>` avant et après le `print_r`. 

**Exemple :**
```php
// toujours avec notre tableau article ou autre...
echo '<pre>';
print_r($article);
echo '<pre>';
```

### Recherche dans un tableau 

**1.) Vérifier qu'une clé existe dans un tableau :**

On a un tableau, et le but est donc de savoir si une clé précise existe dedans. 

Pour cela, on utilise la fonction : `array_key_exists`. On lui donne le nom de la clé à chercher, le nom du tableau, et elle renvoie un booléen en fonction de la présence de la clé ou non :
```php
$tab = [
    'v1' => 'voiture',
    'cle2' => 2345,
];

if (array_key_exists('cle2', $tab)) {
    echo " --> La clé existe dans le tableau ! ";
}
``` 

**2.) Vérifier si une valeur existe dans un tableau :**

Dans la même idée, on va utiliser la fonction `in_array`, qui renvoie elle aussi un booléen en fonction de la présence de la valeur ou non : 
```php
$tab = [
    'v1' => 'voiture',
    'cle2' => 2345,
];

if (in_array('voiture', $tab)) {
    echo " --> La valeur existe dans le tableau ! ";
}
```

**3.) Récupérer la clé d'une valeur dans un tableau**

Maintenant, à partir d'une valeur, on va récupérer sa clé dans un tableau avec la fonction `array_search`. 
Si la clé existe, alors elle est renvoyée, sinon un false est renvoyé : 

```php
$tab = [
    'v1' => 'voiture',
    'cle2' => 2345,
];

$cle = array_search('voiture', $tab);
```
[3.) les fonctions en php (suite des notes...)](./fonctions.md)