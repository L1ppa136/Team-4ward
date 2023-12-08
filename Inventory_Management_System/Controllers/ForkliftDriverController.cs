using Inventory_Management_System.Contracts;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Service.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//Render
namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Forklift Driver, Warehouse Leader, Admin")]
    public class ForkliftDriverController : ControllerBase
    {
        private readonly IStock _stockService;
        private readonly ISupplier _supplierService;

        public ForkliftDriverController(IStock stockService, ISupplier supplierService)
        {
            _stockService = stockService;
            _supplierService = supplierService;
        }

        [HttpPost("StoreComponent")]
        public async Task<IActionResult> StoreComponent([FromBody] OrderRequest request)
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

        [HttpPost("MoveComponentToProduction")]
        public async Task<IActionResult> ComponentToProduction([FromBody] OrderRequest request)
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
                    var result = await _stockService.MoveRawMaterialToProductionAsync((ProductDesignation)parsedValue, request.Quantity);
                    if (result.Success)
                    {
                        return Ok(result.Message);

                    }
                    else
                    {
                        return NotFound(result.Message);
                    }
                }
                else
                {
                    return BadRequest("Incorrect productDesignation!");
                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("MoveFinishedGoodsFromProduction")]
        public async Task<IActionResult> MoveFinishedGoodsFromProduction()
        {
            try
            {
                var result = await _stockService.MoveFinishedGoodToOutboundAsync();
                if(result.Success)
                {
                    return Ok(result.Message);
                }
                else
                {
                    return BadRequest(result.Message);
                }
                              

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Deliver")]
        public async Task<IActionResult> DeliverToCustomer([FromBody] OrderRequest request)
        {
            try
            {
                if (request.Quantity <= 0)
                {
                    return BadRequest("Invalid quantity, must be greater than zero.");
                }

                var result = await _stockService.DeliverFinishedGoodsToCustomer(request.Quantity);

                if (result.Success)
                {
                    return Ok(result.Message);
                }
                else
                {
                    return BadRequest(result.Message);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET all finishedGoodStock
        [HttpGet("GetFinishedGoodStock")]
        public async Task<IActionResult> GetAllFinishedGoodStock()
        {
            return Ok(await _stockService.GetFinishedGoodStockAsync());
        }

        //GET all componentStock
        [HttpGet("GetComponentStock")]
        public async Task<IActionResult> GetAllComponentStock()
        {
            return Ok(await _stockService.GetAllRawMaterialStockAsync());
        }


        //GET productionLocations (prod. stock)
        [HttpGet("GetProductionStock")]
        public async Task<IActionResult> GetProductionStock()
        {
            return Ok(await _stockService.GetAllProductionLocationsAsync());
        }
    }
}