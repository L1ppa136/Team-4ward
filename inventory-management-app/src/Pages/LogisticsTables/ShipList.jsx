import React, { useEffect, useState } from 'react';
import ShipTable from '../../Components/Tables/ShipTable';
import Loading from "../../Components/Loading";
import axios from 'axios';
import "./Table.css";

const fetchShipComponents = async () => {
    try {
        const response = await axios.get('/ForkliftDriver/GetProductionStock');
        return response.data;
    } catch (error) {
        throw error.response ? error.response.data : error;
    }
}

const fetchShipItem = async (formdata) => {
    let response = await axios.post("/ForkliftDriver/MoveFinishedGoodsFromProduction", "shippedMaterialsFile")
}
//KÉSZ levő dolgok Production kommunikál a Ship -> Order -> Produce (if OK, then disappear)
//FINISHGOOD Lekérdezed -> mennyiség alapján Send to Customer-t alkalmazol.

//Collect -> Move To FinishedGood warehouse Ship -> Move Out of  (RENAME THIS TO SHIPPING!  )
const ShipList = () => {
    //CREATE FETCH REQUEST FOR THE DB, CHECK USER ROLE, IF ROLE IS WRONG NAVIGATE TO THE "NO AUTHORIZATION" PAGE
    const [loading, setLoading] = useState(false)
    const [shipComponents, setShipComponents] = useState([])
    const [checkToShip, setCheckToShip] = useState(false)
    //Check order of keys if non-working
    const [formdata, setFormData] = useState({
        "quantity": '',
        "productDesignation": ''
    });

    const handleInputChange = (e) => {
        setFormData({ ...formdata, [e.target.name]: e.target.value });
    }
    const fetchShipComponentsTest = () => {
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
    const handleShipFetch = () => {
        setLoading(true);
        fetchShipComponentsTest()
            .then((components) => {
                if (components && components.length > 0) {
                    setShipComponents(components);
                } else {
                    console.error("Error fetching components");
                }
            })
            .catch((error) => {
                console.error("Error fetching components:", error);
            })
            .finally(() => {
                setLoading(false);
                console.log(shipComponents);
            });
    };

    const handleShipping = async (productDesignation, quantity) => {
        //fetchShipItem(formdata)
        console.log(productDesignation)
        console.log(quantity)
    }


    useEffect(() => {
        handleShipFetch()
    }, [])

    return (
        <>{loading ? (<Loading />) :
            (<ShipTable
                shipComponents={shipComponents}
                handleShipping={handleShipping}
                handleInputChange={handleInputChange}
            />)}
        </>

    )
}

export default ShipList