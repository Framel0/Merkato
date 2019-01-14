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
    public class ProductMechanismController : Controller
    {
        private readonly MerkatoDbContext _context;

        public ProductMechanismController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: ProductMechanism

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.MechanismId = id;

            var mechanism = _context.Mechanism.FirstOrDefault(c => c.Id == id);
            ViewBag.clientId = mechanism.ClientId;
            if (mechanism == null)
                return NotFound();


            ViewBag.MechanismName = $"{mechanism.Name}";
            var data = await _context.ProductMechanism
                .Where(p=>p.MechanismId==id)
                .Include(p => p.Product)
                .Include(p => p.Mechanism)
                .Select(p => new ProductMechanismViewModel
                {
                    Id = p.Id,
                    ProductName = p.Product.ProductName,
                    MechanismName = p.Mechanism.Name,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    ProductOrder = p.ProductOrder,
                    MechanismId = p.MechanismId
                }).ToListAsync();

            return View(data);

        }

        // GET: ProductMechanism/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productMechanism = await _context.ProductMechanism
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productMechanism == null)
            {
                return NotFound();
            }

            return View(productMechanism);
        }

        // GET: ProductMechanism/Create
        public IActionResult Create(int id)
        {
            ProductMechanismViewModel vm;
            vm = new ProductMechanismViewModel(_context);
            ViewData["ProductId"] = new SelectList(_context.ClientProduct, "Id", "ProductName");
            return View(vm);
        }

        // POST: ProductMechanism/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, ProductMechanismViewModel productMechanism)
        {
            if (ModelState.IsValid)
            {
                productMechanism.MechanismId = id;
                _context.Add(productMechanism.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", productMechanism.ClientId);
            ViewData["MechanismId"] = new SelectList(_context.ProductMechanism, "Id", "MechanismName", productMechanism.ProductId);
            productMechanism.loadLists(_context);
            return View(productMechanism);
        }

        // GET: ProductMechanism/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ProductMechanismViewModel vm;
            if (id == 0)
            {
                vm = new ProductMechanismViewModel(_context);
            }
            else
            {
                var productMechanism = await _context.ProductMechanism.FindAsync(id);
                if (productMechanism == null)
                {
                    return NotFound();
                }
                vm = new ProductMechanismViewModel(_context, productMechanism);
            }

            return View(vm);
        }

        // POST: ProductMechanism/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductMechanismViewModel productMechanism)
        {
            if (id != productMechanism.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productMechanism.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductMechanismExists(productMechanism.Id))
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
            //ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", productMechanism.ClientId);
            ViewData["MechanismId"] = new SelectList(_context.ProductMechanism, "Id", "MechanismName", productMechanism.ProductId);
            productMechanism.loadLists(_context);
            return View(productMechanism);
        }

        // GET: ProductMechanism/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productMechanism = await _context.ProductMechanism
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productMechanism == null)
            {
                return NotFound();
            }

            return View(productMechanism);
        }

        // POST: ProductMechanism/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productMechanism = await _context.ProductMechanism.FindAsync(id);
            _context.ProductMechanism.Remove(productMechanism);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductMechanismExists(int id)
        {
            return _context.ProductMechanism.Any(e => e.Id == id);
        }

        public IActionResult ProductMechanism()
        {
            return View();
        }
    }
}
