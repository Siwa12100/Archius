# API

[retour au sommaire](./sommaire.md)

---

La première partie est sur les images base64. Ca semble assez compliqué, et vu que cela n'a pas l'air ultra urgent à maitriser, pour l'instant, je zappe.

## Faire des requêtes http

En gros, ce que fais le cours sur cette partie, c'est qu'il fait une nouvelle classe qui implémente l'interface liée au service de gestion de données, sauf que du coup dans le code, il utilise des requêtes http.

## L'essentiel sur les requêtes http

Généralement, on utilise les fonctions d'appel à des api dans les fonctions de cycle de vie de composant `OnParametersSetAsync` ou `OnInitializedAsync`.

### Syntaxe générale d'appel des différents verbes d'api

Voici la syntaxe générale pour effectuer différents types de requêtes HTTP en utilisant la classe `HttpClient` en C# :

**Requête GET :**

```csharp
@inject HttpClient httpClient

@code {
    List<User> users;

    protected override async Task OnInitializedAsync()
    {
        users = await httpClient.GetFromJsonAsync<List<User>>("https://api.example.com/users");
    }
}
```

**Requête POST (avec envoi de données) :**

```csharp
@inject HttpClient httpClient

@code {
    User newUser = new User { Name = "Alice", Age = 25 };

    protected override async Task OnInitializedAsync()
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("https://api.example.com/users", newUser);

        if (response.IsSuccessStatusCode)
        {
            // La requête a réussi
        }
        else
        {
            // La requête a échoué
        }
    }
}
```

**Requête PUT (avec envoi de données) :**

```csharp
@inject HttpClient httpClient

@code {
    int userIdToUpdate = 1;
    User updatedUser = new User { Name = "UpdatedName", Age = 30 };

    protected override async Task OnInitializedAsync()
    {
        HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://api.example.com/users/{userIdToUpdate}", updatedUser);

        if (response.IsSuccessStatusCode)
        {
            // La requête a réussi
        }
        else
        {
            // La requête a échoué
        }
    }
}
```

**Requête DELETE :**

```csharp
@inject HttpClient httpClient

@code {
    int userIdToDelete = 1;

    protected override async Task OnInitializedAsync()
    {
        HttpResponseMessage response = await httpClient.DeleteAsync($"https://api.example.com/users/{userIdToDelete}");

        if (response.IsSuccessStatusCode)
        {
            // La requête a réussi
        }
        else
        {
            // La requête a échoué
        }
    }
}
```

**Gérer les Paramètres de Requête :**

```csharp
@inject HttpClient httpClient

@code {
    int currentPage = 1;
    int pageSize = 10;

    List<User> users;

    protected override async Task OnInitializedAsync()
    {
        users = await httpClient.GetFromJsonAsync<List<User>>($"https://api.example.com/users?currentPage={currentPage}&pageSize={pageSize}");
    }
}
```

**Gérer l'Authentification (utilisation d'un jeton d'accès) :**

```csharp
@inject HttpClient httpClient

@code {
    string accessToken = "your_access_token";
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

    // Maintenant, tu peux effectuer des requêtes avec l'authentification
}
```

### IHttpClientFactory

Ca a l'air un peu compliqué, assez poussé et ça risque de prendre du temps à comprendre, donc pour l'instant, voilà simplement un [lien](https://medium.com/@marcodesanctis2/using-ihttpclientfactory-with-blazor-webassembly-7cc702f5e9f8) vers un article sympa qui en parle...

Mais ça a l'air sacrément sympa pour personaliser les appels api !

---

[retour au sommaire](./sommaire.md)