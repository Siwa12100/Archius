const fs = require('fs');
const path = require('path');

// Fonction pour parcourir récursivement les fichiers d'un dossier
function getMarkdownFiles(dir, fileList = []) {
    const files = fs.readdirSync(dir);
    files.forEach(file => {
        const filePath = path.join(dir, file);
        const stat = fs.statSync(filePath);
        if (stat.isDirectory()) {
            getMarkdownFiles(filePath, fileList); // Récursion pour les sous-dossiers
        } else if (filePath.endsWith('.md')) {
            fileList.push(filePath); // Ajouter les fichiers .md à la liste
        }
    });
    return fileList;
}

// Fonction pour mélanger un tableau (algorithme de Fisher-Yates)
function shuffle(array) {
    for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
    }
    return array;
}

// Fonction principale
function processMarkdownFiles(dir) {
    const markdownFiles = getMarkdownFiles(dir);
    let francais = [];
    let occitan = [];

    // Expression régulière plus flexible
    const regex = /^(\d+)\.\s*(.*?)\s*[-=]{1,5}>\s*\*\*(.*?)\*\*/;

    markdownFiles.forEach(file => {
        const data = fs.readFileSync(file, 'utf8');
        const lines = data.split('\n');
        
        // Analyse du fichier ligne par ligne
        lines.forEach(line => {
            const match = line.match(regex);
            if (match) {
                // Extraire les parties française et occitane avec flexibilité
                const frWord = match[2].trim();
                const ocWord = match[3].trim();
                francais.push(frWord);
                occitan.push(ocWord);
            }
        });
    });

    if (francais.length === 0) {
        console.error("Aucun mot trouvé dans les fichiers.");
        return;
    }

    // Mélanger la liste française
    const shuffledFrancais = shuffle(francais.slice());
    const shuffledOccitan = shuffledFrancais.map(frWord => occitan[francais.indexOf(frWord)]);

    // Créer le dossier "melange"
    const outputDir = path.join(process.cwd(), 'melange');
    if (!fs.existsSync(outputDir)) {
        fs.mkdirSync(outputDir);
    }

    // Écrire les fichiers mélangés
    const frContent = shuffledFrancais.map((word, index) => `${index + 1}. ${word}`).join('\n');
    const ocContent = shuffledOccitan.map((word, index) => `${index + 1}. ${word}`).join('\n');

    fs.writeFileSync(path.join(outputDir, 'melange_fr.md'), frContent, 'utf8');
    fs.writeFileSync(path.join(outputDir, 'melange_oc.md'), ocContent, 'utf8');

    console.log(`Fichiers mélangés générés dans le dossier : ${outputDir}`);
}

// Lecture du chemin du dossier passé en argument
const folderPath = process.argv[2];
if (!folderPath) {
    console.error('Veuillez fournir un chemin vers un dossier.');
    process.exit(1);
}

// Lancer le processus
processMarkdownFiles(path.resolve(folderPath));
