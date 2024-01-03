import React from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import reportWebVitals from './reportWebVitals';
import './index.css';
import Layout from './Pages/Layout/Layout.jsx';
import Registration from './Pages/Registration.jsx';
import Login from './Pages/Login.jsx';
import User from './Pages/User.jsx';
import Home from './Pages/Home.jsx';
import InboundList from "./Pages/LogisticsTables/InboundList.jsx";
import OutboundList from "./Pages/LogisticsTables/OutboundList.jsx";
import ProdSupplyList from "./Pages/LogisticsTables/ProdSupplyList.jsx";
import ProductionList from "./Pages/LogisticsTables/ProductionList.jsx";
import CustomerPlannerList from './Pages/LogisticsTables/CustomerPlannerList.jsx';
import ShipList from "./Pages/LogisticsTables/ShipList.jsx";
import PrivateRoute from './Components/PrivateRoute.jsx';
import UserManager from './Pages/UserManager.jsx';
import { AuthProvider } from './Components/AuthContext.jsx';

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
        element: <Login/>,
      },
      {
        path: '/User',
        element: <User />,
      },
      {
        path: '/Users',
        element: <PrivateRoute element={<UserManager />} roles={['Admin']} />
      },
      {
        path: '/Inbound',
        element: <PrivateRoute element={<InboundList />} roles={['Admin', 'Forklift Driver', 'Warehouse Leader']} />
      },
      {
        path: '/Outbound',
        element: <PrivateRoute element={<OutboundList />} roles={['Admin', 'Forklift Driver', 'Warehouse Leader']} />
      },
      {
        path: '/Prodsupply',
        element: <PrivateRoute element={<ProdSupplyList />} roles={['Admin', 'Forklift Driver', 'Warehouse Leader']} />
      },
      {
        path: '/Production',
        element: <PrivateRoute element={<ProductionList />} roles={['Admin', 'Production Leader']} />
      },
      {
        path: '/CustomerPlanner',
        element: <PrivateRoute element={<CustomerPlannerList />} roles={['Admin', 'Customer Planner', 'Warehouse Leader']} />
      },
      {
        path: '/ShipList',
        element: <PrivateRoute element={<ShipList />} roles={['Admin', 'Warehouse Leader', 'Forklift Driver']} />
      }
    ]
  }
]);

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <AuthProvider>
        <RouterProvider router={router}>
          {router.route}
        </RouterProvider>
      </AuthProvider>
  </React.StrictMode>
);


reportWebVitals();