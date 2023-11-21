import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';

import reportWebVitals from './reportWebVitals';
import Registration from './Pages/Registration.jsx';
import Layout from './Pages/Layout/Layout.jsx';
import Login from './Pages/Login.jsx';
import InboundList from "./Pages/LogisticsTables/InboundList.jsx";
import OutboundList from "./Pages/LogisticsTables/OutBoundList.jsx";

const router = createBrowserRouter([
  {
      path: '/',
      element: <Layout />,
     // errorElement: <ErrorPage />,
      children: [
          {
              path: '/Register',
              element: <Registration />,
          },
          {
            path: '/Login',
            element: <Login />,
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
      ]
  }
]);

ReactDOM.createRoot(document.getElementById('root')).render(
<React.StrictMode>
    <RouterProvider router={router}>{router.route}</RouterProvider>
</React.StrictMode>
);


reportWebVitals();