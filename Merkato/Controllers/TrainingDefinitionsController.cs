using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;
using Merkato.Lib.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Merkato.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingDefinitionsController : Controller
    {
        private readonly MerkatoDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hostingEnvironment"></param>
        public TrainingDefinitionsController(MerkatoDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _appEnvironment = hostingEnvironment;
        }

        // GET: TrainingDefinitions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var data = await _context.TrainingDefinition
                .Select(a=> new TrainingDefinitionViewModel
                {
                    Id= a.Id,
                    TrainingName = a.TrainingName,
                    JobName = a.Job.Name
                })
                .ToListAsync();

            return View(data);
           
        }

        // GET: TrainingDefinitions/Details/5
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

            var trainingDefinition = await _context.TrainingDefinition
                .Include(t => t.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingDefinition == null)
            {
                return NotFound();
            }
            
            return View(trainingDefinition);
        }

        // GET: TrainingDefinitions/Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Create(int id)
        {

            TrainingDefinitionViewModel vm;
            vm = new TrainingDefinitionViewModel(_context);
            //vm.TrainingId = id;
            
            return View(vm);

            //ViewData["JobId"] = new SelectList(_context.JobTitle, "Id", "Name");
            //ViewData["TrainingId"] = new SelectList(_context.TrainingDetails, "Id", "Id");
            //return View();
        }

        // POST: TrainingDefinitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainingDefinition"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingDefinitionViewModel trainingDefinition, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //trainingDefinition.TrainingId = id;
              
                    if (file == null && file.Length ==0)
                        return Content("file not selected");

                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

                    var filePath = Path.Combine(_appEnvironment.WebRootPath, "images\\TrainingMaterial");

                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                       {
                                await file.CopyToAsync(fileStream);

                                trainingDefinition.TraningMaterial = fileName;
                       }


                _context.Add(trainingDefinition.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            trainingDefinition.loadLists(_context);
            //ViewData["JobId"] = new SelectList(_context.JobTitle, "Id", "Name", trainingDefinition.JobId);
            //ViewData["TrainingId"] = new SelectList(_context.TrainingDetails, "Id", "Id", trainingDefinition.TrainingId);
            return View(trainingDefinition);
        }

        // GET: TrainingDefinitions/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            TrainingDefinitionViewModel vm;
            if (id == 0)
            {
                vm = new TrainingDefinitionViewModel(_context);
            }
            else
            {
                var training = await _context.TrainingDefinition.FindAsync(id);
                if (training == null)
                {
                    return NotFound();
                }

                vm = new TrainingDefinitionViewModel(_context, training);
                //ViewBag.TrainingId = training.TrainingId;
            }


            return View(vm);
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var trainingDefinition = await _context.TrainingDefinition.FindAsync(id);
            //if (trainingDefinition == null)
            //{
            //    return NotFound();
            //}
            //ViewData["JobId"] = new SelectList(_context.JobTitle, "Id", "Name", trainingDefinition.JobId);
            //ViewData["TrainingId"] = new SelectList(_context.TrainingDetails, "Id", "Id", trainingDefinition.TrainingId);
            //return View(trainingDefinition);
        }

        // POST: TrainingDefinitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainingDefinition"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrainingDefinitionViewModel trainingDefinition, IFormFile file)
        {
            if (id != trainingDefinition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (file == null && file.Length == 0)
                        return Content("file not selected");

                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

                    var filePath = Path.Combine(_appEnvironment.WebRootPath, "images\\TrainingMaterial");

                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);

                        trainingDefinition.TraningMaterial = fileName;
                    }

                    _context.Update(trainingDefinition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingDefinitionExists(trainingDefinition.Id))
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
            //ViewData["JobId"] = new SelectList(_context.JobTitle, "Id", "Name", trainingDefinition.JobId);
            //ViewData["TrainingId"] = new SelectList(_context.TrainingDetails, "Id", "Id", trainingDefinition.TrainingId);

            //ViewBag.TrainingId = trainingDefinition.TrainingId;
            trainingDefinition.loadLists(_context);
            return View(trainingDefinition);
        }

        // GET: TrainingDefinitions/Delete/5
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

            var trainingDefinition = await _context.TrainingDefinition
                .Include(t => t.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingDefinition == null)
            {
                return NotFound();
            }

            return View(trainingDefinition);
        }

        // POST: TrainingDefinitions/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingDefinition = await _context.TrainingDefinition.FindAsync(id);
            _context.TrainingDefinition.Remove(trainingDefinition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingDefinitionExists(int id)
        {
            return _context.TrainingDefinition.Any(e => e.Id == id);
        }
    }
}
