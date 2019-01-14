using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merkato.Lib.Models
{
   public class DataManager
    {
        public async Task<CallResult<Agent>> PreApprovedAgent(string email, MerkatoDbContext ctx)
        {
            CallResult<Agent> result = new CallResult<Agent>();

            try
            {

                    var agent = await ctx.Agent.Where(c => c.PersonalEmail.Equals(email)).SingleOrDefaultAsync();

                    if (agent != null)
                    {
                        result.Model = agent;
                        agent.Status = 3;
                         ctx.Update(agent);
                        await ctx.SaveChangesAsync();

                    }
                    else
                    {
 
                        result.Error = "User does not exist";
                    }


              

            }
            catch (Exception ex)
            {
                result.InternalError = ex.Message;
                result.Error = "Update failed";
            }

            return result;
        }
    }
}
