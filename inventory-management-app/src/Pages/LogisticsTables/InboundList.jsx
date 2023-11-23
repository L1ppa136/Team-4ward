import React, { useState, useEffect } from 'react'
import InboundTable from "../../Components/Tables/InboundTable"
import Loading from "../../Components/Loading"
import "./Table.css";
import axios from 'axios';


const fetchStoreComponents = async (orderItem) => {
    try {
        const response = await axios.post('http://localhost:5179/Logistics/OrderComponent', orderItem);
        return response.data;
    } catch (error) {
        throw error.response ? error.response.data : error;
    }
}


//Kellenek "ál" értékek a componentshez, amiket itt érdemes megkreálni, egy "Inspection status-t" hozzá adunk a componenthez, azt az inspectel megváltoztatjuk
// és ez engedélyezi a  

const InboundList = () => {
    const [loading, setLoading] = useState(true)
    const [inboundComponents, setInboundComponents] = useState({})





    const GenerateOrderList = (Quantity) => {
        return {
            Screw: 4 * Quantity,
            Nut: 4 * Quantity,
            Cushion: 1 * Quantity,
            Diffusor: 1 * Quantity,
            Retrainer: 1 * Quantity,
            Cover: 1 * Quantity,
            Emblem: 1 * Quantity,
            Inflator: 1 * Quantity,
            Wireharness: 1 * Quantity
        }
    }

    // betenni egy buttonre (következő store ciklus, ha jött infó a customer plannertől)
    const nextOrderList = () => {
        let CustomerOrder = localStorage.getItem("OrderList");
        let generatedList = GenerateOrderList(CustomerOrder.quantity);
        setInboundComponents(generatedList);
        console.log(CustomerOrder);
        localStorage.removeItem("OrderList");
        return inboundComponents;
    }


    //Ez a method kiveszi a beérkező componentseket és eltárolja őket az adott raktárba (RawMat).
    const handleStore = (quantity, productDesignation) => {
        var componentToFetch = { "quantity": quantity, "productDesignation": productDesignation };
        fetchStoreComponents(componentToFetch);
        removeItem(productDesignation);
    }

    const removeItem = (productDes) => {
        const updatedOrderList = { ...inboundComponents };
        delete updatedOrderList[productDes];
        setInboundComponents(updatedOrderList);
        console.log(updatedOrderList)
    };

    useEffect(() => {

    }, [inboundComponents]);

    if (loading) {
        return <Loading />;
    };

    return (
        <InboundTable
            inboundComponents={inboundComponents}
            handleCollect={handleStore}
            nextOrderList={nextOrderList}
        />
    )
}

export default InboundList