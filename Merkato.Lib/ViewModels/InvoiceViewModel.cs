using  Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public String BatchNo { get; set; }
        public string ClientName { get; set; }
         public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public InvoiceViewModel()
        {

        }

        public InvoiceViewModel(MerkatoDbContext context)
        {
        
            loadLists(context);
        }

        public void loadLists(MerkatoDbContext context)
        {
            
        }
        public InvoiceViewModel(MerkatoDbContext context, ClientRequest activity) : this(context)
        {
            
        }
        
    }
}
