using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;
using Merkato.Lib.ViewModels;

namespace Merkato.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[ApiController]
    public class ProductMechanismApiController: ControllerBase
    {
        private readonly MerkatoDbContext _context;

        public ProductMechanismApiController(MerkatoDbContext context)
        {
            _context = context;
        }

       // GET: api/ProductMechanismApi/getAll
       [HttpGet("getProductsList/{mechanism}")]
        public IEnumerable<ProductMechanism> GetProductMechanism([FromHeader] int mechanism)
        {
           
             return _context.ProductMechanism.Where(m=>m.MechanismId==mechanism)
                 .Select(p => new ProductMechanismViewModel
                 {
                     Id = p.Id,
                     ProductName = p.Product.ProductName,
                     MechanismName = p.Mechanism.Name,
                     ProductId = p.ProductId,
                     Quantity = p.Quantity,
                     ProductOrder = p.ProductOrder,
                     MechanismId = p.MechanismId
                 });
        }

        // GET: api/ProductMechanismApi/getClientProducts/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpGet("getClientProducts/{client}")]
        public IEnumerable<ClientProduct> GetClientProducts([FromRoute] int client)
        {
            var list = _context.ClientProduct.Where(c => c.ClientId == client);

            return list;
        }


        //// GET: api/ProductMechanismApi/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProductMechanism([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var productMechanism = await _context.ProductMechanism.FindAsync(id);

        //    if (productMechanism == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(productMechanism);
        //}

        // PUT: api/ProductMechanismApi/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productMechanism"></param>
        /// <returns></returns>
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

        // POST: api/ProductMechanismApi
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productMechanism"></param>
        /// <returns></returns>
        [HttpPost("saveProduct")]
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

        // DELETE: api/ProductMechanismApi/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deleteProduct/{id}")]
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