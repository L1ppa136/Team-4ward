import React, { useState, useEffect } from 'react'
import axios from 'axios';
import ProductionTable from '../../Components/Tables/ProductionTable';
import "./Table.css";



const fetchProduce = async (ItemToProduce) => {
  try {
    const response = await axios.post('api/ProductionLeader/ProduceFinishedGoods', ItemToProduce);
    console.log(ItemToProduce);
    return response.data;
  } catch (error) {
    throw error.response ? error.response.data : error;
  }
}

const fetchProductionStock = async (ItemToOrder) => {
  try {
    const response = await axios.get('api/ProductionLeader/GetProductionStock', ItemToOrder);
    return response.data;
  } catch (error) {
    throw error.response ? error.response.data : error;
  }
}

function ProductionList() {
  const [productionStock, setProductionStock] = useState([]);
  const [formdata, setFormData] = useState({
    "quantity": '',
    "productDesignation": 'Airbag'
  });

  const GenerateOrderList = (Quantity) => {
    return {
      Screw: 4 * Quantity,
      Nut: 4 * Quantity,
      Cushion: 1 * Quantity,
      Diffusor: 1 * Quantity,
      Retainer: 1 * Quantity,
      Cover: 1 * Quantity,
      Emblem: 1 * Quantity,
      Inflator: 1 * Quantity,
      WireHarness: 1 * Quantity
    }
  }

  useEffect(() => {
    const fetchData = async () => {
      let response = await fetchProductionStock(formdata);
      setProductionStock(JSON.stringify(response));
    };

    fetchData();
  }, [formdata]);

  function checkMaterialAvailability(stock, requirements) {
    for (const item of stock) {
      const itemName = item.locationName;
      const requiredQuantity = requirements[itemName];
      if (requiredQuantity === undefined || item.quantity < requiredQuantity) {
        console.log(`Not enough ${itemName}. Required: ${requiredQuantity}, Available: ${item.quantity}`);
        return false;
      }
    }
    return true;
  }

  const handleInputChange = (e) => {
    setFormData({ ...formdata, [e.target.name]: e.target.value });
  }

  const handleOrderingFromWarehouse = (quantity, productDesignation) => {
    var componentToProduce = { "quantity": quantity, "productDesignation": productDesignation };
    orderComponentsFromWarehouse(componentToProduce);
  }

  const handleProduce = async (quantity, productDesignation) => {
    var componentToProduce = { "quantity": quantity, "productDesignation": productDesignation };
    let response = await fetchProductionStock(componentToProduce);
    setProductionStock(JSON.stringify(response));
    if (checkMaterialAvailability(productionStock, GenerateOrderList(quantity))) {
      console.log("ElementsToProduce:", componentToProduce);
      await fetchProduce(componentToProduce);
      window.alert(`${quantity} item has been produced.`)
    }
    else {
      console.log(componentToProduce);
      window.alert("There is not enough material on the production, please contact the Warehouse Leader!")
    }
  }

  const orderComponentsFromWarehouse = () => {
    // Convert the formdata object to a JSON string before storing
    const formDataString = JSON.stringify(formdata);

    // Store the JSON string in localStorage
    localStorage.setItem("OrderFromWarehouse", formDataString);

    // Retrieve the JSON string from localStorage
    const locStoreDataString = localStorage.getItem("OrderFromWarehouse");

    // Parse the JSON string back to an object
    const locStoreDataObject = JSON.parse(locStoreDataString);

    // Log the keys of the object and the original formdata
    console.log(Object.values(locStoreDataObject), Object.keys(locStoreDataObject));
    console.log(formdata);
  }

  return (
    <ProductionTable
      handleInputChange={handleInputChange}
      handleOrderingFromWarehouse={handleOrderingFromWarehouse}
      handleProduce={handleProduce}
    />
  )
}

export default ProductionList