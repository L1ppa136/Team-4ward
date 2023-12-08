import React, { useState, useEffect } from 'react'
import InboundTable from "../../Components/Tables/InboundTable"
import "./Table.css";
import axios from 'axios';

const fetchStoreComponents = async (orderItem) => {
    try {
        const response = await axios.post('api/ForkliftDriver/StoreComponent', orderItem);
        return response.data;
    } catch (error) {
        throw error.response ? error.response.data : error;
    }
}

const InboundList = () => {


    const [inboundComponents, setInboundComponents] = useState([])
    const [Loading, setLoading] = useState(false)

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
        let CustomerOrder = localStorage.getItem("OrderList");
        let CustomerOrderObject = await JSON.parse(CustomerOrder)

        if (CustomerOrderObject && CustomerOrderObject.quantity !== null) {
            let generatedList = GenerateOrderList(CustomerOrderObject.quantity);
            let keyValueArr = Object.entries(generatedList).map(([key, value]) => {
                console.log(key, value)
                return { key, value }
            })
            setInboundComponents(keyValueArr);
            console.log("Components here!" + inboundComponents.map((comp) => console.log(comp)));
            localStorage.removeItem("OrderList");
            return inboundComponents;
        }
        return console.log("No quantity in Order")
    }

    //NEM MÜKÖDIK

    const handleStore = async (quantity, productDesignation) => {
        if (quantity !== null && productDesignation !== null) {
            var componentToFetch = { "quantity": quantity, "productDesignation": productDesignation };
            await fetchStoreComponents(componentToFetch);
            removeItem(productDesignation);
        }
    }

    const removeItem = (productDes) => {
        setLoading(true);
        setInboundComponents(prevState => prevState.filter(item => item.key !== productDes));
        setLoading(false);
    };

    useEffect(() => {
        console.log("inboundComponents HERE", inboundComponents);
    }, [inboundComponents]);

    useEffect(() => {
    }, []);


    return (

        <>{Loading ? (<Loading />) : (<InboundTable
            inboundComponents={inboundComponents}
            handleStore={handleStore}
            nextOrderList={nextOrderList}
        />)}
        </>
    )
}

export default InboundList