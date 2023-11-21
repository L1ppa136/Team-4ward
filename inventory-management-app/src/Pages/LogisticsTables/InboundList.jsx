import React, { useState, useEffect, Component } from 'react'
import InboundTable from "../../Components/Tables/InboundTable"
import Loading from "../../Components/Loading" 
//CreateFetch Request w/ Axios for the DB, Check role, if role is wrong, navigate to the "NoAuthorization" page.

const fetchInboundComponents = async () =>{
    //empty
}

//Kellenek "ál" értékek a componentshez, amiket itt érdemes megkreálni, egy "Inspection status-t" hozzá adunk a componenthez, azt az inspectel megváltoztatjuk
// és ez engedélyezi a 

const InboundList = () => {
    const [loading, setLoading] = useState(true)
    const [inboundComponents, setInboundComponents] = useState([])

    const handleInboundFetch = async() =>{
        const components = await fetchInboundComponents();
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