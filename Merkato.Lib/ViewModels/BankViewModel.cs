using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class BankViewModel : Bank
    {
        public List<SelectListItem> ActiveList { get; set; }

        public string ActiveString { get; set; }

        public BankViewModel()
        {

        }

        public BankViewModel(MerkatoDbContext context)
        {
            ActiveList = new List<SelectListItem>();

            loadLists(context);
            Id = 0;
        }

        public void loadLists(MerkatoDbContext context)
        {

            ActiveList = context.ActiveList.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
        }

        public BankViewModel(MerkatoDbContext context, Bank B) : this(context)
        {
            this.Id = B.Id;
            this.Code = B.Code;
            this.Name = B.Name;
            this.Active = B.Active;
        }
        public Bank GetModel()
        {
            Bank b = new Bank();
            b.Id = this.Id;
            b.Code = this.Code;
            b.Name = this.Name;
            b.Active = this.Active;

            return b;
        }
    }

}
