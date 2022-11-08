/* eslint-disable react/jsx-pascal-case */
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
import { useContext, useEffect, useState } from "react";
import { DarkModeContext } from "./context/darkModeContext";
import Edit from "./pages/new/Edit";
import Edit_product from "./pages/new/Edit_product";
import Edit_category from "./pages/new/Edit_category";
import New_product from "./pages/new/New_product";
import New_category from "./pages/new/New_category";
import Register from "./pages/register/Register";
import RequestService from "./components/Service/request";


function App() {
  const { darkMode } = useContext(DarkModeContext);
  const [loginState, setLoginState] = useState(-1);
  
  const renderFromLoginState = function(ele) {
    switch(loginState) {
      case -1: return <br/>;
      case 0: return <Login/>;
      default: return ele;
    }
  }

  useEffect( () => {
    if (!localStorage.getItem("token")) {
      setLoginState(0);
      return;
    }
    const checkLogin = async () => {      
      const isLogined = await RequestService.axios.get(
        "https://localhost:7137/Account/checkToken"
      )  
      setLoginState(isLogined ? 1 : 0);      
    }  
    checkLogin()
    .catch(function() {
      setLoginState(0);
    });
  }, []);  
  return (
    <div className={darkMode ? "app dark" : "app"}>
      <BrowserRouter>
        <Routes>
          <Route path="/">

            <Route index element={renderFromLoginState(<Home />)} />
            <Route path="login" element={<Login />} />
            <Route path="register" element={renderFromLoginState(<Register />)} />
            <Route path="users">
              <Route index element={renderFromLoginState(<List />)} />
              <Route path=":userId" element={renderFromLoginState(<Single />)} />
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
              <Route index element={renderFromLoginState(<List_Product />)} />
              <Route path=":productId" element={renderFromLoginState(<Single_product />)} />
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
              <Route index element={renderFromLoginState(<List_category />)} />
              <Route path=":categoryId" element={<Single_category />} />
              <Route
                path="new"
                element={renderFromLoginState(<New_category inputs={categoryInputs_category} title="Add New Category" />)}
              />
              <Route
                path="edit"
                element={renderFromLoginState(<Edit_category inputs={userInputs_category} title="Edit Category" />)}
              />
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
