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



---

[...retour au sommaire](../sommaire.md)