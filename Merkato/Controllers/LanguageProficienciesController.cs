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
    public class LanguageProficienciesController : Controller
    {
        private readonly MerkatoDbContext _context;

        public LanguageProficienciesController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: LanguageProficiencies
        public async Task<IActionResult> Index()
        {
            var data = await _context.LanguageProficiency.
Select(b => new LanguageProficiencyViewModel
{
Id = b.Id,
Code = b.Code,
Name = b.Name,
Description = b.Description,
ActiveString = b.Active == 1 ? "active" : "inactive"
}).ToListAsync();
            return View(data);
            //return View(await _context.LanguageProficiency.ToListAsync());
        }

        // GET: LanguageProficiencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var languageProficiency = await _context.LanguageProficiency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (languageProficiency == null)
            {
                return NotFound();
            }

            return View(languageProficiency);
        }

        // GET: LanguageProficiencies/Create
        public IActionResult Create()
        {
            LanguageProficiencyViewModel vm;
            vm = new LanguageProficiencyViewModel(_context);
            return View(vm);
        }

        // POST: LanguageProficiencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LanguageProficiencyViewModel languageProficiency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(languageProficiency.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            languageProficiency.loadLists(_context);
            return View(languageProficiency);
        }

        // GET: LanguageProficiencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            LanguageProficiencyViewModel vm;
            if (id == 0)
            {
                vm = new LanguageProficiencyViewModel(_context);
            }
            else
            {
                var languageProficiency = await _context.LanguageProficiency.FindAsync(id);
                if (languageProficiency == null)
                {
                    return NotFound();
                }
                vm = new LanguageProficiencyViewModel(_context,languageProficiency);
            }
       
            return View(vm);
        }

        // POST: LanguageProficiencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LanguageProficiencyViewModel languageProficiency)
        {
            if (id != languageProficiency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(languageProficiency.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageProficiencyExists(languageProficiency.Id))
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
            languageProficiency.loadLists(_context);
            return View(languageProficiency);
        }

        // GET: LanguageProficiencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var languageProficiency = await _context.LanguageProficiency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (languageProficiency == null)
            {
                return NotFound();
            }

            return View(languageProficiency);
        }

        // POST: LanguageProficiencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var languageProficiency = await _context.LanguageProficiency.FindAsync(id);
            _context.LanguageProficiency.Remove(languageProficiency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LanguageProficiencyExists(int id)
        {
            return _context.LanguageProficiency.Any(e => e.Id == id);
        }
    }
}
