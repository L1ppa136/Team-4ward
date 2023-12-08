using Inventory_Management_System.Contracts;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Location;
using Inventory_Management_System.Service.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class WareHouseCreatorController : ControllerBase
    {
        private readonly IStock _stockService;

        public WareHouseCreatorController(IStock stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("CreateLocations")]
        public IActionResult Create()
        {
            _stockService.CreateStorageLocations();
            return Ok();
        }
    }
}
        