using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularApp5.Server.Data;
using AngularApp5.Server.Models;
using AngularApp5.Server.Dtos;
using AngularApp5.Server.Errors;

namespace AngularApp5.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XProductsController : ControllerBase
    {
        private readonly StoreContext _context;

        public XProductsController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            // Uwzględnij powiązane dane (ProductType i ProductBrand)
            var products = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();

            return products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                ProductType = product.ProductType.Name,
                ProductBrand = product.ProductBrand.Name
            }).ToList();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            // Uwzględnij powiązane dane (ProductType i ProductBrand)
            var product = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                ProductType = product.ProductType.Name,
                ProductBrand = product.ProductBrand.Name
            };
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            // Sprawdź, czy ProductTypeId i ProductBrandId istnieją w bazie danych
            if (!await _context.ProductsTypes.AnyAsync(pt => pt.Id == product.ProductTypeId) ||
                !await _context.ProductsBrands.AnyAsync(pb => pb.Id == product.ProductBrandId))
            {
                return BadRequest("Invalid ProductTypeId or ProductBrandId.");
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            // Sprawdź, czy ProductTypeId i ProductBrandId istnieją w bazie danych
            var productType = await _context.ProductsTypes.FindAsync(product.ProductTypeId);
            var productBrand = await _context.ProductsBrands.FindAsync(product.ProductBrandId);

            if (productType == null || productBrand == null)
            {
                return BadRequest("Invalid ProductTypeId or ProductBrandId.");
            }

            // Przypisz powiązane dane
            product.ProductType = productType;
            product.ProductBrand = productBrand;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
