#include <stdio.h>
#include <stdlib.h>
#include <string.h>

struct data {
    char buffer[200];
    unsigned char secret;
    char out[200];
};

int copy(char * buf1, char * buf2, unsigned char len1, unsigned char len2) {
    
    struct data d;
    d.secret = 0;
    memset(d.buffer, 0, 200); // Fills with 0.
    memset(d.out, 0, 200); // Fills with 0.

    printf("[BEFORE] buffer = \"%s\"\n", d.buffer);
    printf("[BEFORE] secret = 0x%X\n", d.secret);
    printf("[Secret] --> %X \n", d.secret);
    printf("[BEFORE] out = \"%s\"\n", d.out);
    printf("[DEBUG] len1+len2 = %u\n", len1+len2);

    int test1 = (unsigned char) len1 + len2;
    unsigned char test2 = len1 + len2;
    unsigned char test3 = (len1 + len2)% 256;
    printf("Test 1 : %u ; Test 2 : %u ; Test 3 : %u\n", test1, test2, test3);


    if((unsigned char)(len1 + len2) > 200) {
        printf("[ERROR] Size too long!\n");
        return -1;
    }
        memcpy(d.buffer, buf1, len1);
        memcpy(d.buffer + len1, buf2, len2);

        printf("[AFTER] buffer = \"%s\"\n", d.buffer);
        printf("[AFTER] secret = 0x%X\n",d.secret);
        printf("[Secret] --> %X \n", d.secret);
        printf("[AFTER] out = \"%s\"\n", d.out);
        return 1;
}

int main(int argc, char * argv[]) {
    copy(argv[1], argv[2], atoi(argv[3]), atoi(argv[4]));
}