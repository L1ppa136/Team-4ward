import React from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import reportWebVitals from './reportWebVitals';
import './index.css';

import Registration from './Pages/Registration.jsx';
import Layout from './Pages/Layout/Layout.jsx';
import Login from './Pages/Login.jsx';
import User from './Pages/User.jsx';
import Home from './Pages/Home.jsx';
import InboundList from "./Pages/LogisticsTables/InboundList.jsx";
import OutboundList from "./Pages/LogisticsTables/OutboundList.jsx";
import ProdSupplyList from "./Pages/LogisticsTables/ProdSupplyList.jsx";
import ProductionList from "./Pages/LogisticsTables/ProductionList.jsx";
import CustomerPlannerList from './Pages/LogisticsTables/CustomerPlannerList.jsx';
import Admin from './Pages/Admin.jsx';

const router = createBrowserRouter([
  {
    path: '/',
    element: <Layout />,
    // errorElement: <ErrorPage />,
    children: [
      {
        path: '/',
        element: <Home />,
      },
      {
        path: '/Register',
        element: <Registration />,
      },
      {
        path: '/Login',
        element: <Login />,
      },
      {
        path: '/User',
        element: <User />,
      },
      {
        path: '/Admin',
        element: <Admin />,
      },
      {
        path: '/Inbound',
        element: <InboundList />,
      },
      {
        path: '/Outbound',
        element: <OutboundList />,
      },
      {
        path: '/Prodsupply',
        element: <ProdSupplyList />,
      },
      {
        path: '/Production',
        element: <ProductionList />,
      },
      {
        path: '/CustomerPlanner',
        element: <CustomerPlannerList />,
      }
    ]
  }
]);

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <RouterProvider router={router}>{router.route}</RouterProvider>
  </React.StrictMode>
);


reportWebVitals();