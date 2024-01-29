# Variables & collection

## Types de variables

### `val` (Valeur constante)

- `val` est utilisé pour déclarer une référence à une valeur constante (immuable).
- Une fois assignée, la valeur ne peut pas être modifiée.
- Le type de la variable peut être spécifié explicitement ou inféré par le compilateur.

```kotlin
val answer: Int = 42
val age = 33  // Type inféré
// age += 2  // Erreur, la variable est immuable
```

### `var` (Variable)

- `var` est utilisé pour déclarer une référence à une variable (mutable).
- La valeur peut être modifiée après son initialisation.

```kotlin
var x: Int = 2
var y = 'y'
++x  // OK, la variable est mutable
```

### Préférer `val` à `var` quand c'est possible

- Il est recommandé d'utiliser `val` autant que possible pour favoriser l'immutabilité.
- Cela permet au compilateur d'effectuer des optimisations et de rendre le code plus sûr.

```kotlin
val what: String  // Type nécessaire si pas de valeur
what = "Whaaat ?"  // Initialisation différée
```

## Types de base

### Entiers

   - `Byte` : 8 bits, de -128 à 127
   - `Short` : 16 bits, de -32768 à 32767
   - `Int` : 32 bits, de -2^31 à 2^31-1
   - `Long` : 64 bits, de -2^63 à 2^63-1

   Exemple :
   ```kotlin
   val myByte: Byte = 42
   val myInt: Int = 123
   val myLong: Long = 123456789L
   ```

### Décimaux

   - `Float` : 32 bits, représentation en virgule flottante
   - `Double` : 64 bits, représentation en virgule flottante (par défaut pour les décimaux)

   Exemple :
   ```kotlin
   val myFloat: Float = 3.14f
   val myDouble: Double = 3.141592653589793
   ```

### Caractères et Chaînes de caractères

   - `Char` : Représente un caractère Unicode (16 bits)
   - `String` : Chaîne de caractères

   Exemple :
   ```kotlin
   val myChar: Char = 'A'
   val myString: String = "Hello, Kotlin!"
   ```

### Booléens

   - `Boolean` : Représente les valeurs `true` ou `false`

   Exemple :
   ```kotlin
   val isTrue: Boolean = true
   val isFalse: Boolean = false
   ```

## Types spéciaux

### Unit

   - Équivalent du `void` dans d'autres langages. Représente l'absence de valeur ou le résultat d'une fonction sans retour.

   Exemple :
   ```kotlin
   fun printMessage(): Unit {
       println("Hello, Kotlin!")
   }
   ```

### Nullabilité

   - Kotlin intègre la notion de nullabilité dans les types de données en ajoutant un `?` à la fin du type.
   - Exemple : `String?` permet d'accepter soit une chaîne de caractères, soit la valeur `null`.


## Collections

### Tableaux

Kotlin propose des tableaux pour stocker des collections d'éléments de même type.

```kotlin
val numbers: IntArray = intArrayOf(1, 2, 3, 4, 5)
```

### List

Une liste est une collection ordonnée d'éléments. Chaque élément de la liste peut être accédé par son indice.

```kotlin
val myList: List<String> = listOf("apple", "banana", "orange")
```

**Opérations courantes sur les Listes :**

1. **Accéder aux éléments :**
   ```kotlin
   val firstElement = myList[0]  // Accès au premier élément
   val lastElement = myList.last()  // Accès au dernier élément
   ```

2. **Ajouter et supprimer des éléments :**
   ```kotlin
   val newList = myList + "grape"  // Ajouter un élément
   val removedElement = myList - "banana"  // Supprimer un élément
   ```

3. **Filtrer et transformer :**
   ```kotlin
   val filteredList = myList.filter { it.length > 5 }  // Filtrer les éléments
   val uppercasedList = myList.map { it.toUpperCase() }  // Transformer les éléments
   ```

### Set

Un ensemble est une collection qui ne permet pas d'avoir des éléments en double. Les éléments n'ont pas d'ordre spécifique.

```kotlin
val mySet: Set<Int> = setOf(1, 2, 3, 4, 5)
```

**Opérations courantes sur les Sets :**

1. **Ajouter et supprimer des éléments :**
   ```kotlin
   val newSet = mySet + 6  // Ajouter un élément
   val removedElementSet = mySet - 3  // Supprimer un élément
   ```

2. **Opérations ensemblistes :**
   ```kotlin
   val unionSet = mySet union setOf(5, 6, 7)  // Union de deux ensembles
   val intersectSet = mySet intersect setOf(3, 4, 5)  // Intersection de deux ensembles
   ```

### Map

Une carte (Map) est une collection de paires clé-valeur.

```kotlin
val myMap: Map<String, Int> = mapOf("one" to 1, "two" to 2, "three" to 3)
```

**Opérations courantes sur les Maps :**

1. **Accéder aux éléments :**
   ```kotlin
   val valueForKey = myMap["two"]  // Accès à la valeur pour une clé
   ```

2. **Ajouter et supprimer des paires :**
   ```kotlin
   val newMap = myMap + ("four" to 4)  // Ajouter une paire
   val removedPairMap = myMap - "two"  // Supprimer une paire
   ```

3. **Parcourir les paires clé-valeur :**
   ```kotlin
   for ((key, value) in myMap) {
       println("Key: $key, Value: $value")
   }
   ```

### Manipulations communes à toutes les collections

Kotlin fournit des opérations communes à toutes les collections via des fonctions d'extension :

- **Filtrage :**
  ```kotlin
  val filteredCollection = myCollection.filter { /* condition */ }
  ```

- **Transformation :**
  ```kotlin
  val transformedCollection = myCollection.map { /* transformation */ }
  ```

- **Réduction :**
  ```kotlin
  val result = myCollection.reduce { acc, element -> /* opération de réduction */ }
  ```

- **Vérification d'existence :**
  ```kotlin
  val containsElement = myCollection.contains(someElement)
  ```


### Types personnalisés

En plus des types de base, vous pouvez créer vos propres types personnalisés en utilisant les classes et les enums.

```kotlin
class Person(val name: String, val age: Int)

enum class Color {
    RED, GREEN, BLUE
}

val person: Person = Person("Alice", 30)
val color: Color = Color.BLUE
```

Les intervalles, les opérateurs `..`, `until`, `downTo`, et `step` ainsi que les fonctions d'extension sont des concepts importants en Kotlin pour travailler avec des séquences de nombres ou d'autres types de données. Voici une explication détaillée de chacun de ces concepts :

## Intervalles (Range et Progression)

Un intervalle est une séquence d'éléments. On peut créer des intervalles en utilisant l'opérateur `..` et spécifier un début et une fin.

```kotlin
val interval1 = 1..10  // de 1 inclus jusqu'à 10 inclus
val interval2 = 1 until 10  // de 1 inclus jusqu'à 10 exclus
val interval3 = 4..1  // vide
val interval4 = 4 downTo 1  // de 4 jusqu'à 1 inclus
val interval5 = 10 downTo 1 step 2  // de 10 jusqu'à 1 inclus avec un pas de 2
```

- `..` : Crée un intervalle inclusif.
- `until` : Crée un intervalle exclusif.
- `downTo` : Définit une plage décroissante.
- `step` : Spécifie un pas entre les valeurs de l'intervalle.

### Fonctions d'extension

#### `in`

L'opérateur `in` permet de vérifier si une valeur se trouve dans un intervalle.

```kotlin
val chiffres = 0..9
if (3 in chiffres) {
    println("3 est un chiffre")
}
```

#### Boucle for avec un intervalle

Vous pouvez utiliser un intervalle dans une boucle for pour itérer sur ses valeurs.

```kotlin
for (i in chiffres) {
    print("$i, ")
}
println("sont des chiffres")
```

Cette boucle for itérera de 0 à 9 et imprimera chaque valeur.




