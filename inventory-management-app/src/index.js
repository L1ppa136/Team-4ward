import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';

import reportWebVitals from './reportWebVitals';
import Registration from './Pages/Registration.jsx';
import Layout from './Pages/Layout/Layout.jsx';
import Login from './Pages/Login.jsx';
import SetRole from './Pages/SetRole.jsx';

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
        path: '/SetRole',
        element: <SetRole />,

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