using System.ComponentModel.DataAnnotations;
using VeraxShield.composants.formulaires.modeles.attributsValidationCustoms;

namespace VeraxShield.composants.formulaires.modeles
{
    public class RequeteInscription
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Le mot de passe doit contenir uniquement des caractères alphanumériques.")]
        public string MotDePasse { get; set; }

        [Required]
        [Compare(nameof(MotDePasse), ErrorMessage = "Les mots de passe diffèrent !")]
        public string MotDePasseConfirmation { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Le nom doit contenir uniquement des lettres.")]
        [StringLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères !")]
        public string Nom { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Le prénom ne doit pas dépasser 20 caractères !")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Le prénom doit contenir uniquement des lettres.")]
        public string Prenom {get; set;}

        [Required]
        [StringLength(20, ErrorMessage = "Le pseudo ne doit pas dépasser 20 caractères !")]
        [RegularExpression("^[a-zA-Z0-9_]+$", ErrorMessage = "Le pseudo contient des caractères interdits.")]
        [PseudoCorrect]
        public string Pseudo { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Veuillez entrer une adresse email valide.")]
        public string Mail { get; set;}
    }
}
