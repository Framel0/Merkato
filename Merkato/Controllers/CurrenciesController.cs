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
    public class CurrenciesController : Controller
    {
        private readonly MerkatoDbContext _context;

        public CurrenciesController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: Currencies
        public async Task<IActionResult> Index()
        {
            var data = await _context.Currency.
Select(b => new CurrencyViewModel
{
Id = b.Id,
Code = b.Code,
Name = b.Name,
ActiveString = b.Active == 1 ? "active" : "inactive"
}).ToListAsync();
            return View(data);
            //return View(await _context.Currency.ToListAsync());
        }

        // GET: Currencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // GET: Currencies/Create
        public IActionResult Create()
        {
            CurrencyViewModel vm;
            vm = new CurrencyViewModel(_context);
            return View(vm);
        }

        // POST: Currencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CurrencyViewModel currency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(currency.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            currency.loadLists(_context);
            return View(currency);
        }

        // GET: Currencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            CurrencyViewModel vm;
            if (id == 0)
            {
                vm = new CurrencyViewModel(_context);
            }
            else
            {
                var currency = await _context.Currency.FindAsync(id);
                if (currency == null)
                {
                    return NotFound();
                }
                vm = new CurrencyViewModel(_context, currency);
            }
 
            return View(vm);
        }

        // POST: Currencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CurrencyViewModel currency)
        {
            if (id != currency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(currency.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyExists(currency.Id))
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
            currency.loadLists(_context);
            return View(currency);
        }

        // GET: Currencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currency = await _context.Currency.FindAsync(id);
            _context.Currency.Remove(currency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyExists(int id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}
