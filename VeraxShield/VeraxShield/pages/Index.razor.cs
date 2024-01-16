using Microsoft.AspNetCore.Components;

namespace VeraxShield.pages
{
    public partial class Index
    {
        [Inject]
        private DonneurEtat DonneurEtat {get; set;}

        [Inject]
        private NavigationManager NavigationManager {get; set;}
         private async Task SeDeconnecter()
        {
            await DonneurEtat.Deconnexion();
            NavigationManager.NavigateTo("/connexion");
        }

        private async Task SeConnecter()
        {
            NavigationManager.NavigateTo("/connexion");
        }
    }
}