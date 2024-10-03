#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int main(int argc, char **argv) {
    char buff[128];

    gets(buff);
    char *password = "I am TP ROP PM 2 !";

    if (strcmp(buff, password)) {
        printf("You password is incorrect\n");
    } else {
        printf("Access GRANTED !\n");
    }
    return 0;
}
