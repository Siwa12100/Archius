package VeraxFeather.modele.articles;
import VeraxFeather.modele.articles.contenus.Contenu;

import java.util.ArrayList;
import java.util.List;

public class Article {
    private int id;
    private String titre;
    private String description;
    private String temps;
    private String date;
    private String auteur;

    // politique, environnement, culture, economie
    private String categorie;
    private String imagePrincipale;

    private double note;
    private List<Contenu> lContenus;

    public Article(int id, String titre, String description, String temps, String date, String auteur, String imagePrincipale, String categorie) {

        this.id = id;
        this.titre = titre;
        this.description = description;
        this.temps = temps;
        this.date = date;
        this.auteur = auteur;
        this.imagePrincipale = imagePrincipale;
        this.lContenus = new ArrayList<>();
        this.note = 1;
        this.categorie = categorie;
    }

    public void remplirArticle(List<Contenu> lContenus) {
        this.lContenus.addAll(lContenus);
    }

    public List<Contenu> getContenus() {
        return lContenus;
    }

    public int getId() {
        return id;
    }

    public String getImagePrincipale() {
        return imagePrincipale;
    }

    public String getAuteur() {
        return auteur;
    }

    public String getTitre() {
        return titre;
    }

    public String getDescription() {
        return description;
    }

    public String getTemps() {
        return temps;
    }

    public String getDate() {
        return date;
    }

    public double getNote() {
        return note;
    }

    public String getCategorie() {
        return this.categorie;
    }
}
