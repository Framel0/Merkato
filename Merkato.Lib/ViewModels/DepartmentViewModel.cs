using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class DepartmentViewModel:Department
    {
        public List<SelectListItem> ActiveList { get; set; }

        public string ActiveString { get; set; }

        public DepartmentViewModel()
        {

        }

        public DepartmentViewModel(MerkatoDbContext context)
        {
            ActiveList = new List<SelectListItem>();

            loadLists(context);
            Id = 0;
        }

        public void loadLists(MerkatoDbContext context)
        {

            ActiveList = context.ActiveList.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
        }

        public DepartmentViewModel(MerkatoDbContext context, Department B) : this(context)
        {
            this.Id = B.Id;
            this.Code = B.Code;
            this.Name = B.Name;
            this.Active = B.Active;
        }
        public Department GetModel()
        {
            Department b = new Department();
            b.Id = this.Id;
            b.Code = this.Code;
            b.Name = this.Name;
            b.Active = this.Active;

            return b;
        }
    }
}
