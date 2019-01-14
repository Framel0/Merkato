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
    public class ActivityAssignmentsController : Controller
    {
        private readonly MerkatoDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ActivityAssignmentsController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: ActivityAssignments
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var data = await _context.ClientRequestDetails
          .Include(d => d.ClientRequest)
.Select(b => new ClientRequestDetailsViewModel
{
    Id = b.Id,
        //ClientId = b.ClientId,
        //OutletId = b.OutletId,
        //MechanismId = b.MechanismId,
        //StartDate = b.StartDate,
        //EndDate = b.EndDate,
        //Days = b.Days,
        BatchNo = b.ClientRequest.BatchNo,
    NbAgentShift1 = b.ClientRequest.NbAgentShift1,
    NbAgentShift2 = b.ClientRequest.NbAgentShift2,
    NbAgentShift3 = b.ClientRequest.NbAgentShift3,
    NbAgentShift4 = b.ClientRequest.NbAgentShift4,
    ClientName = b.ClientRequest.Client.ClientName,
    OutletName = b.ClientRequest.Outlet.Name,
    MechanismName = b.ClientRequest.Mechanism.Mechanism.Name,
    LanguageName = b.ClientRequest.Language.Name,
    Date = b.Date
        ////skillName = b.Skill.Name,
        ////GenderName = b.Gender.Name,
        ////MaritalStatusName = b.MaritalStatus.Name,
        ////GradeName = b.Grade.Name
    }).ToListAsync();
            return View(data);
            //var merkatoDbContext = _context.ActivityAssignment.Include(a => a.Activity).Include(a => a.Agent);
            //return View(await merkatoDbContext.ToListAsync());
        }

        // GET: Employees
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AgentsAvailable()
        {
            List<Agent> rawList = await _context.Agent.ToListAsync();
            List<AgentStatus> agentStatuses = await _context.AgentStatus.ToListAsync();

            List<AgentSmallViewModel> list = new List<AgentSmallViewModel>();
            foreach (var item in rawList)
            {
                list.Add(new AgentSmallViewModel(item, agentStatuses));
            }

            return View(list);
        }

        // GET: ActivityAssignments/Details/5
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

            var activityAssignment = await _context.ActivityAssignment             
                .Include(a => a.Agent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityAssignment == null)
            {
                return NotFound();
            }

            return View(activityAssignment);
        }

        // GET: ActivityAssignments/Create
        public IActionResult Create()
        {
            ViewData["ActivityId"] = new SelectList(_context.ClientRequest, "Id", "Id");
            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Id");
            return View();
        }

        // POST: ActivityAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="activityAssignment"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActivityId,AgentId,AssignmentDate")] ActivityAssignment activityAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activityAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityId"] = new SelectList(_context.ClientRequest, "Id", "Id", activityAssignment.ActivityId);
            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Id", activityAssignment.AgentId);
            return View(activityAssignment);
        }

        // GET: ActivityAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityAssignment = await _context.ActivityAssignment.FindAsync(id);
            if (activityAssignment == null)
            {
                return NotFound();
            }
            ViewData["ActivityId"] = new SelectList(_context.ClientRequest, "Id", "Id", activityAssignment.ActivityId);
            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Id", activityAssignment.AgentId);
            return View(activityAssignment);
        }

        // POST: ActivityAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActivityId,AgentId,AssignmentDate")] ActivityAssignment activityAssignment)
        {
            if (id != activityAssignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activityAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityAssignmentExists(activityAssignment.Id))
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
            ViewData["ActivityId"] = new SelectList(_context.ClientRequest, "Id", "Id", activityAssignment.ActivityId);
            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Id", activityAssignment.AgentId);
            return View(activityAssignment);
        }

        // GET: ActivityAssignments/Delete/5
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

            var activityAssignment = await _context.ActivityAssignment
                .Include(a => a.Agent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityAssignment == null)
            {
                return NotFound();
            }

            return View(activityAssignment);
        }

        // POST: ActivityAssignments/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activityAssignment = await _context.ActivityAssignment.FindAsync(id);
            _context.ActivityAssignment.Remove(activityAssignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityAssignmentExists(int id)
        {
            return _context.ActivityAssignment.Any(e => e.Id == id);
        }
    }
}
