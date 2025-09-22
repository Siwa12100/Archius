# Les méthodes magiques

[...retour au sommaire](../intro.md)

---

Les méthodes dites magiques sont des méthodes qui permettent des manipulations très utiles en PHP. 

Certaines ont des comportements par défaut et pourront être appelées sans même avoir été redéfinies dans la déclaration de la classe, mais d'autres comme `__toString` devront forcément être déclarées dans la classe avant d'être utilisées.

Elles doivent être définies avec des visibilités publiques. 

### __toString() et __clone()

La méthode clone permet de cloner un objet. C'est à dire qu'un nouvel objet est instancié et toutes les valeurs de l'objet de base sont automatiquement passées à l'objet cloné pour qu'ils soient absolument similaires. 

**Exemple :**
```php

$var1 = new maClasse();
$var2 = clone $var1;
```

Attention, avec le comportement par défaut de php, il n'y a aucun soucis si les attributs de la classe sont des types primitifs, mais s'il s'agit d'objets, eh ben par défaut, le clone et son original feront tous les deux référence à la même instance de l'objet attribut. 

Il faut donc dans ce cas redéfinir le comportement de la méthode de clonage. Voilà un exemple : 
```php

class maClasse {

    private autreClasse $attribut;
    ...
    ...

    public function __clone() {
        $this -> attribut = clone $this -> attribut;
        // En gros on clone à son tour l'attribut objet 
        // Et évidemment si lui aussi il contient des objets 
        // il faudra aller faire la modification dans cette classe aussi....
    }
}
```

La méthode `__toString` permet de renvoyer une chaîne de caractères contenant des infos sur la classe. 

**Exemple :**
```php
// Dans la classe : 
public function __toString() {
    return ....une chaine de caractères....,
}

// ailleurs dans le code : 
echo $maClasse; // va renvoyer ce qui a été défini dans le toString...
// Attention, si le toString n'a pas été défini, ça va faire une erreur évidemment...
```

---

Il y a pas mal d'autres méthodes dans le genre que je veux absolument noter ici car elles sont incroyablement pratiques et puissantes, mais pour l'instant je n'ai pas eu le temps.

Mais elles sont toutes disponibles ici : [methodes magiques](https://openclassrooms.com/fr/courses/1665806-programmez-en-oriente-objet-en-php/7306942-exploitez-les-methodes-communes-aux-objets). 

Surtout les méthodes pour la serialisation, elles ont l'air ultra utiles. 