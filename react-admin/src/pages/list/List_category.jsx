import "./list.scss"
import Sidebar from "../../components/sidebar/Sidebar"
import Navbar from "../../components/navbar/Navbar"
import Datatable_category from "../../components/datatable/Datatable_category"


const List_category = () => {
    return (
      <div className="list">
        <Sidebar/>
        <div className="listContainer">
          <Navbar/>
          <Datatable_category/>
        </div>
      </div>
    )
  }
  
  export default List_category