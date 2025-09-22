# 🧠 Documentation : Objets IPC sous Unix System V

[...retour en arrière](../intro.md)

---

## 📚 Introduction

Les objets **IPC (Inter Process Communication)** permettent à des **processus** de communiquer et de synchroniser leurs actions sur un système Unix. Dans la version **System V**, les objets IPC principaux sont :

* Les **sémaphores**
* Les **files de messages**
* Les **segments de mémoire partagée**

Tous ces objets sont identifiés **numériquement** par un **identificateur unique** (entier) et manipulés à l’aide d’**appels système spécifiques**.

---

## 🛠️ Commandes système utiles

| Commande | Description                                         |
| -------- | --------------------------------------------------- |
| `ipcs`   | Affiche les objets IPC actifs                       |
| `ipcrm`  | Supprime un objet IPC (sémaphore, mémoire, message) |

---

## 🔑 Principes généraux des IPC System V

1. **Création/Ouverture** : fonction `xxxget(key_t key, ...)`
2. **Contrôle** : fonction `xxxctl(int id, int cmd, ...)`
3. **Utilisation** : fonctions spécifiques à l'objet
4. Chaque objet est **géré par le noyau** Unix et utilise une **clé `key_t`** partagée par les processus concernés.

---

## 📊 Tableau récapitulatif des objets IPC

| Objet            | Headers requis                                | Création   | Contrôle   | Opérations principales | Structure associée |
| ---------------- | --------------------------------------------- | ---------- | ---------- | ---------------------- | ------------------ |
| File de messages | `<sys/types.h>`, `<sys/ipc.h>`, `<sys/msg.h>` | `msgget()` | `msgctl()` | `msgsnd()`, `msgrcv()` | `msqid_ds`         |
| Mémoire partagée | `<sys/types.h>`, `<sys/ipc.h>`, `<sys/shm.h>` | `shmget()` | `shmctl()` | `shmat()`, `shmdt()`   | `shmid_ds`         |
| Sémaphores       | `<sys/types.h>`, `<sys/ipc.h>`, `<sys/sem.h>` | `semget()` | `semctl()` | `semop()`              | `semid_ds`         |

---

## 🔐 Constantes communes (`<sys/ipc.h>`)

| Constante     | Description                                             |
| ------------- | ------------------------------------------------------- |
| `IPC_CREAT`   | Crée un objet s'il n'existe pas                         |
| `IPC_EXCL`    | Utilisé avec `IPC_CREAT`, échoue si l'objet existe déjà |
| `IPC_NOWAIT`  | Opération non bloquante                                 |
| `IPC_PRIVATE` | Clé unique, non partageable                             |
| `IPC_RMID`    | Supprime l'objet IPC                                    |
| `IPC_STAT`    | Lit la structure associée                               |
| `IPC_SET`     | Modifie la structure associée                           |

---

## 🧠 Mémoire partagée (SHM)

### 🔎 Définition

Un segment de mémoire partagée permet à **plusieurs processus d’accéder au même espace mémoire**. Il sert à échanger de grandes quantités de données **sans copie**.

### 🔁 Cycle de vie

1. `shmget()` : Création ou ouverture d’un segment.
2. `shmat()` : Attachement à un pointeur dans l’espace du processus.
3. Opérations mémoire (`memcpy`, `sprintf`, etc.).
4. `shmdt()` : Détachement du segment.
5. `shmctl(..., IPC_RMID, ...)` : Suppression du segment.

### 🧱 Structure `shmid_ds`

```c
struct shmid_ds {
    struct ipc_perm shm_perm;   // Permissions
    int shm_segsz;              // Taille du segment
    long *shm_paddr;            // Adresse physique (obsolète)
    short shm_lpid;             // PID du dernier processus ayant utilisé
    short shm_cpid;             // PID du créateur
    short shm_nattch;           // Nombre de processus attachés
    short shm_cnattch;          // (obsolete) Nombre de processus en mémoire
    time_t shm_atime;           // Dernier attachement
    time_t shm_dtime;           // Dernier détachement
    time_t shm_ctime;           // Dernière modification
};
```

---

## 🧪 Exemple complet : Mono-processus

### 📦 Code

```c
#include <stdio.h>
#include <sys/shm.h>
#include <sys/stat.h>

int main() {
    int segment_id;
    char* shared_memory;
    struct shmid_ds shmbuffer;
    int segment_size;
    const int shared_segment_size = 0x6400;

    // Création
    segment_id = shmget(IPC_PRIVATE, shared_segment_size, IPC_CREAT | IPC_EXCL | S_IRUSR | S_IWUSR);

    // Attachement
    shared_memory = (char*) shmat(segment_id, 0, 0);
    printf("Mémoire partagée attachée à l'adresse %p\n", shared_memory);

    // Taille
    shmctl(segment_id, IPC_STAT, &shmbuffer);
    segment_size = shmbuffer.shm_segsz;
    printf("Taille du segment : %d\n", segment_size);

    // Écriture
    sprintf(shared_memory, "Hello, world.");

    // Détachement
    shmdt(shared_memory);

    // Réattachement
    shared_memory = (char*) shmat(segment_id, (void*) 0x5000000, 0);
    printf("Réattachée à l'adresse %p\n", shared_memory);
    printf("Contenu : %s\n", shared_memory);

    // Détachement et suppression
    shmdt(shared_memory);
    shmctl(segment_id, IPC_RMID, 0);

    return 0;
}
```

---

## 👪 Cas Père-Fils

* Le **père crée** le segment (avec `IPC_PRIVATE`) et l’attache.
* Il **forke** des processus fils, qui héritent du pointeur.
* Chaque **fils détache** le segment en fin de traitement.
* Le **père libère** le SHM avec `shmctl(..., IPC_RMID, ...)`.

---

## 🔗 Cas processus totalement distincts

### Étapes

1. Le premier processus crée une **clé avec `ftok()`** :

   ```c
   key_t key = ftok("fichier", 42);
   ```

2. Création :

   ```c
   int shmid = shmget(key, taille, IPC_CREAT | S_IRUSR | S_IWUSR);
   ```

3. Les autres processus réutilisent la même clé pour faire :

   ```c
   int shmid = shmget(key, 0, 0);
   char* ptr = (char*) shmat(shmid, 0, 0);
   ```

4. Chaque processus **détache** avec `shmdt()`.

5. Un processus **supprime** le segment avec `shmctl(..., IPC_RMID, 0)`.

---

## 🧮 Fonctions utiles en mémoire partagée

| Fonction                 | Description              |
| ------------------------ | ------------------------ |
| `memset()`               | Initialisation mémoire   |
| `memcpy()`               | Copie brute              |
| `memcmp()`               | Comparaison mémoire      |
| `strlen()`               | Taille d'une chaîne      |
| `strcpy()` / `strncpy()` | Copie de chaîne          |
| `strcat()` / `strncat()` | Concatenation de chaînes |

> ⚠️ Attention : la synchronisation entre processus nécessite l’utilisation de **sémaphores IPC System V**, **et non les sémaphores POSIX** (réservés aux threads).

---

## 📌 Bonnes pratiques

* Utiliser `IPC_PRIVATE` pour des SHM temporaires non partagés.
* Utiliser `ftok()` pour créer des clés reproductibles.
* Toujours **détacher et libérer** les SHM pour éviter les fuites mémoire.
* Penser à **protéger les accès concurrentiels** avec des sémaphores.

---

## 🧪 Ressources complémentaires

* `man shmget`, `man shmat`, `man shmctl`
* `man ipcs`, `man ipcrm`
* `man ftok`

---

[...retour en arrière](../intro.md)