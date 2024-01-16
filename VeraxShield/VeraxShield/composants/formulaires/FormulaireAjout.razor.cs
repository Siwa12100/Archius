using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using VeraxShield.composants.formulaires.modeles;
using VeraxShield.factories;
using VeraxShield.modele.utilisateurs;
using VeraxShield.services.UtilisateursDataService;

namespace VeraxShield.composants.formulaires
{
    public partial class FormulaireAjout
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private FormulaireAjoutModele Modele { get; set; }

        [Inject]
        private IUtilisateursDataService utilisateursDataService { get; set; }

        public FormulaireAjout()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            this.Modele = new FormulaireAjoutModele();
            this.Modele.IsBan = false;

            await base.OnInitializedAsync();
        }

        public async void ajouterUtilisateur()
        {
            Utilisateur nouvelUtilisateur = UtilisateursFactory.toUtilisateur(this.Modele);
            await this.utilisateursDataService.AjouterUtilisateur(nouvelUtilisateur);

            //Console.WriteLine("Utilisateur bien ajouté !");

            this.NavigationManager.NavigateTo("/utilisateurs/liste");
        }
    }
}
