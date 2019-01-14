using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Merkato.Controllers
{
    public class ClientAccountingController : Controller
    {
        private readonly MerkatoDbContext _context;

        public ClientAccountingController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: BatchListViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Batch.ToListAsync());
        }

        // GET: InvoiceViewModels
        public async Task<IActionResult> InvoiceList()
        {
            return View(await _context.Invoice.ToListAsync());
        }


    }
}