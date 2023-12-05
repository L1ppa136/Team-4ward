import React, { useEffect, useState } from 'react'

function OutboundTable({ handleInputChange, outboundComponents, handleShipping }) {

/*   const [selectedQuantityOption, setSelectedQuantityOption] = useState('');

  useEffect(() => {
    console.log(selectedQuantityOption)
  }, [selectedQuantityOption])

  useEffect(() => {
    console.log(outboundComponents)
  }, [])

  //this won't work with the current setup
  const handleOptionAndChangeForQuantity = (eTargetValue) => {
    setSelectedQuantityOption(eTargetValue.target.value);
    handleInputChange(eTargetValue);
  } */

  const [selectedQuantities, setSelectedQuantities] = useState({});

  const handleOptionAndChangeForQuantity = (component, selectedValue) => {
    setSelectedQuantities((prevSelectedQuantities) => ({
      ...prevSelectedQuantities,
      [component.ProductDesignation]: selectedValue,
    }));

    // If you need to propagate the change to the parent component
    //handleInputChange({ target: { name: 'quantity', value: selectedValue } });
  };

  return (
    <div className="OutboundTable">
      <table>
        <thead>
          <tr>
            <th>Product Designation</th>
            <th>Quantity</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {outboundComponents && outboundComponents.map((component) => (
            <tr key={component.ProductDesignation}>
              <td>
                <p value={component.ProductDesignation}>{component.ProductDesignation}</p>
              </td>
              <td>
                <select 
                placeholder='Quantity' 
                name="quantity"
                value={selectedQuantities[component.ProductDesignation] || ''}
                onChange={(e) => handleOptionAndChangeForQuantity(component, e.target.value)}>
                  <option value="" disabled hidden>Select your option</option>
                  {Array.from({ length: component.quantity / 200 }, (_, index) => (
                    <option key={index} value={(index + 1) * 200}>
                      {(index + 1) * 200}
                    </option>
                  ))}
                </select>
              </td>
              <td>
                <button type="button" onClick={() => handleShipping(component.ProductDesignation, selectedQuantities[component.ProductDesignation])}>Ship to Customer</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div >
  )
}

export default OutboundTable