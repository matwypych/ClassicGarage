using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    public class BrandModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CarModels> Cars { get; set; }
    }
}