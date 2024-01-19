using Blazorise;
using Microsoft.AspNetCore.Components;
using VeraxShield.composants.formulaires.modeles;
using VeraxShield.factories;
using VeraxShield.modele.utilisateurs;
using VeraxShield.services.UtilisateursDataService;

namespace VeraxShield.composants.formulaires
{
    public partial class FormulaireAjout
    {
        
        [Parameter]
        public Validations Validations {get; set;}

        public FormulaireAjoutModele Modele {get; set;}

        [Inject]
        private NavigationManager NavigationManager {get; set;}

        [Inject]
        private IUtilisateursDataService utilisateursDataService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            this.Modele = new FormulaireAjoutModele();
            await base.OnInitializedAsync();
        }

        public async Task OnSubmit()
        {
            if (await this.Validations.ValidateAll())
            {
                Utilisateur nouvelUtilisateur = UtilisateursFactory.toUtilisateur(this.Modele);
                await this.utilisateursDataService.AjouterUtilisateur(nouvelUtilisateur);
                this.NavigationManager.NavigateTo("/utilisateurs/liste");
            }           
        }

        public async Task OnAnnulation()
        {
            this.NavigationManager.NavigateTo("/utilisateurs/liste");
        }
    }
}