const fs = require('fs');
const path = require('path');

function renumberMarkdownLists(filePath) {
    try {
        // Lire le fichier d'entrée
        const fileContent = fs.readFileSync(filePath, 'utf-8');

        // Diviser le contenu en lignes
        const lines = fileContent.split('\n');

        // Variables pour suivre les sections et les listes
        let inList = false;
        let listIndex = 0;
        const updatedLines = [];

        // Parcourir les lignes
        lines.forEach(line => {
            if (line.startsWith('##') || line.startsWith('###')) {
                // Détecter un nouveau titre de section
                inList = false;
                updatedLines.push(line);
            } else if (/^\d+\.\s/.test(line)) {
                // Si c'est une ligne de liste
                if (!inList) {
                    inList = true;
                    listIndex = 1; // Réinitialiser l'index de la liste
                }
                updatedLines.push(line.replace(/^\d+\.\s/, `${listIndex}. `));
                listIndex++;
            } else {
                // Si ce n'est pas une liste
                inList = false;
                updatedLines.push(line);
            }
        });

        // Créer le chemin pour le fichier traité
        const parsedPath = path.parse(filePath);
        const outputFilePath = path.join(
            parsedPath.dir,
            `${parsedPath.name}_traite${parsedPath.ext}`
        );

        // Écrire le fichier traité
        fs.writeFileSync(outputFilePath, updatedLines.join('\n'), 'utf-8');

        console.log(`✔ Le fichier a été traité avec succès : ${outputFilePath}`);
    } catch (error) {
        console.error(`❌ Une erreur est survenue : ${error.message}`);
    }
}

// Exemple d'utilisation
const filePath = process.argv[2];
if (!filePath) {
    console.error('❌ Veuillez fournir le chemin relatif du fichier à traiter en paramètre.');
    process.exit(1);
}
renumberMarkdownLists(filePath);
