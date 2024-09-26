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
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }

            // Validate product details
            if (string.IsNullOrWhiteSpace(product.Name) || product.Quantity < 0 || product.Price < 0)
            {
                return BadRequest("Invalid product details.");
            }

            // Generate a new GUID for the product
            product.Id = Guid.NewGuid();

            // Save product to the database
            await _productService.CreateAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }

            // Fetch existing product
            var existingProduct = await _productService.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update the product fields
            existingProduct.Name = product.Name ?? existingProduct.Name;
            existingProduct.Quantity = product.Quantity >= 0 ? product.Quantity : existingProduct.Quantity;
            existingProduct.Price = product.Price >= 0 ? product.Price : existingProduct.Price;
            existingProduct.ExpiryDate = product.ExpiryDate != default ? product.ExpiryDate : existingProduct.ExpiryDate;
            existingProduct.PurchaseDate = product.PurchaseDate != default ? product.PurchaseDate : existingProduct.PurchaseDate;
            existingProduct.AisleId = product.AisleId != Guid.Empty ? product.AisleId : existingProduct.AisleId;

            // Save updates to the database
            await _productService.UpdateAsync(id, existingProduct);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            // Fetch existing product
            var existingProduct = await _productService.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Delete the product from the database
            await _productService.DeleteAsync(id);

            return NoContent();
        }
    }
}
