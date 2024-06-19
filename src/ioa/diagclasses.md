@startuml

package com.mongodb.starter.controleurs.auteurs {
    class AuteurControleur {
        - final static Logger LOGGER
        - final AuteurService auteurService
        + postAuteur(auteurDTO : AuteurDTO) : AuteurDTO
        + postAuteurs(auteurDTOs : List<AuteurDTO>) : List<AuteurDTO>
        + getAuteurs() : List<AuteurDTO>
        + getAuteur(id : String) : ResponseEntity<AuteurDTO>
        + getCount() : Long
        + deleteAuteur(id : String) : Long
        + deleteAllAuteurs() : Long
        + putAuteur(auteurDTO : AuteurDTO) : AuteurDTO
        + putAuteurs(auteurDTOs : List<AuteurDTO>) : Long
        + handleAllExceptions(e : RuntimeException) : Exception
    }
}

package com.mongodb.starter.controleurs.googleDrive {
    class GoogleDriveControleur {
        - GoogleDriveService googleDriveService
        - GoogleDriveManager googleDriveManager
        - LivreService livreService
        + getImage(fileId : String) : ResponseEntity<byte[]>
        + creerDossierPourLivre(id : String) : ResponseEntity<?>
        + supprimerDossierPourLivre(id : String) : ResponseEntity<?>
        + uploadPremiereCouverture(id : String, imageFile : byte[]) : ResponseEntity<?>
        + uploadQuatriemeCouverture(id : String, imageFile : byte[]) : ResponseEntity<?>
        + modifierPremiereCouverture(id : String, imageFile : byte[]) : ResponseEntity<?>
        + modifierQuatriemeCouverture(id : String, imageFile : byte[]) : ResponseEntity<?>
        + supprimerPremiereCouverture(id : String) : ResponseEntity<?>
        + supprimerQuatriemeCouverture(id : String) : ResponseEntity<?>
        + uploaderNouvelleImageDansSommaire(id : String, imageFile : byte[]) : ResponseEntity<?>
        + modifierImageDansSommaire(id : String, position : int, fichier : byte[]) : ResponseEntity<?>
        + supprimerImageDansSommaire(id : String, position : int) : ResponseEntity<?>
        - uploaderImageSommaire(fichier : byte[], id : String, position : int, typeMethodeHttp : String) : ResponseEntity<?>
        - uploadCouverture(imageFile : byte[], id : String, nomImage : String, typeMethodeHttp : String) : ResponseEntity<?>
        - supprimerCouverture(id : String, nomCouverture : String) : ResponseEntity<?>
        - jsonResponse(message : String) : String
        - detecterTypeMime(typeImage : String) : String
    }
}

package com.mongodb.starter.controleurs.livres {
    class LivreControleur {
        - final static Logger LOGGER
        - final LivreService livreService
        + postLivre(livreDTO : LivreDTO) : LivreDTO
        + getLivres() : List<LivreDTO>
        + getLivre(id : String) : ResponseEntity<LivreDTO>
        + getCount() : Long
        + deleteLivre(id : String) : Long
        + deleteAllLivres() : Long
        + putLivre(livreDTO : LivreDTO) : LivreDTO
        + putLivres(livreDTOs : List<LivreDTO>) : Long
        + handleAllExceptions(e : RuntimeException) : Exception
    }
}

package com.mongodb.starter.dtos.auteurs {
    class AuteurDTO {
        // Les attributs et méthodes de cette classe sont à définir
    }
}

package com.mongodb.starter.services.auteurs {
    class AuteurService {
        + save(auteurDTO : AuteurDTO) : AuteurDTO
        + saveAll(auteurDTOs : List<AuteurDTO>) : List<AuteurDTO>
        + findAll() : List<AuteurDTO>
        + findOne(id : String) : AuteurDTO
        + count() : Long
        + delete(id : String) : Long
        + deleteAll() : Long
        + update(auteurDTO : AuteurDTO) : AuteurDTO
        + update(auteurDTOs : List<AuteurDTO>) : Long
    }
}

package com.mongodb.starter.services.googleDrive {
    class GoogleDriveService {
        + recupererUneImage(fileId : String) : byte[]
        + detecterFormatImage(imageBytes : byte[]) : String
        + creerDossierDeLivre(id : String, titre : String) : String
        + supprimerDossierDeLivre(id : String, titre : String) : String
        + televerserUneImageDeSommaire(id : String, fichier : byte[], position : int, typeMethodeHttp : String) : String
        + televerserUneCouverture(id : String, imageFile : byte[], nomImage : String, typeMethodeHttp : String) : String
        + supprimerUneCouverture(id : String, nomCouverture : String) : String
        + supprimerUneImageDeSommaire(id : String, position : int) : String
    }
}

package com.mongodb.starter.managers.googleDrive {
    class GoogleDriveManager {
        // Les attributs et méthodes de cette classe sont à définir
    }
}

package com.mongodb.starter.dtos.livres {
    class LivreDTO {
        // Les attributs et méthodes de cette classe sont à définir
        + titre() : String
    }
}

package com.mongodb.starter.services.livres {
    class LivreService {
        + sauvegarder(livreDTO : LivreDTO) : LivreDTO
        + recupererUnLivre(id : String) : LivreDTO
        + recupererTout() : List<LivreDTO>
        + compter() : Long
        + supprimer(id : String) : Long
        + supprimerTout() : Long
        + modifier(livreDTO : LivreDTO) : LivreDTO
        + modifier(livreDTOs : List<LivreDTO>) : Long
    }
}

AuteurControleur --> AuteurService
GoogleDriveControleur --> GoogleDriveService
GoogleDriveControleur --> GoogleDriveManager
GoogleDriveControleur --> LivreService
LivreControleur --> LivreService

@enduml
