import React, {useEffect, useState} from 'react';
import CustomerPlannerTable from '../../Components/Tables/CustomerPlannerTable';
import Loading from '../../Components/Loading';
import axios from 'axios';
import "./Table.css";

const fetchCustomerDemands = async() =>{
    let response = await axios.get("ENDPOINT")
  }
  
  const createCustomerDemands= async(order) =>{
    let response = await axios.post("//ENDPOINT", "order")
  }
  

function CustomerPlannerList() {
    const [loading, setLoading] = useState(true)
    const [customerDemands, setCustomerDemands] = useState([])
    const [formdata, setFormData] = useState({
        "Quantity": '',
        "Product Designation": ''
    });
    const [responseState, setResponseState] = useState('');

    const handleInputChange = (e) => {
        setFormData({...formdata, [e.target.name]: e.target.value });
    }

    const handlePlanFetch = async() =>{
        let plans = []
        //plans = await fetchProductionPlans();
        let component = {id: 1, ProductDesignation: 1, CreatedAt: 11, PartNumber: 11}
        let component2 = {id: 2, ProductDesignation: 2, CreatedAt: 21, PartNumber: 21}
        plans.push(component)
        plans.push(component2)
        setCustomerDemands(plans);
        setLoading(false);
        console.log(customerDemands);
    }
  
    //Ez a method kiveszi a beérkező componentseket és eltárolja őket az adott raktárba (RawMat).
    const handleCustomerDemands = async(id) =>{
        console.log("juj")
    }
  
    useEffect(() => {
      console.log(customerDemands); // This will log the updated value
  }, [customerDemands]);

    useEffect(()=>{
        handlePlanFetch()
    },[])
  
    if (loading) {
        return <Loading />;
      };
  
    return (
    <CustomerPlannerTable
    customerDemands = {customerDemands}
    handleCustomerDemand = {handleCustomerDemands}
    handleInputChange = {handleInputChange}
    />
    )
}

export default CustomerPlannerList