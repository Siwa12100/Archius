package VeraxFeather.services;

import VeraxFeather.modele.articles.Article;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;
import org.springframework.http.*;

@Service
public class ArticleClientService {

    private final RestTemplate restTemplate;
    private final String urlApiDistante = "http://181.214.189.133:9094";

    @Autowired
    public ArticleClientService(RestTemplate restTemplate) {
        this.restTemplate = restTemplate;
    }

    public String editArticle(int id, String userToken, Article updateRequest) throws JsonProcessingException {
        HttpHeaders headers = new HttpHeaders();
        headers.setContentType(MediaType.APPLICATION_JSON);
        headers.set("Authorization", userToken);
        headers.set("Api-Token", "token-api");

        ObjectMapper mapper = new ObjectMapper();
        String requestBody = mapper.writeValueAsString(updateRequest);

        HttpEntity<String> entity = new HttpEntity<>(requestBody, headers);

        String chemin = urlApiDistante + "/modifier/";

        ResponseEntity<String> response = restTemplate.exchange(chemin + id, HttpMethod.POST, entity, String.class);

        return response.getBody();
    }

    public String deleteArticle(int id, String userToken) {
        HttpHeaders headers = new HttpHeaders();
        headers.set("Authorization", userToken);

        HttpEntity<String> entity = new HttpEntity<>(headers);
        ResponseEntity<String> response = restTemplate.exchange(urlApiDistante + id, HttpMethod.DELETE, entity, String.class);

        return response.getBody();
    }
}
