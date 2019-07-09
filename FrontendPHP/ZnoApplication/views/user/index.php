<div class="box" id="startTest">
    <div class="box-body center">
        <?php if($successTest){ ?>
            <h2>Your Subject: <strong> <?=$testSetting->Subject->Name?></strong> </h2>
            <p>Total time: <strong> <?=$testSetting->TestingTime?></strong> </p>
            <!-- <p>Total questions: <strong> <?=$totalQuestions?></strong> </p> -->
            <button class="btn btn-block btn-primary btn-lg btn-block-auto" id="btnStart"  data-subject-id="<?=$testSetting->Subject->Id?>" >Start Test</button>
        <?php } else{ ?>
            <button class="btn btn-block btn-primary btn-lg btn-block-auto" disabled>You are currently unable to take the test.</button>
        <?php } ?>
    </div><!-- /.box-body -->
</div>

<script type="application/javascript" src="<?=\config\DbConfig::$config['public_url']?>javascripts/views/user/index.js"></script>