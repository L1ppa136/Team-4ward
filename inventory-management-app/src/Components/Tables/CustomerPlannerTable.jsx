import React, { useEffect, useState } from 'react'

function CustomerPlannerTable({ handleInputChange, createComponents }) {

  const [selectedDesignationOption, setSelectedDesignationOption] = useState('');
  const [selectedQuantityOption, setSelectedQuantityOption] = useState('');

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
                <option value="2000">2000</option>
                <option value="4000">4000</option>
                <option value="6000">6000</option>
              </select>
            </td>
            <td>
              <button type="button" onClick={() => createComponents()}>Order Components</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div >
  )
}

export default CustomerPlannerTable