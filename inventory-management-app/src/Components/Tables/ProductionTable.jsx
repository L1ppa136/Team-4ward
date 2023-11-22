import React from 'react'

function ProductionTable({productionPlans, handleProductionOrder}) {
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
        {productionPlans.map((plan)=>(
          <tr key={plan.Name}>
            <td>{plan.ProductDesignation}</td>
            <td>dropdownmenuhere</td>
            <td>
              <button type="button" onClick={()=> handleProductionOrder(plan.id)}>Order</button> 
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  </div>
  )
}

export default ProductionTable