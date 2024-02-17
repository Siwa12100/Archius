# Bases

[...retour au sommaire](../sommaire.md)

---

## Types

- Pas de types primitifs en Kotlin, tout est objet.
- Utilisation du type primitif dans la JVM, sauf en cas de généricité ou de type nullable.

### Types Numériques

- Double, Float, Long, Int, Short, Byte.
- Littéraux similaires à Java.
- Possibilité de séparateur : `1_000_000`.
- Booléens : Boolean (true et false).
  
  ```kotlin
  val theAnswer = 42 // OK, de type Int
  val doubleAnswer1: Double = 42 // KO, 42 n'est pas de type Double
  val doubleAnswer2 = 42.0 // OK
  val doubleAnswer3: Double = theAnswer.toDouble() // OK
  ```

- Types entiers non-signés : ULong, UInt, UShort, UByte.

### Types Textuels

- Char, String.
- Chaînes de caractères immuables.
- Littéraux classiques et raw strings.
  
  ```kotlin
  val str = "Hello\tTab"
  val text = """
      \n tel quel \\
      avec saut de ligne possible
      respecte l'indentation
  """.trimMargin()
  ```

### Type String

- Concaténation avec l'opérateur `+`.
- Utilisation de string templates.
  
  ```kotlin
  val name = "Laurent"
  val greetings = "Salut $name !"
  val scream = "EH HO… ${name.toUpperCase()} !"
  ```

### Tableaux

Kotlin utilise la classe `Array<T>` pour représenter les tableaux.

- Accès aux éléments avec l'opérateur `[]` :

  ```kotlin
  val numbers = arrayOf(1, 2, 3, 4, 5)
  val firstElement = numbers[0] // Accès au premier élément
  ```

- Propriété `size` pour obtenir la taille du tableau :

  ```kotlin
  val size = numbers.size // Taille du tableau
  ```

- Fonctions utilitaires pour la création et la manipulation de tableaux :

  ```kotlin
  val anotherArray = intArrayOf(10, 20, 30)
  val filteredArray = numbers.filter { it > 2 }
  ```

- Types spécifiques d'arrays (`IntArray`, `DoubleArray`, etc.) pour optimiser l'utilisation des types primitifs sur la JVM.

  ```kotlin
  val intArray = intArrayOf(1, 2, 3)
  ```

### Les Intervalles (Range et Progression)

Kotlin offre une syntaxe concise pour représenter des intervalles de valeurs. Voici quelques exemples :

- Utilisation de l'opérateur `..` pour définir un intervalle inclusif :

  ```kotlin
  val range = 1..5 // Interval de 1 à 5 inclus
  ```

- Fonctions d'extension `until`, `downTo`, `step` pour spécifier des intervalles exclusifs et avec des pas :

  ```kotlin
  val exclusiveRange = 1 until 5 // Interval de 1 à 4 exclus
  val descendingRange = 5 downTo 1 // Interval de 5 à 1 inclus, ordre décroissant
  val steppedRange = 1..10 step 2 // Interval de 1 à 10 inclus, avec un pas de 2
  ```

- Utilisation dans des conditions avec l'opérateur `in` :

  ```kotlin
  val numbers = 0..9
  if (3 in numbers) println("3 est dans l'intervalle")
  ```

- Utilisation dans des boucles `for` :

  ```kotlin
  for (i in numbers) {
      print("$i, ")
  }
  ```

## Types de Variables

### `val` - Référence une Valeur Constante (Immuabilité)

La déclaration avec `val` crée une référence immuable, ne pouvant pas être modifiée après initialisation.

```kotlin
val answer: Int = 42
val age = 33 // Type inféré
age += 2 // Erreur, non modifiable
```

L'utilisation de `val` est recommandée autant que possible pour permettre au compilateur d'optimiser le code.

### `var` - Référence une Variable (Mutabilité)

La déclaration avec `var` crée une référence mutable, autorisant des modifications ultérieures.

```kotlin
var x: Int = 2
var y = 'y'
++x // OK, mutable
```

Préférez `val` à `var` lorsque c'est possible, pour favoriser l'immutabilité et permettre des optimisations du compilateur.

### `const` - Constante Connue à la Compilation

La déclaration avec `const` crée une constante connue à la compilation, applicable au niveau top level ou en tant que membre d'un objet.

```kotlin
const val PI_APPROX: Float = 3.14f
```

Les constantes doivent être initialisées avec une String ou une valeur primitive, et elles ne peuvent pas avoir de getter personnalisé.

### Comparaison de Références

- `==` réalise une comparaison structurelle (équivalent à `equals()` en Java).
- `===` réalise une comparaison d'instances (emplacement mémoire).

```kotlin
val a = "Hello"
val b = "Hello"
println(a == b)  // true, comparaison structurelle
println(a === b) // true, même instance en mémoire

```

### Smart Cast

- Utilisation de l'opérateur `is` pour vérifier le type d'un objet.

  ```kotlin
  val myst = ...
  if (myst is String) {
      print(myst.uppercase())
  }
  ```

- Le compilateur "se souvient" de la vérification et effectue automatiquement un cast intelligent dans la portée.

  ```kotlin
  if (myst is String && myst.length > 0) {
      println(myst.uppercase())
  }
  ```

## Types Nullables

Les types nullables en Kotlin permettent l'explicitation de la possibilité de valeurs nulles.

```kotlin
var maybeAnswer: Int? = null // OK, maybeAnswer est nullable
val dummyAnswer: Int = null // KO, dummyAnswer non nullable
maybeAnswer = 42 // OK, peut stocker un Int
val answer: Int = maybeAnswer // KO, Int et Int? pas compatibles
```

### Opérateur Safe Call (`?.`)

L'opérateur `?.` permet de manipuler des types nullables sans risque de `NullPointerException`.

```kotlin
val str: String? = ...
str?.uppercase()
```

### Elvis Operator (`?:`)

L'opérateur Elvis `?:` permet de définir une valeur par défaut pour le cas où la variable nullable est nulle.

```kotlin
val len = str?.length ?: -1
```

### Forçage (`!!` - À Éviter)

L'opérateur `!!` force le déréférencement d'un nullable, mais doit être évité autant que possible pour éviter des `NullPointerException`.

```kotlin
val str: String? = null
str!!.uppercase() // Ooops... NPE
```

### Type Plateforme (`Type!`)

Kotlin compilé en bytecode Java utilise le type plateforme (`Type!`) lorsque le type de nullabilité n'est pas clair.

```kotlin
val date = Date()
val instant = date.toInstant() // typé Instant!
```

Il est recommandé de manipuler avec précaution les types de plateforme.

Ces mécanismes permettent de gérer de manière robuste les valeurs nullables et d'éviter les erreurs fréquentes de `NullPointerException`.

Oui, j'ai abordé les concepts de smart cast et du contrôle du flot d'exécution (les structures `if`, `when`, et leurs versions en expressions). Voici un résumé de ce qui a été dit :


## Contrôle du Flot d'Exécution

#### `if` comme Expression

- Les structures `if` peuvent être utilisées comme des expressions, renvoyant la valeur de la dernière instruction exécutée.

  ```kotlin
  val parite = if (x.isOdd()) "Impair" else "Pair"
  ```

#### `when` comme Expression

- `when` peut également être utilisé comme une expression.

  ```kotlin
  val result = when {
      x.isOdd() -> "impair"
      x.isEven() -> "pair"
      else -> "bizarre"
  }
  ```

- Les cas de `when` doivent couvrir le domaine de la variable.

## Boucles

### `while`, `continue`, `break`

Les boucles `while`, `continue`, et `break` fonctionnent de manière similaire à Java.

```kotlin
var i = 0
while (i < 5) {
    println(i)
    i++
}
```

### `for` - Itération sur des itérables

La boucle `for` permet de parcourir tout objet qui fournit un itérateur. L'itérateur doit avoir une méthode `iterator()`, `next()`, et `hasNext()` (marquées comme `operator`).

```kotlin
// Itération classique (intervalle, String, Array, ...)
for (i in 0..9) {
    println(i)
}

for (c in "Hello") {
    println(c)
}
```

### Itération avec Indices

Il est possible d'itérer sur les indices d'un tableau ou d'une collection.

```kotlin
val array = arrayOf(1, 2, 3)
for (i in array.indices) {
    println(array[i])
}

for ((index, value) in array.withIndex()) {
    println("L'élément à la position $index est $value")
}
```

## Labels dans les Boucles

Les labels dans les boucles en Kotlin fournissent un moyen de sortir d'une boucle imbriquée spécifique lorsqu'on utilise l'instruction `break`. L'exemple suivant illustre cette fonctionnalité :

```kotlin
loop@ for (i in 1..100) {
    for (j in 1..100) {
        if (...) break@loop
    }
}
```

Dans cet exemple, la boucle extérieure est étiquetée avec `loop@`, et l'instruction `break@loop` est utilisée pour sortir de cette boucle spécifique lorsque la condition (représentée par `...`) est satisfaite dans la boucle intérieure. Cela permet de contrôler précisément la sortie de la boucle selon les besoins du programme.

## Gestion des Exceptions

La gestion des exceptions en Kotlin est similaire à celle de Java, mais avec quelques nuances.

### Expression `try/catch/finally`

La structure `try/catch/finally` est une expression en Kotlin, ce qui signifie qu'elle peut renvoyer une valeur. Dans l'exemple suivant :

```kotlin
val line: String? = try {
    input.readLine()
} catch (e: NumberFormatException) {
    null
}
```

- Le bloc `try` contient le code qui pourrait générer une exception.
- Le bloc `catch` permet de gérer les exceptions spécifiques, ici, une `NumberFormatException`.
- La valeur de `line` sera soit le résultat de `input.readLine()` si aucune exception n'est levée, soit `null` si une `NumberFormatException` est attrapée.

### Absence de Checked Exceptions

Contrairement à Java, Kotlin n'a pas de notion de checked exceptions. Cela signifie que vous n'êtes pas obligé de déclarer les exceptions potentielles qu'une fonction peut générer. Cependant, vous pouvez toujours choisir de capturer et de traiter les exceptions selon vos besoins, comme illustré dans l'exemple ci-dessus.

## Fonctions

### Fonctions Top Level

En Kotlin, les fonctions peuvent être définies en dehors de toute classe ou objet, ce qui les rend top level. Voici un exemple :

```kotlin
fun sum(a: Int, b: Int): Int {
    return a + b
}
```

Si la fonction a une seule instruction, elle peut être écrite sous forme d'expression body :

```kotlin
fun sum(a: Int, b: Int): Int = a + b
```

L'inférence de type permet même de simplifier davantage :

```kotlin
fun sum(a: Int, b: Int) = a + b
```

### Paramètres

#### Valeurs par Défaut

Les paramètres peuvent avoir des valeurs par défaut, ce qui rend les fonctions plus flexibles.

```kotlin
fun say(text: String = "Something") = println(text)

say()           // Affiche Something
say("Hi guys")  // Affiche Hi guys
```

#### Paramètres Nommés

Les paramètres peuvent être nommés pour une meilleure compréhension et flexibilité.

```kotlin
fun Box.setMargins(left: Int, top: Int, right: Int, bottom: Int) { … }

myBox.setMargins(10, 10, 20, 20) // Ordre conventionnel
myBox.setMargins(left = 10, right = 10, top = 20, bottom = 20) // Ordre spécifié
```

### Arguments Variables

L'utilisation de `vararg` permet de définir un nombre variable de paramètres.

```kotlin
fun foo(vararg strings: String) { … }

foo("bar")
foo("bar", "baz", "foobar", "barbaz")
```

Utilisation du spread operator (`*`) si les arguments sont dans un tableau.

```kotlin
val strings = arrayOf("abc", "defg", "hijk")
foo(*strings)
```

### Fonctions Imbriquées

Les fonctions peuvent être déclarées à l'intérieur d'autres fonctions pour une encapsulation maximale.

```kotlin
fun dfs(graph: Graph) {
    val visited = HashSet<Vertex>()
    
    fun dfs(current: Vertex) {
        if (!visited.add(current)) return
        for (v in current.neighbors)
            dfs(v)
    }

    dfs(graph.vertices[0])
}
```

### Typage des Fonctions

Les fonctions en Kotlin ont un type associé, défini par la signature de la fonction.

- `(A, B) -> C` : une fonction qui prend deux paramètres de types A et B, et retourne un C.
- `() -> A` : pas de paramètre en entrée, retourne un A.
- `(A) -> Unit` : pas de valeur de retour, mais prend un paramètre de type A.
- `A.(B) -> C` : fonction qui peut être appelée sur un objet de type A, prend un paramètre de type B et retourne une valeur de type C.

## Lambdas

En Kotlin, les lambdas sont des expressions anonymes permettant de créer des fonctions de manière concise. Elles sont souvent utilisées pour passer des fonctions en tant que paramètres à d'autres fonctions, comme dans le cas de la fonction `forEach` sur les collections.

### Syntaxe de base

La syntaxe d'une lambda est la suivante :

```kotlin
val square = { num: Int -> num * num } // (Int) -> Int
val more: (String, Int) -> String = { str, num -> str + num }
val printVal: (Int) -> Unit = { num -> println(num) }
```

- La première lambda `square` prend un entier et renvoie le carré de cet entier.
- La deuxième `more` prend une chaîne de caractères et un entier, renvoyant une nouvelle chaîne résultant de la concaténation.
- La troisième `printVal` prend un entier et l'imprime.

### Utilisation

Une lambda peut être utilisée comme argument pour une fonction, par exemple avec la fonction `forEach` :

```kotlin
val a = arrayOf(1, 2, 3, 4, 5)
a.forEach(printVal)
// Équivalent à
a.forEach({ num -> println(num) })
```

### Lambda avec `it`

Pour les lambdas qui ne prennent qu'un paramètre, on peut utiliser le mot-clé `it` :

```kotlin
val printVal: (Int) -> Unit = { println(it) }
val concatInt: String.(Int) -> String = { this + it }
```

- `printVal` imprime simplement la valeur reçue.
- `concatInt` est une fonction d'extension sur les chaînes, concaténant la chaîne avec un entier.

### Conversion entre `(A, B) -> C` et `A.(B) -> C`

Une fonction de type `A.(B) -> C` peut être utilisée en lieu et place de `(A, B) -> C` et vice versa. Cela permet une certaine flexibilité dans le passage de fonctions.

### Référence à une fonction existante

Il est possible de passer une référence à une fonction ou une méthode existante à l'aide de l'opérateur `::` :

```kotlin
val a = arrayOf(1, 2, 3, 4, 5)
a.forEach(::println)
```

### Fonctions d'Extension

Il est possible d'ajouter des fonctions à une classe après coup grâce aux fonctions d'extension. Toutes les instances de la classe peuvent ensuite en profiter.

```kotlin
fun String.reverse() = StringBuilder(this).reverse().toString()
"That's cool !".reverse() // "! looc s'tahT"
```

### Fonctions Infixes

Les fonctions infixes sont déclarées avec le mot-clé `infix`. Elles doivent avoir un seul paramètre et ne peuvent pas avoir de vararg ou de paramètre par défaut.


Bon ceci est le second test du coup...

```kotlin
infix fun String.open(rights: Access): File { … }
"/home/provot/lecture" open Access.WRITE
// Équivalent à
"/home/provot/lecture".open(Access.WRITE)
```

### Documentation avec KDoc

La documentation en Kotlin est réalisée avec KDoc, similaire à Javadoc. Elle inclut des tags tels que `@param`, `@return`, `@constructor`, `@receiver`, `@property`, `@throws`, `@exception`, `@sample`, `@see`, `@author`, `@since`, et `@suppress`.

```kotlin
/**
 * A group of *members*.
 *
 * This class has no useful logic; it's just a documentation example.
 *
 * @param T the type of a member in this group.
 * @property name the name of this group.
 * @constructor Creates an empty group.
 */
class Group<T>(val name: String) {
    /**
     * Adds a [member] to this group.
     * @return the new size of the group.
     */
    fun add(member: T): Int { ... }
}
```

Les KDocs permettent de documenter le code de manière claire et de générer une documentation structurée avec des outils comme Dokka.

---

[...retour au sommaire](../sommaire.md)