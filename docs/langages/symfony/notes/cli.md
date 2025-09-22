# Symfony CLI

[Retour menu Symphony](../menu.md)

## **1. Introduction à Symfony CLI**

Symfony CLI est un outil essentiel pour faciliter le développement d’applications Symfony. Il permet de gérer les projets, de démarrer un serveur local, et d’exécuter des commandes utiles pour accélérer le développement.

### **1.1 Commandes essentielles**

#### **Démarrer un serveur local**

Pour exécuter un serveur local et tester l'application :

```bash
symfony server:start
```

Le serveur sera disponible par défaut à l’adresse `http://127.0.0.1:8000`.

Options utiles :
- `--port=8080` : Permet de spécifier un autre port.
- `--daemon` : Exécute le serveur en arrière-plan.

Exemple :

```bash
symfony server:start --port=8080
```

#### **Arrêter le serveur local**

Pour arrêter un serveur en cours d'exécution :

```bash
symfony server:stop
```

#### **Lister les routes disponibles**

Afficher toutes les routes de l'application :

```bash
symfony console debug:router
```

Chaque route sera listée avec :
- Son nom.
- Sa méthode HTTP (GET, POST, etc.).
- Le chemin de l’URL.
- Le contrôleur associé.

#### **Lister les services dans le conteneur**

Afficher tous les services disponibles dans le conteneur Symfony :

```bash
symfony console debug:container
```

Cette commande affiche :
- Le nom des services.
- Leur classe associée.
- Les tags spécifiques (utile pour identifier des services comme les événements ou middlewares).

Filtrer les services :

```bash
symfony console debug:container --tag=doctrine.entity_manager
```

---

## **2. Commandes importantes pour le développement**

### **2.1 Génération de composants avec les commandes `make`**

Symfony CLI permet de générer des fichiers essentiels pour le développement.

#### **Générer un contrôleur**

Créer un nouveau contrôleur pour gérer les routes :

```bash
symfony console make:controller NomDuControleur
```

Cela génère un fichier `NomDuControleur.php` dans le dossier `src/Controller` avec un exemple de méthode.

#### **Générer une entité Doctrine**

Créer une entité pour représenter une table dans la base de données :

```bash
symfony console make:entity NomEntite
```

L’assistant demande ensuite de définir les champs :

```
Nouveau champ : nom
Type de champ (e.g. string) [string]: string
```

Exemple complet :

```bash
symfony console make:entity Utilisateur
```

Ajouter des champs supplémentaires après la création initiale :

```bash
symfony console make:entity
```

L’assistant proposera de modifier les entités existantes.

---

### **2.2 Gestion des migrations avec Doctrine**

Les **migrations** permettent de synchroniser la structure des entités avec la base de données. 

#### **Créer une migration**

Après avoir modifié une entité ou créé une nouvelle, utiliser cette commande pour générer un fichier de migration :

```bash
symfony console make:migration
```

Cela génère un fichier dans le dossier `migrations/`. Le fichier contient des instructions SQL pour appliquer les modifications (ajout de colonnes, création de tables, etc.).

Exemple de sortie :

```sql
CREATE TABLE utilisateur (id INT AUTO_INCREMENT NOT NULL, nom VARCHAR(255) NOT NULL, PRIMARY KEY(id));
```

#### **Exécuter une migration**

Pour appliquer les modifications à la base de données :

```bash
symfony console doctrine:migrations:migrate
```

Confirmation requise avant l’exécution :

```
ATTENTION : Cela exécutera toutes les migrations non appliquées.
Continuer ? (yes/no) [no]: yes
```

#### **Vérifier l’état des migrations**

Voir quelles migrations ont été appliquées et lesquelles sont en attente :

```bash
symfony console doctrine:migrations:status
```

Sortie typique :

```
 >> Nombre de migrations exécutées : 3
 >> Nombre de migrations en attente : 2
```

#### **Annuler une migration**

Pour revenir à l’état précédent d’une migration :

```bash
symfony console doctrine:migrations:rollback
```

Cela annule la dernière migration appliquée.

---

### **2.3 Autres commandes Doctrine utiles**

#### **Synchroniser la base de données directement**

Pour éviter de passer par des migrations et synchroniser immédiatement la base de données avec les entités :

```bash
symfony console doctrine:schema:update --force
```

Options :
- `--dump-sql` : Affiche les requêtes SQL sans les exécuter.
- `--force` : Exécute les requêtes et applique les changements directement.

Exemple :

```bash
symfony console doctrine:schema:update --dump-sql
```

Cela retourne les instructions SQL nécessaires sans toucher à la base de données.

#### **Vérifier la validité des entités**

Pour vérifier si les entités sont correctement configurées :

```bash
symfony console doctrine:schema:validate
```

Cette commande analyse :
- La synchronisation entre les entités et la base.
- La validité des mappings Doctrine.

---

### **2.4 Commandes diverses pour le cache et le débogage**

#### **Vider le cache**

Après des modifications de configuration ou d’environnement, vider le cache pour appliquer les changements :

```bash
symfony console cache:clear
```

Options :
- `--env=prod` : Vide le cache pour l’environnement de production.
- `--no-warmup` : Ne recrée pas le cache immédiatement après la suppression.

#### **Afficher la configuration des paramètres**

Voir la configuration actuelle des paramètres Symfony :

```bash
symfony console debug:config
```

Utiliser cette commande pour un bundle ou service spécifique :

```bash
symfony console debug:config doctrine
```

---

## **Résumé des commandes Doctrine**

| Commande                                      | Description                                                 |
|-----------------------------------------------|-------------------------------------------------------------|
| `make:entity`                                 | Créer ou modifier une entité Doctrine                       |
| `make:migration`                              | Générer un fichier de migration                             |
| `doctrine:migrations:migrate`                 | Appliquer les migrations                                    |
| `doctrine:migrations:status`                  | Vérifier l'état des migrations                              |
| `doctrine:migrations:rollback`                | Annuler la dernière migration                               |
| `doctrine:schema:update --force`              | Synchroniser directement la base de données avec les entités|
| `doctrine:schema:validate`                    | Vérifier la validité des mappings Doctrine                  |


---

[Retour menu Symphony](../menu.md)