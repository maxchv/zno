<?php
/**
 * Created by PhpStorm.
 * User: ПК
 * Date: 07.07.2019
 * Time: 15:50
 */

namespace controller;


use config\DbConfig;
use models\api\Api;
use mvc\controller\BaseController;

class AdminController extends SiteController
{
    public function __construct($controllerName = "Default", $moduleName = "")
    {
        parent::__construct($controllerName, $moduleName);
        $this->layoutFile = "layouts\\admin_main.php";
        $this->models["user"] = json_decode(Api::getUserByToken($_COOKIE['zno_access_token']));
    }

    public function authorizedActions()
    {
        $base = parent::authorizedActions();
        return array_merge($base, ['success'=>['tmp'], "redirect"=>"account/login"]);
    }

    public function successAuthorize()
    {
        $result = $this->getUserByToken();

        if($result != ""){
            $result = json_decode($result);

        }
        $res = $result != "" && $result->userRoles[0] == "Admin";
        return $res;
    }

    public function tests(){
        $result = Api::getTests($_COOKIE['zno_access_token']);
        $tests = $result != "" ? json_decode($result) : [];

        $this->render("tests", ["tests"=>$tests]);
    }

    public function subjects(){
        $result = Api::getSubjects($_COOKIE['zno_access_token']);
        $subjects = $result != "" ? json_decode($result) : [];

        $this->render("subjects", ["subjects"=>$subjects]);
    }

    public function levels(){
        $result = Api::getAllLevelOfDifficulty($_COOKIE['zno_access_token']);
        $levels = $result != "" ? json_decode($result) : [];

        $this->render("levels", ["levels"=>$levels]);
    }

    public function testsettings(){
        $result = Api::getAllTestSettings($_COOKIE['zno_access_token']);
        $testSettings = $result != "" ? json_decode($result) : [];

        $resultTests = Api::getTests($_COOKIE['zno_access_token']);
        $tests = $resultTests != "" ? json_decode($resultTests) : [];

        $resultSubjects = Api::getSubjects($_COOKIE['zno_access_token']);
        $subjects = $resultSubjects != "" ? json_decode($resultSubjects) : [];

        $resultLevels = Api::getAllLevelOfDifficulty($_COOKIE['zno_access_token']);
        $levels = $resultLevels != "" ? json_decode($resultLevels) : [];

        $this->render("testSettings",
            [
                "testSettings"=>$testSettings,
                'tests' => $tests,
                'subjects' => $subjects,
                'levels' => $levels,
            ]);
    }

    public function logout(){
        \Application::redirect("account/logout");
    }

    public function index(){
        \Application::redirect("admin/testSettings");
        //$this->render("index", []);
    }
}