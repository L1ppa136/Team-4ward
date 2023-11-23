import React from 'react'

function OutboundTable({outboundComponents, handleShipping}) {
  return (

    //Copied, TBD
    <div className="OutboundTable">
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
        {outboundComponents && outboundComponents.map((component)=>(
          <tr key={component.id}>
            <td>{component.ProductDesignation}</td>
            <td>{component.CreatedAt}</td>
            <td>{component.PartNumber}</td>
            <td>
              <button type="button" onClick={() => handleShipping(component.id)}>Ship to Customer</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  </div>
  )
}

export default OutboundTable