using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;
using Merkato.Lib.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Merkato.Lib.Models.ServiceModel;
using Merkato.Models;

namespace Merkato.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly MerkatoDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        Repository repository;
        EmailNotificationSender mailSender;
        public ClientsController(MerkatoDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            repository = new Repository();
            mailSender = new EmailNotificationSender();
        }

        // GET: Clients
        [Authorize(Roles = "Administrator, Super Administrator, Officer, Client")]
        public async Task<IActionResult> Index()
        {
          
            if (User.IsInRole("Client"))
            {
                var client = _context.Client.Where(a => a.ClientAppUserName.Equals(User.Identity.Name)).SingleOrDefault();

                var clientId = client.Id;

                //var clientId = repository.GetCurrentClientId(User.Identity.Name);

                if (clientId == 0)
                    return NotFound();

                var data = await _context.Client.
              Select(b => new ClientViewModel
              {
                  Id = b.Id,
                  ClientCode = b.ClientCode,
                  ClientName = b.ClientName,
                  Address = b.Address,
                  Email = b.Email,
                  PhoneNumber = b.PhoneNumber,
                  FirstContactName = b.FirstContactName,
                  FirstContactEmail = b.FirstContactEmail,
                  FirstContactPhone = b.FirstContactPhone,
                  SecondContactName = b.SecondContactName,
                  SecondContactEmail = b.SecondContactEmail,
                  SecondContactPhone = b.SecondContactPhone,
                  ActiveString = b.Status == 1 ? "active" : "inactive"
              }).Where(b=>b.Id==clientId).ToListAsync();
                return View(data);
            }
            else
            {
                var data = await _context.Client.
            Select(b => new ClientViewModel
            {
                Id = b.Id,
                ClientCode = b.ClientCode,
                ClientName = b.ClientName,
                Address = b.Address,
                Email = b.Email,
                PhoneNumber = b.PhoneNumber,
                FirstContactName = b.FirstContactName,
                FirstContactEmail = b.FirstContactEmail,
                FirstContactPhone = b.FirstContactPhone,
                SecondContactName = b.SecondContactName,
                SecondContactEmail = b.SecondContactEmail,
                SecondContactPhone = b.SecondContactPhone,
                ActiveString = b.Status == 1 ? "active" : "inactive"
            }).ToListAsync();
                return View(data);
            }
            //return View(await _context.Client.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ClientViewModel vm;
            if (id == 0)
            {
                vm = new ClientViewModel(_context);
            }
            else
            {
                var client = await _context.Client
                               .FirstOrDefaultAsync(m => m.Id == id);
                if (client == null)
                {
                    return NotFound();
                }

                vm = new ClientViewModel(_context, client);
            }

            return View(vm);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            ClientViewModel vm;
            vm = new ClientViewModel(_context);

            return View(vm);
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel client)
        {
            Random rd = new Random();
            Int32 passCode = rd.Next(9999);
            if (ModelState.IsValid)
            {
                if (client.ClientCode == null)
                    client.ClientCode = "C-" + passCode;
                //client.ClientAppUserName = client.ClientName;
                client.ClientAppUserName = client.Email;
                var userName = client.ClientAppUserName;
                var email = client.Email;
                var password = client.ClientName + passCode + "@Merkato";
                //var password = "Client@123";
                client.ClientAppPassword = password;
                ApplicationRole applicationRole = await _roleManager.FindByNameAsync("Client");
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

                _context.Add(client.GetModel());
                await _context.SaveChangesAsync();

                mailSender.SendEmail(client.Email, "Merkato Notification",
                      $"{client.ClientName} Successffuly registered, kindly find your credentials below.{Environment.NewLine} -Username :{client.ClientAppUserName} {Environment.NewLine} -Password:{client.ClientAppPassword}"
                     + $"{Environment.NewLine} -Client Code:{client.ClientCode}"                   
                      + $"{Environment.NewLine} -Application link http://173.248.135.167/Merkato"
                      + $"{Environment.NewLine}  {Environment.NewLine} {Environment.NewLine} Merkato Team",_context, false);


                return RedirectToAction(nameof(Index));
            }
            client.loadLists(_context);
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ClientViewModel vm;

            if (id == 0)
            {
                vm = new ClientViewModel(_context);
            }
            else
            {
                var client = await _context.Client.FindAsync(id);
                if (client == null)
                {
                    return NotFound();
                }
                vm = new ClientViewModel(_context, client);
            }

            return View(vm);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientViewModel client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //client.ClientAppUserName = client.ClientName;
                    //var password = "Client@123";
                    //client.ClientAppPassword = password;
                    _context.Update(client.GetModel());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //if (User.IsInRole("Client"))
                //    return RedirectToAction(nameof(ClientView));
                return RedirectToAction(nameof(Index));
            }
            client.loadLists(_context);
            return View(client);
        }

        public IActionResult ClientView()
        {
            return View();
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }


        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
    }
}
