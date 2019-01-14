using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class AgentReportViewModel:Agent
    {
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> GenderList { get; set; }
        public AgentReportViewModel()
        {
                
        }
        public AgentReportViewModel(MerkatoDbContext _context)
        {
            DepartmentList = new List<SelectListItem>();
            GenderList = new List<SelectListItem>();


            loadLists(_context);
        }

        public void loadLists(MerkatoDbContext _context)
        {
            SelectListItem all = new SelectListItem() { Text = "All", Value = "0" };
            DepartmentList = _context.Department.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            DepartmentList.Add(all);
            GenderList = _context.Gender.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            GenderList.Add(all);
        }

        public AgentReportViewModel(MerkatoDbContext _context, Agent E) : this(_context)
        {
            this.DepartmentId = E.DepartmentId;
            this.Gender = E.Gender;       
        }
        public Agent GetModel()
        {
            Agent e = new Agent();

            e.DepartmentId = this.DepartmentId;
            e.Gender = this.Gender;
            


            return e;
        }
    }
}
