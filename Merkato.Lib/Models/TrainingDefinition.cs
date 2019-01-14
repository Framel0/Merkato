using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class TrainingDefinition
    {
        public TrainingDefinition()
        {
            TrainingDetails = new HashSet<TrainingDetails>();
        }

        public int Id { get; set; }
        public int? JobId { get; set; }
        public string TrainingName { get; set; }
        public string TraningMaterial { get; set; }

        public JobTitle Job { get; set; }
        public ICollection<TrainingDetails> TrainingDetails { get; set; }
    }
}
