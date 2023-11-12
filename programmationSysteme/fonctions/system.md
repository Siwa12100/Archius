# La fonction system
Cette fonction permet **d'exécuter une commande du système d'exploitation**, en lui donnant le nom de la commande en paramètre. 

Si la commande n'est pas inscrite dans la variable d'env. PATH, il est nécessaire de préciser le chemin complet vers la commande. 

La fonction renvoie 0 ou 1 en fonction de la réussite ou non de la commande spécifiée. 

```c
#include <stdlib.h>
int system(const char * commande)
```