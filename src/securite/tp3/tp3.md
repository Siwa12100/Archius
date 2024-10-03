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

```bash
clang -o rop vulnerable.c -m64 -fno-stack-protector -Wl,-z,relro,-z,now,-z,noexecstack -static

vulnerable.c:8:5: warning: implicit declaration of function 'gets' is invalid in C99 [-Wimplicit-function-declaration]
    gets(buff);
    ^
1 warning generated.
/usr/bin/ld: /tmp/vulnerable-90c7ba.o: in function `main':
vulnerable.c:(.text+0x23): warning: the `gets' function is dangerous and should not be used.
```

## 2.) 

Première série de tests :

```bash
perl -e 'print "A"x150' | ./rop
perl -e 'print "A"x200' | ./rop
perl -e 'print "A"x300' | ./rop
perl -e 'print "A"x350' | ./rop
perl -e 'print "A"x400' | ./rop

You password is incorrect
You password is incorrect
[1]    1842712 done                perl -e 'print "A"x200' | 
       1842713 segmentation fault  ./rop
You password is incorrect
[1]    1842714 done                perl -e 'print "A"x300' | 
       1842715 segmentation fault  ./rop
You password is incorrect
[1]    1842716 done                perl -e 'print "A"x350' | 
       1842717 segmentation fault  ./rop
You password is incorrect
[1]    1842718 done                perl -e 'print "A"x400' | 
       1842719 segmentation fault  ./rop
``` 

Seconde série de tests : 

```bash
perl -e 'print "A"x160' | ./rop
perl -e 'print "A"x170' | ./rop
perl -e 'print "A"x180' | ./rop
perl -e 'print "A"x190' | ./rop
perl -e 'print "A"x195' | ./rop

You password is incorrect
[1]    1842722 done                perl -e 'print "A"x160' | 
       1842723 segmentation fault  ./rop
You password is incorrect
[1]    1842724 done                perl -e 'print "A"x170' | 
       1842725 segmentation fault  ./rop
You password is incorrect
[1]    1842726 done                perl -e 'print "A"x180' | 
       1842727 segmentation fault  ./rop
You password is incorrect
[1]    1842728 done                perl -e 'print "A"x190' | 
       1842729 segmentation fault  ./rop
You password is incorrect
[1]    1842730 done                perl -e 'print "A"x195' | 
       1842731 segmentation fault  ./rop
```

150 caractères ne suffisent pas pour causer un segmentation fault, mais à partir de 160 caractères, le buffer déborde et l'écrasement se produit.


