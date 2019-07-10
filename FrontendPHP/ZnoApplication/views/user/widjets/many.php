<div style="width: 49%; display: inline-block;">
    <?php
    $idx = 0;
    foreach ($question->Answers as $answer) { ?>
        <?php
        $content = json_decode($answer->Content);
        if($content->FirstBlock != "") { ?>
            <p><strong><?= ($idx + 1) ?></strong> <?= $content->FirstBlock ?></p>
            <?php
            $idx++;
        }
    } ?>
</div>

<div style="width: 49%; display: inline-block;">
    <?php
    $idx2 = 0;
    foreach ($question->Answers as $answer) { ?>
        <p><strong><?=$mssLetters[$idx2]?></strong> <?=json_decode($answer->Content)->TwiceBlock?></p>
        <?php
        $idx2++;
    } ?>
</div>


<div class="box-body">
    <table class="table" style="width: auto">
        <tr>
            <th></th>
            <?php for($i=0; $i<$idx2; $i++) { ?>
                <th><?=$mssLetters[$i]?></th>
            <?php } ?>
        </tr>
    </table>
</div>

<button class="btn btn-block btn-primary btn-lg btn-block-auto" id="sendOne">Send</button>


