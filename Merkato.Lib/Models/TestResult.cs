using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class TestResult
    {
        public int Id { get; set; }
        public int? TrainingId { get; set; }
        public int? AgentId { get; set; }
        public int? Score { get; set; }
    }
}
