import React, { useState, useEffect } from 'react';
import CustomerPlannerTable from '../../Components/Tables/CustomerPlannerTable';
import "./Table.css";

function CustomerPlannerList() {
  const [formdata, setFormData] = useState({
    "quantity": '',
    "productDesignation": 'Airbag'
  });

  const handleInputChange = (e) => {
    setFormData({ ...formdata, [e.target.name]: e.target.value });
  }

  const createComponents = () => {
    // Convert the formdata object to a JSON string before storing
    const formDataString = JSON.stringify(formdata);

    // Store the JSON string in localStorage
    localStorage.setItem("OrderList", formDataString);

    // Retrieve the JSON string from localStorage
    const locStoreDataString = localStorage.getItem("OrderList");

    // Parse the JSON string back to an object
    const locStoreDataObject = JSON.parse(locStoreDataString);

    // Log the keys of the object and the original formdata
    console.log(Object.values(locStoreDataObject), Object.keys(locStoreDataObject));
    window.alert("Order Successful!")
    console.log(formdata);
  }

  return (
    <CustomerPlannerTable
      createComponents={createComponents}
      handleInputChange={handleInputChange}
    />
  )
}

export default CustomerPlannerList