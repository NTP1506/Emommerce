import "./new.scss";
import Sidebar from "../../components/sidebar/Sidebar";
import Navbar from "../../components/navbar/Navbar";
import DriveFolderUploadOutlinedIcon from "@mui/icons-material/DriveFolderUploadOutlined";
import { useState, useEffect } from "react";

const Edit_product = ({ inputs, title }) => {
  const [file, setFile] = useState("");
  const [data, setData] = useState("");
  const id = new URLSearchParams(window.location.search).get("id");
  useEffect(() => {
    fetch("https://localhost:7137/Product/GetID/" + id)
      .then((response) => response.json())
      .then(setData);
  }, []);
  const submitHandler = function (event) {
    event.preventDefault();
    fetch("https://localhost:7137/Product/Update/" + id, {
      method: "put",
      body: JSON.stringify(data),
      headers: {
        "Content-Type": "application/json",
      },
    }).then(function () {
      alert("Lưu thay đổi");
    });
  };
  const changeHandlder = function (e) {
    data[e.target.name] = e.target.value;
  };
  return (
    <div className="new">
      <Sidebar />
      <div className="newContainer">
        <Navbar />
        <div className="top">
          <h1>{title}</h1>
        </div>
        <div className="bottom">
          <div className="left">
            <img
              src={
                file
                  ? URL.createObjectURL(file)
                  : "https://icon-library.com/images/no-image-icon/no-image-icon-0.jpg"
              }
              alt=""
            />
          </div>
          <div className="right">
            <form onSubmit={submitHandler}>
              <div className="formInput">
                <label htmlFor="file">
                  Image: <DriveFolderUploadOutlinedIcon className="icon" />
                </label>
                <input
                  type="file"
                  id="file"
                  onChange={(e) => setFile(e.target.files[0])}
                  style={{ display: "none" }}
                />
              </div>
              <input name="productId" defaultValue={data["productId"] || ""} />
              {inputs.map((input) => {
                const { name } = input;
                return (
                  <div className="formInput" key={input.id}>
                    <label>{input.label}</label>
                    <input
                      type={input.type}
                      placeholder={input.placeholder}
                      name={name}
                      defaultValue={data[name] || ""}
                      onChange={changeHandlder}
                    />
                  </div>
                );
              })}
              <button>Send</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Edit_product;