# 🔐 Gestion des Droits d'Accès sous Linux

[...retorn en rèire](../menu.md)


---

## 📌 **Table des Matières**
- [🔐 Gestion des Droits d'Accès sous Linux](#-gestion-des-droits-daccès-sous-linux)
  - [📌 **Table des Matières**](#-table-des-matières)
  - [📜 **Introduction aux permissions**](#-introduction-aux-permissions)
  - [🔧 **Types de permissions**](#-types-de-permissions)
  - [👥 **Catégories d'utilisateurs**](#-catégories-dutilisateurs)
  - [📊 **Représentation des permissions**](#-représentation-des-permissions)
    - [1️⃣ **Format symbolique** (ex: `rwxr-xr--`)](#1️⃣-format-symbolique-ex-rwxr-xr--)
    - [2️⃣ **Format octal** (ex: `754`)](#2️⃣-format-octal-ex-754)
  - [🛠️ **Commandes pour gérer les permissions**](#️-commandes-pour-gérer-les-permissions)
    - [1. **Changer les permissions** : `chmod`](#1-changer-les-permissions--chmod)
      - [✅ **Format symbolique** (ajout/retrait de droits)](#-format-symbolique-ajoutretrait-de-droits)
      - [✅ **Format octal** (méthode recommandée pour les scripts)](#-format-octal-méthode-recommandée-pour-les-scripts)
    - [2. **Changer le propriétaire** : `chown`](#2-changer-le-propriétaire--chown)
    - [3. **Changer le groupe** : `chgrp`](#3-changer-le-groupe--chgrp)
  - [✨ **Permissions spéciales**](#-permissions-spéciales)
  - [🔄 **Permissions par défaut (`umask`)**](#-permissions-par-défaut-umask)
    - [📌 **Valeurs par défaut**](#-valeurs-par-défaut)
    - [✅ **Modifier `umask`**](#-modifier-umask)
  - [📂 **Exemples concrets**](#-exemples-concrets)
  - [💡 **Bonnes pratiques**](#-bonnes-pratiques)
    - [✅ **Pour les fichiers** :](#-pour-les-fichiers-)
    - [✅ **Pour les répertoires** :](#-pour-les-répertoires-)
    - [❌ **À éviter** :](#-à-éviter-)
  - [📝 **Résumé visuel**](#-résumé-visuel)

---

## 📜 **Introduction aux permissions**
Sous Linux, les **droits d'accès** (ou *permissions*) contrôlent **qui peut lire, modifier ou exécuter** un fichier/répertoire.
→ **Objectif** : Sécuriser le système en limitant les accès non autorisés.

---

## 🔧 **Types de permissions**
Trois permissions de base, applicables aux **fichiers** et **répertoires** :

| Permission | Fichier (File)                     | Répertoire (Directory)                     | Symbole | Valeur octale |
|------------|------------------------------------|--------------------------------------------|---------|---------------|
| **Lecture** (Read) | Lire le contenu (`cat`, `less`) | Lister le contenu (`ls`)                   | `r`     | `4`           |
| **Écriture** (Write) | Modifier le fichier (`vim`, `echo`) | **Créer/supprimer** des fichiers à l'intérieur | `w`     | `2`           |
| **Exécution** (Execute) | Exécuter le fichier (scripts, binaires) | **Accéder** au répertoire (`cd`)          | `x`     | `1`           |

> ⚠️ **Attention** :
> - Sans `x` sur un répertoire, impossible d'y accéder (`cd` échouera).
> - Sans `r` sur un répertoire, impossible de lister son contenu (`ls` affichera "Permission denied").

---

## 👥 **Catégories d'utilisateurs**
Les permissions s'appliquent à **3 groupes** :
1. **Propriétaire (User/u)** : Utilisateur qui possède le fichier.
2. **Groupe (Group/g)** : Membres du groupe propriétaire.
3. **Autres (Others/o)** : Tous les autres utilisateurs.

**Exemple** (affichage via `ls -l`) :
```bash
-rwxr-xr-- 1 alice dev 4096 Jan 1 10:00 fichier.txt
```
- `-rwxr-xr--` : Permissions (détaillées plus bas).
- `alice` : Propriétaire (user).
- `dev` : Groupe (group).

---

## 📊 **Représentation des permissions**
### 1️⃣ **Format symbolique** (ex: `rwxr-xr--`)
Structure en **4 parties** :
```
- rwx r-x r--
│ │ │ │ │ │ │
│ │ │ │ │ │ └─ Autres (o) : Lecture seule (`r--`)
│ │ │ │ │ └─── Groupe (g) : Lecture + Exécution (`r-x`)
│ │ │ │ └───── Propriétaire (u) : Lecture + Écriture + Exécution (`rwx`)
│ └─────────── Type de fichier (`-` = fichier, `d` = répertoire)
```

### 2️⃣ **Format octal** (ex: `754`)
Chaque catégorie est représentée par un **nombre** (somme des valeurs `r=4`, `w=2`, `x=1`) :

| Permission | Calcul               | Valeur octale |
|------------|----------------------|---------------|
| `rwx`      | 4 (r) + 2 (w) + 1 (x) | `7`           |
| `r-x`      | 4 + 0 + 1            | `5`           |
| `r--`      | 4 + 0 + 0            | `4`           |

**Exemple** :
- `754` = `rwxr-xr--` (propriétaire: `rwx`, groupe: `r-x`, autres: `r--`).

---

## 🛠️ **Commandes pour gérer les permissions**
### 1. **Changer les permissions** : `chmod`
#### ✅ **Format symbolique** (ajout/retrait de droits)
```bash
chmod u+x fichier.txt   # Ajoute l'exécution pour le propriétaire
chmod g-w fichier.txt   # Retire l'écriture pour le groupe
chmod o=r fichier.txt   # Donne seulement la lecture aux autres
chmod a+r fichier.txt   # Ajoute la lecture à tous (u+g+o)
```

#### ✅ **Format octal** (méthode recommandée pour les scripts)
```bash
chmod 755 fichier.txt   # rwxr-xr-x
chmod 640 fichier.txt   # rw-r-----
```

### 2. **Changer le propriétaire** : `chown`
```bash
chown nouveau_proprietaire fichier.txt
chown nouveau_proprietaire:nouveau_groupe fichier.txt  # Change aussi le groupe
```

### 3. **Changer le groupe** : `chgrp`
```bash
chgrp nouveau_groupe fichier.txt
```

---

## ✨ **Permissions spéciales**
| Permission | Symbole | Valeur octale | Description                                                                 | Exemple                     |
|------------|---------|---------------|-----------------------------------------------------------------------------|-----------------------------|
| **SETUID** | `s` (dans `u`) | `4`           | Exécute le fichier avec les droits du **propriétaire** (ex: `passwd`).    | `chmod 4755 fichier` → `-rwsr-xr-x` |
| **SETGID** | `s` (dans `g`) | `2`           | Exécute avec les droits du **groupe** ou force l'héritage du groupe.       | `chmod 2755 repertoire` → `drwxrwsr-x` |
| **Sticky Bit** | `t` (dans `o`) | `1`         | Dans un répertoire, seul le **propriétaire** peut supprimer ses fichiers. | `chmod 1777 /tmp` → `drwxrwxrwt` |

> 💡 **Cas d'usage** :
> - **SETUID** : Fichiers comme `/usr/bin/passwd` (permet aux users de modifier leur mot de passe).
> - **SETGID** : Répertoires partagés (ex: `/shared/` où tous les nouveaux fichiers héritent du groupe).
> - **Sticky Bit** : Répertoires comme `/tmp` (évite la suppression de fichiers par d'autres users).

---

## 🔄 **Permissions par défaut (`umask`)**
La commande `umask` définit les permissions **par défaut** pour les nouveaux fichiers/répertoires.

### 📌 **Valeurs par défaut**
| Type       | Permissions initiales | `umask` classique (022) | Résultat final       |
|------------|----------------------|-------------------------|-----------------------|
| **Fichier** | `666` (rw-rw-rw-)     | `022`                   | `644` (rw-r--r--)     |
| **Répertoire** | `777` (rwxrwxrwx) | `022`                   | `755` (rwxr-xr-x)     |

### ✅ **Modifier `umask`**
```bash
umask 002   # Donne plus de droits au groupe (fichiers: 664, répertoires: 775)
umask 027   # Restrictif pour les "others" (fichiers: 640, répertoires: 750)
```

> ⚠️ **Attention** :
> - `umask` est souvent définie dans `/etc/profile` ou `~/.bashrc`.
> - Utilisez `umask -S` pour afficher la valeur en format symbolique.

---

## 📂 **Exemples concrets**
| Cas d'usage                     | Commande                          | Résultat (symbolique) | Valeur octale |
|----------------------------------|-----------------------------------|-----------------------|---------------|
| Rendre un script exécutable par tous | `chmod a+x script.sh`             | `-rwxr-xr-x`          | `755`         |
| Répertoire accessible en lecture seulement | `chmod o+r dossier/`          | `drwxr-xr-x`          | `755`         |
| Fichier privé (propriétaire seul) | `chmod 700 fichier_secret.txt`     | `-rwx------`          | `700`         |
| Répertoire partagé avec SETGID   | `chmod 2775 /shared/`            | `drwxrwsr-x`          | `2775`        |
| Protéger `/tmp` avec Sticky Bit  | `chmod 1777 /tmp`                | `drwxrwxrwt`          | `1777`        |

---

## 💡 **Bonnes pratiques**
### ✅ **Pour les fichiers** :
- **`644`** (rw-r--r--) : Fichiers normaux (lecture pour tous, écriture pour le propriétaire).
- **`755`** (rwxr-xr-x) : Scripts/exécutables (exécution pour tous).

### ✅ **Pour les répertoires** :
- **`755`** (rwxr-xr-x) : Répertoires partagés (accès + liste pour tous).
- **`700`** (rwx------) : Répertoires privés (uniquement le propriétaire).

### ❌ **À éviter** :
- **`777`** : Trop permissif (risque de sécurité).
- **SETUID/SETGID** sur des fichiers non sécurisés (risque d'escalade de privilèges).

---

## 📝 **Résumé visuel**
```
Permissions :  - r w x  r - x  r - -
               │ │ │ │  │ │ │  │ │ │
               │ │ │ │  │ │ │  │ │ └─ 👥 Autres : Lecture (r)
               │ │ │ │  │ │ │  │ └─── 👥 Autres : Aucun (-)
               │ │ │ │  │ │ │  └───── 👥 Autres : Aucun (-)
               │ │ │ │  │ │ └──────── 👥 Groupe : Exécution (x)
               │ │ │ │  │ └────────── 👥 Groupe : Aucun (-)
               │ │ │ │  └──────────── 👥 Groupe : Lecture (r)
               │ │ │ └─────────────── 👤 Propriétaire : Exécution (x)
               │ │ └──────────────── 👤 Propriétaire : Écriture (w)
               │ └────────────────── 👤 Propriétaire : Lecture (r)
               └──────────────────── 📁 Type : Fichier normal (-)
```

---

[...retorn en rèire](../menu.md)