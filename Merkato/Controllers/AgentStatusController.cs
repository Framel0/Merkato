using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;

namespace Merkato.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AgentStatusController : Controller
    {
        private readonly MerkatoDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AgentStatusController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: AgentStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.AgentStatus.ToListAsync());
        }

        // GET: AgentStatus/Details/5
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

            var agentStatus = await _context.AgentStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentStatus == null)
            {
                return NotFound();
            }

            return View(agentStatus);
        }

        // GET: AgentStatus/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: AgentStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentStatus"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Description,UserId,LastDateModified")] AgentStatus agentStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentStatus);
        }

        // GET: AgentStatus/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentStatus = await _context.AgentStatus.FindAsync(id);
            if (agentStatus == null)
            {
                return NotFound();
            }
            return View(agentStatus);
        }

        // POST: AgentStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agentStatus"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,Description,UserId,LastDateModified")] AgentStatus agentStatus)
        {
            if (id != agentStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentStatusExists(agentStatus.Id))
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
            return View(agentStatus);
        }

        // GET: AgentStatus/Delete/5
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

            var agentStatus = await _context.AgentStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentStatus == null)
            {
                return NotFound();
            }

            return View(agentStatus);
        }

        // POST: AgentStatus/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentStatus = await _context.AgentStatus.FindAsync(id);
            _context.AgentStatus.Remove(agentStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentStatusExists(int id)
        {
            return _context.AgentStatus.Any(e => e.Id == id);
        }
    }
}
