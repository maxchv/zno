<table id="tableLevels" class="table table-bordered table-hover">
    <thead>
    <tr>
        <th>Id</th>
        <th>Name</th>
        <th>Option</th>
    </tr>
    </thead>
    <tbody>
    <?php if(is_array($levels) && count($levels) > 0) {
        foreach ($levels as $level){?>
            <tr
                data-id="<?=$level->id?>"
                data-name="<?=$level->name?>"
            >
                <td><?=$level->id?></td>
                <td><?=$level->name?></td>
                <td><input name="questionType" value="<?=$level->id?>" type="checkbox"></td>
            </tr>
        <?php } ?>

    <?php } ?>
    </tbody>
    <tfoot>
    <tr>
        <th>Id</th>
        <th>Name</th>
    </tr>
    </tfoot>
</table>