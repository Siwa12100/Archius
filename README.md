# Archius

> Banque de connaissances personnelle — notes de cours, wiki technique, projets et brouillons.  
> *Occitan & DevOps friendly 💛*

Archius est mon **second cerveau versionné** : j’y range mes notes, documentations, TP/DM, et réflexions.  
Le repo est organisé pour **toujours savoir où ranger quoi** :

- **`docs/`** – Wiki technique personnel (procédures, how-to, langages, DevOps)
- **`cours/`** – Notes liées au BUT (classées par semestre et matière)
- **`projets/`** – Projets personnels (Minecraft/Valorium, Occitan, etc.)
- **`brouillons/`** – Zone tampon pour ce qui n’est pas trié
- **`archives/`** – Vieux contenus gelés

---

## 🧭 Plan rapide

### 📚 docs/ – Wiki technique
- **Sysadmin**
  - [`linux`](docs/sysadmin/linux/) — (bash/commandes/tp)
  - [`vps`](docs/sysadmin/vps/menu.md) ← *menu*
- **DevOps**
  - [`docker`](docs/devops/docker/sommaire.md) ← *sommaire*
  - [`ci-cd`](docs/devops/ci-cd/menu.md) ← *menu*
  - [`stockage → services Web`](docs/devops/stockage/servicesWeb/sommaire.md) ← *sommaire*
- **Langages & outils**
  - [`cpp`](docs/langages/cpp/menu.md) ← *menu*
  - [`html-css → formulaires`](docs/langages/html-css/fichiers/formulaires.md)
  - [`symfony`](docs/langages/symfony/menu.md) ← *menu*
  - [`vue 2`](docs/langages/vue2/menu.md) ← *menu*
- **Tutoriels**
  - [`tutoriels`](docs/tutoriels/tutoriels/menu.md) ← *menu*

### 🎓 cours/ – BUT Informatique

#### Semestre 3–4
- [`php`](cours/semestre3-4/php/intro.md) ← *intro*
- [`programmation-système`](cours/semestre3-4/programmation-systeme/intro.md) ← *intro*
- [`dotnet`](cours/semestre3-4/dotnet/intro.md) ← *intro*
- [`java`](cours/semestre3-4/java/sommaire.md) ← *sommaire*
- [`vue`](cours/semestre3-4/vue/sommaire.md) ← *sommaire*
- [`services web`](cours/semestre3-4/services-web/sommaire.md) ← *sommaire*
- [`sae réseau`](cours/semestre3-4/sae-reseau/notes1.md)

#### Semestre 5–6
- [`qualité/tests`](cours/semestre5-6/qualite-tests/menu.md) ← *menu*
- [`droit`](cours/semestre5-6/droit/menu.md) ← *menu*
- [`BDD — nouveaux paradigmes`](cours/semestre5-6/bdd-paradigmes/menu.md) ← *menu*
- [`javascript`](cours/semestre5-6/javascript/sommaire.md) ← *sommaire*
- [`python`](cours/semestre5-6/python/menu.md) ← *menu*
- [`angular`](cours/semestre5-6/angular/menu.md) ← *menu*
- [`mongo`](cours/semestre5-6/mongo/menu.md) ← *menu*
- [`sécurité`](cours/semestre5-6/securite/) *(pas de menu : dossier)*
- [`IOA`](cours/semestre5-6/ioa/menu.md) ← *menu*
- [`kotlin`](cours/semestre5-6/kotlin/sommaire.md) ← *sommaire*
- [`langage C`](cours/semestre5-6/c/menu.md) ← *menu*
- [`PPP`](cours/semestre5-6/ppp/menu.md) ← *menu*

### 🛠️ projets/
- **Minecraft**
  - [`Valorium — accueil`](projets/minecraft/valorium/accueil.md)
  - [`Elendil — Ligue`](projets/minecraft/Elendil/Ligue.md)
  - [`Display Entities — menu`](projets/minecraft/display-entities/menu.md)
  - [`Réflexions — intro`](projets/minecraft/reflexions/intro.md)
- **Occitan**
  - [`Menu fiches`](projets/occitan/menu.md)
  - [`Menu (lexique al canton)`](projets/occitan/lexiqueAlCanton/sommaire.md)
  - [`Menu (mots courants random)`](projets/occitan/mots_courants_random/menu.md)

### ✍️ brouillons/
- [`root-temp`](brouillons/root-temp/)
- [`src-temp`](brouillons/src-temp/)
- [`src-temporaire`](brouillons/src-temporaire/)

### 🗄️ archives/
- [`archives.md`](archives.md) — index d’archives
- [`dossier archives/`](archives/) — contenus gelés

---

## 📝 Philosophie & avertissement

Aquelas archius son sobretot personals… (texte original conservé).  
Il peut subsister **des erreurs** dans certaines notes techniques. Gardez un **regard critique**.

---

## 🧰 Scripts utiles

- **Réorganisation** : `./scripts/reorg_archius.sh`  
- **Réécriture des liens** : `./scripts/rewire_links.sh`

> Après exécution :
> ```bash
> git status
> git add -A
> git commit -m "refactor(structure): réorg + liens"
> ```
