

# 📖 **Documentation Selenium **  

[...retorn en arrièr](../menu.md)

---

## 1️⃣ **Introduction à Selenium WebDriver**
Selenium WebDriver est un framework permettant d'automatiser l'exécution des navigateurs web. Il est principalement utilisé pour tester des applications web en simulant des interactions réelles avec l'utilisateur. Il permet notamment de :
- Naviguer sur un site web.
- Remplir des champs de formulaire.
- Cliquer sur des boutons et liens.
- Vérifier des éléments sur la page.
- Effectuer des tests d'intégration.

Selenium WebDriver prend en charge plusieurs navigateurs comme **Chrome, Firefox, Edge et Safari**.

---

## 2️⃣ **Démarrage avec Selenium WebDriver**
Une fois le projet Java configuré avec la dépendance **Selenium**, nous pouvons commencer à coder un premier test automatisé.

### **Création d'un WebDriver et Ouverture d’un Navigateur**
Le **WebDriver** est l'objet principal qui nous permet de contrôler un navigateur.

```java
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.firefox.FirefoxDriver;

public class TestSelenium {
    public static void main(String[] args) {
        // Création d'une instance du navigateur Firefox
        WebDriver driver = new FirefoxDriver();

        // Ouvrir un site web
        driver.get("https://www.google.com");

        // Afficher le titre de la page
        System.out.println("Titre de la page : " + driver.getTitle());

        // Fermer le navigateur
        driver.quit();
    }
}
```

**Explication :**
- `WebDriver driver = new FirefoxDriver();` → Instancie un pilote pour Firefox.
- `driver.get("URL");` → Charge l'URL spécifiée.
- `driver.getTitle();` → Récupère et affiche le titre de la page actuelle.
- `driver.quit();` → Ferme le navigateur.

---

## 3️⃣ **Interaction avec les Éléments Web**
Selenium permet de **trouver et manipuler** des éléments sur la page web en utilisant différentes méthodes de sélection.

### **Sélectionner un Élément Web**
```java
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.firefox.FirefoxDriver;

public class InteractionWeb {
    public static void main(String[] args) {
        WebDriver driver = new FirefoxDriver();
        driver.get("https://www.google.com");

        // Trouver la barre de recherche Google par son nom
        WebElement searchBox = driver.findElement(By.name("q"));

        // Saisir un texte dans la barre de recherche
        searchBox.sendKeys("Selenium WebDriver Java");

        // Soumettre la recherche
        searchBox.submit();

        // Afficher le titre de la page des résultats
        System.out.println("Titre de la page après recherche : " + driver.getTitle());

        driver.quit();
    }
}
```

**Méthodes de Sélection d'Éléments :**
- `By.id("id")` → Sélectionne un élément par son **ID**.
- `By.name("name")` → Sélectionne un élément par son **nom**.
- `By.className("classname")` → Sélectionne un élément par sa **classe CSS**.
- `By.tagName("tag")` → Sélectionne un élément par son **tag** HTML.
- `By.linkText("text")` → Sélectionne un **lien** en fonction du texte affiché.
- `By.partialLinkText("text")` → Sélectionne un lien qui contient un certain texte.
- `By.cssSelector("css")` → Sélectionne un élément avec un **sélecteur CSS**.
- `By.xpath("xpath")` → Sélectionne un élément avec une **expression XPath**.

---

## 4️⃣ **Gérer les Attentes (Timeouts)**
Les pages web peuvent mettre un certain temps à charger. Selenium propose deux types d’attentes :
1. **Attente implicite** : Selenium attend un certain temps avant de déclarer un élément introuvable.
2. **Attente explicite** : Selenium attend qu’une condition spécifique soit remplie avant d’agir.

### **Attente Implicite**
```java
driver.manage().timeouts().implicitlyWait(Duration.ofSeconds(5));
```
💡 Cela signifie que Selenium attend **jusqu'à 5 secondes** avant d'échouer lorsqu'il ne trouve pas un élément.

### **Attente Explicite**
```java
import org.openqa.selenium.support.ui.WebDriverWait;
import org.openqa.selenium.support.ui.ExpectedConditions;

WebDriverWait wait = new WebDriverWait(driver, Duration.ofSeconds(10));
WebElement element = wait.until(ExpectedConditions.visibilityOfElementLocated(By.id("result")));
```
💡 Selenium attend **jusqu'à 10 secondes** que l’élément devienne visible avant d’échouer.

---

## 5️⃣ **Cliquer sur un Lien et Naviguer**
On peut **cliquer** sur un élément et naviguer entre les pages.

### **Cliquer sur un Lien**
```java
WebElement firstResult = driver.findElement(By.cssSelector("h3"));
firstResult.click();
```

### **Naviguer dans l'historique du navigateur**
```java
driver.navigate().back();  // Revenir en arrière
driver.navigate().forward();  // Aller en avant
driver.navigate().refresh();  // Rafraîchir la page
```

---

## 6️⃣ **Faire un Test Automatisé avec JUnit**
Pour tester une page web, on utilise **JUnit** avec des assertions.

### **Vérifier l’URL après un clic**
```java
import org.junit.jupiter.api.*;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.firefox.FirefoxDriver;

public class SeleniumTest {
    WebDriver driver;

    @BeforeEach
    void setUp() {
        driver = new FirefoxDriver();
    }

    @Test
    void testGoogleSearch() {
        driver.get("https://www.google.com");
        Assertions.assertEquals("Google", driver.getTitle(), "Le titre de la page ne correspond pas.");
    }

    @AfterEach
    void tearDown() {
        driver.quit();
    }
}
```
💡 **Assertions utiles :**
- `Assertions.assertEquals(expected, actual);`
- `Assertions.assertTrue(condition);`
- `Assertions.assertFalse(condition);`

---

## 7️⃣ **Prendre des Screenshots**
On peut enregistrer une capture d’écran de la page en cours.

### **Exemple de Screenshot**
```java
import org.openqa.selenium.OutputType;
import org.openqa.selenium.TakesScreenshot;
import java.io.File;
import java.io.IOException;
import org.apache.commons.io.FileUtils;

File scrFile = ((TakesScreenshot) driver).getScreenshotAs(OutputType.FILE);
FileUtils.copyFile(scrFile, new File("screenshot.png"));
```

---

## 8️⃣ **Cas de Test sur DuckDuckGo**
1. **Aller sur DuckDuckGo** ✅
2. **Rechercher ton nom** ✅
3. **Cliquer sur chaque lien** ✅
4. **Prendre un screenshot de chaque page visitée** ✅

### **Code Complet**
```java
import org.openqa.selenium.*;
import org.openqa.selenium.firefox.FirefoxDriver;
import java.io.File;
import java.io.IOException;
import org.apache.commons.io.FileUtils;
import java.util.List;

public class DuckDuckGoTest {
    public static void main(String[] args) throws IOException {
        WebDriver driver = new FirefoxDriver();
        driver.get("https://duckduckgo.com/");

        WebElement searchBox = driver.findElement(By.name("q"));
        searchBox.sendKeys("Ton Nom");
        searchBox.submit();

        List<WebElement> results = driver.findElements(By.cssSelector("h2.result__title a"));
        for (WebElement result : results) {
            result.click();
            File scrFile = ((TakesScreenshot) driver).getScreenshotAs(OutputType.FILE);
            FileUtils.copyFile(scrFile, new File("screenshot_" + driver.getTitle() + ".png"));
            driver.navigate().back();
        }

        driver.quit();
    }
}
```

---

[...retorn en arrièr](../menu.md)