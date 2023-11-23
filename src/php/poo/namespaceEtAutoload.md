# Espaces de nom et autoload

[...retour au sommaire](../intro.md)

---

Dans l'idée, le concept est très très semblable aux packages en Java. 

Dans ce sens par convention, on considère que tous les fichiers dans un dossier ont le même namespace. On s'assure aussi que les classes et les fichiers qui les contiennent ont le même nom, sinon ça peut être le bordel après avec certaines fonctionnalités qui utilisent les conventions, comme les autoloaders. 

De manière générale, les namespace servent avant tout à éviter les mélanges entre des classes de même nom. Dans de grands projets, plusieurs classes peuvent avoir le même nom et grâce au fait qu'elles sont présentes dans des namespace différents, on ne les confond pas. 
C'est un peu le même principe que les fichiers : deux fichiers dans le même dossier ne peuvent pas avoir le même nom, et de manière générale, on les identifie grâce à leur chemin... 

### Déclarer et utiliser un namespace

On déclare tout simplement un namespace avec le mot `namespace`. 

**Exemple :**
```php
namespace nomDuNamespace;
```

Pour pouvoir utiliser une classe dans un namespace différent que le namespace courant, on utilise le mot `use` et on lui fourni le chemin jusqu'à la classe. 

**Exemple :**
```php

use \PremierDomaine\PremierSousDomaine\SecondeSousDomaine\NomDeMaClasse as nouveauNom;
```

Dans l'idée, on considère que la structure des fichiers et dossiers est la même que celle précisée par le namespace. 

Cela nous permet ausis de voir qu'il est possible de renommer une classe grâce au `as nouveauNom`. Evidemment, cela reste facultatif.

Si on ne souhaite pas utiliser `use` mais que l'on souhaite quand même faire référence à une classe présente dans un autre espace de nom, on peut spécifier avant le nom de la classe en question l'arborescence de namespace manuellement. 

**Exemple :**
```php

$var = new \PremierDomaine\SousDomaine\maClasse();
```

Attention, dans le cas où le projet ne possède pas de système d'autoload, le simple fait de spécifier des namespace avec `use` ne permet d'utiliser une classe. 
Il faut aussi penser à faire un `require_once("chemin/maClasse.php);`. 


### l'autoloader

L'autoloader va permettre d'enlever l'utilisation de `require`. En fait, simplement en précisant le namespace d'une classe avec `use`, l'autoloader va être en mesure d'intégrer les fichiers automatiquement. 
C'est là que l'on comprend pourquoi il est très utile d'avoir l'arborescence de fichiers/dossiers identique à celle des namespace...

En php, on utilise `spl_autoload_register` pour faire l'autoload (à part si on utilise composer...). 

La fonction spl_autoload_register prend en paramètre une fonction, qui elle prend en paramètre un nom de classe, et explique dans son code comment à partir de ce nom comment aller charger la classe. 

Prenons le cas où l'arborescence fichiers/dossiers correspond parfaitement à celle des namespace. 
Dans ce cas, la fonction de notre autoload va en fait, pour chaque namespace spécifié dans un `use`, aller remplacer les \ par des / et rajouter .php à la fin. Cela va permettre de transformer le chemin en namespace en chemin vers le fichier contenant la classe. 

**Exemple :**
```php
spl_autoload_register(static function ($cheminNamespace) {

    $cheminFichier = str_replace("\\", "/", $cheminNamespace);
    require_once $cheminFichier;
});

use ...;
use ...;

// autre syntaxe : 
function mafonctionChargement($cheminNamespace) {
    ...
    ...
} 

spl_autoload_register('mafontionChargement');
...
...
....
```

Dans l'idéal, le mieux est vraiment de mettre ce bout de code dans le fichier de config, histoire qu'il soit appelé au tout début du programme, et donc opérationnel dans l'ensemble du projet. 