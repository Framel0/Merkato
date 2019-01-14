using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public string BatchId { get; set; }
        public int? AgentId { get; set; }
        public DateTime? AvailabilityDate { get; set; }
        public string Activity1 { get; set; }
        public string Activity2 { get; set; }
        public string Activity3 { get; set; }
        public string Shift1 { get; set; }
        public string Shift2 { get; set; }
        public string Shift3 { get; set; }
        public string Ic { get; set; }
        public short? Status { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }
    }
}
