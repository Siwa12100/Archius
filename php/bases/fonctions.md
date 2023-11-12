# Les fonctions en PHP

[...retour au sommaire](../intro.md)

[...retour à la page précédente](syntaxe.md)

## Syntaxe

La syntaxe des fonctions en php est un peu différence des langages c/java... classiques, mais le principe reste le même. Voilà la syntaxe de base : 
```php
function nomFonction(type $nomParam, ...., ...) : typeDeRetour {
     ... le code ... 
     
     return $variableDuBonType;
}
```

En ce qui concerne l'appel de la fonction, là par contre ça reste très classique : 
```php
$valeurRetour = maFonction($variableParam, 12, 'param string...');
```

## Fonctions de manipulation de texte & date 
### Calculer longueur d'une chaîne de caractères
Pour calculer la taille d'une chaîne de caractères, on utilise la fonction `strlen`. 
On lui passe un string, et elle renvoie sa taille. 

**Exemple :**
```php
$texte : string = 'coucou';
$taille : int = strlen($texte); // renvoie 6 = taille de 'coucou'
```

### Remplacer du texte par un autre
Pour remplacer une partie de chaîne de caractères par une autre, on utilise la fonction `str_replace`. On passe à la fonction la chaîne à remplacer, ce part quoi la remplacer, et le string dans lequel effectuer l'action : 

**Exemple :**
```php
$texte = 'j'aime les pommes';
str_replace('les pommes', 'la confiture', $texte);
echo $texte; // va renvoyer : 'j'aime la confiture'.
```

### Formater une chaîne de caractères
Si on veut fusionner dans un format bien précis des chaînes de caractères, cette fonction est ultra utile. 

**Voilà comment l'utiliser :**
```php
$tab = [
    'nom' => 'Dupont', 
    'prenom' => 'Jules', 
    'age' => '19',
];

echo sprintf('Il s\'appelle %s %s et a %s ans.', 
                $tab['nom'], $tab['prenom'], $tab['age']);
//  Va afficher : 'Il s'appelle Dupont Jules et a 19 ans. 
```

### Récupération de la date
La fonction date va nous permettre de récupérer la date. Enfin pour être précis, elle va récupérer par défaut la date connue par le serveur où est généré le php (donc à voir au besoin pour ajuster les fuseaux horaires...).

**Voici comment l'utiliser :**
```php
$jour = date('d');
$mois = date('m');
$annee = date('Y'); // en majuscule...
$heure = date('H'); // en majuscule 
$minutes = date('i'); 
```
---
