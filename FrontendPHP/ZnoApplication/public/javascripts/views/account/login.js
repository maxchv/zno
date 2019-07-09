

$("#login").submit(function (event) {
   event.preventDefault();

   /*var data = JSON.stringify({
       login: $("#username").val(),
       password: $("#password").val(),
       rememberMe: true
   });

   $.ajax({
       url: LINKS.login,
       method: "POST",
       data: data,
       dataType: "json",
       contentType: "application/json",
       success: function(json){
           console.log(json);
           setCookie('zno_access_token', json.access_token, {expires: json.expires, path: BASE_URL});

           setAccessToken();

           getUserByToken((json) => {
               console.log(json);
               if(json.email && json.userRoles.length > 0){
                   location.href = `${location.protocol}//${location.host}${BASE_URL}/`;
               }
           }, (error) => {
              console.log(error);
           });

       },
       error: function(err){
           console.log("Not valid login or password");
       }
   });*/

   loginUser($("#username").val(), $("#password").val());

   return false;
});