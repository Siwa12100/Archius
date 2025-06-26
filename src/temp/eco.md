
## 🔍 Lecture et analyse graphique par graphique

---

### 📊 1. **Worst Concavity — Diagnostic 'M'**

![Worst Concavity M](sandbox:/mnt/data/1.png)

**📌 Variable** : *Worst Concavity* = Concavité maximale des contours de la tumeur
**🟣 Diagnostic** : *M* (Malin)

**🧠 Lecture** :

* Distribution asymétrique à droite (positive skew)
* Pic autour de `0.35–0.4`
* Des cas extrêmes jusqu’à `1.2`, donc très concaves

**📈 Conclusion** :
Les tumeurs malignes présentent **une forte concavité des bords**, ce qui signifie que leur forme est plus **irrégulière**, un indicateur clinique important.

---

### 📊 2. **Worst Concavity — Diagnostic 'B'**

![Worst Concavity B](sandbox:/mnt/data/2.png)

**📌 Variable** : *Worst Concavity*
**🟢 Diagnostic** : *B* (Bénin)

**🧠 Lecture** :

* Distribution très concentrée entre `0.0` et `0.3`
* Pic massif autour de `0.1–0.15`
* Très peu de cas au-dessus de `0.5`

**📈 Conclusion** :
Les tumeurs bénignes ont **des contours beaucoup plus réguliers**, ce qui rend cette variable **fortement discriminante**.

---

### 📊 3. **Mean Area — Diagnostic 'M'**

![Mean Area M](sandbox:/mnt/data/3.png)

**📌 Variable** : *Mean Area* = Aire moyenne des cellules
**🟣 Diagnostic** : *M*

**🧠 Lecture** :

* Distribution large, centrée autour de `800–1000`
* Des cas très étendus jusqu’à `2500`
* Plusieurs sous-groupes visibles (multimodal)

**📈 Conclusion** :
Les tumeurs malignes tendent à être **beaucoup plus grandes en moyenne**, avec une variabilité importante.

---

### 📊 4. **Mean Area — Diagnostic 'B'**

![Mean Area B](sandbox:/mnt/data/4.png)

**📌 Variable** : *Mean Area*
**🟢 Diagnostic** : *B*

**🧠 Lecture** :

* Distribution centrée autour de `400–500`
* Plus resserrée que pour M
* Très peu de cas au-delà de `1000`

**📈 Conclusion** :
Les tumeurs bénignes ont **une aire moyenne plus petite et plus homogène**, ce qui confirme **une bonne capacité discriminante** de cette variable.

---

### 📊 5. **Mean Radius — Diagnostic 'M'**

![Mean Radius M](sandbox:/mnt/data/5.png)

**📌 Variable** : *Mean Radius* = Rayon moyen de la cellule
**🟣 Diagnostic** : *M*

**🧠 Lecture** :

* Majorité des cas entre `14` et `22`
* Cas extrêmes jusqu’à `27`
* Asymétrie faible, distribution plutôt étalée

**📈 Conclusion** :
Les tumeurs malignes tendent à avoir **un rayon plus grand**, ce qui reflète leur volume important.

---

### 📊 6. **Mean Radius — Diagnostic 'B'**

![Mean Radius B](sandbox:/mnt/data/6.png)

**📌 Variable** : *Mean Radius*
**🟢 Diagnostic** : *B*

**🧠 Lecture** :

* Pic net autour de `12`
* Très peu de cas au-dessus de `15`
* Distribution plus resserrée
  

**📈 Conclusion** :
Le rayon moyen des tumeurs bénignes est **nettement plus faible et concentré**, une différence encore une fois marquée.

---

## ✅ Synthèse des variables discriminantes

| Variable            | Différence notable entre B et M ? | Commentaire                          |
| ------------------- | --------------------------------- | ------------------------------------ |
| **Worst Concavity** | ✅ Très forte                      | Contours très irréguliers chez les M |
| **Mean Area**       | ✅ Forte                           | Tumeurs M plus grandes               |
| **Mean Radius**     | ✅ Claire                          | Les M sont visiblement plus larges   |


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
