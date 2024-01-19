using Microsoft.AspNetCore.Components;
using VeraxShield.modele.utilisateurs;
using VeraxShield.services.UtilisateursDataService;

namespace VeraxShield.pages.utilisateurs
{
    public partial class ModifierUtilisateur
    {
        [Parameter]
        public String Pseudo {get; set;}

        public Utilisateur Utilisateur { get; set;}

        [Inject]
        private IUtilisateursDataService utilisateursDataService {get; set;}

        //private Utilisateur utilisateur {get; set;}

        protected override async Task OnInitializedAsync()
		{
            Console.WriteLine("Passage dans le OnInitializedAsync...");

            this.Utilisateur = await this.utilisateursDataService.getUtilisateurFromPseudo(this.Pseudo);

            if (Utilisateur != null)
            {
                Console.WriteLine("Pseudo du mec : " + this.Utilisateur.Pseudo);
            }
            else
            {
                Console.WriteLine("L'utilisateur est null...");
            }

            Console.WriteLine("Fin du OnInitializedParent");
			await base.OnInitializedAsync();
		}
    }
}