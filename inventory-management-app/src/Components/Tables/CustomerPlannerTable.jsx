import React,{useEffect, useState} from 'react'

function CustomerPlannerList({customerDemands,handleOrder, handleInputChange}) {
  
  const [selectedOption, setSelectedOption] = useState('');

  useEffect(()=>{
    console.log(selectedOption)
  },[selectedOption])

    return (
        <div className="CustomerPlannerTable">
        <table>
          <thead>
            <tr>
              <th>Product Designation</th>
              <th>Quantity</th>
              <th>Part Number</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
            {customerDemands && customerDemands.map((component)=>(
              <tr key={component.id}>
                <td>{component.ProductDesignation}</td>
                <td>
                  <select id="dropdown" name="Quantity" value={selectedOption} onChange={handleInputChange}>
                    <option value="200">200</option>
                    <option value="400">400</option>
                    <option value="600">600</option>
                  </select>
                </td>
                <td>{component.PartNumber}</td>
                <td>
                  <button type="button" onClick={() => handleOrder(component.ProductDesignation)}>Ship to Customer</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      )
}

export default CustomerPlannerList