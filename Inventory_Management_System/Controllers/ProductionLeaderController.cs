using Inventory_Management_System.Contracts;
using Inventory_Management_System.Service.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Production Leader, Admin")]
    public class ProductionLeaderController : ControllerBase
    {
        private readonly IStock _stockService;
        private readonly IProduction _production;

        public ProductionLeaderController(IStock stockService, IProduction production)
        {
            _stockService = stockService;
            _production = production;
        }

        //GET productionLocations (prod. stock)
        [HttpGet("GetProductionStock")]
        public async Task<IActionResult> GetProductionStock()
        {
            return Ok(await _stockService.GetAllProductionLocationsAsync());
        }

        [HttpPost("ProduceFinishedGoods")]
        public async Task<IActionResult> ProduceFinishedGoods([FromBody] OrderRequest request)
        {
            var productionResult = await _production.ProduceAsync(request.Quantity);

            if(productionResult.Success)
            {
                await _stockService.ClearUsedUpComponentStockAsync();
                return Ok(productionResult.Message);
            }
            else
            {
                return BadRequest(productionResult.Message);
            }
        }
    }
}
