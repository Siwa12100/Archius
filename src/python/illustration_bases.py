# 1. Les Bases du Langage Python

# Définition de variables avec différents types de données
x = 10   # int (entier)
name = "Alice"  # str (chaîne de caractères)
pi = 3.14  # float (nombre décimal)
is_happy = True  # bool (valeur booléenne)

# Changement de type
x = "Hello"  # Maintenant, x est une chaîne de caractères

# 2. Opérateurs

a = 10
b = 3

# Opérateurs arithmétiques
addition = a + b  # 13
division_entière = a // b  # 3
puissance = a ** b  # 1000 (10^3)

# Opérateurs logiques
x = True
y = False
logique_et = x and y  # False
logique_ou = x or y  # True
logique_non = not x  # False

# Opérateurs de comparaison
comparaison_egal = a == b  # False
comparaison_sup = a > b  # True

# 3. Structures de Contrôle

# Conditionnelle : if, elif, else
age = 20
a_permis = True
if age >= 18 and a_permis:
    print("Vous pouvez conduire.")
elif age >= 18 and not a_permis:
    print("Vous êtes majeur, mais vous devez obtenir un permis.")
else:
    print("Vous êtes mineur.")

# Boucle for
for i in range(3):
    print(f"Tour de boucle {i}")

# Boucle while
compteur = 0
while compteur < 3:
    print(f"Compteur : {compteur}")
    compteur += 1

# 4. Gestion des Erreurs

try:
    division = 10 / 0  # Ceci va générer une erreur
except ZeroDivisionError:
    print("Erreur : division par zéro.")
finally:
    print("Ceci est toujours exécuté.")

# 5. Les Structures de Données

# Liste : modifiable, indexée
fruits = ["pomme", "banane", "cerise"]
fruits.append("mangue")  # Ajout d'un élément
print(fruits[0])  # Accès au premier élément

# Tuple : immuable
couleurs = ("rouge", "vert", "bleu")
print(couleurs[1])  # Accès à un élément du tuple

# Ensemble : non ordonné, pas de doublons
mon_ensemble = {1, 2, 3, 3}  # Pas de doublons, 3 n'apparaîtra qu'une seule fois
mon_ensemble.add(4)  # Ajout d'un élément

# Dictionnaire : paires clé-valeur
personne = {"nom": "Alice", "âge": 25}
print(personne["nom"])  # Accès à la valeur associée à la clé "nom"
personne["ville"] = "Paris"  # Ajout d'une nouvelle clé-valeur

# 6. Fonctions

# Définition d'une fonction avec un paramètre et une valeur de retour
def saluer(nom="Inconnu"):
    return f"Bonjour {nom} !"

message = saluer("Alice")  # Appel de la fonction
print(message)

# Utilisation d'une variable globale
compteur_global = 0  # Variable globale

def incrementer_compteur():
    global compteur_global  # Accès à la variable globale
    compteur_global += 1

incrementer_compteur()
print(f"Compteur global après incrémentation : {compteur_global}")

# 7. List Comprehensions

# Créer une liste des carrés des nombres pairs de 0 à 9
carres_pairs = [x**2 for x in range(10) if x % 2 == 0]
print(carres_pairs)  # Affiche [0, 4, 16, 36, 64]
