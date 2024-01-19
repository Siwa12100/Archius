using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using VeraxShield.composants.affichages.utilisateurs;
using VeraxShield.modele.utilisateurs;
using VeraxShield.services.UtilisateursDataService;

namespace VeraxShield.composants.formulaires.modeles.attributsValidationCustoms
{
    public class PseudoCorrectAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            bool pseudoExisteDeja = false;
            var pseudo = (string)value;

            foreach (Utilisateur u in DatagridUtilisateurs.Utilisateurs)
            {
                if (u.Pseudo == pseudo && pseudo != DatagridUtilisateurs.UtilisateurSelectionne.Pseudo)
                {
                    return new ValidationResult("Le pseudo existe deja, choississez en un autre.");
                }
            }

            return ValidationResult.Success;
        }
    }
}