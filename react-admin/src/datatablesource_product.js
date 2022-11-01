export const userColumns = [
  { field: "productId", headerName: "ID", width: 70 },
  {
    field: "thump",
    headerName: "Image",
    width: 230,
  },
  {
    field: "productName",
    headerName: "Product",
    width: 230,
    // renderCell: (params) => {
    // return (
    // <div className="cellWithImg">
    // {/* <img className="cellImg" src={params.row.img} alt="avatar" /> */}
    // {/* {params.row.username} */}
    // {/* </div> */}
    // // );
    // },
  },
  {
    field: "alias",
    headerName: "Alias",
    width: 230,
  },
  {
    field: "price",
    headerName: "Price",
    width: 230,
  },

  {
    field: "unitslnStock",
    headerName: "Stock",
    width: 100,
  },
  // {
  // field: "status",
  // headerName: "Status",
  // width: 160,
  // renderCell: (params) => {
  // return (
  // <div className={`cellWithStatus ${params.row.status}`}>
  // {/* {params.row.status} */}
  // {/* </div> */}
  // );
  // },
  // },
];

//temporary data
export const userRows = [];
