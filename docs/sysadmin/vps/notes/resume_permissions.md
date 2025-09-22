# 📁 Comprendre les Permissions, Groupes et Utilisateurs sous Linux (EXT4)

[...retorn en rèire](../menu.md)

---

## **🔹 1. Fondamentaux : Fichiers, Groupes et Propriétaires**
### **✅ Un fichier a TOUJOURS un groupe et un propriétaire**
Chaque fichier/dossier sous Linux est **obligatoirement** associé à :
- **Un propriétaire (user)** : L'utilisateur qui l'a créé (ex: `alice`).
- **Un groupe (group)** : Par défaut, le **groupe principal** de l'utilisateur.

---
### **📌 Exemple avec un fichier nouvellement créé**
```bash
# 1. Créer un fichier en tant qu'utilisateur "alice"
touch mon_fichier.txt

# 2. Vérifier ses permissions
ls -l mon_fichier.txt
```
**Sortie typique** :
```
-rw-r--r-- 1 alice alice 0 Jun 10 12:00 mon_fichier.txt
```
- **1er `alice`** = Propriétaire (user).
- **2ème `alice`** = Groupe propriétaire (groupe principal de `alice`).

---
### **🔧 Comment connaître son groupe principal ?**
```bash
# Affiche les groupes de l'utilisateur (le premier est le groupe principal)
groups alice
```
**Exemple de sortie** :
```
alice sudo devs
```
→ Le groupe principal de `alice` est **`alice`** (premier de la liste).

---
### **🔹 Pourquoi le groupe principal est-il utilisé par défaut ?**
- Quand un utilisateur crée un fichier, Linux attribue **automatiquement** :
  - **Propriétaire** = L'utilisateur (`alice`).
  - **Groupe** = Le **groupe principal** de l'utilisateur (`alice`).

---
### **🛠 Changer le groupe par défaut pour les nouveaux fichiers**
#### **1️⃣ Méthode temporaire (pour la session en cours)**
```bash
# Passer temporairement dans un autre groupe (ex: "devs")
newgrp devs

# Créer un fichier → son groupe sera "devs"
touch fichier_dans_devs.txt
ls -l fichier_dans_devs.txt
```
**Résultat** :
```
-rw-r--r-- 1 alice devs 0 Jun 10 12:05 fichier_dans_devs.txt
```
→ Le groupe est maintenant `devs` (au lieu de `alice`).

---
#### **2️⃣ Méthode permanente (changer le groupe principal)**
```bash
# Changer le groupe principal de "alice" en "devs" (nécessite sudo)
sudo usermod -g devs alice
```
⚠️ **Attention** :
- Cela affecte **tous les nouveaux fichiers** créés par `alice`.
- Le groupe `alice` (homonyme) continue d'exister, mais n'est plus le groupe principal.

---
## **🔹 2. Gestion des Groupes Utilisateurs**
### **✅ Un utilisateur a-t-il toujours un groupe à son nom ?**
**Oui, par défaut**, mais ce n'est pas obligatoire.
- Quand vous créez un utilisateur avec `adduser` ou `useradd`, Linux crée **automatiquement un groupe portant le même nom** (ex: `alice` → groupe `alice`).
- **Exception** : Avec `useradd -N`, aucun groupe n'est créé.

---
### **📌 Vérification des groupes d'un utilisateur**
```bash
# Voir les groupes de l'utilisateur (le premier est le groupe principal)
id alice
```
**Sortie** :
```
uid=1001(alice) gid=1001(alice) groupes=1001(alice),27(sudo),1003(devs)
```
- `gid=1001(alice)` → **Groupe principal**.
- `groupes=...` → **Groupes secondaires** (ex: `sudo`, `devs`).

---
### **🔧 Que se passe-t-il si on supprime le groupe principal ?**
#### **⚠️ Problèmes potentiels**
- L'utilisateur ne peut plus **créer de fichiers** (erreur de permissions).
- Les nouveaux fichiers auront un **GID numérique** (ex: `1001`) au lieu d'un nom de groupe.
- Certaines commandes (comme `newgrp`) peuvent échouer.

---
#### **📌 Solution : Changer le groupe principal AVANT de supprimer l'ancien**
```bash
# 1. Ajouter l'utilisateur à un nouveau groupe (ex: "devs")
sudo usermod -aG devs alice

# 2. Changer le groupe principal
sudo usermod -g devs alice

# 3. Supprimer l'ancien groupe (maintenant inutilisé)
sudo groupdel alice
```
**Résultat** :
- Les nouveaux fichiers auront le groupe `devs` :
  ```bash
  touch test.txt
  ls -l test.txt
  ```
  **Sortie** :
  ```
  -rw-r--r-- 1 alice devs 0 Jun 10 14:05 test.txt
  ```

---
## **🔹 3. Permissions et Répertoires Système (EXT4)**
### **🗺 Hiérarchie typique des répertoires**
```
/
├── /home/          # Dossiers personnels (ex: /home/alice)
├── /etc/           # Fichiers de configuration (ex: /etc/passwd)
├── /var/           # Données variables (logs, bases de données)
├── /usr/           # Programmes et bibliothèques
├── /tmp/           # Fichiers temporaires (accessible à tous en écriture)
├── /root/          # Dossier personnel de root
├── /bin/, /sbin/   # Commandes essentielles (ex: ls, chmod)
└── /boot/          # Fichiers de démarrage
```

---
### **👥 Utilisateurs et Groupes par Défaut**
| Utilisateur | Groupe Principal | Groupes Secondaires | Rôle |
|-------------|------------------|--------------------|------|
| `root`      | `root` (GID 0)   | -                  | Super-utilisateur (accès illimité). |
| `alice`     | `alice` (GID 1000) | `sudo`, `users`    | Utilisateur standard avec droits `sudo`. |
| `bob`       | `bob` (GID 1001)   | `users`            | Utilisateur standard sans `sudo`. |

---
| Groupe       | GID  | Membres typiques | Permission associée |
|--------------|------|------------------|---------------------|
| `root`       | 0    | `root`           | Accès complet. |
| `sudo`       | 27   | `alice`          | Peut utiliser `sudo`. |
| `users`      | 100  | `alice`, `bob`   | Groupe générique. |
| `www-data`   | 33   | -                | Utilisé par Apache/Nginx. |
| `docker`     | 999  | -                | Accès à Docker. |

---
### **📂 Permissions par Répertoire Clé**
| Répertoire       | Permissions (`ls -ld`) | Propriétaire:Groupe | Qui peut faire quoi ? |
|------------------|------------------------|----------------------|------------------------|
| `/`              | `drwxr-xr-x`          | `root:root`          | Tout le monde peut lister (`r-x`), seul `root` peut écrire. |
| `/home`          | `drwxr-xr-x`          | `root:root`          | Les utilisateurs ne peuvent pas lister `/home/bob` (sauf `root`). |
| `/home/alice`    | `drwx------`          | `alice:alice`        | Seul `alice` (ou `root`) peut accéder. |
| `/etc`           | `drwxr-xr-x`          | `root:root`          | Tout le monde peut lire, seul `root` peut modifier. |
| `/var/www`       | `drwxr-xr-x`          | `root:www-data`      | Le groupe `www-data` (Apache) peut lire/écrire. |
| `/tmp`           | `drwxrwxrwt`          | `root:root`          | Tout le monde peut écrire (`rwx`), mais seul le propriétaire peut supprimer ses fichiers (`sticky bit`). |

---
## **🔹 4. Exemples Concrets de Permissions**
### **1️⃣ Fichier personnel dans `/home/alice`**
```bash
ls -l /home/alice/mon_fichier.txt
```
**Sortie** :
```
-rw-r--r-- 1 alice alice 4096 Jun 10 10:00 mon_fichier.txt
```
| Élément       | Valeur       | Signification |
|---------------|--------------|---------------|
| Permissions   | `rw-r--r--`  | `alice` peut lire/écrire, le groupe `alice` et les autres peuvent lire. |
| Propriétaire  | `alice`      | Seul `alice` peut modifier les permissions. |
| Groupe        | `alice`      | Les membres du groupe `alice` (ici, seul `alice`) peuvent lire. |

**Qui peut faire quoi ?**
| Utilisateur | Lire | Écrire | Supprimer |
|-------------|------|--------|-----------|
| `alice`     | ✅   | ✅     | ✅        |
| `bob`       | ✅   | ❌     | ❌        |
| `root`      | ✅   | ✅     | ✅        |

---
### **2️⃣ Répertoire partagé pour une équipe (`/var/www/projet`)**
```bash
ls -ld /var/www/projet
```
**Sortie** :
```
drwxrwsr-x 2 root devs 4096 Jun 10 10:05 /var/www/projet
```
| Élément       | Valeur       | Signification |
|---------------|--------------|---------------|
| Permissions   | `rwxrwsr-x`  | `root` a `rwx`, le groupe `devs` a `rwx` (SetGID activé), les autres ont `r-x`. |
| SetGID (`s`)  | Activé       | Les nouveaux fichiers hériteront du groupe `devs`. |
| Propriétaire  | `root`       | Seul `root` peut changer les permissions. |
| Groupe        | `devs`       | Les membres de `devs` (`alice`, `bob`) peuvent lire/écrire. |

**Qui peut faire quoi ?**
| Utilisateur | Lire | Écrire | Créer des fichiers | Supprimer |
|-------------|------|--------|--------------------|-----------|
| `root`      | ✅   | ✅     | ✅                 | ✅        |
| `alice`     | ✅   | ✅     | ✅ (groupe `devs`) | ✅ (si propriétaire) |
| `bob`       | ✅   | ✅     | ✅ (groupe `devs`) | ✅ (si propriétaire) |
| `charlie`   | ✅   | ❌     | ❌                 | ❌        |

---
### **3️⃣ Commande système dans `/usr/bin`**
```bash
ls -l /usr/bin/ls
```
**Sortie** :
```
-rwxr-xr-x 1 root root 145000 Jan 18 2022 /usr/bin/ls
```
| Élément       | Valeur       | Signification |
|---------------|--------------|---------------|
| Permissions   | `rwxr-xr-x`  | Tout le monde peut **exécuter** (`r-x`), seul `root` peut modifier. |
| Propriétaire  | `root`       | Seul `root` (ou `sudo`) peut mettre à jour la commande. |

---
## **🔹 5. Cas Pratiques Courants**
### **1️⃣ Partager un dossier entre utilisateurs**
**Objectif** : Permettre à `alice` et `bob` (membres de `devs`) de collaborer sur `/home/shared`.
```bash
# 1. Créer le dossier et définir le groupe
sudo mkdir /home/shared
sudo chgrp devs /home/shared

# 2. Donner les permissions (rwx pour le groupe + SetGID)
sudo chmod 770 /home/shared
sudo chmod g+s /home/shared

# 3. Ajouter alice et bob au groupe devs
sudo usermod -aG devs alice
sudo usermod -aG devs bob
```
**Résultat** :
- `alice` et `bob` peuvent **lire/écrire/supprimer** des fichiers.
- Les nouveaux fichiers auront **automatiquement le groupe `devs`** (grâce à `g+s`).

---
### **2️⃣ Donner accès à Docker sans `sudo`**
```bash
# Ajouter alice au groupe docker
sudo usermod -aG docker alice

# Vérifier
groups alice  # Doit afficher "docker" dans la liste
```
**Effet** :
- `alice` peut maintenant exécuter `docker ps` **sans `sudo`**.

---
### **3️⃣ Restreindre l’accès à un fichier sensible**
**Objectif** : Seul `root` et `alice` peuvent lire `/etc/secret.conf`.
```bash
# 1. Créer le fichier
sudo touch /etc/secret.conf

# 2. Définir le propriétaire et groupe
sudo chown root:alice /etc/secret.conf

# 3. Donner les permissions (root=rw, alice=r, autres=rien)
sudo chmod 640 /etc/secret.conf
```
**Permissions résultantes** :
```
-rw-r----- 1 root alice 0 Jun 10 11:00 /etc/secret.conf
```
| Utilisateur | Lire | Écrire |
|-------------|------|--------|
| `root`      | ✅   | ✅     |
| `alice`     | ✅   | ❌     |
| `bob`       | ❌   | ❌     |

---
## **🔹 6. Permissions Spéciales**
| Permission   | Symbole | Effet | Exemple |
|--------------|---------|-------|---------|
| **SetUID**   | `rwsr-xr-x` | Le fichier s’exécute avec les droits du **propriétaire** (ex: `root`). | `/usr/bin/passwd` |
| **SetGID**   | `rwxrwsr-x` | Les nouveaux fichiers dans un répertoire héritent du **groupe du répertoire**. | `/var/www` |
| **Sticky Bit** | `rwxr-xr-t` | Seul le propriétaire d’un fichier peut le **supprimer** dans un répertoire. | `/tmp` |

---
## **🔹 7. Résumé des Bonnes Pratiques**
1. **Utilisez les groupes** pour partager des fichiers (évitez `chmod 777`).
2. **Activez SetGID** (`chmod g+s`) sur les répertoires partagés pour hériter du groupe.
3. **Évitez les permissions `others=w`** (sauf pour `/tmp`).
4. **Utilisez `sudo` avec parcimonie** : Ajoutez les utilisateurs au groupe `sudo` uniquement si nécessaire.
5. **Vérifiez les permissions** avec :
   ```bash
   ls -l   # Pour les fichiers
   ls -ld  # Pour les répertoires
   ```

---
## **📚 Commandes Utiles**
| Commande | Description | Exemple |
|----------|-------------|---------|
| `id <user>` | Affiche l'UID, GID et groupes. | `id alice` |
| `groups <user>` | Liste les groupes de l'utilisateur. | `groups alice` |
| `usermod -g` | Change le groupe principal. | `sudo usermod -g devs alice` |
| `usermod -aG` | Ajoute à un groupe secondaire. | `sudo usermod -aG sudo alice` |
| `groupdel` | Supprime un groupe. | `sudo groupdel oldgroup` |
| `chgrp` | Change le groupe d'un fichier. | `sudo chgrp devs fichier.txt` |
| `chmod g+s` | Active SetGID sur un répertoire. | `chmod g+s /dossier_partage` |
| `find / -group <group>` | Trouve les fichiers d'un groupe. | `find / -group devs 2>/dev/null` |

---
## **🎯 Exemple Final : Arbre des Permissions sur `/home`**
```bash
ls -l /home
```
**Sortie typique** :
```
drwx------  2 alice alice 4096 Jun 10 10:00 alice
drwx------  2 bob   bob   4096 Jun 10 10:01 bob
drwxrwxr-x  2 root  devs  4096 Jun 10 10:02 shared_project
```
| Dossier | Permissions | Propriétaire:Groupe | Signification |
|---------|-------------|----------------------|---------------|
| `/home/alice` | `drwx------` | `alice:alice` | Seul `alice` peut accéder. |
| `/home/bob` | `drwx------` | `bob:bob` | Seul `bob` peut accéder. |
| `/home/shared_project` | `drwxrwxr-x` | `root:devs` | Le groupe `devs` peut lire/écrire, les autres peuvent lire. |

---

## `mv`, `chown` et les **liens symboliques**

### Permissions & appartenance d’un lien symbolique

* Un **lien symbolique** a typiquement des perms indiquées comme `lrwxrwxrwx`, mais **ces permissions sont ignorées** pour l’accès au contenu :
  c’est **le fichier pointé** qui décide des droits d’accès.
* Le **propriétaire/groupe** du symlink existent (métadonnées du lien), mais pour **supprimer** un symlink, ce sont les **droits du dossier parent** (écrire+exécuter) qui comptent, pas ceux du lien ni de la cible.

### `mv` sur un lien symbolique

* `mv symlink …` **déplace/renomme le lien lui-même**, **pas** le fichier cible.
* **Même filesystem** : c’est un *rename* → les métadonnées du lien ne changent pas (même inode).
* **Filesystem différent** : c’est *copy + unlink* → on recrée un **nouveau lien** ; ses métadonnées peuvent différer (nouvel inode, propriétaire/groupe selon le contexte).

### `chown` et liens symboliques

* Par **défaut**, `chown symlink` agit sur **la cible** (il **déréférence** le lien).
* Pour changer **l’owner du lien lui-même**, utilise l’option **`-h`** (ou la variante système **`lchown`**) si disponible :

  * `chown -h nouvel_user:nouveau_groupe mon_lien` → change le **symlink**, pas la cible.
* Astuce : `stat -c '%A %U:%G %N' mon_lien` et `stat -c '%A %U:%G' -- "$(readlink -f mon_lien)"` te permettent de comparer lien vs cible.

### Accès via un lien symbolique

* Lire/écrire/exécuter via un symlink dépend **exclusivement** des droits de la **cible** (et des droits de parcours `x` sur les dossiers de son chemin).
* Les perms affichées sur le lien n’entrent pas dans la décision d’accès.

---

[...retorn en rèire](../menu.md)