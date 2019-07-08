<?php
/**
 * Created by PhpStorm.
 * User: ПК
 * Date: 07.07.2019
 * Time: 15:50
 */

namespace controller;


use config\DbConfig;
use mvc\controller\BaseController;

class AdminController extends SiteController
{
    public function __construct($controllerName = "Default", $moduleName = "")
    {
        parent::__construct($controllerName, $moduleName);
        $this->layoutFile = "layouts\\admin_main.php";
    }

    public function authorizedActions()
    {
        $base = parent::authorizedActions();
        return array_merge($base, ['success'=>['tmp'], "redirect"=>"account/login"]);
    }

    public function successAuthorize()
    {
        $result = $this->getUserByToken();

        if($result != ""){
            $result = json_decode($result);

        }
        $res = $result != "" && $result->userRoles[0] == "Admin";
        return $res;
    }

    public function index(){
        $this->render("index", []);
    }
}