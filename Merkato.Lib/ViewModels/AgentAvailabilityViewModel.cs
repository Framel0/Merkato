using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class AgentAvailabilityViewModel:AgentAvailability
    {

        public List <SelectListItem> AgentList { get; set; }
        public string AgentName { get; set; }

        public string DateRange { get; set; }

        public string DateRangeSelector { get; set; }

        public String StartDateString
        {

            get
            {
                return StartDate.ToString("dd-MM-yyyy");
            }
            set
            {
                StartDate = DateTime.ParseExact(value, "dd-MM-yyyy", null);

            }
        }

        public String EndDateString
        {
            get
            {
                return EndDate.ToString("dd-MM-yyyy");

            }
            set
            {
                EndDate = DateTime.ParseExact(value, "dd-MM-yyyy", null);

            }
        }

        public AgentAvailabilityViewModel()
        {

        }

        public AgentAvailabilityViewModel(MerkatoDbContext context)
        {
            AgentList = new List<SelectListItem>();
            loadLists(context);
            Id = 0;
        }

        public void loadLists(MerkatoDbContext context)
        {
            AgentList = context.Agent.Select(p => new SelectListItem() { Text = p.FirstName + " " + p.SurName , Value = p.Id.ToString() }).ToList();

        }

        public AgentAvailabilityViewModel(MerkatoDbContext context, AgentAvailability B) : this(context)
        {
            this.Id = B.Id;
            this.AgentId = B.AgentId;
            this.BatchNo = B.BatchNo;
            this.StartDate = B.StartDate;
            this.EndDate = B.EndDate;
            this.Days = B.Days;
            this.Shift1 = B.Shift1;
            this.Shift2 = B.Shift2;
            this.Shift3 = B.Shift3;
            this.Shift4 = B.Shift4;

        }
        public AgentAvailability GetModel()
        {
            AgentAvailability b = new AgentAvailability();
            b.Id = this.Id;
            b.AgentId = this.AgentId;
            b.BatchNo = this.BatchNo;
            b.StartDate = this.StartDate;
            b.EndDate = this.EndDate;
            b.Days = this.Days;
            b.Shift1 = this.Shift1;
            b.Shift2 = this.Shift2;
            b.Shift3 = this.Shift3;
            b.Shift4 = this.Shift4;

            return b;
        }
    }
}
