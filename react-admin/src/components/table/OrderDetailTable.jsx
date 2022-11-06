import "./table.scss";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { DataGrid } from "@mui/x-data-grid";
import { userColumns, userRows } from "../../datatablesource_orderdetail";


const OrderDetailTable = ({userId}) => {
  
  const [file, setFile] = useState("");
  //const [data, setData] = useState([]);
  const data = [];
  useEffect(() => {
    fetch("https://localhost:7137/Order/" + userId )
      .then((response) => response.json())
      .then(function(orders){
        Promise.all(orders.map(order => {
          return fetch("https://localhost:7137/OrderDetail/" + order.orderId);
        }))
        .then(function(orderDetails) {
          console.log(orderDetails);
        })
      });
      //console.log(id);
  }, []);
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


  // const rows = [
  //   {
  //     id: 1143155,
  //     product: "Acer Nitro 5",
  //     img: "https://m.media-amazon.com/images/I/81bc8mA3nKL._AC_UY327_FMwebp_QL65_.jpg",
  //     customer: "John Smith",
  //     date: "1 March",
  //     amount: 785,
  //     method: "Cash on Delivery",
  //     status: "Approved",
  //   },
  //   {
  //     id: 2235235,
  //     product: "Playstation 5",
  //     img: "https://m.media-amazon.com/images/I/31JaiPXYI8L._AC_UY327_FMwebp_QL65_.jpg",
  //     customer: "Michael Doe",
  //     date: "1 March",
  //     amount: 900,
  //     method: "Online Payment",
  //     status: "Pending",
  //   },
  //   {
  //     id: 2342353,
  //     product: "Redragon S101",
  //     img: "https://m.media-amazon.com/images/I/71kr3WAj1FL._AC_UY327_FMwebp_QL65_.jpg",
  //     customer: "John Smith",
  //     date: "1 March",
  //     amount: 35,
  //     method: "Cash on Delivery",
  //     status: "Pending",
  //   },
  //   {
  //     id: 2357741,
  //     product: "Razer Blade 15",
  //     img: "https://m.media-amazon.com/images/I/71wF7YDIQkL._AC_UY327_FMwebp_QL65_.jpg",
  //     customer: "Jane Smith",
  //     date: "1 March",
  //     amount: 920,
  //     method: "Online",
  //     status: "Approved",
  //   },
  //   {
  //     id: 2342355,
  //     product: "ASUS ROG Strix",
  //     img: "https://m.media-amazon.com/images/I/81hH5vK-MCL._AC_UY327_FMwebp_QL65_.jpg",
  //     customer: "Harold Carol",
  //     date: "1 March",
  //     amount: 2000,
  //     method: "Online",
  //     status: "Pending",
  //   },
  // ];
  // return (
  //   <TableContainer component={Paper} className="table">
  //     <Table sx={{ minWidth: 650 }} aria-label="simple table">
  //       <TableHead>
  //         <TableRow>
  //           <TableCell className="tableCell">Tracking ID</TableCell>
  //           <TableCell className="tableCell">Product</TableCell>
  //           <TableCell className="tableCell">Customer</TableCell>
  //           <TableCell className="tableCell">Date</TableCell>
  //           <TableCell className="tableCell">Amount</TableCell>
  //           <TableCell className="tableCell">Payment Method</TableCell>
  //           <TableCell className="tableCell">Status</TableCell>
  //         </TableRow>
  //       </TableHead>
  //       <TableBody>
  //         {rows.map((row) => (
  //           <TableRow key={row.id}>
  //             <TableCell className="tableCell">{row.id}</TableCell>
  //             <TableCell className="tableCell">
  //               <div className="cellWrapper">
  //                 <img src={row.img} alt="" className="image" />
  //                 {row.product}
  //               </div>
  //             </TableCell>
  //             <TableCell className="tableCell">{row.customer}</TableCell>
  //             <TableCell className="tableCell">{row.date}</TableCell>
  //             <TableCell className="tableCell">{row.amount}</TableCell>
  //             <TableCell className="tableCell">{row.method}</TableCell>
  //             <TableCell className="tableCell">
  //               <span className={`status ${row.status}`}>{row.status}</span>
  //             </TableCell>
  //           </TableRow>
  //         ))}
  //       </TableBody>
  //     </Table>
  //   </TableContainer>
  //);
// };

export default OrderDetailTable;
