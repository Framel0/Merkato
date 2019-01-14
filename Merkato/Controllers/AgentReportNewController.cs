using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merkato.Lib.Models;
using Merkato.Lib.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Merkato.Controllers
{
    public class AgentReportNewController : Controller
    {
        private readonly MerkatoDbContext _context;

        public AgentReportNewController(MerkatoDbContext context)
        {
            _context = context;
        }
        // GET: Report/
        public IActionResult Index()
        {
            AgentReportViewModel vm;

            vm = new AgentReportViewModel(_context);

            return View(vm);
        }
    }
}