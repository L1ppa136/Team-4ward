using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Location;
using Inventory_Management_System.Service.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogisticsController : ControllerBase
    {
        private readonly IStock _stockService;
        private readonly IProduction _productionService;
        private readonly ISupplier _supplierService;

        public LogisticsController(IStock stockService, IProduction productionService, ISupplier supplierService)
        {
            _stockService = stockService;
            _productionService = productionService;
            _supplierService = supplierService;
        }

        [HttpGet("Outbound")]
        public async Task<IActionResult> GetAllOutbound() 
        {
            List<OutboundLocation> outboundLocations = await _stockService.GetEmptyFinishedGoodLocationsAsync();
            return Ok(outboundLocations);
        }

        [HttpPost("OrderComponent")]
        public async Task<IActionResult> OrderComponent([Required]int quantity, [Required] string productDesignation)
        {
            // might need a look into the generic type
            if(Enum.TryParse(typeof(ProductDesignation), productDesignation, out object parsedValue))
            {
                ProductDesignation parsedString = (ProductDesignation)parsedValue;
                await _supplierService.CreateRawMaterialAsync(quantity, parsedString);
                return Ok(productDesignation);
            }
            else
            {
                return BadRequest("Incorrect productDesignation added!");
            }
        }

        [HttpGet("CreateLocations")]
        public IActionResult Create()
        {
            _stockService.CreateStorageLocations();

            Console.WriteLine();
            
            return Ok();
        }
    }
}
        