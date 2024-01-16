using System.ComponentModel.DataAnnotations;

namespace VeraxShield.composants.formulaires.modeles
{
    public class FormulaireAjoutModele
    {
        [Required]
        [StringLength(20, ErrorMessage = "Le pseudo ne doit pas dépasser 20 caractères !")]
        public String Pseudo { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Le nom ne doit pas dépasser 20 caractères !")]
        public String Nom {  get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Le prénom ne doit pas dépasser 20 caractères !")]
        public String Prenom { get; set; }

        [Required]
        public String Mail { get; set; }

        [Required]
        public String Mdp { get; set; }

        [Required]
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
