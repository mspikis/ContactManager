using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ContactManager.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ContactManagerContext(
                serviceProvider.GetRequiredService<DbContextOptions<ContactManagerContext>>()))
            {
                // Look for any movies.
                if (context.Individual.Any())
                {
                    return;   // DB has been seeded
                }

                context.Individual.AddRange(
                     new Individual
                     {
                         Forename = "Sally",
                         Surname ="Johnson",
                         ID =1,
                         Address = "15 Poland Street",
                         FeePerHour = 35
                     },

                     new Individual
                     {
                         Forename = "r",
                         Surname = "g",
                         ID = 2,
                         Address = "15 Poland Street",
                         FeePerHour = 35
                     },

                     new Individual
                     {
                         Forename = "fgdf",
                         Surname = "gsdd",
                         ID = 3,
                         Address = "15 Poland Street",
                         FeePerHour = 35
                     }
                );
                context.SaveChanges();
            }
        }
    }
}