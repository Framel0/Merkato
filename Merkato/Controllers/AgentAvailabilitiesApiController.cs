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
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AgentAvailabilitiesApiController : ControllerBase
    {
        private readonly MerkatoDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AgentAvailabilitiesApiController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: api/AgentAvailabilitiesApi
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<AgentAvailability> GetAgentAvailability()
        {
            return _context.AgentAvailability;
        }

        // GET: api/AgentAvailabilitiesApi/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgentAvailability([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agentAvailability = await _context.AgentAvailability.FindAsync(id);

            if (agentAvailability == null)
            {
                return NotFound();
            }

            return Ok(agentAvailability);
        }

        // PUT: api/AgentAvailabilitiesApi/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agentAvailability"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgentAvailability([FromRoute] int id, [FromBody] AgentAvailability agentAvailability)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agentAvailability.Id)
            {
                return BadRequest();
            }

            _context.Entry(agentAvailability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentAvailabilityExists(id))
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

        // POST: api/AgentAvailabilitiesApi
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentAvailability"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<AgentAvailabilityViewModel>> PostAgentAvailability([FromBody] AgentAvailabilityViewModel agentAvailability)
        {
            ApiResult<AgentAvailabilityViewModel> result = new ApiResult<AgentAvailabilityViewModel>();

            //Generate number
            Random rd = new Random();
            Int32 batchCode = rd.Next(9999);
            agentAvailability.BatchNo = "A-" + batchCode.ToString();

            if (ModelState.IsValid)
            {
                result.Successfull = 0;

                try
                {
                    AgentAvailability availability = new AgentAvailability();
                    {
                        availability.Id = agentAvailability.Id;
                        availability.AgentId = agentAvailability.AgentId;
                        availability.BatchNo = agentAvailability.BatchNo;
                        availability.StartDate = agentAvailability.StartDate;
                        availability.EndDate = agentAvailability.EndDate;
                        availability.Days = agentAvailability.Days;
                        availability.Shift1 = agentAvailability.Shift1;
                        availability.Shift2 = agentAvailability.Shift2;
                        availability.Shift3 = agentAvailability.Shift3;
                        availability.Shift4 = agentAvailability.Shift4;
                    }
                    _context.Add(availability);
                    await _context.SaveChangesAsync();

                    AgentAvailabilityDetailsViewModel details = new AgentAvailabilityDetailsViewModel();

                    int id = availability.Id;

                    details.AgentAvailabillityId = availability.Id;


                    Util util = new Util();
                    foreach (DateTime day in util.EachDay(agentAvailability.StartDate, agentAvailability.EndDate))
                    {
                        //(int)day.DayOfWeek
                        var a = (int)day.DayOfWeek;
                        foreach (char d in agentAvailability.Days)
                        {
                            var val = (int)Char.GetNumericValue(d);
                            if (a == val)
                            {
                                details.Date = day;
                                _context.Add(details.GetModel());
                                await _context.SaveChangesAsync();
                               
                            }
                        }

                    }
                    result.Successfull = 1;
                    result.Model = agentAvailability;
                }
                catch(Exception ex)
                {
                    result.Error = "Failed to  Create Availability";
                    result.InternalError = ex.Message;
                    result.Successfull = 0;
                }

             
            }

            //return CreatedAtAction("GetAgentAvailability", new { id = agentAvailability.Id }, agentAvailability);
            return result;

        }

        // DELETE: api/AgentAvailabilitiesApi/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgentAvailability([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agentAvailability = await _context.AgentAvailability.FindAsync(id);
            if (agentAvailability == null)
            {
                return NotFound();
            }

            _context.AgentAvailability.Remove(agentAvailability);
            await _context.SaveChangesAsync();

            return Ok(agentAvailability);
        }

        private bool AgentAvailabilityExists(int id)
        {
            return _context.AgentAvailability.Any(e => e.Id == id);
        }
    }
}