# Projet « Monde Intermédiaire » : Document de conception générale

> Réseau : [Alliance], alliance de serveurs Minecraft survie vanilla francophones
> Statut : concept validé dans ses grandes lignes, à prototyper
> Date : octobre 2026

---

## Sommaire

1. Résumé exécutif
2. Le constat de départ
3. L'existant : ce que l'Alliance sait déjà faire
4. Les contraintes non négociables
5. Ce qu'on veut éviter
6. Ce qu'on veut obtenir
7. Le concept en une phrase et la boucle de jeu
8. Les trois espaces
9. Les guildes et le cœur de guilde
10. Les points de contrôle
11. L'entretien et les garde-fous anti-corvée
12. Les claims et les bâtiments
13. La qualité des constructions (niveaux 0, 1, 2)
14. Les zones sauvages, le PvP et la restauration de la carte
15. L'Agora, cœur sacré du réseau
16. L'Influence, la compétition et les saisons
17. Le lien avec les serveurs d'origine
18. La carte : génération et placement automatique
19. L'architecture technique
20. Le temps de gestion admin : objectif quasi nul
21. Les risques et les parades
22. Les pistes de travail et les variantes possibles
23. La feuille de route
24. Les questions encore ouvertes
25. Annexe : la page de règles « joueur » en une minute

---

## 1. Résumé exécutif

L'Alliance relie déjà plusieurs serveurs survie vanilla : une Agora commune, un chat partagé, la liste des joueurs connectés et le transfert de colis entre serveurs via Myriad. Mais elle reste surtout **symbolique**. Les joueurs, eux, s'ennuient sur le long terme : une fois le stuff, la base et les usines en place, il ne reste que la maintenance.

Le projet propose un **monde intermédiaire** partagé par tout le réseau. Les **guildes** de chaque serveur s'y affrontent pour **prendre et tenir des points de contrôle**. On ne peut **rien farmer** dans ce monde : toutes les ressources viennent du serveur d'origine. Le monde intermédiaire **donne donc un objectif** au farm, aux usines et à l'organisation sur chaque serveur, au lieu de leur faire concurrence.

Il est complété par l'**Agora**, qui devient le lieu où l'on dépense ses gains, où l'on suit les classements et où l'on se retrouve sans PvP.

Le principe directeur : **un maximum d'intérêt stratégique et de fun pour un minimum de règles et un minimum de travail admin.** Tout doit être automatisé, généré ou animé par les joueurs.

---

## 2. Le constat de départ

### 2.1 La limite du gameplay vanilla sur le long terme

Le parcours type d'un joueur sur un serveur survie :

1. Il arrive, récolte, se fait un stuff.
2. Il construit sa base, petite ou grande.
3. Il monte des usines et des farms.
4. Il ouvre éventuellement une boutique au spawn.
5. **Puis plus rien** : logistique, maintenance des shops, réapprovisionnement.

**Le paradoxe** : Minecraft offre une richesse énorme (combat, potions, enchantements, redstone, usines, structures, exploration, mobs rares), mais elle sert peu une fois l'installation terminée. **Il manque des objectifs.**

### 2.2 Un multijoueur vécu en solo

Sur un serveur de 10 à 15 joueurs actifs, chacun joue seul ou à 2-3. La présence des autres ne change presque rien au jeu. À l'échelle du réseau, environ **une centaine de joueurs investis** (une trentaine en pic simultané) **ne se rencontrent presque jamais**.

### 2.3 Une alliance au potentiel inexploité

L'infrastructure technique existe, mais l'alliance se résume à :
- une Agora visitée rarement,
- un événement inter-serveurs environ toutes les six semaines,
- des réunions mensuelles longues et procédurales.

### 2.4 Des serveurs méfiants

Les administrateurs aiment l'idée d'alliance, mais ils :
- craignent de **se faire voler leurs joueurs**,
- veulent garder **leur autonomie** (technique, gameplay, économie, permissions),
- **n'ont pas le temps** de mettre en place ni d'animer des concepts communs.

Le coordinateur du réseau non plus n'a pas le temps d'animer l'Alliance au quotidien.

### 2.5 Le besoin, formulé simplement

> Créer un système, mis en place **une seule fois** puis **animé par les joueurs eux-mêmes**, qui donne des **objectifs à long terme**, fait **jouer ensemble** les joueurs d'un même serveur et du réseau, et **renforce** l'activité de chaque serveur au lieu de la vider.

---

## 3. L'existant : ce que l'Alliance sait déjà faire

| Brique | Description | Utilité pour le projet |
|---|---|---|
| **Agora** | Serveur commun : capitale centrale et une île par serveur (le « nid »), avec un portail vers chaque serveur | Devient le cœur sacré du projet |
| **Chat partagé** | Messagerie commune entre serveurs | Annonces, alertes, vie du réseau |
| **Liste globale des joueurs** | Joueurs connectés sur tout le réseau, visibles depuis l'Agora | Classements, présence |
| **Myriad** | Transfert de **colis** entre serveurs (pas des inventaires) | Le canal unique serveur d'origine → monde intermédiaire |
| **Inventaires séparés** | Chaque serveur a ses propres inventaires | Garantit l'étanchéité des économies |
| **Réunions mensuelles** | Administrateurs et modérateurs | Instance de validation, pas d'animation |
| **Compétences de développement** | Le coordinateur code, assisté par Claude Code, et peut utiliser une IA agentique pour manipuler le monde | Permet un système entièrement automatisé |

---

## 4. Les contraintes non négociables

### 4.1 Autonomie des serveurs

| Domaine | Ce que le serveur garde absolument |
|---|---|
| **Technique** | Ses plugins, sa version, son hébergement. Rien d'obligatoire à installer, sauf éventuellement un petit module optionnel |
| **Gameplay** | Ses règles, ses concepts, son économie. Le projet n'injecte **jamais** d'objets dans un serveur d'origine |
| **Administration** | Ses permissions, sa modération, ses sanctions |
| **Joueurs** | Les joueurs restent rattachés à leur serveur. Pas de migration encouragée |

### 4.2 Flux de ressources à sens unique

**Serveur d'origine → monde intermédiaire, jamais l'inverse.** Aucune ressource ne redescend, sinon on casserait les économies locales.

### 4.3 Charge admin quasi nulle

- Pas de construction manuelle de donjons, de jumps ou de bâtiments par les admins.
- Pas d'animation quotidienne.
- Pas de validation manuelle systématique.
- Tout ce qui peut être généré, automatisé ou délégué aux joueurs **doit l'être**.

### 4.4 Esprit vanilla

Pas de classes, pas de compétences magiques, pas d'objets surpuissants. Les ajouts doivent **utiliser** les mécaniques vanilla (combat, potions, farm, construction) plutôt que les remplacer.

---

## 5. Ce qu'on veut éviter

| Dérive | Pourquoi c'est un problème | Parade prévue |
|---|---|---|
| **Les joueurs quittent leur serveur pour le monde intermédiaire** | Les admins refuseront le projet | Aucun farm possible là-bas, construction limitée aux claims, tout vient du serveur d'origine |
| **Le farm comme corvée** | Lassitude, burn-out, jeu malsain | Entretien fixe et plafonné, livrer plus ne rapporte rien |
| **Les farms AFK et automatiques qui rendent tout trivial** | Si une farm à larmes de ghast est infinie, l'offrande n'a plus de valeur | Paniers variés, entretien fixe, la quantité ne rapporte pas d'avantage |
| **Les déséquilibres entre serveurs** | Certains serveurs rendent des items faciles | Paniers diversifiés, liste d'exclusion par serveur, plafond d'équipement en PvP |
| **La domination éternelle des grosses guildes** | Les nouvelles guildes abandonnent | Saisons, coût d'entretien croissant, contrats accessibles aux petites guildes |
| **Le grief et le pillage** | Destructeur, peu intéressant | Territoires non destructibles, zones sauvages restaurées automatiquement |
| **Les constructions moches posées « pour le bonus »** | Monde laid | Les niveaux de qualité récompensent le beau |
| **La complexité** | Les joueurs décrochent | Une règle centrale, 3 types de points, 1 monnaie, 4 bâtiments |
| **Le travail admin** | Le projet meurt faute de temps | Génération et validation automatiques, votes communautaires |
| **Une Agora inutile** | On construit un décor vide | L'Agora est le seul endroit où l'on dépense l'Influence |

---

## 6. Ce qu'on veut obtenir

1. **Des objectifs à long terme** qui donnent un sens au farm, aux usines, aux potions, au stuff et à la construction.
2. **Du jeu collectif** : la guilde devient l'unité de jeu, on s'organise et on recrute.
3. **Une activité renforcée sur chaque serveur** : on farme chez soi pour réussir ailleurs.
4. **Une rencontre réelle entre serveurs**, par la compétition et les zones partagées.
5. **De la stratégie** : où s'étendre, quoi construire, quoi entretenir, quand attaquer.
6. **Du fun** : donjons, embuscades, captures, records.
7. **Un beau monde** qui reste beau dans le temps.
8. **Une Agora vivante.**
9. **De l'équité** : une nouvelle guilde peut gagner une saison.

---

## 7. Le concept en une phrase et la boucle de jeu

> **Prends des points de contrôle, tiens-les le plus longtemps possible, et construis autour pour mieux les tenir.**

### La boucle

```
 SERVEUR D'ORIGINE            MONDE INTERMÉDIAIRE                AGORA
 farm, craft, stuff,    →    prendre les points (défi)    →   dépenser l'Influence
 potions, usines              tenir les points (entretien)      fondations, contrats,
                              construire dans les claims        classements
        ↑                                                            │
        └─────────── colis d'entretien via Myriad ←──────────────────┘
```

Chaque espace a un rôle et un seul :
- **Serveur d'origine** = production.
- **Monde intermédiaire** = terrain de jeu et de conquête.
- **Agora** = récompense, prestige, rencontre.

---

## 8. Les trois espaces

| Espace | Rôle | PvP | Construction | Farm |
|---|---|---|---|---|
| **Territoires de guilde** | Expansion propre à chaque guilde | Non | Uniquement dans les claims débloqués | Non |
| **Zones sauvages** | Affrontement, bastions, donjons ouverts | Oui | Temporaire, effacée à chaque restauration | Non |
| **Agora** | Centre symbolique, dépense, classements | Non | Réservée aux admins (et aux bâtiments générés) | Non |

---

## 9. Les guildes et le cœur de guilde

### 9.1 La guilde

- Rattachée à **un serveur d'origine** (toutes les guildes multi-serveurs sont une variante possible, voir §22).
- Taille minimale : 2 ou 3 joueurs, pour éviter les guildes-solo.
- Création automatique par commande, sans validation admin.

### 9.2 Le cœur de guilde : le dirigeable

- Chaque guilde reçoit un **dirigeable** amarré au-dessus de son petit territoire de départ.
- C'est son QG : point d'apparition, coffre de réception des colis Myriad, tableau de gestion des points.
- Il est **généré automatiquement** à partir d'une schématique (avec quelques variantes cosmétiques au choix).
- **V0 : fixe.** Le déplacement du dirigeable est une option future (techniquement fragile : entités, coffres, collisions).

### 9.3 Le territoire de départ

Très petit : le dirigeable, un point d'atterrissage, éventuellement un premier claim. **Tout le reste se gagne.**

---

## 10. Les points de contrôle

### 10.1 Principe

Un point de contrôle est un lieu de la carte qui :
1. se **prend** par un défi,
2. se **tient** par un entretien,
3. **rapporte de l'Influence** chaque jour tant qu'il est tenu,
4. **étend le territoire** de la guilde et **débloque des claims** autour de lui.

### 10.2 Trois types seulement

| Type | Emplacement | Se prend par | Se tient par | Contestable ? |
|---|---|---|---|---|
| **Autel** | Dans ou près des territoires | Une offrande unique (panier d'items) | Une offrande hebdomadaire | Non |
| **Donjon** | Autour et entre les territoires | Réussir le donjon | Le refaire une fois par semaine, par n'importe quel membre | Indirectement (plusieurs guildes peuvent viser le même donjon libre) |
| **Bastion** | Zones sauvages | Capture « roi de la colline » (rester X minutes dans la zone) | Ne pas se le faire reprendre | **Oui, directement** |

### 10.3 Niveaux de difficulté

Chaque point a un **niveau de I à V**. Plus il est élevé, plus il rapporte et plus il coûte à entretenir.

| Niveau | Exemple de défi | Exemple d'entretien |
|---|---|---|
| I | Donjon court, offrande de fer et de nourriture | Petit panier de ressources communes |
| II | Mini-boss, offrande travaillée (gâteaux, flèches à effet) | Panier intermédiaire |
| III | Vagues longues, potions nécessaires, offrande rare (blaze rods, perles) | Items de Nether |
| IV | Équipe de 4 ou plus, boss, offrande de haut niveau | Items de fin de jeu |
| V | Épreuve majeure, plusieurs salles, rôles complémentaires | Panier exigeant et varié |

**Aucun verrou artificiel** : c'est la difficulté qui filtre. Une petite guilde très douée peut tenter un niveau IV.

### 10.4 L'extension du territoire

- Tenir un point **ajoute sa zone d'influence** au territoire de la guilde.
- Option simple retenue : on peut prendre **n'importe quel point libre**. Les points proches de son territoire sont simplement plus faciles à tenir grâce aux bâtiments (voir §12).
- Option plus stricte (variante) : n'accepter que les points adjacents au territoire existant.

### 10.5 L'extinction

- Pas d'entretien à temps → le point **s'éteint**. Il redevient libre.
- On le reprend en refaisant le défi. C'est la seule règle à retenir.

### 10.6 La rareté, source de tension

**Il doit y avoir moins de points de qualité que de guildes qui les désirent.** Les points de haut niveau, en particulier, doivent être rares et placés entre plusieurs territoires.

---

## 11. L'entretien et les garde-fous anti-corvée

### 11.1 Le principe

Tenir est aussi important que prendre : la compétition porte sur **la capacité à maintenir le plus de points possible**. Mais l'entretien ne doit **jamais** pousser à farmer en AFK ou à jouer par obligation.

### 11.2 Les règles

1. **Entretien fixe et modeste** : un panier par point et par semaine.
2. **Livrer plus ne rapporte rien.** Pas de course à la quantité.
3. **Paniers variés et tirés au sort** dans une liste pondérée. Aucun item ne domine, donc une farm automatique d'un seul item ne suffit pas.
4. **Coût total légèrement croissant** : chaque point supplémentaire coûte un peu plus cher que le précédent. L'expansion infinie devient intenable, même pour une très grande guilde.
5. **Envoi via Myriad** depuis le serveur d'origine : le panier se prépare chez soi, avec les ressources de chez soi.
6. **Délai de grâce** : quelques jours de marge avant extinction (allongés par une tour de guet).
7. **Pas de pénalité de recrutement** : une guilde plus grande peut tenir plus, mais le coût croissant plafonne l'avantage.

### 11.3 Le cas des farms automatiques

Une farm automatique **n'est pas interdite**, elle est même normale en vanilla. Elle facilite un des items demandés, ce qui est une récompense légitime d'un travail technique. Elle ne casse rien, car :
- l'entretien est fixe (avoir 10 000 larmes ne sert à rien),
- le panier change et mélange plusieurs familles d'items,
- les donjons et bastions ne s'automatisent pas.

### 11.4 Les déséquilibres entre serveurs

- Chaque serveur peut déclarer une **liste d'items exclus** (« chez nous, cet item s'achète au spawn »).
- Les paniers piochent dans plusieurs catégories (minerais, nourriture, mobs, Nether, crafts élaborés, potions).
- On peut prévoir un **coefficient par serveur** si un déséquilibre persiste après une saison de test.

---

## 12. Les claims et les bâtiments

### 12.1 Les claims

- Ce sont des **parcelles prédéfinies** sur la carte, placées autour de chaque point de contrôle (2 ou 3 par point).
- Prendre le point **débloque** ses claims. **On ne construit que là.**
- Cela empêche les guildes de construire partout et de faire du monde intermédiaire leur base principale.
- Les claims sont **générés automatiquement** (voir §18), pas dessinés à la main.

### 12.2 Les bâtiments : utiles pour tenir

Un bâtiment ne donne pas un bonus abstrait : **il aide à tenir les points situés dans son rayon**. Le placement devient donc stratégique.

| Bâtiment | Effet sur les points à portée | Placement typique |
|---|---|---|
| **Entrepôt** | Réduit le panier d'entretien des autels | Au milieu d'un groupe d'autels |
| **Tour de guet** | Allonge le délai de grâce, alerte Discord en cas d'attaque d'un bastion | En bordure, vers les zones sauvages |
| **Forge** | Facilite les donjons proches (réparation, bonus temporaire d'équipement en donjon) | Près des donjons tenus |
| **Écurie** | Téléportation ou monture rapide vers les points à portée | Tournée vers les bastions à défendre |

### 12.3 Comment on pose un bâtiment

1. On achète une **pierre de fondation** à l'Agora (avec de l'Influence).
2. On la pose dans un claim débloqué.
3. Le bâtiment est actif immédiatement, au niveau 0.
4. Les joueurs **construisent eux-mêmes** l'édifice autour (aucune construction admin).

### 12.4 Quand un claim est perdu

Si le point s'éteint, ses claims deviennent **gelés** :
- les constructions **restent intactes**, mais le bâtiment est inactif,
- personne d'autre ne peut y construire pendant un délai (par exemple 2 semaines),
- si la guilde reprend le point, tout redevient actif,
- passé le délai, le claim est **archivé** (schématique sauvegardée, rendue à la guilde, restituable ailleurs) puis remis à l'état initial.

Ainsi, perdre un point ne détruit jamais le travail créatif d'une guilde.

---

## 13. La qualité des constructions (niveaux 0, 1, 2)

### 13.1 Le principe

Inciter à faire beau, sans transformer les admins en jury permanent.

| Niveau | Obtention | Effet |
|---|---|---|
| **0** | Automatique : la pierre est posée | Effet de base |
| **1** | **Automatique** : le plugin vérifie un minimum (volume construit, variété de blocs, présence d'un toit, pas de blocs « bruts » dominants) | Effet +25 % |
| **2** | **Vote communautaire** : la guilde soumet son bâtiment, X votes de joueurs **d'autres serveurs** le valident. L'admin n'a qu'un droit de veto | Effet +50 % et mise en avant à l'Agora |

### 13.2 Les garde-fous du vote

- On ne vote pas pour sa propre guilde, ni pour son propre serveur.
- Un votant doit avoir un minimum d'activité (éviter les comptes jetables).
- Sans vote dans un délai donné, la demande expire et peut être resoumise.
- Variante : une évaluation par IA (captures automatiques du bâtiment) comme premier filtre avant le vote.

### 13.3 Équilibrage

Les bonus restent du **confort** : un bâtiment moche fonctionne, un beau fonctionne mieux et se voit. Le beau ne doit jamais être obligatoire pour gagner.

---

## 14. Les zones sauvages, le PvP et la restauration de la carte

### 14.1 Rôle

Les bandes de terrain entre les territoires sont la zone de **conflit direct** : bastions, embuscades, escarmouches.

### 14.2 La capture des bastions

- Mode **« roi de la colline »** : rester dans la zone de capture pendant X minutes, sans adversaire dedans.
- **Fenêtres de capture** (par exemple certains soirs et le week-end) pour ne pas perdre un bastion pendant la nuit. Hors fenêtre, on peut se battre, mais pas capturer.
- Un bastion rapporte beaucoup d'Influence, mais il est **le seul point qu'on peut voler directement**.

### 14.3 Équité du combat

- **Plafond d'équipement** possible en zone sauvage (pas de netherite, ou enchantements limités) pour neutraliser les écarts entre serveurs.
- Les objets apportés ont été envoyés par Myriad : **ce qu'on perd en mourant est vraiment perdu**, ce qui donne du poids au combat et un débouché au farm.

### 14.4 La restauration automatique de la carte

1. À la génération, on **sauvegarde une copie intacte** des fichiers de région.
2. Un plugin **remet les chunks** des zones sauvages dans leur état d'origine, automatiquement (par exemple chaque semaine, à une heure creuse), ou à la demande partout ailleurs.
3. Règle simple pour les joueurs : **rien de ce qu'on construit en zone sauvage ne survit.** Fortifications temporaires autorisées, elles disparaîtront.

Remarque technique : copier les fichiers est plus fiable que régénérer depuis la seed, car une génération custom peut ne pas redonner exactement le même résultat d'une version à l'autre.

---

## 15. L'Agora, cœur sacré du réseau

### 15.1 Pourquoi l'Agora

Elle existe déjà, elle est déjà sans PvP, elle relie déjà tous les serveurs, et sa capitale centrale **manque d'utilité**. Pas besoin de construire un « cœur sacré » dans le monde intermédiaire : c'est elle.

### 15.2 Pourquoi y aller régulièrement

Règle fondamentale : **l'Agora est le seul endroit où l'on dépense son Influence.**

| Lieu | Fonction |
|---|---|
| **Le sanctuaire** | Unique au départ : lieu des offrandes spéciales, du lore, des cérémonies |
| **Le marché des fondations** | Échange d'Influence contre des pierres de fondation et des objets utilisables uniquement dans le monde intermédiaire |
| **Le tableau des contrats** | Défis hebdomadaires communs à tout le réseau (record de donjon, offrande spéciale, capture d'un bastion précis), avec un bonus d'Influence |
| **Le tableau des saisons** | Classements, records, historique des vainqueurs |
| **La galerie** | Bâtiments de niveau 2 mis en avant, avec portail pour les visiter |
| **Le portail** | Accès au monde intermédiaire |

### 15.3 Évolutions possibles

- Plusieurs temples (un par dieu ou par thème) si le lore prend.
- Les îles de chaque serveur affichent les guildes et les exploits de leur serveur.
- Variante lointaine : de petits autels relais non PvP sur les serveurs volontaires (complexe, à ne faire qu'en fin de feuille de route).

---

## 16. L'Influence, la compétition et les saisons

### 16.1 Une seule monnaie

**L'Influence** sert à la fois de score de classement et de monnaie à dépenser.

Option pour éviter le dilemme « dépenser ou rester premier » : distinguer l'**Influence gagnée** (cumulée, pour le classement) et l'**Influence disponible** (dépensable). Pour les joueurs, cela reste un seul chiffre affiché avec deux colonnes.

### 16.2 Les sources d'Influence

- Points tenus (par jour, selon leur niveau).
- Contrats de l'Agora.
- Records de donjon.
- Captures de bastions.

### 16.3 Les saisons

- Durée suggérée : **2 à 3 mois**.
- Le classement porte sur la saison : une guilde nouvelle peut gagner.
- **À la fin de saison :**
  - les points de contrôle repartent à zéro,
  - les **claims et constructions sont conservés** si possible, ou archivés (voir variantes au §22),
  - les zones sauvages sont entièrement restaurées,
  - les vainqueurs reçoivent des récompenses **cosmétiques et permanentes** (bannière, titre, monument à l'Agora).
- Un **classement historique** (toutes saisons) récompense la constance.

### 16.4 Visibilité sur les serveurs d'origine (optionnel)

Récompenses **purement cosmétiques** : titre ou tag de chat de guilde, à installer seulement par les serveurs volontaires. Aucun impact sur l'économie locale.

---

## 17. Le lien avec les serveurs d'origine

C'est ce qui rend le projet acceptable pour les admins.

| Mécanisme | Effet pour le serveur d'origine |
|---|---|
| Aucun farm dans le monde intermédiaire | Toute la production se fait chez soi |
| Entretien et équipement via Myriad | Les farms, usines et shops locaux trouvent un débouché |
| Construction limitée aux claims | La base principale reste sur le serveur d'origine |
| Guildes rattachées à un serveur | Pas de migration, fierté de serveur |
| Récompenses cosmétiques visibles chez soi | Promotion de l'activité locale |
| Nombre de membres actifs | Une guilde plus active localement peut tenir plus de points |

**Piste de travail** : lier le nombre de joueurs autorisés d'une guilde dans le monde intermédiaire à son **activité réelle** sur son serveur d'origine (temps de jeu mesuré automatiquement, par exemple). À garder simple : un indicateur automatique, jamais une évaluation manuelle.

---

## 18. La carte : génération et placement automatique

### 18.1 Le monde

- Génération **custom et belle** (Terralith, Tectonic ou équivalent), pré-générée (Chunky).
- Taille de départ : environ **3 000 × 3 000 blocs**, à ajuster selon le nombre de guildes. Une carte trop grande tue la tension.
- Bordure de monde fixe, et monde non farmable (blocs naturels non cassables hors claims, mobs sans loot de ressources, ou au minimum pas de transfert retour).

### 18.2 Le découpage en territoires

- On place **N germes** (un peu plus que le nombre de guildes attendu) avec un échantillonnage de Poisson.
- Chaque germe définit une **cellule de Voronoï** = un territoire potentiel.
- La bande entre les cellules forme les **zones sauvages**.
- Une guilde qui arrive choisit une cellule libre.

### 18.3 Le placement automatique des points et des claims

Un script analyse la carte et place :
- les autels et donjons de niveau I-II **dans** les cellules,
- les donjons de niveau III-V et les bastions **entre** les cellules, à égale distance de plusieurs territoires,
- les claims autour de chaque point, sur des terrains plats et constructibles.

Règles de placement : espacement minimum, accessibilité, équité de distance entre guildes, préférence pour les sites remarquables (montagnes, structures vanilla, rivières).

### 18.4 Les donjons sans construction manuelle

| Méthode | Description | Avantages | Limites |
|---|---|---|---|
| **Structures vanilla réutilisées** | Forteresses, avant-postes, ancient cities, trial chambers transformés en défis | Gratuit, beau, immédiat | Variété limitée |
| **Schématiques modulaires** | Salles assemblées automatiquement (couloirs, arènes, jump, boss) | Variété infinie, rejouable | Il faut un lot de salles au départ |
| **Packs communautaires** | Schématiques existantes sous licence libre | Rapide | Cohérence visuelle |
| **IA agentique** | Une IA construit ou décore des salles selon un brief | Peu de travail humain | À superviser, qualité variable |
| **Joueurs bâtisseurs** | Concours de construction de salles, validés par vote | Communauté impliquée | Dépend de la motivation |

**Recommandation** : V0 avec les structures vanilla et un petit lot de salles modulaires (Trial Chambers et Trial Spawners sont parfaits pour les vagues de mobs en 1.21+). Élargir ensuite par concours communautaires.

---

## 19. L'architecture technique

### 19.1 Composants

| Composant | Rôle | Outils possibles |
|---|---|---|
| **Serveur monde intermédiaire** | Paper, relié au réseau (Velocity) | Paper, Velocity |
| **Plugin principal « Conquête »** | Guildes, points, entretien, Influence, claims, bâtiments, saisons | Plugin Java/Kotlin maison, développé avec Claude Code |
| **Myriad** | Réception des colis d'entretien et d'équipement | Existant |
| **Protection** | Claims, zones protégées, flags PvP | WorldGuard ou intégré au plugin |
| **Restauration** | Copie des régions, remise à l'état initial | FAWE, ou module maison |
| **Donjons** | Instances, vagues, timers, scores | Plugin maison, MythicMobs éventuellement |
| **Base de données** | État des guildes, scores, historique | MySQL/MariaDB ou SQLite |
| **Bot Discord** | Alertes, votes de niveau 2, classements | Bot maison |
| **Agora** | Marché, contrats, tableaux | Même plugin (module Agora) ou plugin séparé |
| **Carte web** | Visualisation des territoires | BlueMap ou Dynmap avec marqueurs |

### 19.2 Principes de développement

- **Tout en configuration** (YAML) : coûts, délais, paniers, rayons, fenêtres de capture. Aucun équilibrage en dur.
- **Commandes admin minimales** : réinitialiser un point, mettre un veto, forcer une fin de saison.
- **Journalisation complète** pour l'équilibrage (qui tient quoi, combien de temps, ce qui est livré).
- **Modularité** : chaque brique peut être désactivée.

### 19.3 Estimation grossière du développement (avec assistance IA)

| Brique | Effort indicatif |
|---|---|
| Guildes, dirigeable fixe, territoires Voronoï | 2 à 3 semaines |
| Points (autel, donjon simple), entretien, Influence | 3 à 4 semaines |
| Claims, pierres de fondation, bâtiments | 2 à 3 semaines |
| Niveau 1 automatique, niveau 2 par vote Discord | 1 à 2 semaines |
| Génération et placement automatique | 2 à 3 semaines |
| Donjons modulaires | 3 à 5 semaines |
| Bastions, PvP, fenêtres, restauration | 2 à 3 semaines |
| Agora (marché, contrats, classements) | 2 semaines |
| Saisons, archives | 1 à 2 semaines |

Total réaliste pour une version complète : **4 à 7 mois** en temps partiel. **Une V0 jouable en 6 à 10 semaines** est atteignable.

---

## 20. Le temps de gestion admin : objectif quasi nul

| Tâche | Qui | Fréquence |
|---|---|---|
| Création et gestion des guildes | Joueurs, commandes | — |
| Placement des points et claims | Script, une fois par saison | Par saison |
| Construction des donjons | Génération modulaire, structures vanilla | Une fois |
| Entretien et extinction | Plugin | Automatique |
| Validation niveau 1 | Plugin | Automatique |
| Validation niveau 2 | Votes joueurs, veto admin | Rare |
| Restauration de la carte | Plugin | Automatique |
| Contrats hebdomadaires | Tirés au sort dans une liste | Automatique |
| Classements, annonces | Plugin et bot Discord | Automatique |
| Équilibrage | Coordinateur | Entre deux saisons |
| Modération | Modérateurs du réseau | Comme ailleurs |

**Objectif cible : moins d'une heure par semaine** de travail humain une fois le système lancé, hors développement de nouvelles fonctions.

---

## 21. Les risques et les parades

| Risque | Gravité | Parade |
|---|---|---|
| Trop de complexité pour les joueurs | Élevée | Une page de règles, introduction progressive, V0 réduite |
| Les admins refusent par peur de perdre leurs joueurs | Élevée | Rien à installer, flux à sens unique, présentation axée sur « ça fait farmer chez vous » |
| Masse critique insuffisante | Élevée | Carte petite, saison de test, événements de lancement |
| Équilibrage raté (entretien trop dur ou trop facile) | Moyenne | Tout en config, logs, saison 1 déclarée « bêta » |
| Domination d'un serveur ou d'une guilde | Moyenne | Saisons, coût croissant, plafond d'équipement, exclusions d'items |
| PvP toxique | Moyenne | Fenêtres de capture, PvP limité aux zones sauvages, modération |
| Guildes mortes qui figent des territoires | Moyenne | Libération automatique après une saison d'inactivité, archives |
| Votes de niveau 2 sans participation | Faible | Expiration, resoumission, filtre IA en option |
| Charge de développement trop lourde | Moyenne | Feuille de route par versions, chaque version jouable seule |
| Bugs de restauration ou de dirigeable | Faible à moyenne | Dirigeable fixe en V0, sauvegardes, tests |
| Fatigue du coordinateur | Élevée | Automatisation maximale, documentation, déléguer l'équilibrage |

---

## 22. Les pistes de travail et les variantes possibles

### 22.1 Sur les guildes
- **A.** Guildes mono-serveur (recommandé pour la V0 : fierté de serveur, acceptabilité admin).
- **B.** Guildes multi-serveurs autorisées (plus de rencontres, mais plus de risque de « vol » de joueurs).
- **C.** Alliances de guildes pour les bastions (partage de l'Influence d'un bastion, pactes de non-agression affichés).

### 22.2 Sur l'expansion
- **A.** Prise libre de n'importe quel point (simple).
- **B.** Prise adjacente uniquement (expansion visible, plus stratégique, plus de règles).

### 22.3 Sur la fin de saison
- **A.** Tout est conservé, seuls les points s'éteignent.
- **B.** Les constructions sont archivées et restituables dans la saison suivante.
- **C.** Nouvelle carte à chaque saison (frais, mais beaucoup de pertes créatives).

### 22.4 Sur les offrandes
- **A.** Paniers tirés au sort dans une liste pondérée (simple, imprévisible).
- **B.** Dieux avec chacun leurs goûts (forge, mer, nature, Nether, ténèbres, ciel) : plus de lore et de prévisibilité stratégique. Peut s'ajouter plus tard sans changer les règles.

### 22.5 Sur le PvP
- **A.** PvP libre en zone sauvage avec équipement plafonné.
- **B.** PvP uniquement pendant les fenêtres de capture.

### 22.6 Sur l'usure entre guildes
Puisqu'on ne vole pas frontalement un autel ou un donjon, une guilde peut en **épuiser une autre** indirectement :
- prendre ses bastions, ce qui la prive d'Influence et la force à dépenser pour défendre,
- remporter les contrats de l'Agora avant elle,
- battre ses records de donjon (bonus de record transféré),
- piste plus agressive : un **défi de contestation** sur un donjon tenu (la guilde attaquante doit faire un meilleur temps que la tenante pendant une période donnée). À tester avec prudence.

### 22.7 Sur les événements d'alliance
- Ouverture temporaire d'un **donjon légendaire** ou d'un bastion spécial.
- Événements tirés au sort automatiquement (« Éclipse » : entretien doublé, Influence doublée pendant un week-end).
- Fin de saison avec une grande bataille de bastions.

### 22.8 Sur la mesure d'activité locale
- Temps de jeu sur le serveur d'origine (mesure automatique via le réseau).
- Nombre de colis envoyés.
- À éviter : toute évaluation manuelle (shops, roleplay), trop coûteuse et subjective.

### 22.9 Sur l'IA
- Construction ou décoration de salles de donjon.
- Pré-évaluation des bâtiments avant vote.
- Génération de lore (noms de points, chroniques de saison).
- Toujours en assistance, jamais sans possibilité de contrôle humain.

---

## 23. La feuille de route

| Version | Contenu | Question testée | Durée indicative |
|---|---|---|---|
| **V0 – Prototype** | Petite carte, quelques territoires, dirigeable fixe, autels et un donjon modulaire, entretien via Myriad, Influence, claims simples | Les guildes ont-elles envie de prendre et de tenir des points ? | 6 à 10 semaines |
| **V1 – Le cœur** | Bâtiments et pierres de fondation, niveaux 0/1/2, Agora (marché, contrats, classements), saisons | Est-ce que ça relance l'activité sur les serveurs d'origine ? | +2 à 3 mois |
| **V2 – Le conflit** | Zones sauvages, bastions, PvP, fenêtres, restauration automatique, archives | La tension entre guildes est-elle saine ? | +1 à 2 mois |
| **V3 – L'épaisseur** | Dieux et lore, dirigeable mobile, événements avancés, alliances de guildes, autels relais sur les serveurs volontaires | — | Selon envie |

**Étape zéro, avant tout code :** présenter le concept en réunion d'alliance sous forme d'une page (voir annexe), avec un engagement minimal demandé aux serveurs : *« Vous n'avez rien à installer ni à animer. Vos joueurs auront juste une raison de plus de farmer chez vous. »*

---

## 24. Les questions encore ouvertes

1. Combien de guildes réalistes dès la première saison ? (détermine la taille de la carte et le nombre de points)
2. Guildes mono-serveur ou multi-serveurs ?
3. PvP libre ou seulement pendant les fenêtres de capture ?
4. Paniers aléatoires ou dieux dès la V1 ?
5. L'Agora peut-elle être modifiée pour accueillir le marché, le sanctuaire et les tableaux ? Y a-t-il de la place ?
6. Plafond d'équipement en zone sauvage : lequel ?
7. Quelle durée de saison ?
8. Que deviennent les constructions en fin de saison ?
9. Faut-il lier la taille d'une guilde dans le monde intermédiaire à son activité locale, et comment le mesurer simplement ?
10. Qui, parmi les admins ou les joueurs, peut prendre en charge l'équilibrage entre deux saisons ?

---

## 25. Annexe : la page de règles « joueur » en une minute

> **Le Monde Intermédiaire**
>
> 1. Fondez une **guilde** avec des joueurs de votre serveur. Vous recevez un **dirigeable** et un petit territoire.
> 2. Prenez des **points de contrôle** : **autels** (offrande), **donjons** (réussir l'épreuve), **bastions** (capturer en PvP).
> 3. Chaque point tenu rapporte de l'**Influence** chaque jour et agrandit votre territoire.
> 4. Pour **tenir** un point, envoyez chaque semaine son offrande depuis votre serveur (via Myriad) ou refaites son donjon. Sinon, il s'éteint.
> 5. Les points ouvrent des **claims** : construisez-y des **bâtiments** (entrepôt, tour, forge, écurie) qui aident à tenir les points voisins. Plus ils sont beaux, plus ils sont efficaces.
> 6. Dépensez votre Influence à l'**Agora** : fondations, contrats, classements.
> 7. Rien ne se farme ici : **tout vient de votre serveur**.
> 8. Dans les **zones sauvages**, tout est permis, et tout y est effacé chaque semaine.
> 9. Chaque **saison**, le classement repart à zéro. Tout le monde peut gagner.
