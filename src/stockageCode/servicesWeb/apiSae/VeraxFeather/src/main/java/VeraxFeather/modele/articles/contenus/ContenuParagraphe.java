package VeraxFeather.modele.articles.contenus;

import java.util.Map;

public class ContenuParagraphe extends Contenu {
    private String titre;
    private String texte;

    public ContenuParagraphe(int id, String titre, String texte) {
        super(id);
        this.titre = titre;
        this.texte = texte;

        setTypeContenu("paragraphe");
    }

    public String getTitre() {
        return titre;
    }

    public String getTexte() {
        return texte;
    }

    public void setTitre(String titre) {
        this.titre = titre;
    }

    public void setTexte(String texte) {
        this.texte = texte;
    }

    public java.util.Map<String, String> getContenu() {
        Map<String, String> contenu = new java.util.HashMap<>();
        contenu.put("titre", titre);
        contenu.put("contenu", texte);

        return contenu;
    }
}
