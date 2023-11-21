import React from 'react'

function ProdSupplyTable() {
  return (

    //Copied, TBD
    <div className="ProdSupplyTable">
    <table>
      <thead>
        <tr>
          <th>Product Designation</th>
          <th>Time of Arrival</th>
          <th>Part Number</th>
          <th>Inspection Procedure</th>
        </tr>
      </thead>
      <tbody>
        {inboundComponents.map((component)=>(
          <tr key={component.id}>
            <td>{component.ProductDesignation}</td>
            <td>{component.CreatedAt}</td>
            <td>{component.PartNumber}</td>
            <td>
              {component.inspection ? 
              <button type="button" onClick={()=> handleDelivery(component.id)}>Deliver</button> 
              :
              <button type="button" onClick={() => inspectComponent(component.id)}>Inspect</button>}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  </div>
  )
}

export default ProdSupplyTable