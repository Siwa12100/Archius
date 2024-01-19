using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using VeraxShield.composants.formulaires.modeles.attributsValidationCustoms;
using VeraxShield.modele.utilisateurs;
using VeraxShield.services.UtilisateursDataService;

namespace VeraxShield.composants.formulaires.modeles
{
    public class FormulaireAjoutModele
    {
        [Required]
        [StringLength(20, ErrorMessage = "Le pseudo ne doit pas dépasser 20 caractères !")]
        [RegularExpression("^[a-zA-Z0-9_]+$", ErrorMessage = "Le pseudo contient des caractères interdits.")]
        [PseudoCorrect]
        public string Pseudo { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères !")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Le nom doit contenir uniquement des lettres.")]
        public String Nom {  get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Le prénom ne doit pas dépasser 20 caractères !")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Le prénom doit contenir uniquement des lettres.")]
        public String Prenom { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Veuillez entrer une adresse email valide.")]
        public String Mail { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Le mot de passe doit contenir uniquement des caractères alphanumériques.")]
        public String Mdp { get; set; }

        [Required]
        [RoleExistant]
        public String Role { get; set; }

        [Required]
        public Boolean IsBan { get; set; }

        public FormulaireAjoutModele(string pseudo, string nom, string prenom, string mail, string mdp, string role, bool isBan)
        {
            Pseudo = pseudo;
            Nom = nom;
            Prenom = prenom;
            Mail = mail;
            Mdp = mdp;
            IsBan = isBan;
        }

        public FormulaireAjoutModele()
        {
            //Pseudo = "temporairementVide";
            //Nom = "temporairementVide";
            //Prenom = "temporairementVide";
            //Mail = "temporairementVide";
            //Mdp = "temporairementVide";
            //IsBan = true;
        }
    }
}
