# Introduction

[...retour au sommaire docker](../sommaire.md)

---

Une machine virtuelle est une virtualisation lourde dans le sens où elle réserve des ressources de la machine réelle pour son fonctionnement et recréer un système complet, y compris avec un nouvel OS.

C'est intéressant si l'on souhaite :

* isoler totalement la vm de l'hôte
* reserver totalement des ressources
* installer différents OS que celui de l'hôte

Le conteneur lui ne fait qu'une isolation des processus. Il réutilise le même OS et ne virtualise pas de ressources.

Il est possible d'attribuer par exemple 16go de ram à un conteneur, mais s'il ne les utilise pas, il ne les réserve pas donc ils restent libres pour la machine hôte.

## Les conteneurs docker

Dans la vision de docker, un conteneur ne doit faire tourner qu'un seul processus. Dans ce sens, si une application a par exemple besoin d'un serveur Apache, d'une bdd MySql et de PHP, il y aura un conteneur pour chacun des 3 processus.

Cela résoud ainsi les soucis de comptabilité entre les machines.

### Stateless & Stateful

* **Stateless :** Cela signifie que l'application ne stocke pas d'état. COmme une requête http, à chaque nouvelle requête, les mêmes actions seront réalisées.

* **Statefull :** A l'inverse dans une requête stateful, le processus se souvient de l'état. Si on éteint et rallume une bdd, elle se retrouvera dans le même état qu'avant d'être éteinte, elle n'est pas réinitialisée...
  
---

[...retour au sommaire](../sommaire.md)