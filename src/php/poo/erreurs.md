# Gestion des erreurs

[...retour au sommaire](../intro.md)

---

Le principe de la gestion des erreurs, c'est de lancer des exceptions, puis de les récupérer à d'autres moments du script pour les traiter. 
Le but au final, c'est d'éviter de faire juste planter le programme, et de lui permettre d'être plus autonome en gros.

### Lancer une exception

Pour lancer une exeption à un moment dans le code, on utilise le mot clé `throw`, suivi d'une instanciation d'un objet venant de la classe Exception ou de l'une de ses filles. 

**Exemple :**
```php
...
...
if (c'est la merde...) {
    throw new Exception('ptn c le bordel'); 
}
```

### Gestion et capture des exceptions

Si on met un throw dans un bout de code classique, alors le programme va s'arrêter totalement. 
C'est pour cela qu'on utilise les blocs `try {} catch() finally {}`. 

On fait du code dans le try qui peut lancer des exceptions. Si aucune n'est levée, tout se passe bien, on ne passe pas dans le catch, mais on passe (toujours) par le finally. 

Par contre si l'une des exceptions levée correspond à l'une de celles spécifiées dans les paramètres du catch, alors on passe par le bloc catch, puis on fini par le finally.

Et pour info, si une exception est levée dans le try, tout le code du try après la levée de l'exception n'est pas exécuté. 

**Exemple :**
```php
try {
    ...
    ...
    if (...) {
        throw new Exception('sos');
    }
    ...
    ...
} catch (Exception $e) {
    ...
    ...
    // on gère l'exception...
    ...
} finally {
    // dans tous les cas on passe par ici...
}
```

### Créer des exceptions personnalisées

Il est possible de créer nos propres exceptions, en créer des classes qui héritent de la class Exception. 

**Exemple :**
```php
class bordel extends Exception {
    public $message = "ptn c le bordel et tout et tout !";
    ...
    ...
}

//Ailleurs dans le code du coup on peut faire : 

throw new bordel();

// puis un :
... } catch (bordel $b) { ...}
```

Après, des exceptions un peu plus... explicites disons sont déjà créées et peuvent être utilisées, comme la `RuntimeException`.


### Gestion rapide

Si on veut gérer rapidement une exception dans un catch, on peut vite fait faire un : 
```php

catch (RuntimeException $r) { 

print_r($r->getTrace());
// ou un : 
echo $r->getMessage();
}
```

Evidemment, c'est vraiment pas optimal, c'est le strict minimum...