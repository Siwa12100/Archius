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

## .then

Le `.then` est une méthode utilisée pour traiter le résultat réussi d'une Promise en JavaScript. 

### Invocation de `.then`
   - Le `.then` est appelé sur une Promise. Cette méthode prend une ou deux fonctions en arguments. La première fonction spécifie le traitement à effectuer lorsque la Promise est résolue (réussie), et la deuxième fonction (optionnelle) spécifie le traitement des erreurs en cas de rejet de la Promise.

   ```javascript
   maPromise.then(
     function(resultatReussi) {
       // Code à exécuter en cas de réussite
     },
     function(raisonRejet) {
       // Code à exécuter en cas d'échec (rejet)
     }
   );
   ```

### Exécution du code réussi

Si la Promise associée est résolue avec succès, c'est-à-dire que l'opération asynchrone a réussi, la première fonction passée à `.then` est invoquée avec la valeur de résolution en tant qu'argument.

   ```javascript
   maPromise.then(function(resultat) {
     console.log('Opération réussie avec le résultat :', resultat);
   });
   ```

### Exécution du code en cas d'erreur (optionnel)

Si la Promise est rejetée (l'opération asynchrone a échoué), la deuxième fonction passée à `.then` (la fonction de rejet) est invoquée avec la raison du rejet en tant qu'argument.

   ```javascript
   maPromise.then(
     function(resultatReussi) {
       // Code à exécuter en cas de réussite
     },
     function(raisonRejet) {
       console.error('Opération échouée avec la raison :', raisonRejet);
     }
   );
   ```

### Chaînage de `.then`

Plusieurs appels `.then` peuvent être chaînés, formant une séquence d'opérations asynchrones à exécuter en cas de réussite.

   ```javascript
   maPromise
     .then(function(resultat1) {
       // Code pour traiter le résultat1
       return resultat1 * 2;
     })
     .then(function(resultat2) {
       // Code pour traiter le résultat2
       console.log('Résultat final :', resultat2);
     });
   ```

### Retour de Promesse

Chaque `.then` renvoie une nouvelle Promesse, permettant ainsi le chaînage de méthodes `.then` ou `.catch`. Cette Promesse résultante représente la valeur retournée par la fonction de traitement du `.then`.

   ```javascript
   maPromise
     .then(function(resultat) {
       return resultat * 2;
     })
     .then(function(resultatFinal) {
       console.log('Résultat final :', resultatFinal);
     });
   ```

## .catch & .finally

Les méthodes `.catch` et `.finally` permettent de gérer respectivement les erreurs et les actions à effectuer indépendamment du succès ou de l'échec de la Promesse.

### `.catch`

La méthode `.catch` est utilisée pour gérer les erreurs survenues lors de la résolution ou du rejet d'une Promesse. Elle prend en argument une fonction qui sera appelée en cas de rejet de la Promesse.

```javascript
maPromesse.then((resultat) => {
  // Traitement réussi
  console.log(resultat);
}).catch((erreur) => {
  // Traitement en cas d'erreur
  console.error(erreur);
});
```

**Attention important :** Si dans un .then on a déjà fourni une seconde méthode pour gérer le cas d'erreur de la promesse, alors c'est cette fonction qui sera appelée et l'erreur ne sera pas propagée jusqu'au .catch.

### `.finally`

La méthode `.finally` est utilisée pour définir des actions qui doivent être exécutées indépendamment du succès ou de l'échec de la Promesse.

```javascript
maPromesse.then((resultat) => {
  // Traitement réussi
  console.log(resultat);
}).catch((erreur) => {
  // Traitement en cas d'erreur
  console.error(erreur);
}).finally(() => {
  // Actions à effectuer indépendamment du succès ou de l'échec
  console.log('Opération terminée, que la Promesse soit résolue ou rejetée.');
});
```

**Combinaison de `.catch` et `.finally` :**

```javascript
maPromesse
  .then((resultat) => {
    // Traitement réussi
    console.log(resultat);
  })
  .catch((erreur) => {
    // Traitement en cas d'erreur
    console.error(erreur);
  })
  .finally(() => {
    // Actions à effectuer indépendamment du succès ou de l'échec
    console.log('Opération terminée, que la Promesse soit résolue ou rejetée.');
  });
```

## Fetch

---

[...retour au sommaire](../sommaire.md)