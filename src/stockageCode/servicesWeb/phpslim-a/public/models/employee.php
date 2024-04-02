<?php

class Employee implements \JsonSerializable{
    private $id;
    private $nom;
    private $poste;

    function __construct($id, $nom, $poste){
        $this->id=$id;
        $this->nom=$nom;
        $this->poste=$poste;
    }
    
    function jsonSerialize(): mixed{
        return ['id'=>$this->id, 'name'=>$this->nom, 'poste'=>$this->poste];
    }

    function getId():int{
        return $this.id;
    }
}