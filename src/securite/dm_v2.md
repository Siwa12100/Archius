# Devoir Maison : EBIOS

## Exercice 1 : Cadrage et socle de sécurité (6 points)

### Liste de 10 biens essentiels et supports nécessaires à cette mission :

1. **Ordinateur**  
   Essentiel pour réaliser les projets, les TPs, et accéder aux outils de développement.
   
2. **Connexion internet stable**  
   Cruciale pour accéder aux sujets, documentations, applications, et plateformes en ligne. En cas d'instabilité, avoir une solution de secours comme les données mobiles ou l'accès à une bibliothèque avec une bonne connexion.

3. **IDE (Environnement de Développement Intégré)**  
   Indispensable pour écrire et tester les programmes (ex : IntelliJ IDEA, Visual Studio Code).

4. **Logiciels spécialisés**  
   Certains logiciels sont nécessaires pour effectuer les TPs, comme Sage, Android Studio, ou des outils de virtualisation (ex : Docker, VirtualBox).

5. **Cahier ou de quoi écrire**  
   Utilisé pour noter les cours, schématiser des concepts, et garder une trace des idées importantes.

6. **Matériel de stockage**  
   Pour sauvegarder les projets et les TPs. Idéalement, un disque dur externe ou un service de stockage cloud (Google Drive, Dropbox) pour sécuriser les données.

7. **Téléphone portable**  
   Utile pour la communication avec les enseignants et les camarades (appels, mails, notifications), mais aussi pour l'accès à des applications comme les outils de gestion de projets ou les alertes.

8. **Documentation technique**  
   Ressources indispensables pour résoudre des problèmes de code, chercher des exemples et comprendre les frameworks utilisés.

9. **Accès à des outils de versionnement (Git, CodeFirst)**  
   Essentiel pour gérer les versions des projets, collaborer avec d'autres étudiants, et sécuriser le travail via des commits réguliers.

10. **Lieu d’étude calme**  
    Important pour se concentrer et être productif. Une bibliothèque universitaire ou un espace de coworking pourrait être une alternative si le domicile n'est pas adapté.

---

### Liste de 5 événements redoutés :

1. **Manque de communication**  
   - **Impact** : Cela peut entraîner des malentendus sur les attentes des enseignants, des erreurs dans la réalisation des projets, ou une absence de soutien durant les moments critiques.  
   - **Biens affectés** : Livrables des projets, qualité des TPs.  
   - **Gravité** : 2 (en fonction de la durée et de la gravité du manque de communication, cela peut devenir plus critique).

2. **Oubli de sauvegarde / commit**  
   - **Impact** : Peut entraîner la perte de plusieurs heures voire jours de travail, compliquant la collaboration avec les camarades.  
   - **Biens affectés** : Livrables des projets, données de travail.  
   - **Gravité** : 4 (une perte de données sans sauvegarde pourrait être désastreuse, d'où la nécessité de commits réguliers).

3. **Matériel abîmé ou en panne**  
   - **Impact** : La défaillance d'un ordinateur ou autre matériel essentiel ralentit considérablement le rythme de travail et augmente le risque de perte de données.  
   - **Biens affectés** : Ordinateur, projets.  
   - **Gravité** : 3 (peut affecter la capacité à suivre les cours et à réaliser les projets).

4. **Corruption ou perte des fichiers**  
   - **Impact** : Perte de plusieurs heures voire jours de travail. Une récupération est souvent difficile et nécessite du temps supplémentaire pour refaire le travail perdu.  
   - **Biens affectés** : Matériel de stockage, projets stockés sur Git/CodeFirst.  
   - **Gravité** : 3 (la corruption des fichiers critiques peut retarder les projets).

5. **Maladie ou accident**  
   - **Impact** : Empêche l’étudiant de suivre les cours ou de participer aux TPs/projets, ce qui réduit la productivité et affecte la progression.  
   - **Biens affectés** : Suivi des cours, réalisation des TPs/projets.  
   - **Gravité** : 3 (une absence prolongée peut fortement nuire à la continuité des études et des projets).

---

## Exercice 2 : Source du risque (3 points)

### Liste de 3 sources de risque et leurs objectifs visés :

1. **Source du risque : Défaillance matérielle**  
   - **Objectif visé** : Continuité du matériel informatique  
     Le matériel doit fonctionner sans interruption pour permettre la réalisation des TPs et des projets. Une défaillance (ordinateur, disque dur) pourrait bloquer la mission et entraîner des retards significatifs.

2. **Source du risque : Erreur humaine**  
   - **Objectif visé** : Fiabilité des actions humaines  
     Les erreurs humaines (suppression accidentelle de fichiers, mauvaises manipulations) affectent directement la progression des projets. Elles peuvent provoquer des pertes de données critiques ou des bugs difficiles à résoudre, entraînant des retards.

3. **Source du risque : Mauvaise gestion du temps**  
   - **Objectif visé** : Optimisation de l'organisation  
     Une mauvaise gestion du temps ou la procrastination peut entraîner des retards dans les projets, une accumulation de travail, et une charge supplémentaire à la dernière minute.

---

## Exercice 3 : Scénarios stratégiques et opérationnels (6 points)

### Scénarios pour chaque couple Source du Risque (SR) et Objectif Visé (OV) :

#### SR1 : Défaillance matérielle / OV : Continuité du matériel informatique

1. **Scénario 1** : Panne soudaine de l’ordinateur principal  
   - **Gravité** : 4  
   - **Vraisemblance** : 2  
   - **Impact** : L'étudiant ne peut plus accéder à ses projets, perd du temps à trouver un remplacement (ordinateur de secours, réparation).

2. **Scénario 2** : Perte de données sur le disque dur  
   - **Gravité** : 3  
   - **Vraisemblance** : 3  
   - **Impact** : Une mauvaise gestion du stockage entraîne la perte de fichiers critiques, retardant les livrables.

#### SR2 : Erreur humaine / OV : Fiabilité des actions humaines

1. **Scénario 1** : Suppression accidentelle de fichiers critiques  
   - **Gravité** : 4  
   - **Vraisemblance** : 2  
   - **Impact** : Cela pourrait entraîner une perte irrémédiable de travail sans sauvegardes régulières.

2. **Scénario 2** : Mauvaise configuration de l’environnement de développement  
   - **Gravité** : 3  
   - **Vraisemblance** : 3  
   - **Impact** : Une configuration erronée peut générer des erreurs persistantes dans les projets, nécessitant des corrections importantes.

#### SR3 : Mauvaise gestion du temps / OV : Optimisation de l'organisation

1. **Scénario 1** : Procrastination sur les projets  
   - **Gravité** : 2  
   - **Vraisemblance** : 4  
   - **Impact** : L'étudiant reporte constamment le travail, ce qui entraîne une surcharge à la dernière minute, affectant la qualité des livrables.

2. **Scénario 2** : Accumulation de retard dans les révisions  
   - **Gravité** : 3  
   - **Vraisemblance** : 3  
   - **Impact** : Le retard dans les révisions complique la compréhension des cours, rendant les TPs et les examens plus difficiles.

### Scénarios les plus critiques :

1. **Scénario 2** : Mauvaise configuration de l’environnement de développement (SR : Erreur humaine / OV : Fiabilité des actions humaines)  
   - Gravité : 3  
   - Vraisemblance : 3  
   - Criticité : Élevée.

2. **Scénario 1** : Procrastination sur les projets (SR : Mauvaise gestion du temps / OV : Optimisation de l’organisation)  
   - Gravité : 2  
   - Vraisemblance : 4  
   - Criticité : Élevée.

---

## Exercice 4 : Traitement du risque (5 points)

### Contre-mesures pour chaque scénario :

1. **Scénario : Procrastination sur les projets**  
   - **Contre-mesure** : Mise en place d’un planning avec des objectifs intermédiaires.  
   - **Impact** : Réduit la vraisemblance en structurant mieux le travail et en évitant l'accumulation de tâches à la dernière minute.

2. **Scénario : Accumulation de retard dans les révisions**  
   - **Contre-mesure** : Révisions régulières et création de fiches de révision.  
   - **Impact** : Diminue la vraisemblance en incitant à une révision continue, empêchant l'accumulation.

3. **Scénario : Suppression accidentelle de fichiers critiques**  
   - **Contre-mesure** : Automatisation des sauvegardes via des outils comme Git/CodeFirst.  
   - **Impact** : Réduit la gravité en prévenant la perte de fichiers grâce à des commits réguliers et des sauvegardes automatisées.

4. **Scénario : Mauvaise configuration de l’environnement de développement**  
   - **Contre-mesure** : Utilisation de configurations prédéfinies (Docker, scripts) et tests réguliers de l'environnement.  
   - **Impact** : Diminue la vraisemblance en standardisant les configurations et en évitant les erreurs répétées.

5. **Scénario : Panne d’ordinateur ou perte de données**  
   - **Contre-mesure** : Avoir un ordinateur de secours et utiliser un stockage cloud pour des sauvegardes régulières.  
   - **Impact** : Réduit la gravité en permettant une transition rapide vers un autre appareil et en garantissant l'accès aux données.
