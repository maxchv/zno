<?php
/**
 * Created by PhpStorm.
 * User: ПК
 * Date: 08.07.2019
 * Time: 6:10
 */

namespace models\api;


use config\DbConfig;

class Api
{
    private static function baseConfig($ch, $token){
        if(isset($token) && !empty($token)) {
            $authorization = "Authorization: Bearer " . $token;
            curl_setopt($ch, CURLOPT_HTTPHEADER, array($authorization));
        }
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    }

    public static function getTests($token){
        $url = DbConfig::getLinks()['getAllTests'];
        $ch = curl_init($url);
        self::baseConfig($ch, $token);
        $result = curl_exec($ch);
        curl_close($ch);
        return $result;
    }

    public static function getAllLevelOfDifficulty($token){
        $url = DbConfig::getLinks()['getAllLevelOfDifficulty'];
        $ch = curl_init($url);
        self::baseConfig($ch, $token);
        $result = curl_exec($ch);
        curl_close($ch);
        return $result;
    }

    public static function getCurrentQuestion($token){
        $url = DbConfig::getLinks()['getCurrentQuestion'];
        $ch = curl_init($url);
        self::baseConfig($ch, $token);
        $result = curl_exec($ch);
        curl_close($ch);
        return $result;
    }

    public static function completingTestAndGetResult($token, $generatedTestId){
        $url = DbConfig::getLinks()['completingTestAndGetResult'] . "?generatedTestId=$generatedTestId";
        $ch = curl_init($url);
        self::baseConfig($ch, $token);
        $result = curl_exec($ch);
        curl_close($ch);
        return $result;
    }

    public static function getAllTestSettings($token){
        $url = DbConfig::getLinks()['getAllTestSettings'];
        $ch = curl_init($url);
        self::baseConfig($ch, $token);
        $result = curl_exec($ch);
        curl_close($ch);
        return $result;
    }

    public static function getSubjects($token){
        $url = DbConfig::getLinks()['getAllSubjects'];
        $ch = curl_init($url);
        self::baseConfig($ch, $token);
        $result = curl_exec($ch);
        curl_close($ch);
        return $result;
    }

    public static function logout($token){
        $url = DbConfig::getLinks()['logout'];
        $ch = curl_init($url);
        self::baseConfig($ch, $token);
        curl_setopt($ch, CURLOPT_POST, 1);

        $result = curl_exec($ch);
        curl_close($ch);
        return $result;
    }

    public static function getUserByToken($token){

        $url = DbConfig::getLinks()['getCurrentUser'];
        $ch = curl_init($url);
        self::baseConfig($ch, $token);
        $result = curl_exec($ch);
        curl_close($ch);
        return $result;
    }
}