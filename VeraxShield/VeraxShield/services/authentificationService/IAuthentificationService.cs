using VeraxShield.composants.formulaires.modeles;

public interface IAuthentificationService
{
    public UtilisateurCourant GetUtilisateur(String nom);
    public void Connexion(RequeteConnexion requete);
    public void Inscription(RequeteInscription requete);
}