import React, {useEffect, useState} from 'react';
import OutboundTable from '../../Components/Tables/OutboundTable';
import Loading from "../../Components/Loading";
import axios from 'axios';
import "./Table.css";

const fetchOutboundComponents = async() =>{
    let response = await axios.get("//ENDPOINT")
}

const fetchShipItem = async (id) =>{
  let response = await axios.post("//ENDPOINT", "shippedMaterialsFile")
}

const OutboundList = () => {

     //CREATE FETCH REQUEST FOR THE DB, CHECK USER ROLE, IF ROLE IS WRONG NAVIGATE TO THE "NO AUTHORIZATION" PAGE
     const [loading, setLoading] = useState(true)
     const [outboundComponents, setOutboundComponents] = useState([])
 
     const handleOutboundFetch = async() =>{
         let components = []
         //components = await fetchOutboundComponents();
         var component = {id: 1, ProductDesignation: 1, CreatedAt: 11, PartNumber: 11}
         components.push(component)
         setOutboundComponents(components);
         setLoading(false);
         console.log(outboundComponents);
     }
 
     //Ez a method kiveszi a beérkező componentseket és eltárolja őket az adott raktárba (RawMat).
     const handleShipping = async(id) =>{
      fetchShipItem(id)
     }
 
 
     useEffect(()=>{
         handleOutboundFetch()
     },[])
 
     if (loading) {
         return <Loading />;
       };
 
     return (
     <OutboundTable
     outboundComponents = {outboundComponents}
     handleShipping = {handleShipping}
     />
     )
}

export default OutboundList