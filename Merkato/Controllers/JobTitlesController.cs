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
    public class JobTitlesController : Controller
    {
        private readonly MerkatoDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public JobTitlesController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: JobTitles
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
                        var data = await _context.JobTitle.
           Select(b => new JobTitleViewModel
           {
               Id = b.Id,
               Code = b.Code,
               Description = b.Description,
               Name = b.Name,
               ActiveString = b.Active == 1 ? "active" : "inactive"
           }).ToListAsync();
            return View(data);

        }

        // GET: JobTitles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitle = await _context.JobTitle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobTitle == null)
            {
                return NotFound();
            }

            return View(jobTitle);
        }

        // GET: JobTitles/Create
        public IActionResult Create()
        {
            JobTitleViewModel vm;
            vm = new JobTitleViewModel(_context);
            return View(vm);
        }

        // POST: JobTitles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobTitleViewModel jobTitle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobTitle.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            jobTitle.loadLists(_context);
            return View(jobTitle);
        }

        // GET: JobTitles/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            JobTitleViewModel vm;
            if (id == 0)
            {
                vm = new JobTitleViewModel(_context);
            }
            else
            {
                var jobTitle = await _context.JobTitle.FindAsync(id);
                if (jobTitle == null)
                {
                    return NotFound();
                }
                vm = new JobTitleViewModel(_context, jobTitle);
            }
           
            return View(vm);
        }

        // POST: JobTitles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobTitleViewModel jobTitle)
        {
            if (id != jobTitle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobTitle.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobTitleExists(jobTitle.Id))
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
            jobTitle.loadLists(_context);
            return View(jobTitle);
        }

        // GET: JobTitles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobTitle = await _context.JobTitle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobTitle == null)
            {
                return NotFound();
            }

            return View(jobTitle);
        }

        // POST: JobTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobTitle = await _context.JobTitle.FindAsync(id);
            _context.JobTitle.Remove(jobTitle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobTitleExists(int id)
        {
            return _context.JobTitle.Any(e => e.Id == id);
        }
    }
}
