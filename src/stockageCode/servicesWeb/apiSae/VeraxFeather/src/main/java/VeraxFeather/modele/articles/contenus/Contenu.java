package VeraxFeather.modele.articles.contenus;

public abstract class Contenu {
    protected int id;
    protected String typeContenu;

    public Contenu(int id) {
        this.id = id;
    }

    public int getId() {
        return this.id;
    }

    public String getTypeContenu() {
        return this.typeContenu;
    }

    protected void setTypeContenu(String typeContenu) {
        this.typeContenu = typeContenu;
    }
}
