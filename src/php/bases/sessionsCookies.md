# Conserver les données avec les sessions et les cookies

[...retour au sommaire](../intro.md)

[...retour à la page précédente](./connexion.md)

---

Dorénavant, il va être question de garder des informations sur un utilisateur, même si la page est rechargée par exemple. C'est ce que l'on appelle la persistance. 

## Les sessions

Dans la logique des sessions, on a un utilisateur qui arrive, on lui fait une session, et php génère un numéro de session unique. 

Ensuite, on aura encore une nouvelle variable, un tableau nommé `$_SESSION` qui contiendra toujours les informations que l'on souhaite conserver sur l'utilisateur en train d'utiliser le site. 

Pour commencer une session, on a tout simplement à appeler la fonction `session_start()`, sans aucun paramètres. C'est pas sorcier. 

Et ensuite, soit à la déconnexion de l'utilisateur en utilisant la fonction réciproque `session_destroy()` ou bien si l'utilisateur ferme son navigateur pendant une certaine période (et dans ce cas là, c'est même pas la peine d'appeler session_destroy, ça se fait automatiquement), la session sera supprimée.

L'utilité est par exemple de retenir l'identifiant ou d'autres données dans le genre de l'utilisateur au fils de pages, et de ne pas lui demander de les ressaisir à chaque fois, une fois qu'il s'est connecté...

Dans les sites de vente en ligne, il est par exemple possible de retenir le panier en cours de remplissage de la personne en le stockant dans le tableau `$_SESSION`... 


## Les cookies 

Le soucis des sessions, c'est qu'une fois qu'elles sont détruites, tous les informations sont supprimées, pour toujours...

On utilise donc des cookies, qui sont de petits fichiers qui s'enregistrent au niveau du navigateur de l'utilisateur, pour conserver des données sur un utilisateur, même après que sa session soit détruite. 

Pour créer un cookie sur un utilisateur, on a la fonction `set_cookie()` qui prend plusieurs paramètres, dont le nom de la variable contenant l'info, et le contenu de l'info. Mais ce n'est pas tout. 

**Voilà un exemple classique et sécurisé de la fonction :**
```php
...
...
code php...
setcookie(
    'couleurPrefere', 
    'vert', 
    [
        'expires' => time() + (nbDeSecondes...),
        // toujours à mettre ces 2 choses, ultra important pour la sécurité apparemment j'ai vu : 
        'secure' => true,
        'httponly' => true,
    ]
);
```

Le expires permet d'indiquer le temps avant que le cookie ne soit supprimé. Il s'agit du nombre de secondes après jsp plus quand en 1970...
Donc on utilise time() pour avoir le nombre de seconde actuelles depuis jsp quand en 1970, et on y rajoute le nombre de secondes après lesquels le cookies sera supprimé (on peut mettre des durées en années même si on veut, mais exprimées en secondes...). 

A l'arrivée de l'utilisateur sur le site, php va aller récupérer les potentiels cookies et les mettre dans le tableau `$_COOKIE`. 
Dans l'exemple ci dessus, on pourrait donc par la suite récupérer la valeur vert en appelant `$_COOKIE['couleurPrefere']`...

Pour modifier un cookie déjà existant, on fait comme quand on le créé, avec le même nom de variable, et ça va le remplacer...

--- 