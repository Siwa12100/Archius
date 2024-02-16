# Kotlin Poo - Notes

[...retour au sommaire](../sommaire.md)

---

## Déclaration de Classe en Kotlin

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

### Instanciation Facilitée

L'instanciation d'objets en Kotlin est simplifiée, éliminant le besoin du mot-clé `new`.

```kotlin
val moi = Person("Laurent Provot", 27)
```

## Visibilités en Kotlin

Les modificateurs de visibilité déterminent la portée d'une classe en Kotlin.

- **Public**: Accessible partout.
- **Private**: Limité au fichier de déclaration.
- **Internal**: Accessible dans le même module.
- **Protected**: Comme private, mais accessible dans les classes dérivées.

Un module regroupe des fichiers Kotlin compilés ensemble, avec des implications différentes selon la plateforme (par exemple, module pour IntelliJ IDEA ou source set pour Android via Gradle).

## Constructeurs (Ctor) en Kotlin

### Constructeur Principal

Le constructeur principal permet la déclaration initiale sans spécifier de code immédiat. Cependant, on peut utiliser un bloc d'initialisation pour exécuter du code au moment de la création de l'objet.

```kotlin
class Person(name: String) {
    private val nameUpper: String
    
    init {
        nameUpper = name.uppercase()
        println("My name is: $name. What? I said $nameUpper")
    }
}
```

### Constructeurs Secondaires

Kotlin offre une flexibilité accrue avec les constructeurs secondaires. Reprenons l'exemple de la classe `Person` en montrant comment créer des instances avec différents constructeurs.

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

Délégation au constructeur principal : Obligatoire, et tous les blocs d'initialisation sont appelés en premier.

### Annotations et Modificateurs

Les annotations et les modificateurs peuvent être ajoutés dans la déclaration du constructeur.

```kotlin
class Person private constructor(name: String) {
    // Logique de la classe
}
class Person @Inject constructor(name: String) {
    // Logique de la classe avec annotation Inject
}
```

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