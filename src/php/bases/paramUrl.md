# Passage de paramètres via l'URL & méthode GET

[...retour au sommaire](../intro.md)

[...retour à la page précédente](./blocs.md)

Une URL (Uniform Resource Locator) représente l'adresse d'une page web aux yeux d'un navigateur, elle commence généralement par http (ou https...). 

Il est ainsi possible, par le biais d'URL, de passer des informations par le biais de paramètre à la page visée par une URL, et c'est ce qui est expliqué ici. 

### Envoyer des paramètres dans une URL

Il est possible, dans une URL, de mettre des paramètres, qui sont des informations que la page appelée dans l'URL recevra. 
Dans une URL, ces paramètres sont notés sous la forme `nom=valeur` et sont séparés par un `&` entre eux. 

**Voilà un exemple :**
```
https://www.monsite.com/page1.php?nom=dupont&prenom=Louis
```
On regarde que juste avant de commencer à noter les paramètres, on met un `?`.
De cette manière, on peut écrire autant de paramètres que l'on souhaite, tant que l'on respecte la taille max d'une URL (en général 256 caractères...). 

Pour créer un lien avec des paramètres, on peut donc créer des URL en HTML qui contiennent des paramètres, par exemple : 
```html
<a href="bonjour.php?nom=dupont&amp;prenom=Jean"> Clique ici ! </a> 
```
Il est important que les `&` qui servent à séparer les paramètres dans une URL doivent être dans le cas présent notés `&amp;` dans le code.
Ce code permet donc créer créer un lien, qui sera affiché comme "Clique ici" pour l'utilisateur, et qui amènera donc vers la page bonjour.php, en lui passant les paramètres spécifiés. 


### Faire circuler des informations avec des formulaires et HTTP GET
Au lieu de directement inscrire les paramètres en dur dans le code, il est aussi possible de les faire à l'aide d'un formulaire, avec la méthode HTTP GET. 

Pour commencer, voilà un rappel du fonctionnement des formulaires en html & css (essentiel de maîtriser cela pour la suite) : [Les formulaires en html & css](../../htmlCss/fichiers/formulaires.md). 




---
à terminer de noter : https://openclassrooms.com/fr/courses/918836-concevez-votre-site-web-avec-php-et-mysql/912799-ecoutez-la-requete-de-vos-utilisateurs-grace-aux-url

