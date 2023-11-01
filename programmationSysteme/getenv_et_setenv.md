# Les fonctions pour variables d'environnement

Les fonctions **setenv** et **getenv** en C sont utilisées pour manipuler les variables d'environnement du système.

 Les variables d'environnement sont des valeurs stockées par le système d'exploitation qui peuvent être utilisées par les programmes en cours d'exécution pour obtenir des informations sur la configuration du système ou pour personnaliser le comportement des programmes.


 ##  setenv
 La fonction setenv est utilisée pour définir ou **mettre à jour une variable d'environnement.**

Elle prend trois arguments : 
* le **nom** de la variable d'environnement
* la **valeur** à lui attribuer
*  un **drapeau** optionnel.

```c
#include <stdlib.h>

int setenv(const char *name, const char *value, int overwrite);
```

* **name :** le nom de la variable d'env. que l'on souhaite définir. 

* **value :** la valeur à attribuer à la variable. 

* **overwrite :** Si la valeur n'est pas nulle (souvent = 1), alors même si la variable existe déjà, sa valeur sera remplacée. 
Mais si la valeur est nulle (= 0), alors la var. d'env. ne sera mise à jour que si elle n'existe pas. 

## getenv
