using Merkato.Lib.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merkato.Lib.ViewModels
{
  public class TrainingDefinitionViewModel:TrainingDefinition
    {
        public List<SelectListItem> JobList { get; set; }

        public string JobName { get; set; }
        public TrainingDefinitionViewModel()
        {

        }

        public TrainingDefinitionViewModel(MerkatoDbContext context)
        {

            JobList = new List<SelectListItem>();

            loadLists(context);
            Id = 0;
        }

        public void loadLists(MerkatoDbContext context)
        {
            JobList = context.JobTitle.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
        }

        public TrainingDefinitionViewModel(MerkatoDbContext context, TrainingDefinition B) : this(context)
        {
            this.Id = B.Id;
            this.JobId = B.JobId;
            this.TrainingName = B.TrainingName;
            this.TraningMaterial = B.TraningMaterial;
        }
        public TrainingDefinition GetModel()
        {
            TrainingDefinition b = new TrainingDefinition();
            b.Id = this.Id;
            b.JobId = this.JobId;
            b.TrainingName = this.TrainingName;
            b.TraningMaterial = this.TraningMaterial;

            return b;
        }
    }
}
