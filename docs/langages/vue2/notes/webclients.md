# Documentation Complète : DTO, Modèle, Mapper, Interface WebClient et Implémentation avec Axios

[Menu vue](../menu.md)

---

## 1. **DTO (Data Transfer Object)**

### Définition
Un **DTO** est une structure de données utilisée pour **transporter des informations** entre le client et le serveur. Contrairement aux modèles métier, il ne contient **aucune logique**.

### Rôle
- **Simplifie les échanges** entre les couches de l'application.
- Permet de **transformer les données** avant qu'elles ne soient manipulées ou affichées.
- **Optimise les réponses** de l'API en exposant uniquement les champs nécessaires.

### Exemple de DTO

#### `UserDTO.ts`
```typescript
export interface UserDTO {
  id: number;
  name: string;
  email: string;
}
```

---

## 2. **Modèle (Entity ou Business Object)**

### Définition
Un **modèle** est une **représentation métier** des données, contenant potentiellement des **méthodes** et de la **logique**.

### Rôle
- Manipule les données de manière **orientée objet**.
- Contient la **logique métier** propre à l'entité.

### Exemple de Modèle

#### `User.ts`
```typescript
export class User {
  constructor(
    public id: number,
    public name: string,
    public email: string
  ) {}

  // Méthode métier
  greet(): string {
    return `Hello, ${this.name}!`;
  }
}
```

---

## 3. **Mapper**

### Définition
Un **mapper** est une classe ou un ensemble de fonctions permettant de **convertir** un **DTO** en **modèle** et vice-versa.

### Rôle
- **Isoler** la logique de transformation.
- Faciliter la **conversion bidirectionnelle** entre les DTOs (données brutes) et les modèles (objets métier).

### Exemple de Mapper

#### `UserMapper.ts`
```typescript
import { User } from './User';
import { UserDTO } from './UserDTO';

export class UserMapper {
  // Transforme un DTO en Modèle
  static toEntity(dto: UserDTO): User {
    return new User(dto.id, dto.name, dto.email);
  }

  // Transforme un Modèle en DTO
  static fromEntity(user: User): UserDTO {
    return {
      id: user.id,
      name: user.name,
      email: user.email,
    };
  }
}
```

---

## 4. **Interface pour un WebClient**

### Définition
L'interface **IWebClient** définit les méthodes que tout client HTTP doit implémenter. Cela permet une **abstraction** des appels API.

### Rôle
- Garantir une **interface commune** pour les clients HTTP.
- Faciliter le **changement d'implémentation** (e.g., Axios, Fetch).

### Exemple d'Interface

#### `IWebClient.ts`
```typescript
export interface IWebClient {
  get<T>(url: string, params?: Record<string, any>): Promise<T>;
  post<T>(url: string, body: any): Promise<T>;
  put<T>(url: string, body: any): Promise<T>;
  delete<T>(url: string): Promise<T>;
}
```

---

## 5. **Implémentation de WebClient avec Axios**

### Définition
Axios est une **bibliothèque HTTP** qui simplifie les appels API. Elle est souvent utilisée pour implémenter l'interface **IWebClient**.

### Installation d'Axios
```bash
npm install axios
```

### Exemple d'Implémentation

#### `AxiosWebClient.ts`
```typescript
import axios, { AxiosInstance } from 'axios';
import { IWebClient } from './IWebClient';

export class AxiosWebClient implements IWebClient {
  private client: AxiosInstance;

  constructor(baseURL: string) {
    this.client = axios.create({
      baseURL,
      timeout: 10000,
      headers: {
        'Content-Type': 'application/json',
      },
    });
  }

  async get<T>(url: string, params?: Record<string, any>): Promise<T> {
    const response = await this.client.get<T>(url, { params });
    return response.data;
  }

  async post<T>(url: string, body: any): Promise<T> {
    const response = await this.client.post<T>(url, body);
    return response.data;
  }

  async put<T>(url: string, body: any): Promise<T> {
    const response = await this.client.put<T>(url, body);
    return response.data;
  }

  async delete<T>(url: string): Promise<T> {
    const response = await this.client.delete<T>(url);
    return response.data;
  }
}
```

---

## 6. **Service : Utilisation du WebClient et du Mapper**

Un **service** consomme l'interface **IWebClient** pour faire des appels API, et utilise le **mapper** pour transformer les données.

### Exemple de Service

#### `UserService.ts`
```typescript
import { IWebClient } from './IWebClient';
import { UserDTO } from './UserDTO';
import { User } from './User';
import { UserMapper } from './UserMapper';

export class UserService {
  constructor(private webClient: IWebClient) {}

  async getAllUsers(): Promise<User[]> {
    const dtos = await this.webClient.get<UserDTO[]>('/users');
    return dtos.map(UserMapper.toEntity);
  }

  async createUser(user: User): Promise<User> {
    const dto = UserMapper.fromEntity(user);
    const createdDto = await this.webClient.post<UserDTO>('/users', dto);
    return UserMapper.toEntity(createdDto);
  }
}
```

---

## 7. **Tests et Mocks**

Pour tester les services et clients sans dépendre de l'implémentation réelle, on utilise des **mocks**.

### Exemple de Mock pour `IWebClient`

#### `MockWebClient.ts`
```typescript
import { IWebClient } from './IWebClient';

export class MockWebClient implements IWebClient {
  async get<T>(url: string): Promise<T> {
    return Promise.resolve([{ id: 1, name: 'John', email: 'john@example.com' }] as unknown as T);
  }

  async post<T>(url: string, body: any): Promise<T> {
    return Promise.resolve({ id: 1, ...body } as unknown as T);
  }

  async put<T>(url: string, body: any): Promise<T> {
    return Promise.resolve({ id: 1, ...body } as unknown as T);
  }

  async delete<T>(url: string): Promise<T> {
    return Promise.resolve() as unknown as T;
  }
}
```

---

## 8. **Résumé**

### Concepts Clés

| **Concept**       | **Description**                                                                 |
|-------------------|---------------------------------------------------------------------------------|
| **DTO**           | Objet de transfert de données entre le client et le serveur, sans logique.       |
| **Modèle**         | Représentation métier des données, avec logique métier.                         |
| **Mapper**         | Conversion entre DTOs et modèles.                                               |
| **IWebClient**     | Interface abstraite pour les clients HTTP.                                      |
| **AxiosWebClient** | Implémentation de `IWebClient` utilisant Axios pour les appels API.             |
| **Service**        | Consomme le WebClient et utilise le Mapper pour gérer les données métier.       |

---

[Menu vue](../menu.md)