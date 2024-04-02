package VeraxFeather.controleurs;

import VeraxFeather.services.ArticleClientService;
import com.fasterxml.jackson.core.JsonProcessingException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.hateoas.EntityModel;
import org.springframework.hateoas.CollectionModel;
import org.springframework.hateoas.server.mvc.WebMvcLinkBuilder;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;
import VeraxFeather.modele.articles.IArticlesDataManager;
import VeraxFeather.modele.articles.stub.StubArticles;
import VeraxFeather.modele.articles.Article;

import javax.servlet.http.HttpServletRequest;
import java.util.List;
import java.util.stream.Collectors;

@RestController
public class ArticlesControleur {

    @Autowired
    private ArticleClientService articleClientService;

    private final IArticlesDataManager articlesDataManager = new StubArticles();

    @GetMapping(value="/articles", produces="application/json")
    @PreAuthorize("hasRole('USER')")
    public CollectionModel<EntityModel<Article>> getAllArticles() {
        List<EntityModel<Article>> articles = articlesDataManager.getAllArticles().stream()
                .map(article -> EntityModel.of(article,
                        WebMvcLinkBuilder.linkTo(WebMvcLinkBuilder.methodOn(ArticlesControleur.class).getArticle(article.getId())).withSelfRel(),
                        WebMvcLinkBuilder.linkTo(WebMvcLinkBuilder.methodOn(ArticlesControleur.class).getAllArticles()).withRel("articles")))
                .collect(Collectors.toList());

        return CollectionModel.of(articles,
                WebMvcLinkBuilder.linkTo(WebMvcLinkBuilder.methodOn(ArticlesControleur.class).getAllArticles()).withSelfRel());
    }

    @GetMapping(value="/articles/{id}", produces="application/json")
    @PreAuthorize("hasRole('USER')")
    public EntityModel<Article> getArticle(@PathVariable int id) {
        Article article = articlesDataManager.getArticle(id);
        return EntityModel.of(article,
                WebMvcLinkBuilder.linkTo(WebMvcLinkBuilder.methodOn(ArticlesControleur.class).getArticle(id)).withSelfRel(),
                WebMvcLinkBuilder.linkTo(WebMvcLinkBuilder.methodOn(ArticlesControleur.class).getAllArticles()).withRel("articles"));
    }

    @PostMapping(value="/articles/edit/{id}")
    @PreAuthorize("hasRole('USER')")
    public EntityModel<String> editArticle(@PathVariable int id, @RequestBody Article updateRequest, HttpServletRequest request) throws JsonProcessingException {
        String userToken = request.getHeader("Authorization");
        String response = articleClientService.editArticle(id, userToken, updateRequest);

        return EntityModel.of(response,
                WebMvcLinkBuilder.linkTo(WebMvcLinkBuilder.methodOn(ArticlesControleur.class).editArticle(id, updateRequest, request)).withSelfRel(),
                WebMvcLinkBuilder.linkTo(WebMvcLinkBuilder.methodOn(ArticlesControleur.class).getAllArticles()).withRel("articles"));
    }

    @DeleteMapping(value="/articles/supprimer/{id}")
    @PreAuthorize("hasRole('USER')")
    public EntityModel<String> deleteArticle(@PathVariable int id, HttpServletRequest request) {
        String userToken = request.getHeader("Authorization");
        String response = articleClientService.deleteArticle(id, userToken);

        return EntityModel.of(response,
                WebMvcLinkBuilder.linkTo(WebMvcLinkBuilder.methodOn(ArticlesControleur.class).deleteArticle(id, request)).withSelfRel(),
                WebMvcLinkBuilder.linkTo(WebMvcLinkBuilder.methodOn(ArticlesControleur.class).getAllArticles()).withRel("articles"));
    }

    @GetMapping(value="/test")
    public String testAPI() {
        return "L'API fonctionne correctement!";
    }
}


