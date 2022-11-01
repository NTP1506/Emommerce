import Home from "./pages/home/Home";
import Login from "./pages/login/Login";
import List from "./pages/list/List";
import List_Product from "./pages/list/List_product";
import Single from "./pages/single/Single";
import Single_product from "./pages/single/Single_product";
import New from "./pages/new/New";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { productInputs, userInputs } from "./formSource";
import { productInputs_product, userInputs_product } from "./formSource_product";
import "./style/dark.scss";
import { useContext } from "react";
import { DarkModeContext } from "./context/darkModeContext";
import Edit from "./pages/new/Edit";
import Edit_product from "./pages/new/Edit_product";
import New_product from "./pages/new/New_product";


function App() {
  const { darkMode } = useContext(DarkModeContext);
  return (
    <div className={darkMode ? "app dark" : "app"}>
      <BrowserRouter>
        <Routes>
          <Route path="/">
            <Route index element={<Home />} />
            <Route path="login" element={<Login />} />
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
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
