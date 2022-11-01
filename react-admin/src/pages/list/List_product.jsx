import "./list.scss"
import Sidebar from "../../components/sidebar/Sidebar"
import Navbar from "../../components/navbar/Navbar"
import Datatable_product from "../../components/datatable/Datatable_product"


const List_Product = () => {
    return (
      <div className="list">
        <Sidebar/>
        <div className="listContainer">
          <Navbar/>
          <Datatable_product/>
        </div>
      </div>
    )
  }
  
  export default List_Product