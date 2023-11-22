import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

const Test = () => {
  const navigate = useNavigate();
  useEffect(() => {
    const token = localStorage.getItem("jwtToken");
    if (!token) {
      navigate("/");
      return;
    }
    fetch("https://localhost:7139/api/Bmx", {
      headers: { Authorization: `Bearer ${token}` },
    }).then((resp) => {
      resp = resp.json();
      resp.then((result) => {
        console.log(result);
      });
    });
  });

  return (
    <div>
      <p>test</p>
    </div>
  );
};

export default Test;
