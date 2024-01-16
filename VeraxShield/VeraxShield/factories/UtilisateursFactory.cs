using VeraxShield.composants.formulaires.modeles;
using VeraxShield.modele.utilisateurs;

namespace VeraxShield.factories
{
    public static class UtilisateursFactory
    {
        public static Utilisateur toUtilisateur(FormulaireAjoutModele modele)
        {
            Utilisateur temp = new Utilisateur(
                modele.Pseudo,
                modele.Nom,
                modele.Prenom,
                modele.Role,
                modele.Mdp,
                modele.Mail,
                modele.IsBan);

            return temp;
        }

        public static FormulaireAjoutModele toModele(Utilisateur u)
        {
            FormulaireAjoutModele temp = new FormulaireAjoutModele();
            temp.Pseudo = u.Pseudo;
            temp.Nom = u.Nom;
            temp.Prenom = u.Prenom;
            temp.Mdp = u.Mdp;
            temp.Role = u.Role;
            temp.Mail = u.Mail;
            temp.IsBan = u.IsBan;

            return temp;
        }
    }    
}
