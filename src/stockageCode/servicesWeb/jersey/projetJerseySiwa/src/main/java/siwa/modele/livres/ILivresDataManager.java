package siwa.modele.livres;

import java.util.List;

public interface ILivresDataManager {

    List<Livre> getTousLesLivres();
    Livre getLivre(int id);

    void updateLivre(Livre livre);
    void supprimerLivre(int id);
    void creerLivre(Livre nouveauLivre);
}

