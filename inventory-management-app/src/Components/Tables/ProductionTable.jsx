import React, { useEffect, useState } from 'react'


function ProductionTable({ handleInputChange, handleProduce, handleOrderingFromWarehouse }) {

  const [selectedDesignationOption, setSelectedDesignationOption] = useState('');
  const [selectedQuantityOption, setSelectedQuantityOption] = useState('');
  const [hasBeenOrdered, setHasBeenOrdered] = useState(false);

  useEffect(() => {
    console.log(selectedQuantityOption)
  }, [selectedQuantityOption])

  const handleOptionAndChangeForQuantity = (eTargetValue) => {
    setSelectedQuantityOption(eTargetValue.target.value);
    handleInputChange(eTargetValue);
  }

  const handleOptionAndChangeForDesignation = (eTargetValue) => {
    setSelectedDesignationOption(eTargetValue.target.value);
    handleInputChange(eTargetValue);
  }

  const handleOrder = () => {
    handleOrderingFromWarehouse();
    setHasBeenOrdered(true);
  }

  return (
    <div className="ProductionTable">
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
                <option value="400">400</option>
                <option value="600">600</option>
              </select>
            </td>
            <td>
              <>{!hasBeenOrdered ? (<button type="button" onClick={() => handleOrder()}>Order Components</button>) : (<button type="button" onClick={() => handleProduce()}>Produce</button>)}</>
            </td>
          </tr>
        </tbody>
      </table>
    </div >
  )
}

export default ProductionTable