<?php
/**
 * Created by PhpStorm.
 * User: ПК
 * Date: 26.06.2019
 * Time: 6:37
 */

namespace mvc\controller;


class ApiController extends Controller
{
    public function json($models){
        header('Content-type: application/json');
        echo json_encode($models);
    }
}