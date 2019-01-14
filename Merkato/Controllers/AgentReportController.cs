using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Merkato.Controllers
{
    public class AgentReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}