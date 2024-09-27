#include <stdlib.h>
#include <unistd.h>
#include <stdio.h>

int main(int argc, char **argv)
{
    int modified;
    char buffer[64];
    modified = 0;
    scanf("%s", buffer);



    int nbCarateres = 0;

    for (int i = 0; i < 64 ; i++) {
        
        if (buffer[i] == '\0') {
            i = 64;
        }
        nbCarateres++;
    }

    nbCarateres = nbCarateres - 1;

        printf("Reponse : %s. taille : %d \n", &buffer, nbCarateres);

    // for (int i = 0; i = )

    if(modified != 0) {
        printf("you have changed the ’modified’ variable\n");
    } else {
        printf("Try again?\n");
    }

    return 0;
}