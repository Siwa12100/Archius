# **📡 Canaux de Communication Nommés (Named Pipes / FIFO) sous Linux**

[...retorn en rèire](../menu.md)

---

## **📋 Sommaire**
1. **[Introduction aux FIFO](#-1-introduction-aux-fifo)**
2. **[Création et Utilisation en Shell/Bash](#-2-création-et-utilisation-en-shellbash)**
   - 2.1. [Créer un FIFO](#21-créer-un-fifo)
   - 2.2. [Écrire/Lire depuis un FIFO](#22-écrirelire-depuis-un-fifo)
   - 2.3. [Exemples Pratiques](#23-exemples-pratiques)
3. **[Manipulation en C](#-3-manipulation-en-c)**
   - 3.1. [Créer un FIFO avec `mkfifo()`](#31-créer-un-fifo-avec-mkfifo)
   - 3.2. [Écrire dans un FIFO](#32-écrire-dans-un-fifo)
   - 3.3. [Lire depuis un FIFO](#33-lire-depuis-un-fifo)
   - 3.4. [Exemple Complet en C](#34-exemple-complet-en-c)
4. **[Cas d'Usage Avancés](#-4-cas-dusage-avancés)**
5. **[Comparaison avec les Pipes Anonymes](#-5-comparaison-avec-les-pipes-anonymes)**
6. **[Sécurité et Bonnes Pratiques](#-6-sécurité-et-bonnes-pratiques)**
7. **[Annexes : Commandes et Fonctions Utiles](#-7-annexes-commandes-et-fonctions-utiles)**

---

## **🔹 1. Introduction aux FIFO**
### **📌 Qu'est-ce qu'un FIFO ?**
- **FIFO** (*First-In-First-Out*) ou **named pipe** est un **fichier spécial** qui permet une **communication inter-processus (IPC)**.
- Contrairement aux **pipes anonymes** (`|` dans le shell), un FIFO est **persistant** et accessible via un **chemin dans le système de fichiers** (ex: `/tmp/mon_fifo`).
- Fonctionne comme un **tuyau** : les données écrites d'un côté sont lues de l'autre, dans l'ordre.

### **🔍 Caractéristiques Clés**
| Propriété               | Détails                                                                 |
|-------------------------|-------------------------------------------------------------------------|
| **Type de fichier**     | Fichier spécial (type `p` dans `ls -l`).                                |
| **Persistance**         | Existe tant qu'il n'est pas supprimé (contrairement aux pipes anonymes).|
| **Directionnel**         | Unidirectionnel (mais on peut créer deux FIFO pour une communication bidirectionnelle). |
| **Blocage**             | Par défaut, `open()` bloque jusqu'à ce qu'un autre processus ouvre l'autre extrémité. |
| **Taille**              | Pas de limite de taille (contrairement aux pipes anonymes, limités à `PIPE_BUF`). |

### **📂 Où les trouve-t-on ?**
- Souvent dans `/tmp` ou `/var/run` pour une communication temporaire.
- Exemple réel : `systemd` utilise des FIFO pour la journalisation (`/run/systemd/journal/stdout`).

---

## **🔧 2. Création et Utilisation en Shell/Bash**
### **2.1. Créer un FIFO**
Utilisez la commande `mkfifo` :
```bash
mkfifo /tmp/mon_fifo
```
**Vérification** :
```bash
ls -l /tmp/mon_fifo
```
Sortie :
```
prw-r--r-- 1 user user 0 mai  10 12:34 /tmp/mon_fifo
```
- Le `p` au début indique qu'il s'agit d'un FIFO.

---

### **2.2. Écrire/Lire depuis un FIFO**
#### **📝 Écrire dans un FIFO**
```bash
echo "Bonjour depuis le FIFO" > /tmp/mon_fifo
```
- **Blocage** : La commande attend qu'un processus lise de l'autre côté.

#### **📖 Lire depuis un FIFO**
Dans un autre terminal :
```bash
cat /tmp/mon_fifo
```
- Affiche : `Bonjour depuis le FIFO`.

#### **⚠️ Comportement par Défaut**
- Si aucun lecteur n'est présent, l'écriture **bloque** (attend un `open()` en lecture).
- Si aucun écrivain n'est présent, la lecture **bloque** (attend un `open()` en écriture).

---

### **2.3. Exemples Pratiques**
#### **🔄 Communication Bidirectionnelle**
Créez deux FIFO :
```bash
mkfifo /tmp/fifo_in /tmp/fifo_out
```
- **Terminal 1 (Écrivain)** :
  ```bash
  while true; do
      read -p "Message: " msg
      echo "$msg" > /tmp/fifo_in
  done
  ```
- **Terminal 2 (Lecteur)** :
  ```bash
  while true; do
      cat /tmp/fifo_in
      echo "Réponse" > /tmp/fifo_out
  done
  ```

#### **📊 Redirection de Sortie**
Envoyer la sortie d'une commande vers un FIFO :
```bash
tail -f /var/log/syslog > /tmp/log_fifo &
```
Dans un autre terminal :
```bash
cat /tmp/log_fifo   # Affiche les logs en temps réel
```

#### **🚀 Utilisation avec `netcat`**
```bash
mkfifo /tmp/netcat_fifo
nc -l 1234 < /tmp/netcat_fifo | tee /tmp/netcat_fifo
```
- Permet de créer un **chat local** via TCP.

---

## **💻 3. Manipulation en C**
### **3.1. Créer un FIFO avec `mkfifo()`**
```c
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <unistd.h>
#include <stdio.h>

int main() {
    const char *fifo_path = "/tmp/mon_fifo_c";

    // Créer le FIFO (mode 0666 = rw-rw-rw)
    if (mkfifo(fifo_path, 0666) == -1) {
        perror("mkfifo");
        return 1;
    }

    printf("FIFO créé : %s\n", fifo_path);
    return 0;
}
```
**Compilation** :
```bash
gcc create_fifo.c -o create_fifo
./create_fifo
```

---

### **3.2. Écrire dans un FIFO**
```c
#include <fcntl.h>
#include <unistd.h>
#include <stdio.h>

int main() {
    const char *fifo_path = "/tmp/mon_fifo_c";
    int fd = open(fifo_path, O_WRONLY);

    if (fd == -1) {
        perror("open (écriture)");
        return 1;
    }

    const char *msg = "Message depuis C !\n";
    write(fd, msg, sizeof(msg) - 1);  // -1 pour exclure le '\0'

    close(fd);
    return 0;
}
```

---

### **3.3. Lire depuis un FIFO**
```c
#include <fcntl.h>
#include <unistd.h>
#include <stdio.h>

int main() {
    const char *fifo_path = "/tmp/mon_fifo_c";
    int fd = open(fifo_path, O_RDONLY);

    if (fd == -1) {
        perror("open (lecture)");
        return 1;
    }

    char buffer[256];
    ssize_t bytes_read = read(fd, buffer, sizeof(buffer) - 1);

    if (bytes_read > 0) {
        buffer[bytes_read] = '\0';  // Terminer la chaîne
        printf("Lu depuis FIFO : %s\n", buffer);
    }

    close(fd);
    return 0;
}
```

---

### **3.4. Exemple Complet en C (Communication Bidirectionnelle)**
**fifo_writer.c** :
```c
#include <fcntl.h>
#include <unistd.h>
#include <stdio.h>

int main() {
    const char *fifo_in = "/tmp/fifo_in";
    const char *fifo_out = "/tmp/fifo_out";

    int fd_in = open(fifo_in, O_WRONLY);
    int fd_out = open(fifo_out, O_RDONLY);

    if (fd_in == -1 || fd_out == -1) {
        perror("open");
        return 1;
    }

    const char *msg = "Ping depuis C !";
    write(fd_in, msg, sizeof(msg));

    char buffer[256];
    read(fd_out, buffer, sizeof(buffer));
    printf("Réponse : %s\n", buffer);

    close(fd_in);
    close(fd_out);
    return 0;
}
```

**fifo_reader.c** :
```c
#include <fcntl.h>
#include <unistd.h>
#include <stdio.h>

int main() {
    const char *fifo_in = "/tmp/fifo_in";
    const char *fifo_out = "/tmp/fifo_out";

    int fd_in = open(fifo_in, O_RDONLY);
    int fd_out = open(fifo_out, O_WRONLY);

    if (fd_in == -1 || fd_out == -1) {
        perror("open");
        return 1;
    }

    char buffer[256];
    read(fd_in, buffer, sizeof(buffer));
    printf("Lu : %s\n", buffer);

    const char *reply = "Pong depuis C !";
    write(fd_out, reply, sizeof(reply));

    close(fd_in);
    close(fd_out);
    return 0;
}
```

**Compilation & Exécution** :
```bash
gcc fifo_writer.c -o writer
gcc fifo_reader.c -o reader
mkfifo /tmp/fifo_in /tmp/fifo_out

# Dans deux terminaux différents :
./writer   # Terminal 1
./reader   # Terminal 2
```

---

## **🔄 4. Cas d'Usage Avancés**
### **4.1. Logging Centralisé**
- Plusieurs processus écrivent dans un FIFO, et un **logger** central lit et enregistre les messages.
```bash
mkfifo /tmp/app_logs
tail -f /tmp/app_logs >> /var/log/app_combined.log &
```
- Chaque application écrit dans `/tmp/app_logs`.

### **4.2. Communication entre Scripts**
- Un script Python envoie des données à un script Bash via un FIFO.
**Python (écrivain)** :
```python
with open("/tmp/python_fifo", "w") as f:
    f.write("Données depuis Python\n")
```
**Bash (lecteur)** :
```bash
while true; do
    data=$(cat /tmp/python_fifo)
    echo "Reçu : $data"
done
```

### **4.3. Simulation de Files d'Attente**
- Un FIFO peut simuler une **file d'attente** pour des tâches asynchrones.
```bash
mkfifo /tmp/task_queue
# Producteur
echo "tache1" > /tmp/task_queue
echo "tache2" > /tmp/task_queue
# Consommateur
while true; do
    task=$(cat /tmp/task_queue)
    echo "Exécution de : $task"
    # Traiter la tâche...
done
```

### **4.4. Intégration avec `inotifywait`**
- Surveiller les modifications d'un FIFO avec `inotify-tools` :
```bash
inotifywait -m /tmp/mon_fifo | while read -r event; do
    echo "Événement détecté : $event"
    cat /tmp/mon_fifo
done
```

---

## **🔀 5. Comparaison avec les Pipes Anonymes**
| Caractéristique       | **FIFO (Named Pipe)**                          | **Pipe Anonyme (`|`)**                     |
|-----------------------|-----------------------------------------------|--------------------------------------------|
| **Persistance**       | Persiste dans le système de fichiers.         | Disparaît quand le processus se termine.   |
| **Accessibilité**     | Accessible via un chemin (ex: `/tmp/mon_fifo`).| Uniquement entre processus liés (parent/enfant). |
| **Communication**     | Peut être utilisé par des processus non liés. | Uniquement entre processus liés.          |
| **Création**          | `mkfifo` ou `mkfifo()` en C.                 | Créé avec `|` dans le shell.               |
| **Blocage**           | Bloque jusqu'à ce qu'un autre processus ouvre l'autre extrémité. | Idem. |
| **Cas d'usage**       | IPC entre applications indépendantes.        | Chaînage de commandes (`ls | grep`).     |

---

## **🔒 6. Sécurité et Bonnes Pratiques**
### **6.1. Permissions**
- Par défaut, un FIFO a les permissions `0666` (lecture/écriture pour tous).
- **Restriction recommandée** :
  ```bash
  mkfifo /tmp/mon_fifo
  chmod 600 /tmp/mon_fifo   # Seul le propriétaire peut lire/écrire
  ```

### **6.2. Emplacement**
- Placez les FIFO dans `/tmp` ou `/var/run` (répertoires temporaires).
- Évitez les chemins prédictibles pour limiter les attaques par **symlink**.

### **6.3. Gestion des Blocages**
- Utilisez `open()` avec `O_NONBLOCK` pour éviter les blocages :
  ```c
  int fd = open("/tmp/mon_fifo", O_RDONLY | O_NONBLOCK);
  ```
  - Si aucun écrivain n'est présent, `open()` retourne immédiatement (au lieu de bloquer).

### **6.4. Nettoyage**
- Supprimez les FIFO après utilisation :
  ```bash
  rm /tmp/mon_fifo
  ```
- En C, utilisez `unlink()` :
  ```c
  unlink("/tmp/mon_fifo");
  ```

### **6.5. Éviter les Fuites de Données**
- Un FIFO n'a **pas de limite de taille** : si un écrivain envoie trop de données sans lecteur, la mémoire peut saturer.
- **Solution** : Utilisez des tampons ou un mécanisme de contrôle de flux.

---

## **📚 7. Annexes : Commandes et Fonctions Utiles**
### **7.1. Commandes Shell**
| Commande               | Description                                                                 |
|------------------------|-----------------------------------------------------------------------------|
| `mkfifo <fichier>`     | Crée un FIFO.                                                              |
| `ls -l`                | Affiche les FIFO (type `p`).                                               |
| `stat <fifo>`          | Affiche les métadonnées (taille = 0).                                      |
| `cat <fifo>`           | Lit depuis un FIFO (bloquant).                                             |
| `echo "txt" > <fifo>`  | Écrit dans un FIFO (bloquant).                                             |
| `tail -f <fifo>`       | Suit les données écrites dans un FIFO en temps réel.                      |
| `stdbuf -oL`           | Désactive la mise en tampon pour les pipes (utile pour les logs en direct).|

### **7.2. Fonctions C**
| Fonction               | En-tête          | Description                                                                 |
|------------------------|------------------|-----------------------------------------------------------------------------|
| `mkfifo()`             | `sys/stat.h`     | Crée un FIFO.                                                              |
| `open()`               | `fcntl.h`        | Ouvre un FIFO en lecture/écriture (bloquant par défaut).                  |
| `read()`               | `unistd.h`       | Lit depuis un FIFO.                                                        |
| `write()`              | `unistd.h`       | Écrit dans un FIFO.                                                        |
| `unlink()`             | `unistd.h`       | Supprime un FIFO.                                                          |
| `fcntl()`              | `fcntl.h`        | Configure les options (ex: `O_NONBLOCK`).                                 |

### **7.3. Outils Externes**
| Outil                  | Description                                                                 |
|------------------------|-----------------------------------------------------------------------------|
| `socat`                | Outil polyvalent pour rediriger des flux (peut utiliser des FIFO).         |
| `netcat (nc)`          | Peut lire/écrire depuis/vers un FIFO pour une communication réseau.        |
| `inotifywait`          | Surveille les changements sur un FIFO.                                    |
| `strace`               | Trace les appels système (utile pour déboguer les FIFO).                   |

---

[...retorn en rèire](../menu.md)