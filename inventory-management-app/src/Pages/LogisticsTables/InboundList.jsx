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

const InboundList = () => {
    const [inboundComponents, setInboundComponents] = useState([])

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

    const nextOrderList = async () => {
        let CustomerOrder = localStorage.getItem("OrderList");
        let CustomerOrderObject = await JSON.parse(CustomerOrder)

        if (CustomerOrderObject && CustomerOrderObject.quantity !== null) {
            let generatedList = GenerateOrderList(CustomerOrderObject.quantity);
            let keyValueArr = Object.entries(generatedList).map(([key, value])=>{
                console.log(key, value)
                return {key, value}
            })
            setInboundComponents(keyValueArr);
            console.log("Components here!" + inboundComponents.map((comp)=>console.log(comp)));
            localStorage.removeItem("OrderList");
            return inboundComponents;
        }
        return console.log("No quantity in Order")
    }

    //NEM MÜKÖDIK
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
        console.log("HERE", inboundComponents);
      }, [inboundComponents]);

    useEffect(() => {
    }, []);


    return (
        <InboundTable
            inboundComponents={inboundComponents}
            handleStore={handleStore}
            nextOrderList={nextOrderList}
        />
    )
}

export default InboundList