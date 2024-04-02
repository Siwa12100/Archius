<?php

class Book implements \JsonSerializable{

    private long $isbn;
    private String $titre;
    private int $stock;


    public function __construct(long $isbn, String $titre, int $stock){
        $this->isbn = $isbn;
        $this->titre = $titre;
        $this->stock = $stock;
    }


    public function jsonSerialize(){
        return[ 'isbn' => $this->isbn,
                'titre' => $this->titre];
    }

    public function getIsbn(){ return $this->isbn; }
    public function setIsbn($isbn){ $this->isbn = $isbn; }

    public function getTitre(){ return $this->titre; }
    public function setTitre($titre){ $this->titre = $titre; }
    
    public function getStock(){ return $this->stock; }
    public function setStock(){ $this->stock=$this->stock -1; }


}



?>