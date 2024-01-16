using System.Security.Claims;
using VeraxShield.composants.formulaires.modeles;

public class AuthentificationService : IAuthentificationService
{

    private static readonly List<AppUtilisateur> utilisateursApplication;



    static AuthentificationService() 
    {
        utilisateursApplication = new List<AppUtilisateur>();
        utilisateursApplication.Add(new AppUtilisateur("Admin", "Admin", "admin"));
    }

    public void Connexion(RequeteConnexion requete)
    {
        var utilisateur = utilisateursApplication.FirstOrDefault(w => w.Nom == requete.Nom && w.MotDePasse == requete.MotDePasse);

        if (utilisateur == null)
        {
            throw new Exception("Pseudo ou mot de passe invalide ! ");
        }
    }

    public UtilisateurCourant GetUtilisateur(string nom)
    {
        var utilisateur = utilisateursApplication.FirstOrDefault(w => w.Nom == nom);

        if (utilisateur == null)
        {
            throw new Exception("Pseudo ou mot de passe invalide !");
        }

        var claims = new List<Claim>();
        claims.AddRange(utilisateur.Roles.Select(s => new Claim(ClaimTypes.Role, s)));

        return new UtilisateurCourant
        {
            EstAuthentifie = true,
            Nom = utilisateur.Nom,
            Claims = claims.ToDictionary(c => c.Type, c => c.Value)
        };
    }

    public void Inscription(RequeteInscription requete)
    {
         utilisateursApplication.Add(new AppUtilisateur(requete.Nom, requete.MotDePasse, "invite" ));
    }
}