<?php
/*echo "<pre>";
print_r($subjects);
echo "</pre>";*/
?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Subjects
    </h1>
</section>

<div class="box">
    <div class="box-header">
        <h3 class="box-title">Hover Data Table</h3>
    </div><!-- /.box-header -->
    <div class="box-body">
        <table id="tableSubjects" class="table table-bordered table-hover">
            <thead>
            <tr>
                <th>Id</th>
                <th>Name</th>
            </tr>
            </thead>
            <tbody>
            <?php if(is_array($subjects) && count($subjects) > 0) {
                foreach ($subjects as $subject){?>
                    <tr
                        data-id="<?=$subject->id?>"
                        data-name="<?=$subject->name?>"
                    >
                        <td><?=$subject->id?></td>
                        <td><?=$subject->name?></td>
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