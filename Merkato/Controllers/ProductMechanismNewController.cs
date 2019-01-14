using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;

namespace Merkato.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ProductMechanismNewController : ControllerBase
    {
        private readonly MerkatoDbContext _context;

        public ProductMechanismNewController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductMechanismNew
        [HttpGet]
        public IEnumerable<ProductMechanism> GetProductMechanism()
        {
            return _context.ProductMechanism;
        }

        // GET: api/ProductMechanismNew/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductMechanism([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productMechanism = await _context.ProductMechanism.FindAsync(id);

            if (productMechanism == null)
            {
                return NotFound();
            }

            return Ok(productMechanism);
        }

        // PUT: api/ProductMechanismNew/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductMechanism([FromRoute] int id, [FromBody] ProductMechanism productMechanism)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productMechanism.Id)
            {
                return BadRequest();
            }

            _context.Entry(productMechanism).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductMechanismExists(id))
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

        // POST: api/ProductMechanismNew
        [HttpPost]
        public async Task<IActionResult> PostProductMechanism([FromBody] ProductMechanism productMechanism)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductMechanism.Add(productMechanism);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductMechanism", new { id = productMechanism.Id }, productMechanism);
        }

        // DELETE: api/ProductMechanismNew/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductMechanism([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productMechanism = await _context.ProductMechanism.FindAsync(id);
            if (productMechanism == null)
            {
                return NotFound();
            }

            _context.ProductMechanism.Remove(productMechanism);
            await _context.SaveChangesAsync();

            return Ok(productMechanism);
        }

        private bool ProductMechanismExists(int id)
        {
            return _context.ProductMechanism.Any(e => e.Id == id);
        }
    }
}