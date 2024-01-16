using Microsoft.AspNetCore.Components;
using VeraxShield.composants.formulaires.modeles;
using VeraxShield.factories;
using VeraxShield.modele.utilisateurs;
using VeraxShield.services.UtilisateursDataService;

namespace VeraxShield.composants.formulaires
{
    public partial class FormulaireModification
    {
        [Parameter]
        public Utilisateur Utilisateur {get; set;}

        public FormulaireAjoutModele Modele {get; set;}

        [Inject]
        private IUtilisateursDataService utilisateursDataService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set;}

        protected override async Task OnParametersSetAsync()
        {
           
            if (this.Utilisateur != null)
            {
                Console.WriteLine("Le mec encore trouvé : " +  this.Utilisateur.Pseudo);
                Utilisateur temp = new Utilisateur(" ", " ", " ", " ", " ", " ", true);
                this.Modele = UtilisateursFactory.toModele(this.Utilisateur);
            } else
            {
                Console.WriteLine("Le mec nul cette fois...");
                Utilisateur temp = new Utilisateur(" ", " ", " ", " ", " ", " ", true);
                this.Modele = UtilisateursFactory.toModele(temp);
            }
            

            


            Console.WriteLine("Fin du onParameterEnfant");
            await base.OnParametersSetAsync();
        }

        
        protected async Task modifierUtilisateur()
        {
            Utilisateur temp = UtilisateursFactory.toUtilisateur(this.Modele);
            await this.utilisateursDataService.MettreAJourUtilisateur(temp);

            this.NavigationManager.NavigateTo("/utilisateurs/liste");
        }
    }
}