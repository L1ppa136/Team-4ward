import React, { useEffect, useState } from 'react';
import OutboundTable from '../../Components/Tables/OutboundTable';
import Loading from "../../Components/Loading";
import axios from 'axios';
import "./Table.css";

const fetchOutboundComponents = async () => {
    try {
        const response = await axios.get('/ForkliftDriver/GetProductionStock');
        return response.data;
    } catch (error) {
        throw error.response ? error.response.data : error;
    }
}

const fetchOutboundItem = async (formdata) => {
    let response = await axios.post("/ForkliftDriver/MoveFinishedGoodsFromProduction", "OutboundpedMaterialsFile")
}
//This list collects thing from Production Location to store in the FinishedGoodsWarhouse
//Collect -> Move To FinishedGood warehouse Outbound -> Move Out of  (RENAME THIS TO OutboundPING!  )
const OutboundList = () => {
    //CREATE FETCH REQUEST FOR THE DB, CHECK USER ROLE, IF ROLE IS WRONG NAVIGATE TO THE "NO AUTHORIZATION" PAGE
    const [loading, setLoading] = useState(false)
    const [outboundComponents, setOutboundComponents] = useState([])
    const [checkToOutbound, setCheckToOutbound] = useState(false)
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

    const handleOutboundping = async (productDesignation, quantity) => {
        //fetchOutboundItem(formdata)
        console.log(productDesignation)
        console.log(quantity)
    }


    useEffect(() => {
        handleOutboundFetch()
    }, [])

    return (
        <>{loading ? (<Loading />) :
            (<OutboundTable
                outboundComponents={outboundComponents}
                handleOutboundping={handleOutboundping}
                handleInputChange={handleInputChange}
            />)}
        </>

    )
}

export default OutboundList