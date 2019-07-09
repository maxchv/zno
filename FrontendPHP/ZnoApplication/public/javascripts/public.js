const URL_API = "http://localhost:2021/api/v1";
const BASE_URL = "/zno/FrontendPHP/ZnoApplication";

const LINKS = {
    login: `${URL_API}/account/Login`,
    getCurrentUser: `${URL_API}/account/GetCurrentUser`,
    register:  `${URL_API}/account/Register`,
    getAllTests:  `${URL_API}/test/GetAllTests`,
    getAllSubjects:  `${URL_API}/test/GetAllSubjects`,
    getAllLevelOfDifficulty:  `${URL_API}/test/GetAllLevelOfDifficulty`,
    createTestSettings:  `${URL_API}/test/CreateTestSettings`,
    updateTestSettings:  `${URL_API}/test/UpdateTestSettings`,
    deleteTestSettings:  `${URL_API}/test/DeleteTestSettings`,
    createNewTest:  `${URL_API}/testing/CreateNewTestV2`,
    savingAnswer:  `${URL_API}/testing/SavingAnswer`,

};

var baseHeaders = {};

function setAccessToken() {
    baseHeaders = {
        'Authorization': "Bearer "+getCookie("zno_access_token"),
    };
}

function deleteTestSettings(settingsId, success, error) {
    setAccessToken();
    $.ajax({
        url: LINKS.deleteTestSettings + "?settingsId="+settingsId,
        headers: baseHeaders,
        method: "DELETE",
        dataType: "json",
        contentType: "application/json",
        success: success,
        error: error,
    });
}

function createNewTest(subjectId, success, error) {
    setAccessToken();
    $.ajax({
        url: LINKS.createNewTest + "?subjectId="+subjectId,
        headers: baseHeaders,
        method: "POST",
        dataType: "json",
        contentType: "application/json",
        success: success,
        error: error,
    });
}

function savingAnswer(questionId, generatedTestId, answers, success, error) {
    setAccessToken();

    var data = JSON.stringify(answers);

    $.ajax({
        url: LINKS.savingAnswer + `?questionId=${questionId}&generatedTestId=${generatedTestId}`,
        headers: baseHeaders,
        method: "POST",
        dataType: "json",
        data: data,
        contentType: "application/json",
        success: success,
        error: error,
    });
}

function loginUser(username, password){
    var data = JSON.stringify({
        login: username,
        password: password,
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
                    /*var date = new Date(new Date().getTime() + 60 * 1000);
                    setCookie("zno_user_name", json.userName, {expires: date.toUTCString(), path: BASE_URL});
                    setCookie("zno_user_email", json.email, {expires: date.toUTCString(), path: BASE_URL});
                    setCookie("zno_user_role", json.userRoles[0], {expires: date.toUTCString(), path: BASE_URL});*/
                    location.href = `${location.protocol}//${location.host}${BASE_URL}/`;
                }
            }, (error) => {
                console.log(error);
            });

        },
        error: function(err){
            console.log("Not valid login or password");
        }
    });
}

function getUserByToken(success, error) {
    $.ajax({
        url: LINKS.getCurrentUser,
        method: "GET",
        headers: baseHeaders,
        success: success,
        error: error
    });
}

// возвращает cookie с именем name, если есть, если нет, то undefined
function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

function setCookie(name, value, options) {
    options = options || {};

    var expires = options.expires;

    if (typeof expires == "number" && expires) {
        var d = new Date();
        d.setTime(d.getTime() + expires * 1000);
        expires = options.expires = d;
    }
    if (expires && expires.toUTCString) {
        options.expires = expires.toUTCString();
    }

    value = encodeURIComponent(value);

    var updatedCookie = name + "=" + value;

    for (var propName in options) {
        updatedCookie += "; " + propName;
        var propValue = options[propName];
        if (propValue !== true) {
            updatedCookie += "=" + propValue;
        }
    }

    document.cookie = updatedCookie;
}