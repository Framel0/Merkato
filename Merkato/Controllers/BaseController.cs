using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merkato.Data;
using Merkato.Models;
using Merkato.Lib.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Merkato.Lib.Models;

namespace Merkato.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ApplicationDbContext context;
        private readonly ILogger _logger;
        private readonly MerkatoDbContext dbContext;

        public BaseController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext context, MerkatoDbContext dbContext, ILogger<BaseController> logger)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            this.dbContext = dbContext;
            this._logger = logger;
        }

        public BaseController()
        {

        }

        public CallResult<int> GetCurrentAgentId()
        {
            CallResult<int> result = new CallResult<int>();

            try
            {
                //MerkatoDbContext context = new MerkatoDbContext();

                using (var context = new MerkatoDbContext(null))
                {
                    Agent agent = context.Agent.Where(a => a.AgentAppUserName.Equals(User.Identity.Name)).SingleOrDefault();
                    result.Model = agent.Id;
                    result.HasError = 0;
                }
           
            }
            catch (Exception ex)
            {
                result.HasError = 1;
                result.Error = "Agent Not Found";
                result.InternalError = ex.Message;
            }

            return result;
        }

        public CallResult<int> GetCurrentClientId()
        {
            CallResult<int> result = new CallResult<int>();

            try
            {
                var client = dbContext.Client.Where(a => a.ClientAppUserName.Equals(User.Identity.Name)).SingleOrDefault();
                result.Model = client.Id;
                result.HasError = 0;
            }
            catch (Exception ex)
            {
                result.HasError = 1;
                result.Error = "Client Not Found";
                result.InternalError = ex.Message;
            }




            return result;
        }

        public string GetCurrentUserRoleId()
        {           
            var user = context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var  role = roleManager.Roles.Single(r => r.Name == userManager.GetRolesAsync(user).Result.Single()).Id;
            return role;
        }

        public string GetCurrentUserRoleName()
        {
            var user = context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var role = roleManager.Roles.Single(r => r.Name == userManager.GetRolesAsync(user).Result.Single()).Name;
            return role;
        }

        public async Task<CallResult<ApplicationUser>> CreateUser(UserViewModel model)
        {
            CallResult<ApplicationUser> callResult = new CallResult<ApplicationUser>();
            try
            {
                ApplicationUser user = new ApplicationUser
                {
                    //UserName = model.UserName,
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
            catch (Exception ex)
            {
                callResult.HasError = 1;
                callResult.InternalError = ex.Message;
                callResult.Error = "Internal Error";

            }

            return callResult;
        }

    }
}