import Home from "./pages/home/Home";
import Login from "./pages/login/Login";
import List from "./pages/list/List";
import List_Product from "./pages/list/List_product";
import List_category from "./pages/list/List_category";
import Single from "./pages/single/Single";
import Single_product from "./pages/single/Single_product";
import Single_category from "./pages/single/Single_category";
import New from "./pages/new/New";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { productInputs, userInputs } from "./formSource";
import { productInputs_product, userInputs_product } from "./formSource_product";
import { categoryInputs_category, userInputs_category } from "./formSource_category";
import "./style/dark.scss";
import { useContext } from "react";
import { DarkModeContext } from "./context/darkModeContext";
import Edit from "./pages/new/Edit";
import Edit_product from "./pages/new/Edit_product";
import Edit_category from "./pages/new/Edit_category";
import New_product from "./pages/new/New_product";
import New_category from "./pages/new/New_category";
import Register from "./pages/register/Register";



function App() {
  const { darkMode } = useContext(DarkModeContext);
  return (
    <div className={darkMode ? "app dark" : "app"}>
      <BrowserRouter>
        <Routes>
          <Route path="/">
            <Route index element={<Home />} />
            <Route path="login" element={<Login />} />
            <Route path="register" element={<Register />} />
            <Route path="users">
              <Route index element={<List />} />
              <Route path=":userId" element={<Single />} />
              <Route
                path="new"
                element={<New inputs={productInputs} title="Add New User" />}
              />
              <Route
                path="edit"
                element={<Edit inputs={userInputs} title="Edit User" />}
              />
            </Route>
            <Route path="products">
              <Route index element={<List_Product />} />
              <Route path=":productId" element={<Single_product />} />
              <Route
                path="new"
                element={<New_product inputs={productInputs_product} title="Add New Product" />}
              />
              <Route
                path="edit"
                element={<Edit_product inputs={userInputs_product} title="Edit Product" />}
              />
            </Route>
            <Route path="categories">
              <Route index element={<List_category />} />
              <Route path=":categoryId" element={<Single_category />} />
              <Route
                path="new"
                element={<New_category inputs={categoryInputs_category} title="Add New Category" />}
              />
              <Route
                path="edit"
                element={<Edit_category inputs={userInputs_category} title="Edit Category" />}
              />
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
