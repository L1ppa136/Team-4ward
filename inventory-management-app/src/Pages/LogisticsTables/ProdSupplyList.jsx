import React, { useState, useEffect } from 'react';
import ProdSupplyTable from '../../Components/Tables/ProdSupplyTable';
import Loading from "../../Components/Loading"
import "./Table.css";
import axios from 'axios';

const fetchStoreComponentToProduction = async (orderItem) => {
  try {
    const response = await axios.post('api/ForkliftDriver/MoveComponentToProduction', orderItem);
    return response.data;
  } catch (error) {
    throw error.response ? error.response.data : error;
  }
}

const ProdSupplyList = () => {
  const [goodsToSupply, setGoodsToSupply] = useState([])
  const [loading, setLoading] = useState(false)

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

  const nextOrderList = async () => {
    //var componentToProduceTEST = { "quantity": 1000, "productDesignation": "Airbag" }
    let productionOrder = localStorage.getItem("OrderFromWarehouse");
    let productionOrderObject = await JSON.parse(productionOrder)
    //let productionOrderObject = componentToProduceTEST

    if (productionOrderObject && productionOrderObject.quantity !== null) {
      let generatedList = GenerateOrderList(productionOrderObject.quantity);
      let keyValueArr = Object.entries(generatedList).map(([key, value]) => {
        console.log(key, value)
        return { key, value }
      })
      setGoodsToSupply(keyValueArr);
      console.log("Components here!" + goodsToSupply.map((comp) => console.log(comp)));
      localStorage.removeItem("OrderFromWarehouse");
      return goodsToSupply;
    }
    return console.log("No quantity in Order")
  }

  const handleStore = async (quantity, productDesignation) => {
    var componentToFetch = { "quantity": quantity, "productDesignation": productDesignation };
    console.log("componentsToProduction:", componentToFetch)
    await fetchStoreComponentToProduction(componentToFetch);
    removeItem(productDesignation);
  }

  const removeItem = (productDes) => {
    setLoading(true);
    setGoodsToSupply(prevState => prevState.filter(item => item.key !== productDes));
    setLoading(false);
  };

  useEffect(() => {
    console.log("HERE", goodsToSupply);
  }, [goodsToSupply]);

  useEffect(() => {
  }, []);


  return (
    <>{loading ? (<Loading />) : (<ProdSupplyTable
      goodsToSupply={goodsToSupply}
      handleStore={handleStore}
      nextOrderList={nextOrderList}
    />)}
    </>

  )
}

export default ProdSupplyList