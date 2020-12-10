using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RegistruCentras.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistruCentras.Data
{
    public class Context : IdentityDbContext<AppUser>
    {
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<RequestForInfo> Requests { get; set; }
        public DbSet<ResponseInfo> Responses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RegistruCentras;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
