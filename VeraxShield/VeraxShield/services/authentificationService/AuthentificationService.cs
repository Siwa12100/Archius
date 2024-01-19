using System.Linq;
using System.Security.Claims;
using VeraxShield.composants.formulaires.modeles;
using VeraxShield.modele.utilisateurs;
using VeraxShield.services.UtilisateursDataService;

public class AuthentificationService : IAuthentificationService
{
    private List<Utilisateur> UtilisateursApplication {get; set;}
    private IUtilisateursDataService _utilisateursDataService;
   
    public AuthentificationService(IUtilisateursDataService utilisateursDataService) 
    {
        Console.WriteLine("Passage dans le constructeur !");
        this._utilisateursDataService = utilisateursDataService;
        this.UtilisateursApplication = new List<Utilisateur>();
    }

    public async Task Connexion(RequeteConnexion requete)
    {
        await this.MajUtilisateurs();
        var utilisateur = this.UtilisateursApplication.FirstOrDefault(w => w.Pseudo == requete.Pseudo && w.Mdp == requete.MotDePasse);

        if (utilisateur == null)
        {
            throw new Exception("Pseudo ou mot de passe invalide ! ");
        }
    }

    public async Task<UtilisateurCourant> GetUtilisateur(string pseudo)
    {
        await this.MajUtilisateurs();
        var utilisateur = this.UtilisateursApplication.FirstOrDefault(w => w.Pseudo == pseudo);

        if (utilisateur == null)
        {
            throw new Exception("Pseudo ou mot de passe invalide !");
        }

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Role, utilisateur.Role));

        return new UtilisateurCourant
        {
            EstAuthentifie = true,
            Pseudo = utilisateur.Pseudo,
            Claims = claims.ToDictionary(c => c.Type, c => c.Value)
        };
    }

    public async Task Inscription(RequeteInscription requete)
    {
         await this._utilisateursDataService.AjouterUtilisateur(new Utilisateur(requete.Pseudo, requete.Nom, requete.Prenom, "invite",
             requete.MotDePasse, requete.Mail,  false));
        
         await this.MajUtilisateurs();
    }

    public async Task MajUtilisateurs()
    {
        this.UtilisateursApplication = await this._utilisateursDataService.getAllUtilisateurs();
    }
}