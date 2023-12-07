import React, { useEffect, useState } from 'react';
import ShipTable from '../../Components/Tables/ShipTable';
import Loading from "../../Components/Loading";
import axios from 'axios';
import "./Table.css";

const fetchFinishedGoodStockFromWarehouse = async () => {
    try {
        const response = await axios.get('/ForkliftDriver/GetFinishedGoodStock');
        return response.data;
    } catch (error) {
        throw error.response ? error.response.data : error;
    }
}

const fetchShipItem = async (formdata) => {
    try {
        let response = await axios.get("/ForkliftDriver/MoveFinishedGoodsFromProduction", formdata)
        return response.data;
    } catch (error) {
        throw error.response ? error.response.data : error;
    }
}

//KÉSZ levő dolgok Production kommunikál a Ship -> Order -> Produce (if OK, then disappear)
//FINISHGOOD Lekérdezed -> mennyiség alapján Send to Customer-t alkalmazol.

//Collect -> Move To FinishedGood warehouse Ship -> Move Out of  (RENAME THIS TO SHIPPING!  )
const ShipList = () => {
    //CREATE FETCH REQUEST FOR THE DB, CHECK USER ROLE, IF ROLE IS WRONG NAVIGATE TO THE "NO AUTHORIZATION" PAGE
    const [loading, setLoading] = useState(false)
    const [shipFinishedGoods, setShipFinishedGoods] = useState([])
    //Check order of keys if non-working
    const [formdata, setFormData] = useState({
        "quantity": '',
        "productDesignation": 'Airbag'
    });

    const handleInputChange = (e) => {
        setFormData({ ...formdata, [e.target.name]: e.target.value });
    }

    const handleShipFetch = () => {
        setLoading(true);
        fetchFinishedGoodStockFromWarehouse()
            .then((components) => {
                if (components && components.length > 0) {
                    setShipFinishedGoods(components.filter(component => component.productDesignation === 'Airbag'));
                } else {
                    console.error("Error fetching components");
                }
            })
            .catch((error) => {
                console.error("Error fetching components:", error);
            })
            .finally(() => {
                setLoading(false);
                console.log(shipFinishedGoods);
            });
    };

    const handleShipping = async (productDesignation, quantity) => {
        await fetchShipItem(formdata)
        console.log(productDesignation)
        console.log(quantity)
    }

    useEffect(() => {
        handleShipFetch()
    }, [])

    return (
        <>{loading ? (<Loading />) :
            (<ShipTable
                shipFinishedGoods={shipFinishedGoods}
                handleShipping={handleShipping}
                handleInputChange={handleInputChange}
            />)}
        </>

    )
}

export default ShipList