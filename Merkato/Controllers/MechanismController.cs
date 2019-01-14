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
    public class MechanismController : Controller
    {
        private readonly MerkatoDbContext _context;

        public MechanismController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: Mechanism
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.ClientId = id;

            var client = _context.Client.FirstOrDefault(c => c.Id == id);
            if (client == null)
                return NotFound();

            ViewBag.ClientName = $"{client.ClientName}";
            var data = await _context.Mechanism.Where(c => c.ClientId == id).Include(c => c.Client).
                Select(c => new MechanismViewModel
                {
                    Id = c.Id,
                    ClientId = c.ClientId,
                    Price = c.Price,
                    Name = c.   Name,
                    ClientName = c.Client.ClientName

                }).ToListAsync();

            return View(data);
            //var merkatoDbContext = _context.Mechanism.Include(m => m.Client);
            //return View(await merkatoDbContext.ToListAsync());
        }

        // GET: Mechanism/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mechanism = await _context.Mechanism
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mechanism == null)
            {
                return NotFound();
            }
            ViewBag.ClientId = mechanism.ClientId;
            return View(mechanism);
       
        }

        // GET: Mechanism/Create
        public IActionResult Create(int id)
        {
            MechanismViewModel vm;
            vm = new MechanismViewModel(_context);
            vm.ClientId = id;
            ViewBag.ClientId = id;
            //ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id");
            return View(vm);
        }

        // POST: Mechanism/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, MechanismViewModel mechanism)
        {
            if (ModelState.IsValid)
            {
                mechanism.ClientId = id;
                ViewBag.ClientId = id;
                mechanism.Id = 0;
                _context.Add(mechanism.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = mechanism.ClientId });
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", mechanism.ClientId);
            return View(mechanism);
        }

        // GET: Mechanism/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            MechanismViewModel vm;
            if (id == 0)
            {
                vm = new MechanismViewModel(_context);
            }
            else
            {
                var mechanism = await _context.Mechanism.FindAsync(id);
                if (mechanism == null)
                {
                    return NotFound();
                }
                vm = new MechanismViewModel(_context, mechanism);
                ViewBag.ClientId = mechanism.ClientId;
            }

            //ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", clientProduct.ClientId);
            return View(vm);
        }

        // POST: Mechanism/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MechanismViewModel mechanism)
        {
            if (id != mechanism.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mechanism.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MechanismExists(mechanism.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = mechanism.ClientId });
            }
            ViewBag.ClientId = mechanism.ClientId;
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", mechanism.ClientId);
            return View(mechanism);
        }

        // GET: Mechanism/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mechanism = await _context.Mechanism
                .Include(m => m.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mechanism == null)
            {
                return NotFound();
            }

            return View(mechanism);
        }

        // POST: Mechanism/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mechanism = await _context.Mechanism.FindAsync(id);
            _context.Mechanism.Remove(mechanism);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MechanismExists(int id)
        {
            return _context.Mechanism.Any(e => e.Id == id);
        }
    }
}
