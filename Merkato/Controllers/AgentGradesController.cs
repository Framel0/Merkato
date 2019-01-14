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
    public class AgentGradesController : Controller
    {
        private readonly MerkatoDbContext _context;

        public AgentGradesController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: AgentGrades
        public async Task<IActionResult> Index()
        {
            var data = await _context.AgentGrade.
        Select(b => new AgentGradeViewModel
        {
            Id = b.Id,
            Code = b.Code,
            Name = b.Name,
            Amount= b.Amount,
            Description = b.Description,
            ActiveString = b.Active == 1 ? "active" : "inactive"
        }).ToListAsync();
            return View(data);
            //return View(await _context.AgentGrade.ToListAsync());
        }

        // GET: AgentGrades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentGrade = await _context.AgentGrade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentGrade == null)
            {
                return NotFound();
            }

            return View(agentGrade);
        }

        // GET: AgentGrades/Create
        public IActionResult Create()
        {
            AgentGradeViewModel vm;
            vm = new AgentGradeViewModel(_context);
            return View(vm);
        }

        // POST: AgentGrades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgentGradeViewModel agentGrade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agentGrade.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            agentGrade.loadLists(_context);
            return View(agentGrade);
        }

        // GET: AgentGrades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            AgentGradeViewModel vm;
            if (id == 0)
            {
                vm = new AgentGradeViewModel(_context);
            }
            else
            {
                var agentGrade = await _context.AgentGrade.FindAsync(id);
                if (agentGrade == null)
                {
                    return NotFound();
                }
                vm = new AgentGradeViewModel(_context, agentGrade);
            }
          
            return View(vm);
        }

        // POST: AgentGrades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AgentGradeViewModel agentGrade)
        {
            if (id != agentGrade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentGrade.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentGradeExists(agentGrade.Id))
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
            agentGrade.loadLists(_context);
            return View(agentGrade);
        }

        // GET: AgentGrades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentGrade = await _context.AgentGrade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentGrade == null)
            {
                return NotFound();
            }

            return View(agentGrade);
        }

        // POST: AgentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agentGrade = await _context.AgentGrade.FindAsync(id);
            _context.AgentGrade.Remove(agentGrade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentGradeExists(int id)
        {
            return _context.AgentGrade.Any(e => e.Id == id);
        }
    }
}
