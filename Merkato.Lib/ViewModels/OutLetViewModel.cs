using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class OutLetViewModel:Outlet
    {
        public List<SelectListItem> ActiveList { get; set; }
        public List<SelectListItem> LocationList { get; set; }

        public string LocationName { get; set; }

        public string ActiveString { get; set; }

        public OutLetViewModel()
        {

        }

        public OutLetViewModel(MerkatoDbContext context)
        {
            ActiveList = new List<SelectListItem>();
            LocationList = new List<SelectListItem>();
            



            loadLists(context);
            Id = 0;
        }

        public void loadLists(MerkatoDbContext context)
        {

            ActiveList = context.ActiveList.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            LocationList = context.Location.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
        }

        public OutLetViewModel(MerkatoDbContext context, Outlet B) : this(context)
        {
            this.Id = B.Id;
            this.Code = B.Code;
            this.Name = B.Name;
            this.ContactName = B.ContactName;
            this.ContactPhoneNber = B.ContactPhoneNber;
            this.Supervisor = B.Supervisor;
            this.Latitude = B.Latitude;
            this.Longitude = B.Longitude;
            this.LocationId = B.LocationId;
            this.Status = B.Status;
        }
        public Outlet GetModel()
        {
            Outlet b = new Outlet();
            b.Id = this.Id;
            b.Code = this.Code;
            b.Name = this.Name;
            b.ContactName = this.ContactName;
            b.ContactPhoneNber = this.ContactPhoneNber;
            b.Supervisor = this.Supervisor;
            b.Latitude = this.Latitude;
            b.Longitude = this.Longitude;
            b.LocationId = this.LocationId;
            b.Status = this.Status;

            return b;
        }
    }
}
