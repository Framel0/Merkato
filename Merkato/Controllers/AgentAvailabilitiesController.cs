using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;
using Merkato.Lib.ViewModels;

namespace Merkato.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AgentAvailabilitiesController : Controller
    {
        private readonly MerkatoDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AgentAvailabilitiesController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: AgentAvailabilities
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {

            


            var merkatoDbContext = _context.AgentAvailabilityDetails.Include(a => a.AgentAvailabillity).
                Select(a=> new AgentAvailabilityDetailsViewModel {
                    Id= a.Id,
                    BatchNo = a.AgentAvailabillity.BatchNo,
                    Date = a.Date,
                    NbAgentFirstShift = a.AgentAvailabillity.Shift1,
                    NbAgentSecondShift = a.AgentAvailabillity.Shift2,
                    NbAgentThirdShift = a.AgentAvailabillity.Shift3,
                    NbAgentFourthShift = a.AgentAvailabillity.Shift4,
                    AgentName = a.AgentAvailabillity.Agent.FirstName + " "+ a.AgentAvailabillity.Agent.SurName
                });
            return View(await merkatoDbContext.ToListAsync());
        }

        // GET: AgentAvailabilities/Details/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentAvailability = await _context.AgentAvailability
                .Include(a => a.Agent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentAvailability == null)
            {
                return NotFound();
            }

            return View(agentAvailability);
        }

        // GET: AgentAvailabilities/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            AgentAvailabilityViewModel vm;
            vm = new AgentAvailabilityViewModel(_context);
            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Id");
            return View(vm);
        }

        // POST: AgentAvailabilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentAvailability"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgentAvailabilityViewModel agentAvailability)
        {
            //Generate number
            Random rd = new Random();
            Int32 batchCode = rd.Next(9999);
            agentAvailability.BatchNo = "A-" + batchCode.ToString();

            if (ModelState.IsValid)
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
                return RedirectToAction(nameof(Index));
            }

            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Id", agentAvailability.AgentId);
            agentAvailability.loadLists(_context);
            return View(agentAvailability);
        }

        // GET: AgentAvailabilities/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            AgentAvailabilityViewModel vm;
            if (id == 0)
            {
                vm = new AgentAvailabilityViewModel(_context);
            }
            else
            {
                var agentAvailability = await _context.AgentAvailability.FindAsync(id);
                if (agentAvailability == null)
                {
                    return NotFound();
                }
                vm = new AgentAvailabilityViewModel(_context, agentAvailability);
            }
            
            //ViewData["AgentId"] = new SelectList(_context.Employee, "Id", "Id", agentAvailability.AgentId);
            return View(vm);
       
        }

        // POST: AgentAvailabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agentAvailability"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AgentAvailabilityViewModel agentAvailability)
        {
            if (id != agentAvailability.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentAvailability.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentAvailabilityExists(agentAvailability.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Id", agentAvailability.AgentId);

             agentAvailability.loadLists(_context);
            return View(agentAvailability);
        }

        // GET: AgentAvailabilities/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentAvailability = await _context.AgentAvailability
                .Include(a => a.Agent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentAvailability == null)
            {
                return NotFound();
            }

            return View(agentAvailability);
        }

        // POST: AgentAvailabilities/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentAvailability = await _context.AgentAvailability.FindAsync(id);
            _context.AgentAvailability.Remove(agentAvailability);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentAvailabilityExists(int id)
        {
            return _context.AgentAvailability.Any(e => e.Id == id);
        }
    }
}
