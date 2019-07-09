<table id="tableTests" class="table table-bordered table-hover">
    <thead>
    <tr>
        <th>Id</th>
        <th>Year</th>
        <th>Type</th>
        <th>Subject</th>
        <th>Option</th>
    </tr>
    </thead>
    <tbody>
    <?php if(is_array($tests) && count($tests) > 0) {
        foreach ($tests as $test){?>
            <tr
                data-id="<?=$test->id?>"
                data-year="<?=$test->year?>"
                data-type-id="<?=$test->type->id?>"
                data-type-name="<?=$test->type->name?>"
                data-subject-id="<?=$test->subject->id?>"
                data-subject-name="<?=$test->subject->name?>"
            >
                <td><?=$test->id?></td>
                <td><?=$test->year?></td>
                <td><?=$test->type->name?></td>
                <td><?=$test->subject->name?></td>
                <td><input name="test" value="<?=$test->id?>" type="checkbox"></td>
            </tr>
        <?php } ?>

    <?php } ?>
    </tbody>
    <tfoot>
    <tr>
        <th>Id</th>
        <th>Year</th>
        <th>Type</th>
        <th>Subject</th>
    </tr>
    </tfoot>
</table>