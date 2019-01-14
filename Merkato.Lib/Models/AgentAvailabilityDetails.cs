using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class AgentAvailabilityDetails
    {
        public int Id { get; set; }
        public int? AgentAvailabillityId { get; set; }
        public DateTime? Date { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public AgentAvailability AgentAvailabillity { get; set; }
    }
}
