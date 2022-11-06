export const userColumns = [
  { field: "productId", headerName: "ID", width: 70 },
  {
    field: "thumb",
    headerName: "Image",
    width: 230,
    renderCell: params => {
      const url = "https://firebasestorage.googleapis.com/v0/b/ecommerce-5ae64.appspot.com/o/images%" + params.row.thumb;
      return (<div className="cellWithImg">
        <img className="cellImg" src={url} alt="avatar" onError={(e)=>e.target.style.display='none'} onLoad={(e)=>e.target.style.display='block'} />
      </div>);
    }
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
    field: "cartName",
    headerName: "cartName",
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
