<?php
use Psr\Http\Message\ResponseInterface as Response;
use Psr\Http\Message\ServerRequestInterface as Request;
use Slim\Factory\AppFactory;
use GuzzleHttp\Client;
require __DIR__ . '/../modele/Book.php';

$loader = require __DIR__ . '/../vendor/autoload.php';
//for namespace BL PSR4 here namespace ; dir
$loader->addPsr4('BL\\', __DIR__);
$app = AppFactory::create();


$app->addBodyParsingMiddleware();
$app->addRoutingMiddleware();
$app->addErrorMiddleware(true, true, true);

$app->get('/search/{id}', function(Request $request, Response $response, $args){

    // La c'est le pseudo client qui va contacter l'autre service
    try{
        $client = new GuzzleHttp\Client(['base_uri' => 'http://localhost:8080/getstock', 'proxy' => '']);
        $res= $client->request('GET', '');
        $code= $response->getStatusCode();
        echo $res->getBody();
        $body= json_decode($res->getBody()->getContents());
        //c'est ca qui retourne un stream

        

        echo "\n";
    } catch (ConnectException $ee){ echo "error in URL";}
    catch (ClientException $e) {
        echo Psr7\Message::toString($e->getRequest());
        echo Psr7\Message::toString($e->getResponse());
    }
    // jusque la

    //ce qu'on write doit etre un string, mais le $body la c'est un stream
    $response->getBody()->writeStream($body);

    return $response;

});

$app->put('/buy/{id}', function(Request $request, Response $response, $args){
    $id=$args['id'];

    foreach($listBook as $b){
        if($b->getIsbn() == args['id']){
            $data = $request->getParsedBody();

            $listBook[$id]->setStock();

            $jsonBook= json_encode($listBook, JSON_PRETTY_PRINT);
            $response->getBody()->write($jsonBook);

            return $response;
        }
    }
    
    $response->getBody()->write("Livre {$id} non trouvé");
    $response=$response->withStatus(404);

    throw new Exception("Livre {$id} non trouvé");

});



$app->get('/Books', function (Request $request, Response $response, $args){
    $jsonBook= json_encode($listBook, JSON_PRETTY_PRINT);
    $response->getBody()->write($jsonBook);
    return $response;
});

$app->get('/Books/{id}', function(Request $request, Response $response, $args){
    $id=$args['id'];
    $Book=$listBook[$id];

    $jsonBook= json_encode($Book, JSON_PRETTY_PRINT);
    $response->getBody()->write($jsonBook);

    return $response;
});

$app->post('/Books', function(Request $request, Response $response, $args): 
Response{
    $data=$request->getParsedBody();

    $Book=new Book($data['id'], $data['nom'], $data['prenom']);
    $listBook[$data['id']] = $Book;

    $jsonBook= json_encode($listBook, JSON_PRETTY_PRINT);
    $response->getBody()->write($jsonBook);

    return $response;
});

$app->put('/Books/{id}', function(Request $request, Response $response, $args){
    $id=$args['id'];

    if(isset($listBook[$id])){
        $data = $request->getParsedBody();

        $listBook[$id]->setNom($data['nom']);

        $jsonBook= json_encode($listBook, JSON_PRETTY_PRINT);
        $response->getBody()->write($jsonBook);
    }
    else{
        $response->getBody()->write("Emloyé {$id} non trouvé");
        $response=$response->withStatus(404);
    }

    return $response;
});

$app->delete('/Books/{id}', function(Request $request, Response $response, $args){
    $id=$args['id'];

    if(isset($listBook[$id])){
        unset($listBook[$id]);

        $response->getBody()->write("Emloyé {$id} supprimé");
        $jsonBook= json_encode($listBook, JSON_PRETTY_PRINT);
        $response->getBody()->write($jsonBook);
    }
    else{
        $response->getBody()->write("Emloyé {$id} non trouvé");
        $response=$response->withStatus(404);
    }

    return $response;
});


$app->run();



