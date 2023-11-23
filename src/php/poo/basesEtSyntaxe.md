# Bases et syntaxe 

[...retour au sommaire](../intro.md)

---

Pour information, je ne vais pas revenir dans ces notes sur le concept même de programmation orientée objet. 
Le but est uniquement d'expliquer la manière de l'appliquer au PHP, dans sa syntaxe et ses concepts. 

### Instanciation et référence

En PHP, l'instanciation est très similaire au Java. On passe par un `new`, qui fait l'appel au constructeur de la classe. 

**Exemple :**
```php
$a = new maClasse();
```

Il possible de passer aux fonctions directement des références à des variables (comme en C++ par exemple).
Le but est que le paramètre passé par référence à une fonction ne soit pas une copie de la vraie variable, mais soit la variable elle même. 
Du coup si elle est modifiée pendant la fonction, elle gardera ses modifications même après son passage. 

**Exemple :**
```php
function add($var) {
    $var = $var + 1;
}

$val = 7;
add(&val); 

echo $val; // va renvoyer 7 + 1 -> 8  
```

Ce qui est très important de comprendre, car ça peut être aussi utile que dangereux, c'est que dans le cas des instances de classe, elles sont toujours passées par référence à des fonctions. 

Pour information, `==` entre deux objets va simplement s'assurer qu'ils possèdent les mêmes propriétés, avec les mêmes valeurs. Alors qu'avec un `===`, on vérifie en plus qu'ils soient de la même classe. 

Que ce soit pour accéder aux attributs ou appeler les méthodes d'une instance de classe, on fait toujours `$instance -> monAttribut` ou `$instance -> maMethode(...)`. 

Toujours comme en Java, on peut avoir plusieurs références vers une même instance. Et donc toutes ces références doivent être supprimées pour que PHP libère la mémoire de l'instance. 

**Exemple :**
```php

$var1 = new maClasse();
$var2 = $var1;
```
Dans ce cas, si je modifie var2, var1 aussi sera modifié car ils pointent vers la même instance. 

# Création de classes

En php, on déclare une classe en faisant `class nomCLasse { ... } `. 

### Déclaration de propriétés

On déclare une propriété en spécifiant sa visibilité, de manière facultative son type, puis son nom.

**Exemple :**
```php

public string $texte;
protected $age;
```

Les trois types de protection sont :

* `public` : tout le monde a accès aux attributs.
* `private` : seul la classe a accès à ses attributs.
* `protected` : seul la classe et ses filles ont accès aux attributs de la classe. 

### Déclaration des méthodes et type nullable

Voilà la syntaxe générale pour déclarer des méthodes : 
```php

public function fct1(?string $texte) {...}

public function fct2() : ?maClasse{...}

protected function fct(int $val) : array{...}

// -- Ailleurs dans le code ... --
maClass -> fct1(NULL); 
maClass -> fct1("coucou"); 

$obj = $maClass -> fct2(); // Peut retourner null

$tab = $maClass -> fct3(3); // prend forcément un param et renvoie forcément un tableau...
```

En fait le `?` permet de dire que le paramètre ou la valeur de retour peut être nulle.

### Constructeur et destructeur

Le constructeur a la syntaxe suivante : 
```php
public function __construct ($arg1, $arg2) {
    ...
}
```

Le destructeur de son côté a la syntaxe suivante :
```php
public function __destruct() {

}

```

Le descructeur est facultatif et ne peux pas prendre de paramètres. Le constructeur de son côté est aussi facultatif même si très recommandé, et peut prendre des paramètres, ou pas. 

### La fonction unset

Je suis tombé sur cette fonction et je la trouve plutôt utile. Elle permet de détruire manuellement une instance en gros.
C'est une sorte d'équivalent du `free()` en C un peu...

**Exemple :**
```php

$var = new maClasse();
unset($var);
```
Cela marche techniquement aussi pour les types primitifs, dans le sens où ça va supprimer la valeur contenue dans la variable, mais ça n'a pas vraiment d'utilité...

Après, à la différence d'un `free()` en C, dans le cas du unset la mémoire n'est pas immédiatement libérée et ça reste au Garbage Collector de supprimer la mémoire derrière. 
Donc à part dans des situations spécifiques peut être, il ne sert pas vraiment à gérer proprement la mémoire plus que ça...

### Héritage et interfaces

Pour hériter d'une classe, c'est le classique : 
```php
class maClass extends classMere {...}
```

En ce qui concerne les interfaces, voilà la syntaxe : 
```php

interface monInterface {
    public function ....;
    ...
}
class maClass implements monInterface {...}
```

Il est possible de spécifier une visibilité à des méthodes d'interface, même si c'est facultatif. 
Il faut aussi noter que l'on peut avoir une visibilité plus restreinte lors de l'implémentation dans une classe d'une méthode, par rapport à sa déclaration dans l'interface.

C'est à dire qu'une méthode public dans une interface peut être implémentée en protected par exemple dans une classe. 
Mais une méthode private dans une interface elle, ne peut pas être protected dans une classe, car la visibilité est moins restreinte sinon...

Pour déclare une classe abstraite en php, il faut simplement utiliser le mot `abstract`. 

**Exemple :**
```php
abstract class maClasse {...}
```

### $this, $self et static

En PHP, le `static` ressemble énormément à son utilisation en Java. Il peut être mis au niveau d'une méthode ou d'un attribut. Grâce à lui, ces méthodes ou attributs n'appartiendront plus à chaque instance de la classe, mais à la classe en général. 

Pour y accéder, on utilise ensuite la syntaxe `maClass::attributOuMethode...`. 

**Exemple :**
```php
class maClass {
    ...
    ...
    static public $age;

    ...
    static public function getAge() : int {...}
}

maClass::$age = ....;
$val = maClass::fctStatique();
```

En ce qui concerne le `$this`, il permet, au sein de la déclaration d'une classe, de faire référence à une méthode ou un attribut d'une instance. 

Donc chaque instance possède ses propres valeurs d'attributs, et on peut vraiment indiquer que l'on parle de l'attribut d'une instance grâce au `$this`.

De son côté, le `$self` fait référence aux méthodes ou attributs statiques au sein d'une classe. C'est un peu comme le `maClass::...` en dehors de la déclaration d'une classe. C'est une sorte de `$this`, mais réservé aux éléments statiques (méthodes ou attributs).

Attention, on fait `$this -> ...` pour accéder à un élément, mais avec self c'est des `::`, du style `self::....`. 

Et pour finir, le `parent::` permet d'indiquer, au sein de la déclaration d'une classe que l'on fait référence à un attribut ou une méthode de la classe parente (à conditions que ceux-ci soient en protected évidemment...). 


### mot clé : final

Sur une classe, il permet d'indiquer que la classe ne peut plus posséder de classes filles (= ne peut plus être étendue).

**Exemple :**
```php
final class maClass{...}
```

Dans le cadre d'une méthode, cela permet de dire que la méthode ne peut pas être redéfinie par une classe fille.

**Exemple :**
```php
final public function getAge() : int {...}
```

Et dans le cadre d'un attribut, cela indique qu'une fois une valeur attribuée à un attribut, elle ne peut pas être modifiée. 

**Exemple :**
```php
final public string $texte = "coucou !";
```

### mot clé : global

Il n'est pas lié à la POO, mais c'est intéressant de revenir dessus rapidement. 

Il permet d'utiliser une variable en dehors du contexte dans lequel elle a été déclarée.

C'est à dire que si je défini une variable puis en dessous une fonction, je pourrais accéder à la variable du dessus au sein de la fonction directement, grâce au mot clé `global`. 

**Exemple :**
```php

$var = 4;

function getVal() {
    global $var;
    echo $var;
}

getVal();
// même si $var n'est pas passé en param de la fonction, 
// getVal() affichera la valeur de $var -> 4.
```

### var_dump(...)

En gros, c'est une sorte d'echo, mais spécialisé pour afficher à la fois le contenu d'une variable et des infos supplémentaire pour débugguer...


### constantes 

On peut déclarer un attribut const pour qu'il ne puisse pas être modifié. Par convention, on le nomme en majuscule.

**Exemple :**
```php

class maclass {

    ...
    ...
    private const VALEUR = 8;
    ...
    ...
}
```

Les constantes sont toujours statiques. 
Si on ne précise pas de visibilité, les constantes sont publiques par défaut. 






