# Documentation Complète sur Tmux

[...retour en arriere](../menu.md)

---

## Introduction

### Qu'est-ce que Tmux ?
Tmux est un multiplexeur de terminal qui permet :
- De créer plusieurs sessions dans un seul terminal.
- De diviser l'écran en plusieurs panneaux.
- De naviguer facilement entre différents environnements de travail.
- De détacher et rattacher des sessions pour les maintenir actives en arrière-plan.

### Installation

#### Sous Debian/Ubuntu :
```bash
sudo apt update
sudo apt install tmux
```

#### Sous Fedora/CentOS :
```bash
sudo dnf install tmux
```

#### Sous macOS (via Homebrew) :
```bash
brew install tmux
```

#### Vérification de l'installation :
```bash
tmux -V
```

---

## Commandes de Base

### Lancer Tmux
```bash
tmux
```

### Créer une nouvelle session
```bash
tmux new -s nom_de_session
```

### Attacher une session existante
```bash
tmux attach -t nom_de_session
```

### Lister les sessions
```bash
tmux list-sessions
```

### Détacher une session active
**Raccourci clavier** : `Ctrl+b` puis `d`

---

## Gestion des Fenêtres

### Créer une nouvelle fenêtre
**Raccourci clavier** : `Ctrl+b` puis `c`

### Naviguer entre les fenêtres
- **Suivante** : `Ctrl+b` puis `n`
- **Précédente** : `Ctrl+b` puis `p`

### Renommer une fenêtre
**Raccourci clavier** : `Ctrl+b` puis `,`

### Fermer une fenêtre
```bash
exit
```
Ou avec `Ctrl+d` dans la fenêtre.

---

## Gestion des Panneaux

### Diviser l'écran en panneaux
- **Horizontalement** : `Ctrl+b` puis `%`
- **Verticalement** : `Ctrl+b` puis `"`

### Naviguer entre les panneaux
- **Vers la gauche** : `Ctrl+b` puis `←`
- **Vers la droite** : `Ctrl+b` puis `→`
- **Vers le haut** : `Ctrl+b` puis `↑`
- **Vers le bas** : `Ctrl+b` puis `↓`

### Résizer un panneau
- **Augmenter en hauteur** : `Ctrl+b` puis `Ctrl+↑`
- **Diminuer en hauteur** : `Ctrl+b` puis `Ctrl+↓`
- **Augmenter en largeur** : `Ctrl+b` puis `Ctrl+→`
- **Diminuer en largeur** : `Ctrl+b` puis `Ctrl+←`

### Fermer un panneau
**Raccourci clavier** : `Ctrl+b` puis `x` (confirmer avec `y`).

---

## Gestion des Sessions

### Renommer une session
```bash
tmux rename-session -t ancien_nom nouveau_nom
```

### Tuer une session
```bash
tmux kill-session -t nom_de_session
```

### Tuer toutes les sessions
```bash
tmux kill-server
```

---

## Fichiers de Configuration

### Localisation
Le fichier de configuration par défaut est : `~/.tmux.conf`

### Exemple de Configuration Basique
```bash
# Modifier la touche préfixe par Ctrl+a
set-option -g prefix C-a
unbind C-b
bind C-a send-prefix

# Activer la synchronisation des panneaux
bind-key S setw synchronize-panes on

# Désactiver la synchronisation des panneaux
bind-key s setw synchronize-panes off

# Créer des raccourcis personnalisés
bind -n C-Space next-window
```

Pour appliquer les changements :
```bash
tmux source-file ~/.tmux.conf
```

---

## Fonctionnalités Avancées

### Mode de Copie
Entrer dans le mode de copie : `Ctrl+b` puis `[`  
Naviguer dans le texte avec les flèches et copier avec `Ctrl+Space` pour débuter et `Enter` pour valider.

### Charger des Sessions au Démarrage
Créez un fichier avec les commandes tmux :
```bash
# Exemple : ~/tmux-startup.sh
tmux new-session -d -s dev "vim"
tmux split-window -h
```
Puis lancez-le :
```bash
bash ~/tmux-startup.sh
```

---

## Commandes Fréquemment Utilisées

| Commande                          | Description                           |
|-----------------------------------|---------------------------------------|
| `tmux`                            | Lancer Tmux                          |
| `tmux new -s nom`                 | Créer une nouvelle session           |
| `tmux attach -t nom`              | Attacher une session                 |
| `tmux list-sessions`              | Lister les sessions                  |
| `Ctrl+b c`                        | Nouvelle fenêtre                    |
| `Ctrl+b %`                        | Diviser horizontalement              |
| `Ctrl+b "`                        | Diviser verticalement                |
| `Ctrl+b d`                        | Détacher la session                  |
| `tmux kill-session -t nom`        | Supprimer une session                |
| `tmux kill-server`                | Supprimer toutes les sessions        |
| `Ctrl+b [`                        | Entrer en mode copie                 |

---

## Références

- [Documentation Officielle Tmux](https://github.com/tmux/tmux/wiki)
- [Cheat Sheet Tmux](https://tmuxcheatsheet.com/)

---

[...retour en arriere](../menu.md)

