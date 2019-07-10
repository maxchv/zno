<?php
/*echo "<pre>";
print_r($testSettings);
echo "</pre>";*/
?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Tests
    </h1>
</section>

<div class="box">
    <div class="box-header">

        <div class="modal fade" id="formTests" tabindex="-1" role="dialog" aria-labelledby="formTestLabel"
             aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="formTestLabel">Add Test </h4><button class="close" type="button" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button></div>

                    <div class="modal-body">
                        <button class="btn btn-block btn-primary btn-xs btn-block-auto" id="btnClear">Clear</button>
                        <?php require_once "widjets/_tableTests.php";?>
                    </div>
                    <div class="modal-footer"><button class="btn btn-secondary" id="btnClose" type="button" data-dismiss="modal">Close</button><button class="btn btn-primary" id="btnSend" type="button">Send</button></div>
                </div>
            </div>
        </div>

    </div><!-- /.box-header -->
    <div class="box-body">
        <table id="tableTestSetiings" class="table table-bordered table-hover">
            <thead>
            <tr>
                <th>Id</th>
                <th>Testing Time</th>
                <th>Number Of Questions</th>
                <th>Subject</th>
                <th>Option</th>
            </tr>
            </thead>
            <tbody>
            <?php if(is_array($testSettings) && count($testSettings) > 0) {
                foreach ($testSettings as $testSetting){?>
                    <tr
                        data-id="<?=$testSetting->Id?>"
                        data-testingTime="<?=$testSetting->TestingTime?>"
                        data-numberOfQuestions="<?=$testSetting->NumberOfQuestions?>"
                        data-subject-name="<?=$testSetting->Subject->Name?>"
                    >
                        <td><?=$testSetting->Id?></td>
                        <td><?=$testSetting->TestingTime?></td>
                        <td><?=$testSetting->NumberOfQuestions?></td>
                        <td><?=$testSetting->Subject->Name?></td>
                        <td><a href="#" class="delete">Delete</a></td>
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
    </div><!-- /.box-body -->
</div><!-- /.box -->

<div class="box">
    <div class="box-header">
        Add Test Settings
    </div><!-- /.box-header -->
    <div class="box-body">
        <form id="addTestSettings">

            <div class="form-group">
                <label for="testingTime">Testing Time</label>
                <input type="number" class="form-control" id="testingTime" placeholder="Enter Testing Time">
            </div>

            <div class="form-group">
                <label for="numberOfQuestions">Number Of Questions</label>
                <input type="number" class="form-control" id="numberOfQuestions" placeholder="Enter Number Of Questions">
            </div>

            <div class="form-group">
                <label for="selectSubject">Subject</label>
                <select class="form-control" id="selectSubject">
                    <?php if(is_array($subjects) && count($subjects) > 0) {
                        foreach ($subjects as $subject){?>
                            <option value="<?=$subject->id?>"><?=$subject->name?></option>
                        <?php } ?>

                    <?php } ?>
                </select>
            </div>

            <div class="form-group">
                <label for="selectTests">Tests</label>
                <!-- <input type="text" class="form-control" id="selectTests" placeholder="Selected Tests" disabled> -->
                <table id="tableSelectedTests" class="table table-bordered table-hover">
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

                    </tbody>
                </table>
                <button type="button" id="btnSelectTest" class="btn btn-block btn-primary btn-block-auto" data-toggle="modal" data-target="#formTests" data-whatever="@mdo">Select Tests</button>
                <button type="button" id="btnClearSelectTest" class="btn btn-block btn-danger btn-block-auto">Clear Tests</button>
            </div>

            <div class="form-group">
                <label for="selectQuestionTypes">Question Types</label>
                <?php require_once "widjets/_tableQuestionTypes.php";?>
            </div>
            <div>
                <button type="button" id="btnAddTestSettings" class="btn btn-block btn-success">Add Test Settings</button>
            </div>
        </form>
    </div><!-- /.box-body -->
</div><!-- /.box -->

<script type="application/javascript" src="<?=\config\DbConfig::$config['public_url']?>javascripts/views/admin/testSettings.js"></script>

<script>
    $(function () {

        $('#tableTests, #tableTestSetiings, #tableSelectedTests').DataTable({
            "paging": true,
            "lengthChange": false,
            "searching": false,
            "ordering": true,
            "info": true,
            "autoWidth": false
        });
    });
</script>