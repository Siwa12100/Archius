#!/bin/bash

if [ $# -ne 2 ]; then
    exit 1
fi

fichier_mdp=$1
hash_recherche=$2
echo "hash rech. : $hash_recherche"
sel=$(echo $hash_recherche | cut -d'$' -f3)

echo "sel : $sel"
echo "hash rech. : $hash_recherche"

while IFS= read -r mot_de_passe
do
    mot_de_passe_escaped=$(printf '%q' "$mot_de_passe")

    hash_calcule=$(openssl passwd -1 -salt "$sel" "$mot_de_passe")

    echo "Test du mot de passe : $mot_de_passe_escaped"
    echo "Hash calculé : $hash_calcule"
    
    if [ "$hash_calcule" == "$hash_recherche" ]; then
        echo "Mot de passe trouvé : $mot_de_passe"
        exit 0
    fi
done < "$fichier_mdp"

echo "Mot de passe non trouvé dans la liste."
exit 1
