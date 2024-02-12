Votre texte est une introduction bien formulée à la virtualisation légère avec Docker, mettant en avant les différences entre les machines virtuelles (VM) et les conteneurs. Voici quelques suggestions pour améliorer et clarifier certains points :

---

# Introduction

[...retour au sommaire Docker](../sommaire.md)

---

Une machine virtuelle représente une forme de virtualisation lourde, réservant des ressources substantielles de la machine physique pour recréer un environnement complet, y compris un nouveau système d'exploitation. Cette approche est pertinente lorsque l'on souhaite :

* Isoler totalement la VM de l'hôte.
* Résoudre des ressources dédiées exclusivement à la VM.
* Installer différents systèmes d'exploitation que celui de l'hôte.

En revanche, le conteneur adopte une approche légère, se contentant d'isoler des processus. Il réutilise le même système d'exploitation et évite la virtualisation des ressources. Il peut ainsi être configuré pour utiliser jusqu'à une certaine quantité de ressources (RAM, CPU), mais ne les réserve pas, laissant ainsi les ressources non utilisées disponibles pour la machine hôte.

## Les conteneurs Docker

Selon la vision de Docker, un conteneur doit exécuter un seul processus. Ainsi, si une application nécessite un serveur Apache, une base de données MySQL et PHP, il y aura un conteneur distinct pour chacun de ces trois processus. Cette approche résout les problèmes potentiels de compatibilité entre les environnements.

### Stateless & Stateful

* **Stateless :** Cela signifie que l'application ne conserve pas d'état entre les requêtes. À chaque nouvelle requête, les mêmes actions sont réalisées, ce qui rend le comportement prévisible et cohérent.

* **Stateful :** En revanche, dans un contexte stateful, le processus se souvient de l'état entre les requêtes. Par exemple, si une base de données est éteinte et rallumée, elle retrouvera son état antérieur. Elle n'est pas réinitialisée à chaque redémarrage.
  
---

[...retour au sommaire](../sommaire.md)