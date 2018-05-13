using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyOSBB.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyOSBB.DAL.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            //context.Database.EnsureCreated();
            context.Database.Migrate();

            ApplicationDbContext.CreateAdminAccount(serviceProvider, configuration).Wait();

            if (context.Months.Any())
            {
                return;   // DB has been seeded
            }

            // January, February, March, April, May, June, July, August, September, October, November, December
            var months = new Month[]
            {
                new Month(){ Name = "January" },
                new Month(){ Name = "February" },
                new Month(){ Name = "March" },
                new Month(){ Name = "April" },
                new Month(){ Name = "May" },
                new Month(){ Name = "June" },
                new Month(){ Name = "July" },
                new Month(){ Name = "August" },
                new Month(){ Name = "September" },
                new Month(){ Name = "October" },
                new Month(){ Name = "November" },
                new Month(){ Name = "December" },
            };

            foreach (Month s in months)
            {
                context.Months.Add(s);
            }
            context.SaveChanges();
        }
    }
}
