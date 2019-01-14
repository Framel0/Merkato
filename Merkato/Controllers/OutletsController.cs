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
    public class OutletsController : Controller
    {
        private readonly MerkatoDbContext _context;

        public OutletsController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: Outlets
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.LocationId = id;

            var location = _context.Location.FirstOrDefault(c => c.Id == id);
            if (location == null)
                return NotFound();

            ViewBag.LocationName = $"{location.Name}";

            var data = await _context.Outlet.Where(c=>c.LocationId==id).Include(r=>r.Location).
       Select(b => new OutLetViewModel
       {
           Id = b.Id,
           Code = b.Code,
           Name = b.Name,
           ContactName = b.ContactName,
           ContactPhoneNber = b.ContactPhoneNber,
           Supervisor = b.Supervisor,
           Latitude = b.Latitude,
           Longitude = b.Longitude,
           LocationName = b.Location.Name,
           ActiveString = b.Status == 1 ? "active" : "inactive"
       }).ToListAsync();
            return View(data);
            //return View(await _context.Outlet.ToListAsync());
        }

        // GET: Outlets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outlet = await _context.Outlet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outlet == null)
            {
                return NotFound();
            }
            ViewBag.LocationId = outlet.LocationId;
            return View(outlet);
        }

        // GET: Outlets/Create
        public IActionResult Create(int id)
        {
            OutLetViewModel vm;           
            vm = new OutLetViewModel(_context);
            vm.LocationId = id;
            ViewBag.LocationId = id;
            return View(vm);
        }

        // POST: Outlets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, OutLetViewModel outlet)
        {
            if (ModelState.IsValid)
            {
                outlet.LocationId = id;
                ViewBag.LocationId = id;
                _context.Add(outlet.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            outlet.loadLists(_context);
            return View(outlet);
        }

        // GET: Outlets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            OutLetViewModel vm;
            if (id == 0)
            {
                vm = new OutLetViewModel(_context);
            }
            else
            {
                var outlet = await _context.Outlet.FindAsync(id);
                if (outlet == null)
                {
                    return NotFound();
                }

                vm = new OutLetViewModel(_context, outlet);
                ViewBag.LocationId = outlet.LocationId;
            }

           
            return View(vm);
        }

        // POST: Outlets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OutLetViewModel outlet)
        {
           
            if (id != outlet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outlet.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutletExists(outlet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {id=outlet.LocationId});
            }
            ViewBag.LocationId = outlet.LocationId;
            outlet.loadLists(_context);
            return View(outlet);
        }

        // GET: Outlets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outlet = await _context.Outlet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (outlet == null)
            {
                return NotFound();
            }
            ViewBag.LocationId = outlet.LocationId;
            return View(outlet);
        }

        // POST: Outlets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outlet = await _context.Outlet.FindAsync(id);
            _context.Outlet.Remove(outlet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OutletExists(int id)
        {
            return _context.Outlet.Any(e => e.Id == id);
        }
    }
}
