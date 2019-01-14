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
    public class TrainingDetailsController : Controller
    {
        private readonly MerkatoDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public TrainingDetailsController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: TrainingDetails
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.TrainingId = id;

            var training = _context.TrainingDefinition.FirstOrDefault(c => c.Id == id);
            if (training == null)
                return NotFound();

            ViewBag.TrainingName = $"{training.TrainingName}";

            var data = await _context.TrainingDetails.Where(c => c.TrainingId == id).
       Select(b => new TrainingDetailsViewModel
       {
           Id = b.Id,
           Question = b.Question,
           AnswerValues = b.AnswerValues,
           AnswerCorrect = b.AnswerCorrect,
           AnswerPoint = b.AnswerPoint
       }).ToListAsync();
            return View(data);
        }

        // GET: TrainingDetails/Details/5
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

            var trainingDetails = await _context.TrainingDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingDetails == null)
            {
                return NotFound();
            }
            ViewBag.TrainingId = trainingDetails.TrainingId;
            return View(trainingDetails);
        }

        // GET: TrainingDetails/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create(int id)
        {
            TrainingDetailsViewModel vm;
            vm = new TrainingDetailsViewModel(_context);

            vm.TrainingId = id;
            ViewBag.TrainingId = id;
            return View(vm);
        }

        // POST: TrainingDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, TrainingDetailsViewModel trainingDetails)
        {
            if (ModelState.IsValid)
            {
                ViewBag.TrainingId = id;
                trainingDetails.Id = 0;
                trainingDetails.TrainingId = id;

                _context.Add(trainingDetails.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = trainingDetails.TrainingId });
            }
            return View(trainingDetails);
        }

        // GET: TrainingDetails/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {

            TrainingDetailsViewModel vm;
            if (id == 0)
            {
                vm = new TrainingDetailsViewModel(_context);
            }
            else
            {
                var trainingDetails = await _context.TrainingDetails.FindAsync(id);
                if (trainingDetails == null)
                {
                    return NotFound();
                }
                vm = new TrainingDetailsViewModel(_context, trainingDetails);
                ViewBag.TrainingId = trainingDetails.TrainingId;
            }

            return View(vm);
        }

        // POST: TrainingDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrainingDetailsViewModel trainingDetails)
        {
            if (id != trainingDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingDetails.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingDetailsExists(trainingDetails.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = trainingDetails.TrainingId });
            }
            ViewBag.TrainingId = trainingDetails.TrainingId;
            return View(trainingDetails);
        }

        // GET: TrainingDetails/Delete/5
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

            var trainingDetails = await _context.TrainingDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingDetails == null)
            {
                return NotFound();
            }

            return View(trainingDetails);
        }

        // POST: TrainingDetails/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingDetails = await _context.TrainingDetails.FindAsync(id);
            _context.TrainingDetails.Remove(trainingDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingDetailsExists(int id)
        {
            return _context.TrainingDetails.Any(e => e.Id == id);
        }
    }
}
