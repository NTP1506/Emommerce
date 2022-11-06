import "./new.scss";
import Sidebar from "../../components/sidebar/Sidebar";
import Navbar from "../../components/navbar/Navbar";
import DriveFolderUploadOutlinedIcon from "@mui/icons-material/DriveFolderUploadOutlined";
import { useEffect, useState } from "react";

const New = ({ inputs, title }) => {
  const [file, setFile] = useState("");
  const [cate, setCate] = useState([]);
  const data = {};
  useEffect(function () {
    fetch("https://localhost:7137/Category")
      .then((response) => response.json())
      .then(setCate);
  }, []);
  const submitHandler = function (event) {
    event.preventDefault();
    fetch("https://localhost:7137/Category", {
      method: "post",
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
            <form onSubmit={submitHandler} style={{justifyContent: "space-between"}}>
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

              {/* <div className="formInput">
                <label>Category</label>
                <select
                  name="catId"
                  onChange={changeHandlder}
                  style={{
                    width: "100%",
                    border: "unset",
                    borderBottom: "1px solid gray",
                    padding: "5px",
                  }}
                >
                  <option value=""></option>
                  {cate.map((x) => {
                    return <option value={x.catId}>{x.cartName}</option>;
                  })}
                </select>
              </div> */}
              <button>Send</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default New;
