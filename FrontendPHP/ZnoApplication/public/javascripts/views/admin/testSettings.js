$("#addTestSettings").submit(function (event) {
    event.preventDefault();

    return false;
})

$("#btnClear").click(function (event) {
    $( "input[name=test]:checked" ).prop('checked', false);
});

$("#btnClearSelectTest").click(function (event) {
    var tbody = $("#tableSelectedTests tbody");
    tbody.empty();
});

$("#btnSend").click(function (event) {
    var tbody = $("#tableSelectedTests tbody");
    var checked = $( "input[name=test]:checked" );
    for(var i = 0; i<checked.length; i++){
        var check = checked[i];
        var tr = check.parentElement.parentElement;
        var testId = tr.getAttribute("data-id");
        if($("#tableSelectedTests tr[data-id="+testId+"]").length == 0){
            var trAppend = $("<tr>");
            trAppend.attr("data-id", testId);
            trAppend.append($("<td>").text(testId));
            trAppend.append($("<td>").text(tr.getAttribute("year")));
            trAppend.append($("<td>").text(tr.getAttribute("data-type-name")));
            trAppend.append($("<td>").text(tr.getAttribute("data-subject-name")));
            tbody.append(trAppend);
        }
    }
});

$("a.delete").click(function (event) {
    var tr = $(this).parent().parent();
    var settingsId = tr.attr("data-id");
    deleteTestSettings(settingsId,
        function (json) {
            location.reload(true);
        },
        function (error) {
            location.reload(true);
        });
});

$("#btnAddTestSettings").click(function (event) {
    const testingTime = $("#testingTime").val();
    const numberOfQuestions = $("#numberOfQuestions").val();
    const subject = {
        id: $("#selectSubject").val(),
    };
    var trs = $("#tableSelectedTests tr[data-id]");
    var tests = [];
    for(var i = 0; i<trs.length; i++){
        tests.push({id: trs[i].getAttribute("data-id")});
    }

    var questionTypesNodes = $( "input[name=questionType]:checked" );
    var questionTypes = [];
    if(questionTypesNodes.length > 0){
        for(var i = 0; i<questionTypesNodes.length; i++){
            questionTypes.push({questionTypeId:questionTypesNodes[i].value});
        }
    }



    var data = JSON.stringify({
        "subject": subject,
        "testingTime": testingTime,
        "numberOfQuestions": numberOfQuestions,
        "tests": tests,
        "questionTypes": questionTypes
    });

    console.log(data);

    setAccessToken();

    $.ajax({
        url: LINKS.createTestSettings,
        method: "POST",
        data: data,
        dataType: "json",
        contentType: "application/json",
        headers: baseHeaders,
        success: function(json){
            console.log(json);
            location.reload(true);
        },
        error: function(err){
            console.log("Error");
            location.reload(true);
        }
    });
});