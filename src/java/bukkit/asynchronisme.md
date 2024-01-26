# Bukkit - Asynchronisme

[...retour au sommaire](../sommaire.md)

---

### Rappel

L'asynchronisme fait référence à l'exécution de tâches de manière indépendante, sans bloquer le fil d'exécution principal. En d'autres termes, au lieu d'attendre que chaque tâche se termine avant de passer à la suivante, le programme peut continuer son exécution tout en attendant que certaines tâches se terminent en arrière-plan.

### Utilisation

D'abord, on récupère une instance de `BukkitSheduler` :

```java
BukkitScheduler scheduler = getServer().getScheduler();
```

Ensuite, on créé une méthode asynchrone :

```java
public CompletableFuture<String> maMethodeAsynchrone() {
    ...
    ...
    CompletableFuture<String> monMessage = CompletableFuture.supplyAsync(() -> autreMethodeAsynchrone());

    return monMessage;
}

public String autreMethodeAsynchrone() {
    ...
    // on fait des trucs potentiellement asynchrones
    return "coucou";
}

// Plus tard dans le code pour appeler ces méthodes :
scheduler.runTaskAsynchronously(this, () -> {
            // Code à exécuter de manière asynchrone
            CompletableFuture<String> future = maMethodeAsynchrone();

            // Gestion du résultat de manière synchrone
            future.thenAccept(result -> {
                // Code à exécuter avec le résultat
                String monStringRecupere = result;
            });
        });
```

* le `thenAccept` permet de spécifier du code à exécuter une fois que le résultat est récupéré.

* CompletableFuture permet de manipuler les méthodes asynchrones. C'est autant un type permettant de représenter la tâche lancée (comme Task en c#) qu'une interface permettant de lancer des méthodes de manipulation de l'asynchronisme comme `supplyAsync`.

### Mettre un délai avant l'exécution d'une tâche

Si on veut attendre un certains temps avant de faire une tâche, au lieu d'utiliser la méthode `runTaskAsynchronously`, on aurait pu utiliser `runTaskLaterAsynchronously`, qui permet de préciser un délais (en ticks) avant l'exécution.

**Exemple :**

```java
BukkitScheduler scheduler = getServer().getScheduler();

scheduler.runTaskLaterAsynchronously(this, () -> {
    // Code à exécuter...
    ...
    ...
}, 20 * 60);

// 1 seconde = 20 ticks
// donc 20 * 60 --> 1 min
```

---

[...retour au sommaire](../sommaire.md)