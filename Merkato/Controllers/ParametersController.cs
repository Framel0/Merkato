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
    public class ParametersController : Controller
    {
        private readonly MerkatoDbContext _context;

        public ParametersController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: Parameters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parameters.ToListAsync());
        }

        // GET: Parameters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameters = await _context.Parameters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parameters == null)
            {
                return NotFound();
            }

            return View(parameters);
        }

        // GET: Parameters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Value")] Parameters parameters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parameters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parameters);
        }

        // GET: Parameters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameters = await _context.Parameters.FindAsync(id);
            if (parameters == null)
            {
                return NotFound();
            }
            return View(parameters);
        }

        // POST: Parameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Value")] Parameters parameters)
        {
            if (id != parameters.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parameters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParametersExists(parameters.Id))
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
            return View(parameters);
        }

        // GET: Parameters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameters = await _context.Parameters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parameters == null)
            {
                return NotFound();
            }

            return View(parameters);
        }

        // POST: Parameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parameters = await _context.Parameters.FindAsync(id);
            _context.Parameters.Remove(parameters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParametersExists(int id)
        {
            return _context.Parameters.Any(e => e.Id == id);
        }

        public IActionResult SmtpConfiguration()
        {
            var smtpConfig = Parameters.Get<SmtpConfig>(SmtpConfig.KEY_NAME, _context);
            if (smtpConfig == null)
            {
                smtpConfig = new SmtpConfig();
            }
            return View(smtpConfig);
        }

        [HttpPost]
        public IActionResult SmtpConfiguration(SmtpConfig smtpConfig)
        {
            Parameters.Save(smtpConfig, SmtpConfig.KEY_NAME, _context);
            //return View(smtpConfig);
            return RedirectToAction(nameof(Index));
        }

    }
}
