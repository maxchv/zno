<?php
/**
 * Created by PhpStorm.
 * User: ПК
 * Date: 07.07.2019
 * Time: 11:53
 */

namespace controller;


use config\DbConfig;
use models\api\Api;
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

    public function logout(){
        if(isset($_COOKIE['zno_access_token'])) {
            unset($_COOKIE['zno_access_token']);
            setcookie('zno_access_token', null, -1, DbConfig::$config['_base_url']);
        }

        \Application::redirect("account/login");
    }

    public function login(){
        $this->render("login", [], false);
    }

    public function register(){
        $this->render("register", [], false);
    }
}