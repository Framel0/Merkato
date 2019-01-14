using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class AgentAvailability
    {
        public AgentAvailability()
        {
            AgentAvailabilityDetails = new HashSet<AgentAvailabilityDetails>();
        }

        public int Id { get; set; }
        public string BatchNo { get; set; }
        public int? AgentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Days { get; set; }
        public bool Shift1 { get; set; }
        public bool Shift2 { get; set; }
        public bool Shift3 { get; set; }
        public bool Shift4 { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public Agent Agent { get; set; }
        public ICollection<AgentAvailabilityDetails> AgentAvailabilityDetails { get; set; }
    }
}
