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
    public class BankBranchesController : Controller
    {
        private readonly MerkatoDbContext _context;

        public BankBranchesController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: BankBranches
        public async Task<IActionResult> Index()
        {
            var data = await _context.BankBranch.Include(a => a.Bank).
           Select(b => new BankBranchViewModel
           {
               Id = b.Id,
               Code = b.Code,
               Name = b.Name,
               BankId = b.BankId,
               BankName = b.Bank.Name,
               ActiveString = b.Active == 1 ? "active" : "inactive"
           }).ToListAsync();
            return View(data);

            //return View(await _context.BankBranch.ToListAsync());
        }

        // GET: BankBranches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            BankBranchViewModel vm;
            if (id == 0)
            {
                vm = new BankBranchViewModel(_context);
            }
            else
            {
                var bankBranch = await _context.BankBranch
                              .FirstOrDefaultAsync(m => m.Id == id);
                if (bankBranch == null)
                {
                    return NotFound();
                }

                vm = new BankBranchViewModel(_context, bankBranch);
            }
          
            return View(vm);
        }

        // GET: BankBranches/Create
        public IActionResult Create()
        {
            BankBranchViewModel vm;
            vm = new BankBranchViewModel(_context);
            return View(vm);
        }

        // POST: BankBranches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankBranchViewModel bankBranch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankBranch.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            bankBranch.loadLists(_context);
            return View(bankBranch);
        }

        // GET: BankBranches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            BankBranchViewModel vm;
            if (id == 0)
            {
                vm = new BankBranchViewModel(_context);
            }
            else
            {
                var bankBranch = await _context.BankBranch.FindAsync(id);
                if (bankBranch == null)
                {
                    return NotFound();
                }
                vm = new BankBranchViewModel(_context, bankBranch);
            }
           
            return View(vm);
        }

        // POST: BankBranches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BankBranchViewModel bankBranch)
        {
            if (id != bankBranch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankBranch.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankBranchExists(bankBranch.Id))
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
            bankBranch.loadLists(_context);
            return View(bankBranch);
        }

        // GET: BankBranches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankBranch = await _context.BankBranch
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankBranch == null)
            {
                return NotFound();
            }

            return View(bankBranch);
        }

        // POST: BankBranches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankBranch = await _context.BankBranch.FindAsync(id);
            _context.BankBranch.Remove(bankBranch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankBranchExists(int id)
        {
            return _context.BankBranch.Any(e => e.Id == id);
        }
    }
}
