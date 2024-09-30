# TypeScript : bases

[...retour menu sur Angular](../menu.md)

---

## Sommaire

1. [Variables, Types, et Déclarations](#1-variables-types-et-déclarations)
2. [Boucles et Conditions](#2-boucles-et-conditions)
3. [Programmation Orientée Objet (POO) en TypeScript](#3-programmation-orientée-objet-poo-en-typescript)
4. [Gestion des Erreurs en TypeScript](#4-gestion-des-erreurs-en-typescript)
5. [Fonctions Lambda et Async/Await](#5-fonctions-lambda-et-asyncawait)
6. [Structures de Données en TypeScript](#6-structures-de-données-en-typescript)
7. [Nullables et Gestion du Null](#7-nullables-et-gestion-du-null)
8. [Interfaces en TypeScript](#8-interfaces-en-typescript)

---

## 1. **Variables, Types, et Déclarations**

### 1.1. **Déclaration de Variables**
TypeScript permet de déclarer des variables avec des types statiques pour assurer une meilleure robustesse du code.

```typescript
let nom: string = 'Jean';
const age: number = 25;
```

### 1.2. **Types Primitifs**
Les types incluent `string`, `number`, `boolean`, `any`, `void`, `undefined`, et `null`.

```typescript
let isDone: boolean = true;
let randomValue: any = 'peut être n’importe quoi';
```

### 1.3. **Tableaux et Tuples**
Les tableaux contiennent des éléments d’un type spécifique. Les tuples peuvent contenir des éléments de différents types.

```typescript
let tableauDeNombres: number[] = [1, 2, 3, 4];
let tuple: [string, number] = ['Jean', 30];
```

---

## 2. **Boucles et Conditions**

### 2.1. **Conditions**
Les conditions sont similaires à celles en JavaScript, avec le typage strict en plus.

```typescript
if (age >= 18) {
  console.log('Majeur');
}
```

### 2.2. **Boucles**
TypeScript supporte `for`, `for..of`, `for..in`, et `while`.

```typescript
for (let nombre of [1, 2, 3]) {
  console.log(nombre);
}
```

---

## 3. **Programmation Orientée Objet (POO) en TypeScript**

### 3.1. **Classes et Constructeurs**
TypeScript permet de définir des classes avec des constructeurs et des méthodes.

```typescript
class Personne {
  nom: string;
  age: number;

  constructor(nom: string, age: number) {
    this.nom = nom;
    this.age = age;
  }

  sePresenter(): string {
    return `Bonjour, je m'appelle ${this.nom} et j'ai ${this.age} ans.`;
  }
}

let jean = new Personne('Jean', 30);
```

### 3.2. **Encapsulation (Modificateurs d'Accès)**
Les modificateurs d'accès `public`, `private`, et `protected` contrôlent la visibilité des propriétés et méthodes.

```typescript
class Animal {
  protected espece: string;

  constructor(espece: string) {
    this.espece = espece;
  }
}
```

### 3.3. **Héritage et Polymorphisme**
Les classes enfant héritent des propriétés et méthodes de la classe parent.

```typescript
class Voiture extends Vehicule {
  rouler(): void {
    console.log('La voiture roule sur la route');
  }
}
```

---

## 4. **Gestion des Erreurs en TypeScript**

### 4.1. **Bloc `try-catch-finally`**
Comme en JavaScript, TypeScript permet de gérer les erreurs via `try-catch-finally`.

```typescript
try {
  throw new Error('Une erreur est survenue');
} catch (error) {
  console.log((error as Error).message);
}
```

---

## 5. **Fonctions Lambda et Async/Await**

### 5.1. **Fonctions Lambda (Fonctions Fléchées)**
Les lambdas capturent le contexte lexical de `this`.

```typescript
let ajouter = (a: number, b: number): number => a + b;
```

### 5.2. **Méthodes Async/Await**
Les mots-clés `async` et `await` simplifient la gestion des promesses.

```typescript
async function fetchData() {
  try {
    let response = await fetch('https://api.example.com/data');
    let data = await response.json();
    console.log(data);
  } catch (error) {
    console.error('Erreur:', error);
  }
}
```

---

## 6. **Structures de Données en TypeScript**

### 6.1. **Tableaux (`Array`)**

```typescript
let nombres: number[] = [1, 2, 3, 4];
```

### 6.2. **Maps**
Les **Maps** sont des collections de paires clé-valeur.

```typescript
let map = new Map<string, number>();
map.set('clé1', 100);
```

### 6.3. **Sets**
Les **Sets** stockent des valeurs uniques.

```typescript
let set = new Set<number>();
set.add(1);
set.add(2);
```

---

## 7. **Nullables et Gestion du Null**

### 7.1. **Null et Undefined**
Les types `null` et `undefined` sont gérés de manière stricte.

```typescript
let nom: string | null = null;
```

### 7.2. **Opérateur Optionnel**
L'opérateur `?.` permet d'accéder aux propriétés en évitant les erreurs lorsque la valeur est `null` ou `undefined`.

```typescript
console.log(personne?.adresse?.ville);
```

---

## 8. **Interfaces en TypeScript**

Les **interfaces** permettent de définir des contrats de structure pour des objets, classes ou fonctions. Elles sont utiles pour garantir que certains objets respectent une certaine forme, ou pour décrire les signatures des fonctions.

### 8.1. **Définir une Interface**
Une interface en TypeScript décrit la structure d'un objet.

```typescript
interface Personne {
  nom: string;
  age: number;
  sePresenter(): string;
}
```

### 8.2. **Implémenter une Interface dans une Classe**
Les classes peuvent implémenter des interfaces pour garantir qu’elles respectent une structure spécifique.

```typescript
class Employe implements Personne {
  nom: string;
  age: number;

  constructor(nom: string, age: number) {
    this.nom = nom;
    this.age = age;
  }

  sePresenter(): string {
    return `Bonjour, je suis ${this.nom}, employé âgé de ${this.age} ans.`;
  }
}
```

### 8.3. **Utiliser une Interface pour Typage d'Objets**
Une interface peut également être utilisée pour typer un objet sans créer de classe.

```typescript
let personne: Personne = {
  nom: 'Alice',
  age: 25,
  sePresenter: () => `Je suis Alice.`
};
```

### 8.4. **Propriétés Optionnelles dans les Interfaces**
Les propriétés d'une interface peuvent être rendues optionnelles en utilisant le point d'interrogation (`?`).

```typescript
interface Adresse {
  rue?: string;
  ville: string;
}

let monAdresse: Adresse = { ville: 'Paris' };
```

### 8.5. **Interfaces et Héritage**
Les interfaces peuvent également hériter d'autres interfaces.

```typescript
interface Contact {
  email: string;
}

interface PersonneAvecContact extends Personne, Contact {
  telephone?: string;
}

let employe: PersonneAvecContact = {
  nom: 'Bob',
  age: 28,
  email: 'bob@example.com',
  sePresenter: () => 'Je suis Bob.'
};
```

---

[...retour menu sur Angular](../menu.md)
