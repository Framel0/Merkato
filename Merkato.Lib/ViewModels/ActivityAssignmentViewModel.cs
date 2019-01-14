using  Merkato.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  Merkato.Lib.ViewModels
{
    public class ActivityAssignmentViewModel:ActivityAssignment
    {
        public ActivityAssignmentViewModel()
        {

        }
        public ActivityAssignmentViewModel(MerkatoDbContext context)
        {

        }

        public void loadLists(MerkatoDbContext context)
        {

        }

        public ActivityAssignmentViewModel(MerkatoDbContext context, ActivityAssignment A) : this(context)
        {
            this.Id = A.Id;
            this.ActivityId = A.ActivityId;
            this.AgentId = A.AgentId;
            this.AssignmentDate = A.AssignmentDate;

        }

        public ActivityAssignment GetModel()
        {
            ActivityAssignment assignment = new ActivityAssignment();

            assignment.Id = this.Id;
            assignment.ActivityId = this.ActivityId;
            assignment.AgentId = this.AgentId;
            assignment.AssignmentDate = DateTime.Now;

            return assignment;
        }
    }
}
