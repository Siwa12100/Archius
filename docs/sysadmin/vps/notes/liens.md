# 🔗 Liens sous Linux (Symlinks vs Hard Links)

[...retorn en rèire](../menu.md)

---

## **📌 Comparatif Rapide**
| **Type**       | **Lien Symbolique (Symlink)**                     | **Lien Physique (Hard Link)**                     |
|----------------|------------------------------------------------|------------------------------------------------|
| **Syntaxe**    | `ln -s source lien`                            | `ln source lien`                               |
| **Cible**      | Pointe vers le **nom** d'un fichier.           | Pointe vers les **données** (inode) du fichier. |
| **Fichiers**   | Fonctionne pour **fichiers + dossiers**.       | **Fichiers uniquement** (pas dossiers).         |
| **Filesystem** | Peut cibler un autre filesystem.               | Doit être sur le **même filesystem**.          |
| **Inode**      | A son **propre inode** (différent de la cible). | Partage le **même inode** que la source.       |
| **Suppression**| Si la source est supprimée, le lien est **brisé**. | Les données persistent tant qu’un lien existe. |
| **Taille**     | Occupe quelques octets (stocke le chemin).     | N’occupe **pas d’espace supplémentaire**.       |

---

## **🛠️ Commandes Clés**
| Commande                          | Description                                  |
|-----------------------------------|---------------------------------------------|
| `ln source lien`                  | Crée un **hard link**.                      |
| `ln -s source lien`               | Crée un **symlink**.                        |
| `ls -i fichier`                  | Affiche l’**inode** du fichier.             |
| `ls -l`                           | Affiche les liens (symlinks avec `->`).     |
| `find /chemin -samefile fichier`  | Trouve tous les **hard links** d’un fichier.|
| `readlink lien_symbolique`        | Affiche la cible d’un symlink.             |
| `stat fichier`                    | Affiche les métadonnées (inode, liens, etc.). |

---

## **📂 Cas d’Usage**
### **1. Hard Links**
- **Quand l’utiliser ?**
  - Pour avoir **plusieurs noms** pour le **même fichier** (sans dupliquer les données).
  - Exemple : Sauvegardes ou versions alternatives d’un fichier.
- **Exemple :**
  ```bash
  echo "Données" > fichier.txt
  ln fichier.txt backup.txt  # Crée un hard link
  ls -i fichier.txt backup.txt  # Même inode
  ```
- **Limites :**
  - Impossible pour les **dossiers**.
  - Impossible entre **filesystems différents**.

### **2. Symlinks (Liens Symboliques)**
- **Quand l’utiliser ?**
  - Pour créer des **raccourcis** (comme sous Windows).
  - Pour lier des fichiers/dossiers **sur différents disks**.
  - Pour les **dossiers** (contrairement aux hard links).
- **Exemple :**
  ```bash
  ln -s /chemin/vers/dossier lien_dossier  # Symlink vers un dossier
  ln -s fichier.txt lien_fichier           # Symlink vers un fichier
  ```
- **Attention :**
  - Si la source est supprimée, le symlink devient **brisé** (`ls` affiche en rouge).
  - Un symlink peut pointer vers **un fichier qui n’existe pas encore**.

---

## **⚠️ Pièges Courants**
| Problème                          | Solution                                  |
|-----------------------------------|------------------------------------------|
| **Symlink brisé** (`dangling`)   | Vérifiez la cible avec `readlink`.       |
| **Hard link vers un dossier**     | Impossible (utilisez `ln -s` à la place).|
| **Suppression accidentelle**      | Les données persistent si un hard link existe. |
| **Copie (`cp`) d’un symlink**     | Utilisez `cp -L` pour copier la cible, ou `cp -P` pour copier le lien. |

---

## **📊 Exemple Pratique**
```bash
# Créer un fichier et des liens
echo "Contenu" > original.txt

# Hard link
ln original.txt hardlink.txt

# Symlink
ln -s original.txt symlink.txt

# Vérifier
ls -li *
```
**Sortie :**
```
123456 -rw-r--r-- 2 user group 8 mai 10 10:00 hardlink.txt
123456 -rw-r--r-- 2 user group 8 mai 10 10:00 original.txt
789012 lrwxrwxrwx 1 user group 11 mai 10 10:00 symlink.txt -> original.txt
```
- **`123456`** : Même inode pour `original.txt` et `hardlink.txt` (mêmes données).
- **`789012`** : Le symlink a son propre inode et pointe vers `original.txt`.

---

## **🔍 Vérifications Utiles**
| Commande                          | Utile pour...                              |
|-----------------------------------|--------------------------------------------|
| `ls -l`                           | Voir les symlinks (flèche `->`).           |
| `file lien`                       | Savoir si c’est un symlink ou hard link.   |
| `find . -type l`                  | Lister tous les symlinks dans un dossier.  |
| `find . -links +1 -type f`        | Lister les fichiers avec plusieurs hard links. |

---

## **🎯 Quand Utiliser Quoi ?**
| Besoin                          | Solution               |
|---------------------------------|------------------------|
| **Économiser de l’espace**      | Hard link.             |
| **Lier un dossier**             | Symlink (`ln -s`).     |
| **Lier entre disks**            | Symlink.               |
| **Créer un raccourci**          | Symlink.               |
| **Garantir l’intégrité des données** | Hard link.       |

---

[...retorn en rèire](../menu.md)