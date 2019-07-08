<?php
/**
 * Created by PhpStorm.
 * User: ПК
 * Date: 23.06.2019
 * Time: 15:17
 */

namespace controller;


use config\DbConfig;
use mvc\controller\BaseController;

class SiteController extends BaseController
{
    public function getUserByToken(){
        $result = "";
        if(isset($_COOKIE['zno_access_token']) && !empty($_COOKIE["zno_access_token"])) {
            $url = DbConfig::$config["api_url"] . "/account/GetCurrentUser";
            $ch = curl_init($url);
            $authorization = "Authorization: Bearer " . $_COOKIE['zno_access_token'];
            curl_setopt($ch, CURLOPT_HTTPHEADER, array($authorization));
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
            $result = curl_exec($ch); // Execute the cURL statement
            curl_close($ch);
        }
        return $result;
    }

    public function notFound($err){

    }

    public function serviceError($ex){

    }
}