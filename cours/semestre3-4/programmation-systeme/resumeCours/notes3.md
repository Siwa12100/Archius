# Les segments de mémoire partagée (SHM) en C sous Unix

[...retour en arrière](../intro.md)

---

## 📌 Qu'est-ce que la mémoire partagée ?

La **mémoire partagée (Shared Memory)** est un mécanisme d’IPC (Inter-Process Communication) qui permet à plusieurs processus **d'accéder simultanément à une même zone de mémoire**, sans avoir à copier les données.

🔧 **Avantages** :

* Très **rapide** (pas de copie entre processus)
* Idéal pour **gros volumes de données**
* Combine performance + simplicité d’accès

🚨 **Inconvénients** :

* Il faut **gérer la synchronisation** manuellement (avec des sémaphores !)
* Risque de **conflits d’accès mémoire**

---

## 🛠️ Les fonctions de base

### 1. `shmget` – Création ou ouverture

```c
int shmget(key_t key, size_t size, int shmflg);
```

* `key` : identifiant (utiliser `ftok()` ou `IPC_PRIVATE`)
* `size` : taille en octets du segment
* `shmflg` : droits + options (`IPC_CREAT`, `IPC_EXCL`, `S_IRUSR | S_IWUSR`…)

🔁 Retourne : identifiant de segment (`shmid`) ou `-1` en cas d’erreur

---

### 2. `shmat` – Attachement

```c
void *shmat(int shmid, const void *shmaddr, int shmflg);
```

* `shmid` : ID retourné par `shmget`
* `shmaddr` : adresse souhaitée (généralement `NULL`)
* `shmflg` : `0` ou `SHM_RDONLY`

🔁 Retourne : pointeur vers la zone mémoire ou `(void *) -1` en cas d’échec

---

### 3. `shmdt` – Détachement

```c
int shmdt(const void *shmaddr);
```

* `shmaddr` : pointeur retourné par `shmat`

🔁 Retourne `0` si OK, `-1` si erreur

---

### 4. `shmctl` – Contrôle

```c
int shmctl(int shmid, int cmd, struct shmid_ds *buf);
```

* `cmd` : `IPC_RMID`, `IPC_STAT`, `IPC_SET`…
* `buf` : pointeur vers structure `shmid_ds` pour lire/modifier des infos

🔁 Retourne `0` si OK, `-1` si erreur

---

## 🧱 Structure `shmid_ds`

```c
struct shmid_ds {
    struct ipc_perm shm_perm;  // Permissions (UID, GID, mode)
    size_t shm_segsz;          // Taille en octets
    pid_t shm_lpid;            // PID dernier opérateur
    pid_t shm_cpid;            // PID créateur
    shmatt_t shm_nattch;       // Nombre de processus attachés
    time_t shm_atime;          // Dernier attachement
    time_t shm_dtime;          // Dernier détachement
    time_t shm_ctime;          // Dernier changement
};
```

---

## 📂 Étapes complètes pour utiliser un segment partagé

### ✅ Création (processus initial)

```c
key_t key = ftok("fichier.txt", 42);  // Clé partagée
int shmid = shmget(key, 4096, IPC_CREAT | S_IRUSR | S_IWUSR);
char* mem = (char*) shmat(shmid, NULL, 0);
```

### ✍️ Utilisation

```c
sprintf(mem, "Bonjour depuis le processus A !");
```

### 🔚 Détachement

```c
shmdt(mem);
```

### ❌ Suppression (dernier utilisateur uniquement)

```c
shmctl(shmid, IPC_RMID, NULL);
```

---

## 👨‍👩‍👧 Exemple complet : communication père-fils

```c
#include <stdio.h>
#include <stdlib.h>
#include <sys/shm.h>
#include <unistd.h>
#include <string.h>

int main() {
    int shmid = shmget(IPC_PRIVATE, 1024, IPC_CREAT | 0666);
    if (shmid == -1) exit(1);

    char* mem = (char*) shmat(shmid, NULL, 0);
    if (mem == (void*) -1) exit(2);

    pid_t pid = fork();

    if (pid == 0) { // Fils
        sleep(1); // Attendre le père
        printf("Fils lit : %s\n", mem);
        shmdt(mem);
        exit(0);
    } else { // Père
        strcpy(mem, "Message partagé !");
        wait(NULL);
        shmdt(mem);
        shmctl(shmid, IPC_RMID, NULL);
    }

    return 0;
}
```

---

## 🔁 Synchronisation : Protéger les accès concurrents

### 🚫 Problème :

Plusieurs processus peuvent écrire en même temps → données corrompues

### ✅ Solution :

Utiliser des **sémaphores System V** pour verrouiller la mémoire avant lecture/écriture.

> Exemple :
>
> * `semget()` pour créer le sémaphore
> * `semop()` pour le prendre/libérer
> * `semctl(..., IPC_RMID)` pour le supprimer

---

## 🧪 Exemple multi-processus avec clé partagée

### Fichier 1 – Créateur (`init.c`)

```c
#include <stdio.h>
#include <sys/shm.h>
#include <sys/ipc.h>
#include <string.h>

int main() {
    key_t key = ftok("cle.txt", 'A');
    int shmid = shmget(key, 1024, IPC_CREAT | 0666);
    char* mem = (char*) shmat(shmid, NULL, 0);

    strcpy(mem, "Données partagées entre processus !");
    shmdt(mem);

    return 0;
}
```

### Fichier 2 – Lecteur (`client.c`)

```c
#include <stdio.h>
#include <sys/shm.h>
#include <sys/ipc.h>

int main() {
    key_t key = ftok("cle.txt", 'A');
    int shmid = shmget(key, 1024, 0666);
    char* mem = (char*) shmat(shmid, NULL, 0);

    printf("Lu : %s\n", mem);
    shmdt(mem);

    return 0;
}
```

---

## ⚠️ Erreurs fréquentes

| Erreur                 | Cause                                       | Solution                                |
| ---------------------- | ------------------------------------------- | --------------------------------------- |
| `shmget() = -1`        | Mauvaise clé ou droits insuffisants         | Vérifier `key`, `ftok()` et `IPC_CREAT` |
| `shmat() = (void*) -1` | Segment non existant ou accès refusé        | Vérifier `shmid` et droits              |
| Mémoire non libérée    | `shmdt()` ou `shmctl(..., IPC_RMID)` oublié | Toujours libérer après usage            |
| Conflit d’écriture     | Accès concurrent sans synchronisation       | Utiliser sémaphores System V            |

---

## 📚 Fonctions utiles

| Fonction                      | Usage                                       |
| ----------------------------- | ------------------------------------------- |
| `ftok(pathname, proj_id)`     | Génère une clé unique à partir d’un fichier |
| `strcpy`, `strcat`, `sprintf` | Manipulation de chaînes dans le segment     |
| `memset`, `memcpy`, `memcmp`  | Manipulation mémoire rapide                 |
| `ipcs -m`                     | Liste les SHM actifs                        |
| `ipcrm -m ID`                 | Supprime un segment manuellement            |

---

## 🧽 Nettoyage automatique

> La mémoire partagée **reste dans le système même après la fin des processus**, si elle n’est pas supprimée explicitement.
> Utilisez toujours `shmctl(shmid, IPC_RMID, NULL)` **à la fin** du dernier processus utilisateur.

---

[...retour en arrière](../intro.md)
