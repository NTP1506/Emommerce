import axios from "axios";
class RequestService {
    axios;

    constructor() {
        this.axios = axios.create();
        var token = localStorage.getItem("token");
        if(token)
            this.axios.defaults.headers["Authorization"] = `Bearer ${token}`;
    }
}

export default new RequestService();