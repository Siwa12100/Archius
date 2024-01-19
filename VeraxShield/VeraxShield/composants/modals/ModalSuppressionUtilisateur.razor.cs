using Blazorise;
using Microsoft.AspNetCore.Components;
using VeraxShield.modele.utilisateurs;
using VeraxShield.services.UtilisateursDataService;

namespace VeraxShield.composants.modals
{
    public partial class ModalSuppressionUtilisateur
    {
        private Modal modalRef;

        [Parameter]
        public Utilisateur? utilisateur { get; set; }

        [Parameter]
        public EventCallback<bool> modalFerme {get; set;}
        
        protected async Task suppressionAnnulee()
        {
            await this.fermer();
            await this.modalFerme.InvokeAsync(false);
        }

        protected async Task suppressionConfirmee() 
        {
            await this.fermer();
            await this.modalFerme.InvokeAsync(true);
        }

        public async Task afficher()
        {
            if (this.utilisateur != null)
            {
                await modalRef.Show();
            }
        }

        private Task fermer()
        {
            return modalRef.Hide();
        }
    }
}
