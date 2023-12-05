using Inventory_Management_System.Service.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Production Leader, Admin")]
    public class ProductionLeaderController : ControllerBase
    {
        private readonly IStock _stockService;

        public ProductionLeaderController(IStock stockService)
        {
            _stockService = stockService;
        }

        //GET productionLocations (prod. stock)
        [HttpGet("GetProductionStock")]
        public async Task<IActionResult> GetProductionStock()
        {
            return Ok(await _stockService.GetAllProductionLocationsAsync());
        }
    }
}
