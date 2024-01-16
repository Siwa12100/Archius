
using Blazorise;
using Microsoft.AspNetCore.Components;
using VeraxShield.composants.formulaires.modeles;

namespace VeraxShield.composants.authentification
{
    public partial class FormulaireConnexion
    {
        [Parameter]
        public Validations Validations {get; set;}

        public RequeteConnexion Requete {get; set;}

        [Inject]
        private NavigationManager NavigationManager {get; set;}

        [Inject]
        private DonneurEtat DonneurEtat {get; set;}

        public String Erreur {get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.Requete = new RequeteConnexion();
        }

        public async Task OnSubmit()
        {
            if (await this.Validations.ValidateAll())
            {
                try
                {
                    await DonneurEtat.Connexion(this.Requete);
                    NavigationManager.NavigateTo("/");
                    
                } catch (Exception ex)
                {
                    this.Erreur = ex.Message;
                }
            }           
        }
    }  
}