using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class Batch
    {
        public int Id { get; set; }
        public string BatchNo { get; set; }
        public int? ActivityId { get; set; }
        public string ClientName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }
    }
}
