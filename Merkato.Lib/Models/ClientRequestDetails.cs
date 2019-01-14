using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class ClientRequestDetails
    {
        public int Id { get; set; }
        public int? ClientRequestId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public int? AssignedAgentId { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public Agent AssignedAgent { get; set; }
        public ClientRequest ClientRequest { get; set; }
    }
}
