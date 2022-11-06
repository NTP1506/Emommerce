// import React from "react";
// import {
//   Grid,
//   Paper,
//   Avatar,
//   TextField,
//   Button,
//   Typography,
//   Link,
// } from "@material-ui/core";
// import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
// import FormControlLabel from "@material-ui/core/FormControlLabel";
// import Checkbox from "@material-ui/core/Checkbox";
// import * as EmailValidator from "email-validator";
// import * as Yup from "yup";
// import  {Formik , useFormik}  from "formik";
// import { useNavigate } from "react-router-dom";
// import { NotificationManager } from "react-notifications";
// import { registerRequest } from "../../components/Service/registerservice";
// import RequestService from "../../components/Service/request";


// const initialFormValues = {
//   UserName: "",
//   Password: "",
// };
// const validationSchema = Yup.object().shape({
//   UserName: Yup.string().required("*UserName is required"),
//   Password: Yup.string().required("*password is required"),
// });
// const Register = () => {
//   let navigate = useNavigate();
//   const handleRegister = async (form) => {
//     var result = await registerRequest(form.formValues).catch((err) =>
//       handleResult(false, "Register Failed")
//     );

//     const handleResult = (result, message) => {
//       if (result) {
//         NotificationManager.success(
//           `Register successfully `,
//           `Register successfully`,
//           2000
//         );

//         setTimeout(() => {
//           navigate("/");
//         }, 1000);
//       } else {
//         NotificationManager.error(message, `Login Failed Failed`, 2000);
//       }
//     };
//     const paperStyle = {
//       padding: 20,
//       height: "70vh",
//       width: 280,
//       margin: "20px auto",
//     };
//     const avatarStyle = { backgroundColor: "#1bbd7e" };
//     const btnstyle = { margin: "8px 0" };

//     const formik = useFormik ({
//       initialValues: initialFormValues,
//       validationSchema: validationSchema,
//       onSubmit: (values) => handleRegister({ formValues: values }),
//     });

//     return (
//       <div className="registerForm">
//         <form onSubmit={formik.handleSubmit}>
//           <Grid>
//             <Paper elevation={10} style={paperStyle}>
//               <Grid align="center">
//                 <Avatar style={avatarStyle}>
//                   <LockOutlinedIcon />
//                 </Avatar>
//                 <h2>Register</h2>
//               </Grid>
//               <TextField
//                 label="FullName"
//                 name="FullName"
//                 placeholder="Enter FullName"
//                 fullWidth
//                 required
//                 value={formik.values.FullName}
//                 onChange={formik.handleChange}
//                 error={
//                   formik.touched.FullName && Boolean(formik.errors.FullName)
//                 }
//                 helperText={formik.touched.FullName && formik.errors.FullName}
//               />
//               <TextField
//                 label="Email"
//                 name="Email"
//                 placeholder="Enter Email"
//                 fullWidth
//                 required
//                 value={formik.values.Email}
//                 onChange={formik.handleChange}
//                 error={formik.touched.Email && Boolean(formik.errors.Email)}
//                 helperText={formik.touched.Email && formik.errors.Email}
//               />
//               <TextField
//                 label="Phone"
//                 name="Phone"
//                 placeholder="Enter Phone"
//                 fullWidth
//                 required
//                 value={formik.values.Phone}
//                 onChange={formik.handleChange}
//                 error={formik.touched.Phone && Boolean(formik.errors.Phone)}
//                 helperText={formik.touched.Phone && formik.errors.Phone}
//               />
//               <TextField
//                 label="Password"
//                 name="Password"
//                 placeholder="Enter password"
//                 type="password"
//                 fullWidth
//                 required
//                 value={formik.values.Password}
//                 onChange={formik.handleChange}
//                 error={
//                   formik.touched.Password && Boolean(formik.errors.Password)
//                 }
//                 helperText={formik.touched.Password && formik.errors.Password}
//               />
//               <TextField
//                 label="ConfirmPassword"
//                 name="ConfirmPassword"
//                 placeholder="Enter ConfirmPassword"
//                 type="password"
//                 fullWidth
//                 required
//                 value={formik.values.ConfirmPassword}
//                 onChange={formik.handleChange}
//                 error={
//                   formik.touched.ConfirmPassword &&
//                   Boolean(formik.errors.ConfirmPassword)
//                 }
//                 helperText={
//                   formik.touched.ConfirmPassword &&
//                   formik.errors.ConfirmPassword
//                 }
//               />
//               {/* <FormControlLabel
//                 control={<Checkbox name="checkedB" color="primary" />}
//                 label="Remember me"
//               /> */}
//               <Button
//                 type="submit"
//                 color="primary"
//                 variant="contained"
//                 style={btnstyle}
//                 fullWidth
//               >
//                 Register
//               </Button>
//               {/* <Typography>
//                 <Link href="#">Forgot password ?</Link>
//               </Typography>
//               <Typography>
//                 {" "}
//                 Do you have an account ?<Link href="#">Sign Up</Link>
//               </Typography> */}
//             </Paper>
//           </Grid>
//         </form>
//       </div>
//     );
//   };
// };

// export default Register;
