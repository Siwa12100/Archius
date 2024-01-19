using Blazorise;
using Microsoft.AspNetCore.Components;
using VeraxShield.composants.formulaires.modeles;
using VeraxShield.composants.formulaires.modeles.attributsValidationCustoms;
using VeraxShield.factories;
using VeraxShield.modele.utilisateurs;
using VeraxShield.services.UtilisateursDataService;

namespace VeraxShield.composants.formulaires
{
    public partial class FormulaireModification
    {
        [Parameter]
        public Utilisateur Utilisateur {get; set;}

        [Parameter]
        public Validations Validations {get; set;}

        public FormulaireAjoutModele Modele {get; set;}

        [Inject]
        private IUtilisateursDataService utilisateursDataService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set;}

        protected override async Task OnParametersSetAsync()
        {
           //PseudoCorrectAttribute.Initialiser(await this.utilisateursDataService.getAllUtilisateurs());

            if (this.Utilisateur != null)
            {
                Utilisateur temp = new Utilisateur(" ", " ", " ", " ", " ", " ", true);
                this.Modele = UtilisateursFactory.toModele(this.Utilisateur);
            } else
            {
                Utilisateur temp = new Utilisateur(" ", " ", " ", " ", " ", " ", true);
                this.Modele = UtilisateursFactory.toModele(temp);
            }

            await base.OnParametersSetAsync();
        }

        
        protected async Task modifierUtilisateur()
        {
            if (await this.Validations.ValidateAll())
            {
                Utilisateur temp = UtilisateursFactory.toUtilisateur(this.Modele);

                if (this.Utilisateur.Pseudo != this.Modele.Pseudo)
                {
                    await this.utilisateursDataService.MettreAJourUtilisateur(this.Utilisateur, temp);
                }
                else
                {
                    await this.utilisateursDataService.MettreAJourUtilisateur(temp);
                }
                
                this.NavigationManager.NavigateTo("/utilisateurs/liste");
            }
        }

        protected async Task OnAnnulation()
        {
            this.NavigationManager.NavigateTo("/utilisateurs/liste");
        }
    }
}