# Traiter une image soumise dans un formulaire

Sans trop rentrer dans les détails (car tout est dans le doc [ici](https://learn.microsoft.com/fr-fr/aspnet/core/blazor/file-uploads?view=aspnetcore-8.0)), voilà comment gérer une image soumise dans un formulaire.

Dans le HandleValidSubmit :
```c#
...
...
// Save the image
        var imagePathInfo = new DirectoryInfo($"{WebHostEnvironment.WebRootPath}/images");

        // Check if the folder "images" exist
        if (!imagePathInfo.Exists)
        {
            imagePathInfo.Create();
        }
        
        // Determine the image name
        var fileName = new FileInfo($"{imagePathInfo}/{itemModel.Name}.png");

        // Write the file content
        await File.WriteAllBytesAsync(fileName.FullName, itemModel.ImageContent);
...
...
```

On a aussi dans le codebehind une fonction appelée depuis un évènement dans le formulaire, ici précisemment :

```html
<p>
        <label>
            Item image:
            <!-- Appelle la méthode LoadImage lorsque le fichier est changé -->
            <InputFile OnChange="@LoadImage" accept=".png" />
        </label>
    </p>
```

Et le code de la fonction est ainsi : 

```c#
private async Task LoadImage(InputFileChangeEventArgs e)
    {
        // Set the content of the image to the model
        using (var memoryStream = new MemoryStream())
        {
            await e.File.OpenReadStream().CopyToAsync(memoryStream);
            itemModel.ImageContent = memoryStream.ToArray();
        }
    }
```