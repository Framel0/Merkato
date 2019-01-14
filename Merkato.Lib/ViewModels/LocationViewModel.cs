using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class LocationViewModel:Location
    {
        public List<SelectListItem> ActiveList { get; set; }

        public string ActiveString { get; set; }

        public LocationViewModel()
        {

        }

        public LocationViewModel(MerkatoDbContext context)
        {
            ActiveList = new List<SelectListItem>();

            loadLists(context);
            Id = 0;
        }

        public void loadLists(MerkatoDbContext context)
        {

            ActiveList = context.ActiveList.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
        }

        public LocationViewModel(MerkatoDbContext context, Location B) : this(context)
        {
            this.Id = B.Id;
            this.Code = B.Code;
            this.Name = B.Name;
            this.Status = B.Status;
        }
        public Location GetModel()
        {
            Location b = new Location();
            b.Id = this.Id;
            b.Code = this.Code;
            b.Name = this.Name;
            b.Status = this.Status;

            return b;
        }
    }
}
