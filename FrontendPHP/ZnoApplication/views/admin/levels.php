<?php
/*echo "<pre>";
print_r($levels);
echo "</pre>";*/
?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Levels
    </h1>
</section>

<div class="box">
    <div class="box-header">
        <h3 class="box-title">Hover Data Table</h3>
    </div><!-- /.box-header -->
    <div class="box-body">
        <?php require_once "widjets/_tableQuestionTypes.php";?>
    </div><!-- /.box-body -->
</div><!-- /.box -->

<script>
    $(function () {

        $('#tableSubjects').DataTable({
            "paging": true,
            "lengthChange": false,
            "searching": false,
            "ordering": true,
            "info": true,
            "autoWidth": false
        });
    });
</script>