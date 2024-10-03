# Securité - TP 3

---

## Exercice 1

Pour changer la variable `modified`, il faut surcharger le buffer lorsque l'on rentre le string, de manière à écrire au delà de la mémoire attribuée au buffer et ainsi changer le string. 

Ainsi, en rentrant 67 caractères, nous arrivons à changer modified : `aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa`. 

## Exercice 2

### 1.

Le programme prend en paramètre 2 strings ainsi que leur nombre de caractères. Il rend ainsi dans le buffer les 2 strings concaténés, ainsi que les tailles de 2 strings additionnés. 
Par contre, si on ne donne pas la bonne taille pour les 2 strings (par exemple une taille plus petite que la taille réel du string), alors seul le nombre de caractères spécifié en paramètre sera pris dans le string. 

**Exemple :**

```bash
./exo 2 coucou bateau 6 6 ---> buffer : coucoubateau, len1+len2 = 12
./exo 2 coucou bateau 5 6 ----> buffer : coucobateau, len1+len2 = 11
```

### 2.

Un unsigned char fait 1 octet.

### 3.

Pour changer la valeur de secret, il faut tout simplement que la valeur de len1 + len2 soit supérieure à 255. 

### 4.

Commande qui fonctionne : 

```bash
./a.out a BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB 255 161
``` 

Approche a expliquer....

## Exercice 3

## 1.)

La fonction `gets()` est signalée comme dangereuse par le compilateur. Il est connu que cette fonction permet un buffer overflow car elle ne limite pas la quantité de données saisies. Cela signifie que si un utilisateur entre plus de 128 caractères (la taille du buffer), les données supplémentaires vont déborder en mémoire, écrasant d'autres variables ou le retour d'adresse du programme.





