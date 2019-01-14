using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class Invoice
    {
        public int Id { get; set; }
        public string BatchNo { get; set; }
        public string ClientName { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? Amount { get; set; }
        public int? Status { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }
    }
}
