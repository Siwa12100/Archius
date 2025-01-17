
### Exercice 1
#### 1. Créer une Series avec le nombre d'habitants par département
import pandas as pd

habitants = pd.Series({
    'Allier': 339384,
    'Cantal': 145969,
    'Haute-Loire': 226322,
    'Puy-de-Dôme': 636383
})
print(habitants)

#### 2. Créer une Series avec la surface par département
surface = pd.Series({
    'Allier': 7340,
    'Cantal': 5726,
    'Haute-Loire': 4977,
    'Puy-de-Dôme': 7970
})
print(surface)

#### 3. Créer une DataFrame avec des informations supplémentaires
data = {
    'Nombre habitants': habitants,
    'Surface (km²)': surface,
    'Code postal': ['03', '15', '43', '63'],
    'Date de création': ['1790', '1790', '1790', '1790'],
    'Nombre arrondissements': [3, 3, 3, 5],
    'Nombre de cantons': [19, 15, 19, 31],
    'Nombre de communes': [317, 246, 257, 464]
}
df_auvergne = pd.DataFrame(data)
print(df_auvergne)

### Exercice 2
#### 1. Charger le fichier food.csv
food = pd.read_csv('food.csv', sep=';')  # Remplacez ';' par le bon séparateur

#### 2. Afficher les 6 premières lignes
print(food.head(6))

#### 3. Nombre d'observations
print(f"Nombre d'observations : {food.shape[0]}")

#### 4. Nombre de colonnes
print(f"Nombre de colonnes : {food.shape[1]}")

#### 5. Afficher les noms des colonnes
print(food.columns.tolist())

#### 6. Nom de la 105ème colonne
print(f"Nom de la 105ème colonne : {food.columns[104]}")

#### 7. Type de la 105ème colonne
print(f"Type de la 105ème colonne : {food.dtypes[104]}")

#### 8. Index du dataset
print(f"Index : {food.index}")

#### 9. Nom du produit à la 19ème ligne
print(f"Produit à la 19ème ligne : {food.iloc[18]['product_name']}")

