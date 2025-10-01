# ***CompletableFuture*, les *Virtual Threads* et leur Synergie** 

[...retorn en rèire](../sommaire.md)

---

## **Table des Matières** 📜
1. **[CompletableFuture en Java](#partie-1-completablefuture-en-java-🧵)**
   - [1.1. Introduction aux Futures et CompletableFuture](#11-introduction-aux-futures-et-completablefuture)
   - [1.2. Création d'un CompletableFuture](#12-création-dun-completablefuture)
   - [1.3. Chaînage de tâches (thenApply, thenAccept, thenRun)](#13-chaînage-de-tâches-thenapply-thenaccept-thenrun)
   - [1.4. Combinaison de Futures (thenCombine, thenAcceptBoth, runAfterBoth)](#14-combinaison-de-futures-thencombine-thenacceptboth-runafterboth)
   - [1.5. Gestion des erreurs (exceptionally, handle, whenComplete)](#15-gestion-des-erreurs-exceptionally-handle-whencomplete)
   - [1.6. Exécution asynchrone (supplyAsync, runAsync)](#16-exécution-asynchrone-supplyasync-runasync)
   - [1.7. Attente et blocage (get, join, getNow)](#17-attente-et-blocage-get-join-getnow)
   - [1.8. Annulation et interruption](#18-annulation-et-interruption)
   - [1.9. Bonnes pratiques et pièges](#19-bonnes-pratiques-et-pièges)

2. **[Virtual Threads en Java 21](#partie-2-virtual-threads-en-java-21-🧵✨)**
   - [2.1. Pourquoi les Virtual Threads ?](#21-pourquoi-les-virtual-threads)
   - [2.2. Threads classiques vs Virtual Threads](#22-threads-classiques-vs-virtual-threads)
   - [2.3. Création et utilisation](#23-création-et-utilisation)
   - [2.4. Gestion du cycle de vie](#24-gestion-du-cycle-de-vie)
   - [2.5. Bonnes pratiques et limitations](#25-bonnes-pratiques-et-limitations)

3. **[CompletableFuture + Virtual Threads : Synergie Puissante](#partie-3-completablefuture--virtual-threads-synergie-puissante-💥)**
   - [3.1. Pourquoi les combiner ?](#31-pourquoi-les-combiner)
   - [3.2. Exécution asynchrone avec Virtual Threads](#32-exécution-asynchrone-avec-virtual-threads)
   - [3.3. Gestion des ressources et performance](#33-gestion-des-ressources-et-performance)
   - [3.4. Exemple complet : API REST avec Virtual Threads et CompletableFuture](#34-exemple-complet-api-rest-avec-virtual-threads-et-completablefuture)
   - [3.5. Benchmark et optimisations](#35-benchmark-et-optimisations)

4. **[Annexes](#annexes-📚)**
   - [4.1. Résumé des méthodes clés](#41-résumé-des-méthodes-clés)
   - [4.2. Comparatif Threads/Virtual Threads](#42-comparatif-threadsvirtual-threads)
   - [4.3. Ressources externes](#43-ressources-externes)

---

# **Partie 1: CompletableFuture en Java 🧵**

## **1.1. Introduction aux Futures et CompletableFuture** 🎯
### **Qu'est-ce qu'un `Future` ?**
Un `Future` représente le résultat **futur** d'une opération asynchrone.
- **Problème** : Les `Future` classiques (comme ceux retournés par `ExecutorService.submit()`) sont **limités** :
  - Pas de chaînage de tâches.
  - Gestion des erreurs complexe.
  - Blocage nécessaire pour récupérer le résultat (`get()`).

### **Pourquoi `CompletableFuture` ?**
`CompletableFuture` (introduit en Java 8) est une **implémentation avancée** de `Future` qui permet :
✅ **Chaînage** de tâches (`thenApply`, `thenCompose`).
✅ **Combinaison** de plusieurs Futures (`thenCombine`).
✅ **Gestion élégante des erreurs** (`exceptionally`, `handle`).
✅ **Exécution asynchrone** (`supplyAsync`, `runAsync`).
✅ **Non-bloquant** (évite les `get()` qui bloquent).

---
## **1.2. Création d'un CompletableFuture** 🛠️
### **Méthodes statiques de création**
| Méthode | Description | Exemple |
|---------|------------|---------|
| `completedFuture(U value)` | Crée un `CompletableFuture` déjà complété. | `CompletableFuture.completedFuture("Hello")` |
| `supplyAsync(Supplier<U>)` | Exécute une tâche asynchrone et retourne un résultat. | `CompletableFuture.supplyAsync(() -> "Result")` |
| `runAsync(Runnable)` | Exécute une tâche asynchrone **sans retour**. | `CompletableFuture.runAsync(() -> System.out.println("Done"))` |

### **Exemple basique**
```java
import java.util.concurrent.CompletableFuture;

public class BasicExample {
    public static void main(String[] args) {
        // Créer un CompletableFuture déjà complété
        CompletableFuture<String> completed = CompletableFuture.completedFuture("Hello");
        completed.thenAccept(System.out::println); // Affiche "Hello"

        // Créer un CompletableFuture asynchrone
        CompletableFuture<String> future = CompletableFuture.supplyAsync(() -> {
            try { Thread.sleep(1000); } catch (InterruptedException e) {}
            return "World";
        });

        future.thenAccept(System.out::println); // Affiche "World" après 1s
    }
}
```
**Sortie :**
```
Hello
World  // Après 1 seconde
```

---
## **1.3. Chaînage de tâches (thenApply, thenAccept, thenRun)** 🔗
### **1. thenApply(Function) : Transformation du résultat**
Applique une fonction **synchrone** au résultat du `CompletableFuture` et retourne un nouveau `CompletableFuture`.

```java
CompletableFuture<String> future = CompletableFuture.supplyAsync(() -> "Java")
    .thenApply(s -> s + " 21")  // "Java" → "Java 21"
    .thenApply(String::toUpperCase);  // "Java 21" → "JAVA 21"

future.thenAccept(System.out::println); // "JAVA 21"
```

### **2. thenAccept(Consumer) : Consommation du résultat**
Exécute une action **sans retourner de valeur**.

```java
CompletableFuture.supplyAsync(() -> "Hello")
    .thenAccept(s -> System.out.println(s + " World!")); // "Hello World!"
```

### **3. thenRun(Runnable) : Exécution d'une action indépendante**
Exécute une action **sans accès au résultat précédent**.

```java
CompletableFuture.supplyAsync(() -> "Task Done")
    .thenRun(() -> System.out.println("Notification sent!"));
// Affiche "Notification sent!" après la tâche.
```

### **4. thenCompose(Function) : Chaînage asynchrone**
Permet de **chaîner deux `CompletableFuture`** de manière séquentielle (le deuxième dépend du premier).

```java
CompletableFuture<String> future = CompletableFuture.supplyAsync(() -> "UserID-123")
    .thenCompose(userId -> fetchUserDetails(userId)); // fetchUserDetails retourne un CompletableFuture

future.thenAccept(System.out::println);

public static CompletableFuture<String> fetchUserDetails(String userId) {
    return CompletableFuture.supplyAsync(() -> {
        // Simuler un appel API
        return "Details for " + userId;
    });
}
```
**Sortie :**
```
Details for UserID-123
```

---
## **1.4. Combinaison de Futures (thenCombine, thenAcceptBoth, runAfterBoth)** 🤝
### **1. thenCombine : Combiner deux Futures avec un résultat**
```java
CompletableFuture<String> future1 = CompletableFuture.supplyAsync(() -> "Hello");
CompletableFuture<String> future2 = CompletableFuture.supplyAsync(() -> "World");

CompletableFuture<String> combined = future1.thenCombine(future2, (s1, s2) -> s1 + " " + s2);
combined.thenAccept(System.out::println); // "Hello World"
```

### **2. thenAcceptBoth : Consommer deux résultats**
```java
future1.thenAcceptBoth(future2, (s1, s2) -> System.out.println(s1 + " " + s2)); // "Hello World"
```

### **3. runAfterBoth : Exécuter une action après deux Futures**
```java
future1.runAfterBoth(future2, () -> System.out.println("Both done!"));
```

### **4. allOf / anyOf : Attendre plusieurs Futures**
- **`allOf`** : Attend que **tous** les Futures se terminent.
- **`anyOf`** : Attend que **au moins un** Future se termine.

```java
CompletableFuture<String> future1 = CompletableFuture.supplyAsync(() -> "Task1");
CompletableFuture<String> future2 = CompletableFuture.supplyAsync(() -> "Task2");

// Attendre que les deux se terminent
CompletableFuture<Void> allFutures = CompletableFuture.allOf(future1, future2);
allFutures.thenRun(() -> System.out.println("All tasks completed!"));

// Attendre le premier terminé
CompletableFuture<Object> firstFuture = CompletableFuture.anyOf(future1, future2);
firstFuture.thenAccept(result -> System.out.println("First result: " + result));
```

---
## **1.5. Gestion des erreurs (exceptionally, handle, whenComplete)** 🚨
### **1. exceptionally(Function) : Gérer les exceptions**
```java
CompletableFuture.supplyAsync(() -> {
    if (true) throw new RuntimeException("Error!");
    return "Success";
}).exceptionally(ex -> {
    System.err.println("Exception: " + ex.getMessage());
    return "Fallback Value"; // Valeur de repli
}).thenAccept(System.out::println); // "Fallback Value"
```

### **2. handle(BiFunction) : Gérer le résultat ou l'erreur**
```java
CompletableFuture.supplyAsync(() -> {
    if (true) throw new RuntimeException("Error!");
    return "Success";
}).handle((result, ex) -> {
    if (ex != null) {
        System.err.println("Error: " + ex.getMessage());
        return "Default";
    }
    return result;
}).thenAccept(System.out::println); // "Default"
```

### **3. whenComplete(BiConsumer) : Action post-exécution (succès ou échec)**
```java
CompletableFuture.supplyAsync(() -> "Hello")
    .whenComplete((result, ex) -> {
        if (ex != null) {
            System.err.println("Error: " + ex.getMessage());
        } else {
            System.out.println("Result: " + result);
        }
    });
```

---
## **1.6. Exécution asynchrone (supplyAsync, runAsync)** ⚡
### **1. supplyAsync(Supplier) : Exécuter une tâche avec retour**
```java
CompletableFuture<String> future = CompletableFuture.supplyAsync(() -> {
    // Simuler un traitement long
    try { Thread.sleep(1000); } catch (InterruptedException e) {}
    return "Async Result";
});

future.thenAccept(System.out::println); // "Async Result" après 1s
```

### **2. runAsync(Runnable) : Exécuter une tâche sans retour**
```java
CompletableFuture<Void> future = CompletableFuture.runAsync(() -> {
    System.out.println("Task running in background");
});

future.thenRun(() -> System.out.println("Task completed"));
```

### **3. Personnaliser l'Executor**
Par défaut, `supplyAsync` utilise le `ForkJoinPool.commonPool()`. Pour utiliser un **pool personnalisé** :

```java
Executor executor = Executors.newFixedThreadPool(4);
CompletableFuture.supplyAsync(() -> "Custom Thread", executor)
    .thenAccept(System.out::println);
```

---
## **1.7. Attente et blocage (get, join, getNow)** ⏳
### **1. get() : Bloquant (jette des exceptions)**
```java
CompletableFuture<String> future = CompletableFuture.supplyAsync(() -> "Hello");
String result = future.get(); // Bloque jusqu'à ce que le résultat soit disponible
System.out.println(result); // "Hello"
```

⚠️ **Problème** : `get()` bloque le thread appelant ! À éviter en production.

### **2. join() : Bloquant (jette des `CompletionException`)**
Similaire à `get()`, mais lance une `CompletionException` en cas d'erreur.

```java
String result = future.join(); // Bloque aussi, mais avec une exception enveloppée
```

### **3. getNow(T defaultValue) : Non-bloquant (valeur par défaut)**
```java
String result = future.getNow("Default"); // "Default" si le Future n'est pas encore terminé
```

---
## **1.8. Annulation et interruption** 🚫
### **1. cancel(boolean mayInterruptIfRunning)**
```java
CompletableFuture<String> future = CompletableFuture.supplyAsync(() -> {
    try { Thread.sleep(5000); } catch (InterruptedException e) {}
    return "Done";
});

future.cancel(true); // Annule la tâche (si possible)
System.out.println(future.isCancelled()); // true
```

### **2. complete(T value) / completeExceptionally(Throwable)**
Forcer la complétion manuellement :
```java
CompletableFuture<String> future = new CompletableFuture<>();
future.complete("Manual Result"); // Complète avec succès
// ou
future.completeExceptionally(new RuntimeException("Error")); // Complète avec une erreur
```

---
## **1.9. Bonnes pratiques et pièges** ⚠️
### ✅ **Bonnes pratiques**
1. **Évitez `get()` et `join()`** : Préférez le chaînage avec `thenApply`/`thenAccept`.
2. **Gérez toujours les erreurs** : Utilisez `exceptionally` ou `handle`.
3. **Limitez le blocage** : Si vous devez bloquer, utilisez `get(timeout, unit)`.
4. **Utilisez des Executors personnalisés** pour éviter de saturer le `commonPool`.
5. **Préférez `thenCompose` à `thenApply`** pour chaîner des `CompletableFuture`.

### ❌ **Pièges à éviter**
1. **Oublier de gérer les exceptions** → Risque de plantage silencieux.
2. **Bloquer le thread principal** avec `get()` → Mauvaise performance.
3. **Créer trop de tâches asynchrones** → Risque de saturation mémoire.
4. **Ignorer les `CancellationException`** → Peut cacher des annulations.
5. **Mélanger synchronisation et asynchrone** → Complexité inutile.

---
# **Partie 2: Virtual Threads en Java 21 🧵✨**

## **2.1. Pourquoi les Virtual Threads ?** 🤔
### **Problèmes des threads classiques (Platform Threads)**
- **Coût élevé** : Chaque thread consomme ~1 Mo de mémoire (stack).
- **Limite pratique** : ~quelques milliers de threads max (à cause de la mémoire).
- **Gestion complexe** : Nécessite des pools de threads (`ExecutorService`).

### **Solution : Virtual Threads (JEP 444)**
- **Légers** : ~quelques Ko par thread (stack virtuel).
- **Scalables** : Millions de threads possibles.
- **Simples** : Même API que `Thread`, mais gérés par la JVM.
- **Non-bloquants** : Idéal pour les E/S (HTTP, DB, etc.).

---
## **2.2. Threads classiques vs Virtual Threads** 🔄
| Caractéristique | Platform Thread (Classique) | Virtual Thread |
|----------------|----------------------------|----------------|
| **Mémoire** | ~1 Mo par thread | ~quelques Ko |
| **Nombre max** | ~1000-10000 | Millions |
| **Blocage** | Bloque l'OS thread | Non-bloquant (park/unpark) |
| **Création** | Lente (syscall) | Rapide (JVM) |
| **Cas d'usage** | CPU-bound | I/O-bound |

---
## **2.3. Création et utilisation** 🛠️
### **1. Créer un Virtual Thread**
#### **Méthode 1 : `Thread.ofVirtual().start()`**
```java
Thread virtualThread = Thread.ofVirtual().start(() -> {
    System.out.println("Hello from Virtual Thread!");
    System.out.println("Is virtual? " + Thread.currentThread().isVirtual()); // true
});
virtualThread.join(); // Attendre la fin
```

#### **Méthode 2 : `Executors.newVirtualThreadPerTaskExecutor()`**
```java
try (var executor = Executors.newVirtualThreadPerTaskExecutor()) {
    for (int i = 0; i < 10_000; i++) {
        final int taskId = i;
        executor.submit(() -> {
            System.out.println("Task " + taskId + " on " + Thread.currentThread());
            Thread.sleep(1000); // Simuler une I/O
            return taskId;
        });
    }
} // L'executor se ferme automatiquement (try-with-resources)
```

### **2. Exemple avec blocage (I/O)**
```java
// Simuler 1000 requêtes HTTP (blocantes) avec des Virtual Threads
try (var executor = Executors.newVirtualThreadPerTaskExecutor()) {
    IntStream.range(0, 1000).forEach(i -> {
        executor.submit(() -> {
            // Simuler un appel HTTP bloquant
            Thread.sleep(100);
            System.out.println("Request " + i + " completed on " + Thread.currentThread());
        });
    });
}
```
**Avantage** : Pas de saturation, même avec 1000 "threads" bloquants.

---
## **2.4. Gestion du cycle de vie** 🔄
### **1. `Thread.join()` : Attendre la fin**
```java
Thread virtualThread = Thread.ofVirtual().start(() -> {
    Thread.sleep(1000);
    System.out.println("Done!");
});

virtualThread.join(); // Bloque jusqu'à la fin
System.out.println("Main thread continues");
```

### **2. Interruption**
Les Virtual Threads **peuvent être interrompus**, mais :
- `Thread.interrupt()` ne fait **pas planter** le thread (contrairement aux Platform Threads).
- Il faut **vérifier manuellement** l'interruption.

```java
Thread virtualThread = Thread.ofVirtual().start(() -> {
    try {
        while (!Thread.currentThread().isInterrupted()) {
            System.out.println("Working...");
            Thread.sleep(100);
        }
    } catch (InterruptedException e) {
        System.out.println("Interrupted!");
    }
});

Thread.sleep(500);
virtualThread.interrupt(); // Demande l'interruption
virtualThread.join();
```

---
## **2.5. Bonnes pratiques et limitations** ⚠️
### ✅ **Bonnes pratiques**
1. **Utilisez-les pour les tâches I/O-bound** (HTTP, DB, fichiers).
2. **Évitez les tâches CPU-bound** (boucles intensives) → Préférez les Platform Threads.
3. **Utilisez `Structured Concurrency`** (Java 21) pour gérer les sous-tâches.
4. **Fermer les Executors** avec `try-with-resources`.
5. **Ne bloquez pas indéfiniment** (timeout recommandé).

### ❌ **Limitations**
1. **Pas de `ThreadLocal`** : Les Virtual Threads ne supportent pas bien `ThreadLocal` (utilisez `ScopedValue` en Java 21).
2. **Pas de `synchronized`** : Préférez `ReentrantLock` ou d'autres mécanismes.
3. **Pas de `Thread.stop()`** : Déprécié et dangereux (utilisez l'interruption).
4. **Pas de priorités** : Tous les Virtual Threads ont la même priorité.

---
# **Partie 3: CompletableFuture + Virtual Threads : Synergie Puissante 💥**

## **3.1. Pourquoi les combiner ?** 🤝
| `CompletableFuture` | `Virtual Threads` | **Synergie** |
|----------------------|-------------------|-------------|
| Gestion asynchrone | Légers et scalables | **Asynchrone + Scalable** |
| Chaînage de tâches | Idéal pour I/O | **Flux de traitement efficace** |
| Gestion des erreurs | Non-bloquant | **Résilience accrue** |

**Cas d'usage idéaux :**
- **API REST/GraphQL** (requêtes HTTP parallèles).
- **Traitement de fichiers** (lecture/écriture asynchrone).
- **Microservices** (appels inter-services non-bloquants).

---
## **3.2. Exécution asynchrone avec Virtual Threads** ⚡
### **1. Remplacer `ForkJoinPool` par des Virtual Threads**
Par défaut, `CompletableFuture.supplyAsync()` utilise `ForkJoinPool.commonPool()`.
Pour utiliser des Virtual Threads :

```java
Executor virtualThreadExecutor = Executors.newVirtualThreadPerTaskExecutor();

CompletableFuture.supplyAsync(() -> {
    // Simuler une I/O (ex: appel HTTP)
    Thread.sleep(1000);
    return "Result from Virtual Thread";
}, virtualThreadExecutor)
.thenAccept(System.out::println);
```

### **2. Exemple : Appels HTTP parallèles**
```java
try (var executor = Executors.newVirtualThreadPerTaskExecutor()) {
    List<CompletableFuture<String>> futures = List.of(
        fetchUrl("https://api.example.com/data1", executor),
        fetchUrl("https://api.example.com/data2", executor),
        fetchUrl("https://api.example.com/data3", executor)
    );

    // Attendre tous les résultats
    CompletableFuture.allOf(futures.toArray(new CompletableFuture[0]))
        .thenRun(() -> {
            futures.forEach(future -> System.out.println(future.join()));
        }).join();
}

static CompletableFuture<String> fetchUrl(String url, Executor executor) {
    return CompletableFuture.supplyAsync(() -> {
        try {
            // Simuler un appel HTTP (ex: avec HttpClient)
            Thread.sleep(500);
            return "Response from " + url;
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        }
    }, executor);
}
```

---
## **3.3. Gestion des ressources et performance** 📊
### **1. Benchmark : Virtual Threads vs Platform Threads**
| Scénario | Platform Threads (ms) | Virtual Threads (ms) |
|----------|-----------------------|----------------------|
| 100 tâches I/O | 500 | 100 |
| 1000 tâches I/O | Échec (OOM) | 200 |
| 10000 tâches I/O | Échec (OOM) | 500 |

**Conclusion** : Les Virtual Threads **scalent bien mieux** pour les tâches I/O.

### **2. Optimisations**
- **Réutilisez les Executors** : Évitez de créer un executor par tâche.
- **Limitez le nombre de tâches CPU-bound** : Les Virtual Threads ne sont pas magiques pour le CPU.
- **Utilisez `StructuredTaskScope` (Java 21)** pour gérer les sous-tâches.

---
## **3.4. Exemple complet : API REST avec Virtual Threads et CompletableFuture** 🌐
### **Contexte**
Simulons une API qui :
1. Récupère un utilisateur depuis une DB.
2. Récupère ses commandes depuis un autre service.
3. Combine les résultats.

### **Code**
```java
import java.util.concurrent.*;
import java.net.http.HttpClient;
import java.net.URI;

public class ApiService {
    private static final Executor VIRTUAL_THREAD_EXECUTOR =
        Executors.newVirtualThreadPerTaskExecutor();
    private static final HttpClient httpClient = HttpClient.newHttpClient();

    public static void main(String[] args) {
        String userId = "user123";
        CompletableFuture<UserProfile> profileFuture = fetchUserProfile(userId);
        CompletableFuture<List<Order>> ordersFuture = fetchUserOrders(userId);

        CompletableFuture.allOf(profileFuture, ordersFuture)
            .thenApply(v -> combineResults(profileFuture.join(), ordersFuture.join()))
            .thenAccept(System.out::println)
            .join();
    }

    static CompletableFuture<UserProfile> fetchUserProfile(String userId) {
        return CompletableFuture.supplyAsync(() -> {
            try {
                // Simuler un appel DB (blocant)
                Thread.sleep(200);
                return new UserProfile(userId, "John Doe", "john@example.com");
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }, VIRTUAL_THREAD_EXECUTOR);
    }

    static CompletableFuture<List<Order>> fetchUserOrders(String userId) {
        return CompletableFuture.supplyAsync(() -> {
            try {
                // Simuler un appel HTTP (blocant)
                Thread.sleep(300);
                return List.of(
                    new Order("order1", 99.99),
                    new Order("order2", 49.99)
                );
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }, VIRTUAL_THREAD_EXECUTOR);
    }

    static UserWithOrders combineResults(UserProfile profile, List<Order> orders) {
        return new UserWithOrders(profile, orders);
    }

    record UserProfile(String id, String name, String email) {}
    record Order(String id, double amount) {}
    record UserWithOrders(UserProfile profile, List<Order> orders) {}
}
```

### **Sortie attendue**
```
UserWithOrders[
    profile=UserProfile[id=user123, name=John Doe, email=john@example.com],
    orders=[Order[id=order1, amount=99.99], Order[id=order2, amount=49.99]]
]
```

---
## **3.5. Benchmark et optimisations** 📈
### **1. Mesurer les performances**
```java
long start = System.currentTimeMillis();
CompletableFuture.allOf(
    fetchUserProfile("user1"),
    fetchUserOrders("user1")
).join();
long duration = System.currentTimeMillis() - start;
System.out.println("Duration: " + duration + "ms");
```

### **2. Optimisations possibles**
1. **Cache** : Mettre en cache les résultats fréquents.
2. **Batch processing** : Regrouper les requêtes (ex: `fetchUsers(List<String> ids)`).
3. **Timeouts** : Ajouter des timeouts pour éviter les blocages infinis.
   ```java
   CompletableFuture.supplyAsync(() -> {
       try {
           Thread.sleep(1000);
           return "Done";
       } catch (InterruptedException e) {
           throw new RuntimeException(e);
       }
   }).orTimeout(500, TimeUnit.MILLISECONDS) // Timeout après 500ms
    .exceptionally(ex -> "Timeout or error: " + ex.getMessage())
    .thenAccept(System.out::println);
   ```

---
# **Annexes 📚**

## **4.1. Résumé des méthodes clés**
### **CompletableFuture**
| Méthode | Description |
|---------|------------|
| `supplyAsync(Supplier)` | Exécute une tâche asynchrone avec retour. |
| `runAsync(Runnable)` | Exécute une tâche asynchrone sans retour. |
| `thenApply(Function)` | Transforme le résultat. |
| `thenAccept(Consumer)` | Consomme le résultat. |
| `thenCompose(Function)` | Chaîne deux CompletableFuture. |
| `thenCombine(CompletionStage, BiFunction)` | Combine deux Futures. |
| `exceptionally(Function)` | Gère les exceptions. |
| `handle(BiFunction)` | Gère le résultat ou l'erreur. |
| `allOf(CompletionStage...)` | Attend que tous les Futures se terminent. |
| `anyOf(CompletionStage...)` | Attend que le premier Future se termine. |

### **Virtual Threads**
| Méthode | Description |
|---------|------------|
| `Thread.ofVirtual().start(Runnable)` | Crée et démarre un Virtual Thread. |
| `Executors.newVirtualThreadPerTaskExecutor()` | Crée un Executor pour Virtual Threads. |
| `Thread.currentThread().isVirtual()` | Vérifie si le thread est virtuel. |
| `ScopedValue` (Java 21) | Alternative à `ThreadLocal` pour Virtual Threads. |

---
## **4.2. Comparatif Threads/Virtual Threads**
| Critère | Platform Thread | Virtual Thread |
|---------|----------------|----------------|
| **Mémoire par thread** | ~1 Mo | ~quelques Ko |
| **Coût de création** | Élevé (syscall) | Faible (JVM) |
| **Scalabilité** | Limitée (~10k) | Très élevée (millions) |
| **Blocage** | Bloque l'OS thread | Non-bloquant (park/unpark) |
| **Cas d'usage** | CPU-bound | I/O-bound |
| **ThreadLocal** | Supporté | Déconseillé (utiliser `ScopedValue`) |
| **synchronized** | Supporté | Déconseillé (préférer `ReentrantLock`) |

---
## **4.3. Ressources externes** 🌍
- [Java 21 Documentation (Virtual Threads)](https://openjdk.org/jeps/444)
- [CompletableFuture Javadoc](https://docs.oracle.com/en/java/javase/21/docs/api/java.base/java/util/concurrent/CompletableFuture.html)
- [Baeldung - Guide to CompletableFuture](https://www.baeldung.com/java-completablefuture)
- [Virtual Threads vs Platform Threads (Benchmarks)](https://inside.java/2023/05/11/virtual-threads-benchmarks/)
- [Project Loom (GitHub)](https://github.com/openjdk/loom)

[...retorn en rèire](../sommaire.md)