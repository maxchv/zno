<?php
/**
 * Created by PhpStorm.
 * User: ПК
 * Date: 26.06.2019
 * Time: 23:06
 */

namespace mvc\controller;


class Controller
{
    protected $controllerName;
    protected $models = [];

    public function __construct($controllerName = "Default")
    {
        $this->controllerName = $controllerName;
    }

    /*
     * Возвращает массив с разрешенными action в контроллере. Должен содержать или вложенный массив с ключем 'success'
     * */
    public function authorizedActions(){
        return ['loginPage' => "login"];
    }

    /*
     * Возвращает true, если соблюдено условие для вызова метода незарегистрированных пользователей
     * */
    public function successAuthorize(){
        return true;
    }
}