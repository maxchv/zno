<?php

namespace config;

class DbConfig{
    private static $baseUrl = "http://localhost:2021";
    private static $vApi = "v1";
    private static $links = null;
    public static $config = [
        /*'dsn' => 'mysql:host=localhost;port=3306;dbname=;charset=utf8;',
        'username' => 'root',
        'password' => '',
        'charset' => 'utf8',
        "table_prefix" => "",*/
        'file' => [
            'upload_dir' => "/zno/FrontendPHP/ZnoApplication/public/images/"
        ],
        'public_url' => "/zno/FrontendPHP/ZnoApplication/public/",
        'admin_public_url' => "/zno/FrontendPHP/ZnoApplication/public/admin_lte/",
        "base_url" => "/zno/FrontendPHP/ZnoApplication/",
        "api_url" => "http://localhost:2021/api/v1",

    ];

    public static function getLinks(){

        if(!isset(static::$links) || empty(static::$links)){
            static::$links = [
                'login' => static::$baseUrl."/api/".static::$vApi."/account/Login",
                'register'=>  static::$baseUrl."/api/".static::$vApi."/account/Register",
                'getAllTests'=>  static::$baseUrl."/api/".static::$vApi."/test/get-all-tests",
                'getAllSubjects'=>  static::$baseUrl."/api/".static::$vApi."/test/get-all-subjects",
                'getAllLevelOfDifficulty'=>  static::$baseUrl."/api/".static::$vApi."/test/get-all-level-of-difficulty",
                'createTestSettings'=>  static::$baseUrl."/api/".static::$vApi."/test/create-test-settings",
                'updateTestSettings'=>  static::$baseUrl."/api/".static::$vApi."/test/update-test-settings",
                'deleteTestSettings'=>  static::$baseUrl."/api/".static::$vApi."/test/delete-test-settings",
            ];
        }
        return static::$links;
    }


}