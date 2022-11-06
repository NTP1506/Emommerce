// import "./login.scss";
// import { Formik } from "formik";
// import * as EmailValidator from "email-validator";
// import * as Yup from "yup";

// const Login = () => {
//   return (
//     <div className="col-8 text-center p-5">
//       <h1>ADMIN SIGN IN</h1>
//       <hr />
//       <form>
//         <table className="d-flex justify-content-center">
//           <tbody>
//             <tr>
//               <td>
//                 <label for="username">Username</label>
//               </td>
//               <td>
//                 <input
//                   required
//                   className="input"
//                   type="text"
//                   name="name"
//                   placeholder="Username..."
//                 />
//               </td>
//             </tr>
//             <tr>
//               <td>
//                 <label for="password">Password</label>
//               </td>
//               <td>
//                 <input
//                   required
//                   type="password"
//                   name="password"
//                   placeholder="Password..."
//                 />
//               </td>
//             </tr>
//             <tr className="d-flex justify-content-end">
//               <td>
//                 <button className="btn btn-outline-primary" type="submit">
//                   Sign in
//                 </button>
//               </td>
//             </tr>
//           </tbody>
//         </table>
//       </form>
//     </div>
//   );
// };

// export default Login;
import React from "react";
import {
  Grid,
  Paper,
  Avatar,
  TextField,
  Button,
  Typography,
  Link,
} from "@material-ui/core";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Checkbox from "@material-ui/core/Checkbox";
import * as EmailValidator from "email-validator";
import * as Yup from "yup";
import { Formik, useFormik } from "formik";
import { useNavigate } from "react-router-dom";
import { NotificationManager } from "react-notifications";
import { loginRequest, setToken } from "../../components/Service/loginservice";
import RequestService from "../../components/Service/request";

const initialFormValues = {
  UserName: "",
  Password: "",
};
const validationSchema = Yup.object().shape({
  UserName: Yup.string().required("*UserName is required"),
  Password: Yup.string().required("*password is required"),
});

const Login = () => {
  let navigate = useNavigate();
  const handleLogin = async (form) => {
    var result = await loginRequest(form.formValues).catch((err) =>
      handleResult(false, "Login Failed")
    );
    var token = result.data.token;
    
    // var isAdmin = result.data.isAdmin;
    // if (token && isAdmin) {
    //     setToken(token);
    //     handleResult(true, 'Login sucessfully')
    // }
    // else {
    //     handleResult(false, 'Login Failed')
    // }
    if (token) {
      //localStorage.setItem("admin", true);
      setToken(token);
      const check =await RequestService.axios.get(
        "https://localhost:7137/Account/checkToken"
        
      );
      alert(check)
      if (check) {
        
        handleResult(true, "Login sucessfully");
      } else {
        handleResult(false, "Login Failed");
      }
    } else {
      handleResult(false, "Login Failed");
    }
  };

  const handleResult = (result, message) => {
    if (result) {
      NotificationManager.success(
        `Login successfully `,
        `Login successfully`,
        2000
      );

      setTimeout(() => {
        navigate("/");
      }, 1000);
    } else {
      NotificationManager.error(message, `Login Failed Failed`, 2000);
    }
  };
  const paperStyle = {
    padding: 20,
    height: "70vh",
    width: 280,
    margin: "20px auto",
  };
  const avatarStyle = { backgroundColor: "#1bbd7e" };
  const btnstyle = { margin: "8px 0" };

  const formik = useFormik({
    initialValues: initialFormValues,
    validationSchema: validationSchema,
    onSubmit: (values) => handleLogin({ formValues: values }),
  });

  return (
    <div className="loginForm">
      <form onSubmit={formik.handleSubmit}>
        <Grid>
          <Paper elevation={10} style={paperStyle}>
            <Grid align="center">
              <Avatar style={avatarStyle}>
                <LockOutlinedIcon />
              </Avatar>
              <h2>Sign In</h2>
            </Grid>
            <TextField
              label="Username"
              name="UserName"
              placeholder="Enter username"
              fullWidth
              required
              value={formik.values.UserName}
              onChange={formik.handleChange}
              error={formik.touched.UserName && Boolean(formik.errors.UserName)}
              helperText={formik.touched.UserName && formik.errors.UserName}
            />
            <TextField
              label="Password"
              name="Password"
              placeholder="Enter password"
              type="password"
              fullWidth
              required
              value={formik.values.Password}
              onChange={formik.handleChange}
              error={formik.touched.Password && Boolean(formik.errors.Password)}
              helperText={formik.touched.Password && formik.errors.Password}
            />
            <FormControlLabel
              control={<Checkbox name="checkedB" color="primary" />}
              label="Remember me"
            />
            <Button
              type="submit"
              color="primary"
              variant="contained"
              style={btnstyle}
              fullWidth
            >
              Sign in
            </Button>
            <Typography>
              <Link href="#">Forgot password ?</Link>
            </Typography>
            <Typography>
              {" "}
              Do you have an account ?<Link href="#">Sign Up</Link>
            </Typography>
          </Paper>
        </Grid>
      </form>
    </div>
  );
};

export default Login;
