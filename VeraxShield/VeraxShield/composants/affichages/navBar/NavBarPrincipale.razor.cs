using Microsoft.AspNetCore.Components;

namespace VeraxShield.composants.affichages.navBar
{
    public partial class NavBarPrincipale
    {
        [Inject]
        private DonneurEtat DonneurEtat { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private async Task NavUtilisateurs()
        {
            NavigationManager.NavigateTo("/utilisateurs/liste");
        }

        private async Task NavArticles()
        {
            NavigationManager.NavigateTo("/articles/liste");
        }

        private async Task NavModerateurs()
        {
            NavigationManager.NavigateTo("/moderateurs/liste");
        }

        private async Task Deconnecter()
        {
            await DonneurEtat.Deconnexion();
            NavigationManager.NavigateTo("/connexion");
        }

        private async Task NavAccueil()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
