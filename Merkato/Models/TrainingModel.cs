using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merkato.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingModel
    {
        public int? TrainingId { get; set; }
        public int? JobId { get; set; }
        public string Question { get; set; }
        public string AnswerValues { get; set; }
        public short? AnswerPoint { get; set; }
        public string AnswerCorrect { get; set; }
        public string TrainingName { get; set; }
        public string TraningMaterial { get; set; }
    }
}
