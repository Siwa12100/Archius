<?php

namespace modele;

// require __DIR__ . '/vendor/autoload.php';

use modele\IArticleDataManager;
use metier\Article;

use modele\Contenu;
use modele\contenuParagraphe;
use modele\contenuMedia;

use GuzzleHttp\Client;
use GuzzleHttp\Exception\GuzzleException;

class articlesService implements IArticleDataManager {
    private $client;
    private $apiBaseUrl;

    public function __construct($apiBaseUrl) {
        $this->apiBaseUrl = $apiBaseUrl;
        $this->client = new Client([
            // Base URI is used with relative requests
            'base_uri' => $this->apiBaseUrl,
            // You can set any number of default request options.
            'timeout'  => 2.0,
        ]);
    }

    public function getAllArticles() : array {
        try {

            // echo "debut de getAllArticles ! <br>";

            $response = $this->client->request('GET', '/articles');

            $body = $response->getBody();

            // echo $body;

            $articlesDansTableau = json_decode($body, true);

            // var_dump($articlesDansTableau);

            foreach ($articlesDansTableau as $articleColomne) {
                // Créer un nouvel objet Article en utilisant les données de l'array

                // echo "<br>";
                // echo "<br>";
                // echo "recuperation nouvel article dans le foreach ";
                // echo "<br>";
                // echo "<br>";

                $nouvelArticle = new Article(
                    $articleColomne['id'],
                    $articleColomne['titre'],
                    $articleColomne['description'],
                    $articleColomne['temps'],
                    $articleColomne['date'],
                    $articleColomne['auteur'],
                    $articleColomne['imagePrincipale']

                );

                $nouvelArticle -> setCategorie($articleColomne['categorie']);


                // echo "<br> debut deserialisation dans le foreach !";

                // var_dump($articleColomne['contenus']);

                $contenusFinaux = [];

                foreach ($articleColomne['contenus'] as $donnee) {

                    $typeContenu = $donnee['typeContenu'];
                
                    if ($typeContenu === 'image') {

                        $contenu = new contenuMedia(
                            $donnee['id'],
                            $donnee['titre'],
                            $donnee['lien']
                        );
                    } elseif ($typeContenu === 'paragraphe') {

                        $contenu = new contenuParagraphe(
                            $donnee['id'],
                            $donnee['titre'],
                            $donnee['texte']
                        );
                    } elseif ($typeContenu === 'video') {

                        $contenu = contenuMedia::newVideo($donnee['id'],
                        $donnee['titre'],
                        $donnee['lien']);
                    }
                

                    $contenusFinaux[] = $contenu;
                }

                // echo "<br>";
                // echo "contenu deserialisé dans le foreach ! ";
                // echo "<br>";
                // echo "<br>";

                $nouvelArticle -> remplirArticle($contenusFinaux);

                $articlesFinaux[] = $nouvelArticle;
            }

            return $articlesFinaux;

        } catch (GuzzleException $e) {

            echo "Erreur lors de la récupération des articles : " . $e->getMessage();
            return [];
        }
    }

    public function getArticle($id) : Article {
        try {
            $response = $this->client->request('GET', "/articles/{$id}");
            $body = $response->getBody();
            $article = json_decode($body, true);
            return $article;
        } catch (GuzzleException $e) {
            echo "Erreur lors de la récupération de l'article : " . $e->getMessage();
            return null;
        }
    }

    public function getDerniersArticles($nbArticles) {
        try {
            
            return $this->getAllArticles();

        } catch (\Exception $e) {
            echo "Erreur lors de la récupération des derniers articles : " . $e->getMessage();
            return [];
        }
    }
    
}
