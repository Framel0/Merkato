using Merkato.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Merkato.Lib.ViewModels
{
   public class AgentStatusViewModel:AgentStatus
    {
        public AgentStatusViewModel()
        {

        }

        public AgentStatusViewModel(MerkatoDbContext context)
        {
            Id = 0;
        }

     

        public AgentStatusViewModel(MerkatoDbContext context, AgentStatus B) : this(context)
        {
            this.Id = B.Id;
            this.Code = B.Code;
            this.Name = B.Name;
            this.Description = B.Description;
        }
        public AgentStatus GetModel()
        {
            AgentStatus b = new AgentStatus();
            b.Id = this.Id;
            b.Code = this.Code;
            b.Name = this.Name;
            b.Description = this.Description;

            return b;

        }
    }
}
