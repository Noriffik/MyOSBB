using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyOSBB.DAL.Models;
using MyOSBB.DAL.Models.Invoices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyOSBB.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (await roleManager.FindByNameAsync("Admins") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admins"));
            }
            if (await roleManager.FindByNameAsync("Users") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Users"));
            }

            string username = configuration["AdminUser:Name"];
            string email = configuration["AdminUser:Email"];
            string password = configuration["AdminUser:Password"];
            string role = configuration["AdminUser:Role"];
            bool emailConfirmed = configuration["AdminUser:EmailConfirmed"] == "true" ? true : false;

            var admin = await userManager.FindByNameAsync(username);

            if (admin == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                ApplicationUser user = new ApplicationUser
                {
                    UserName = username,
                    Email = email,
                    EmailConfirmed = emailConfirmed
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<InvoiceElectro> InvoiceElectros { get; set; }
        public DbSet<InvoiceGaz> InvoiceGazs { get; set; }
        public DbSet<InvoiceService> InvoiceServices { get; set; }
        public DbSet<InvoiceTel> InvoiceTels { get; set; }
        public DbSet<InvoiceWater> InvoiceWaters { get; set; }
        public DbSet<Month> Months { get; set; }
    }
}
