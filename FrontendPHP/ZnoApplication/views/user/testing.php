<section class="center">

    <?php if(!$currentQuestion->stopTesting) { ?>
        <div class="box padding30 btn-block-auto left" id="startTest">
        <?php
        $question = $currentQuestion->question;
        $mssLetters = ["A", "B", "C", "D", "E", "F", "G", "H"];
        ?>
    <div class="box-header">
        <strong><?=$currentQuestion->currNumQuestion?> / <?=$currentQuestion->totalQuestion?></strong>
        <div> <?=$question->Content->Title?> </div>
        <input type="hidden" id="questionId" value="<?=$question->Id?>">
        <input type="hidden" id="generatedTestId" value="<?=$currentQuestion->generatedTestId?>">
    </div>
    <div class="box-body">
        <?php switch ($question->QuestionType->Id) {
            //  С одним правильным
            case 1:
                require_once "widjets/one.php";
                break;
            //  С несколькими правильными
            case 2:
                require_once "widjets/many.php";
                break;
            //  С ручным вводом ответа
            case 3:
                require_once "widjets/manual.php";
                break;
            //С ручным вводом ответа (с ручной проверкой преподавателя)
            case 4:
            default:
                require_once "widjets/task.php";
                break;
         } ?>
    </div><!-- /.box-body -->
</div>
    <?php } else { ?>
    <div class="box padding30 btn-block-auto left" style="margin-top:20%" id="startTest">
        You answered correctly: <?=$completed?>
    </div>
    <?php } ?>



</section>