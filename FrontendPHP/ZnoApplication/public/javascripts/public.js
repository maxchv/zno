const URL_API = "http://localhost:2021/api/v1";
const BASE_URL = "/zno/FrontendPHP/ZnoApplication";

const LINKS = {
    login: `${URL_API}/account/Login`,
    getCurrentUser: `${URL_API}/account/GetCurrentUser`,
    register:  `${URL_API}/account/Register`,
    getAllTests:  `${URL_API}/test/get-all-tests`,
    getAllSubjects:  `${URL_API}/test/get-all-subjects`,
    getAllLevelOfDifficulty:  `${URL_API}/test/get-all-level-of-difficulty`,
    createTestSettings:  `${URL_API}/test/create-test-settings`,
    updateTestSettings:  `${URL_API}/test/update-test-settings`,
    deleteTestSettings:  `${URL_API}/test/delete-test-settings`,
};

var baseHeaders = {};

function setAccessToken() {
    baseHeaders = {
        'Authorization': "Bearer "+getCookie("zno_access_token"),
    };
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