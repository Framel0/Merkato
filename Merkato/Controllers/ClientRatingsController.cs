using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;
using Merkato.Lib.ViewModels;

namespace Merkato.Controllers
{
    public class ClientRatingsController : Controller
    {
        private readonly MerkatoDbContext _context;

        public ClientRatingsController(MerkatoDbContext context)
        {
            _context = context;
        }

        // GET: ClientRatings
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
        }

        // GET: ClientRatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientRating = await _context.ClientRating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientRating == null)
            {
                return NotFound();
            }

            return View(clientRating);
        }

        // GET: ClientRatings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActivityId,RatingDate,RatingTypeId,Score")] ClientRating clientRating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientRating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientRating);
        }

        // GET: ClientRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientRating = await _context.ClientRating.FindAsync(id);
            if (clientRating == null)
            {
                return NotFound();
            }
            return View(clientRating);
        }

        // POST: ClientRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActivityId,RatingDate,RatingTypeId,Score")] ClientRating clientRating)
        {
            if (id != clientRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientRatingExists(clientRating.Id))
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
            return View(clientRating);
        }

        // GET: ClientRatings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientRating = await _context.ClientRating
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientRating == null)
            {
                return NotFound();
            }

            return View(clientRating);
        }

        // POST: ClientRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientRating = await _context.ClientRating.FindAsync(id);
            _context.ClientRating.Remove(clientRating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientRatingExists(int id)
        {
            return _context.ClientRating.Any(e => e.Id == id);
        }
    }
}
