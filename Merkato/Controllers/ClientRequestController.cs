using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;
using Merkato.Lib.ViewModels;
using Merkato.Models;

namespace Merkato.Controllers
{
    public class ClientRequestController : Controller
    {
        private readonly MerkatoDbContext _context;
        Repository repository;


        public ClientRequestController(MerkatoDbContext context)
        {
            _context = context;
            repository = new Repository();

        }

        // GET: ActivityDetails
        public async Task<IActionResult> Index()
        {
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
          NbAgentShift4 = b.ClientRequest.NbAgentShift4,
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

        public async Task<IActionResult> ClientRequest()
        {

            //var data = await _context.ClientRequestDetails
            //       .Include(d => d.ClientRequest)
            //       .ThenInclude(d => d.Mechanism)
            //       .Select(b => new ClientRequestDetailsViewModel
            //       {
            //           Id = b.Id,
            //             //ClientId = b.ClientId,
            //             //OutletId = b.OutletId,
            //             //MechanismId = b.MechanismId,
            //             //StartDate = b.StartDate,
            //             //EndDate = b.EndDate,
            //             //Days = b.Days,
            //             BatchNo = b.ClientRequest.BatchNo,
            //           NbAgentMorning = b.ClientRequest.NbAgentShift1,
            //           NbAgentAfternoon = b.ClientRequest.NbAgentShift2,
            //           NbAgentEvening = b.ClientRequest.NbAgentShift3,
            //           ClientName = b.ClientRequest.Client.ClientName,
            //           OutletName = b.ClientRequest.Outlet.Name,
            //           MechanismName = b.ClientRequest.Mechanism.Mechanism.Name,
            //           LanguageName = b.ClientRequest.Language.Name,
            //           Date = b.Date
            //             ////skillName = b.Skill.Name,
            //             ////GenderName = b.Gender.Name,
            //             ////MaritalStatusName = b.MaritalStatus.Name,
            //             ////GradeName = b.Grade.Name
            //         }).ToListAsync();
            //return View(data);

            if (User.IsInRole("Client"))
            {
                var client = _context.Client.Where(a => a.ClientAppUserName.Equals(User.Identity.Name)).SingleOrDefault();

                var clientId = client.Id;

                //var clientId = repository.GetCurrentClientId(User.Identity.Name);

                if (clientId == 0)
                    return NotFound();

                var data = await _context.ClientRequestDetails
                .Include(d => d.ClientRequest)
                .ThenInclude(d => d.Mechanism)
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
                    NbAgentShift4 = b.ClientRequest.NbAgentShift4,
                    ClientName = b.ClientRequest.Client.ClientName,
                    OutletName = b.ClientRequest.Outlet.Name,
                    MechanismName = b.ClientRequest.Mechanism.Mechanism.Name,
                    LanguageName = b.ClientRequest.Language.Name,
                    Date = b.Date,
                    ClientId = b.ClientRequest.Client.Id
                    ////skillName = b.Skill.Name,
                    ////GenderName = b.Gender.Name,
                    ////MaritalStatusName = b.MaritalStatus.Name,
                    ////GradeName = b.Grade.Name
                }).Where(b => b.ClientId == clientId).ToListAsync();
                return View(data);
            }
            else
            {
                var data = await _context.ClientRequestDetails
                     .Include(d => d.ClientRequest)
                     .ThenInclude(d => d.Mechanism)
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
                         NbAgentShift4 = b.ClientRequest.NbAgentShift4,
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

            }
            //return View(await _context.ActivityDetails.ToListAsync());
        }

        // GET: ActivityDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityDetails = await _context.ClientRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityDetails == null)
            {
                return NotFound();
            }

            return View(activityDetails);
        }

        // GET: ActivityDetails/Create
        public IActionResult Create()
        {
            ClientRequestViewModel vm;
            vm = new ClientRequestViewModel(_context);
            return View(vm);
        }

        // POST: ActivityDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientRequestViewModel activityDetails)
        {

            var clientId = repository.GetCurrentAgentId(User.Identity.Name);

            //Generate number
            Random rd = new Random();
            Int32 batchCode = rd.Next(9999);
            activityDetails.BatchNo = "B-" + batchCode.ToString();
            if (ModelState.IsValid)
            {
                ClientRequest request = new ClientRequest();
                {
                    request.Id = activityDetails.Id;
                    request.BatchNo = activityDetails.BatchNo;
                    request.ClientId = clientId;
                    request.OutletId = activityDetails.OutletId;
                    request.StartDate = activityDetails.StartDate;
                    request.EndDate = activityDetails.EndDate;
                    request.Days = activityDetails.Days;
                    request.Age = activityDetails.Age;
                    request.NbAgentShift1 = activityDetails.NbAgentShift1;
                    request.NbAgentShift2 = activityDetails.NbAgentShift2;
                    request.NbAgentShift3 = activityDetails.NbAgentShift3;
                    request.NbAgentShift4 = activityDetails.NbAgentShift4;
                    request.MechanismId = activityDetails.MechanismId;
                    request.GenderId = activityDetails.GenderId;
                    request.LanguageId = activityDetails.LanguageId;
                    request.GradeId = activityDetails.GradeId;
                    request.SkillId = activityDetails.SkillId;
                }

                if (request.GenderId == 0)
                {
                    request.GenderId = null;
                }

                if (request.LanguageId == 0)
                {
                    request.LanguageId = null;
                }

                if (request.GradeId == 0)
                {
                    request.GradeId = null;
                }

                if (request.SkillId == 0)
                {
                    request.SkillId = null;
                }

                _context.Add(request);
                await _context.SaveChangesAsync();

                ClientRequestDetailsViewModel details = new ClientRequestDetailsViewModel();

                int id = request.Id;

                details.ClientRequestId = request.Id;

                Util util = new Util();
                foreach (DateTime day in util.EachDay(activityDetails.StartDate, activityDetails.EndDate))
                {
                    var a = (int)day.DayOfWeek;
                    foreach (char d in activityDetails.Days)
                    {
                        var val = (int)Char.GetNumericValue(d);
                        if (a == val)
                        {
                            details.Date = day;
                            _context.Add(details.GetModel());
                            await _context.SaveChangesAsync();
                        }
                    }

                }

                return RedirectToAction(nameof(ClientRequest));
            }
            activityDetails.loadLists(_context);
            return View(activityDetails);
        }

        // GET: ActivityDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ClientRequestViewModel vm;
            if (id == 0)
            {
                vm = new ClientRequestViewModel(_context);
            }
            else
            {
                var activityDetails = await _context.ClientRequest.FindAsync(id);
                if (activityDetails == null)
                {
                    return NotFound();
                }
                vm = new ClientRequestViewModel(_context, activityDetails);
            }

            return View(vm);
        }

        // POST: ActivityDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientRequestViewModel activityDetails)
        {
            if (id != activityDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activityDetails.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityDetailsExists(activityDetails.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            activityDetails.loadLists(_context);
            return View(activityDetails);
        }

        // GET: ActivityDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityDetails = await _context.ClientRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activityDetails == null)
            {
                return NotFound();
            }

            return View(activityDetails);
        }

        // POST: ActivityDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activityDetails = await _context.ClientRequest.FindAsync(id);
            _context.ClientRequest.Remove(activityDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityDetailsExists(int id)
        {
            return _context.ClientRequest.Any(e => e.Id == id);
        }
    }
}
