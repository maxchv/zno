import cookie, { load } from "react-cookies";
import { apiUrl } from "./links";

const tokenKeys = {
  token: "token",
  tokenType: "token-type"
};

const requestSettings = {
  method: 'POST',
  headers: {
    "Content-Type": "application/json",
  }
};

const defaultError = "An error has occurred, try again.";

export const roles = {
  user: 'user',
  admin: 'admin',
  teacher: 'teacher',
}
function getRoles(userToken) {
  // TODO: Реализовать запрос на сервер для получения ролей, по переданному токену
  return ['user'];
}

function getCurrentUserToken() {
  // TODO: Реализовать получение токена из куков
  const tokenValue = cookie.load(tokenKeys.token);
  return tokenValue;
}

function hasRole(userToken, role) {
  if (!isAuthenticated())
    userToken = getCurrentUserToken();
  return getRoles(userToken).some(userRole => userRole.toLowerCase() === role.toLowerCase());
}

function isUser(userToken) {
  return hasRole(userToken, roles.user);
}
function isTeacher(userToken) {
  return hasRole(userToken, roles.teacher);
}
function isAdmin(userToken) {
  return hasRole(userToken, roles.admin);
}

function saveToken(json) {
  const expires = new Date(json.expires);
  cookie.save(tokenKeys.token, json.access_token, { expires: expires });
  cookie.save(tokenKeys.tokenType, json.access_token_type, { expires: expires });
}

async function signIn(user) {

  requestSettings.body = JSON.stringify({
    login: user.login,
    password: user.password,
    rememberMe: !!user.rememberMe
  });


  const response = await fetch(apiUrl.login, requestSettings);
  console.log({ response });
  if (response.status === 200) {
    const json = await response.json();
    saveToken(json);
  }
  else {
    let error = await response.json();
    if (!!error || error.toString().trim() === '')
      error = defaultError;
    throw error;
  }
}

async function getAllTests() {

  const response = await fetch(apiUrl.getAllTests);
  console.log({ response });
  if (response.status === 200) {
    var json = await response.json();
    return json;
  }
  else {
    let error = await response.json();
    if (!!error || error.toString().trim() === '')
      error = defaultError;
    throw error;
  }
}

async function getAllSubjects() {

  const response = await fetch(apiUrl.getAllSubjects);
  console.log({ response });
  if (response.status === 200) {
    var json = await response.json();
    return json;
  }
  else {
    let error = await response.json();
    if (!!error || error.toString().trim() === '')
      error = defaultError;
    throw error;
  }
}

async function getAllLevelOfDifficulty() {

  const response = await fetch(apiUrl.getAllLevelOfDifficulty);
  console.log({ response });
  if (response.status === 200) {
    var json = await response.json();
    return json;
  }
  else {
    let error = await response.json();
    if (!!error || error.toString().trim() === '')
      error = defaultError;
    throw error;
  }
}

async function createTestSettings(testSettings) {

  requestSettings.body = JSON.stringify({
    
  });


  const response = await fetch(apiUrl.login, requestSettings);
  console.log({ response });
  if (response.status === 200) {
    const json = await response.json();
    saveToken(json);
  }
  else {
    let error = await response.json();
    if (!!error || error.toString().trim() === '')
      error = defaultError;
    throw error;
  }
}

async function register(newUser) {
  requestSettings.body = JSON.stringify({
    phone: newUser.phone,
    password: newUser.password,
    confirmPassword: newUser.confirmPassword,
    email: newUser.email,
  });

  const response = await fetch(apiUrl.register, requestSettings);
  if (response.status === 200) {
    await signIn({ login: newUser.phone, password: newUser.password });
  }
  else {
    let error = await response.json();
    if (!!error || error.toString().trim() === '')
      error = defaultError;
    throw error;
    // signIn(this.state.signupUser.phone, this.state.signupUser.password);
    // const url = apiUrl.login;
    // requestSettings.body = JSON.stringify({
    //   login: this.state.signupUser.phone,
    //   password: this.state.signupUser.password
    // });
    // fetch(url, requestSettings)
    //   .then(resp => resp.json())
    //   .then(json => saveToken(json));
  }

}

export const logout = () => {
  
  cookie.remove(tokenKeys.token);
  cookie.remove(tokenKeys.tokenType);
};

export const isAuthenticated = () => !!getCurrentUserToken();

export { getCurrentUserToken, isUser, isTeacher, isAdmin, saveToken, signIn, register };