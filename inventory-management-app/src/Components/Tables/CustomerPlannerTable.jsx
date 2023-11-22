import React from 'react'

function CustomerPlannerList(productionPlans,handleOrder) {
    return (

        //Copied, TBD
        <div className="OutboundTable">
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
            {productionPlans.map((component)=>(
              <tr key={component.id}>
                <td>{component.ProductDesignation}</td>
                <td>{component.CreatedAt}</td>
                <td>{component.PartNumber}</td>
                <td>
                  <button type="button" onClick={() => handleOrder(component.id)}>Ship to Customer</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      )
}

export default CustomerPlannerList