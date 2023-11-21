import React from 'react'

function ProductionTable({orderableGoods, handleOrder}) {
  return (
    <div className="ProductionTable">
    <table>
      <thead>
        <tr>
          <th>Good</th>
          <th>Quantity</th>
          <th>Order</th>
        </tr>
      </thead>
      <tbody>
        {orderableGoods.map((good)=>(
          <tr key={good.id}>
            <td>{good.ProductDesignation}</td>
            <td>dropdownmenuhere</td>
            <td>
              <button type="button" onClick={()=> handleOrder(good.id)}>Order</button> 
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  </div>
  )
}

export default ProductionTable