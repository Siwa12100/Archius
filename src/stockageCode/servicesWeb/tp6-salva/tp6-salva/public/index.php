<?php
use Psr\Http\Message\ResponseInterface as Response;
use Psr\Http\Message\ServerRequestInterface as Request;
use Slim\Factory\AppFactory;

$proxies = [
    'http'  => 'proxycl.iut.uca.fr:8080',
    'https' => 'proxycl.iut.uca.fr:8080',
];

$apis = [
    'accounts' => 'http://192.168.127.123:8080',
    'approvals' => 'http://192.168.127.123:8090'
];

$loader = require __DIR__ . '/../vendor/autoload.php';
//for namespace BL PSR4 here namespace ; dir
$loader->addPsr4('LoanApproval\\', __DIR__);

$app = AppFactory::create();
$app->addBodyParsingMiddleware();
$app->addRoutingMiddleware();

$app->post('/loan', function (Request $request, Response $response, $args) {
    global $proxies;
    global $apis;

    $data = (array)$request->getParsedBody();
    if (!isset($data['account_id']) || !isset($data['approval_id']) || !isset($data['somme'])) {
        return $response->withHeader('Content-Type', 'application/json')->withStatus(422); // Unprocessable Entity
    }

    $data['account_id'] = (int)$data['account_id'];
    $data['approval_id'] = (int)$data['approval_id'];
    $data['somme'] = (int)$data['somme'];

    if($data['account_id'] < 1 || $data['approval_id'] < 1 || $data['somme'] < 1) {
        return $response->withHeader('Content-Type', 'application/json')->withStatus(422); // Unprocessable Entity
    }

    $loan = new \LoanApproval\Loan($data['account_id'], $data['approval_id'], $data['somme']);
    $client = new \GuzzleHttp\Client(['proxy' => $proxies]);

    if($loan->getSomme() < 10000) {
        try {
            $serviceResponse = $client->request('GET', "{$apis['accounts']}/accounts/" . $loan->getAccountId());
            $risk = json_decode($serviceResponse->getBody()->getContents());

            if($serviceResponse->getStatusCode() == 404) {
                $serviceResponse = $client->request('POST', "{$apis['accounts']}/accounts", [
                    'json' => [
                        'account_id' => $loan->getAccountId(),
                        'somme' => $loan->getSomme(),
                        'risk' => "LOW"
                    ]
                ]);

                $risk = "LOW";
                
                if($serviceResponse->getStatusCode() != 200) {
                    $response->getBody()->write(json_decode($serviceResponse->getBody(), true)['error']);
                    return $response->withHeader('Content-Type', 'application/json')->withStatus($serviceResponse->getStatusCode());
                }
            }

            if($risk == "HIGH") {
                try {
                    $serviceResponse = $client->request('GET', "{$apis['approvals']}/approvals/" . $loan->getApprovalId());
                    $approval = json_decode($serviceResponse->getBody()->getContents(), true);
            
                    if($serviceResponse->getStatusCode() == 404) {
                        $serviceResponse = $client->request('POST', "{$apis['approvals']}/approvals/", [
                            'json' => [
                                'approval_id' => $loan->getApprovalId(),
                                'reponse' => 'REFUSED'
                            ]
                        ]);
                        
                        if($serviceResponse->getStatusCode() != 200) {
                            $response->getBody()->write(json_decode($serviceResponse->getBody(), true)['error']);
                            return $response->withHeader('Content-Type', 'application/json')->withStatus($serviceResponse->getStatusCode());
                        }
                    }

                    $response->getBody()->write($approval);
                    return $response->withHeader('Content-Type', 'application/json')->withStatus(201);
                } catch (\GuzzleHttp\Exception\ConnectException $ee){
                    $response->getBody()->write(json_encode(["error" => "error in URL"], JSON_PRETTY_PRINT));
                    return $response->withHeader('Content-Type', 'application/json')->withStatus(500);
                } catch (\GuzzleHttp\Exception\ClientException $e) {
                    $response->getBody()->write(json_encode(["error" => Psr7\Message::toString($e->getResponse())], JSON_PRETTY_PRINT));
                    return $response->withHeader('Content-Type', 'application/json')->withStatus(403);
                }
            } else if($risk == "LOW") {
                $response->getBody()->write("APPROVED");
                return $response->withHeader('Content-Type', 'application/json')->withStatus(201);
            }
        } catch (\GuzzleHttp\Exception\ConnectException $ee){
            $response->getBody()->write(json_encode(["error" => "error in URL"], JSON_PRETTY_PRINT));
            return $response->withHeader('Content-Type', 'application/json')->withStatus(500);
        } catch (\GuzzleHttp\Exception\ClientException $e) {
            $response->getBody()->write(json_encode(["error" => Psr7\Message::toString($e->getResponse())], JSON_PRETTY_PRINT));
            return $response->withHeader('Content-Type', 'application/json')->withStatus(403);
        }
    } else {
        try {
            $serviceResponse = $client->request('GET', "{$apis['approvals']}/approvals/" . $loan->getApprovalId());
            $approval = json_decode($serviceResponse->getBody(), true);

            if($serviceResponse->getStatusCode() == 404) {
                $serviceResponse = $client->request('POST', "{$apis['approvals']}/approvals/", [
                    'json' => [
                        'approval_id' => $loan->getApprovalId(),
                        'reponse' => 'REFUSED'
                    ]
                ]);
                
                if($serviceResponse->getStatusCode() != 200) {
                    $response->getBody()->write(json_decode($serviceResponse->getBody(), true)['error']);
                    return $response->withHeader('Content-Type', 'application/json')->withStatus($serviceResponse->getStatusCode());
                }
            }

            $response->getBody()->write($approval);
            return $response->withHeader('Content-Type', 'application/json')->withStatus(201); // 201 created
        } catch (\GuzzleHttp\Exception\ConnectException $ee){
            $response->getBody()->write(json_encode(["error" => "error in URL"], JSON_PRETTY_PRINT));
            return $response->withHeader('Content-Type', 'application/json')->withStatus(500);
        } catch (\GuzzleHttp\Exception\ClientException $e) {
            $response->getBody()->write(json_encode(["error" => Psr7\Message::toString($e->getResponse())], JSON_PRETTY_PRINT));
            return $response->withHeader('Content-Type', 'application/json')->withStatus(403);
        }
    }

});

$app->run();