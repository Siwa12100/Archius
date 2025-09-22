
# **Introduction au Shell**

[...retorn en rèire](../menu.md)

---

## **📋 Table des Matières**
1. [🌟 Introduction](#-introduction)
2. [🔧 Généralités sur le Shell](#-généralités-sur-le-shell)
3. [📖 Documentation (`man`)](#-documentation-man)
4. [⚡ Méta-caractères](#-méta-caractères)
5. [💬 Délimiteurs de Chaînes](#-délimiteurs-de-chaînes)
6. [📁 Expansion des Noms de Fichiers](#-expansion-des-noms-de-fichiers)
7. [🔄 Redirection](#-redirection)
8. [🔗 Pipeline](#-pipeline)
9. [🔗 Enchaînement de Commandes](#-enchaînement-de-commandes)
10. [📦 Variables](#-variables)
11. [⚖️ Commande `test`](#⚖️-commande-test)
12. [🧮 Commande `expr`](#-commande-expr)
13. [🔄 Structures de Contrôle](#-structures-de-contrôle)
14. [🔁 Boucles](#-boucles)
15. [⚙️ Commande Interne `set`](#⚙️-commande-interne-set)
16. [⚠️ Pièges & Bonnes Pratiques](#⚠️-pièges--bonnes-pratiques)
17. [📝 Mini-Aide-Mémoire](#-mini-aide-mémoire)

---

## **🌟 Introduction**
Le **Shell** est un **interpréteur de commandes** qui permet d’interagir avec le système d’exploitation.
🔹 **Double rôle** :
- **Langage de commandes** (exécution interactive).
- **Langage de programmation** (scripts pour automatiser des tâches).

🔹 **Deux modes d’utilisation** :
| Mode | Description | Exemple |
|------|-------------|---------|
| **Interactif** | Interface utilisateur avec historique (`↑`/`↓`), complétion (`Tab`), etc. | `ls -l` → liste les fichiers. |
| **Programmé** | Scripts Shell (fichiers `.sh`) avec variables, boucles, tests, etc. | `#!/bin/bash` → shebang pour un script. |

💡 **Pourquoi apprendre le Shell ?**
✅ **Automatisation** (tâches répétitives).
✅ **Puissance** (manipulation de fichiers/processus).
✅ **Portabilité** (disponible sur tous les systèmes Unix/Linux).

---

## **🔧 Généralités sur le Shell**

### **🎯 Rôle du Shell**
| Type de commande | Description | Exemple |
|------------------|-------------|---------|
| **Commandes externes** | Lancement d’un nouveau processus. | `ls`, `grep` |
| **Commandes internes** | Pas de nouveau processus (exécutées par le Shell lui-même). | `cd`, `echo`, `read` |
| **Mots-clés** | Structures du langage (contrôle de flux). | `if`, `while`, `for` |
| **Fonctions utilisateur** | Blocs de code réutilisables définis par l’utilisateur. | `ma_fonction() { ... }` |
| **Commandes `PATH`** | Exécutables trouvés dans les répertoires listés par `$PATH`. | `/bin/ls`, `/usr/bin/grep` |

### **🖥️ Fonctionnement du Shell**
| Mode | Description | Fonctionnalités |
|------|-------------|-----------------|
| **Interactif** | Interface pour l’utilisateur. | Historique (`history`), complétion (`Tab`), alias. |
| **Programmé** | Langage de script. | Variables, boucles, tests, fonctions. |

### **📌 Caractéristiques**
- **Langage interprété** (pas de compilation).
- **Orienté fichiers et processus**.
- **Syntaxe stricte** (les espaces comptent !).
- **Scripts** = fichiers texte **commentés** pour la lisibilité.

> ⚠️ **Piège** : `var=valeur` ✅ vs `var = valeur` ❌ (espaces interdits autour de `=`).

---

## **📖 Documentation (`man`)**
La commande `man` (manual) affiche la documentation des commandes, organisée en **sections** :

```bash
man [section] nom_commande
```

| Section | Description | Exemple |
|---------|-------------|---------|
| **1** | Commandes utilisateur. | `man 1 ls` |
| **2** | Appels système (kernel). | `man 2 open` |
| **3** | Fonctions de bibliothèque. | `man 3 printf` |
| **4** | Fichiers spéciaux (périphériques). | `man 4 tty` |
| **5** | Formats de fichiers. | `man 5 passwd` |
| **6** | Jeux. | `man 6 tetris` |
| **7** | Divers. | `man 7 regex` |
| **8** | Administration système. | `man 8 iptables` |

💡 **Astuces** :
- `apropos mot` → cherche dans les pages de man.
- Dans `man` :
  - `/motif` → recherche.
  - `n` → occurrence suivante.
  - `q` → quitter.

---

## **⚡ Méta-caractères**
Les méta-caractères ont une **signification spéciale** pour le Shell.

| Méta-caractère | Description | Exemple |
|----------------|-------------|---------|
| `#` | Commentaire jusqu’à la fin de la ligne. | `# Ceci est un commentaire` |
| `\` | Désactive le caractère spécial qui suit. | `echo "C\'est"` → `C'est` |
| `'` | Quote simple (protège **tous** les caractères). | `echo '$HOME'` → `$HOME` (littéral) |
| `"` | Double quote (protège sauf `$`, `` ` ``, `\`). | `echo "Home: $HOME"` → `Home: /home/user` |
| `` ` `` | Anti-quote (exécute une commande). | `echo "Date: $(date)"` |
| `<`, `>`, `>>` | Redirection d’E/S. | `ls > fichiers.txt` |
| `;` | Enchaînement séquentiel. | `cd /tmp; ls` |
| `&` | Lancement en arrière-plan. | `sleep 10 &` |
| `|` | Pipeline (communication entre commandes). | `ls | grep ".txt"` |
| `$` | Substitution de variable. | `echo $USER` |
| `~`, `?`, `*`, `[ ]` | Génération de noms de fichiers (globbing). | `ls *.txt` |
| `Tab` | Complétion automatique (interactif). | `cd /ho` + `Tab` → `/home/` |

---

## **💬 Délimiteurs de Chaînes**

### **1. Quote simple (`'...'`)**
- **Protège tous les caractères** (aucun n’est interprété).
- **Seul caractère interdit** : `'` (ne peut pas être échappé).

```bash
echo 'Bonjour $USER'  # Affiche littéralement : Bonjour $USER
```

### **2. Double quote (`"..."`)**
- Protège tous les caractères **sauf** :
  - `$` (variables) → `echo "$USER"` → `jean`.
  - `` ` `` (substitution de commande) → `echo "Date: $(date)"`.
  - `\` (échappement) → `echo "Guillemet: \""` → `Guillemet: "`.

```bash
echo "Home: $HOME"  # Affiche : Home: /home/jean
```

### **3. Anti-quote (`` `...` `` ou `$(...)`)**
- **Exécute une commande** et substitue son résultat.
- **Forme moderne préférée** : `$(...)`.

```bash
echo "Il est $(date +%H:%M)"  # Affiche : Il est 14:30
```

> ✅ **Bonnes pratiques** :
> - Utilisez `'...'` pour **désactiver toute interprétation**.
> - Préférez `"..."` pour **autoriser les variables**.
> - Évitez les backticks `` `...` `` (peu lisibles, non imbriquables).

---

## **📁 Expansion des Noms de Fichiers (Globbing)**

### **1. Caractère `~` (Tilde)**
- `~` → Répertoire personnel (`$HOME`).
- `~login` → Répertoire personnel de l’utilisateur `login`.

```bash
cd ~          # Équivalent à : cd $HOME
ls ~root      # Liste le home de root (si autorisé)
```

### **2. Complétion (`Tab`)**
- Complète automatiquement :
  - Noms de **fichiers**.
  - **Variables** (`$HO` + `Tab` → `$HOME`).
  - **Commandes**.
  - **Utilisateurs** (`~je` + `Tab` → `~jean`).

### **3. Génération de noms (Globs)**
| Caractère | Description | Exemple |
|-----------|-------------|---------|
| `?` | **Un caractère** quelconque. | `ls fichier?.txt` → `fichier1.txt`, `fichierA.txt` |
| `*` | **Zéro ou plusieurs** caractères. | `ls *.txt` → Tous les fichiers `.txt` |
| `[abc]` | **Un caractère** dans l’ensemble. | `ls fichier[12].txt` → `fichier1.txt`, `fichier2.txt` |
| `[a-z]` | **Intervalle** de caractères. | `ls [a-z]*.sh` → Tous les scripts commençant par une minuscule. |
| `[!...]` | **Exclusion** (tous **sauf** les caractères listés). | `ls [!0-9]*.txt` → Fichiers `.txt` ne commençant **pas** par un chiffre. |

```bash
ls chapitre[12][01]  # Liste : chapitre10, chapitre11, chapitre20, chapitre21
rm *.bak             # Supprime tous les fichiers .bak
```

> ⚠️ **Piège** : Les globs sont **expansés par le Shell avant exécution**.
> Exemple : `rm *` → **DANGEREUX** si le répertoire contient des fichiers importants !

---

## **🔄 Redirection**
Les **flux standard** sont identifiés par des **descripteurs** :

| Descripteur | Nom | Valeur par défaut |
|-------------|-----|-------------------|
| **0** | Entrée standard (STDIN) | Clavier |
| **1** | Sortie standard (STDOUT) | Écran |
| **2** | Erreur standard (STDERR) | Écran |

### **📤 Opérateurs de Redirection**
| Opérateur | Description | Exemple |
|-----------|-------------|---------|
| `< fichier` | Redirige **STDIN** depuis un fichier. | `sort < noms.txt` |
| `> fichier` | Redirige **STDOUT** vers un fichier (**écrase**). | `ls > liste.txt` |
| `>> fichier` | Redirige **STDOUT** vers un fichier (**ajoute**). | `echo "Ligne" >> log.txt` |
| `2> fichier` | Redirige **STDERR** vers un fichier (**écrase**). | `grep "erreur" app.log 2> erreurs.txt` |
| `2>> fichier` | Redirige **STDERR** vers un fichier (**ajoute**). | `make 2>> erreurs.log` |
| `2>&1` | Redirige **STDERR** vers la **même cible que STDOUT**. | `ls /inexistant > sortie.txt 2>&1` |
| `&> fichier` | Redirige **STDOUT et STDERR** vers un fichier (Bash). | `commande &> log.txt` |

### **🔄 Exemples Pratiques**
```bash
# 1. Sauvegarder la sortie d'une commande
ls -l > liste_fichiers.txt

# 2. Ignorer les erreurs
grep "motif" fichier.txt 2> /dev/null

# 3. Fusionner STDOUT et STDERR dans un fichier
make 2>&1 > compilation.log

# 4. Ajouter à un fichier de log
echo "Nouvelle entrée" >> journal.log

# 5. Lire depuis un fichier
sort < données.txt
```

> ⚠️ **Piège** : L’ordre des redirections est **important** !
> - `commande > out 2>&1` ✅ (STDERR suit STDOUT vers `out`).
> - `commande 2>&1 > out` ❌ (STDERR suit l’**ancienne** STDOUT = écran).

---

## **🔗 Pipeline (`|`)**
Le pipeline (`|`) permet de **chaîner des commandes** en connectant la **STDOUT** de la première à la **STDIN** de la suivante.

### **🔄 Fonctionnement**
- Les commandes s’exécutent **en parallèle**.
- Le **kernel** gère la synchronisation via un **tube (pipe)**.
- **Pas de fichier temporaire** nécessaire.

### **📌 Commandes Typiques en Pipeline**
| Commande | Description | Exemple |
|----------|-------------|---------|
| `grep` | Filtre les lignes contenant un motif. | `ps aux | grep "nginx"` |
| `sort` | Trie les lignes. | `cat fichier.txt | sort` |
| `uniq` | Supprime les doublons **consécutifs**. | `sort fichier.txt | uniq` |
| `wc` | Compte les lignes/mots/caractères. | `cat fichier.txt | wc -l` |
| `cut` | Extrait des colonnes. | `ps aux | cut -d' ' -f1,11` |
| `awk` | Traitement avancé de texte. | `ls -l | awk '{print $9}'` |
| `sed` | Édition de flux. | `echo "hello" | sed 's/h/H/'` |

### **🔄 Exemples**
```bash
# 1. Compter le nombre de fichiers .txt
ls -l | grep "\.txt$" | wc -l

# 2. Trouver les 5 plus gros fichiers
du -ah | sort -rh | head -n 5

# 3. Extraire les IPs uniques d'un log
cat access.log | cut -d' ' -f1 | sort | uniq

# 4. Remplacer "foo" par "bar" dans un fichier
cat fichier.txt | sed 's/foo/bar/g' > fichier_modifié.txt
```

> 💡 **Astuce** : Utilisez `less` à la fin d’un pipeline pour un affichage paginé :
> `dmesg | grep "error" | less`

---

## **🔗 Enchaînement de Commandes**
| Opérateur | Description | Exemple |
|-----------|-------------|---------|
| `;` | **Séquentiel** : Exécute `cmd2` **après** `cmd1` (qu’importe le résultat). | `cd /tmp; ls` |
| `&&` | **ET logique** : Exécute `cmd2` **seulement si** `cmd1` réussit (`exit 0`). | `make && make install` |
| `\|\|` | **OU logique** : Exécute `cmd2` **seulement si** `cmd1` échoue (`exit ≠ 0`). | `cat fichier.txt || echo "Fichier introuvable"` |
| `&` | **Arrière-plan** : Lance `cmd` en arrière-plan. | `sleep 60 &` |
| `( ... )` | **Groupement** : Exécute les commandes dans un **sous-shell**. | `(cd /tmp && ls)` |

### **🔄 Exemples**
```bash
# 1. Compiler et installer si succès
make && make install

# 2. Essayer une commande, sinon afficher un message
rm fichier.txt || echo "Échec de la suppression"

# 3. Exécuter plusieurs commandes en arrière-plan
(sleep 10; echo "Fin") &

# 4. Groupement avec sous-shell
(cd /var/log && grep "error" syslog) > erreurs.txt
```

> ⚠️ **Piège** : Les modifications dans un sous-shell `( ... )` (comme `cd`) **ne persistent pas** après la parenthèse.

---

## **📦 Variables**

### **📌 Types de Variables**
| Type | Description | Exemple |
|------|-------------|---------|
| **Prédéfinies** | Variables du Shell (ex: `HOME`, `PATH`). | `echo $HOME` |
| **Utilisateur** | Définies par l’utilisateur. | `nom="Jean"` |
| **Spéciales** | Accès en lecture seule (ex: `$?`, `$$`). | `echo "PID: $$"` |
| **Positionnelles** | Paramètres du script (`$1`, `$2`, ...). | `echo "Premier arg: $1"` |

### **🔧 Affectation et Utilisation**
```bash
# 1. Affectation (pas d'espaces autour de '=' !)
fichier="exemple.txt"

# 2. Lecture
echo "Fichier: $fichier"

# 3. Suppression
unset fichier

# 4. Variables numériques (Bash)
declare -i compteur=0
compteur=$compteur+1  # Incrémentation
```

### **📜 Commandes Associées**
| Commande | Description | Exemple |
|----------|-------------|---------|
| `declare` / `typeset` | Déclare une variable avec des attributs. | `declare -r VAR="valeur"` (lecture seule) |
| `read` | Lit une entrée utilisateur. | `read -p "Nom: " nom` |
| `export` | Exporte une variable dans l’environnement. | `export PATH=$PATH:/usr/local/bin` |

### **🔢 Variables Spéciales**
| Variable | Description | Exemple |
|----------|-------------|---------|
| `$#` | Nombre d’arguments positionnels. | `echo "Args: $#"` |
| `$*` | Liste de tous les arguments. | `echo "Args: $*"` |
| `$?` | Code de retour de la dernière commande. | `grep "mot" fichier.txt; echo "Code: $?"` |
| `$$` | PID du Shell courant. | `echo "PID: $$"` |
| `$!` | PID du dernier processus en arrière-plan. | `sleep 10 &; echo "PID: $!"` |
| `$0` | Nom du script/commande. | `echo "Script: $0"` |
| `$1`, `$2`, ... | Arguments positionnels. | `echo "Premier arg: $1"` |

### **🔄 Exemple Complet**
```bash
#!/bin/bash
echo "Script: $0"
echo "Nombre d'args: $#"
echo "Liste des args: $*"

read -p "Entrez votre nom: " nom
echo "Bonjour, $nom !"

compteur=0
for arg in "$@"; do
  compteur=$((compteur + 1))
  echo "Arg $compteur: $arg"
done
```

> ✅ **Bonnes pratiques** :
> - **Quotez toujours les variables** : `"$var"` (évite les problèmes avec les espaces).
> - Utilisez `${var}` pour éviter les ambiguïtés : `${fichier}.txt`.
> - Préférez `$(...)` à `` `...` `` pour les substitutions de commandes.

---

## **⚖️ Commande `test` (ou `[ ... ]`)**
La commande `test` (ou sa forme abrégée `[ ... ]`) évalue une **condition** et retourne :
- **0** (vrai) si la condition est satisfaite.
- **1** (faux) sinon.

### **📁 Tests sur les Fichiers**
| Option | Description | Exemple |
|--------|-------------|---------|
| `-e fichier` | Le fichier **existe**. | `[ -e fichier.txt ]` |
| `-f fichier` | C’est un **fichier ordinaire**. | `[ -f /etc/passwd ]` |
| `-d fichier` | C’est un **répertoire**. | `[ -d /home ]` |
| `-r fichier` | Le fichier est **lisible**. | `[ -r fichier.txt ]` |
| `-w fichier` | Le fichier est **inscriptible**. | `[ -w fichier.txt ]` |
| `-x fichier` | Le fichier est **exécutable**. | `[ -x script.sh ]` |
| `-s fichier` | Le fichier **n’est pas vide**. | `[ -s fichier.txt ]` |

### **💬 Tests sur les Chaînes**
| Option | Description | Exemple |
|--------|-------------|---------|
| `-z str` | La chaîne est **vide**. | `[ -z "$var" ]` |
| `-n str` | La chaîne est **non vide**. | `[ -n "$var" ]` |
| `str1 = str2` | Les chaînes sont **égales**. | `[ "$a" = "$b" ]` |
| `str1 != str2` | Les chaînes sont **différentes**. | `[ "$a" != "$b" ]` |

### **🔢 Tests sur les Nombres**
| Option | Description | Exemple |
|--------|-------------|---------|
| `-eq` | **Égal**. | `[ $a -eq 5 ]` |
| `-ne` | **Différent**. | `[ $a -ne 5 ]` |
| `-lt` | **Inférieur**. | `[ $a -lt 10 ]` |
| `-gt` | **Supérieur**. | `[ $a -gt 10 ]` |
| `-le` | **Inférieur ou égal**. | `[ $a -le 10 ]` |
| `-ge` | **Supérieur ou égal**. | `[ $a -ge 10 ]` |

### **🔄 Combinaisons Logiques**
| Opérateur | Description | Exemple |
|-----------|-------------|---------|
| `!` | **Négation**. | `[ ! -f fichier.txt ]` |
| `-a` | **ET logique**. | `[ -f fichier.txt -a -r fichier.txt ]` |
| `-o` | **OU logique**. | `[ -f fichier.txt -o -d fichier.txt ]` |
| `\( ... \)` | **Groupement** (nécessite des échappements). | `[ \( -f a -a -r a \) -o \( -f b -a -r b \) ]` |

### **📌 Exemples**
```bash
# 1. Vérifier si un fichier existe et est lisible
if [ -f "$fichier" -a -r "$fichier" ]; then
  echo "Fichier $fichier est lisible."
fi

# 2. Tester si une variable est vide
if [ -z "$var" ]; then
  echo "La variable est vide."
fi

# 3. Comparer deux nombres
if [ "$a" -gt "$b" ]; then
  echo "a est supérieur à b."
fi

# 4. Combinaison complexe
if [ \( -f "$fichier" -a -r "$fichier" \) -o \( -d "$fichier" -a -x "$fichier" \) ]; then
  echo "Fichier ou répertoire accessible."
fi
```

> ⚠️ **Pièges** :
> - **Espaces obligatoires** autour des crochets et opérateurs : `[ -f fichier ]` ✅ vs `[-ffichier]` ❌.
> - **Quotez toujours les variables** : `[ -f "$fichier" ]` (évite les erreurs si `$fichier` est vide).

---

## **🧮 Commande `expr`**
`expr` évalue une **expression** et affiche le résultat sur **STDOUT**.
🔹 **Syntaxe** :
```bash
expr expression
```
🔹 **Utilisation** :
- **Arithmétique** (entiers).
- **Comparaisons** (numériques ou lexicographiques).

### **🔢 Opérateurs Arithmétiques**
| Opérateur | Description | Exemple |
|-----------|-------------|---------|
| `+` | Addition. | `expr 5 + 3` → `8` |
| `-` | Soustraction. | `expr 5 - 3` → `2` |
| `*` | Multiplication. | `expr 5 \* 3` → `15` (échappement obligatoire !) |
| `/` | Division (entière). | `expr 5 / 2` → `2` |
| `%` | Modulo (reste). | `expr 5 % 2` → `1` |

### **🔄 Opérateurs de Comparaison**
| Opérateur | Description | Résultat si vrai | Exemple |
|-----------|-------------|------------------|---------|
| `=` | Égalité (chaînes ou nombres). | `1` | `expr "$a" = "$b"` |
| `!=` | Différence. | `1` | `expr "$a" != "$b"` |
| `<` | Inférieur (lexicographique si chaînes). | `1` | `expr "$a" \< "$b"` (échappement obligatoire) |
| `>` | Supérieur. | `1` | `expr "$a" \> "$b"` |
| `<=` | Inférieur ou égal. | `1` | `expr "$a" \<= "$b"` |
| `>=` | Supérieur ou égal. | `1` | `expr "$a" \>= "$b"` |

### **📌 Exemples**
```bash
# 1. Calcul arithmétique
x=5
y=$(expr $x + 4)  # y = 9

# 2. Multiplication (échappement obligatoire)
z=$(expr $x \* $y)  # z = 45

# 3. Comparaison numérique
resultat=$(expr $x \> 4)  # resultat = 1 (vrai)

# 4. Comparaison de chaînes
egal=$(expr "$a" = "$b")  # egal = 1 si a == b

# 5. Longueur d'une chaîne
longueur=$(expr length "$chaine")
```

> ⚠️ **Pièges** :
> - **Échappement obligatoire** pour `*`, `<`, `>`, etc. : `expr 5 \* 3`.
> - **Espaces obligatoires** entre opérateurs et opérandes.
> - **Alternatives modernes** (Bash) :
>   - Arithmétique : `(( x = 5 + 3 ))` ou `let "x=5+3"`.
>   - Comparaisons : `[ $a -gt $b ]` ou `(( a > b ))`.

---

## **🔄 Structures de Contrôle**

### **1. `if` (Conditionnelle)**
```bash
if listeDeCommandes1
then
    listeDeCommandes2
elif listeDeCommandes3
then
    listeDeCommandes4
else
    listeDeCommandes5
fi
```
- **`listeDeCommandes1`** : Condition (son **code de retour** détermine la branche).
  - `0` → **vrai** (exécute `then`).
  - `≠ 0` → **faux** (passe à `elif`/`else`).
- **`then`/`elif`/`else`/`fi`** : Mots-clés **obligatoires**.

#### **📌 Exemple**
```bash
if [ $# -eq 0 ]; then
    echo "Erreur: Aucun argument fourni." >&2
    echo "Usage: $0 fichier" >&2
    exit 1
elif [ ! -f "$1" ]; then
    echo "Erreur: $1 n'est pas un fichier." >&2
    exit 1
else
    echo "Traitement de $1..."
    # ... traitement ...
fi
```

---

### **2. `case` (Sélection Multiple)**
```bash
case valeur in
    motif1)
        listeDeCommandes1 ;;
    motif2)
        listeDeCommandes2 ;;
    *)
        listeDeCommandesParDefaut ;;
esac
```
- **`valeur`** : Variable ou expression à tester.
- **`motif`** : Peut utiliser `*`, `?`, `[...]` (comme le globbing).
- **`;;`** : Termine chaque cas (obligatoire).
- **`*`** : Cas par défaut (comme `default` en C).

#### **📌 Exemple**
```bash
read -p "Voulez-vous continuer ? [o/O/n/N] " reponse
case "$reponse" in
    [oO]*)
        echo "Continuons !"
        ;;
    [nN]*)
        echo "Annulé."
        exit 0
        ;;
    *)
        echo "Réponse invalide."
        exit 1
        ;;
esac
```

> 💡 **Astuce** : Utilisez `case` pour remplacer des `if` imbriqués complexes (plus lisible).

---

## **🔁 Boucles**

### **1. `for` (Itération)**
```bash
for variable [in liste]
do
    listeDeCommandes
done
```
- **`liste`** : Liste de mots (globs, variables, résultats de commandes).
  - Si omise : itère sur **les arguments positionnels** (`$@`).
- **`variable`** : Prend successivement chaque valeur de la liste.

#### **📌 Exemples**
```bash
# 1. Itérer sur une liste explicite
for fruit in pomme banane orange
do
    echo "J'aime les $fruit"
done

# 2. Itérer sur les arguments du script
for arg in "$@"
do
    echo "Argument: $arg"
done

# 3. Itérer sur les fichiers .txt
for fichier in *.txt
do
    echo "Traitement de $fichier"
    wc -l "$fichier"
done
```

---

### **2. `while` (Tant que)**
```bash
while listeDeCommandes1
do
    listeDeCommandes2
done
```
- **`listeDeCommandes1`** : Condition (exécutée **avant chaque itération**).
  - Si **code de retour = 0** → exécute `listeDeCommandes2`.
  - Sinon → sort de la boucle.

#### **📌 Exemple**
```bash
# Lire jusqu'à ce que l'entrée soit "quit"
read -p "Entrez un mot (ou 'quit') : " mot
while [ "$mot" != "quit" ]
do
    echo "Vous avez entré: $mot"
    read -p "Entrez un mot (ou 'quit') : " mot
done
```

---

### **3. `until` (Jusqu’à)**
```bash
until listeDeCommandes1
do
    listeDeCommandes2
done
```
- **Inverse de `while`** :
  - Exécute `listeDeCommandes2` **tant que** `listeDeCommandes1` **échoue** (`≠ 0`).
  - S’arrête quand `listeDeCommandes1` **réussit** (`= 0`).

#### **📌 Exemple**
```bash
# Attendre qu'un fichier existe
until [ -f "/tmp/ready" ]
do
    echo "En attente de /tmp/ready..."
    sleep 1
done
echo "Fichier trouvé !"
```

---

## **⚙️ Commande Interne `set`**
`set` permet de :
1. **Afficher/modifier les variables** du Shell.
2. **Positionner les options** du Shell.
3. **Réinitialiser les arguments positionnels** (`$1`, `$2`, ...).

### **📌 Syntaxe**
```bash
set [options] [-o mot] [argument...]
```

### **🔧 Options Utiles**
| Option | Description |
|--------|-------------|
| `-b` | Affiche immédiatement l’état des jobs en arrière-plan. |
| `-u` | Considère l’utilisation de **variables non définies** comme une **erreur**. |
| `-x` | Affiche chaque commande **avant exécution** (débogage). |
| `--` | Réinitialise les **arguments positionnels** (`$1`, `$2`, ...). |

### **📌 Exemples**
```bash
# 1. Afficher toutes les variables
set

# 2. Activer le mode "erreur sur variable non définie"
set -u
echo "$var_non_definie"  # Erreur !

# 3. Désactiver une option
set +u

# 4. Positionner les arguments positionnels
set un deux trois
echo "$1 $2 $3"  # Affiche : un deux trois

# 5. Utiliser avec une commande (découpage selon IFS)
set $(date)
echo "Aujourd'hui : $2 $3 $6"  # Ex: "Aujourd'hui : 10 oct. 2025"
```

> 💡 **Astuce** :
> - `set -x` est utile pour **déboguer un script** (affiche chaque commande avant exécution).
> - `set --` efface les arguments positionnels (utile pour réinitialiser `$1`, `$2`, ...).

---

## **⚠️ Pièges & Bonnes Pratiques**

### **🚨 Pièges Courants**
| Piège | Explication | Solution |
|-------|-------------|----------|
| **Espaces autour de `=`** | `var = valeur` ❌ → Le Shell interprète `=` comme une commande. | `var=valeur` ✅ |
| **Variables non quotées** | `rm $fichier` ❌ → Problème si `$fichier` contient des espaces. | `rm "$fichier"` ✅ |
| **Oublier les quotes dans `[ ]`** | `[ -f $fichier ]` ❌ → Échec si `$fichier` est vide. | `[ -f "$fichier" ]` ✅ |
| **Mauvais ordre de redirection** | `commande 2>&1 > fichier` ❌ → STDERR pointe vers l’écran. | `commande > fichier 2>&1` ✅ |
| **Backticks vs `$(...)`** | `` `ls` `` ❌ → Peu lisible, non imbriquable. | `$(ls)` ✅ |
| **Sous-shell `(...)`** | Les modifications (ex: `cd`) **ne persistent pas** après la parenthèse. | Utiliser `{ ...; }` pour un groupement **sans sous-shell** (attention aux espaces et `;` final). |
| **`[` vs `[[`** | `[` est une commande externe (moins robuste), `[[` est un mot-clé Bash (plus sûr). | Préférez `[[ -f "$fichier" ]]` en Bash. |
| **`expr` obsolète** | `expr` est sensible aux espaces et nécessite des échappements. | Utiliser `(( ... ))` ou `let` en Bash : `(( x = 5 + 3 ))`. |
| **Pipeline et échecs** | Par défaut, un pipeline (`cmd1 | cmd2`) ne renvoie que le code de `cmd2`. | Activez `set -o pipefail` pour détecter les échecs dans le pipeline. |
| **Variables non exportées** | Une variable locale n’est pas accessible dans un sous-shell. | Utiliser `export VAR=valeur` pour la rendre disponible. |
| **`if [ "$var" = "val" ]`** | Échec si `$var` est vide (syntaxe invalide). | Toujours quoter : `[ "$var" = "val" ]`. |

---

### **🔧 Bonnes Pratiques**
1. **Quotez systématiquement les variables** :
   ```bash
   # ❌ Risqué (éclatement si espaces, globbing si *)
   rm $fichier

   # ✅ Sécurisé
   rm -- "$fichier"
   ```

2. **Utilisez `$(...)` au lieu de backticks** :
   ```bash
   # ❌ Peu lisible
   echo `ls`

   # ✅ Lisible et imbriquable
   echo "$(ls)"
   ```

3. **Préférez `[[ ... ]]` à `[ ... ]`** (Bash) :
   ```bash
   # ✅ Plus robuste (pas de problème avec <, >, &&, etc.)
   if [[ -f "$fichier" && -r "$fichier" ]]; then
       ...
   fi
   ```

4. **Gérez les erreurs** :
   ```bash
   # Arrêter le script en cas d'erreur
   set -e

   # Détecter les variables non définies
   set -u

   # Afficher les commandes exécutées (débogage)
   set -x
   ```

5. **Utilisez `read -r` pour éviter l’interprétation des `\`** :
   ```bash
   read -r ligne  # Préserve les backslashes
   ```

6. **Pour les boucles sur des fichiers** :
   ```bash
   # ✅ Gère les noms avec espaces ou caractères spéciaux
   for fichier in *; do
       [ -e "$fichier" ] || continue  # Évite les problèmes si aucun fichier
       command "$fichier"
   done
   ```

7. **Redirections sécurisées** :
   ```bash
   # Écrase le fichier uniquement si la commande réussit
   commande > fichier || { rm -f fichier; exit 1; }
   ```

8. **Utilisez `printf` au lieu de `echo`** (plus portable) :
   ```bash
   printf '%s\n' "Bonjour $nom"  # Pas de problème avec -e, -n, etc.
   ```

---

## **📝 Mini-Aide-Mémoire**

### **📁 Globbing (Expansion de fichiers)**
| Motif | Description | Exemple |
|-------|-------------|---------|
| `*` | 0 ou plusieurs caractères | `*.txt` |
| `?` | 1 caractère | `fichier?.log` |
| `[abc]` | 1 caractère parmi `a`, `b`, `c` | `[aeiou]*` |
| `[a-z]` | 1 caractère dans l’intervalle | `[0-9][0-9].txt` |
| `[!...]` | 1 caractère **non** dans la liste | `[!0-9]*` |

### **🔄 Redirections**
| Syntaxe | Description |
|---------|-------------|
| `> fichier` | Redirige STDOUT (écrase) |
| `>> fichier` | Redirige STDOUT (ajoute) |
| `2> fichier` | Redirige STDERR (écrase) |
| `2>> fichier` | Redirige STDERR (ajoute) |
| `&> fichier` | Redirige STDOUT **et** STDERR (Bash) |
| `2>&1` | Redirige STDERR vers STDOUT |
| `< fichier` | Redirige STDIN depuis un fichier |
| `<< EOF` | Here-document (entrée multiline) |

### **🔗 Enchaînements**
| Opérateur | Description |
|-----------|-------------|
| `;` | Exécute séquentiellement |
| `&&` | Exécute la 2ème commande **si la 1ère réussit** |
| `\|\|` | Exécute la 2ème commande **si la 1ère échoue** |
| `&` | Lance en arrière-plan |
| `( ... )` | Groupe dans un sous-shell |
| `{ ...; }` | Groupe **sans sous-shell** (attention aux espaces) |

### **📦 Variables Spéciales**
| Variable | Description |
|----------|-------------|
| `$#` | Nombre d’arguments |
| `$*` | Tous les arguments (en une seule chaîne) |
| `$@` | Tous les arguments (tableau) |
| `$?` | Code de retour de la dernière commande |
| `$$` | PID du Shell courant |
| `$!` | PID du dernier processus en arrière-plan |
| `$0` | Nom du script |
| `$1`, `$2`, ... | Arguments positionnels |

### **⚖️ Tests (`test` ou `[ ... ]`)**
| Test | Description |
|------|-------------|
| `-e fichier` | Fichier existe |
| `-f fichier` | Fichier ordinaire |
| `-d fichier` | Répertoire |
| `-r fichier` | Lisible |
| `-w fichier` | Inscriptible |
| `-x fichier` | Exécutable |
| `-z "$var"` | Chaîne vide |
| `-n "$var"` | Chaîne non vide |
| `$a -eq $b` | Nombres égaux |
| `$a -lt $b` | `$a` < `$b` |

### **🔁 Boucles**
```bash
# for
for var in liste; do
    commandes
done

# while
while condition; do
    commandes
done

# until
until condition; do
    commandes
done
```

### **🧮 Arithmétique (Bash)**
```bash
# Méthode 1 : Double parenthèses
(( x = 5 + 3 ))

# Méthode 2 : let
let "x = 5 + 3"

# Méthode 3 : declare -i
declare -i x=5+3
```

### **📌 Commandes Utiles**
| Commande | Description |
|----------|-------------|
| `man [section] cmd` | Affiche le manuel |
| `apropos motif` | Cherche dans les pages de man |
| `type cmd` | Affiche le type de la commande (interne, externe, alias) |
| `which cmd` | Localise la commande dans `$PATH` |
| `history` | Affiche l’historique des commandes |
| `alias` | Liste les alias |
| `unset var` | Supprime une variable |
| `export var=val` | Exporte une variable dans l’environnement |
| `read var` | Lit une entrée utilisateur |
| `set` | Affiche/modifie les options du Shell |
| `trap` | Gère les signaux (ex: `trap "exit" INT`) |

---

## **🎯 Exemple Complet : Script Robuste**
```bash
#!/bin/bash
set -euo pipefail  # Mode strict : erreur sur variable non définie, pipeline, etc.

usage() {
    echo "Usage: $0 [-f fichier] [-v]" >&2
    exit 1
}

# Parsing des arguments
while getopts ":f:v" opt; do
    case "$opt" in
        f) fichier="$OPTARG" ;;
        v) verbose=true ;;
        \?) echo "Option invalide: -$OPTARG" >&2; usage ;;
        :) echo "Option -$OPTARG nécessite un argument" >&2; usage ;;
    esac
done

# Vérifications
if [[ -z "${fichier:-}" ]]; then
    echo "Erreur: Aucun fichier spécifié." >&2
    usage
fi

if [[ ! -f "$fichier" ]]; then
    echo "Erreur: $fichier n'existe pas ou n'est pas un fichier." >&2
    exit 1
fi

# Traitement
if [[ "$verbose" = true ]]; then
    echo "Traitement de $fichier..." >&2
fi

wc -l "$fichier" | awk '{print $1}' || {
    echo "Erreur lors du traitement de $fichier" >&2
    exit 1
}
```

---

## **📚 Pour Aller Plus Loin**
1. **Lecture recommandée** :
   - *The Linux Command Line* (William Shotts)
   - *Bash Guide for Beginners* (Machtelt Garrels)
   - `man bash` (documentation complète)

2. **Outils complémentaires** :
   - **`awk`** : Traitement avancé de texte.
   - **`sed`** : Édition de flux.
   - **`find`** : Recherche de fichiers avec conditions.
   - **`xargs`** : Construction et exécution de commandes.

3. **Exercices pratiques** :
   - Écrire un script qui renomme tous les fichiers `.txt` en `.bak`.
   - Créer un script qui vérifie si un utilisateur existe (`/etc/passwd`).
   - Automatiser la sauvegarde d’un répertoire avec `tar` et une date dans le nom.
   - Parser un fichier CSV avec `awk`.

4. **Débogage** :
   - `set -x` : Active le mode trace.
   - `trap 'echo "Ligne $LINENO" >&2' ERR` : Affiche la ligne en cas d’erreur.
   - `shellcheck script.sh` : Vérifie les bonnes pratiques (outil externe).

---

## **💡 Encadré Pédagogique : Quand Utiliser le Shell ?**
| **À faire en Shell** ✅ | **À éviter en Shell** ❌ |
|------------------------|-------------------------|
| Automatisation de tâches système (`cron`, logs, sauvegardes). | Traitements complexes (utilisez Python/Perl). |
| Manipulation de fichiers/textes (filtres, `grep`, `sed`). | Calculs mathématiques intensifs. |
| Enchaînement de commandes (`|`, `&&`, `||`). | Gestion de structures de données complexes. |
| Scripts courts et portables. | Applications avec interface utilisateur. |
| Prototypage rapide. | Code critique (préférez un langage compilé). |

---

## **🎉 Conclusion**
Le Shell est un outil **puissant** mais **exigeant** :
- **Syntaxe stricte** (espaces, quotes, etc.).
- **Philosophie Unix** : une commande = une tâche simple, combinable via pipes.
- **Automatisation** : idéale pour les tâches répétitives.

🔹 **Pour maîtriser le Shell** :
1. **Pratiquez** avec des petits scripts.
2. **Lisez du code** (scripts système dans `/etc/init.d`, `/usr/bin`).
3. **Utilisez `man` et `tldr`** pour découvrir des commandes.
4. **Adoptez les bonnes pratiques** (quoting, `set -euo pipefail`).

---
**🚀 Prochaine étape** :
- Apprendre `awk`/`sed` pour le traitement de texte avancé.
- Explorer `find` + `xargs` pour la gestion de fichiers.
- Découvrir `tmux` pour multiplexer les terminaux.

---
**❓ Questions fréquentes** :
- **"Pourquoi mon script ne fonctionne pas ?"**
  → Vérifiez les permissions (`chmod +x script.sh`), le shebang (`#!/bin/bash`), et activez le mode débogage (`set -x`).

- **"Comment gérer les espaces dans les noms de fichiers ?"**
  → **Quotez toujours** : `"$fichier"`, et utilisez `find -print0` + `xargs -0` pour les noms complexes.

- **"Comment faire une boucle sur les arguments ?"**
  → `for arg in "$@"; do ... done` (preserve les arguments avec espaces).

---
**📌 Cheat Sheet à Imprimer** :
[Lien vers un template PDF](#) (à générer avec les tableaux ci-dessus).

---

[...retorn en rèire](../menu.md)
