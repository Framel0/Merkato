using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Merkato.Lib.Models;
using Merkato.Lib.ViewModels;
using Merkato.Models;

namespace Merkato.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<UserListViewModel> UserListViewModel { get; set; }

        public DbSet<ApplicationRole> ApplicationRole { get; set; }

        public DbSet<ApplicationRoleListViewModel> ApplicationRoleListViewModel { get; set; }

        public DbSet<ApplicationRoleViewModel> ApplicationRoleViewModel { get; set; }

    }
}
