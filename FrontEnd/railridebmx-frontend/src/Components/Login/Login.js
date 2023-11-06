import React from "react";
import "./Login.css";
import FormValidation from "../FormValidation/FormValidation";
import { Formik, Form } from "formik";
import * as Yup from "yup";
import { useNavigate } from "react-router-dom";
import NavBar from "../NavBar/NavBar";

const SigninSchema = Yup.object().shape({
  email: Yup.string().email("Invalid email").required("required"),
  password: Yup.string().required("Password required"),
});

const Login = () => {
  const navigate = useNavigate();
  const onLogin = async (data) => {
    try {
      const res = await fetch("https://localhost:7134/api/Auth/Login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });
      if (res.ok) {
        const responseData = await res.json();
        const token = responseData.data.token;
        localStorage.setItem("jwtToken", token);
        navigate("/test");
      }
    } catch (err) {
      console.log(err);
    }
  };
  return (
    <>
      <NavBar />
      <div className="login-field">
        <div className="title-login">
          <h1>Signin</h1>
        </div>
        <Formik
          initialValues={{
            email: "",
            password: "",
          }}
          validationSchema={SigninSchema}
          onSubmit={(values) => {
            onLogin(values);
          }}
        >
          <div className="login-content">
            <Form>
              <FormValidation
                type="email"
                name="email"
                title="Email"
                className="login-field"
              ></FormValidation>

              <FormValidation
                type="password"
                name="password"
                title="Password"
                className="login-field"
              ></FormValidation>
              <button type="submit" className="login-button">
                Login
              </button>
            </Form>
          </div>
        </Formik>
      </div>
    </>
  );
};
export default Login;
