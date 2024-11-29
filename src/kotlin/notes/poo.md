# Kotlin POO - Notes

[...retour au sommaire](../sommaire.md)

---

## **Déclaration de Classe en Kotlin**

En Kotlin, la déclaration d'une classe est simplifiée par rapport à Java. Prenons l'exemple d'une classe `Person` avec des propriétés, un hashCode, une fonction equals, et une représentation en chaîne.

```kotlin
class Person(var name: String, var age: Int) {
    override fun hashCode(): Int { 
        // Logique de génération de hashCode
    }
    
    override fun equals(other: Any?): Boolean {
        // Logique de comparaison avec un autre objet
    }
    
    override fun toString(): String {
        return "Person(name=$name, age=$age)"
    }
}
```

### **Instanciation Facilitée**

L'instanciation d'objets en Kotlin est simplifiée, éliminant le besoin du mot-clé `new`.

```kotlin
val moi = Person("Laurent Provot", 27)
```

---

## **Visibilités en Kotlin**

Les modificateurs de visibilité déterminent la portée d'une classe ou de ses membres.

- **Public**: Accessible partout. C'est la visibilité par défaut.
- **Private**: Limité à la classe ou au fichier de déclaration.
- **Internal**: Accessible uniquement dans le même module.
- **Protected**: Comme `private`, mais accessible dans les classes dérivées.

### **Exemples**

```kotlin
class Person {
    private var name = "John"
    internal var age = 30
    protected var address = "Unknown"
    public var gender = "Male"
}
```

---

## **Constructeurs (Ctor) en Kotlin**

### **Constructeur Principal**

Le constructeur principal est intégré directement dans la déclaration de la classe. Si nécessaire, on peut utiliser un bloc d'initialisation pour exécuter des instructions.

```kotlin
class Person(val name: String, var age: Int) {
    init {
        println("Person initialized: $name, $age")
    }
}
```

### **Constructeurs Secondaires**

Les constructeurs secondaires sont définis pour offrir des alternatives lors de la création d'objets. Ils doivent déléguer au constructeur principal.

```kotlin
class Person(val name: String) {
    var age: Int = 0

    constructor(name: String, age: Int) : this(name) {
        this.age = age
    }
}
```

### **Annotations et Modificateurs**

Vous pouvez spécifier des annotations ou des modificateurs dans la déclaration du constructeur.

```kotlin
class Person @Inject constructor(name: String)
```

---

## **Propriétés en Kotlin**

Les propriétés remplacent les champs et les méthodes getter/setter explicites de Java.

### **Propriétés avec `val` et `var`**

- `val`: Lecture seule (équivalent à `final` en Java).
- `var`: Lecture et écriture.

```kotlin
class Person(val name: String, var age: Int)
```

### **Propriétés Calculées**

Une propriété peut être calculée dynamiquement avec un `getter`.

```kotlin
val isAdult: Boolean
    get() = age >= 18
```

---

## **Data Classes**

### **Introduction**

Les `data class` sont conçues pour les objets de données simples. Kotlin génère automatiquement les méthodes `equals`, `hashCode`, `toString`, `copy`, et des méthodes pour la déstructuration.

```kotlin
data class Person(val name: String, val age: Int)
```

### **Méthode `copy`**

La méthode `copy` permet de créer une nouvelle instance en modifiant certaines propriétés.

```kotlin
val john = Person("John", 30)
val youngJohn = john.copy(age = 20)
```

### **Déstructuration**

Les `data class` permettent la déstructuration.

```kotlin
val (name, age) = john
println("$name is $age years old")
```

---

## **Héritage**

### **Base et Dérivation**

En Kotlin, les classes ne sont pas héritables par défaut. Utilisez `open` pour permettre l'héritage.

```kotlin
open class Animal(val name: String)

class Dog(name: String, val breed: String) : Animal(name)
```

### **Redéfinition**

Les méthodes ou propriétés peuvent être redéfinies avec le mot-clé `override`.

```kotlin
open class Animal {
    open fun sound() {
        println("Generic sound")
    }
}

class Dog : Animal() {
    override fun sound() {
        println("Bark")
    }
}
```

---

## **Interfaces**

Les interfaces définissent des contrats. Une classe peut en implémenter plusieurs.

```kotlin
interface Drivable {
    fun drive()
}

class Car : Drivable {
    override fun drive() {
        println("Driving a car")
    }
}
```

### **Propriétés dans les Interfaces**

Les interfaces peuvent définir des propriétés, mais elles doivent être soit abstraites, soit accompagnées d'un getter par défaut.

```kotlin
interface Named {
    val name: String
        get() = "Unnamed"
}
```

---

## **`object` et Singleton**

### **Objet Simple**

Kotlin fournit le mot-clé `object` pour créer un singleton.

```kotlin
object Database {
    fun connect() {
        println("Connected to database")
    }
}
```

### **Companion Object**

Les `companion object` permettent de définir des membres statiques pour une classe.

```kotlin
class Person {
    companion object {
        fun create(name: String): Person {
            return Person(name)
        }
    }
}

val john = Person.create("John")
```

---

## **Surcharge d'Opérateurs**

Kotlin permet de surcharger les opérateurs.

```kotlin
data class Vector(val x: Int, val y: Int) {
    operator fun plus(other: Vector) = Vector(x + other.x, y + other.y)
}
```

---

## **Classes Imbriquées**

### **Classes `Nested`**

Une classe `nested` ne peut pas accéder aux membres de la classe englobante.

```kotlin
class Outer {
    class Nested {
        fun greet() = "Hello from Nested"
    }
}
```

### **Classes `Inner`**

Une classe `inner` peut accéder aux membres de la classe englobante.

```kotlin
class Outer(val message: String) {
    inner class Inner {
        fun greet() = "Hello from $message"
    }
}
```

---

## **Sealed Classes**

Les `sealed class` limitent l'héritage à un ensemble connu de sous-classes.

```kotlin
sealed class Shape {
    data class Circle(val radius: Double) : Shape()
    data class Rectangle(val width: Double, val height: Double) : Shape()
}

fun describe(shape: Shape): String = when (shape) {
    is Shape.Circle -> "Circle with radius ${shape.radius}"
    is Shape.Rectangle -> "Rectangle with width ${shape.width} and height ${shape.height}"
}
```

---

## **Extensions**

Kotlin permet d'ajouter des fonctions à des classes existantes sans modifier leur code source.

```kotlin
fun String.isPalindrome(): Boolean {
    return this == this.reversed()
}

println("radar".isPalindrome()) // true
```

---

## **Coroutines et Gestion Asynchrone**

Bien que non directement liées à la POO, les coroutines sont essentielles pour les projets Kotlin modernes. Elles permettent une gestion efficace des tâches asynchrones.

```kotlin
suspend fun fetchData(): String {
    delay(1000) // Simulation d'une tâche asynchrone
    return "Data fetched"
}

fun main() = runBlocking {
    println(fetchData())
}
```

