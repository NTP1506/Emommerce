import "./table.scss";
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { DataGrid } from "@mui/x-data-grid";
import { userColumns, userRows } from "../../datatablesource_orderdetail";


const OrderDetailTable = ({userId}) => {
  
  const [file, setFile] = useState("");
  //const [data, setData] = useState([]);
  const data = [];
  // useEffect(() => {
  //   fetch("https://localhost:7137/Order/" + userId )
  //     .then((response) => response.json())
  //     .then(function(orders){
        
  //       Promise.all(orders.map(order => {
  //         return fetch("https://localhost:7137/OrderDetail/" + order.orderId);
  //       }))
  //       .then(function(orderDetails) {
  //         console.log(orderDetails);
  //       })
  //     });
  //     //console.log(id);
  // }, []);
  // const submitHandler = function (event) {
  //   event.preventDefault();
  //   fetch("https://localhost:7137/Product/Update/" + id, {
  //     method: "put",
  //     body: JSON.stringify(data),
  //     headers: {
  //       "Content-Type": "application/json",
  //     },
  //   }).then(function () {
  //     alert("Lưu thay đổi");
  //   });
  // };
  // const changeHandlder = function (e) {
  //   data[e.target.name] = e.target.value;
  
  const actionColumn = [
    {
      field: "action",
      headerName: "Action",
      width: 200,
      renderCell: (params) => {
        return (
          <div className="cellAction">
            <Link
              to={"/products/" + params.row.id}
              style={{ textDecoration: "none" }}
            >
              <div className="viewButton">View</div>
            </Link>
            <Link
              to={"/products/edit?id=" + params.row.id}
              style={{ textDecoration: "none" }}
            >
              <div className="viewButton">Edit</div>
            </Link>
            {/* <div
              className="deleteButton"
              onClick={() => handleDelete(params.row.id)}
            >
              Delete
            </div> */}
          </div>
        );
      },
    },
  ];
  return (
    <div className="datatable">
      <div className="datatableTitle">
        Danh sách hóa đơn.
        <Link to="/products/new" className="link">
          Add New
        </Link>
      </div>

      <DataGrid
        className="datagrid"
        rows={data}        
        columns={userColumns.concat(actionColumn)}
        pageSize={9}
        rowsPerPageOptions={[9]}
        checkboxSelection
      />
    </div>
  );
};



export default OrderDetailTable;
