using Inventory_Management_System.Contracts;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Service.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Customer Planner, Admin")]
    public class CustomerPlannerController : ControllerBase
    {
        private readonly IStock _stockService;
        private readonly ISupplier _supplierService;

        public CustomerPlannerController(IStock stockService, ISupplier supplierService)
        {
            _stockService = stockService;
            _supplierService = supplierService;
        }

        [HttpPost("OrderComponent")]
        public async Task<IActionResult> OrderComponent([FromBody] ComponentOrderRequest request)
        {
            try
            {
                if (request.Quantity <= 0)
                {
                    return BadRequest("Requested quantity can not be zero or negative!");
                }
                // might need a look into the generic type
                if (Enum.TryParse(typeof(ProductDesignation), request.ProductDesignation, out object parsedValue))
                {
                    await _supplierService.CreateRawMaterialAsync(request.Quantity, (ProductDesignation)parsedValue);
                    return Ok($"{request.Quantity} pcs of {request.ProductDesignation} successfully ordered from supplier. Raw material locations will be filled accordingly.");
                }
                else
                {
                    return BadRequest("Incorrect productDesignation!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
