import "./login.scss";
import { Formik } from "formik";
import * as EmailValidator from "email-validator";
import * as Yup from "yup";

const validationSchema = Yup.object().shape({
  Username: Yup.string().required('*UserName is required'),
  Password: Yup.string().required('*password is required')
});

const Login = () => {
  return (
    <div className="col-8 text-center p-5">
      <h1>ADMIN SIGN IN</h1>
      <hr />
      <form>
        <table className="d-flex justify-content-center">
          <tbody>
            <tr>
              <td>
                <label for="username">Username</label>
              </td>
              <td>
                <input
                  required
                  className="input"
                  type="text"
                  name="name"
                  placeholder="Username..."
                />
              </td>
            </tr>
            <tr>
              <td>
                <label for="password">Password</label>
              </td>
              <td>
                <input
                  required
                  type="password"
                  name="password"
                  placeholder="Password..."
                />
              </td>
            </tr>
            <tr className="d-flex justify-content-end">
              <td>
                <button className="btn btn-outline-primary" type="submit">
                  Sign in
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </form>
    </div>
  );
};

export default Login;
