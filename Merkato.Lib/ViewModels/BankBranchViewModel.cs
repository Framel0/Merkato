using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class BankBranchViewModel: BankBranch
    {
        public List<SelectListItem> ActiveList { get; set; }

        public List<SelectListItem> BankList { get; set; }

        public string ActiveString { get; set; }

        public string BankName { get; set; }

        public BankBranchViewModel()
        {

        }

        public BankBranchViewModel(MerkatoDbContext context)
        {
            ActiveList = new List<SelectListItem>();
            BankList = new List<SelectListItem>();
            loadLists(context);
            Id = 0;
        }

        public void loadLists(MerkatoDbContext context)
        {

            ActiveList = context.ActiveList.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            BankList = context.Bank.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
        }

        public BankBranchViewModel(MerkatoDbContext context, BankBranch B) : this(context)
        {
            this.Id = B.Id;
            this.Code = B.Code;
            this.Name = B.Name;
            this.BankId = B.BankId;
            this.Active = B.Active;
        }
        public BankBranch GetModel()
        {
            BankBranch b = new BankBranch();
            b.Id = this.Id;
            b.Code = this.Code;
            b.Name = this.Name;
            b.BankId = this.BankId;
            b.Active = this.Active;

            return b;
        }

    }
}
