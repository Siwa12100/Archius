public class AppUtilisateur
{
    public string MotDePasse { get; set; }
    public List<string> Roles { get; set; }
    public string Nom { get; set; }

    public AppUtilisateur(string nom, string mdp, String premierRole)
    {
        this.MotDePasse = mdp;
        this.Nom = nom;

        this.Roles = new List<string>();
        this.Roles.Add(premierRole);
    }

    public void ajouterRole(string nouveauRole)
    {
        this.Roles.Add(nouveauRole);
    }

    public void supprimerRole(string ancienRole)
    {
        this.Roles.Remove(ancienRole);
    }
}