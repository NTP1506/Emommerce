import "./single.scss";
import Sidebar from "../../components/sidebar/Sidebar";
import Navbar from "../../components/navbar/Navbar";
import Chart from "../../components/chart/Chart";
import { useEffect, useState } from "react";
import OrderDetailTable from "../../components/table/OrderDetailTable";

const Single = () => {
  const [data, setData] = useState({});
  const productId = window.location.pathname.split("/")[2];
  useEffect(() => {
    fetch("https://localhost:7137/Category/" + productId)
      .then((response) => response.json())
      .then(setData);
  }, []);
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
                src="https://images.pexels.com/photos/733872/pexels-photo-733872.jpeg?auto=compress&cs=tinysrgb&dpr=3&h=750&w=1260"
                alt=""
                className="itemImg"
              />
              <div className="details">
                <h1 className="itemTitle">{data.cartName}</h1>
                <div className="detailItem">
                  <span className="itemKey">CatID:</span>
                  <span className="itemValue">{data.catId}</span>
                </div>
                <div className="detailItem">
                  <span className="itemKey">Description:</span>
                  <span className="itemValue">{data.descriptions}</span>
                </div>
              </div>
            </div>
          </div>
          <div className="right">
            <Chart aspect={3 / 1} title="User Spending ( Last 6 Months)" />
          </div>
        </div>
        <div className="bottom">
          <h1 className="title">Last Transactions</h1>
          <OrderDetailTable />
        </div>
      </div>
    </div>
  );
};

export default Single;
