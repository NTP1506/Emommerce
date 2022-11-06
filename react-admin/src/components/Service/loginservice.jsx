import requestService from './request'

export async function loginRequest(loginForm) {
    //const formData = new FormData();
    return requestService.axios.post("https://localhost:7137/Account/Login", loginForm);
}

export async function setToken(token) {
    //localStorage.setItem("admin", true);
    localStorage.setItem("token", token);
    var axiosRequest = requestService.axios;
    axiosRequest.defaults.headers["Authorization"] = `Bearer ${token}`;
}

export async function checkLogin() {
    if(localStorage.getItem("admin")==null) {
        return false
    }
    return true;
}
