import React, { useState, useEffect} from 'react'
import InboundTable from "../../Components/Tables/InboundTable"
import Loading from "../../Components/Loading" 
import "./Table.css";

//CreateFetch Request w/ Axios for the DB, Check role, if role is wrong, navigate to the "NoAuthorization" page.

const fetchInboundComponents = async () =>{
        return console.log("juj")
}

//Kellenek "ál" értékek a componentshez, amiket itt érdemes megkreálni, egy "Inspection status-t" hozzá adunk a componenthez, azt az inspectel megváltoztatjuk
// és ez engedélyezi a  

const InboundList = () => {
    const [loading, setLoading] = useState(true)
    const [inboundComponents, setInboundComponents] = useState([])

    const handleInboundFetch = async() =>{
        let components = []
        //components = await fetchInboundComponents();
        var component = {id: 1, ProductDesignation: 1, CreatedAt: 11, PartNumber: 11}
        components.push(component)
        setInboundComponents(components);
        setLoading(false);
        console.log(inboundComponents);
    }

    //Ez a method kiveszi a beérkező componentseket és eltárolja őket az adott raktárba (RawMat).
    const handleCollect = async(id) =>{
        
    }

    useEffect(()=>{
        handleInboundFetch()
    },[])

    if (loading) {
        return <Loading />;
      };

    return (
    <InboundTable
    inboundComponents = {inboundComponents}
    handleCollect = {handleCollect}
    />
    )
}

export default InboundList