import React from 'react'

function ProdSupplyTable({prodSupplyComponents, handleDelivery}) {
  return (

    //Copied, TBD
    <div className="ProdSupplyTable">
    <table>
      <thead>
        <tr>
          <th>Product Designation</th>
          <th>Time of Arrival</th>
          <th>Part Number</th>
          <th>Action</th>
        </tr>
      </thead>
      <tbody>
        {prodSupplyComponents && prodSupplyComponents.map((component)=>(
          <tr key={component.id}>
            <td>{component.ProductDesignation}</td>
            <td>{component.CreatedAt}</td>
            <td>{component.PartNumber}</td>
            <td>
              <button type="button" onClick={()=> handleDelivery(component.id)}>Collect & Deliver</button> 
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  </div>
  )
}

export default ProdSupplyTable