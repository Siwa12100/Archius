const fs = require('fs');
const path = require('path');

function countWords(filePath) {
    const data = fs.readFileSync(filePath, 'utf8');
    const lines = data.split('\n');
    let wordCount = 0;

    lines.forEach(line => {
        // Ignorer les lignes vides et les titres
        if (line.trim() && !line.startsWith('#') && !line.startsWith('---') && !line.startsWith('[')) {
            wordCount++;
        }
    });

    return wordCount;
}

const filePath = path.join(__dirname, 'mots_restants.md');
const wordCount = countWords(filePath);
console.log(`Nombre de mots restants à apprendre : ${wordCount}`);