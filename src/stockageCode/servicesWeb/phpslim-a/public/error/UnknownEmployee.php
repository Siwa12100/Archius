<?php

class UnknownEmployee extends \HttpSpecializedException{
    protected $code=504;
    protected $message="Wallah";
    protected $title ="504 wallah";
    protected $description="Je suis pas musulman";
}