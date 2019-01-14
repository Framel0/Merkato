using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class ActivityAssignment
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public int? AgentId { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public Agent Agent { get; set; }
    }
}
