@startuml
' Définition des Entités
class Groupe {
    + int id
    + string nom
    --
    + Collection<Utilisateur> getUtilisateurs()
    + void addUtilisateur(Utilisateur utilisateur)
    + void removeUtilisateur(Utilisateur utilisateur)
}

class Utilisateur {
    + int id
    + string nom
    + string role
    --
    + Groupe getGroupe()
    + void setGroupe(Groupe groupe)
    + Collection<Paillasse> getPaillasses()
    + void addPaillasse(Paillasse paillasse)
    + void removePaillasse(Paillasse paillasse)
}

class Paillasse {
    + int id
    + string nom
    + DateTime dateDerniereModification
    + DateTime dateDeCalendrier
    --
    + Utilisateur getProprietaire()
    + void setProprietaire(Utilisateur utilisateur)
    + Collection<Flacon> getFlacons()
    + void addFlacon(Flacon flacon)
    + void removeFlacon(Flacon flacon)
}

class Flacon {
    + int id
    + string nom
    + bool nourriture
    + array compteurs
    + array listeTypeDrosophile
    --
    + Paillasse getPaillasse()
    + void setPaillasse(Paillasse paillasse)
}

class TypeDrosophile {
    + int id
    + string nom
    + int fertilite
    --
    + Flacon getFlacon()
    + void setFlacon(Flacon flacon)
    + Collection<Phenotype> getPhenotypes()
    + void addPhenotype(Phenotype phenotype)
    + void removePhenotype(Phenotype phenotype)
}

class Phenotype {
    + int id
    + string nom
    + string caracteres
    + string image
}

class Gene {
    + int id
    + string nom
    --
    + Collection<Allele> getAlleles()
    + void addAllele(Allele allele)
    + void removeAllele(Allele allele)
}

class Allele {
    + int id
    + string nom
    --
    + Gene getGene()
    + void setGene(Gene gene)
}

' Définition des Interfaces de Repositories
interface IGroupeRepository {
    + find(int id): Groupe
    + save(Groupe groupe): void
    + remove(Groupe groupe): void
}

interface IUtilisateurRepository {
    + find(int id): Utilisateur
    + save(Utilisateur utilisateur): void
    + remove(Utilisateur utilisateur): void
}

interface IPaillasseRepository {
    + find(int id): Paillasse
    + save(Paillasse paillasse): void
    + remove(Paillasse paillasse): void
}

interface IFlaconRepository {
    + find(int id): Flacon
    + save(Flacon flacon): void
    + remove(Flacon flacon): void
}

' Définition des Implémentations de Repositories
class DoctrineGroupeRepository {
    + find(int id): Groupe
    + save(Groupe groupe): void
    + remove(Groupe groupe): void
}

class DoctrineUtilisateurRepository {
    + find(int id): Utilisateur
    + save(Utilisateur utilisateur): void
    + remove(Utilisateur utilisateur): void
}

class DoctrinePaillasseRepository {
    + find(int id): Paillasse
    + save(Paillasse paillasse): void
    + remove(Paillasse paillasse): void
}

class DoctrineFlaconRepository {
    + find(int id): Flacon
    + save(Flacon flacon): void
    + remove(Flacon flacon): void
}

' Définition des Interfaces de Services
interface IGroupeService {
    + createGroup(string nom): Groupe
    + getGroupById(int id): Groupe
    + updateGroup(int id, string nom): void
    + deleteGroup(int id): void
}

interface IUtilisateurService {
    + createUser(string nom, string role, int nbPaillassesMax): Utilisateur
    + getUserById(int id): Utilisateur
    + deleteUser(int id): void
}

interface IPaillasseService {
    + createPaillasse(string nom, int userId): Paillasse
    + getPaillasseById(int id): Paillasse
    + deletePaillasse(int id): void
}

interface IFlaconService {
    + createFlacon(array data, int paillasseId): Flacon
    + getFlaconById(int id): Flacon
    + deleteFlacon(int id): void
}

' Définition des Implémentations de Services
class GroupeService {
    + createGroup(string nom): Groupe
    + getGroupById(int id): Groupe
    + updateGroup(int id, string nom): void
    + deleteGroup(int id): void
}

class UtilisateurService {
    + createUser(string nom, string role, int nbPaillassesMax): Utilisateur
    + getUserById(int id): Utilisateur
    + deleteUser(int id): void
}

class PaillasseService {
    + createPaillasse(string nom, int userId): Paillasse
    + getPaillasseById(int id): Paillasse
    + deletePaillasse(int id): void
}

class FlaconService {
    + createFlacon(array data, int paillasseId): Flacon
    + getFlaconById(int id): Flacon
    + deleteFlacon(int id): void
}

' Relations entre Interfaces et Implémentations
IGroupeRepository <|-- DoctrineGroupeRepository
IUtilisateurRepository <|-- DoctrineUtilisateurRepository
IPaillasseRepository <|-- DoctrinePaillasseRepository
IFlaconRepository <|-- DoctrineFlaconRepository

IGroupeService <|-- GroupeService
IUtilisateurService <|-- UtilisateurService
IPaillasseService <|-- PaillasseService
IFlaconService <|-- FlaconService

' Dépendances entre Interfaces de Services et Entités
IGroupeService ..> Groupe
IUtilisateurService ..> Utilisateur
IPaillasseService ..> Paillasse
IFlaconService ..> Flacon

' Dépendances entre Services
FlaconService ..> IPaillasseService
PaillasseService ..> IUtilisateurService

' Relations entre Entités
Groupe "1" --> "0..*" Utilisateur
Utilisateur "1" --> "0..*" Paillasse
Paillasse "1" --> "0..*" Flacon
Flacon "1" --> "0..*" TypeDrosophile
TypeDrosophile "0..*" -- "0..*" Phenotype
Gene "1" --> "0..*" Allele
@enduml
