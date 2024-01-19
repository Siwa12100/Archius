using System.ComponentModel.DataAnnotations;

namespace VeraxShield.composants.formulaires.modeles.attributsValidationCustoms
{
    public class RoleExistantAttribute : ValidationAttribute
    {
        private List<string> RolesExistants {get; set;}

        public RoleExistantAttribute()
        {
            this.RolesExistants = new List<string>();
            this.RolesExistants.Add("admin");
            this.RolesExistants.Add("modo");
            this.RolesExistants.Add("invite");
            this.RolesExistants.Add("redacteur");
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var role = (string)value;

            foreach (string roleListe in this.RolesExistants)
            {
                if (roleListe == role)
                {
                    return ValidationResult.Success;;
                }
            }
            
            return new ValidationResult("Le role n'existe pas.");
        }
    }
}