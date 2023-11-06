import React from "react";
import { ErrorMessage, Field } from "formik";

const FormValidation = (props) => {
  return (
    <div>
      <p>{props.title}</p>
      <Field
        type={props.type}
        name={props.name}
        className={props.className}
      ></Field>
      <ErrorMessage
        className="error-message"
        name={props.name}
        component="div"
      ></ErrorMessage>
    </div>
  );
};
export default FormValidation;
