import React from 'react'

function ProdSupplyTable({ goodsToSupply, handleStore, nextOrderList }) {

  if (goodsToSupply.length === 0) {
    return (
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
          {goodsToSupply && goodsToSupply.map((component) => (
            <tr key={component.key}>
              <td>{component.key}</td>
              <td>{component.value}</td>
              <td>
                <button type="button" onClick={() => handleStore(component.value, component.key)}>Collect & Deliver</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}

export default ProdSupplyTable