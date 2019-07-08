<?php
/**
 * Created by PhpStorm.
 * User: ПК
 * Date: 07.07.2019
 * Time: 11:53
 */

namespace controller;


use config\DbConfig;
use mvc\controller\BaseController;

class AccountController extends SiteController
{
    public function index(){
        $user = $this->getUserByToken();
        if($user != "") {
            $user = json_decode($user);
            if (isset($user->userRoles)) {
                switch ($user->userRoles[0]) {
                    case "Admin":
                        \Application::redirect("admin/index");
                        break;
                    case "User":
                        \Application::redirect("user/index");
                        break;
                    case "Teacher":
                        \Application::redirect("teacher/index");
                        break;
                }
            }else{
                \Application::redirect("account/login");
            }
        }
        else {
            \Application::redirect("account/login");
        }
    }

    public function login(){
        /*if($_SERVER['REQUEST_METHOD'] == "POST"){
            $json = [
                'login' => $_REQUEST['login'],
                'password'=> $_REQUEST['password'],
                'rememberMe'=> true
            ];

            $links = DbConfig::getLinks();
            $ch = curl_init();
            curl_setopt($ch, CURLOPT_URL, $links['login']);
            curl_setopt($ch, CURLOPT_POST, 1);
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
            curl_setopt($ch, CURLOPT_POSTFIELDS, $json);
            $result = curl_exec($ch);

        }*/
        $cookie = $_COOKIE;
        $this->render("login", [], false);
    }

    public function register(){

    }
}