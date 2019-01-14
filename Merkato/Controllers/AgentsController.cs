using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Merkato.Lib.Models.ServiceModel;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Merkato.Lib.ViewModels;
using Merkato.Models;

namespace Merkato.Controllers
{

    //[Authorize]
    /// <summary>
    /// 
    /// </summary>
    public class AgentsController : Controller
    {
        private readonly MerkatoDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _appEnvironment;

        Repository repository;

        EmailNotificationSender _mailSender;
        SmtpConfig _smtpConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="logger"></param>
        /// <param name="hostingEnvironment"></param>
        public AgentsController(MerkatoDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ILogger<AgentsController> logger, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _appEnvironment = hostingEnvironment;
            repository = new Repository();
            _mailSender = new EmailNotificationSender();
            _smtpConfig = new SmtpConfig();

        }

        // GET: Agents
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrator, Super Administrator, Officer, Agent")]
        public async Task<IActionResult> Index()
        {

            if (User.IsInRole("Agent"))
            {
                //var agent = _context.Agent.Where(a => a.AgentAppUserName.Equals(User.Identity.Name)).SingleOrDefault();

                //var agentId = agent.Id;
                var agentId = repository.GetCurrentAgentId(User.Identity.Name);

                _logger.LogInformation("Getting Agent item {ID}", agentId);

                if (agentId == 0)
                {
                    _logger.LogWarning("GetById({ID}) NOT FOUND", agentId);
                    return NotFound();

                }

                List<Agent> rawList = await _context.Agent.Where(a => a.Id == agentId).ToListAsync();
                List<AgentStatus> agentStatuses = await _context.AgentStatus.ToListAsync();

                List<AgentSmallViewModel> list = new List<AgentSmallViewModel>();
                foreach (var item in rawList)
                {
                    list.Add(new AgentSmallViewModel(item, agentStatuses));
                }

                return View(list);
            }
            else
            {
                List<Agent> rawList = await _context.Agent.ToListAsync();
                List<AgentStatus> agentStatuses = await _context.AgentStatus.ToListAsync();

                List<AgentSmallViewModel> list = new List<AgentSmallViewModel>();
                foreach (var item in rawList)
                {
                    list.Add(new AgentSmallViewModel(item, agentStatuses));
                }

                return View(list);
            }



        }

        // GET: Agent ActivityDetails
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> AgentActivities(int id)
        {
            ViewBag.AgentId = id;
            var data = await _context.ClientRequestDetails
                  .Include(d => d.ClientRequest)
        .Select(b => new ClientRequestDetailsViewModel
        {
            Id = b.Id,
            //ClientId = b.ClientId,
            //OutletId = b.OutletId,
            //MechanismId = b.MechanismId,
            //StartDate = b.StartDate,
            //EndDate = b.EndDate,
            //Days = b.Days,
            BatchNo = b.ClientRequest.BatchNo,
            NbAgentShift1 = b.ClientRequest.NbAgentShift1,
            NbAgentShift2 = b.ClientRequest.NbAgentShift2,
            NbAgentShift3 = b.ClientRequest.NbAgentShift3,
            ClientName = b.ClientRequest.Client.ClientName,
            OutletName = b.ClientRequest.Outlet.Name,
            MechanismName = b.ClientRequest.Mechanism.Mechanism.Name,
            LanguageName = b.ClientRequest.Language.Name,
            Date = b.Date
            ////skillName = b.Skill.Name,
            ////GenderName = b.Gender.Name,
            ////MaritalStatusName = b.MaritalStatus.Name,
            ////GradeName = b.Grade.Name
        }).ToListAsync();
            return View(data);
            //return View(await _context.ActivityDetails.ToListAsync());
        }

        // GET: Agents/Details/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            AgentViewModel vm;
            if (id == 0)
            {
                vm = new AgentViewModel(_context);
            }
            else
            {
                var agent = await _context.Agent
                             .FirstOrDefaultAsync(m => m.Id == id);
                if (agent == null)
                {
                    return NotFound();
                }
                vm = new AgentViewModel(_context, agent);
            }

            return View(vm);
        }

        // GET: Agents/Create
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            AgentViewModel vm;

            vm = new AgentViewModel(_context);

            return View(vm);
        }

        // POST: Agents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AgentViewModel agent)
        {
            Random rd = new Random();
            Int32 passCode = rd.Next(9999);

            // DO BE EDIT BACK
            if (!ModelState.IsValid)
            {
                if (agent.Status == 1)
                {
                    if (agent.EmpCode == null)
                        agent.EmpCode = "A-" + passCode;
                    //agent.AgentAppUserName = agent.FirstName + "." + agent.SurName;                    

                    agent.AgentAppUserName = agent.PersonalEmail;
                    var password = agent.SurName + passCode + "@Merkato";
                    //var password = "Agent@123";
                    agent.AgentAppPassword = password;
                    var userName = agent.AgentAppUserName;
                    var email = agent.PersonalEmail;
                    ApplicationRole applicationRole = await _roleManager.FindByNameAsync("Agent");
                    var role = applicationRole.Id;

                    UserViewModel user = new UserViewModel();
                    {
                        //user.UserName = userName;
                        user.Email = email;
                        user.Password = password;
                        user.ConfirmPassword = password;
                        user.ApplicationRoleId = role;
                    }

                    Repository repository = new Repository(_userManager, _roleManager);

                    await repository.CreateUser(user);

                    agent.IsActive = true;

                    //var httpRequest = HttpContext.Request.Form.Files;

                    //var files = new List<string>();
                    //foreach (var Image in files)
                    //{

                    //    if (Image != null && Image.Length > 0)
                    //    {
                    //        var file = Image;
                    //        var postedFile = files[0];
                    //        //There is an error here
                    //        if (file.Length > 0)
                    //        {
                    //            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

                    //            var filePath = Path.Combine(_appEnvironment.WebRootPath, "images\\Agents");

                    //            using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    //            {
                    //               await file.CopyToAsync(fileStream);

                    //                //files.Add(filePath);

                    //                agent.FirstPictureUrl = fileName;

                    //                //result = fileName;
                    //            }

                    //        }
                    //    }
                    //}
                  
                    _context.Add(agent.GetModel());
                    await _context.SaveChangesAsync();

                    _mailSender.SendEmail(agent.PersonalEmail, "Merkato Notification",
                     $"Merkato has been accepted your request, kindly find your credentials below.{Environment.NewLine} -Username :{agent.AgentAppUserName} {Environment.NewLine} -Password:{agent.AgentAppPassword}"
                     + $"{Environment.NewLine} -Agent Code:{agent.EmpCode}"
                     + $"{Environment.NewLine} -Application link http://173.248.135.167/Merkato"
                     + $"{Environment.NewLine}  {Environment.NewLine} {Environment.NewLine} Merkato Team", _context,false);

                    return RedirectToAction(nameof(Index));


                }
                else
                {
                    agent.IsActive = false;
                    _context.Add(agent.GetModel());
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            agent.loadLists(_context);
            return View(agent);
        }

        // GET: Agents/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            AgentViewModel vm;
            if (id == 0)
            {
                vm = new AgentViewModel(_context);
            }
            else
            {
                var agent = await _context.Agent.FindAsync(id);
                if (agent == null)
                {
                    return NotFound();
                }
                vm = new AgentViewModel(_context, agent);
            }

            return View(vm);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agent"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AgentViewModel agent)
        {
            if (id != agent.Id)
            {
                return NotFound();
            }
            Random rd = new Random();
            Int32 passCode = rd.Next(9999);

            if (ModelState.IsValid)
            {
                try
                {


                    if (!agent.IsActive && agent.Status == 1)
                    {
                        if (agent.EmpCode == null)
                            agent.EmpCode = "A-" + passCode;
                        ////agent.AgentAppUserName = agent.FirstName + "." + agent.SurName;
                        //agent.AgentAppUserName = agent.PersonalEmail;
                        //var password = agent.SurName + passCode + "@Merkato";
                        ////var password = "Agent@123";
                        //agent.AgentAppPassword = password;
                        //var userName = agent.AgentAppUserName;
                        //var email = agent.PersonalEmail;
                        //ApplicationRole applicationRole = await _roleManager.FindByNameAsync("Agent");
                        //var role = applicationRole.Id;

                        //UserViewModel user = new UserViewModel();
                        //{
                        //    //user.UserName = userName;
                        //    user.Email = email;
                        //    user.Password = password;
                        //    user.ConfirmPassword = password;
                        //    user.ApplicationRoleId = role;
                        //}

                        //Repository repository = new Repository(_userManager, _roleManager);

                        //await repository.CreateUser(user);

                        //agent.IsActive = true;

                        _context.Update(agent.GetModel());
                        await _context.SaveChangesAsync();


                        _mailSender.SendEmail(agent.PersonalEmail, "Merkato Notification",
$"Merkato has been approved your request, kindly start with the training.{Environment.NewLine} -Username :{agent.PersonalEmail} {Environment.NewLine} -Password:{agent.AgentAppPassword}"
+ $"{Environment.NewLine} -Application link http://173.248.135.167/Merkato"
+ $"{Environment.NewLine}  {Environment.NewLine} {Environment.NewLine} Merkato Team", _context, false);

                    }
                    else
                    {
                        _context.Update(agent.GetModel());
                        await _context.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentExists(agent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //if (User.IsInRole("Agent"))
                //    return RedirectToAction(nameof(AgentView));
                return RedirectToAction(nameof(Index));
            }
            agent.loadLists(_context);
            return View(agent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult AgentView()
        {
            return View();
        }

        // GET: Agents/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // POST: Agents/Delete/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agent = await _context.Agent.FindAsync(id);
            _context.Agent.Remove(agent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentExists(int id)
        {
            return _context.Agent.Any(e => e.Id == id);
        }

    
    }
}
