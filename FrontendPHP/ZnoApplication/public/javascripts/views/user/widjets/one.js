$("#sendOne").click(function (event) {
    var id = $("[name=answer]:checked").val();
    if(id != undefined && id != "") {
        var answers = [{
            id: id
        }];

        savingAnswer($("#questionId").val(), $("#generatedTestId").val(), answers,
            function (json) {
                window.location = window.location;
            },

            function (error) {
                //alert(error.responseText);
                window.location = window.location;
            }
            )
    }else{
        alert("Select answer");
    }
});