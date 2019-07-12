<?php
/**
 * Created by PhpStorm.
 * User: ПК
 * Date: 23.06.2019
 * Time: 15:17
 */

namespace controller;


use config\DbConfig;
use models\api\Api;
use mvc\controller\BaseController;

class SiteController extends BaseController
{
    public function getUserByToken(){
        $result = "";
        if(isset($_COOKIE['zno_access_token']) && !empty($_COOKIE["zno_access_token"])) {
            $result = Api::getUserByToken($_COOKIE["zno_access_token"]);
        }
        return $result;
    }

    public function notFound($err){

    }

    public function serviceError($ex){

    }
}