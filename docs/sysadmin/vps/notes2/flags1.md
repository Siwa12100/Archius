# **Flags Spéciaux Linux (`s/S`, `t/T`)**

[...retorn en rèire](../menu.md)

---

## **📋 Sommaire**
1. **[Introduction](#-1-introduction)**
2. **[Tableau Récapitulatif des Flags](#-2-tableau-récapitulatif-des-flags)**
3. **[Syntaxe de Placement](#-3-syntaxe-de-placement)**
   - 3.1. [Syntaxe Absolue (Octale)](#31-syntaxe-absolue-octale)
   - 3.2. [Syntaxe Symbolique](#32-syntaxe-symbolique)
4. **[Détails par Flag](#-4-détails-par-flag)**
   - 4.1. [SetUID (`s`/`S`)](#41-setuid-ss)
   - 4.2. [SetGID (`s`/`S`)](#42-setgid-ss)
   - 4.3. [Sticky Bit (`t`/`T`)](#43-sticky-bit-tt)
5. **[Vérification des Flags](#-5-vérification-des-flags)**
6. **[Sécurité et Bonnes Pratiques](#-6-sécurité-et-bonnes-pratiques)**
7. **[Cas d'Usage Concrets](#-7-cas-dusage-concrets)**
8. **[Annexes : Outils et Commandes Utiles](#-8-annexes-outils-et-commandes-utiles)**

---

## **📌 1. Introduction**
Les **flags spéciaux** sous Linux (`s/S`, `t/T`) permettent de modifier le comportement standard des permissions pour :
- **Exécuter des fichiers avec des droits élevés** (SetUID/SetGID).
- **Protéger les répertoires partagés** (Sticky Bit).
- **Gérer l'héritage des groupes** (SetGID sur les répertoires).

Ces flags sont **puissants mais risqués** s'ils sont mal configurés.

---

## **📊 2. Tableau Récapitulatif des Flags**
| Flag | Nom          | Contexte               | Effet                                                                 | Affichage `ls -l`       | Risque          |
|------|--------------|------------------------|-----------------------------------------------------------------------|--------------------------|-----------------|
| `s`  | **SetUID**   | Fichiers exécutables   | Exécution avec les **droits du propriétaire**.                       | `-rwsr-xr-x`            | ⚠️ Élevé        |
| `S`  | **SetUID**   | Fichiers non exécutables | SetUID **positionné mais inactif** (fichier non exécutable).         | `-rwSr--r--`            | Aucun           |
| `s`  | **SetGID**   | Fichiers/répertoires   | Exécution avec les **droits du groupe** ou héritage de groupe.       | `-rwxr-sr-x`            | ⚠️ Moyen        |
| `S`  | **SetGID**   | Fichiers/répertoires   | SetGID **positionné mais inactif** (pas de droit `x`).               | `-rw-r-Sr--`            | Aucun           |
| `t`  | **Sticky Bit** | Répertoires           | Seul le **propriétaire** peut supprimer/renommer ses fichiers.       | `drwxrwxrwt`            | ⚠️ Faible       |
| `T`  | **Sticky Bit** | Répertoires           | Sticky Bit **inactif** (répertoire non traversable par "others").     | `drwxrwxr-T`            | Aucun           |

---

## **🛠️ 3. Syntaxe de Placement**
### **3.1. Syntaxe Absolue (Octale)**
Les permissions spéciales sont ajoutées via un **4ème chiffre** en notation octale :
| Flag       | Valeur Octale | Exemple `chmod` | Résultat `ls -l`          |
|------------|---------------|-----------------|----------------------------|
| **SetUID** | `4`           | `chmod 4755`    | `-rwsr-xr-x`              |
| **SetGID** | `2`           | `chmod 2755`    | `-rwxr-sr-x`              |
| **Sticky** | `1`           | `chmod 1777`    | `drwxrwxrwt`              |
| **Combiné**| `4+2+1=7`     | `chmod 7755`    | `-rwsr-sr-x`              |

**Exemples** :
```bash
# Activer SetUID sur un binaire
chmod 4755 /usr/local/bin/mon_outil   # -rwsr-xr-x

# Activer SetGID sur un répertoire (héritage de groupe)
chmod 2775 /dossier_partage          # drwxrwsr-x

# Activer Sticky Bit sur /tmp
chmod 1777 /tmp                      # drwxrwxrwt
```

---
### **3.2. Syntaxe Symbolique**
Utilise des lettres pour cibler **qui** (`u/g/o`) et **quoi** (`s/t`).
| Commande          | Effet                                  | Exemple                  |
|-------------------|----------------------------------------|--------------------------|
| `chmod u+s`       | Active **SetUID** (propriétaire).      | `chmod u+s mon_fichier`  |
| `chmod u-s`       | Désactive SetUID.                      | `chmod u-s mon_fichier`  |
| `chmod g+s`       | Active **SetGID** (groupe).            | `chmod g+s mon_dossier`  |
| `chmod g-s`       | Désactive SetGID.                      | `chmod g-s mon_dossier`  |
| `chmod +t`        | Active **Sticky Bit**.                 | `chmod +t /tmp`          |
| `chmod -t`        | Désactive Sticky Bit.                  | `chmod -t /tmp`          |

**Exemples** :
```bash
# Activer SetUID sur un binaire
chmod u+s /usr/local/bin/mon_outil   # -rwsr-xr-x

# Activer SetGID sur un répertoire
chmod g+s /srv/projet                # drwxrwsr-x

# Désactiver Sticky Bit
chmod -t /dossier                    # drwxrwxrwx
```

---

## **🔍 4. Détails par Flag**
### **4.1. SetUID (`s`/`S`)**
#### **Fonctionnement**
- Un fichier avec **SetUID** s'exécute avec les **droits du propriétaire** (et non de l'utilisateur).
- **Exemple** : `passwd` (propriétaire `root`) permet de modifier `/etc/shadow`.

#### **Cas d'Usage**
- **Binaires système** : `passwd`, `sudo`, `chsh`.
- **Programmes personnalisés** nécessitant des droits élevés.

#### **Risques**
- **Élévation de privilèges** si le binaire est vulnérable.
- **Exemple d'attaque** :
  ```bash
  # Si un programme SetUID appelle `/bin/sh` sans précaution :
  ./programme_vulnerable
  # Peut donner un shell root !
  ```

#### **Bonnes Pratiques**
- **Limiter aux binaires essentiels**.
- **Auditer régulièrement** :
  ```bash
  find / -type f -perm -4000 -ls  # Liste tous les SetUID
  ```

---
### **4.2. SetGID (`s`/`S`)**
#### **Fonctionnement**
- **Fichiers** : Exécution avec les droits du **groupe propriétaire**.
- **Répertoires** : Les nouveaux fichiers **héritent du groupe du répertoire**.

#### **Cas d'Usage**
- **Répertoires partagés** entre membres d'un groupe.
- **Binaires** nécessitant des droits de groupe spécifiques.

#### **Exemple avec Répertoire**
```bash
mkdir /srv/equipe_dev
chgrp dev /srv/equipe_dev      # Change le groupe en "dev"
chmod 2775 /srv/equipe_dev    # Active SetGID (drwxrwsr-x)
```
- **Effet** : Tous les fichiers créés dans `/srv/equipe_dev` appartiendront au groupe `dev`.

---
### **4.3. Sticky Bit (`t`/`T`)**
#### **Fonctionnement**
- **Répertoires** : Seul le **propriétaire du fichier** peut le supprimer/renommer.
- **Exemple** : `/tmp` utilise le Sticky Bit pour éviter que les utilisateurs ne supprimient les fichiers des autres.

#### **Cas d'Usage**
- **Répertoires partagés** (`/tmp`, `/var/tmp`).
- **Répertoires avec écriture pour tous** (`777`).

#### **Activation**
```bash
chmod +t /tmp   # drwxrwxrwt
```

#### **Sticky Bit Inactif (`T`)**
- Si le répertoire n'a **pas de droit `x` pour "others"**, le `t` devient `T` :
  ```bash
  chmod +t,o-x dossier   # drwxrwxr-T
  ```

---

## **🔎 5. Vérification des Flags**
| Commande               | Description                                  |
|------------------------|----------------------------------------------|
| `ls -l`                | Affiche `s`, `S`, `t`, `T` dans les permissions. |
| `stat fichier`         | Détails avancés (UID/GID effectifs).         |
| `find / -perm -4000`   | Liste tous les fichiers **SetUID**.          |
| `find / -perm -2000`   | Liste tous les fichiers **SetGID**.          |
| `find / -perm -1000`   | Liste tous les répertoires **Sticky Bit**.   |

**Exemple avec `stat`** :
```bash
stat /usr/bin/passwd
```
**Résultat** :
```
Access: (4755/-rwsr-xr-x)  Uid: (    0/    root)   Gid: (    0/    root)
```

---

## **🛡️ 6. Sécurité et Bonnes Pratiques**
### **✅ À Faire**
| Pratique                     | Commande Exemple                          |
|------------------------------|-------------------------------------------|
| **Limiter SetUID/SetGID**    | `chmod u-s,g-s fichier_dangereux`         |
| **Auditer régulièrement**    | `find / -type f \( -perm -4000 -o -perm -2000 \)` |
| **Utiliser SetGID pour les répertoires partagés** | `chmod g+s /srv/projet` |
| **Protéger `/tmp` avec Sticky Bit** | `chmod 1777 /tmp`               |
| **Vérifier les propriétaires** | `chown root:root /chemin/vers/binaire_sensible` |

### **❌ À Éviter**
| Piège                          | Risque                                  |
|--------------------------------|-----------------------------------------|
| **SetUID sur des scripts**     | Linux l'ignore, mais risque sur d'autres Unix. |
| **SetUID sur des binaires personnalisés non audités** | Vulnérabilités => élévation de privilèges. |
| **Permissions `777` + SetGID** | Tout le monde peut modifier les fichiers. |
| **Sticky Bit sur des répertoires non partagés** | Inutile et source de confusion. |

---

## **📂 7. Cas d'Usage Concrets**
### **7.1. Répertoire Partagé avec SetGID**
```bash
sudo mkdir -p /srv/equipe
sudo chgrp dev /srv/equipe
sudo chmod 2775 /srv/equipe
```
- **Effet** : Tous les fichiers créés dans `/srv/equipe` appartiendront au groupe `dev`.

---
### **7.2. Sécuriser `/tmp` avec Sticky Bit**
```bash
sudo chmod 1777 /tmp   # drwxrwxrwt
```
- **Effet** : Les utilisateurs ne peuvent supprimer que leurs propres fichiers.

---
### **7.3. Créer un Binaire SetUID (Exemple en C)**
```c
#include <stdio.h>
#include <unistd.h>

int main() {
    printf("UID réel: %d\n", getuid());      // UID de l'utilisateur
    printf("UID effectif: %d\n", geteuid());  // UID du propriétaire (root si SetUID)
    return 0;
}
```
**Compilation & Activation** :
```bash
gcc mon_programme.c -o mon_programme
sudo chown root:root mon_programme
chmod 4755 mon_programme   # Active SetUID
./mon_programme
```
**Résultat** :
```
UID réel: 1000 (votre UID)
UID effectif: 0 (root)
```

---

## **📚 8. Annexes : Outils et Commandes Utiles**
| Outil          | Description                                                                 |
|----------------|-----------------------------------------------------------------------------|
| `namei -l /chemin` | Affiche les permissions de chaque composant d'un chemin.                  |
| `getfacl`      | Affiche les **ACL** (permissions avancées) si activées.                   |
| `setuid()` (C) | Fonction pour changer l'UID effectif dans un programme.                   |
| `strace`       | Trace les appels système (utile pour déboguer SetUID).                     |
| `umask`        | Contrôle les permissions par défaut des nouveaux fichiers.                |

---
---

## **🎯 Conclusion**
- **SetUID (`s`/`S`)** : Permet d'exécuter un fichier avec les droits du propriétaire (**risque élevé**).
- **SetGID (`s`/`S`)** : Utile pour les **répertoires partagés** et l'héritage de groupe (**risque moyen**).
- **Sticky Bit (`t`/`T`)** : Protège les répertoires partagés contre les suppressions non autorisées (**risque faible**).
- **Syntaxe** :
  - **Octale** (`4`=SetUID, `2`=SetGID, `1`=Sticky) pour une application rapide.
  - **Symbolique** (`u+s`, `g+s`, `+t`) pour une approche plus lisible.

**🔹 Règle d'or** : *"Moins il y a de fichiers avec des flags spéciaux, plus le système est sécurisé."*

--- 

[...retorn en rèire](../menu.md)