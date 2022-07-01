using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiJwtAuth.Data;
using WebApiJwtAuth.Models;

namespace WebApiJwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DbContextClass _context;

        public ProductsController(DbContextClass context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("Products")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("Product")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult<Product>> Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<ActionResult<IEnumerable<Product>>> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public async Task<ActionResult<IEnumerable<Product>>> Update(int id, Product product)
        {
            if (id != product.ProductId)
                return BadRequest();
            var result = await _context.Products.FindAsync(id);
            if (result == null)
                return NotFound();

            result.ProductName = product.ProductName;
            result.ProductStock = product.ProductStock;
            result.ProductDescription = product.ProductDescription;
            result.ProductCost = product.ProductCost;

            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

    }
}
