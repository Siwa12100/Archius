<?php
use Psr\Http\Message\ResponseInterface as Response;
use Psr\Http\Message\ServerRequestInterface as Request;
use Slim\Factory\AppFactory;
require __DIR__.'/models/employee.php';
require __DIR__.'/error/UnknownEmployee.php';

$loader = require __DIR__ . '/../vendor/autoload.php';
//for namespace BL PSR4 here namespace ; dir
$loader->addPsr4('BL\\', __DIR__);
$app = AppFactory::create();
$app->addBodyParsingMiddleware();
$app->addErrorMiddleware(true, true, true);

$app->get('/', function (Request $request, Response $response, $args) {
    $response->getBody()->write("Hello world!");
    return $response;
});

$employees = [
    new Employee(0, 'Felix', 'Manager'),
    new Employee(1,'Jean','Developer'),
    new Employee(2,'Shana','Designer')
];

$app->get('/employees', function (Request $request, Response $response, $args) use ($employees) {
    $response->getBody()->write(json_encode($employees));
    return $response->withHeader('Content-Type', 'application/json');
});

$app->get("/employees/{id}", function(Request $request, Response $response, $args) use ($employees){
    $id = $args['id'];
    $data = $employees[$id];
    $response->getBody()->write(json_encode($data));
    return $response->withHeader('Content-Type', 'application/json');
});

$app->post("/employees", function(Request $request, Response $response, $args) use ($employees) {
    $data = $request->getParsedBody();
    $newEmployee = new Employee(count($employees), $data['name'], $data['poste']);
    $response->getBody()->write(json_encode($newEmployee));
    return $response->withHeader('Content-Type', 'application/json')->withStatus(201);
});

$app->put("/employees/{id}", function(Request $request, Response $response, $args) use ($employees){
    $data = $request->getParsedBody();
    $employees[$args['id']] = $data;
    $response->getBody()->write(json_encode($data));
    return $response->withHeader('Content-Type', 'application/json')->withStatus(200);
});

$app->delete("/employees/{id}", function(Request $request, Response $response, $args) use ($employees){
    try{
        $data = $employees[$args['id']];
        unset($employees[$args['id']]);
        $response->getBody()->write(json_encode($data));
        return $response->withHeader('Content-Type', 'application/json')->withStatus(200);
    } catch(UnknownEmployee $e){
        echo "Caught Unknow Employee Exception :",$e->message;
    }
});

$app->run();


// Utilisez Slim pour développer un service web qui expose les routes suivantes:

// // GET /employees : pour récupérer la liste des employés
// // GET /employees/{id} : pour récupérer un employé spécifique en fonction de son ID
// POST /employees : pour ajouter un nouvel employé
// PUT /employees/{id} : pour mettre à jour un employé existant
// DELETE /employees/{id} : pour supprimer un employé existant