package VeraxFeather.modele.articles;

import java.util.List;

public interface IArticlesDataManager {
    List<Article> getAllArticles();

    Article getArticle(int id);

    List<Article> getDerniersArticles(int nbArticles);
}
