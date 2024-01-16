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

		public List<Utilisateur> Utilisateurs { get; set; }

		[Inject]
		private NavigationManager NavigationManager { get; set; }

		private Utilisateur? utilisateurSelectionne;

		[Parameter]
		public ModalSuppressionUtilisateur Modal {get; set;}

		protected override async Task OnInitializedAsync()
		{
            //await this.utilisateursDataService.resetDataUtilisateurs();

			this.utilisateurSelectionne = null;
            this.Utilisateurs = await this.utilisateursDataService.getAllUtilisateurs();
			await base.OnInitializedAsync();
		}

        private async Task HandleUtilisateurSupprime()
        {
            this.Utilisateurs = await this.utilisateursDataService.getAllUtilisateurs();
            StateHasChanged(); // Actualiser la vue
        }

		public async Task fermetureModal(bool val) {

			if (val) {
				await this.supprimerUtilisateur(this.utilisateurSelectionne);
			}
		}

		public async Task supprimerUtilisateur(Utilisateur u)
		{
			await this.utilisateursDataService.SupprimerUtilisateur(u);
			this.Utilisateurs = await this.utilisateursDataService.getAllUtilisateurs();
			
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
			if (this.utilisateurSelectionne != null)
			{
				this.NavigationManager.NavigateTo("/utilisateurs/modifier/" + utilisateurSelectionne.Pseudo);
			}
		}

		public async Task onClickBoutonSuppression(String Pseudo)
		{
			this.utilisateurSelectionne = await this.utilisateursDataService.getUtilisateurFromPseudo(Pseudo);
			await this.afficherModal();
		}

		public async Task onClickBoutonModification(String Pseudo)
		{
			this.utilisateurSelectionne = await this.utilisateursDataService.getUtilisateurFromPseudo(Pseudo);
			this.modifierUtilisateur();
		}
    }
}