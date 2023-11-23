import React, { useEffect, useState } from 'react'
import { Link } from "react-router-dom";


function InboundTable({ inboundComponents, handleStore, nextOrderList }) {

  return (
    <div className="InboundTable">
      <table>
        <thead>
          <tr>
            <th>Product Designation</th>
            <th>Quantity</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {inboundComponents && inboundComponents.map(({ prodDes, quantity }) => (
            <tr>
              <td>{prodDes}</td>
              <td>{quantity}</td>
              <td>
                <button type="button" onClick={() => handleStore(quantity, prodDes)}>Send to Storage</button>
              </td>
              <td>
                <button type="button" onClick={() => nextOrderList()}>Next Order List</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}

export default InboundTable