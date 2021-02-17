using ClassicGarage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ClassicGarage.DAL
{
    public class GarageContext : DbContext
    {
        public GarageContext():base("ClassicGarage")
        {
            Database.SetInitializer(new GarageDBInitializer<GarageContext>());
        }

        public DbSet<CarModels> Cars { get; set; }
        public DbSet<OwnerModels> Owners { get; set; }
        public DbSet<PartsModels> Parts { get; set; }
        public DbSet<RepairModels> Repairs { get; set; }
        public DbSet<AdvertisementModels> Advertisements { get; set; }

        public DbSet<BrandModel> Brand { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<BrandModel>()
                .HasMany(s => s.Cars)
                .WithRequired(m => m.Brand)
                .HasForeignKey(f => f.BrandID);

         
             

        }

    }
}