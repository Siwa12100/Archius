# 📂 Comprendre les Groupes sous Linux

[...retorn en rèire](../menu.md)

---

## 🔹 1. Introduction aux Groupes
Un **groupe** sous Linux est une **collection d'utilisateurs** partageant les mêmes permissions sur des fichiers/répertoires.
→ **Analogie** : Une équipe (ex: "Développeurs", "Administrateurs") avec des accès communs.

### ✅ Caractéristiques Clés
| Propriété               | Explication                                                                 |
|-------------------------|-----------------------------------------------------------------------------|
| **1 groupe par fichier** | Un fichier/répertoire a **un seul groupe propriétaire** (mais des ACL peuvent étendre cela). |
| **Utilisateurs multi-groupes** | Un utilisateur peut appartenir à **plusieurs groupes** (ex: `alice` dans `devs` et `users`). |
| **Permissions héritées** | Les droits (`rwx`) du groupe s'appliquent à **tous ses membres**. |

---

## 🔹 2. Commandes de Base pour les Groupes

| Commande               | Exemple                     | Description                                                                 |
|------------------------|-----------------------------|-----------------------------------------------------------------------------|
| Lister les groupes     | `groups` ou `groups alice`  | Affiche les groupes de l'utilisateur courant ou spécifié.                  |
| Créer un groupe        | `sudo groupadd devs`        | Crée un nouveau groupe nommé `devs`.                                       |
| Supprimer un groupe     | `sudo groupdel devs`        | Supprime le groupe `devs` (doit être vide).                                |
| Ajouter un utilisateur | `sudo usermod -aG devs alice` | Ajoute `alice` au groupe `devs` (`-a` pour "append", `G` pour groupes secondaires). |
| Changer le groupe d'un fichier | `sudo chgrp devs fichier.txt` | Définit `devs` comme groupe propriétaire du fichier.                     |
| Voir le groupe d'un fichier | `ls -l fichier.txt`       | Affiche le groupe dans la 4ème colonne (ex: `-rw-r--r-- 1 user **devs**`). |

---

## 🔹 3. Permissions et Groupes
### 🔧 Ajouter `x` (exécution) au groupe uniquement
```bash
chmod g+x fichier  # Ajoute `x` au groupe
```
- **Avant** : `-rw-r--r--` (644) → Groupe a `r--`.
- **Après** : `-rw-r-xr--` (654) → Groupe a `r-x`.

#### 📌 Équivalent en octal
```bash
chmod 020 fichier  # 0 (user) + 2 (groupe=x) + 0 (others)
```

#### ⚠️ Cas particuliers
1. **Groupe sans droits initiaux** :
   ```bash
   # Avant : -rw------- (600)
   chmod g+x fichier
   # Après : -rw--x--- (610) → Groupe a `--x`.
   ```
2. **Pour un répertoire** :
   - `g+x` permet aux membres du groupe d'**entrer** (`cd`) dans le répertoire, mais **pas de lister** son contenu (il faut aussi `r`).
   - Exemple :
     ```bash
     chmod g+x dossier/  # Autorise `cd dossier/` pour le groupe.
     ```

---

### 📊 Résumé des Commandes de Permissions
| Commande       | Effet                                  | Exemple de résultat (`ls -l`) |
|----------------|----------------------------------------|--------------------------------|
| `chmod g+x`    | Ajoute `x` au groupe.                  | `-rw-r-xr--` (654)            |
| `chmod g-x`    | Retire `x` du groupe.                  | `-rw-r--r--` (644)            |
| `chmod 020`    | Ajoute `x` au groupe (notation octale).| `-rw-r-xr--` (si initial=644) |

---

## 🔹 4. Le Groupe `sudo` : Explications
### 🔹 Qu'est-ce que le groupe `sudo` ?
- **Groupe système** spécial qui permet à ses membres d'exécuter des commandes **en tant que `root`** (administrateur) via `sudo`.
- **Fichier de configuration** : `/etc/sudoers` (à éditer avec `visudo`).

### 🔧 Comment ajouter un utilisateur au groupe `sudo` ?
```bash
sudo usermod -aG sudo username  # Remplacez `username` par le nom de l'utilisateur.
```
- **Vérification** :
  ```bash
  groups username  # Doit afficher `sudo` dans la liste.
  sudo -l          # Liste les droits sudo de l'utilisateur.
  ```

### ⚠️ Attention
- **Ne pas confondre** :
  - `sudo` (groupe) → Permet d'exécuter des commandes en `root`.
  - `root` (utilisateur) → Compte administrateur absolu.
- **Sécurité** : Limitez l'accès au groupe `sudo` aux utilisateurs de confiance.

---

## 🔹 5. `ls -la` : Décryptage Complet
La commande `ls -la` affiche **tous les fichiers** (y compris cachés) avec leurs **permissions détaillées**.

### 📌 Structure d'une ligne `ls -la`
```bash
drwxr-xr-x  2 alice devs 4096 Jun 10 10:00 mon_dossier
```
| Colonne       | Signification                                                                 |
|---------------|------------------------------------------------------------------------------|
| `d`           | Type de fichier (`d`=répertoire, `-`=fichier, `l`=lien symbolique).         |
| `rwxr-xr-x`   | Permissions (User/Group/Others).                                              |
| `2`           | Nombre de liens physiques (sous-répertoires pour un dossier).                |
| `alice`       | Propriétaire du fichier.                                                     |
| `devs`        | **Groupe propriétaire** du fichier.                                          |
| `4096`        | Taille en octets.                                                            |
| `Jun 10 10:00`| Date et heure de dernière modification.                                      |
| `mon_dossier`  | Nom du fichier/répertoire.                                                  |

### 🔍 Exemple de permissions
| Permission | Signification                                                                 |
|------------|------------------------------------------------------------------------------|
| `rwx------`| Propriétaire a `rwx`, groupe et others ont **aucun droit**.                |
| `drwxr-x---`| Répertoire : propriétaire `rwx`, groupe `r-x`, others **aucun droit**.     |
| `-rw-r--r--`| Fichier : propriétaire `rw-`, groupe `r--`, others `r--` (644).            |

---

## 🔹 6. Cas Pratique : Partager un Dossier avec un Groupe
### 📌 Objectif
Créer un dossier `/shared_project` accessible en **lecture/écriture** par le groupe `devs`.

### 🔧 Étapes
```bash
# 1. Créer le groupe (si inexistant)
sudo groupadd devs

# 2. Ajouter des utilisateurs au groupe
sudo usermod -aG devs alice
sudo usermod -aG devs bob

# 3. Créer le dossier et définir le groupe
sudo mkdir /shared_project
sudo chgrp devs /shared_project  # Définit le groupe

# 4. Donner les permissions (rw pour groupe)
sudo chmod 770 /shared_project   # rwx pour user+group, rien pour others
```

### ✅ Résultat
```bash
ls -ld /shared_project
# drwxrwx--- 2 root devs 4096 /shared_project
```
- **Accès** :
  - `alice` et `bob` (membres de `devs`) → **lecture/écriture/exécution**.
  - Autres utilisateurs → **accès refusé**.

---

## 🔹 7. Permissions Avancées : ACL (Access Control Lists)
Si un fichier doit être accessible par **plusieurs groupes**, utilisez `setfacl` :
```bash
# Donner rwx au groupe "admins" EN PLUS du groupe propriétaire
sudo setfacl -m g:admins:rwx /shared_project

# Vérifier les ACL
getfacl /shared_project
```
- **Résultat** :
  ```
  # group:devs:rwx
  # group:admins:rwx
  ```

---

## 🔹 8. Résumé et Bonnes Pratiques
### ✅ À retenir
1. **1 fichier = 1 groupe propriétaire** (mais ACL permet d'étendre cela).
2. **`chmod g+x`** → Ajoute `x` au groupe **sans modifier les autres permissions**.
3. **Groupe `sudo`** → Permet d'exécuter des commandes en `root`.
4. **`ls -la`** → Affiche **permissions + propriétaire + groupe**.

### 🔐 Bonnes pratiques de sécurité
- **Évitez `chmod 777`** → Trop permissif. Préférez `770` + groupes.
- **Limitez l'accès `sudo`** → Seuls les administrateurs doivent en faire partie.
- **Utilisez `setfacl`** pour des permissions complexes (plusieurs groupes).

---

## 🔹 9. Exercices Pratiques
1. **Créer un groupe** `testgroup` et y ajouter votre utilisateur.
2. **Créer un fichier** `test.txt` et définir son groupe comme `testgroup`.
3. **Donner au groupe** le droit d'écrire (`g+w`).
4. **Vérifier** avec `ls -l` et tester l'écriture depuis un autre utilisateur (dans le groupe vs. hors groupe).

---
### 📌 Solution
```bash
sudo groupadd testgroup
sudo usermod -aG testgroup $USER
touch test.txt
sudo chgrp testgroup test.txt
chmod g+w test.txt
ls -l test.txt  # Doit afficher : -rw-rw-r-- 1 user testgroup
```

---

[...retorn en rèire](../menu.md)