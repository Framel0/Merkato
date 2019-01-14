using Merkato.Data;
using Merkato.Lib.Models;
using  Merkato.Lib.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace  Merkato.Models
{
    public class Repository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;



        public Repository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public Repository()
        {

        }
        public async Task<CallResult<ApplicationUser>> CreateUser(UserViewModel model)
        {
            CallResult<ApplicationUser> callResult = new  CallResult<ApplicationUser>();
            try
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                IdentityResult idResult = await userManager.CreateAsync(user, model.Password);
                if (idResult.Succeeded)
                {
                    ApplicationRole applicationRole = await roleManager.FindByIdAsync(model.ApplicationRoleId);
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await userManager.AddToRoleAsync(user, applicationRole.Name);
                        if (roleResult.Succeeded)
                        {
                            callResult.Model = user;
                            return callResult;
                        }
                        else
                        {
                            callResult.HasError = 1;
                            callResult.Error = "Unable to add  role for to the user";
                        }
                    }
                }
                else
                {
                    callResult.HasError = 1;
                    callResult.Error = "Unable to create the user";
                }

               
            }
            catch(Exception ex)
            {
                callResult.HasError = 1;
                callResult.InternalError = ex.Message;
                callResult.Error = "Internal Error";
                //_logger.LogError("User {user} Creation Failed", ex.Message);



            }

            return callResult;
        }

        public  int GetCurrentAgentId(string userName)
        {
            CallResult<int> result = new CallResult<int>();

            var agentId=0;
            try
            {
                MerkatoDbContext context = new MerkatoDbContext(null);

                Agent agent = context.Agent.Where(a => a.AgentAppUserName.Equals(userName)).SingleOrDefault();

                 agentId = agent.Id;
                result.HasError = 0;
            }
            catch(Exception ex)
            {
                result.HasError = 1;
                result.Error = "Agent Not Found";
                result.InternalError = ex.Message;
                //_logger.LogError("Agent {agent} retreiving Failed", ex.Message);
            }
                            
            return agentId;
        }

        public int GetCurrentClientId(string userName)
        {
            CallResult<int> result = new CallResult<int>();

            var clientId = 0;

            try
            {
                MerkatoDbContext dbContext = new MerkatoDbContext(null);
                var client = dbContext.Client.Where(a => a.ClientAppUserName == userName).SingleOrDefault();
                clientId = client.Id;
            }
            catch (Exception ex)
            {
                result.HasError = 1;
                result.Error = "Client Not Found";
                result.InternalError = ex.Message;
                //_logger.LogError("Client {client} retreiving Failed", ex.Message);
            }

            return clientId;
        }

    }
}
