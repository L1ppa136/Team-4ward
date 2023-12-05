import React, {useEffect, useState} from 'react';
import OutboundTable from '../../Components/Tables/OutboundTable';
import Loading from "../../Components/Loading";
import axios from 'axios';
import "./Table.css";

 const fetchOutboundComponents = async() =>{
    try {
      const response = await axios.get('http://localhost:5179/CustomerPlanner/OrderComponent');
      return response.data;
  } catch (error) {
      throw error.response ? error.response.data : error;
  }
}
 
const fetchShipItem = async (formdata) =>{
  let response = await axios.post("http://localhost:5179/ForkliftDriver/MoveToProduction", "shippedMaterialsFile")
}
//KÉSZ levő dolgok Production kommunikál a OUTBOUND -> Order -> Produce (if OK, then disappear)
//FINISHGOOD Lekérdezed -> mennyiség alapján Send to Customer-t alkalmazol.
const OutboundList = () => {
 //CREATE FETCH REQUEST FOR THE DB, CHECK USER ROLE, IF ROLE IS WRONG NAVIGATE TO THE "NO AUTHORIZATION" PAGE
     const [loading, setLoading] = useState(false)
     const [outboundComponents, setOutboundComponents] = useState([])
     const [checkToShip, setCheckToShip] = useState(false)
     //Check order of keys if non-working
     const [formdata, setFormData] = useState({
      "quantity": '',
      "productDesignation": ''
    });

     const handleInputChange = (e) => {
      setFormData({ ...formdata, [e.target.name]: e.target.value });
    }
    const fetchOutboundComponentsTest = () => {
      return new Promise((resolve) => {
          // Simulating a delay to mimic an async operation
          setTimeout(() => {
              resolve([
                  { ProductDesignation: "Something", quantity: 1000 },
                  { ProductDesignation: "Something2", quantity: 1000 },
              ]);
          }, 1000);
      });
  };
  const handleOutboundFetch = () => {
    setLoading(true);
    fetchOutboundComponentsTest()
        .then((components) => {
            if (components && components.length > 0) {
                setOutboundComponents(components);
            } else {
                console.error("Error fetching components");
            }
        })
        .catch((error) => {
            console.error("Error fetching components:", error);
        })
        .finally(() => {
            setLoading(false);
            console.log(outboundComponents);
        });
};
     
     const handleShipping = async(productDesignation, quantity) =>{
      //fetchShipItem(formdata)
      console.log(productDesignation)
      console.log(quantity)
     }
 
 
     useEffect(()=>{
        handleOutboundFetch()
     },[])
 
     return (
      <>{loading ? (<Loading />) :
        (<OutboundTable
        outboundComponents = {outboundComponents}
        handleShipping = {handleShipping}
        handleInputChange={handleInputChange}
        />)}
      </>

     )
}

export default OutboundList