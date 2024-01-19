using VeraxShield.composants.formulaires.modeles;

public interface IAuthentificationService
{
    public Task<UtilisateurCourant> GetUtilisateur(String nom);
    public Task Connexion(RequeteConnexion requete);
    public Task Inscription(RequeteInscription requete);
}