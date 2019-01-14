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
    public class ClientRequestApiController : ControllerBase
    {
        private readonly MerkatoDbContext _context;

        public ClientRequestApiController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: api/ActivityApi
        [HttpGet]
        public IEnumerable<ClientRequest> GetClientRequest()
        {

            var list = _context.ClientRequest
                .Include(r => r.Client)
                .Include(a => a.Outlet)
                .Include(b => b.Mechanism)
                .
      Select(b => new ClientRequestViewModel
      {
          Id = b.Id,
          ClientId = b.ClientId,
          OutletId = b.OutletId,
          MechanismId = b.MechanismId,
          StartDate = b.StartDate,
          EndDate = b.EndDate,
          Days = b.Days,
          BatchNo = b.BatchNo,
          NbAgentShift1 = b.NbAgentShift1,
          NbAgentShift2 = b.NbAgentShift2,
          NbAgentShift3 = b.NbAgentShift3,
          NbAgentShift4 = b.NbAgentShift4,
          ClientName = b.Client.ClientName,
          OutletName = b.Outlet.Name,
          MechanismName = b.Mechanism.Mechanism.Name,
          LanguageName = b.Language.Name,
          skillName = b.Skill.Name,
          GenderName = b.Gender.Name,       
          GradeName = b.Grade.Name
      });
            return list;
        }

        // GET: api/ActivityApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientRequest = await _context.ClientRequest.FindAsync(id);

            if (clientRequest == null)
            {
                return NotFound();
            }

            return Ok(clientRequest);
        }

        // PUT: api/ActivityApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientRequest([FromRoute] int id, [FromBody] ClientRequest clientRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientRequestExists(id))
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

        // POST: api/ActivityApi
        [HttpPost]
        public async Task<IActionResult> PostClientRequest([FromBody] ClientRequest clientRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ClientRequest.Add(clientRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientRequest", new { id = clientRequest.Id }, clientRequest);
        }

        // DELETE: api/ActivityApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientRequest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientRequest = await _context.ClientRequest.FindAsync(id);
            if (clientRequest == null)
            {
                return NotFound();
            }

            _context.ClientRequest.Remove(clientRequest);
            await _context.SaveChangesAsync();

            return Ok(clientRequest);
        }

        private bool ClientRequestExists(int id)
        {
            return _context.ClientRequest.Any(e => e.Id == id);
        }
    }
}