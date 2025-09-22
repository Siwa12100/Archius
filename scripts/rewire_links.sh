#!/usr/bin/env bash
set -euo pipefail

JOBS="${JOBS:-$(command -v nproc >/dev/null 2>&1 && nproc || echo 4)}"
DRY_RUN="${DRY_RUN:-0}"         # 1 = prévisualiser sans écrire
SHOW_UNCHANGED="${SHOW_UNCHANGED:-0}"

# --- Mapping des chemins (ancien -> nouveau) ---
read -r -d '' MAPPING <<'MAP'
src/vps|docs/sysadmin/vps
src/linux|docs/sysadmin/linux
src/docker|docs/devops/docker
src/ci|docs/devops/ci-cd
src/stockageCode|docs/devops/stockage
src/cpp|docs/langages/cpp
src/htmlCss|docs/langages/html-css
src/symphonie|docs/langages/symfony
src/vue2|docs/langages/vue2
src/php|cours/semestre3-4/php
src/programmationSysteme|cours/semestre3-4/programmation-systeme
src/dotnet|cours/semestre3-4/dotnet
src/java|cours/semestre3-4/java
src/vue|cours/semestre3-4/vue
src/servicesWeb|cours/semestre3-4/services-web
src/saeReseau|cours/semestre3-4/sae-reseau
src/tests|cours/semestre5-6/qualite-tests
src/droit|cours/semestre5-6/droit
src/nouveaux_parag_bdd|cours/semestre5-6/bdd-paradigmes
src/javascript|cours/semestre5-6/javascript
src/python|cours/semestre5-6/python
src/angular|cours/semestre5-6/angular
src/mongo|cours/semestre5-6/mongo
src/securite|cours/semestre5-6/securite
src/ioa|cours/semestre5-6/ioa
src/kotlin|cours/semestre5-6/kotlin
src/prog_c|cours/semestre5-6/c
src/mc|projets/minecraft
src/oc|projets/occitan
src/tutoriels|docs/tutoriels
src/temp|brouillons/src-temp
src/temporaire|brouillons/src-temporaire
temp/|brouillons/root-temp/
MAP

# --- fichiers ciblés : uniquement md suivis par git ---
mapfile -t FILES < <(git ls-files '*.md' | LC_ALL=C sort)
TOTAL="${#FILES[@]}"
[ "$TOTAL" -eq 0 ] && { echo "Aucun fichier .md suivi par git."; exit 0; }

# --- Génère le programme perl pour le mapping (ancien -> nouveau) ---
perl_prog_mapping() {
  while IFS='|' read -r FROM TO; do
    [ -z "$FROM" ] && continue
    cat <<EOF
# Inline: [txt](${FROM}/...) et [txt](${FROM})
s{(\\]\\()\\Q${FROM}\\E/}{\$1${TO}/}g;
s{(\\]\\()\\Q${FROM}\\E(\\))}{\$1${TO}\$2}g;
# Réf: [txt]: ${FROM}/... et [txt]: ${FROM}
s{(\\]:\\s*)\\Q${FROM}\\E/}{\$1${TO}/}g;
s{(\\]:\\s*)\\Q${FROM}\\E\\s*\$}{\$1${TO}}mg;
EOF
  done <<< "$MAPPING"
}

# --- Calcule le préfixe relatif vers la racine (ex: "../../../") ---
prefix_to_root() {
  local path="$1"
  local dir="${path%/*}"
  [ "$dir" = "$path" ] && dir="."         # si le fichier est à la racine
  [ "$dir" = "." ] && { printf ""; return; }
  # compte les segments non vides
  IFS='/' read -r -a seg <<< "$dir"
  local n=0 s
  for s in "${seg[@]}"; do
    [ -n "$s" ] && n=$((n+1))
  done
  printf '%0.s../' $(seq 1 "$n")
}

# --- Traite un fichier : mapping + correction de profondeur (README/archives) ---
process_one() {
  local f="$1"
  [ -f "$f" ] || { echo "skip (not file): $f"; return; }

  local root_prefix
  root_prefix="$(prefix_to_root "$f")"

  # programme perl additionnel pour corriger les liens vers la racine
  # - remplace n'importe quel pattern ../*/README.md par le bon $root_prefix/README.md
  # - idem pour archives.md
  local perl_root_fix
  perl_root_fix=$(cat <<EOF
# Normalise les ancres/queries
s{(\\]\\([^\\)#?]+)\\#[^)]+}{\$1}g;

# README.md
s{(\\]\\()((?:\\./|\\.\\./)*)(README\\.md\\))}{\$1${root_prefix}README.md}g;
s{(\\]:\\s*)((?:\\./|\\.\\./)*)README\\.md\\s*\$}{\$1${root_prefix}README.md}mg;

# archives.md
s{(\\]\\()((?:\\./|\\.\\./)*)(archives\\.md\\))}{\$1${root_prefix}archives.md}g;
s{(\\]:\\s*)((?:\\./|\\.\\./)*)archives\\.md\\s*\$}{\$1${root_prefix}archives.md}mg;
EOF
)

  # Compose le programme complet pour ce fichier
  local PERL_PROG
  PERL_PROG="$(perl_prog_mapping; echo "$perl_root_fix")"

  if [ "$DRY_RUN" = "1" ]; then
    # Diff visuelle
    perl -0777 -pe "$PERL_PROG" "$f" | diff -u --label "$f (old)" --label "$f (new)" "$f" - || true
    return
  fi

  # Édition in-place avec sauvegarde .bak
  perl -0777 -i.bak -pe "$PERL_PROG" "$f" || { echo "ERR perl on $f"; return 1; }

  if cmp -s "$f" "$f.bak"; then
    rm -f -- "$f.bak"
    [ "$SHOW_UNCHANGED" = "1" ] && echo "… unchanged: $f"
  else
    rm -f -- "$f.bak"
    echo "✏️  updated: $f   (root_prefix=${root_prefix:-"."})"
  fi
}

export -f process_one perl_prog_mapping prefix_to_root
export MAPPING

echo "➡️  Réécriture des liens sur $TOTAL fichiers (.md) avec $JOBS jobs…"
printf "%s\0" "${FILES[@]}" | xargs -0 -n1 -P "$JOBS" bash -lc 'process_one "$@"' _

echo "✅ Terminé."
