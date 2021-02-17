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

            BrandModel brand1 = new BrandModel() { Name = "Ford" };

            OwnerModels user1 = new OwnerModels() { FirstName = "Mateusz", LastName = "Wypych", Email = "matjas9@gmail.com", PhoneNo = 506666666, ID = 1 };

            DateTime newDAte = DateTime.Now;


            IList<CarModels> carModels = new List<CarModels>
            {
                new CarModels(){Brand=brand1, ID=1, Model="Ka", Owner=user1, BrandID=7, PurchaseDate=newDAte,
                    OwnerId=1, PurchasePrice=30000, Series=1, VIN=4445556667, Year=1998, SaleDate=newDAte}
            };


            foreach (var item in brandModels)
            {
               context.Brand.Add(item);
            }

            context.SaveChanges();
        }
    }
}