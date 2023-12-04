﻿using Inventory_Management_System.Contracts;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Service.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = "Forklift Driver, Admin")]
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
        public async Task<IActionResult> StoreComponent([FromBody] ComponentOrderRequest request)
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

        [HttpPost("MoveToProduction")]
        public async Task<IActionResult> ComponentToProduction([FromBody] ComponentOrderRequest request)
        {
            if (request.Quantity <= 0)
            {
                return BadRequest("Requested quantity can not be zero or negative!");
            }
            // might need a look into the generic type
            if (Enum.TryParse(typeof(ProductDesignation), request.ProductDesignation, out object parsedValue))
            {
                await _stockService.MoveRawMaterialToProductionAsync((ProductDesignation)parsedValue, request.Quantity);
                return Ok($"{request.Quantity} pcs of {request.ProductDesignation} successfully moved to production line.");
            }
            else
            {
                return BadRequest("Incorrect productDesignation!");
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