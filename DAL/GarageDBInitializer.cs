using ClassicGarage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClassicGarage.DAL
{
    public class GarageDBInitializer<T>:CreateDatabaseIfNotExists<GarageContext>
    {
        protected override void Seed(GarageContext context)
        {

            IList<BrandModel> brandModels = new List<BrandModel>
            {
                new BrandModel(){Name = "Abarth"},
                new BrandModel(){Name = "Audi"},
                new BrandModel(){Name = "BMW"},
                new BrandModel(){Name = "Citroen"},
                new BrandModel(){Name = "Maserati"},
                new BrandModel(){Name = "Mercedes"}
            };


         
           foreach (var item in brandModels)
           {
               context.Brand.Add(item);
            }

            context.SaveChanges();
        }
    }
}