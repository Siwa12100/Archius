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
  - [`linux`](docs/sysadmin/linux/)
  - [`vps`](docs/sysadmin/vps/) → [menu](docs/sysadmin/vps/menu.md)
- **DevOps**
  - [`docker`](docs/devops/docker/) → [sommaire](docs/devops/docker/sommaire.md)
  - [`ci-cd`](docs/devops/ci-cd/) → [menu](docs/devops/ci-cd/menu.md)
  - [`stockage`](docs/devops/stockage/)
- **Langages & outils**
  - [`cpp`](docs/langages/cpp/)
  - [`html-css`](docs/langages/html-css/)
  - [`symfony`](docs/langages/symfony/)
  - [`vue2`](docs/langages/vue2/)
- **Tutoriels**
  - [`tutoriels`](docs/tutoriels/)

### 🎓 cours/ – BUT Informatique

#### Semestre 3–4
- [`php`](cours/semestre3-4/php/)
- [`programmation-système`](cours/semestre3-4/programmation-systeme/)
- [`dotnet`](cours/semestre3-4/dotnet/)
- [`java`](cours/semestre3-4/java/)
- [`vue`](cours/semestre3-4/vue/)
- [`services-web`](cours/semestre3-4/services-web/)  
- [`sae-reseau`](cours/semestre3-4/sae-reseau/)

#### Semestre 5–6
- [`qualite-tests`](cours/semestre5-6/qualite-tests/)
- [`droit`](cours/semestre5-6/droit/)
- [`bdd-paradigmes`](cours/semestre5-6/bdd-paradigmes/)
- [`javascript`](cours/semestre5-6/javascript/)
- [`python`](cours/semestre5-6/python/)
- [`angular`](cours/semestre5-6/angular/)
- [`mongo`](cours/semestre5-6/mongo/)
- [`securite`](cours/semestre5-6/securite/)
- [`ioa`](cours/semestre5-6/ioa/)
- [`kotlin`](cours/semestre5-6/kotlin/)
- [`c`](cours/semestre5-6/c/)
- [`ppp`](cours/semestre5-6/ppp/)

### 🛠️ projets/
- [`minecraft`](projets/minecraft/) → Valorium, Elendil, etc.
- [`occitan`](projets/occitan/) → fiches, lexiques, entraînements, scripts, etc.

### ✍️ brouillons/
- [`root-temp`](brouillons/root-temp/)
- [`src-temp`](brouillons/src-temp/)
- [`src-temporaire`](brouillons/src-temporaire/)

### 🗄️ archives/
- [`semestre3-4`](archives/semestre3-4/)
- [`anciens_projets`](archives/anciens_projets/)

---

## 🔗 Anciennes pages d’accueil

- Ancien **menu VPS** : [`docs/sysadmin/vps/menu.md`](docs/sysadmin/vps/menu.md)  
- Ancien **archives.md** : [`archives.md`](archives.md) (conservé, mais liens mis à jour)

---

## 📝 Philosophie & avertissement

Aquelas archius son sobretot personals… (texte original conservé).  
Il peut subsister **des erreurs** dans certaines notes techniques. Gardez un **regard critique**.

---

## ✅ Contribuer (moi-même, futur moi)

- Créer systématiquement un `menu.md` / `sommaire.md` par dossier
- Utiliser des **liens relatifs** et des **titres explicites**
- Ranger :
  - en **`docs/`** ce qui est générique / intemporel,
  - en **`cours/`** ce qui est lié à la formation,
  - en **`projets/`** ce qui est concret et personnel,
  - en **`brouillons/`** ce qui n’est pas trié,
  - en **`archives/`** ce qui est gelé.

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

---

## 📌 À approfondir

- Docker (playlist)
- Bot Discord (playlist)
- JUnit & xUnit (docs officiels)
