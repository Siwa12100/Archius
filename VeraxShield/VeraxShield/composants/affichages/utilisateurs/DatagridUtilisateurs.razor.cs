using Microsoft.AspNetCore.Components;
using VeraxShield.composants.formulaires.modeles;
using VeraxShield.factories;
using VeraxShield.modele.utilisateurs;
using VeraxShield.services.UtilisateursDataService;
using VeraxShield.composants.modals;

namespace VeraxShield.composants.affichages.utilisateurs
{
    public partial class DatagridUtilisateurs
    {
        [Inject]
		private IUtilisateursDataService utilisateursDataService { get; set; }

		public static List<Utilisateur> Utilisateurs { get; set; }

		[Inject]
		private NavigationManager NavigationManager { get; set; }

		[Inject]
		private DonneurEtat DonneurEtat {get; set;}

		public static Utilisateur? UtilisateurSelectionne;

		[Parameter]
		public ModalSuppressionUtilisateur Modal {get; set;}

		protected override async Task OnInitializedAsync()
		{
            //await this.utilisateursDataService.resetDataUtilisateurs();

			DatagridUtilisateurs.UtilisateurSelectionne = null;
            DatagridUtilisateurs.Utilisateurs = await this.utilisateursDataService.getAllUtilisateurs();
			await base.OnInitializedAsync();
		}

        private async Task HandleUtilisateurSupprime()
        {
            DatagridUtilisateurs.Utilisateurs = await this.utilisateursDataService.getAllUtilisateurs();
            StateHasChanged(); // Actualiser la vue
        }

		public async Task fermetureModal(bool val) {

			if (val) {
				await this.supprimerUtilisateur(DatagridUtilisateurs.UtilisateurSelectionne);
			}
		}

		public async Task supprimerUtilisateur(Utilisateur u)
		{
			await this.utilisateursDataService.SupprimerUtilisateur(u);
			DatagridUtilisateurs.Utilisateurs = await this.utilisateursDataService.getAllUtilisateurs();
			
			this.NavigationManager.NavigateTo("/utilisateurs/liste");
		}

		public async Task afficherModal()
		{
			if (this.Modal != null) {
				await this.Modal.afficher();
			}
		}

		public void modifierUtilisateur()
		{
			if (DatagridUtilisateurs.UtilisateurSelectionne != null)
			{
				this.NavigationManager.NavigateTo("/utilisateurs/modifier/" + DatagridUtilisateurs.UtilisateurSelectionne.Pseudo);
			}
		}

		public async Task onClickBoutonSuppression(String Pseudo)
		{
			DatagridUtilisateurs.UtilisateurSelectionne= await this.utilisateursDataService.getUtilisateurFromPseudo(Pseudo);
			if (this.DonneurEtat._utilisateurCourant.Pseudo != DatagridUtilisateurs.UtilisateurSelectionne.Pseudo)
			{
				await this.afficherModal();
			}
		}

		public async Task onClickBoutonModification(String Pseudo)
		{
			DatagridUtilisateurs.UtilisateurSelectionne = await this.utilisateursDataService.getUtilisateurFromPseudo(Pseudo);
			this.modifierUtilisateur();
		}

		public async Task boutonclick()
		{
			NavigationManager.NavigateTo("/utilisateurs/ajouter");
		}
    }
}