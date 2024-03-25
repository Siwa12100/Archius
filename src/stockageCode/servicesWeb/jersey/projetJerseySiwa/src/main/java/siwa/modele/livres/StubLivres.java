package siwa.modele.livres;



import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

public class StubLivres implements ILivresDataManager {
    private final List<Livre> livres;

    public StubLivres() {
        livres = new ArrayList<>();
        initialiserDonnees();
    }

    private void initialiserDonnees() {
        livres.add(new Livre(1, 300, "Le Petit Prince", "Une histoire touchante d'un prince voyageant entre les planètes."));
        livres.add(new Livre(2, 250, "1984", "Roman dystopique sur une société sous surveillance constante."));
        livres.add(new Livre(3, 500, "Les Misérables", "Une épopée dramatique dans la France du 19e siècle."));
        livres.add(new Livre(4, 150, "Le Meilleur des mondes", "Une dystopie sur un futur technocratique et planifié."));
        livres.add(new Livre(5, 450, "L'Étranger", "L'histoire d'un homme indifférent qui commet un meurtre."));
        livres.add(new Livre(6, 220, "Fahrenheit 451", "Un futur où les livres sont interdits et brûlés."));
        livres.add(new Livre(7, 350, "Harry Potter à l'école des sorciers", "Le début des aventures d'un jeune sorcier."));
        livres.add(new Livre(8, 280, "Le Seigneur des anneaux: La Communauté de l'anneau", "La quête pour détruire l'Anneau."));
        livres.add(new Livre(9, 320, "Le Hobbit", "L'aventure préliminaire au Seigneur des Anneaux."));
        livres.add(new Livre(10, 200, "Pride and Prejudice", "Une comédie romantique sur les moeurs en Angleterre."));
    }

    @Override
    public List<Livre> getTousLesLivres() {
        return new ArrayList<>(livres);
    }

    @Override
    public Livre getLivre(int id) {
        return livres.stream()
                .filter(livre -> livre.getId() == id)
                .findFirst()
                .orElse(null);
    }

    @Override
    public void updateLivre(Livre livre) {
        Optional<Livre> livreExistant = livres.stream()
                .filter(l -> l.getId() == livre.getId())
                .findFirst();
        livreExistant.ifPresent(l -> {
            l.setTitre(livre.getTitre());
            l.setNombreDePages(livre.getNombreDePages());
            l.setDescription(livre.getDescription());
        });
    }

    @Override
    public void supprimerLivre(int id) {
        livres.removeIf(livre -> livre.getId() == id);
    }

    @Override
    public void creerLivre(Livre nouveauLivre) {
        int maxId = livres.stream().mapToInt(Livre::getId).max().orElse(0) + 1;
        nouveauLivre.setId(maxId);
        livres.add(nouveauLivre);
    }
}
