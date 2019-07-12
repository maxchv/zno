<?php
$idx = 0;
foreach ($question->Answers as $answer) { ?>
    <p><strong><?=$mssLetters[$idx]?></strong> <?=$answer->Content?></p>
<?php
$idx++;
} ?>

<div class="box-body">
    <table class="table" style="width: auto">
        <tr>
            <th></th>
            <?php for($i=0; $i<$idx; $i++) { ?>
                <th><?=$mssLetters[$i]?></th>
            <?php } ?>
        </tr>
        <tr>
            <td><strong>1</strong></td>
            <?php foreach($question->Answers as $answer) { ?>
                <td><input type="radio" name="answer" value="<?=$answer->Id?>"></td>
            <?php } ?>
        </tr>
    </table>
</div>

<button class="btn btn-block btn-primary btn-lg btn-block-auto" id="sendOne">Send</button>

<script type="application/javascript" src="<?=\config\DbConfig::$config['public_url']?>javascripts/views/user/widjets/one.js"></script>
