import React, { useEffect, useState } from 'react'
import { Link } from "react-router-dom";


function InboundTable({ inboundComponents, handleStore, nextOrderList }) {

  //Ha üres az InboundComponents, akkor megjeleníti a gombot amivel lekérhetünk újra dolgokat.
  if(inboundComponents.length === 0){
    return(
    <div>
    <button className='orderButton' type="button" onClick={() => nextOrderList()}>Next Order List</button>
    </div>
    )
    
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
          {inboundComponents && inboundComponents.map((component) => (
            <tr key={component.key}>
              <td>{component.key}</td>
              <td>{component.value}</td>
              <td>
                <button type="button" onClick={() => handleStore(component.value, component.key)}>Send to Storage</button>
              </td>
            </tr>
             ))}
        </tbody>
      </table>
    </div>
  )
}

export default InboundTable