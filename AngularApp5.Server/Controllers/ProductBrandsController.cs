using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularApp5.Server.Data;
using AngularApp5.Server.Models;

namespace AngularApp5.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBrandsController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductBrandsController(StoreContext context)
        {
            _context = context;
        }

        // GET: api/ProductBrands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductsBrands()
        {
            return await _context.ProductsBrands.ToListAsync();
        }

        // GET: api/ProductBrands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        {
            var productBrand = await _context.ProductsBrands.FindAsync(id);

            if (productBrand == null)
            {
                return NotFound();
            }

            return productBrand;
        }

        // PUT: api/ProductBrands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductBrand(int id, ProductBrand productBrand)
        {
            if (id != productBrand.Id)
            {
                return BadRequest();
            }

            _context.Entry(productBrand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductBrandExists(id))
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

        // POST: api/ProductBrands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductBrand>> PostProductBrand(ProductBrand productBrand)
        {
            _context.ProductsBrands.Add(productBrand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductBrand", new { id = productBrand.Id }, productBrand);
        }

        // DELETE: api/ProductBrands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductBrand(int id)
        {
            var productBrand = await _context.ProductsBrands.FindAsync(id);
            if (productBrand == null)
            {
                return NotFound();
            }

            _context.ProductsBrands.Remove(productBrand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductBrandExists(int id)
        {
            return _context.ProductsBrands.Any(e => e.Id == id);
        }
    }
}
