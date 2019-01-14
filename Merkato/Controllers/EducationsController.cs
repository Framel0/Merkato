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
    public class EducationsController : Controller
    {
        private readonly MerkatoDbContext _context;

        public EducationsController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: Educations
        public async Task<IActionResult> Index()
        {
            var data = await _context.Education.
Select(b => new EducationViewModel
{
Id = b.Id,
Code = b.Code,
Name = b.Name,
Description = b.Description,
ActiveString = b.Active == 1 ? "active" : "inactive"
}).ToListAsync();
            return View(data);
            //return View(await _context.Education.ToListAsync());
        }

        // GET: Educations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Education
                .FirstOrDefaultAsync(m => m.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // GET: Educations/Create
        public IActionResult Create()
        {
            EducationViewModel vm;
            vm = new EducationViewModel(_context);
            return View(vm);
        }

        // POST: Educations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EducationViewModel education)
        {
            if (ModelState.IsValid)
            {
                _context.Add(education.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            education.loadLists(_context);
            return View(education);
        }

        // GET: Educations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            EducationViewModel vm;
            if (id == 0)
            {
                vm = new EducationViewModel(_context);
            }
            else
            {
                var education = await _context.Education.FindAsync(id);
                if (education == null)
                {
                    return NotFound();
                }
                vm = new EducationViewModel(_context, education);
            }
           
            return View(vm);
        }

        // POST: Educations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EducationViewModel education)
        {
            if (id != education.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(education.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(education.Id))
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
            education.loadLists(_context);
            return View(education);
        }

        // GET: Educations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Education
                .FirstOrDefaultAsync(m => m.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var education = await _context.Education.FindAsync(id);
            _context.Education.Remove(education);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationExists(int id)
        {
            return _context.Education.Any(e => e.Id == id);
        }
    }
}
