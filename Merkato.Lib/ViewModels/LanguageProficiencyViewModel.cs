using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class LanguageProficiencyViewModel: LanguageProficiency
    {
        public List<SelectListItem> ActiveList { get; set; }

        public string ActiveString { get; set; }

        public LanguageProficiencyViewModel()
        {

        }

        public LanguageProficiencyViewModel(MerkatoDbContext context)
        {
            ActiveList = new List<SelectListItem>();

            loadLists(context);
            Id = 0;
        }

        public void loadLists(MerkatoDbContext context)
        {

            ActiveList = context.ActiveList.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
        }

        public LanguageProficiencyViewModel(MerkatoDbContext context, LanguageProficiency B) : this(context)
        {
            this.Id = B.Id;
            this.Code = B.Code;
            this.Name = B.Name;
            this.Description = B.Description;
            this.Active = B.Active;
        }
        public LanguageProficiency GetModel()
        {
            LanguageProficiency b = new LanguageProficiency();
            b.Id = this.Id;
            b.Code = this.Code;
            b.Name = this.Name;
            b.Description = this.Description;
            b.Active = this.Active;

            return b;
        }
    }
}
