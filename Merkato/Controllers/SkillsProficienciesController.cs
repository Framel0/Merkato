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
    public class SkillsProficienciesController : Controller
    {
        private readonly MerkatoDbContext _context;

        public SkillsProficienciesController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: SkillsProficiencies
        public async Task<IActionResult> Index()
        {
            var data = await _context.SkillsProficiency.
    Select(b => new SkillsProficiencyViewModel
    {
        Id = b.Id,
        Code = b.Code,
        Name = b.Name,
        Description = b.Description,
        ActiveString = b.Active == 1 ? "active" : "inactive"
    }).ToListAsync();
            return View(data);
            //return View(await _context.SkillsProficiency.ToListAsync());
        }

        // GET: SkillsProficiencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillsProficiency = await _context.SkillsProficiency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skillsProficiency == null)
            {
                return NotFound();
            }

            return View(skillsProficiency);
        }

        // GET: SkillsProficiencies/Create
        public IActionResult Create()
        {
            SkillsProficiencyViewModel vm;
            vm = new SkillsProficiencyViewModel(_context);
            return View(vm);
        }

        // POST: SkillsProficiencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SkillsProficiencyViewModel skillsProficiency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skillsProficiency.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            skillsProficiency.loadLists(_context);
            return View(skillsProficiency);
        }

        // GET: SkillsProficiencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            SkillsProficiencyViewModel vm;
            if (id == 0)
            {
                vm = new SkillsProficiencyViewModel(_context);
            }
            else
            {
                var skillsProficiency = await _context.SkillsProficiency.FindAsync(id);
                if (skillsProficiency == null)
                {
                    return NotFound();
                }

                vm = new SkillsProficiencyViewModel(_context, skillsProficiency);
            }
         
            return View(vm);
        }

        // POST: SkillsProficiencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SkillsProficiencyViewModel skillsProficiency)
        {
            if (id != skillsProficiency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skillsProficiency.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillsProficiencyExists(skillsProficiency.Id))
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
            skillsProficiency.loadLists(_context);
            return View(skillsProficiency);
        }

        // GET: SkillsProficiencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skillsProficiency = await _context.SkillsProficiency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skillsProficiency == null)
            {
                return NotFound();
            }

            return View(skillsProficiency);
        }

        // POST: SkillsProficiencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skillsProficiency = await _context.SkillsProficiency.FindAsync(id);
            _context.SkillsProficiency.Remove(skillsProficiency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillsProficiencyExists(int id)
        {
            return _context.SkillsProficiency.Any(e => e.Id == id);
        }
    }
}
