using System;
using Microsoft.EntityFrameworkCore;
using ContactManager.Models;

namespace ContactManager.Models
{
    public class ContactManagerContext: DbContext 
    {
        public ContactManagerContext(DbContextOptions<ContactManagerContext> options)
             : base(options)
        {
        }

        public DbSet<ContactManager.Models.Individual> Individual { get; set; }

        public DbSet<ContactManager.Models.Company> Company { get; set; }
    }
}
