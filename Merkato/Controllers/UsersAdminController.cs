using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merkato.Data;
using Merkato.Models;
using Merkato.Models.AccountViewModels;
using Merkato.Lib.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Merkato.Controllers
{
    public class UsersAdminController : Controller
    {
        //public UserManager<ApplicationUser> userManager { get; private set; }
        //public RoleManager<ApplicationRole> roleManager { get; private set; }
        //public ApplicationDbContext context { get; private set; }

        //public UsersAdminController()
        //{
        //    context = new ApplicationDbContext(null);
        //    userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context),null, null,null,null,null,null,null,null);
        //     roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context), null, null, null, null);
        //}

        //public UsersAdminController(UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager)
        //{
        //    userManager = _userManager;
        //    roleManager = _roleManager;
        //}

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersAdminController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            model = userManager.Users.Select(u => new UserListViewModel
            {
                Id = u.Id,
                //UserName = u.UserName,
                Email = u.Email
        }).ToList();          
           return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            UserViewModel model = new UserViewModel();
            model.ApplicationRoles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {                  
                    UserName = model.Email,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ApplicationRole applicationRole = await roleManager.FindByIdAsync(model.ApplicationRoleId);
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await userManager.AddToRoleAsync(user, applicationRole.Name);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", roleResult.Errors.First().ToString());
                            model.ApplicationRoles = roleManager.Roles.Select(r => new SelectListItem
                            {
                                Text = r.Name,
                                Value = r.Id
                            }).ToList();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", result.Errors.First().ToString());
                    model.ApplicationRoles = roleManager.Roles.Select(r => new SelectListItem
                    {
                        Text = r.Name,
                        Value = r.Id
                    }).ToList();
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            EditUserViewModel model = new EditUserViewModel();
            model.ApplicationRoles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    //model.UserName = user.UserName;
                    model.Email = user.Email;
                    model.ApplicationRoleId = roleManager.Roles.Single(r => r.Name == userManager.GetRolesAsync(user).Result.Single()).Id;
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.UserName = model.Email;
                    user.Email = model.Email;
                    string existingRole = userManager.GetRolesAsync(user).Result.Single();
                    string existingRoleId = roleManager.Roles.Single(r => r.Name == existingRole).Id;
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (existingRoleId != model.ApplicationRoleId)
                        {
                            IdentityResult roleResult = await userManager.RemoveFromRoleAsync(user, existingRole);
                            if (roleResult.Succeeded)
                            {
                                ApplicationRole applicationRole = await roleManager.FindByIdAsync(model.ApplicationRoleId);
                                if (applicationRole != null)
                                {
                                    IdentityResult newRoleResult = await userManager.AddToRoleAsync(user, applicationRole.Name);
                                    if (newRoleResult.Succeeded)
                                    {
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", newRoleResult.Errors.First().ToString());
                                        model.ApplicationRoleId = roleManager.Roles.Single(r => r.Name == userManager.GetRolesAsync(user).Result.Single()).Id;

                                    }
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", roleResult.Errors.First().ToString());
                                model.ApplicationRoleId = roleManager.Roles.Single(r => r.Name == userManager.GetRolesAsync(user).Result.Single()).Id;
                            }
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser applicationUser = await userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    name = applicationUser.UserName;
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, FormCollection form)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser applicationUser = await userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    IdentityResult result = await userManager.DeleteAsync(applicationUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
    }
}