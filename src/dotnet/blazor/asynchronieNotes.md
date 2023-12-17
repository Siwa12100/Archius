# bases de l'asynchronisme en C #

[...retour aux notes sur le cours de Blazor](./resumeDuCours.md)

---

L'asynchronisme est le fait de pouvoir lancer des tâches au sein de l'application qui s'exécutent en parralèle du programme principal (thread principal...).

Le but est de ne pas avoir à attendre le retour d'une fonction avant de continuer à faire fonctionner l'application, et donc d'éviter des lags côté utilisateur liés à une application qui ne répond plus en gros.

Le concept de manière générale est donc de lancer une tâche de manière asynchrone (qui elle même peut lancer au besoin d'autres tâches de manière asynchrone...), d'effectuer une suite d'action indépendant de la tâche lancée, puis au final, d'attendre la fin de la tâche asynchrone, en bloquant le thread depuis lequel on l'a lancé.

## await et async

Ces deux mots correspondent à la base de la gestion de méthodes asynchrones en c#.

### async

C'est lui qui va nous permettre de déclarer une méthode asynchrone. Dans la manière de l'employer, d'un point de vue syntaxique, il ressemble un peu à un override par exemple.

**Déclaration classique d'une méthode asynchrone :**

```c#
public async Task methodeAsynchrone(String val) {
    ...;
    ...;
    Console.WriteLine(val);
    ...;
    ...;
}
```

Il est aussi possible de déclarer des méthodes asynchrones qui renvoient des valeurs de retour :

```c#
public static async Task<UneClass> methodeAsynchrone2() {
    UneClass c = new UneClasse();
    ...;
    ...:
    return c;
}
```

### await

C'est grâce au mot clé `await` que l'on va être en mesure de manipuler proprement des méthodes asynchrones comme celles-ci définies au dessus.

Le but est ainsi de lancer une méthode asynchrone, puis d'effectuer certains actions indépendantes de la méthode asynchrone, et une fois que l'on souhaite attendre la fin de la méthode asynchrone, on bloque le thread (=fil d'exécution en gros) courant.

**Exemple classique :**

```c#
// on reprend les méthodes utilisées au dessus...
// on imagine que les méthodes au dessus ont été déclarées dans une classe Caillou...

var tache1 = Caillou.methodeAsynchrone("Coucou");
// On imagine que la tache1 prend pas mal de temps à se faire...
// mtn on fait du code indépendant de tâche1 
Console.WriteLine("Exemple de code...");

// Ensuite on attend la fin de la tache1 et on bloque le fil courant jusqu'à sa fin : 
await tache1;
```

De la même manière, on peut attendre une valeur à la fin de la fonction asynchrone, et on peut directement attendre la fin d'une méthode asynchrone sans passer par une variable qui stocke la tâche :

```c#
// Le code ne se continuera qu'après la fin de la méthode suivante :
await Caillou.methodeAsynchrone("On attend que je me finisse");

// On stocke la tâche correspondant à la méthode 2 : 
var tache2 = Caillou.methodeAsynchrone2();

Console.WriteLine("On exécute du code en attendant...");

// On attend que la méthode 2 se termine et on stocke l'instance qu'elle renvoie
UneClasse instance = await tache2;
```

### Faire attendre une fonction un certains temps

Astuce utile, en utilisant le principe du await, il est possible de mettre en pause du code pendant une période donnée avec le code suivant :

```c#
await Task.Delay(10 * 1000);
```

La valeur à mettre en paramètre est en milisecondes, donc là on attend 10 secondes...

## Gérer des tâches en parallèle

Les mots clés `WhenAny` et `WhenAll` permettent de gérer l'exécution de plusieurs tâches aynchrones en même temps.

### WhenAll

Le but va être de stocker différentes tâches aysnchrones dans une liste. On va ensuite faire un `WhenAll` sur cette liste, qui correspond à créer une tâche qui se termine lorsque toute les tâches de la liste sont terminées.

C'est un moyen de suivre l'exécution de plusieurs tâches en ne manipulant qu'une seule tâche.

**Exemple :**

```c#
List<Task> maListe = new List<Task>;
maListe.add(methodeAsync1());
maListe.add(methodeAsync2());
maListe.add(methodeAsync3());

await Task.WhenAll(maListe);
```

### WhenAny

`WhenAny`est très similaire à `WhenAll`, mais au lieu de renvoyer une tâche qui se termine quand toutes les tâches de la liste se terminent, elle renvoie une tâche qui se termine simplement dès qu'une seule tâche se termine.

**Exemple :**
```c#
List<Task> maListe = new List<Task>;
maListe.add(methodeAsync1());
maListe.add(methodeAsync2());
maListe.add(methodeAsync3());

await Task.WhenAny(maListe);
```

### Gestion des résultats

Si les tâches de la liste renvoient un résultat, il est possible de récupérer les résultats dans un tableau. 

**Exemple :**
```c#
List<Task<int>> maListe = new List<Task<int>>;
maListe.add(methodeAsync1());
maListe.add(methodeAsync2());
maListe.add(methodeAsync3());

// On récupère les valeurs des tâches
int [] tab = await Task.WhenAll(maListe);
```