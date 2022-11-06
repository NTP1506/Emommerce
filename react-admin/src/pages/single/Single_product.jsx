import "./single.scss";
import Sidebar from "../../components/sidebar/Sidebar";
import Navbar from "../../components/navbar/Navbar";
import Chart from "../../components/chart/Chart";
import OrderDetailTable from "../../components/table/OrderDetailTable";
import { useEffect, useState } from "react";

const Single = () => {
  const [data, setData] = useState({});
  const productId = window.location.pathname.split("/")[2];
  useEffect(() => {
    fetch("https://localhost:7137/Product/GetID/" + productId)
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
                src={"https://firebasestorage.googleapis.com/v0/b/ecommerce-5ae64.appspot.com/o/images%" + data.thumb}
                alt=""
                className="itemImg"
              />
              <div className="details">
                <h1 className="itemTitle">{data.alias}</h1>
                <div className="detailItem">
                  <span className="itemKey">Name:</span>
                  <span className="itemValue">{data.productName}</span>
                </div>
                {/* <div className="detailItem">
                  <span className="itemKey">CartName:</span>
                  <span className="itemValue">{data.cat.cartName}</span>
                </div> */}
                <div className="detailItem">
                  <span className="itemKey">Price:</span>
                  <span className="itemValue">{data.price}</span>
                </div>
                <div className="detailItem">
                  <span className="itemKey">Discount:</span>
                  <span className="itemValue">{data.discount}</span>
                </div>
                <div className="detailItem">
                  <span className="itemKey">Stock:</span>
                  <span className="itemValue">{data.unitslnStock}</span>
                </div>
                {/* <div className="detailItem">
                  <span className="itemKey">CategoryId:</span>
                  <span className="itemValue">{data.catId}</span>
                </div> */}
                <div className="detailItem">
                  <span className="itemKey">Description:</span>
                  <span className="itemValue">{data.descriptions}</span>
                </div>
                <div className="detailItem">
                  <span className="itemKey">Rating:</span>
                  <span className="itemValue">{data.rating}</span>
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
          <OrderDetailTable id = {productId}/>
        </div>
      </div>
    </div>
  );
};

export default Single;
