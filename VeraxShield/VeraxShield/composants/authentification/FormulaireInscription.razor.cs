using Blazorise;
using Microsoft.AspNetCore.Components;
using VeraxShield.composants.formulaires.modeles;

namespace VeraxShield.composants.authentification
{
    public partial class FormulaireInscription
    {
        
        [Parameter]
        public Validations Validations {get; set;}

        public RequeteInscription Requete {get; set;}

        [Inject]
        private NavigationManager NavigationManager {get; set;}

        [Inject]
        private DonneurEtat DonneurEtat {get; set;}

        public String Erreur {get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.Requete = new RequeteInscription();
            await base.OnInitializedAsync();
        }

        public async Task OnSubmit()
        {
            if (await this.Validations.ValidateAll())
            {
                    await DonneurEtat.Inscription(this.Requete);
                    NavigationManager.NavigateTo("/connexion");
            }           
        }

        public async Task OnAnnulation()
        {
            this.NavigationManager.NavigateTo("/connexion");
        }
    }
}