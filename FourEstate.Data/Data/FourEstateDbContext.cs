using FourEstate.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Data
{
    public class FourEstateDbContext : IdentityDbContext<User>
    {
        public FourEstateDbContext(DbContextOptions<FourEstateDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<RealEstatetAttachment> RealEstatetAttachments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<ContentChangeLog> ContentChangeLogs { get; set; }



    }
}
