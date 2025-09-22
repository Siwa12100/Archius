#!/usr/bin/env bash
set -euo pipefail

# À lancer depuis la racine du dépôt
# but : réorganiser Archius en {docs/, cours/, projets/, brouillons/, archives/}

mkdir -p scripts
mkdir -p docs/sysadmin docs/devops docs/langages docs/tutoriels
mkdir -p cours/semestre3-4 cours/semestre5-6
mkdir -p projets
mkdir -p brouillons
mkdir -p archives/semestre3-4 archives/anciens_projets

mv_if_exists() {
  local src="$1"; local dst="$2"
  if [ -e "$src" ]; then
    mkdir -p "$(dirname "$dst")"
    git mv "$src" "$dst"
    echo "✔ Moved: $src  ->  $dst"
  else
    echo "… Skipped (missing): $src"
  fi
}

# ---- DOCS (wiki technique) ----
mv_if_exists src/vps                      docs/sysadmin/vps
mv_if_exists src/linux                    docs/sysadmin/linux

mv_if_exists src/docker                   docs/devops/docker
mv_if_exists src/ci                       docs/devops/ci-cd
mv_if_exists src/stockageCode             docs/devops/stockage

# Choix : C++ en docs (peu dépendant du semestre)
mv_if_exists src/cpp                      docs/langages/cpp
# HTML/CSS renommé pour lisibilité
mv_if_exists src/htmlCss                  docs/langages/html-css
# Symfony renommé "symfony" (cohérence)
mv_if_exists src/symphonie                docs/langages/symfony
# Vue 2 peut rester à part
mv_if_exists src/vue2                     docs/langages/vue2
# (optionnel) Services Web peut être un cours S3-4 -> on le met en cours
# mv_if_exists src/servicesWeb            docs/langages/services-web

# ---- COURS (BUT) ----
# Semestre 3-4 (selon ton archives.md)
mv_if_exists src/php                      cours/semestre3-4/php
mv_if_exists src/programmationSysteme     cours/semestre3-4/programmation-systeme
mv_if_exists src/dotnet                   cours/semestre3-4/dotnet
mv_if_exists src/java                     cours/semestre3-4/java
mv_if_exists src/vue                      cours/semestre3-4/vue
mv_if_exists src/servicesWeb              cours/semestre3-4/services-web
mv_if_exists src/saeReseau                cours/semestre3-4/sae-reseau

# Semestre 5-6
mv_if_exists src/tests                    cours/semestre5-6/qualite-tests
mv_if_exists src/droit                    cours/semestre5-6/droit
mv_if_exists src/nouveaux_parag_bdd       cours/semestre5-6/bdd-paradigmes
mv_if_exists src/javascript               cours/semestre5-6/javascript
mv_if_exists src/python                   cours/semestre5-6/python
mv_if_exists src/angular                  cours/semestre5-6/angular
mv_if_exists src/mongo                    cours/semestre5-6/mongo
mv_if_exists src/securite                 cours/semestre5-6/securite
mv_if_exists src/ioa                      cours/semestre5-6/ioa
mv_if_exists src/kotlin                   cours/semestre5-6/kotlin
mv_if_exists src/prog_c                   cours/semestre5-6/c

# PPP : typiquement côté BUT
mv_if_exists src/ppp                      cours/semestre5-6/ppp

# ---- PROJETS ----
# Minecraft & Valorium = projets
mv_if_exists src/mc                       projets/minecraft
# Apprentissage occitan = projet perso "langue/occitan"
mv_if_exists src/oc                       projets/occitan
# Tutoriels génériques -> docs
mv_if_exists src/tutoriels                docs/tutoriels

# ---- BROUILLONS ----
mv_if_exists src/temp                     brouillons/src-temp
mv_if_exists src/temporaire               brouillons/src-temporaire
mv_if_exists temp                         brouillons/root-temp

# ---- FICHIERS RACINE ----
# on conserve README.md et archives.md à la racine
echo "✅ Réorganisation des dossiers terminée."
