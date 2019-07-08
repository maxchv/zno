<?php
/**
 * Created by PhpStorm.
 * User: bykov
 * Date: 20.06.2018
 * Time: 17:18
 */

namespace controller;

use mvc\controller\BaseController;

class DefaultController extends BaseController
{
    public function index($id = null) {
        //$this->render("index", null);
        header('Location: '. BASE_URL . 'account/');
    }
}