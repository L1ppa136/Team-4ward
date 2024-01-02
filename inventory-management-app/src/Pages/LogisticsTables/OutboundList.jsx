import React, { useEffect, useState } from 'react';
import OutboundTable from '../../Components/Tables/OutboundTable';
import Loading from "../../Components/Loading";
import axios from 'axios';
import "./Table.css";

const fetchProductionStock = async () => {
    try {
        const response = await axios.get('api/ForkliftDriver/GetProductionStock');
        return response.data;
    } catch (error) {
        throw error.response ? error.response.data : error;
    }
}

// Finishing endpoint
const fetchSendProductionToWarehouse = async () => {
    try {
        let response = await axios.get("api/ForkliftDriver/MoveFinishedGoodsFromProduction")
        return response.data
    } catch (error) {
        throw error.response ? error.response.data : error;
    }
}

//This list collects thing from Production Location to store in the FinishedGoodsWarhouse
//Collect -> Move To FinishedGood warehouse Outbound -> Move Out of  (RENAME THIS TO Outbound!  )
const OutboundList = () => {
    //CREATE FETCH REQUEST FOR THE DB, CHECK USER ROLE, IF ROLE IS WRONG NAVIGATE TO THE "NO AUTHORIZATION" PAGE
    const [loading, setLoading] = useState(false);
    const [productionStock, setProductionStock] = useState([]);
    //Check order of keys if non-working

    const handleOutboundFetch = () => {
        setLoading(true);
        fetchProductionStock()
            .then((components) => {
                if (components && components.length > 0) {
                    setProductionStock(components.filter(location => location.locationName === 'Airbag'));
                } else {
                    console.error("Error fetching components");
                }
            })
            .catch((error) => {
                console.error("Error fetching components:", error);
            })
            .finally(() => {
                setLoading(false);
                console.log(productionStock);
            });
    };

    const handleOutbound = async (productDesignation, quantity) => {
        setLoading(true);
        if (quantity <= 0) {
            window.alert("Not enough good on stock!")
        } else {
            await fetchSendProductionToWarehouse()
            setProductionStock([])
            window.alert(`${quantity} finished good has been delivered to the Warehouse`)
        }
        setLoading(false)
        console.log(productDesignation)
        console.log(quantity)
    }

    useEffect(() => {
        handleOutboundFetch()
    }, [])

    return (
        <>{loading ? (<Loading />) :
            (<OutboundTable
                productionStock={productionStock}
                handleOutbound={handleOutbound}
            />)}
        </>

    )
}

export default OutboundList