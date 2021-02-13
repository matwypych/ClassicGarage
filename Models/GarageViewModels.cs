using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    public class GarageViewModels
    {
        public IEnumerable<OwnerModels> OwnerModels { get; set; }
        public IEnumerable<CarModels> CarModels { get; set; }
        public IEnumerable<BrandModel> BrandModels { get; set; }
        public IEnumerable<AdvertisementModels> AdsModels { get; set; }
        public IEnumerable<RepairModels> Repairs { get; set; }
        public IEnumerable<PartsModels> Parts { get; set; }

    }
}