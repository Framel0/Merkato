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
    public class JobRatingsController : Controller
    {
        private readonly MerkatoDbContext _context;

        public JobRatingsController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: JobRatings
        public async Task<IActionResult> Index()
        {
            var data = await _context.JobRating.
Select(b => new JobRatingViewModel
{
    Id = b.Id,
    Code = b.Code,
    Name = b.Name,
    ActiveString = b.Active == 1 ? "active" : "inactive"
}).ToListAsync();
            return View(data);
            //return View(await _context.JobRating.ToListAsync());
        }

        // GET: JobRatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobRating = await _context.JobRating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobRating == null)
            {
                return NotFound();
            }

            return View(jobRating);
        }

        // GET: JobRatings/Create
        public IActionResult Create()
        {
            JobRatingViewModel vm;
            vm = new JobRatingViewModel(_context);
            return View(vm);
        }

        // POST: JobRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobRatingViewModel jobRating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobRating.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            jobRating.loadLists(_context);
            return View(jobRating);
        }

        // GET: JobRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            JobRatingViewModel vm;
            if (id == 0)
            {
                vm = new JobRatingViewModel(_context);
            }
            else
            {
                var jobRating = await _context.JobRating.FindAsync(id);
                if (jobRating == null)
                {
                    return NotFound();
                }
                vm = new JobRatingViewModel(_context, jobRating);
            }

            return View(vm);
        }

        // POST: JobRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobRatingViewModel jobRating)
        {
            if (id != jobRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobRating.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobRatingExists(jobRating.Id))
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
            jobRating.loadLists(_context);
            return View(jobRating);
        }

        // GET: JobRatings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobRating = await _context.JobRating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobRating == null)
            {
                return NotFound();
            }

            return View(jobRating);
        }

        // POST: JobRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobRating = await _context.JobRating.FindAsync(id);
            _context.JobRating.Remove(jobRating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobRatingExists(int id)
        {
            return _context.JobRating.Any(e => e.Id == id);
        }
    }
}
