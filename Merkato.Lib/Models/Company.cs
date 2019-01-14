using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Tin { get; set; }
        public string Ssf { get; set; }
        public string EmailFrom { get; set; }
        public string EmailServer { get; set; }
        public string EmailPort { get; set; }
        public string RequireSsl { get; set; }
        public string Logo { get; set; }
        public decimal? NbDecimal { get; set; }
        public string ThousandSeparator { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }
    }
}
