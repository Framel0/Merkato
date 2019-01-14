using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merkato.Lib.Models;
using Merkato.Lib.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Merkato.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AgentAccountingController : Controller
    {
        private readonly MerkatoDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public AgentAccountingController(MerkatoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            List<Agent> rawList = await _context.Agent.ToListAsync();
            List<AgentStatus> agentStatuses = await _context.AgentStatus.ToListAsync();

            List<AgentAccountingViewModel> list = new List<AgentAccountingViewModel>();
            foreach (var item in rawList)
            {
                list.Add(new AgentAccountingViewModel(item, agentStatuses));
            }

            return View(list);
        }
    }
}