//Файл для хранения объекта с ссылкам
export const links = {
    default: '/',
    home: '/home',
    about : '/about',
    signin: '/signin',
    signup: '/signup',
    logout: '/logout',
};

export const baseUrl = "http://localhost:2021";//"http://104.248.135.234:8080";
export const vApi = "v1";
export const apiUrl = {
    login: `${baseUrl}/api/${vApi}/account/Login`,
    register:  `${baseUrl}/api/${vApi}/account/Register`,
    getAllTests:  `${baseUrl}/api/${vApi}/test/get-all-tests`,
    getAllSubjects:  `${baseUrl}/api/${vApi}/test/get-all-subjects`,
    getAllLevelOfDifficulty:  `${baseUrl}/api/${vApi}/test/get-all-level-of-difficulty`,
    createTestSettings:  `${baseUrl}/api/${vApi}/test/create-test-settings`,
    updateTestSettings:  `${baseUrl}/api/${vApi}/test/update-test-settings`,
    deleteTestSettings:  `${baseUrl}/api/${vApi}/test/delete-test-settings`,
}