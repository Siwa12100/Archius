#!/usr/bin/env bash
set -euo pipefail

JOBS="${JOBS:-$(command -v nproc >/dev/null 2>&1 && nproc || echo 4)}"
DRY_RUN="${DRY_RUN:-0}"      # 1 = prévisualiser (diff) sans écrire
VERBOSE="${VERBOSE:-1}"      # 1 = logs détaillés, 0 = logs minimum
SHOW_UNCHANGED="${SHOW_UNCHANGED:-0}"  # 1 = afficher aussi les fichiers inchangés

ROOT_README="README.md"

# -- fichiers ciblés : tous les .md suivis par git --
mapfile -t FILES < <(git ls-files '*.md' | LC_ALL=C sort)

# -- util: est-ce le README racine ? --
is_root_readme() {
  local f="$1"
  [[ "$f" == "$ROOT_README" ]]
}

# -- calcule le préfixe relatif (../../../) pour rejoindre la racine depuis le fichier --
prefix_to_root() {
  local path="$1"
  local dir="${path%/*}"
  [[ "$dir" == "$path" ]] && dir="."     # pas de slash -> racine
  [[ "$dir" == "." ]] && { printf ""; return; }
  IFS='/' read -r -a seg <<< "$dir"
  local n=0 s
  for s in "${seg[@]}"; do
    [[ -n "$s" ]] && n=$((n+1))
  done
  printf '%0.s../' $(seq 1 "$n")
}

# -- compte les occurrences de liens vers README dans un fichier --
count_readme_links() {
  local f="$1"
  # Inline: ](../*/README.md[...])
  # Ref:   ]: ../*/README.md
  perl -0777 -ne '
    $c  = () = /\]\(((?:\.\/|\.\.\/)*)README\.md(?:#[^)]+)?\)/g;
    $c += () = /\]:\s*((?:\.\/|\.\.\/)*)README\.md(?:\s*|\s+#.*)$/mg;
    print $c;
  ' -- "$f"
}

# -- programme perl sur-mesure pour un fichier donné (insertion du bon prefix) --
build_perl_prog() {
  local prefix="$1"
  cat <<'PPL'
# normalise : on enlève temporairement ancre/query côté RHS si présents (gérées par backref)
# (on conserve l’ancre/les queries si elles existent)
PPL
  cat <<PPL
# Inline [txt](./README.md#...) ou (../../README.md)
s{(\\]\\()(?:(?:\\./|\\.\\./)*)README\\.md(#[^)]+)?\\)}{\$1${prefix}README.md\$2)}g;

# Références  [txt]: ./README.md  ou  ../../README.md
s{(\\]:\\s*)(?:(?:\\./|\\.\\./)*)README\\.md(\\s*(?:#.*)?)}{\$1${prefix}README.md\$2}mg;
PPL
}

process_one() {
  local f="$1"
  [[ -f "$f" ]] || { [[ "$VERBOSE" = 1 ]] && echo "skip (not file): $f"; return; }
  is_root_readme "$f" && { [[ "$VERBOSE" = 1 ]] && echo "skip root README: $f"; return; }

  local prefix links_before links_after
  prefix="$(prefix_to_root "$f")"
  links_before="$(count_readme_links "$f")"

  if [[ "$links_before" -eq 0 ]]; then
    [[ "$SHOW_UNCHANGED" = 1 ]] && echo "… unchanged (no README links): $f"
    return
  fi

  # construit le programme perl pour CE fichier
  local PERL_PROG
  PERL_PROG="$(build_perl_prog "$prefix")"

  if [[ "$DRY_RUN" = 1 ]]; then
    echo "—— DRY-RUN —— $f"
    [[ "$VERBOSE" = 1 ]] && echo "  prefix_to_root='$prefix' ; matches=$links_before"
    perl -0777 -pe "$PERL_PROG" "$f" | diff -u --label "$f (old)" --label "$f (new)" "$f" - || true
    return
  fi

  perl -0777 -i.bak -pe "$PERL_PROG" "$f" || { echo "ERR perl on $f"; return 1; }

  links_after="$(count_readme_links "$f")"

  if cmp -s "$f" "$f.bak"; then
    rm -f -- "$f.bak"
    [[ "$SHOW_UNCHANGED" = 1 ]] && echo "… unchanged: $f (prefix='$prefix', found=$links_before)"
  else
    rm -f -- "$f.bak"
    local fixed=$(( links_before - links_after ))
    [[ "$fixed" -lt 0 ]] && fixed=0
    echo "✏️  updated: $f"
    [[ "$VERBOSE" = 1 ]] && echo "    prefix_to_root='$prefix' ; before=$links_before ; after=$links_after ; fixed=$fixed"
  fi
}

export -f process_one prefix_to_root count_readme_links build_perl_prog is_root_readme
export DRY_RUN VERBOSE SHOW_UNCHANGED ROOT_README

echo "➡️  Correction des liens vers '$ROOT_README'… (jobs=$JOBS)"
printf "%s\0" "${FILES[@]}" | xargs -0 -n1 -P "$JOBS" bash -lc 'process_one "$@"' _

echo "✅ Terminé."
