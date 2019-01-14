using Merkato.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merkato.Lib.ViewModels
{
    public class TrainingDetailsViewModel:TrainingDetails
    {
        public TrainingDetailsViewModel()
        {

        }

        public TrainingDetailsViewModel(MerkatoDbContext context)
        {
            Id = 0;
        }

        public TrainingDetailsViewModel(MerkatoDbContext context, TrainingDetails B) : this(context)
        {
            this.Id = B.Id;
            this.TrainingId = B.TrainingId;
            this.Question = B.Question;
            this.AnswerValues = B.AnswerValues;
            this.AnswerPoint = B.AnswerPoint;
            this.AnswerCorrect = B.AnswerCorrect;
        }
        public TrainingDetails GetModel()
        {
            TrainingDetails b = new TrainingDetails();
            b.Id = this.Id;
            b.TrainingId = this.TrainingId;
            b.Question = this.Question;
            b.AnswerValues = this.AnswerValues;
            b.AnswerPoint = this.AnswerPoint;
            b.AnswerCorrect = this.AnswerCorrect;

            return b;
        }
    }
}
