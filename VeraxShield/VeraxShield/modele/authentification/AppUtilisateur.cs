public class AppUtilisateur
{
    public string MotDePasse { get; set; }
    public List<string> Roles { get; set; }
    public string Pseudo { get; set; }
    public string Prenom {  get; set; }
    public string Nom { get; set; }
    public string Mail { get; set; }


    public AppUtilisateur(string pseudo,string nom, string prenom, string mail, string mdp, String premierRole)
    {
        this.MotDePasse = mdp;
        this.Pseudo = pseudo;
        this.Mail = mail;
        this.Prenom = prenom;
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