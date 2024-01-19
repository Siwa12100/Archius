using System.ComponentModel.DataAnnotations;

namespace VeraxShield.composants.formulaires.modeles
{
    public class RequeteConnexion
    {
        [Required(ErrorMessage="le champ est obligatoire !")]
        public string MotDePasse { get; set; }

        [Required(ErrorMessage="le champ est obligatoire !")]
        public string Pseudo { get; set; }
    }
}
