# Guide Complet des Bases de Python

[...retour menu sur python](./menu.md)

## 1. Les Bases du Langage Python

### Syntaxe de Base
Python est un langage de programmation interprété et orienté vers la lisibilité du code. Il utilise l'indentation pour structurer le code. Contrairement à d'autres langages qui utilisent des accolades `{}`, Python se base sur une structure hiérarchique par **indentation**.

### Variables
En Python, une variable est simplement un espace mémoire qui stocke des données. Il n'est pas nécessaire de spécifier le type de la variable car Python est un langage à typage dynamique.

#### Exemple :
```python
x = 10   # x est un entier
name = "Alice"  # name est une chaîne de caractères
pi = 3.14  # pi est un nombre à virgule flottante
```

#### Changement de type :
```python
x = 5
x = "Hello"  # Maintenant, x est une chaîne de caractères
```

### Types de Données
Voici les principaux types de données en Python :
- **Int** : Entier
- **Float** : Nombre décimal
- **Str** : Chaîne de caractères
- **Bool** : Booléen (True/False)
- **List** : Liste modifiable
- **Tuple** : Liste immuable
- **Dict** : Dictionnaire (paires clé-valeur)

#### Exemple :
```python
age = 30  # int
temperature = 22.5  # float
nom = "Bob"  # string
est_vrai = True  # bool
liste = [1, 2, 3, 4]  # liste
personne = {"nom": "Alice", "âge": 25}  # dictionnaire
```

### Indentation
En Python, l'indentation est essentielle pour délimiter les blocs de code.

```python
if x > 0:
    print("x est positif")
else:
    print("x est négatif ou nul")
```

## 2. Opérateurs

### Opérateurs Arithmétiques
- **+** : Addition
- **-** : Soustraction
- **\*** : Multiplication
- **/** : Division
- **%** : Modulo
- **//** : Division entière
- **\*\*** : Exponentiation

#### Exemple :
```python
a = 10
b = 3
print(a + b)  # 13
print(a // b)  # 3
print(a ** b)  # 1000 (10^3)
```

### Opérateurs Logiques
- **and** : Vrai si les deux opérandes sont vrais.
- **or** : Vrai si au moins un opérande est vrai.
- **not** : Inverse la valeur.

```python
x = True
y = False
print(x and y)  # False
```

### Opérateurs de Comparaison
- **==** : Egalité
- **!=** : Différent
- **>**, **<**, **>=**, **<=**

## 3. Structures de Contrôle

### Conditionnelles : `if`, `else`, `elif`
Les conditions permettent d'exécuter des blocs de code en fonction de certaines conditions.

#### Exemple :
```python
x = 15
if x > 10:
    print("x est supérieur à 10")
elif x == 10:
    print("x est égal à 10")
else:
    print("x est inférieur à 10")
```

### Boucles : `for`, `while`

#### Boucle `for`
Utilisée pour parcourir des séquences (listes, chaînes, etc.).
```python
for i in range(5):
    print(i)
```

#### Boucle `while`
Répète une action tant qu'une condition est vraie.
```python
compteur = 0
while compteur < 5:
    print(compteur)
    compteur += 1
```

## 4. Gestion des Erreurs

Les blocs `try` et `except` permettent de capturer des erreurs sans interrompre le programme.

```python
try:
    x = 10 / 0
except ZeroDivisionError:
    print("Erreur : division par zéro.")
finally:
    print("Ce bloc s'exécute toujours.")
```

## 5. Les Structures de Données

### Listes
Modifiables et ordonnées, elles permettent de stocker plusieurs types d'éléments.
```python
ma_liste = [1, 2, 3, "bonjour"]
```

### Tuples
Immuables et ordonnés, ils sont utilisés pour stocker des ensembles fixes.
```python
mon_tuple = (1, 2, 3)
```

### Ensembles (Sets)
Non ordonnés, ils ne contiennent pas de doublons.
```python
mon_ensemble = {1, 2, 3, "bonjour"}
```

### Dictionnaires
Stockent des paires clé-valeur.
```python
mon_dictionnaire = {"nom": "Alice", "age": 25}
```

## 6. Fonctions

### Définition et Utilisation de Fonctions
On définit une fonction avec `def`, et on peut lui passer des paramètres.
```python
def saluer(nom):
    print(f"Bonjour {nom} !")
```

### Paramètres par Défaut
```python
def saluer(nom="Inconnu"):
    print(f"Bonjour {nom} !")
```

### Portée des Variables
Les variables définies dans une fonction sont **locales** et ne sont pas accessibles en dehors de celle-ci.

```python
def ma_fonction():
    x = 10  # locale
```

Pour modifier une variable globale :
```python
x = 50
def modifier_x():
    global x
    x = 100
```

## 7. List Comprehensions

Syntaxe concise pour créer des listes.
```python
carres = [x**2 for x in range(10)]
```
