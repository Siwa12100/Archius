using System.ComponentModel.DataAnnotations;

namespace VeraxShield.composants.formulaires.modeles
{
    public class RequeteInscription
    {
        [Required]
        public string MotDePasse { get; set; }

        [Required]
        [Compare(nameof(MotDePasse), ErrorMessage = "Les mots de passe diffèrent !")]
        public string MotDePasseConfirmation { get; set; }

        [Required]
        public string Nom { get; set; }
    }
}
