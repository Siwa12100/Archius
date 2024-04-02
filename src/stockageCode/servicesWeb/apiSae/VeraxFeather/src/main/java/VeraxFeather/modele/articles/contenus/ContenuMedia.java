package VeraxFeather.modele.articles.contenus;

public class ContenuMedia extends Contenu {
    private String titre;
    private String lien;

    public ContenuMedia(int id, String titre, String lien) {
        super(id);
        this.titre = titre;
        this.lien = lien;

        setTypeContenu("image");
    }

    public static ContenuMedia newVideo(int id, String titre, String lien) {
        ContenuMedia temp = new ContenuMedia(id, titre, lien);
        temp.setTypeContenu("video");
        return temp;
    }

    public String getTitre() {
        return titre;
    }

    public String getLien() {
        return lien;
    }

    public void setLien(String lien) {
        this.lien = lien;
    }

    public void setTitre(String titre) {
        this.titre = titre;
    }

    public java.util.Map<String, String> getContenu() {
        java.util.Map<String, String> contenu = new java.util.HashMap<>();
        contenu.put("titre", titre);
        contenu.put("contenu", lien);

        return contenu;
    }
}
