using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Advertisement")]
    public class AdvertisementModels
    {
        public int ID { set; get; }

        public int CarID { set; get; }

        [Required]
        public bool Active { set; get; }

        [DataType(DataType.Currency)]
        [Required]
        [Display(Name = "Offer Amount")]
        public double OfferAmount { get; set; }
        public virtual CarModels Car { get; set; }
    }
}