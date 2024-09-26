using GroceryShopAPI.Models;
using GroceryShopAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AislesController : ControllerBase
    {
        private readonly AisleService _aisleService;
        private readonly ProductService _productService;

        public AislesController(AisleService aisleService, ProductService productService)
        {
            _aisleService = aisleService;
            _productService = productService;
        }

        // List all aisles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aisle>>> GetAll()
        {
            var aisles = await _aisleService.GetAllAsync();
            return Ok(aisles);
        }

        // Get aisle by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Aisle>> GetById(string id)
        {
            var aisle = await _aisleService.GetByIdAsync(id);
            if (aisle == null)
            {
                return NotFound();
            }
            return Ok(aisle);
        }

        // Create a new aisle
        [HttpPost]
        public async Task<ActionResult<Aisle>> Create(Aisle aisle)
        {
            if (aisle == null)
            {
                return BadRequest("Aisle is null.");
            }

            // Generate a new GUID for the aisle
            aisle.Id = Guid.NewGuid();

            await _aisleService.CreateAsync(aisle);

            return CreatedAtAction(nameof(GetById), new { id = aisle.Id }, aisle);
        }

        // Update an existing aisle
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Aisle aisle)
        {
            if (aisle == null)
            {
                return BadRequest("Aisle is null.");
            }

            var existingAisle = await _aisleService.GetByIdAsync(id);
            if (existingAisle == null)
            {
                return NotFound();
            }

            // Update the aisle details
            existingAisle.Name = aisle.Name ?? existingAisle.Name;

            await _aisleService.UpdateAsync(id, existingAisle);
            return NoContent();
        }

        // Delete an aisle
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingAisle = await _aisleService.GetByIdAsync(id);
            if (existingAisle == null)
            {
                return NotFound();
            }

            await _aisleService.DeleteAsync(id);
            return NoContent();
        }

        // List all products that belong to a specific aisle
        [HttpGet("{id}/products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByAisle(string id)
        {
            var aisle = await _aisleService.GetByIdAsync(id);
            if (aisle == null)
            {
                return NotFound();
            }

            var products = await _productService.GetProductsByAisleAsync(id);
            return Ok(products);
        }
    }
}
