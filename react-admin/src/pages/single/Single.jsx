import "./single.scss";
import Sidebar from "../../components/sidebar/Sidebar";
import Navbar from "../../components/navbar/Navbar";
import Chart from "../../components/chart/Chart";
import { useEffect, useState } from "react";
import OrderDetailTable from "../../components/table/OrderDetailTable";
import RequestService from "../../components/Service/request";

const Single = () => {
  const [data, setData] = useState({});
  const userId = window.location.pathname.split("/")[2];
  useEffect(() => {
    const f = async () => {      
      return (await RequestService.axios.get("https://localhost:7137/" + userId))["data"];     
    }  
    f().then(setData);
    
    // fetch("https://localhost:7137/" + userId)
    // .then((response) =>response.json())
    // .then(setData);
  },[]);
  return (
    <div className="single">
      <Sidebar />
      <div className="singleContainer">
        <Navbar />
        <div className="top">
          <div className="left">
            <div className="editButton">Edit</div>
            <h1 className="title">Information</h1>
            <div className="item">
              <img
                //src="https://images.pexels.com/photos/733872/pexels-photo-733872.jpeg?auto=compress&cs=tinysrgb&dpr=3&h=750&w=1260"
                src =""
                alt=""
                className="itemImg"
              />
              <div className="details">
                <h1 className="itemTitle">{data.fullName}</h1>
                <div className="detailItem">
                  <span className="itemKey">Email:</span>
                  <span className="itemValue">{data.email}</span>
                </div>
                <div className="detailItem">
                  <span className="itemKey">Phone:</span>
                  <span className="itemValue">{data.phone}</span>
                </div>
                <div className="detailItem">
                  <span className="itemKey">Address:</span>
                  <span className="itemValue">
                   {data.address}
                  </span>
                </div>
                {/* <div className="detailItem"> */}
                  {/* <span className="itemKey">Country:</span> */}
                  {/* <span className="itemValue">USA</span> */}
                {/* </div> */}
              </div>
            </div>
          </div>
          {/* <div className="right">
            <Chart aspect={3 / 1} title="User Spending ( Last 6 Months)" />
          </div> */}
        </div>
        <div className="bottom">
        <h1 className="title">Last Transactions</h1>
          <OrderDetailTable userId = {userId} />
        </div>
      </div>
    </div>
  );
};

export default Single;
