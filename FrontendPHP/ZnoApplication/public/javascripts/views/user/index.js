$("#btnStart").click(function (event) {
    var subjectId = $("#btnStart").attr("data-subject-id");
    createNewTest(
        subjectId,
        function(json){
            console.log(json);
            if(json.success){
                location.href = `${location.protocol}//${location.host}${BASE_URL}/user/testing`;
            }
        },
        function(err){
            console.log(err);
        }
    );
});