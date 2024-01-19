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

        // public static Utilisateur toUtilisateur(UtilisateurCourant appUtilisateur)
        // {
        //     return new Utilisateur(appUtilisateur.Pseudo, appUtilisateur.Nom, appUtilisateur.Prenom,
        //         appUtilisateur.Roles[0], appUtilisateur.MotDePasse, appUtilisateur.Mail, false);
        // }

        public static AppUtilisateur toAppUtilisateur(Utilisateur u)
        {
            return new AppUtilisateur(u.Pseudo, u.Nom, u.Prenom, u.Mail, u.Mdp, u.Role);
        }
    }    
}
