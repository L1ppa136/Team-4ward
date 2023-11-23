import React, { useEffect, useState } from 'react'
import { Link } from "react-router-dom";


function InboundTable({inboundComponents, handleCollect}) {

  return (
    <div className="InboundTable">
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
          {inboundComponents && inboundComponents.map((component)=>(
            <tr key={component.id}>
              <td>{component.ProductDesignation}</td>
              <td>{component.CreatedAt}</td>
              <td>{component.PartNumber}</td>
              <td>
                <button type="button" onClick={()=> handleCollect(component.id)}>Send to Storage</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}

export default InboundTable