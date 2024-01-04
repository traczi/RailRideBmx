import React from "react";
import "./Register.css";
import { Formik, Form } from "formik";
import * as Yup from "yup";
import FormValidation from "../FormValidation/FormValidation";
import NavBar from "../NavBar/NavBar";

const onRegister = async (data) => {
  try {
    console.log(data);
    const res = await fetch("https://localhost:7139/api/Auth/Register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
    console.log(res);
  } catch (err) {
    console.log(err);
  }
};

const SignupSchema = Yup.object().shape({
  firstname: Yup.string()
    .min(2, "Too short!")
    .max(50, "Too long!")
    .required("Required"),
  lastname: Yup.string()
    .min(2, "Too short!")
    .max(50, "Too long!")
    .required("Required"),
  email: Yup.string().email("Invalid email").required("Required"),
  password: Yup.string()
    .required("Password required")
    .matches(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,})/,
      "Must Contain 8 Characters, One Uppercase, One Lowercase, One Number and One Special Case Character"
    ),
  retypePassword: Yup.string()
    .required("Required")
    .oneOf([Yup.ref("password")], "Passwords does not match"),
});

const Register = () => {
  return (
    <div className="registerCss">
      <NavBar />
      <div className="register-field">
        <div className="title-register">
          <h1>Signup</h1>
        </div>
        <Formik
          initialValues={{
            firstname: "",
            lastname: "",
            email: "",
            password: "",
            retypePassword: "",
          }}
          validationSchema={SignupSchema}
          onSubmit={(values) => {
            onRegister(values);
          }}
        >
          <div className="register-content">
            <Form>
              <FormValidation
                type="text"
                name="firstname"
                title="Firstname"
                className="register-field"
              ></FormValidation>

              <FormValidation
                type="text"
                name="lastname"
                title="Lastname"
                className="register-field"
              ></FormValidation>

              <FormValidation
                type="email"
                name="email"
                title="Email"
                className="register-field"
              ></FormValidation>

              <FormValidation
                type="password"
                name="password"
                title="Password"
                className="register-field"
              ></FormValidation>

              <FormValidation
                type="password"
                name="retypePassword"
                title="Retype your password"
                className="register-field"
              ></FormValidation>
              <button type="submit" className="register-button">
                Register
              </button>
            </Form>
          </div>
        </Formik>
      </div>
    </div>
  );
};

export default Register;
