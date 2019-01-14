using System;
using System.Collections.Generic;

namespace Merkato.Lib.Models
{
    public partial class TrainingDetails
    {
        public int Id { get; set; }
        public int? TrainingId { get; set; }
        public string Question { get; set; }
        public string AnswerValues { get; set; }
        public short? AnswerPoint { get; set; }
        public string AnswerCorrect { get; set; }

        public TrainingDefinition Training { get; set; }
    }
}
