package VeraxFeather.config;

import VeraxFeather.authentification.AuthenticationRequest;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.client.RestTemplate;

import org.springframework.http.client.ClientHttpRequestInterceptor;
import org.springframework.web.client.RestTemplate;

import java.util.List;

@Configuration
public class RestTemplateConfig {

    @Bean
    public RestTemplate restTemplate() {
        RestTemplate restTemplate = new RestTemplate();

        // Configure Basic Auth
        List<ClientHttpRequestInterceptor> interceptors = restTemplate.getInterceptors();
        interceptors.add((ClientHttpRequestInterceptor) new AuthenticationRequest("groupe-sae", "notre-mot-de-passe"));
        restTemplate.setInterceptors(interceptors);

        return restTemplate;
    }
}