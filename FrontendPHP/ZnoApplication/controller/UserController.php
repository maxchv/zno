<?php
/**
 * Created by PhpStorm.
 * User: ПК
 * Date: 09.07.2019
 * Time: 17:04
 */

namespace controller;


use models\api\Api;

class UserController extends SiteController
{
    public function __construct($controllerName = "Default", $moduleName = "")
    {
        parent::__construct($controllerName, $moduleName);
        $this->layoutFile = "layouts\\main.php";
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
        $res = $result != "" && $result->userRoles[0] == "User";
        return $res;
    }

    public function index(){
        $resultSettings = Api::getAllTestSettings($_COOKIE['zno_access_token']);
        $testSettings =  $resultSettings != false && $resultSettings != "" ? json_decode($resultSettings) : [];
        $successTest = is_array($testSettings) && count($testSettings) > 0;
        $testSetting = $successTest ? $testSettings[0] : null;
        $totalQuestions = $successTest == true ? $testSetting->NumberOfQuestions * count($testSetting->Tests) : 0;

        $this->render("index", ["successTest"=>$successTest, "testSetting"=>$testSetting, "totalQuestions"=>$totalQuestions]);
    }

    public function testing(){
        $resultCurrentQuestion = Api::getCurrentQuestion($_COOKIE['zno_access_token']);
        $currentQuestion =  $resultCurrentQuestion != false && $resultCurrentQuestion != "" ? json_decode($resultCurrentQuestion) : null;
        $completed = null;
        if($currentQuestion != null && !is_string($currentQuestion)){
            if(!$currentQuestion->stopTesting){
                $question = $currentQuestion->question;
                $content = json_decode($question->Content);
                if($content != null){
                    $question->Content = $content;
                }
            }
            else{
                $result = Api::completingTestAndGetResult($_COOKIE['zno_access_token'], $currentQuestion->generatedTestId);
                $completed =  $result != false && $result != "" ? json_decode($result) : null;
            }
        }

        if(is_string($currentQuestion)){
            \Application::redirect("user/index");
        }else {

            $this->render("testing", ["currentQuestion" => $currentQuestion, "completed" => $completed]);
        }
    }
}