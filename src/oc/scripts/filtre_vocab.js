// Import des modules nécessaires
const fs = require('fs');
const path = require('path');

/**
 * Fonction pour nettoyer un fichier de vocabulaire tout en conservant le format,
 * en supprimant uniquement les lignes contenant des doublons de mots.
 * @param {string} relativePath - Le chemin relatif du fichier à traiter.
 */
function processVocabularyFile(relativePath) {
    // Résoudre le chemin absolu
    const absolutePath = path.resolve(relativePath);

    // Lire le contenu du fichier
    let fileContent;
    try {
        fileContent = fs.readFileSync(absolutePath, 'utf-8');
    } catch (err) {
        console.error(`Erreur lors de la lecture du fichier : ${err.message}`);
        return;
    }

    // Séparer le contenu en lignes
    const lines = fileContent.split('\n');
    const seenWords = new Set();
    const cleanedLines = [];

    // Parcourir chaque ligne et nettoyer les doublons
    for (const line of lines) {
        const match = line.match(/^\d+\.\s+(.+)$/);
        if (match) {
            const word = match[1].trim();
            if (seenWords.has(word)) {
                continue; // Ignorer les doublons
            }
            seenWords.add(word);
        }
        cleanedLines.push(line); // Ajouter la ligne au résultat
    }

    // Créer le nom du fichier de sortie
    const dir = path.dirname(absolutePath);
    const ext = path.extname(absolutePath);
    const base = path.basename(absolutePath, ext);
    const outputFilename = `${base}_trié${ext}`;
    const outputPath = path.join(dir, outputFilename);

    // Écrire le fichier nettoyé
    try {
        fs.writeFileSync(outputPath, cleanedLines.join('\n'), 'utf-8');
        console.log(`Fichier nettoyé créé : ${outputPath}`);
    } catch (err) {
        console.error(`Erreur lors de l'écriture du fichier : ${err.message}`);
    }
}

// Vérifier que l'utilisateur a fourni un chemin en argument
if (process.argv.length < 3) {
    console.error('Usage: node script.js <cheminVersFichier>');
    process.exit(1);
}

// Récupérer le chemin du fichier depuis les arguments de la ligne de commande
const cheminRelatif = process.argv[2];
processVocabularyFile(cheminRelatif);
