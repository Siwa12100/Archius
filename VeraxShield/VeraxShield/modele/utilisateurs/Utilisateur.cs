namespace VeraxShield.modele.utilisateurs
{
	public class Utilisateur
	{
		public String Pseudo {  get; set; }
		public String Mail { get; set; }
		public String Nom {  get; set; }
		public String Prenom { get; set; }
		public String Role { get; set; }
		public String Mdp { get; set; }
		public Boolean IsBan { get; set; }

		public Utilisateur (String Pseudo, String Nom, String Prenom, String Role, String Mdp, String Mail, Boolean IsBan)
		{
			this.Pseudo = Pseudo;
			this.Nom = Nom;
			this.Prenom = Prenom;
			this.Role = Role;
			this.Mdp = Mdp;
			this.Mail = Mail;
			this.IsBan = IsBan;
		}
	}
}
