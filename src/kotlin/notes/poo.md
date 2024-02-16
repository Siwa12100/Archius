# Kotlin Poo - Notes

[...retour au sommaire](../sommaire.md)

---

## Classes et Objets en Kotlin

### Squelette de classe standard en Java (POJO)
En Java, la création d'une classe standard implique la définition de getters, setters, hashCode, equals, et toString. Voici un exemple :

```java
class Person {
    private String name;
    private int age;

    public Person(String name, int age) {
        this.name = name;
        this.age = age;
    }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }
    public String getAge() { return age; }
    public void setAge(int age) { this.age = age; }

    // hashCode, equals, toString overrides
}
```

### Équivalent en Kotlin
En Kotlin, la syntaxe est plus concise. Voici le même exemple en Kotlin :

```kotlin
class Person(var name: String, var age: Int) {
    override fun hashCode(): Int { ... }
    override fun equals(): Boolean { ... }
    override fun toString() = "Person(name=$name, age=$age)"
}
```

L'instanciation est également simplifiée, pas besoin de `new` :

```kotlin
val moi = Person("Laurent Provot", 27)
```

## Visibilités en Kotlin

Les modificateurs de visibilité déterminent la portée d'une classe.

- **Public**: accessible partout.
- **Private**: limité au fichier de déclaration.
- **Internal**: accessible dans le même module.
- **Protected**: comme private, mais accessible dans les classes dérivées.

Un module est un ensemble de fichiers Kotlin compilés ensemble.

## Constructeurs (Ctor) en Kotlin

### Constructeur principal
Le constructeur principal ne permet pas de spécifier du code immédiatement.

```kotlin
class Person constructor(name: String) { ... }
```

Bloc d'initialisation : utilisation des paramètres du constructeur principal.

```kotlin
class Person(name: String) {
    private val nameUpper = name.uppercase()
    
    init {
        println("My name is: $name. What? I said $nameUpper")
    }
}
```

### Constructeurs secondaires
Kotlin prend en charge les constructeurs secondaires.

```kotlin
class Person(name: String) {
    private val upperName = name.uppercase()
    private var age = 0
    
    constructor(name: String, age: Int) : this(name) {
        this.age = age
    }
    
    constructor(codename: Int) : this(decipher(codename), 42)
}
```

La délégation au constructeur principal est obligatoire, et tous les blocs d'initialisation sont appelés en premier.

### Annotations et modificateurs
On peut spécifier des annotations ou des modificateurs dans la déclaration du constructeur.

```kotlin
class Person private constructor(name: String) { ... }
class Person @Inject constructor(name: String) { ... }
```

## Propriétés en Kotlin

Les propriétés peuvent être déclarées avec `val` pour une lecture seule et `var` pour une lecture/écriture.

```kotlin
class Person {
    var name = "John Doe"
}
```

L'accès et la modification se font avec le getter et le setter respectivement.

```kotlin
val john = Person()
val name = john.name // accès avec le getter
john.name = "Johnny" // modification avec le setter
```

Pour initialiser les propriétés dans le constructeur :

```kotlin
class Person(val firstName: String, var age: Int) { ... }
```

## Propriétés & Getter/Setter en Kotlin

La syntaxe complète pour définir une propriété avec getter et setter :

```kotlin
var <propertyName>[: PropertyType] [= <initializer>]
    [<getter>]
    [<setter>]
```

Les propriétés personnalisées permettent d'accéder à l'attribut avec `field`.

```kotlin
var speed: Int
    get() = field * 100
    set(value) {
        if (value >= 0) field = value
    }
```

## Propriétés Calculées en Kotlin

Les propriétés calculées fournissent un attribut (backing field) uniquement si nécessaire.

```kotlin
val isEmpty: Boolean
    get() = this.size == 0
```

On peut changer la visibilité ou ajouter des annotations tout en conservant l'implémentation par défaut.

```kotlin
var devOnly: String
    @NotNull get
    private set
```

Ceci résume les concepts fondamentaux liés à la création d'objets en Kotlin, couvrant la déclaration de classes, les constructeurs, les visibilités, et les propriétés.