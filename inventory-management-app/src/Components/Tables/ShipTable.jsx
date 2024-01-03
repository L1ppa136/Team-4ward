import React, { useEffect, useState } from 'react'

function ShipTable({ handleInputChange, shipFinishedGoods, handleShipping }) {

  const [selectedQuantityOption, setSelectedQuantityOption] = useState('');

  //this won't work with the current setup
  const handleOptionAndChangeForQuantity = (e) => {
    setSelectedQuantityOption(e.target.value);
    handleInputChange(e);
  }

  useEffect(()=>{
    console.log(selectedQuantityOption);
  },[])

  /* const [selectedQuantities, setSelectedQuantities] = useState({});

  const handleOptionAndChangeForQuantity = (shipFinishedGoods, selectedValue) => {
    setSelectedQuantities((prevSelectedQuantities) => ({
      ...prevSelectedQuantities,
      [shipFinishedGoods.name]: selectedValue,
    }));

    // If you need to propagate the change to the parent shipFinishedGoods
    //handleInputChange({ target: { name: 'quantity', value: selectedValue } });
  };
 */
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
          {shipFinishedGoods && 
            <tr key={shipFinishedGoods.name}>
              <td>
                <p value={shipFinishedGoods.name}>{shipFinishedGoods.name}</p>
              </td>
              <td>
                <select
                  placeholder='Quantity'
                  name="quantity"
                  value={shipFinishedGoods ? selectedQuantityOption : 0}
                  onChange={(e) => handleOptionAndChangeForQuantity(e)}>
                  <option value="" disabled hidden>Select your option</option>
                  {Array.from({ length: shipFinishedGoods.quantity / 200 }, (_, index) => (
                    <option key={index} value={(index + 1) * 200}>
                      {(index + 1) * 200}
                    </option>
                  ))}
                </select>
              </td>
              <td>
                <button type="button" onClick={() => handleShipping(shipFinishedGoods.name, selectedQuantityOption)}>Ship to Customer</button>
              </td>
            </tr>
          }
        </tbody>
      </table>
    </div >
  )
}

export default ShipTable