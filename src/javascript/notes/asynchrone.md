# Notes - Fetch & aysnchronisme

[...retour au sommaire](../sommaire.md)

---

Dans les grandes lignes, l'asynchronisme est le fait de ne pas attendre qu'une tâche se termine avant de poursuivre l'exécution du code.
Cela permet de continuer à rester réactif, tout en attendant les résultats de calculs ou de requêtes qui peuvent prendre du temps.

## Await/Async & promise

La manière de gérer les await et async est très proche de ce qui se fait en .NET, à la différence qu'une fonction `async` ne renverra pas une Task, mais une **promise**.

Pour commencer, on a le mot clé `async` qui permet de déclarer des fonctions asynchrones. 

```js
async function fonctionAsynchrone() {
    ...
    // code potentiellement asynchrone
    ...
}
```

Lorsqu'on appelle une fonction asynchrone, elle ne renvoie pas de valeurs directement, mais une promesse, qui elle renverra une valeur si elle réussit bien.
On attend le résultat de la promesse avec un await.

```js
async function fonctionAsynchrone() {
    ...
    return 9;
}

const maPromesse = fonctionAsynchrone();
...
...
let valeur = await maPromesse;

// ou bien :
let autreValeur = await fonctionAsynchrone();
```

Il est aussi possible de stocker plusieurs promesses dans un tableau, et d'attendre de différentes manières leur fin.

```js
async function maFonctionAsynchrone(val) {
    // ...
    return val * 3;
}

const maPromesse1 = maFonctionAsynchrone(7);
const maPromesse2 = maFonctionAsynchrone(5);
const maPromesse3 = maFonctionAsynchrone(18);

// On utilise un tableau pour passer les promesses à Promise.all
let tableauDeValeurs = await Promise.all([maPromesse1, maPromesse2, maPromesse3]);

console.log(tableauDeValeurs);
```

* `Promise.all` : Renvoie une promesse qui est résolue lorsque toutes les promesses dans l'itérable sont résolues, ou rejetée si au moins une promesse est rejetée. La résolution est un tableau contenant les valeurs de résolution des promesses d'origine, dans l'ordre.

* `Promise.any` : Renvoie une seule promesse résolue avec la valeur de la première promesse dans l'itérable qui est résolue. Si toutes les promesses sont rejetées, renvoie une promesse rejetée avec un objet d'erreur agrégeant toutes les raisons des rejets.

* `Promise.allSettled` : Attend que toutes les promesses de l'itérable soient acquittées (qu'elles soient tenues ou rejetées) et renvoie une promesse résolue avec un tableau contenant des objets représentant le résultat de chaque promesse, indiquant si elles ont été tenues ou rejetées.

* `Promise.race` : Renvoie une promesse qui est résolue ou rejetée dès que la première promesse dans l'itérable est résolue ou rejetée. La valeur ou la raison de la première promesse déterminante est transmise à la promesse renvoyée.

---

[...retour au sommaire](../sommaire.md)