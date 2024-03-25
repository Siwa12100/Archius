package siwa.modele.livres;

public class Livre {
    private int id;
    private int nombreDePages;
    private String titre;
    private String description;

    public Livre(int id, int nombreDePages, String titre, String description) {
        this.id = id;
        this.nombreDePages = nombreDePages;
        this.titre = titre;
        this.description = description;
    }

    public int getId() {
        return id;
    }

    public int getNombreDePages() {
        return nombreDePages;
    }

    public String getTitre() {
        return titre;
    }

    public String getDescription() {
        return description;
    }

    public void setId(int id) {
        this.id = id;
    }

    public void setNombreDePages(int nombreDePages) {
        this.nombreDePages = nombreDePages;
    }

    public void setTitre(String titre) {
        this.titre = titre;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    @Override
    public String toString() {
        return "Livre{" +
                "id=" + id +
                ", nombreDePages=" + nombreDePages +
                ", titre='" + titre + '\'' +
                ", description='" + description + '\'' +
                '}';
    }
}
