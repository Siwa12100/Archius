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

