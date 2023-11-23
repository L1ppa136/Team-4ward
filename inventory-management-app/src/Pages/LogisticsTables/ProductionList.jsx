import React, {useState,useEffect} from 'react'
import Loading from '../../Components/Loading'
import ProductionTable from '../../Components/Tables/ProductionTable'
import axios from 'axios';
import "./Table.css";

//Endpointról kikell szedni a Bill of materials -> ez alapján lehessen rendelni.

const fetchProductionPlans = async() =>{
  let response = await axios.get("ENDPOINT")
}

const createProductionOrder= async(order) =>{
  let response = await axios.post("//ENDPOINT", "order")
}

const ProductionList = () => {
  const [loading, setLoading] = useState(true)
  const [productionPlans, setProductionPlans] = useState([])

  const handlePlanFetch = async() =>{
      let plans = []
      //plans = await fetchProductionPlans();
      var component = {id: 1, ProductDesignation: 1, CreatedAt: 11, PartNumber: 11}
      plans.push(component)
      setProductionPlans(plans);
      setLoading(false);
      console.log(productionPlans);
  }

  //Ez a method kiveszi a beérkező componentseket és eltárolja őket az adott raktárba (RawMat).
  const handleProductionOrder = async(id) =>{
      
  }

  useEffect(()=>{
      handlePlanFetch()
  },[])

  if (loading) {
      return <Loading />;
    };

  return (
  <ProductionTable
  productionPlans = {productionPlans}
  handleProductionOrder = {handleProductionOrder}
  />
  )
}

export default ProductionList