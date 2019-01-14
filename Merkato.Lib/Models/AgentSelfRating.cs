using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class AgentSelfRating
    {
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public DateTime? RatingDate { get; set; }
        public short? RatingTypeId { get; set; }
        public short Score { get; set; }
        public string UserId { get; set; }
        public DateTime? LastDateModified { get; set; }

        public Agent Agent { get; set; }
    }
}
