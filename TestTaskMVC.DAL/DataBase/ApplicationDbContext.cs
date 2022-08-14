using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

using TestTaskMVC.DomainModels.Models;

namespace TestTaskMVC.DAL.DataBase
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Client>()
            //    .HasOne(s=>s.Adress)
            //    .WithOne(ad => ad.Client);

            base.OnModelCreating(builder);
        }


        public DbSet<Client> Clients { get; set; }
        public DbSet<Adress> Adresses { get;set; }
        
    }
}