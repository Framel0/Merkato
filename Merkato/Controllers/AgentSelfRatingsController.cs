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
    public class AgentSelfRatingsController : Controller
    {
        private readonly MerkatoDbContext _context;

        public AgentSelfRatingsController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: AgentSelfRatings
        public async Task<IActionResult> Index()
        {
            List<Agent> rawList = await _context.Agent.ToListAsync();
            List<AgentStatus> agentStatuses = await _context.AgentStatus.ToListAsync();

            List<AgentSmallViewModel> list = new List<AgentSmallViewModel>();
            foreach (var item in rawList)
            {
                list.Add(new AgentSmallViewModel(item, agentStatuses));
            }

            return View(list);
            //return View(await _context.AgentSelfRating.ToListAsync());
        }

        // GET: AgentSelfRatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentSelfRating = await _context.AgentSelfRating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentSelfRating == null)
            {
                return NotFound();
            }

            return View(agentSelfRating);
        }

        // GET: AgentSelfRatings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgentSelfRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AgentId,RatingDate,RatingTypeId,Score")] AgentSelfRating agentSelfRating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentSelfRating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentSelfRating);
        }

        // GET: AgentSelfRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentSelfRating = await _context.AgentSelfRating.FindAsync(id);
            if (agentSelfRating == null)
            {
                return NotFound();
            }
            return View(agentSelfRating);
        }

        // POST: AgentSelfRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AgentId,RatingDate,RatingTypeId,Score")] AgentSelfRating agentSelfRating)
        {
            if (id != agentSelfRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentSelfRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentSelfRatingExists(agentSelfRating.Id))
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
            return View(agentSelfRating);
        }

        // GET: AgentSelfRatings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentSelfRating = await _context.AgentSelfRating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentSelfRating == null)
            {
                return NotFound();
            }

            return View(agentSelfRating);
        }

        // POST: AgentSelfRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentSelfRating = await _context.AgentSelfRating.FindAsync(id);
            _context.AgentSelfRating.Remove(agentSelfRating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentSelfRatingExists(int id)
        {
            return _context.AgentSelfRating.Any(e => e.Id == id);
        }
    }
}
