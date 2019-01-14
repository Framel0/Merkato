using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class PerformanceAppraisal
    {
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public DateTime? RatingDate { get; set; }
        public string RatingType { get; set; }
        public short Score { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }
    }
}
