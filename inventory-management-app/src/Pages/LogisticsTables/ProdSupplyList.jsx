import React, {useState,useEffect} from 'react';
import ProdSupplyTable from '../../Components/Tables/ProdSupplyTable';
import Loading from '../../Components/Loading';
import "./Table.css";

const ProdSupplyList = () => {

  const [loading, setLoading] = useState(true)
  const [products, setProducts] = useState([])

  const handleProdSupplyFetch = async() =>{
      let products = []
      //components = await fetchInboundComponents();
      var component = {id: 1, ProductDesignation: 1, CreatedAt: 11, PartNumber: 11}
      products.push(component)
      setProducts(products);
      setLoading(false);
      console.log(products);
  }

  //Ez a method kiveszi a beérkező componentseket és eltárolja őket az adott raktárba (RawMat).
  const handleDelivery = async(id) =>{
      
  }

  useEffect(()=>{
      handleProdSupplyFetch()
  },[])

  if (loading) {
      return <Loading />;
    };

  return (
  <ProdSupplyTable
  prodSupplyComponents = {products}
  handleDelivery = {handleDelivery}
  />
  )
}

export default ProdSupplyList