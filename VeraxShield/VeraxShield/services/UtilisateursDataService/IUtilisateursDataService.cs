using VeraxShield.modele.utilisateurs;

namespace VeraxShield.services.UtilisateursDataService
{
	public interface IUtilisateursDataService
	{
		public Task<List<Utilisateur>> getAllUtilisateurs();

		public Task SaveAllUtilisateurs(List<Utilisateur> list);

		public Task resetDataUtilisateurs();
		public Task AjouterUtilisateur(Utilisateur u);

		public Task SupprimerUtilisateur(Utilisateur u);

		public Task MettreAJourUtilisateur(Utilisateur u);
		public Task MettreAJourUtilisateur(Utilisateur ancienneVersion, Utilisateur nouvelleVersion);

		public Task<Utilisateur> getUtilisateurFromPseudo(String pseudo);
	}
}
