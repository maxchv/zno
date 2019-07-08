<?php

namespace mvc\controller;

class BaseController extends Controller
{
    private $directoryViews = 'views';
    protected $layoutFile = "layouts\\main.php";

    public function render($action, $models = null, $isLayout = true){
        $view = "";
        $content = "";
        if($isLayout) {
            $view = "$this->directoryViews\\$this->layoutFile";
            $content = "$this->directoryViews\\" . strtolower($this->controllerName) . "\\$action.php";
        }
        else{
            $view = "$this->directoryViews\\" . strtolower($this->controllerName) . "\\$action.php";
        }

        if(!empty($models)){
            $this->models = array_merge($models, $this->models);
        }

        if(!empty($this->models)){
            extract($this->models);
        }

        if(file_exists($view)) {
            require $view;
        }
    }

}