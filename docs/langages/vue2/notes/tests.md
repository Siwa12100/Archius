# Guide Complet pour les Tests avec Vue.js, TypeScript et Jest

[Menu vue](../menu.md)

---



## **1. Introduction à Jest**
**Jest** est un framework de tests JavaScript populaire et performant. Il permet de tester :
- La logique métier (fonctions et services).
- Les composants Vue.js (avec Vue Test Utils).
- Les appels API (via des WebClients).
  
**Pourquoi Jest ?**
- Simple à configurer.
- Compatible avec TypeScript et Vue.js.
- Supporte les mocks et les tests asynchrones.

---

## **2. Installation**

### **Prérequis**
- Node.js installé (avec npm ou yarn).
- Un projet Vue.js configuré avec TypeScript.

### **Installation des dépendances**
Exécutez les commandes suivantes dans le terminal :

```bash
# Installer Jest et ses outils pour TypeScript
npm install --save-dev jest ts-jest @types/jest

# Installer Vue Test Utils pour tester les composants Vue.js
npm install --save-dev @vue/test-utils vue-jest

# (Optionnel) Installer un transformateur si vous utilisez ES Modules
npm install --save-dev @jest/transform
```

---

## **3. Configuration**

### **Fichier `jest.config.cjs`**
Pour configurer Jest dans un projet Vue.js, créez un fichier `jest.config.cjs` à la racine du projet. Voici une configuration complète :

```javascript
const { pathsToModuleNameMapper } = require('ts-jest');
const { compilerOptions } = require('./tsconfig.json');

module.exports = {
  preset: 'ts-jest', // Permet de tester du TypeScript
  testEnvironment: 'jsdom', // Simule un DOM pour tester les composants Vue.js
  moduleFileExtensions: ['ts', 'js', 'json', 'vue', 'node'], // Extensions de fichiers supportées
  roots: ['<rootDir>/src'], // Dossier racine pour les tests
  testMatch: ['**/__tests__/**/*.spec.ts', '**/?(*.)+(spec|test).ts'], // Types de fichiers à tester
  transform: {
    '^.+\\.vue$': 'vue-jest', // Transformateur pour les fichiers Vue
    '^.+\\.ts$': 'ts-jest', // Transformateur pour TypeScript
  },
  moduleNameMapper: pathsToModuleNameMapper(compilerOptions.paths, { prefix: '<rootDir>/' }), // Alias TypeScript
};
```

### **Alias TypeScript dans `tsconfig.json`**
Assurez-vous que vos alias sont bien configurés pour simplifier les imports dans votre projet. Exemple de fichier `tsconfig.json` :

```json
{
  "compilerOptions": {
    "target": "esnext",
    "module": "esnext",
    "moduleResolution": "node",
    "strict": true,
    "jsx": "preserve",
    "esModuleInterop": true,
    "skipLibCheck": true,
    "forceConsistentCasingInFileNames": true,
    "baseUrl": ".",
    "paths": {
      "@/*": ["src/*"]
    }
  },
  "include": ["src/**/*.ts", "src/**/*.d.ts", "src/**/*.tsx", "src/**/*.vue"],
  "exclude": ["node_modules"]
}
```

---

## **4. Organisation des fichiers**

Organisez vos fichiers pour maintenir un projet clair et structuré. Voici une structure typique pour un projet Vue.js avec des tests :

```
src/
├── Components/
│   ├── MyButton.vue
│   └── __tests__/
│       └── MyButton.spec.ts
├── Services/
│   ├── MyService.ts
│   ├── __tests__/
│       └── MyService.spec.ts
├── WebClients/
│   ├── MyWebClient.ts
│   └── __tests__/
│       └── MyWebClient.spec.ts
```

- Placez vos tests dans des dossiers `__tests__` ou utilisez des fichiers avec l'extension `.spec.ts`.

---

## **5. Écriture de Tests**

Dans cette section, nous détaillons les types de tests que vous pouvez écrire en TypeScript avec Jest, en insistant sur la logique métier. Vous apprendrez à tester des fonctions simples, des comportements asynchrones, à vérifier que des erreurs spécifiques sont levées, et à couvrir des cas plus complexes avec des services.

---

### **1. Tests unitaires : Logique métier simple**


#### Exemple : Tester une fonction simple
Imaginons une fonction qui additionne deux nombres :

**Fonction à tester (`mathUtils.ts`)**
```typescript
export function additionner(a: number, b: number): number {
  return a + b;
}
```

**Test unitaire (`mathUtils.spec.ts`)**
```typescript
import { additionner } from './mathUtils';

describe('additionner', () => {
  it('ajoute deux nombres correctement', () => {
    expect(additionner(2, 3)).toBe(5);
  });

  it('retourne 0 pour deux zéros', () => {
    expect(additionner(0, 0)).toBe(0);
  });

  it('gère les nombres négatifs', () => {
    expect(additionner(-2, -3)).toBe(-5);
  });

  it('gère le mélange de nombres positifs et négatifs', () => {
    expect(additionner(5, -3)).toBe(2);
  });
});
```

**Points importants :**
- **`describe`** regroupe les tests d'une même fonctionnalité.
- **`it`** décrit un cas précis de test.
- **`expect`** effectue une assertion, c'est-à-dire vérifie que le résultat attendu correspond au résultat obtenu.

---

### **2. Tests Asynchrones**

Quand vos fonctions ou méthodes exécutent des opérations asynchrones, comme des appels réseau ou des opérations sur des fichiers, les tests doivent gérer ces comportements.

#### Exemple : Tester une fonction asynchrone
Prenons une fonction qui simule un appel réseau avec `setTimeout` :

**Fonction à tester (`asyncUtils.ts`)**
```typescript
export async function attendreEtRetourner(valeur: string): Promise<string> {
  return new Promise((resolve) => {
    setTimeout(() => resolve(valeur), 1000);
  });
}
```

**Test Asynchrone (`asyncUtils.spec.ts`)**
```typescript
import { attendreEtRetourner } from './asyncUtils';

describe('attendreEtRetourner', () => {
  it('retourne la valeur après un délai', async () => {
    const result = await attendreEtRetourner('Bonjour');
    expect(result).toBe('Bonjour');
  });
});
```

**Points importants :**
1. **`async/await` dans les tests** : Utilisez `async` pour marquer un test comme asynchrone.
2. **Assertions après `await`** : Assurez-vous que l'opération asynchrone est terminée avant de vérifier le résultat.

---

### **3. Vérification des erreurs levées**

Certaines fonctions doivent lever des exceptions pour des cas particuliers. Jest permet de vérifier que ces exceptions sont correctement levées.

#### Exemple : Tester une fonction qui lève une erreur
Imaginons une fonction qui vérifie qu'un mot de passe est suffisamment long :

**Fonction à tester (`validationUtils.ts`)**
```typescript
export function validerMotDePasse(motDePasse: string): void {
  if (motDePasse.length < 8) {
    throw new Error('Le mot de passe est trop court');
  }
}
```

**Test des erreurs (`validationUtils.spec.ts`)**
```typescript
import { validerMotDePasse } from './validationUtils';

describe('validerMotDePasse', () => {
  it('lève une erreur si le mot de passe est trop court', () => {
    expect(() => validerMotDePasse('12345')).toThrowError('Le mot de passe est trop court');
  });

  it('ne lève pas d\'erreur si le mot de passe est valide', () => {
    expect(() => validerMotDePasse('MotDePasse123')).not.toThrow();
  });
});
```

**Points importants :**
- **`toThrowError`** : Vérifie que la fonction lève une exception avec un message spécifique.
- **Fonctions anonymes dans `expect`** : Pour tester une exception, passez une fonction anonyme à `expect`.

---

### **4. Tests combinant des comportements synchrones et asynchrones**

Certaines fonctions mélangent logique synchrone et opérations asynchrones. Il est crucial de tester les deux parties.

#### Exemple : Une fonction mixte
Prenons une fonction qui effectue une validation synchrone avant d’exécuter une opération asynchrone :

**Fonction à tester (`userUtils.ts`)**
```typescript
export async function creerUtilisateur(nom: string): Promise<string> {
  if (!nom) {
    throw new Error('Le nom est requis');
  }
  return new Promise((resolve) => setTimeout(() => resolve(`Utilisateur ${nom} créé`), 500));
}
```

**Test mixte (`userUtils.spec.ts`)**
```typescript
import { creerUtilisateur } from './userUtils';

describe('creerUtilisateur', () => {
  it('lève une erreur si le nom est vide', async () => {
    await expect(creerUtilisateur('')).rejects.toThrowError('Le nom est requis');
  });

  it('retourne un message de confirmation si le nom est valide', async () => {
    const result = await creerUtilisateur('Alice');
    expect(result).toBe('Utilisateur Alice créé');
  });
});
```

**Points importants :**
- **`rejects.toThrowError`** : Vérifie que la promesse rejetée lève une erreur spécifique.
- **Combinaison d'assertions** : Testez d'abord la partie synchrone (validation), puis la partie asynchrone.

---

### **5. Tests de services**

Les services encapsulent des logiques métier complexes, souvent basées sur des appels API ou des interactions avec des bases de données. Testez-les de manière isolée.

#### Exemple : Service simple
Imaginons un service qui récupère des données :

**Service à tester (`MyService.ts`)**
```typescript
export class MyService {
  async fetchData(): Promise<string> {
    return 'Données récupérées';
  }
}
```

**Test du service (`MyService.spec.ts`)**
```typescript
import { MyService } from '../MyService';

describe('MyService', () => {
  let service: MyService;

  beforeEach(() => {
    service = new MyService();
  });

  it('doit récupérer les données', async () => {
    const data = await service.fetchData();
    expect(data).toBe('Données récupérées');
  });
});
```

---

### **6. Tests avec Mocking**

Pour les services dépendants d’autres modules (comme Axios), utilisez des mocks pour isoler le comportement.

#### Exemple : Mock d’un appel API
**Service utilisant Axios (`ApiService.ts`)**
```typescript
import axios from 'axios';

export class ApiService {
  async getUsers(): Promise<any> {
    const response = await axios.get('https://api.example.com/users');
    return response.data;
  }
}
```

**Test avec Mock d’Axios (`ApiService.spec.ts`)**
```typescript
import axios from 'axios';
import { ApiService } from '../ApiService';

jest.mock('axios'); // Mock Axios
const mockedAxios = axios as jest.Mocked<typeof axios>;

describe('ApiService', () => {
  let service: ApiService;

  beforeEach(() => {
    service = new ApiService();
  });

  it('récupère une liste d\'utilisateurs', async () => {
    const mockUsers = [{ id: 1, name: 'Alice' }, { id: 2, name: 'Bob' }];
    mockedAxios.get.mockResolvedValueOnce({ data: mockUsers });

    const users = await service.getUsers();

    expect(mockedAxios.get).toHaveBeenCalledWith('https://api.example.com/users');
    expect(users).toEqual(mockUsers);
  });
});
```

**Points importants :**
- **`jest.mock`** simule le module entier (ici Axios).
- **`mockResolvedValueOnce`** : Spécifie le retour attendu d’une promesse dans un test donné.
- **`toHaveBeenCalledWith`** : Vérifie que l'appel API a été effectué avec les bons paramètres.

---



## **6. Résolution des erreurs courantes**

### **Erreur : `module is not defined in ES module scope`**
**Problème :** Jest utilise CommonJS par défaut, mais le projet est en ES Modules.  
**Solution :**
1. Renommez `jest.config.js` en `jest.config.cjs`.
2. Si vous voulez rester en mode ES, utilisez `export default` dans le fichier de configuration et ajoutez `--experimental-vm-modules` au script `test`.

---

### **Alias non résolus (`Cannot find module '@/...`)**
**Problème :** Jest ne reconnaît pas les alias définis dans `tsconfig.json`.  
**Solution :** Ajoutez `moduleNameMapper` dans `jest.config.cjs` :
```javascript
const { pathsToModuleNameMapper } = require('ts-jest');
const { compilerOptions } = require('./tsconfig.json');

module.exports = {
  moduleNameMapper: pathsToModuleNameMapper(compilerOptions.paths, { prefix: '<rootDir>/' }),
};
```

---

### **Erreur JSON dans `tsconfig.json`**
**Problème :** Présence de commentaires ou de syntaxe non valide dans le fichier JSON.  
**Solution :** Supprimez tous les commentaires. Exemple corrigé :
```json
{
  "compilerOptions": {
    "baseUrl": ".",
    "paths": {
      "@/*": ["src/*"]
    }
  }
}
```

---

[Menu Vue](../menu.md)