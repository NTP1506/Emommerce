import "./new.scss";
import Sidebar from "../../components/sidebar/Sidebar";
import Navbar from "../../components/navbar/Navbar";
import DriveFolderUploadOutlinedIcon from "@mui/icons-material/DriveFolderUploadOutlined";
import { useEffect, useState } from "react";
import Uploader from "../../firebase/Upload";
import RequestService from "../../components/Service/request";

const New = ({ inputs, title }) => {
  const [file, setFile] = useState("");
  const [cate, setCate] = useState([]);
  const data = {};
  useEffect(function () {
    const f = async () => {      
      return (await RequestService.axios.get("https://localhost:7137/Category"))["data"];     
    }  
    f()
    // fetch("https://localhost:7137/Category")
    //   .then((response) => response.json())
      .then(setCate);
  }, []);
  const submitHandler = async function (event) {
    event.preventDefault();
    const files = Array.from(event.target.Thumb.files);
    const filenames = await Uploader.upload(files);
    //console.log('filenames');
    Promise.resolve().then(() => {
      data["Thumb"] = filenames;
      const f = async () => {
        return (
          await RequestService.axios.post("https://localhost:7137/Product/Create", data, {            
            headers: {
              "Content-Type": "application/json",
            },
          })
        )["data"];
      };
      f()
        // fetch("https://localhost:7137/Product/Create", {
        //   method: "post",
        //   body: JSON.stringify(data),
        //   headers: {
        //     "Content-Type": "application/json",
        //   },
        // })
        .then(function () {
          alert("Lưu thay đổi");
        });
    });
  };
  const changeHandlder = function (e) {
    if (e.target.name === "bestSellers") {
      data[e.target.name] = e.target.value.toLowerCase() === "true";
      return;
    }
    if (e.target.name === "active") {
      data[e.target.name] = e.target.value.toLowerCase() === "true";
      return;
    }
    if (e.target.name === "homeFlag") {
      data[e.target.name] = e.target.value.toLowerCase() === "true";
      return;
    }
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
                  : data.thumb
                  ? "https://firebasestorage.googleapis.com/v0/b/ecommerce-5ae64.appspot.com/o/images%" +
                    data.thumb
                  : "https://icon-library.com/images/no-image-icon/no-image-icon-0.jpg"
              }
              alt=""
            />
          </div>
          <div className="right">
            <form
              onSubmit={submitHandler}
              style={{ justifyContent: "space-between" }}
            >
              <div className="formInput">
                <label htmlFor="file">
                  Image: <DriveFolderUploadOutlinedIcon className="icon" />
                </label>
                <input
                  type="file"
                  id="img"
                  accept="image/*"
                  name="Thumb"
                  onChange={changeHandlder}
                  //onChange={(e) => setFile(e.target.files[0])}
                  //style={{ display: "none" }}
                  multiple
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

              <div className="formInput">
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
              </div>
              <button>Send</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default New;
