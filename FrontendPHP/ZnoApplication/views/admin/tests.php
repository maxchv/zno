<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Tests
    </h1>
</section>

<div class="box">
    <div class="box-header">
        <h3 class="box-title">Hover Data Table</h3>
    </div><!-- /.box-header -->
    <div class="box-body">
        <?php require_once "widjets/_tableTests.php";?>
    </div><!-- /.box-body -->
</div><!-- /.box -->

<script>
    $(function () {

        $('#tableTests').DataTable({
            "paging": true,
            "lengthChange": false,
            "searching": false,
            "ordering": true,
            "info": true,
            "autoWidth": false
        });
    });
</script>