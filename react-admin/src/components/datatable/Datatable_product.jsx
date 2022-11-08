import { DataGrid } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { userColumns, userRows } from "../../datatablesource_product";
import "./datatable.scss";
import RequestService from "../../components/Service/request";

const Datatable = () => {
  const [data, setData] = useState(userRows);

  const handleDelete = (id) => {
    setData(data.filter((item) => item.id !== id));
    // post delete
    RequestService.axios.delete("https://localhost:7137/Product/Delete/" + id);
    //fetch("https://localhost:7137/Product/Delete/" + id, { method: "DELETE" });
  };
  useEffect(() => {
    const f = async () => {
      return (
        await RequestService.axios.get("https://localhost:7137/Product/Get")
      )["data"];
    };
    f()
      // fetch("https://localhost:7137/Product/Get")
      //   .then((response) => response.json())
      .then((data) => {        
        data = data.map((x) => {
          x["id"] = x["productId"];
          try {
            x["cartName"] = x["cat"]["cartName"];
          } catch(e) {
            console.error(e);
            x["cartName"] = "n/a";
          }
          
          return x;
        });
        setData(data);
      });
  }, []);
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
            <div
              className="deleteButton"
              onClick={() => handleDelete(params.row.id)}
            >
              Delete
            </div>
          </div>
        );
      },
    },
  ];
  return (
    <div className="datatable">
      <div className="datatableTitle">
        Danh sách sản phẩm.
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

export default Datatable;
