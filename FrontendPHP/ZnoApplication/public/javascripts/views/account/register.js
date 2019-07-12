

$("#register").submit(function (event) {
    event.preventDefault();

    var object = {
        phone: $("#phone").val(),
        password: $("#password").val(),
        confirmPassword: $("#confirmPassword").val(),
        email: $("#email").val(),
    };
    var data = JSON.stringify(object);

    $(".errors").empty();
    console.log(data);

    $.ajax({
        url: LINKS.register,
        method: "POST",
        data: data,
        dataType: "json",
        contentType: "application/json",
        success: function(json){
            console.log(json);
            if(json){
                loginUser(object.email, object.password);
            }
        },
        error: function(err){
            console.log(err);
            $(".errors").append($("<p>").text(err.responseText));
        }
    });

    return false;
});