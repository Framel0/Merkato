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
    public class ClientProductsController : Controller
    {
        private readonly MerkatoDbContext _context;

        public ClientProductsController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: ClientProducts
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.ClientId = id;

            var client = _context.Client.FirstOrDefault(c => c.Id == id);
            if (client == null)
                return NotFound();

            ViewBag.ClientName = $"{client.ClientName}";
            var data = await _context.ClientProduct.Where(c => c.ClientId == id).Include(c => c.Client).
                Select(c => new ClientProductViewModel
                {
                    Id = c.Id,
                    ClientId=c.ClientId,
                    ProductName = c.ProductName,
                    ClientName = c.Client.ClientName

                }).ToListAsync();

            return View(data);
        }

        // GET: ClientProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientProduct = await _context.ClientProduct
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientProduct == null)
            {
                return NotFound();
            }
            ViewBag.ClientId = clientProduct.ClientId;
            return View(clientProduct);
        }

        // GET: ClientProducts/Create
        public IActionResult Create(int id)
        {
            ClientProductViewModel vm;
            vm = new ClientProductViewModel(_context);
            vm.ClientId = id;
            ViewBag.ClientId = id;
            //ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id");
            return View(vm);
        }

        // POST: ClientProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, ClientProductViewModel clientProduct)
        {
            if (ModelState.IsValid)
            {
                clientProduct.ClientId = id;
                ViewBag.ClientId = id;
                clientProduct.Id = 0;
                _context.Add(clientProduct.GetModel());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = clientProduct.ClientId });
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", clientProduct.ClientId);          
            return View(clientProduct);
        }

        // GET: ClientProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ClientProductViewModel vm;
            if (id == 0)
            {
                vm = new ClientProductViewModel(_context);
            }
            else
            {
                var clientProduct = await _context.ClientProduct.FindAsync(id);
                if (clientProduct == null)
                {
                    return NotFound();
                }
                vm = new ClientProductViewModel(_context, clientProduct);
                ViewBag.ClientId = clientProduct.ClientId;
            }
            
            //ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", clientProduct.ClientId);
            return View(vm);
        }

        // POST: ClientProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientProductViewModel clientProduct)
        {
            if (id != clientProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientProduct.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientProductExists(clientProduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = clientProduct.ClientId });
            }
            ViewBag.ClientId = clientProduct.ClientId;
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", clientProduct.ClientId);
            return View(clientProduct);
        }

        // GET: ClientProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientProduct = await _context.ClientProduct
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientProduct == null)
            {
                return NotFound();
            }
            ViewBag.ClientId = clientProduct.ClientId;
            return View(clientProduct);
        }

        // POST: ClientProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientProduct = await _context.ClientProduct.FindAsync(id);
            _context.ClientProduct.Remove(clientProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientProductExists(int id)
        {
            return _context.ClientProduct.Any(e => e.Id == id);
        }
    }
}
