import React, { useState } from 'react'

function OutboundTable({ productionStock, handleOutbound }) {

  //Ezeket nem töröltem ki mert hasznos lehet, a handleOptionAndChangeForQuantity képes option-ökhez hozzácsatolni dolgokat.
  /*   const [selectedQuantityOption, setSelectedQuantityOption] = useState('');
  
    useEffect(() => {
      console.log(selectedQuantityOption)
    }, [selectedQuantityOption])
  
    useEffect(() => {
      console.log(OutboundoundComponents)
    }, [])
  
    //this won't work with the current setup
    const handleOptionAndChangeForQuantity = (eTargetValue) => {
      setSelectedQuantityOption(eTargetValue.target.value);
      handleInputChange(eTargetValue);
    } */
  /* 
    const [selectedQuantities, setSelectedQuantities] = useState({});
  
    const handleOptionAndChangeForQuantity = (component, selectedValue) => {
      setSelectedQuantities((prevSelectedQuantities) => ({
        ...prevSelectedQuantities,
        [component.ProductDesignation]: selectedValue,
      })); */

  // If you need to propagate the change to the parent component
  //handleInputChange({ target: { name: 'quantity', value: selectedValue } });
  //};

  return (
    <div className="Table">
      <table>
        <thead>
          <tr>
            <th>Product Designation</th>
            <th>Quantity</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {productionStock.map((component) => (
            <tr key={component.locationName}>
              <td>
                <p value={component.locationName}>{component.locationName}</p>
              </td>
              <td>
                <p placeholder='Quantity' name="quantity" value={component.quantity}>{component.quantity}</p>
              </td>
              <td>
                <button type="button" onClick={() => handleOutbound(component.locationName, component.quantity)}>Collect</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div >
  )
}

export default OutboundTable