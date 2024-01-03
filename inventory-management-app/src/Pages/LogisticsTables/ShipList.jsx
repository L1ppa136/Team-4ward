import React, { useEffect, useState } from 'react';
import ShipTable from '../../Components/Tables/ShipTable';
import Loading from "../../Components/Loading";
import axios from 'axios';
import "./Table.css";

const fetchFinishedGoodStockFromWarehouse = async () => {
    try {
        const response = await axios.get('api/ForkliftDriver/GetFinishedGoodStock');
        return response.data;
    } catch (error) {
        throw error.response ? error.response.data : error;
    }
}

const fetchShipItem = async (formdata) => {
    try {
        let response = await axios.post("api/ForkliftDriver/Deliver", formdata)
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
    const [shipFinishedGoods, setShipFinishedGoods] = useState()
    //Check order of keys if non-working
    const [formdata, setFormData] = useState({
        "quantity": '',
        "productDesignation": 'Airbag'
    });

    const handleInputChange = (e) => {
        setFormData({ ...formdata, [e.target.name]: e.target.value });
    }

    const handleShipFetch = async () => {
        setLoading(true);

        try {
            const components = await fetchFinishedGoodStockFromWarehouse();
            const goodsObj = await handleSummerize(components);

            if (goodsObj) {
                console.log("ITT AZ OBJECT", goodsObj);
                setShipFinishedGoods(goodsObj);
            } else {
                console.error("Error fetching components");
            }
        } catch (error) {
            console.error("Error fetching components:", error);
        } finally {
            setLoading(false);
            console.log(shipFinishedGoods);
        }
    };

    useEffect(() => {
        console.log(shipFinishedGoods)
    }, [shipFinishedGoods])

    const handleShipping = async (productDesignation, quantity) => {
        let object = {
            quantity: quantity,
            ProductDesignation: productDesignation
        }
        await fetchShipItem(object)
        window.alert(`${quantity} finished good has been shipped.`)
        handleShipFetch()
        console.log(productDesignation)
        console.log(quantity)
    }

    //This function is needed if there are two or more finished goods but it is too simple as i doesn't look at locations(This might be best handled in the backend)
    const handleCreationOfGoodsArr = async (rawComponents) => {
        const Arr = await rawComponents.reduce((accumlator, rawComponentBox) => {
            const existingBox = accumlator.find(item => item.partNumber === rawComponentBox.partNumber)
            if (existingBox) {
                existingBox.quantity += rawComponentBox.quantity
            } else {
                accumlator.push({ name: rawComponentBox.partNumber, quantity: rawComponentBox.quantity })
            }
            return accumlator;
        }, [])
        return Arr
    }

    const handleSummerize = async (components) => {
        let summQuantity = 0
        summQuantity = components.reduce((accumulator, location) => {
            const locationTotal = location.boxes.reduce((locationAccumulator, box) => {
                if (box.quantity && box.quantity > 0) {
                    locationAccumulator += box.quantity;
                    return locationAccumulator;
                }
            }, 0);
            accumulator += locationTotal;
            return accumulator;
        }, 0);
        console.log("Summ quantity" + summQuantity)
        let airbagObj = {
            name: "Airbag",
            quantity: summQuantity
        }
        return airbagObj
    }

    const handleSummerizeOld = async (components) => {
        let summQuantity = 0;
        summQuantity = await components.reduce(async (accumulator, location) => {
            const locationTotal = await location.boxes.reduce(async (locationAccumulator, box) => {
                if (box.quantity && box.quantity > 0) {
                    const currentLocationAccumulator = await locationAccumulator;
                    return currentLocationAccumulator + box.quantity;
                }
                return await locationAccumulator;
            }, Promise.resolve(0));

            accumulator += locationTotal;
            return accumulator;
        }, 0);
        console.log("Summ quantity" + summQuantity)


        let airbagObj = {
            name: "Airbag",
            quantity: summQuantity
        };

        return airbagObj;
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