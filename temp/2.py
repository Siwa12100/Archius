import pandas as pd
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LogisticRegression
from sklearn.metrics import accuracy_score, confusion_matrix

# Exercice 1
# 1. Charger et explorer le dataset
file_path = './titanic/train.csv'  # Remplacez par le chemin correct

df = pd.read_csv(file_path)
print("Aperçu du dataset :")
print(df.head())
print(df.info())

# 2. Vérifier les valeurs manquantes
print("Valeurs manquantes par colonne :")
print(df.isnull().sum())

# 3. Proposer une solution pour remplacer les valeurs manquantes
df['Age'].fillna(df['Age'].median(), inplace=True)
df['Embarked'].fillna(df['Embarked'].mode()[0], inplace=True)
print("Valeurs manquantes après remplacement :")
print(df.isnull().sum())

# 4. Visualiser les données (exemple avec Matplotlib)
import matplotlib.pyplot as plt
plt.figure(figsize=(8, 6))
plt.hist(df['Age'], bins=30, alpha=0.7, label='Âge')
plt.xlabel('Âge')
plt.ylabel('Nombre de passagers')
plt.title('Distribution de l\'âge des passagers')
plt.legend()
plt.show()

# 5. Créer une colonne "Sexe" (Homme = 0, Femme = 1)
df['Sex_numeric'] = df['Sex'].map({'male': 0, 'female': 1})

# 6. Combien de personnes ont survécu ?
survivants = df['Survived'].sum()
print(f"Nombre de survivants : {survivants}")

# 7. Importance de Pclass et de l'âge pour la survie
# Classification avec âge uniquement
X_age = df[['Age']]
y = df['Survived']
X_train_age, X_test_age, y_train, y_test = train_test_split(X_age, y, test_size=0.2, random_state=42)

model_age = LogisticRegression()
model_age.fit(X_train_age, y_train)

y_pred_age = model_age.predict(X_test_age)
age_accuracy = accuracy_score(y_test, y_pred_age)
print(f"Précision avec l'âge uniquement : {age_accuracy}")

# Classification avec Pclass uniquement
X_pclass = df[['Pclass']]
X_train_pclass, X_test_pclass, y_train, y_test = train_test_split(X_pclass, y, test_size=0.2, random_state=42)

model_pclass = LogisticRegression()
model_pclass.fit(X_train_pclass, y_train)

y_pred_pclass = model_pclass.predict(X_test_pclass)
pclass_accuracy = accuracy_score(y_test, y_pred_pclass)
print(f"Précision avec Pclass uniquement : {pclass_accuracy}")

# Exercice 2
# 1. Transformer le sexe en nombre (déjà fait plus haut)

# 2. Tester l'importance de l'âge et du sexe
X_age_sex = df[['Age', 'Sex_numeric']]
X_train_age_sex, X_test_age_sex, y_train, y_test = train_test_split(X_age_sex, y, test_size=0.2, random_state=42)

model_age_sex = LogisticRegression()
model_age_sex.fit(X_train_age_sex, y_train)

y_pred_age_sex = model_age_sex.predict(X_test_age_sex)
age_sex_accuracy = accuracy_score(y_test, y_pred_age_sex)
print(f"Précision avec âge et sexe : {age_sex_accuracy}")

# 3. Tester différentes méthodes de classification
from sklearn.tree import DecisionTreeClassifier
from sklearn.neighbors import KNeighborsClassifier

# Arbre de décision
model_tree = DecisionTreeClassifier(max_depth=5, random_state=42)
model_tree.fit(X_train_age_sex, y_train)

y_pred_tree = model_tree.predict(X_test_age_sex)
tree_accuracy = accuracy_score(y_test, y_pred_tree)
print(f"Précision avec l'arbre de décision : {tree_accuracy}")

# KNN
model_knn = KNeighborsClassifier(n_neighbors=5)
model_knn.fit(X_train_age_sex, y_train)

y_pred_knn = model_knn.predict(X_test_age_sex)
knn_accuracy = accuracy_score(y_test, y_pred_knn)
print(f"Précision avec KNN : {knn_accuracy}")

# 4. Transformation d'autres colonnes
# Transformer Embarked en numérique
embarked_mapping = {'C': 0, 'Q': 1, 'S': 2}
df['Embarked_numeric'] = df['Embarked'].map(embarked_mapping)

# Tester l'importance des colonnes transformées
X_embarked = df[['Embarked_numeric', 'Pclass', 'Sex_numeric']]
X_train_embarked, X_test_embarked, y_train, y_test = train_test_split(X_embarked, y, test_size=0.2, random_state=42)

model_embarked = LogisticRegression()
model_embarked.fit(X_train_embarked, y_train)

y_pred_embarked = model_embarked.predict(X_test_embarked)
embarked_accuracy = accuracy_score(y_test, y_pred_embarked)
print(f"Précision avec Embarked, Pclass et Sexe : {embarked_accuracy}")

# Conclusion
print("\nConclusions :")
print(f"- Précision avec âge uniquement : {age_accuracy}")
print(f"- Précision avec Pclass uniquement : {pclass_accuracy}")
print(f"- Précision avec âge et sexe : {age_sex_accuracy}")
print(f"- Précision avec l'arbre de décision : {tree_accuracy}")
print(f"- Précision avec KNN : {knn_accuracy}")
print(f"- Précision avec Embarked, Pclass et Sexe : {embarked_accuracy}")
