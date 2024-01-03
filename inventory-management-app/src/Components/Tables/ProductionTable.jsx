import React, { useEffect, useState } from 'react'


function ProductionTable({ handleInputChange, handleProduce, handleOrderingFromWarehouse }) {

  const [selectedDesignationOption, setSelectedDesignationOption] = useState('');
  const [selectedQuantityOption, setSelectedQuantityOption] = useState('');
  const [hasBeenOrdered, setHasBeenOrdered] = useState(false);

  useEffect(() => {
    console.log(selectedQuantityOption)
  }, [selectedQuantityOption])

  const handleOptionAndChangeForQuantity = (event) => {
    setSelectedQuantityOption(event.target.value);
    handleInputChange(event);
  }

  const handleOptionAndChangeForDesignation = (event) => {
    setSelectedDesignationOption(event.target.value);
    handleInputChange(event);
  }

  const handleOrder = () => {
    handleOrderingFromWarehouse(selectedQuantityOption, selectedDesignationOption);
    setHasBeenOrdered(true);
  }

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
          <tr>
            <td>
              <select placeholder='Designation' name="productDesignation" value={selectedDesignationOption} onChange={(e) => handleOptionAndChangeForDesignation(e)}>
                <option value="" disabled selected>Select your option</option>
                <option value="Airbag">Airbag</option>
              </select>
            </td>
            <td>
              <select placeholder='Quantity' name="quantity" value={selectedQuantityOption} onChange={(e) => handleOptionAndChangeForQuantity(e)}>
                <option value="" disabled selected hidden>Select your option</option>
                <option value="200">200</option>
                <option value="4000">4000</option>
                <option value="6000">6000</option>
              </select>
            </td>
            <td>
              <>{!hasBeenOrdered ? (<button type="button" onClick={() => handleOrder()}>Order Components</button>) : (<button type="button" onClick={() => handleProduce(selectedQuantityOption, selectedDesignationOption)}>Produce</button>)}</>
            </td>
          </tr>
        </tbody>
      </table>
    </div >
  )
}

export default ProductionTable