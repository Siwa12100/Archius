# Cycle de vie des composants & evenements associés

[...retour au résumé du cours](./resumeDuCours.md)

---

## Résumé des méthodes

1. **`OnInitialized` / `OnInitializedAsync`:**
   - **Quand :** Appelé lors de la création de l'instance du composant.
   - **Utilisation :** Il est possible d'effectuer des opérations d'initialisation synchrones (`OnInitialized`) ou asynchrones (`OnInitializedAsync`).
   - **Pertinent pour :** L'initialisation de données, l'injection de dépendances, ou d'autres opérations qui doivent se produire une seule fois lors de la création du composant.

2. **`OnParametersSet` / `OnParametersSetAsync`:**
   - **Quand :** Appelé après `OnInitialized` lorsqu'il y a des changements de paramètres.
   - **Utilisation :** Possibilité de surcharger ces méthodes pour réagir aux changements de paramètres et effectuer des opérations en réponse à ces changements.
   - **Pertinent pour :** Réagir aux changements de propriétés du composant.

3. **`ShouldRender`:**
   - **Quand :** Appelé avant le rendu pour déterminer si le rendu est nécessaire.
   - **Utilisation :** Il est possible de surcharger cette méthode pour personnaliser la logique conditionnelle du rendu.
   - **Pertinent pour :** Éviter des rendus inutiles lorsque le contenu du composant n'a pas changé.

4. **`Render`:**
   - **Quand :** Appelé pour générer l'arbre de rendu du composant.
   - **Utilisation :** Possibilité de définir la structure HTML du composant dans cette méthode.
   - **Pertinent pour :** Définir l'aspect visuel du composant.

5. **`OnAfterRender` / `OnAfterRenderAsync`:**
   - **Quand :** Appelé après le rendu du composant dans le DOM.
   - **Utilisation :** Il est possible de surcharger ces méthodes pour effectuer des opérations post-rendu, telles que l'interaction avec le DOM.
   - **Pertinent pour :** Effectuer des actions après que le composant a été rendu dans le DOM.

En résumé, ces points d'ancrage du cycle de vie offrent la possibilité de personnaliser le comportement des composants à différentes étapes de leur existence. Il est possible d'utiliser ces méthodes pour des opérations spécifiques liées à l'initialisation, aux changements de paramètres, au rendu et aux actions post-rendu.

## Prototypes de méthodes

Voici le prototype des méthodes associées au cycle de vie des composants Blazor, ainsi qu'un exemple de ce que l'on pourrait faire à l'intérieur de chaque méthode :

1. **`OnInitialized` / `OnInitializedAsync`:**
   - **Prototype :**
     ```csharp
     protected override void OnInitialized();
     protected override Task OnInitializedAsync();
     ```
   - **Exemple :**
     ```csharp
     protected override void OnInitialized()
     {
         // Initialisation synchrone du composant
         // Par exemple, initialisation de données
     }

     protected override async Task OnInitializedAsync()
     {
         // Initialisation asynchrone du composant
         // Par exemple, appel à un service asynchrone
         data = await myService.GetDataAsync();
     ```

2. **`OnParametersSet` / `OnParametersSetAsync`:**
   - **Prototype :**
     ```csharp
     protected override void OnParametersSet();
     protected override Task OnParametersSetAsync();
     ```
   - **Exemple :**
     ```csharp
     protected override void OnParametersSet()
     {
         // Réaction aux changements de paramètres
         // Par exemple, mise à jour de données en réponse à un changement de paramètre
     }

     protected override async Task OnParametersSetAsync()
     {
         // Réaction aux changements de paramètres de manière asynchrone
         // Par exemple, appel à un service asynchrone en réponse à un changement de paramètre
         data = await myService.GetDataAsync(Parameter);
     ```

3. **`ShouldRender`:**
   - **Prototype :**
     ```csharp
     protected override bool ShouldRender();
     ```
   - **Exemple :**
     ```csharp
     protected override bool ShouldRender()
     {
         // Logique conditionnelle pour décider si le rendu est nécessaire
         // Par exemple, éviter le rendu si certaines conditions ne sont pas remplies
         return dataIsDirty;
     ```

4. **`Render`:**
   - **Prototype :**
     ```csharp
     protected override void Render(RenderTreeBuilder builder);
     ```
   - **Exemple :**
     ```csharp
     protected override void Render(RenderTreeBuilder builder)
     {
         // Générer l'arbre de rendu du composant
         // Par exemple, construire la structure HTML en utilisant le RenderTreeBuilder
         builder.OpenElement(0, "div");
         builder.AddContent(1, "Contenu du composant");
         builder.CloseElement();
     ```

5. **`OnAfterRender` / `OnAfterRenderAsync`:**
   - **Prototype :**
     ```csharp
     protected override void OnAfterRender(bool firstRender);
     protected override Task OnAfterRenderAsync(bool firstRender);
     ```
   - **Exemple :**
     ```csharp
     protected override void OnAfterRender(bool firstRender)
     {
         // Actions à effectuer après le rendu (synchrone)
         // Par exemple, manipulation du DOM ou des éléments du composant
         if (firstRender)
         {
             // Code exécuté uniquement après le premier rendu
         }
     }

     protected override async Task OnAfterRenderAsync(bool firstRender)
     {
         // Actions à effectuer après le rendu (asynchrone)
         // Par exemple, appel à un service asynchrone après le rendu
         if (firstRender)
         {
             await myService.DoSomethingAsync();
         }
     ```

Ces exemples illustrent comment chaque méthode peut être utilisée pour personnaliser le comportement du composant à différentes étapes de son cycle de vie.