# ⚠️ Gestion des erreurs en C 

[...retorn en rèire](./../menu.md)

---

En C, la gestion des erreurs est **manuelle** : c’est au programmeur de **vérifier systématiquement** les valeurs de retour et d’utiliser les mécanismes fournis par la bibliothèque standard.
Cette section est cruciale pour écrire du code **fiable, robuste et sécurisé**. 🚀

---

## 6.1 🔄 Valeur de retour d’une fonction

La **plupart des fonctions C** signalent les erreurs via leur **valeur de retour**.

* Exemple classique : `scanf()` retourne le **nombre de valeurs correctement lues**.

```c
#include <stdio.h>

int main() {
    int x;
    printf("Entrez un entier : ");
    if (scanf("%d", &x) != 1) {   // Vérification obligatoire ✅
        printf("Erreur : saisie invalide.\n");
        return 1;  // code d'erreur
    }
    printf("Vous avez saisi : %d\n", x);
    return 0;
}
```

👉 Bonne pratique : **ne jamais ignorer** la valeur de retour d’une fonction critique (`scanf`, `fopen`, `malloc`, etc.).

---

## 6.2 🏁 Code de retour du programme (`main`)

* En C, la fonction `main()` retourne un **code de retour** à l’OS :

  * `return 0;` → succès ✅
  * `return 1;` (ou autre ≠0) → échec ❌

Exemple :

```c
int main() {
    FILE *f = fopen("data.txt", "r");
    if (!f) {
        printf("Erreur : impossible d’ouvrir le fichier.\n");
        return 1; // signaler l'erreur
    }

    // ... traitement ...
    fclose(f);
    return 0; // succès
}
```

💡 En ligne de commande (Linux/Unix), la variable `$?` contient le code retour du dernier programme exécuté.
Exemple :

```bash
./mon_programme
echo $?
```

---

## 6.3 🛑 La variable globale `errno`

* Définie dans `<errno.h>`.
* Certaines fonctions (notamment de la libc) définissent `errno` lorsqu’une erreur survient.
* `errno` contient un **entier** indiquant le type d’erreur.

### 🔖 Valeurs typiques :

* `EDOM` → domaine invalide (ex. racine carrée négative).
* `ERANGE` → dépassement de capacité (ex. nombre trop grand/petit).
* `EILSEQ` → séquence invalide (ex. erreur de conversion).

---

## 6.4 📢 Afficher un message d’erreur avec `strerror`

`strerror(errno)` transforme le code d’erreur en message **lisible par l’humain**.

Exemple avec `pow()` :

```c
#include <stdio.h>
#include <math.h>
#include <errno.h>
#include <string.h>

int main() {
    errno = 0; // réinitialiser
    double x = pow(-1, 0.5); // racine carrée de -1 ❌
    
    if (errno) {
        printf("Erreur détectée : %s\n", strerror(errno));
        return 1;
    }

    printf("Résultat : %f\n", x);
    return 0;
}
```

👉 Ici, `errno` sera mis à `EDOM` et affichera un message comme :
`Erreur détectée : Numerical argument out of domain`.

---

## 6.5 🛡️ Assertions (`assert`)

* Définies dans `<assert.h>`.
* Vérifient une condition à l’exécution.

  * Si la condition est **fausse** → affichage d’un message d’erreur + arrêt brutal du programme (`abort()`).

```c
#include <assert.h>
#include <stdio.h>

int division(int a, int b) {
    assert(b != 0); // 🚨 garantit que b n'est pas nul
    return a / b;
}

int main() {
    printf("%d\n", division(10, 2)); // ok
    printf("%d\n", division(5, 0));  // crash volontaire
    return 0;
}
```

### ⚙️ Désactiver les assertions

* En production, on peut désactiver les assertions avec l’option de compilation :

```bash
gcc prog.c -DNDEBUG -o prog
```

💡 `NDEBUG` signifie **No DEBUG** → les `assert()` sont ignorés.

---

[...retorn en rèire](./../menu.md)