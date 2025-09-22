Voici une documentation complète sur la manipulation des **entités de texte** (Text Entities) dans Minecraft, en français :

## Manipulation des Entités de Texte

Les entités de texte permettent d'afficher des messages flottants dans le monde de jeu. Ces entités sont hautement personnalisables via plusieurs paramètres.

### 1. **Invocation d'une Entité de Texte**
Utilisez la commande `/summon` pour invoquer une entité de texte dans le jeu :

```bash
/summon text_display ~ ~ ~ {text:"{\"text\":\"Bonjour, Monde!\"}"}
```

### 2. **Paramètres Modifiables**

- **Text** : Définit le texte affiché. Formaté en [JSON Text Component](https://minecraft.fandom.com/wiki/Raw_JSON_text_format).
  - Exemple : `"text":"{\"text\":\"Hello!\"}"`

- **Couleur d'Arrière-plan (BackgroundColor)** : Définit la couleur de fond du texte.
  - Valeurs : code hexadécimal (ex. `"color":"#FFFFFF"` pour blanc)

- **Opacité du Texte (TextOpacity)** : Définit l'opacité du texte. Valeurs de 0 (transparent) à 255 (opaque).
  - Exemple : `"TextOpacity":255`

- **Alignement du Texte (TextAlignment)** : Spécifie l'alignement du texte.
  - Valeurs : `LEFT`, `CENTER`, `RIGHT`
  - Exemple : `"TextAlignment":"CENTER"`

- **Largeur de Ligne (LineWidth)** : Définit la largeur maximale du texte avant le passage à la ligne suivante.
  - Exemple : `"LineWidth":200`

- **Ombre (Shadowed)** : Indique si le texte a une ombre.
  - Valeurs : `true` ou `false`
  - Exemple : `"Shadowed":true`

- **Transparence (SeeThrough)** : Détermine si le texte est visible à travers d'autres objets.
  - Valeurs : `true` ou `false`
  - Exemple : `"SeeThrough":false`

- **Arrière-plan par Défaut (DefaultBackground)** : Si l'arrière-plan est celui par défaut.
  - Valeurs : `true` ou `false`
  - Exemple : `"DefaultBackground":true`

### 3. **Modifier une Entité de Texte**
Vous pouvez ajuster les propriétés d'une entité de texte existante à l'aide de la commande `/data modify` :

```bash
/data modify entity @e[type=text_display,limit=1] Text set value '{"text":"Nouveau Texte!"}'
```

### 4. **Exemple Complet**
Invoquer une entité de texte avec des paramètres personnalisés :

```bash
/summon text_display ~ ~ ~ {text:"{\"text\":\"Ceci est un exemple!\"}", TextOpacity:150, TextAlignment:"RIGHT", Shadowed:true, SeeThrough:false, LineWidth:300}
```

### 5. **Références et Liens Utiles**
- [Guide JSON Text Components](https://minecraft.fandom.com/wiki/Raw_JSON_text_format)
