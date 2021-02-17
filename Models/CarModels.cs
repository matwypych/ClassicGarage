using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Cars")]
    public class CarModels
    {
        public int ID { set; get; }
        
        [Required]
        public int BrandID { set; get; }

        [Required]
        public string Model { set; get; }

        [Required]
        public int Year { set; get; }

        [Required]
        [Display(Name = "VIN Number")]
        public long VIN { set; get; }

        [Required]
        public int Series { set; get; }

        [Display(Name = "Picture")]
        public string Photo { set; get; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
              ApplyFormatInEditMode = true)]
        [Display(Name = "Purchase date")]
        public DateTime PurchaseDate { set; get; }

      
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        [Display(Name = "Sale date")]
        public DateTime? SaleDate { set; get; }

        [DataType(DataType.Currency)]
        [Display(Name = "Purchase price")]
        public double PurchasePrice { set; get; }

        [DataType(DataType.Currency)]
        [Display(Name = "Sale price")]
        public double? SellPrice { set; get; }

        public int? OwnerId { set; get; }


        public virtual OwnerModels Owner { get; set; }
        public virtual ICollection<PartsModels> Parts { get; set; }
        public virtual ICollection<RepairModels> Repairs { get; set; }
        public virtual ICollection<AdvertisementModels> Advertisement { get; set; }
        public virtual BrandModel Brand { set; get; }
    }
}