const fs = require('fs');
const path = require('path');

function renumberMarkdownListsInFile(filePath) {
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

        // Écrire dans le même fichier
        fs.writeFileSync(filePath, updatedLines.join('\n'), 'utf-8');
        console.log(`✔ Fichier traité : ${filePath}`);
    } catch (error) {
        console.error(`❌ Erreur lors du traitement du fichier ${filePath} : ${error.message}`);
    }
}

function processMarkdownFilesInDirectory(directoryPath) {
    try {
        // Lire tous les fichiers et dossiers dans le répertoire
        const items = fs.readdirSync(directoryPath);

        items.forEach(item => {
            const itemPath = path.join(directoryPath, item);
            const stats = fs.statSync(itemPath);

            if (stats.isDirectory()) {
                // Traiter récursivement les sous-dossiers
                processMarkdownFilesInDirectory(itemPath);
            } else if (stats.isFile() && path.extname(itemPath) === '.md') {
                // Traiter les fichiers .md
                renumberMarkdownListsInFile(itemPath);
            }
        });
    } catch (error) {
        console.error(`❌ Erreur lors du traitement du répertoire ${directoryPath} : ${error.message}`);
    }
}

// Exemple d'utilisation
const directoryPath = process.argv[2];
if (!directoryPath) {
    console.error('❌ Veuillez fournir le chemin du dossier à traiter en paramètre.');
    process.exit(1);
}

processMarkdownFilesInDirectory(directoryPath);
