<?php

require_once 'Application.php';
require_once 'config/DbConfig.php';
//require_once 'config/DbConfigig.php';
//require_once "config/config.php";
define("PUBLIC_URL", \config\DbConfig::$config['public_url']);
define("BASE_URL", \config\DbConfig::$config['base_url']);

$app = new Application();
$app->run();