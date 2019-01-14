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
    public class BanksController : Controller
    {
        private readonly MerkatoDbContext _context;

        public BanksController(MerkatoDbContext context)
        {
            _context = context;
        }

        //GET: Banks
        public async Task<IActionResult> Index()
        {
            var data = await _context.Bank.
            Select(b => new BankViewModel
            {
                Id = b.Id,
                Code = b.Code,
                Name = b.Name,
                ActiveString = b.Active == 1 ? "active" : "inactive"
            }).ToListAsync();
            return View(data);
        }

        //public async Task<IActionResult> Index()
        //{

        //    return View(await _context.Bank.ToListAsync());
        //}

        // GET: Banks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _context.Bank
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        // GET: Banks/Create
        public IActionResult Create()
        {
            BankViewModel vm;
            vm = new BankViewModel(_context);
            return View(vm);
        }

        // POST: Banks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankViewModel bank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bank.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            bank.loadLists(_context);
            return View(bank);
        }

        // GET: Banks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            BankViewModel vm;
            if (id == 0)
            {
                vm = new BankViewModel(_context);
            }
            else
            {
                var bank = await _context.Bank.FindAsync(id);
                if (bank == null)
                {
                    return NotFound();
                }
                vm = new BankViewModel(_context, bank);
            }

            return View(vm);
        }

        // POST: Banks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BankViewModel bank)
        {
            //if (id != bank.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bank.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankExists(bank.Id))
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
            bank.loadLists(_context);
            return View(bank);
        }

        // GET: Banks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _context.Bank
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        // POST: Banks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bank = await _context.Bank.FindAsync(id);
            _context.Bank.Remove(bank);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankExists(int id)
        {
            return _context.Bank.Any(e => e.Id == id);
        }
    }
}
