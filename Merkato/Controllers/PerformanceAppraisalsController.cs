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
    public class PerformanceAppraisalsController : Controller
    {
        private readonly MerkatoDbContext _context;

        public PerformanceAppraisalsController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: PerformanceAppraisals
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
            //return View(await _context.PerformanceAppraisal.ToListAsync());
        }

        // GET: PerformanceAppraisals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performanceAppraisal = await _context.PerformanceAppraisal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (performanceAppraisal == null)
            {
                return NotFound();
            }

            return View(performanceAppraisal);
        }

        // GET: PerformanceAppraisals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PerformanceAppraisals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AgentId,RatingDate,RatingType,Score")] PerformanceAppraisal performanceAppraisal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performanceAppraisal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(performanceAppraisal);
        }

        // GET: PerformanceAppraisals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performanceAppraisal = await _context.PerformanceAppraisal.FindAsync(id);
            if (performanceAppraisal == null)
            {
                return NotFound();
            }
            return View(performanceAppraisal);
        }

        // POST: PerformanceAppraisals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AgentId,RatingDate,RatingType,Score")] PerformanceAppraisal performanceAppraisal)
        {
            if (id != performanceAppraisal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performanceAppraisal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformanceAppraisalExists(performanceAppraisal.Id))
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
            return View(performanceAppraisal);
        }

        // GET: PerformanceAppraisals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performanceAppraisal = await _context.PerformanceAppraisal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (performanceAppraisal == null)
            {
                return NotFound();
            }

            return View(performanceAppraisal);
        }

        // POST: PerformanceAppraisals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performanceAppraisal = await _context.PerformanceAppraisal.FindAsync(id);
            _context.PerformanceAppraisal.Remove(performanceAppraisal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformanceAppraisalExists(int id)
        {
            return _context.PerformanceAppraisal.Any(e => e.Id == id);
        }
    }
}
