
# 📊 Analyse des histogrammes par caractéristiques

---

## 🔷 1. Histogramme de `Mean Area` pour le diagnostic 'B'

- **Axe X** : Mean Area (surface moyenne de la tumeur)
- **Axe Y** : Effectif (nombre d'échantillons observés dans chaque intervalle de surface)
- **Interprétation** :
  - La majorité des tumeurs bénignes (diagnostic `B`) ont une surface moyenne comprise entre **300 et 600**.
  - On observe une distribution relativement centrée avec une forme légèrement asymétrique vers la droite.
- **Conclusion** : Les tumeurs bénignes ont une **taille moyenne modérée**, rarement très élevée.


## 🔷 5. Histogramme de `Mean Area` pour le diagnostic 'M'

- **Axe X** : Mean Area
- **Axe Y** : Effectif
- **Interprétation** :
  - Les surfaces des tumeurs malignes (`M`) sont souvent comprises entre **500 et 1200**, mais certaines atteignent plus de **2000**.
  - Distribution plus **étalée et décalée vers la droite** que pour les bénignes.
- **Conclusion** : Les tumeurs malignes sont **plus grandes** en moyenne.

---

---

## 🔷 2. Histogramme de `Mean Radius` pour le diagnostic 'M'

- **Axe X** : Mean Radius (rayon moyen de la tumeur)
- **Axe Y** : Effectif (nombre d’échantillons dans chaque intervalle de rayon)
- **Interprétation** :
  - Pour les tumeurs malignes (`M`), les rayons sont plus grands que ceux des tumeurs bénignes.
  - La distribution est étalée entre **12,5 et 27,5**, avec un maximum d’occurrences vers **15 à 20**.
- **Conclusion** : Les tumeurs malignes tendent à être **plus larges** que les bénignes.

---

## 🔷 3. Histogramme de `Mean Radius` pour le diagnostic 'B'

- **Axe X** : Mean Radius
- **Axe Y** : Effectif
- **Interprétation** :
  - Pour les tumeurs bénignes (`B`), le rayon est souvent entre **10 et 13**, avec peu d’observations au-delà de 15.
- **Conclusion** : Le rayon est un indicateur discriminant : **plus faible en cas de tumeur bénigne**.

---

### 📊 Graphique de gauche : Diagnostic 'B' (Bénin)
- **Axe X** : `Worst Concavity` (concavité la plus prononcée mesurée sur la tumeur)
- **Axe Y** : `Effectif` (nombre de tumeurs correspondant à une certaine valeur de concavité)

#### 🔍 Analyse :
- La plupart des tumeurs bénignes ont une concavité faible, majoritairement inférieure à 0.2.
- Très peu de cas bénins présentent une concavité au-delà de 0.4.
- Distribution fortement **asymétrique à droite**, concentrée sur de faibles valeurs.

#### ✅ Conclusion :
- Les tumeurs bénignes ont généralement des bords plus **réguliers et moins concaves**.

---

### 📊 Graphique de droite : Diagnostic 'M' (Malin)
- **Axe X** : `Worst Concavity`
- **Axe Y** : `Effectif`

#### 🔍 Analyse :
- La distribution est plus **étalée** que pour les cas bénins.
- Beaucoup de tumeurs malignes ont une concavité comprise entre 0.3 et 0.6, avec un pic autour de 0.4.
- Une proportion significative de cas dépasse 0.6, certains allant jusqu’à 1.2.

#### ❗ Conclusion :
- Les tumeurs malignes présentent des **formes plus irrégulières et concaves**.
- La concavité est un **critère pertinent pour distinguer** les tumeurs malignes des tumeurs bénignes.




### 🧭 Lecture du graphique

Ce graphique représente une **projection PCA (Analyse en Composantes Principales)** des individus du dataset après application du **clustering K-Means en 3 groupes** (clusters 0, 1, 2).

* Chaque **point** correspond à un individu.
* Les **coordonnées** sur les axes PC1 et PC2 sont les deux principales composantes (combinaisons des variables originales) qui résument au mieux la variance des données.
* La **couleur** du point indique son **cluster** d’appartenance.
* Les axes **PC1** et **PC2** ne correspondent pas à des variables réelles, mais à des directions qui expliquent le plus de variance dans les données.

---

### 🔍 Interprétation des clusters

On observe une **séparation nette** entre les trois groupes :

* **🟠 Cluster 1 (à gauche)** : le plus compact, avec des valeurs basses sur PC1.
  ↳ D’après les statistiques, ce groupe a :

  * Un petit **mean radius** (11.89)
  * Une petite **mean area** (442)
  * Une **faible concavité** (0.0389)
    ➤ **Hypothèse : il correspond à des tumeurs bénignes**, plus petites et moins agressives.

* **🟢 Cluster 2 (au centre)** : intermédiaire, entre les deux autres.
  ↳ Avec :

  * Un **radius moyen** (15.27)
  * Une **zone moyenne** (731)
  * Une **concavité intermédiaire** (0.1309)
    ➤ **Hypothèse : cas ambigus ou modérés**, ni totalement bénins ni franchement agressifs.

* **🔵 Cluster 0 (à droite)** : dispersé, avec les plus fortes valeurs sur PC1.
  ↳ Il a :

  * Le plus grand **mean radius** (20.49)
  * La plus grande **mean area** (1318)
  * La **concavité la plus élevée** (0.2007)
    ➤ **Hypothèse : tumeurs potentiellement malignes**, plus volumineuses et plus irrégulières.

---
