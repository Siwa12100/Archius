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

---

[...retour au sommaire](../sommaire.md)