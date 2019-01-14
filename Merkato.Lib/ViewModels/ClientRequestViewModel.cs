using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class ClientRequestViewModel:ClientRequest
    {
        public List<SelectListItem> ClientList { get; set; }

        public List<SelectListItem> OutletList { get; set; }

        public List<SelectListItem> MechanismList { get; set; }

        public List<SelectListItem> GenderList { get; set; }

        public List<SelectListItem> MaritalStatusList { get; set; }

        public List<SelectListItem> SkillsList { get; set; }

        public List<SelectListItem> LanguageList { get; set; }
        public List<SelectListItem> GradeList { get; set; }

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

        public string ClientName { get; set; }
        public string OutletName { get; set; }
        public string MechanismName { get; set; }
        public string LanguageName { get; set; }
        public string skillName { get; set; }
        public string GenderName { get; set; }
        public string MaritalStatusName { get; set; }

        public string GradeName { get; set; }





        public ClientRequestViewModel()
        {
                
        }

        public ClientRequestViewModel(MerkatoDbContext context)
        {
            ClientList = new List<SelectListItem>();
            OutletList = new List<SelectListItem>();
            MechanismList = new List<SelectListItem>();

            GenderList = new List<SelectListItem>();          
            SkillsList = new List<SelectListItem>();
            LanguageList = new List<SelectListItem>();
            GradeList = new List<SelectListItem>();

            loadLists(context);
        }

        public void loadLists(MerkatoDbContext context)
        {
            SelectListItem all = new SelectListItem() { Text = "(All)", Value = "0" };
            ClientList = context.Client.Select(p => new SelectListItem() { Text = p.ClientName, Value = p.Id.ToString() }).ToList();
            OutletList = context.Outlet.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            GenderList = context.Gender.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            GenderList.Add(all);
            SkillsList = context.SkillsProficiency.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            SkillsList.Add(all);
            LanguageList = context.LanguageProficiency.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            LanguageList.Add(all);
            GradeList = context.AgentGrade.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            GradeList.Add(all);

            var query = (from PM in context.ProductMechanism
                         join M in context.Mechanism on PM.MechanismId equals M.Id
                         select new { ProductMechanism = PM, Mechanism = M })
                        .Select(p => new MechanismModel { Name = p.Mechanism.Name, Id = p.ProductMechanism.MechanismId }).ToList();

            MechanismList = query.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();

        }
        public ClientRequestViewModel(MerkatoDbContext context, ClientRequest activity):this(context)
        {
            this.Id = activity.Id;
            this.ClientId = activity.ClientId;
            this.OutletId = activity.OutletId;
            this.MechanismId = activity.MechanismId;
            this.StartDate = activity.StartDate;
            this.EndDate = activity.EndDate;
            this.Days = activity.Days;
            this.BatchNo = activity.BatchNo;
            this.NbAgentShift1 = activity.NbAgentShift1;
            this.NbAgentShift2 = activity.NbAgentShift2;
            this.NbAgentShift3 = activity.NbAgentShift3;
            this.NbAgentShift4 = activity.NbAgentShift4;
            this.Age = activity.Age;
            this.GenderId = activity.GenderId;
            this.SkillId = activity.SkillId;
            this.LanguageId = activity.LanguageId;
            this.GradeId = activity.GradeId;
        }
        public ClientRequest GetModel()
        {
         

            ClientRequest a = new ClientRequest();
            a.Id = this.Id;
            a.ClientId = this.ClientId;
            a.OutletId = this.OutletId;
            a.MechanismId = this.MechanismId;
            a.StartDate = this.StartDate;
            a.EndDate = this.EndDate;
            a.Days = this.Days;
            a.BatchNo = this.BatchNo;
            a.NbAgentShift1 = this.NbAgentShift1;
            a.NbAgentShift2 = this.NbAgentShift2;
            a.NbAgentShift3 = this.NbAgentShift3;
            a.NbAgentShift4 = this.NbAgentShift4;
            a.Age = this.Age;
            a.GenderId = this.GenderId;
            a.SkillId = this.SkillId;
            a.LanguageId = this.LanguageId;
            a.GradeId = this.GradeId;

            return a;
        }
    }
}
