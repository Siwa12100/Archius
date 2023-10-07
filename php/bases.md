# PHP - Les bases
---

### Les balises
Le code php vient s'insérer au milieu du code html. Il s'agit donc de venir placer des bouts de php dans les parties en html, quand il s'agit des parties dynamiques d'une page. 

Les balises php ont a forme suivante : `<?php ... le code ... ?>`
Il existe aussi des balises de la forme `<? ... ?>`, `<% ... %>` et `<?= ... =?>`mais il ne vaut mieux pas les utiliser dans de manière générale. 

**Exemple :**
```html
<!DOCTYPE html>
<html>
    <head>
        <title>Ceci est une page de test <?php /* Code PHP */ ?></title>
        <meta charset="utf-8" />
    </head>
```